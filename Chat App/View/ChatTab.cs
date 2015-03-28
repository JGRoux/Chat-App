﻿using System;
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

        private Client client;

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
            }

        }

        private void listboxContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.listboxContextMenu.Hide();
            if (e.ClickedItem.ToString().Equals("Start private chat"))
            {
                /*Channel channel = new Channel();
                channel.addClient(this.client);
                channel.addClient(this.client.Channel.getClient(listBoxUsers.SelectedItem.ToString()));*/
                CreatePrivateChat(this, e);
            }
        }

        private void getConnectedClients()
        {
            Chat_Library.Model.Message message = new Chat_Library.Model.Message("ReqClients");
            this.client.Connection.sendMessage(message);
        }

        private void getMessages()
        {
            Chat_Library.Model.Message message;
            while (true)
            {
                if ((message = this.client.Connection.getMessage()) != null)
                {
                    if (message.cmd.Equals("ClientsList"))
                        this.Invoke((setConnectedClient)setClientList, message);
                    else if (message.cmd.Equals("NewClient"))
                        this.Invoke((setConnectedClient)setClient, message);
                    else if (message.cmd.Equals("RemoveClient"))
                        this.Invoke((setConnectedClient)removeClient, message);
                    else if (message.cmd.Equals("NewMessage"))
                        if (message.getArg("text") != null)
                            this.Invoke((setNewText)setText, message);
                        else if (message.getArg("picture") != null)
                            this.Invoke((setNewPicture)setPicture, message);
                }
            }
        }

        private delegate void setConnectedClient(Chat_Library.Model.Message message);

        private void setClientList(Chat_Library.Model.Message message)
        {
            int i = 0;
            foreach (String name in message.getArgContents("name"))
                {
                    this.listBoxUsers.Items.Add(name);
                i++;
            }
            this.txtBoxDiscussion.Text += "There are currently " + i.ToString() + " users connected" + Environment.NewLine;
        }

        private void setClient(Chat_Library.Model.Message message)
        {
            this.listBoxUsers.Items.Add(message.getArg("name"));
            this.txtBoxDiscussion.Text += "Client " + message.getArg("name") + " is now connected" + Environment.NewLine;
                }

        private void removeClient(Chat_Library.Model.Message message)
        {
            this.listBoxUsers.Items.Remove(message.getArg("name"));
            this.txtBoxDiscussion.Text += "Client " + message.getArg("name") + " has disconnected" + Environment.NewLine;
        }

        private delegate void setNewText(Chat_Library.Model.Message message);

        private void setText(Chat_Library.Model.Message message)
        {
            if (message.getArg("name") != null)
                this.txtBoxDiscussion.Text += message.getArg("name") + ": " + message.getArg("text") + Environment.NewLine;
            else
                foreach (String text in message.getArgContents("text"))
                    this.txtBoxDiscussion.Text += text + Environment.NewLine;
        }

        private delegate void setNewPicture(Chat_Library.Model.Message message);

        private void setPicture(Chat_Library.Model.Message message)
        {
            /*if (message.getArg("name") != null)
                this.txtBoxDiscussion.Text += message.getArg("name") + ": " + message.getArg("picture") + Environment.NewLine;
            else
                foreach (String text in message.getArgContents("text"))
                    this.txtBoxDiscussion.Text += text + Environment.NewLine;*/

            Bitmap bitmap = Base64ImageConverter.stringToImage(message.getArg("picture"));
            Clipboard.SetDataObject(bitmap);
            DataFormats.Format format = DataFormats.GetFormat(DataFormats.Bitmap);
            //this.txtBoxDiscussion.Paste(format);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Chat_Library.Model.Message message = new Chat_Library.Model.Message("Broadcast");
            message.addArgument("text", this.txtBoxMessage.Text);
            client.Connection.sendMessage(message);
        }

        private void getConnectedClientTimer_Tick(object sender, EventArgs e)
        {
            this.getConnectedClients();
        }

        public void closeTab()
        {
            this.getConnectedClientTimer.Enabled = false;
        }
    }
}
