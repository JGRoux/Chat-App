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

namespace Chat_Client
{
    public partial class ChatTab : UserControl
    {
        private Client client;

        public ChatTab(Client client)
        {
            InitializeComponent();
            this.client = client;

            Console.WriteLine("URI: " + this.client.Channel.Uri);
            //clientSocket.Bind(new IPEndPoint(IPAddress.Parse(this.client.Channel.Uri), this.client.Channel.Port));
            //clientSocket.Connect(this.client.Channel.Uri, this.client.Channel.Port);

            this.client.Connection = new Connection(new TcpClient());

            this.client.Connection.connect(this.client.Channel.Uri, this.client.Channel.Port);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }
    }
}
