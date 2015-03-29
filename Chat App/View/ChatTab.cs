using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat_Client.Model;
using Chat_Library.Model;
using System.Net.Sockets;
using Chat_Library.Controller;
using System.Net;
using System.Threading;
using System.Drawing.Imaging;

namespace Chat_Client
{
    public partial class ChatTab : UserControl
    {
        private ContextMenuStrip listboxContextMenu;

        public Client client { get; set; }
        public Client clientSelected { get; set; }

        public EventHandler CreatePrivateChat;

        public ChatTab(Client client)
        {
            InitializeComponent();
            this.client = client;

            this.listboxContextMenu = new ContextMenuStrip();
            this.listboxContextMenu.Opening += new CancelEventHandler(listboxContextMenu_Opening);
            this.listboxContextMenu.ItemClicked += new ToolStripItemClickedEventHandler(listboxContextMenu_ItemClicked);
            this.listBoxUsers.ContextMenuStrip = listboxContextMenu;

            new Thread(this.getMessages).Start();
            this.getConnectedClients();
        }

        private void listboxContextMenu_Opening(object sender, CancelEventArgs e)
        {
            // Clear the menu and add custom items.
            this.listboxContextMenu.Items.Clear();
            if (this.listBoxUsers.SelectedIndex != -1)
            {
                this.listboxContextMenu.Items.Add("Start private chat");
                clientSelected = this.client.Channel.getClient(listBoxUsers.SelectedItem.ToString());
            }
        }

        private void listboxContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.listboxContextMenu.Hide();
            if (e.ClickedItem.ToString().Equals("Start private chat"))
            {
                CreatePrivateChat(this, e);
            }
        }

        private void getConnectedClients()
        {
            Chat_Library.Model.Message message = new Chat_Library.Model.Message("ReqClients");
            this.client.Connection.sendMessage(message);
        }

        // Threaded method to continuously listen to receive message
        private void getMessages()
        {
            Chat_Library.Model.Message message;
            while (true)
            {
                if ((message = this.client.Connection.getMessage()) != null)
                {
                    if (message.cmd.Equals("ClientsList"))
                        this.Invoke((setConnectedClientList)setClientList, message);
                    else if (message.cmd.Equals("NewClient"))
                        this.Invoke((setConnectedClient)setClient, message);
                    else if (message.cmd.Equals("RemoveClient"))
                        this.Invoke((setRemovedClient)removeClient, message);
                    else if (message.cmd.Equals("NewMessage"))
                        if (message.getArg("text") != null)
                            this.Invoke((setNewText)setText, message);
                        else if (message.getArg("picture") != null)
                            this.Invoke((setNewPicture)setPicture, message);
                }
            }
        }

        private delegate void setConnectedClientList(Chat_Library.Model.Message message);
        private delegate void setConnectedClient(Chat_Library.Model.Message message);
        private delegate void setRemovedClient(Chat_Library.Model.Message message);
        private delegate void setNewText(Chat_Library.Model.Message message);
        private delegate void setNewPicture(Chat_Library.Model.Message message);

        // Set the whole clients connected list
        private void setClientList(Chat_Library.Model.Message message)
        {
            int i = 0;
            foreach (String name in message.getArgContents("name"))
            {
                this.listBoxUsers.Items.Add(name);
                i++;
            }
            this.txtBoxDiscussion.AppendText("There are currently " + i.ToString() + " users connected\n");
        }

        // Add a client to the connected client list
        private void setClient(Chat_Library.Model.Message message)
        {
            this.listBoxUsers.Items.Add(message.getArg("name"));
            this.txtBoxDiscussion.AppendText("Client " + message.getArg("name") + " is now connected\n");
        }

        // Remove a client to the connected client list
        private void removeClient(Chat_Library.Model.Message message)
        {
            this.listBoxUsers.Items.Remove(message.getArg("name"));
            this.txtBoxDiscussion.AppendText("Client " + message.getArg("name") + " has disconnected\n");
        }

        // Set received text into the txtboxdiscussion
        private void setText(Chat_Library.Model.Message message)
        {
            if (message.getArg("name") != null)
                this.txtBoxDiscussion.AppendText("<" + message.getArg("name") + "> " + message.getArg("text") + "\n");
            else
                foreach (String text in message.getArgContents("text"))
                    this.txtBoxDiscussion.AppendText(text + "\n");
        }

        // Set received picture into the txtboxdiscussion
        private void setPicture(Chat_Library.Model.Message message)
        {
            this.txtBoxDiscussion.AppendText("<" + message.getArg("name") + "> \n");
            Bitmap bitmap = new Bitmap(Base64ImageConverter.stringToImage(message.getArg("picture")));
            this.displayBitmap(bitmap);
        }

        // Ask to open picture and send it to broadcast
        private void pictureButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.png, *.jpg)|*.png;*.jpg";
            openFileDialog.Title = "Select a picture";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog.FileName);
                Size size;
                if(img.Width > img.Height)
                    size = new Size(50,img.Height * 50 / img.Width);
                else
                    size = new Size(img.Width * 50 / img.Height,50);
                Bitmap bitmap = new Bitmap(img, size);
                String pictureString = Base64ImageConverter.imageToString(bitmap, ImageFormat.Bmp);
                this.txtBoxDiscussion.AppendText("<Me> \n");
                this.displayBitmap(bitmap);
                Chat_Library.Model.Message message = new Chat_Library.Model.Message("Broadcast");
                message.addArgument("picture", pictureString);
                client.Connection.sendMessage(message);
            }
        }

        // Display bitmap in the txtboxdiscussion
        private void displayBitmap(Bitmap bitmap)
        {
            Clipboard.Clear();
            Clipboard.SetImage(bitmap);
            DataFormats.Format format = DataFormats.GetFormat(DataFormats.Bitmap);
            this.txtBoxDiscussion.ReadOnly = false; // Disable read only else it can not paste 
            this.txtBoxDiscussion.Select(this.txtBoxDiscussion.TextLength,1);
            this.txtBoxDiscussion.Paste(format);
            this.txtBoxDiscussion.ReadOnly = true;
            this.txtBoxDiscussion.AppendText("\n");
        }

        // Send message on send button click
        private void btnSend_Click(object sender, EventArgs e)
        {
            this.sendMessage();
        }

        // Send message on key entered press
        private void txtBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.sendMessage();
                e.Handled = e.SuppressKeyPress = true; // Allow to suppress the 'ding' sound
            }
        }

        // Send message in txtboxmessage to broadcast
        private void sendMessage()
        {
            if (!this.txtBoxMessage.Text.Equals(""))
            {
                Chat_Library.Model.Message message = new Chat_Library.Model.Message("Broadcast");
                message.addArgument("text", this.txtBoxMessage.Text);
                client.Connection.sendMessage(message);
                this.txtBoxDiscussion.AppendText("<Me> " + this.txtBoxMessage.Text + "\n");
                this.txtBoxMessage.Text = "";
            }
        }

        // Does not allow the txtboxdiscussion to get the focus
        private void txtBoxDiscussion_MouseDown(object sender, MouseEventArgs e)
        {
            this.txtBoxMessage.Focus();
        }

        // Open URL link in the default webbrowser
        private void txtBoxDiscussion_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
