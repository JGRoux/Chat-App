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
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }
    }
}
