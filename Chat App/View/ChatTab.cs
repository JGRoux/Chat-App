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

namespace Chat_Client
{
    public partial class ChatTab : UserControl
    {
        private Client client;

        public ChatTab(Client client)
        {
            InitializeComponent();
            this.client = client;
            new Thread(this.getMessages).Start();
        }

        private void getConnectedClients()
        {
            Chat_Library.Model.Message message = new Chat_Library.Model.Message("ReqClients");
            this.client.Connection.sendMessage(message);
        }

        private void getMessages()
        {
            Chat_Library.Model.Message message;
            if ((message = this.client.Connection.getMessage()).cmd.Equals("ClientsList")
                || (message = this.client.Connection.getMessage()).cmd.Equals("NewClient"))
                this.Invoke((setConnectedClient)setClientList, message);
            else if ((message = this.client.Connection.getMessage()).cmd.Equals("NewMessage"))
                this.Invoke((setNewText)setText, message);
        }

        private delegate void setConnectedClient(Chat_Library.Model.Message message);

        private void setClientList(Chat_Library.Model.Message message)
        {
            foreach (String name in message.getArgContents("name"))
                if (!this.listBoxUsers.Items.Contains(name))
                {
                    this.listBoxUsers.Items.Add(name);
                    this.txtBoxDiscussion.Text = "Client " + name + " is connected";
                }
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
