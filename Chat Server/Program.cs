using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.IO;

namespace Chat_Server
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Server server = new Server();

            server.Start();
        }
    }
}
