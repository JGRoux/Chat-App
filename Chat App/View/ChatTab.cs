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

            this.client.Connection = new Connection(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
            this.client.Connection.connect(this.client.Channel.Uri, 8000);
            Chat_Library.Model.Message message = new Chat_Library.Model.Message("Auth");
            message.addArgument("channel", "channel");
            message.addArgument("username", "toto");
            message.addArgument("password", "pwd");
            this.client.Connection.sendMessage(message);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }
    }
}
