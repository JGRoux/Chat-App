using Chat_Client.View;
using Chat_Client.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat_Library.Model;
using Chat_Library.Controller;

namespace Chat_Client
{
    public partial class MainWindow : Form
    {
        private ChatClient chatClient;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.chatClient = new ChatClient();
            this.setConnectionTab();
        }

        // Add a tab when clicking on the last tab.
        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == this.tabControl.TabPages.Count - 1)
            {
                this.tabControl.TabPages.Insert(this.tabControl.TabPages.Count - 1, "New Tab     ");
                this.tabControl.SelectedIndex = e.TabPageIndex;
                this.setConnectionTab();
            }
        }

        // Method to draw the tab and add a close button and an add button on the last tab.
        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            RectangleF tabTextArea = RectangleF.Empty;
            if (e.Index != this.tabControl.TabCount - 1)
            {
                tabTextArea = (RectangleF)this.tabControl.GetTabRect(e.Index);
                using (Bitmap bmp = new Bitmap(Chat_Client.Properties.Resources.icon_close))
                {
                    if (e.Index == this.tabControl.SelectedIndex)
                    {
                        e.Graphics.DrawImage(bmp, tabTextArea.X + tabTextArea.Width - 16, 5, 13, 13);
                        //e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Bounds);
                    }
                    else
                        e.Graphics.DrawImage(bmp, tabTextArea.X + tabTextArea.Width - 16, 7, 13, 13);
                }
            }
            else
            {
                tabTextArea = (RectangleF)this.tabControl.GetTabRect(e.Index);
                using (Bitmap bmp = new Bitmap(Chat_Client.Properties.Resources.icon_add))
                {
                    e.Graphics.DrawImage(bmp, tabTextArea.X + tabTextArea.Width / 2 - 8, 5, 13, 13);
                }
            }
            // This code will render a "x" mark at the end of the Tab caption.
            Font BoldFont = new Font(tabControl.Font, FontStyle.Bold);
            e.Graphics.DrawString(this.tabControl.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 5, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        // Visual effect on close area of the tab when mouse is hover.
        private void tabControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isOnCloseArea(e))
            {
                //this.Refresh();
                //TODO
            }
        }

        // Remove a tab when clicking on close area.
        private void tabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.isOnCloseArea(e))
            {
                if (this.tabControl.TabPages[this.tabControl.SelectedIndex].Controls[0].GetType() == typeof(ChatTab))
                    ((ChatTab)this.tabControl.TabPages[this.tabControl.SelectedIndex].Controls[0]).closeConnection();
                this.tabControl.TabPages.RemoveAt(this.tabControl.SelectedIndex);
            }
        }

        private bool isOnCloseArea(MouseEventArgs e)
        {
            RectangleF tabTextArea = (RectangleF)this.tabControl.GetTabRect(this.tabControl.SelectedIndex);
            tabTextArea = new RectangleF(tabTextArea.X + tabTextArea.Width - 16, 5, 13, 13);
            Point pt = new Point(e.X, e.Y);
            if (tabTextArea.Contains(pt))
                return true;
            else
                return false;
        }

        // Method to set the connection user control into the selected tab.
        private void setConnectionTab()
        {
            ConnectionTab tab = new ConnectionTab(this.chatClient);
            tab.Dock = System.Windows.Forms.DockStyle.Fill;

            foreach (Client client in this.chatClient.clientsList)
                tab.getComboBox().Items.Add(client.Channel.Name);

            this.tabControl.TabPages[this.tabControl.SelectedIndex].Controls.Add(tab);
            tab.connect += createConnection;
        }

        // Launch new channel connection when clicking on button Connect.
        private void createConnection(object sender, EventArgs e)
        {
            ConnectionTab tab = (ConnectionTab)sender;
            if (tab.getComboBox().Text.Equals(""))
                MessageBox.Show("Please select a channel !");
            else if (tab.getTxtBoxUsername().Text.Equals(""))
                MessageBox.Show("Please enter a username !");
            else if (tab.getTxtBoxPwd().Text.Equals(""))
                MessageBox.Show("Please enter a password !");
            if (!tab.getComboBox().Text.Equals("") && !tab.getTxtBoxPwd().Text.Equals("") && !tab.getTxtBoxUsername().Text.Equals(""))
            {
                try
                {
                    Client client = this.chatClient.getClient(tab.getComboBox().Text);
                    client.setCredentials(tab.getTxtBoxUsername().Text, tab.getTxtBoxPwd().Text);
                    client.Connection = new Connection(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
                    String[] Uri = client.Channel.Uri.Split('/');
                    client.Connection.connect(Uri[0], 8000);
                    Chat_Library.Model.Message message = new Chat_Library.Model.Message("Auth");
                    message.addArgument("channel", Uri[1]);
                    message.addArgument("username", client.Username);
                    message.addArgument("password", client.Password);
                    client.Connection.sendMessage(message);
                    if (client.Connection.getMessage().cmd.Equals("Connected"))
                    {
                        this.chatClient.save(); // Save username and password
                        ChatTab chatTab = new ChatTab(client);
                        chatTab.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.tabControl.TabPages[this.tabControl.SelectedIndex].Text = tab.getComboBox().Text + "     ";
                        this.tabControl.TabPages[this.tabControl.SelectedIndex].Controls.Clear();
                        this.tabControl.TabPages[this.tabControl.SelectedIndex].Controls.Add(chatTab);

                        chatTab.CreatePrivateChat += chatTab_CreatePrivateChat;
                    }
                    else
                    {
                        MessageBox.Show("Wrong password !");
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Impossible to establish connection to server !");
                    Console.WriteLine(exception.ToString());
                }
            }
        }

        private void channelAlreadyOpen()
        {

        }

        private void chatTab_CreatePrivateChat(object sender, EventArgs e)
        {
            var chatTabCaller = sender as ChatTab;

            //try
            {
                Client oldClient = null;
                String channelURI;
                String[] Uri;

                if (chatTabCaller.clientSelected != null)
                {
                    foreach (Client client in this.chatClient.clientsList)
                        if (client.Username.Equals(chatTabCaller.clientSelected))
                            oldClient = client;
                    Uri = oldClient.Channel.Uri.Split('/');
                    channelURI = Uri[1] + ": " + chatTabCaller.client.Username + " & " + chatTabCaller.clientSelected;
                }
                else
                {
                    oldClient = chatTabCaller.client;
                    Uri = oldClient.Channel.Uri.Split('/');
                    channelURI = Uri[1] + ": " + chatTabCaller.clientCaller + " & " + chatTabCaller.client.Username;
                }
                Client newClient = new Client(oldClient.Channel);
                newClient.Channel.Uri = channelURI;
                newClient.Username = oldClient.Username;
                newClient.Password = oldClient.Password;
                newClient.Connection = new Connection(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));

                newClient.Connection.connect(Uri[0], 8000);

                Chat_Library.Model.Message message = new Chat_Library.Model.Message("Auth");
                message.addArgument("channel", newClient.Channel.Uri);
                message.addArgument("username", newClient.Username);
                message.addArgument("password", newClient.Password);
                newClient.Connection.sendMessage(message);
                if (newClient.Connection.getMessage().cmd.Equals("Connected"))
                {
                    ChatTab chatTab = new ChatTab(newClient);
                    chatTab.Dock = System.Windows.Forms.DockStyle.Fill;

                    this.tabControl.TabPages.Insert(this.tabControl.TabPages.Count - 1, newClient.Channel.Uri + "     ");
                    this.tabControl.SelectedIndex = this.tabControl.TabPages.Count - 2;
                    this.tabControl.TabPages[this.tabControl.SelectedIndex].Controls.Add(chatTab);

                    chatTab.CreatePrivateChat += chatTab_CreatePrivateChat;
                }
            }
            //catch (Exception exception)
            {
                //MessageBox.Show("Impossible to establish connection to server !");
                //Console.WriteLine(exception.ToString());
            }

        }

    }
}

