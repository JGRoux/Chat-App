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

namespace Chat_App
{
    public partial class MainWindow : Form
    {
        private Socket clientSocket;
        private String serverHost;
        private int serverPort;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Establishes the connection with the server.
        private void connectToServer()
        {
            this.clientSocket.Connect(this.serverHost, this.serverPort);
        }

        // Sends a message to the server.
        private void sendMessage(String message)
        {
            int nbByteSent = this.clientSocket.Send(Encoding.UTF8.GetBytes(message));
        }

        // Gets a message from the server.
        private String getMessage()
        {
            Byte[] myBuffer = new byte[256];
            int nbByteReceived = this.clientSocket.Receive(myBuffer);
            String message = Encoding.UTF8.GetString(myBuffer);

            EndPoint anEndPoint = new IPEndPoint(IPAddress.Any, 0);
            this.clientSocket.ReceiveFrom(myBuffer, ref anEndPoint);

            return message;
        }

        // Disconnects the client.
        private void closeSocket()
        {
            this.clientSocket.Close();
            this.clientSocket.Disconnect(true); 
        }
    }
}
