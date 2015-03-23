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
    public class clientServerShared
    {
        /*
        // After creating the server or a client, binding is necessary. 
        public void bind(Socket socket, String serverHost, int serverPort)
        {
            socket.Bind(new IPEndPoint(IPAddress.Parse(serverHost), serverPort));
        }
         * */
    }
}
