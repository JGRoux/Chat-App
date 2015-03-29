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
            this.socket.Send(Encoding.UTF8.GetBytes(new StreamReader(stream).ReadToEnd()));
        }

        // Gets a message from the server or a client.
        public Message getMessage()
        {
            Message message = null;
            while (message == null)
            {
                if (this.socket.Available > 0)
                {
                    byte[] buffer = new Byte[this.socket.Available];
                    try
                    {
                        this.socket.Receive(buffer);
                        
                        
                        DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Message));
                        MemoryStream stream = new MemoryStream(buffer);
                        message = (Message)js.ReadObject(stream);
                        return message;


                    }
                    catch (System.Runtime.Serialization.SerializationException e)
                    {
                        Console.WriteLine("erreur:" + e.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("erreur:" + e.ToString());
                    }
                }


                Thread.Sleep(1);
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

        //Test if the socket is deconnected
        public bool isDeconnected()
        {
            
            //We make a Send call with 0 bytes
            //If the send is a success, the socket is connected
            byte[] buffer =new Byte[0];

            
            try {
                int result = this.socket.Send(buffer);

                if (result == 0)
                    return false;
            }
            catch( System.ObjectDisposedException e)
            {
                Console.WriteLine("ObjectDisposedException, trying the connection of the socket");
                return true;
            }
            catch(System.Net.Sockets.SocketException e)
            {

                Console.WriteLine("Socket Excetption, trying the connection of the socket");
                return true;
            }
            return true;
         
            //   return this.socket.Poll(10, SelectMode.SelectRead) && (this.socket.Available == 0) ;
	   
        }
    }
}
