using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat_Server
{
    class ThreadClient
    {
        private Socket clientSocket;

        public ThreadClient(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            Thread newThreadClient = new Thread(threadClientMethod);
        }

        private static void threadClientMethod()
        {

        }
    }
}
