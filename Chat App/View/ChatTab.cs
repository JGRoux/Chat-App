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

namespace Chat_Client
{
    public partial class ChatTab : UserControl
    {
        private Client client;

        public ChatTab(Client client)
        {
            InitializeComponent();
            this.client = client;
            Chat_Library.Model.Message message = new Chat_Library.Model.Message("ReqClients");
            client.Connection.sendMessage(message);
            if ((message = client.Connection.getMessage()).cmd.Equals("ClientsList"))
            {
                foreach (String name in message.getArgContents("name"))
                {
                    this.listBoxUsers.Items.Add(name);
                }
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }
    }
}
