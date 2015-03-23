using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat_Server
{
    public class Server
    {
        private Socket serverSocket;

        public Server()
        {
            // Create the server socket with the TCP protocol.
            this.serverSocket = new Socket(AddressFamily.InterNetwork,
                             SocketType.Stream,
                             ProtocolType.Tcp); 
        }

        // Keeps listening and accepts the connection of 1 client.
        public Socket listenAndAcceptSocket()
        {
            this.serverSocket.Listen(1);
            Socket newClientSocket = this.serverSocket.Accept();

            return newClientSocket;
        }
    }
}
