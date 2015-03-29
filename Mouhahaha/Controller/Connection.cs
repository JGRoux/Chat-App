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
using System.Threading;

// The shared library that we use if a function is used by the server and also by the client. 
namespace Chat_Library.Controller
{
    public class Connection
    {
        private static String delimiter = "<EOM>";
        public Socket socket { get; set; }

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
            this.socket.Send(Encoding.UTF8.GetBytes(new StreamReader(stream).ReadToEnd() + delimiter));
        }

        // Gets a message from the server or a client.
        public Message getMessage()
        {
            byte[] delimiterBytes = Encoding.UTF8.GetBytes(delimiter);
            byte[] buffer = new Byte[1];
            String msg = "";

            do
                {
                if (this.isAvailable())
                {
                    try
                    {
                        this.socket.Receive(buffer, 1, SocketFlags.None);
                        msg += Encoding.UTF8.GetString(buffer);
                    }
                    catch (SocketException e)
                    {
                        throw new SocketException();
                    }
                }
                else
                    throw new SocketException();
                Thread.Sleep(1);
            } while (!msg.Contains(delimiter));

            msg = msg.Replace(delimiter, "");
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Message));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(msg));
            Message message = (Message)js.ReadObject(stream);
            return message;
        }

        // Boolean to know if the Connection is available.
        public bool isAvailable()
        {
            try
        {
                return !(this.socket.Poll(1, SelectMode.SelectRead) && this.socket.Available == 0);
        }
            catch (SocketException)
            {
                    return false;
            }
            }

        // Disconnects a client.
        public void closeSocket()
        {
            this.socket.Close();
        }
    }
}
