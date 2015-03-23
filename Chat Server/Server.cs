using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace Chat_Server
{
    public class Server
    {
        private Socket serverSocket;
        private ArrayList socketList = new ArrayList();

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
            IPHostEntry ipHostEntry = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostEntry.AddressList[0];
            Console.WriteLine("IP=" + ipAddress.ToString());
            //On lie la socket au point de communication
            serverSocket.Bind(new IPEndPoint(ipAddress, 8000));

            this.serverSocket.Listen(10);
            Console.WriteLine("Waiting for a new connection...");
            Socket newClientSocket = this.serverSocket.Accept();

            Console.WriteLine("New Client:" + newClientSocket.GetHashCode());
            return newClientSocket;
        }

        public void Start()
        {
            while (true)
            {
                Socket newClientSocket = listenAndAcceptSocket();
             
                socketList.Add(newClientSocket);
                
                new ThreadClient(newClientSocket);
            }
        }
    }
}
