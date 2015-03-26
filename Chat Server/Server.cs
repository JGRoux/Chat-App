using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chat_Library.Model;

namespace Chat_Server
{
    public class Server
    {
        private Socket serverSocket;
        private List<Channel> channelsList = new List<Channel>();

        public Server()
        {
            // Create the server socket with the TCP protocol.
            this.serverSocket = new Socket(AddressFamily.InterNetwork,
                             SocketType.Stream,
                             ProtocolType.Tcp);
            Console.WriteLine("Server listen on port 8000");
            //On lie la socket au point de communication
            this.serverSocket.Bind(new IPEndPoint(Dns.GetHostAddresses("127.0.0.1")[0], 8000));
        }

        // Infinite loop to wait for client connection
        public void start()
        {
            while (true)
            {
                Socket newClientSocket = listenAndAcceptSocket();
                new ThreadClient(new Client(newClientSocket), channelsList);
            }
        }

        // Keeps listening and accepts the connection of 1 client.
        public Socket listenAndAcceptSocket()
        {
            this.serverSocket.Listen(1);
            Console.WriteLine("Waiting for a new connection...");
            Socket newClientSocket = this.serverSocket.Accept();
            Console.WriteLine("New connection from " + newClientSocket.RemoteEndPoint.ToString());
            return newClientSocket;
        }
    }
}