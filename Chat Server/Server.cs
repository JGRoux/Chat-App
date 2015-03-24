using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using Chat_Library.Model;

namespace Chat_Server
{
    public class Server
    {
        private Socket serverSocket;
        private ArrayList ChannelsList = new ArrayList();

        public Server()
        {
            // Create the server socket with the TCP protocol.
            this.serverSocket = new Socket(AddressFamily.InterNetwork,
                             SocketType.Stream,
                             ProtocolType.Tcp);
            IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostEntry.AddressList[2];
            Console.WriteLine("Server listen on IP " + ipAddress.ToString() + " port 8000");
            //On lie la socket au point de communication
            this.serverSocket.Bind(new IPEndPoint(ipAddress, 8000));
        }

        // Infinite loop to wait for client connection
        public void start()
        {
            while (true)
            {
                Socket newClientSocket = listenAndAcceptSocket();
                new ThreadClient(new Client(newClientSocket));
            }
        }

        // Keeps listening and accepts the connection of 1 client.
        public Socket listenAndAcceptSocket()
        {
            this.serverSocket.Listen(1);
            Console.WriteLine("Waiting for a new connection...");
            Socket newClientSocket = this.serverSocket.Accept();
            Console.WriteLine("New Client:" + newClientSocket.GetHashCode());
            return newClientSocket;
        }
    }
}