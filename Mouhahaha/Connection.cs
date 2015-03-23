using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

// The shared library that we use if a function is used by the server and also by the client. 
namespace SharedLibrary
{
    public class Connection
    {
        private Socket socket;

        // After creating the server or a client, binding is necessary. 
        public void bind(String serverHost, int serverPort)
        {
            this.socket.Bind(new IPEndPoint(IPAddress.Parse(serverHost), serverPort));
        }

        // Sends a message to the server or a client.
        private void sendMessage(String message)
        {
            int nbByteSent = this.socket.Send(Encoding.UTF8.GetBytes(message));
        }

        // Gets a message from the server or a client.
        private String getMessage()
        {
            Byte[] myBuffer = new byte[256];
            int nbByteReceived = this.socket.Receive(myBuffer);
            String message = Encoding.UTF8.GetString(myBuffer);

            EndPoint anEndPoint = new IPEndPoint(IPAddress.Any, 0);
            this.socket.ReceiveFrom(myBuffer, ref anEndPoint);

            return message;
        }

        // Disconnects a client.
        private void closeSocket()
        {
            this.socket.Close();
            this.socket.Disconnect(true);
        }
    }
}
