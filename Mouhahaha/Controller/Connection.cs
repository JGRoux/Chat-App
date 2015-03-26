using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Chat_Library.Model;

// The shared library that we use if a function is used by the server and also by the client. 
namespace Chat_Library.Controller
{
    public class Connection
    {
        public Socket socket {get; set;}
        public TcpClient clientSocket {get; set;}

        public Connection(Socket socket)
        {
            this.socket = socket;
        }

        // Connects a client to a server.
        public void connect(String serverHost, int serverPort)
        {
            this.socket.Connect(new IPEndPoint(Dns.GetHostAddresses(serverHost)[0], serverPort));
        }

        // After creating the server or a client, binding is necessary. 
        public void bind(String serverHost, int serverPort)
        {
            this.socket.Bind(new IPEndPoint(IPAddress.Parse(serverHost), serverPort));
        }

        // Sends a message to the server or a client.
        public void sendMessage(Message message)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Message));
            MemoryStream stream = new MemoryStream();
            js.WriteObject(stream, message);
            stream.Position = 0;
            this.socket.Send(Encoding.UTF8.GetBytes(new StreamReader(stream).ReadToEnd()));
        }

        // Gets a message from the server or a client.
        public Message getMessage()
        {
            try
            {
                byte[] buffer = new Byte[this.socket.Available];
                this.socket.Receive(buffer);
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Message));
                MemoryStream stream = new MemoryStream(buffer);
                Message message = (Message)js.ReadObject(stream);
                return message;
            }
            catch (Exception e)
            {
                Console.WriteLine("erreur:"+e.ToString());
            }
            return null;
        }

        public bool isAvailable()
        {
            return this.socket.Connected;
        }

        // Disconnects a client.
        public void closeSocket()
        {
            this.socket.Close();
            this.socket.Disconnect(true);
        }
    }
}
