using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using Chat_Library.Controller;
using Chat_Library.Model;

namespace Chat_Server
{
    class ThreadClient
    {
        private Client client;
        private List<Channel> channelsList;

        public ThreadClient(Client client, List<Channel> channelsList)
        {
            this.client = client;
            this.channelsList = channelsList;
            Thread newThreadClient = new Thread(threadClientMethod);
            newThreadClient.Start();
        }

        private void threadClientMethod()
        {
            Message message;
            while (this.client.Connection.isAvailable())
            {
                if ((message = this.client.Connection.getMessage()) != null)
                {
                    if (message.cmd.Equals("Auth"))
                        this.authClient(message);
                }
            }
        }

        private void authClient(Message message)
        {
            foreach (Channel channel in this.channelsList)
                if (channel.Uri.Split('/')[1].Equals(message.getArg("channel")))
                {
                    this.checkCredentials(message, channel);
                }
                else { }

        }

        private void checkCredentials(Message message, Channel channel)
        {
            Client client;
            if ((client = channel.getClient(message.getArg("username"))) != null)
            {
                if (client.Password.Equals(message.getArg("password")))
                {
                    client.isConnected = true;
                    client.Connection = this.client.Connection;
                    this.client = client;
                }
            }
            else
            {
            }
        }

        /*private void FwdMsg()
        {
            
            for (int i = 0; i < socketList.Count; i++)
            {
                if (((Socket)socketList[i]).Connected)
                {
                    try
                    {
                        int bytesSent = ((Socket)socketList[i]).Send(msg, msg.Length, SocketFlags.None);
                    }

                    catch
                    {
                        Console.Write(((Socket)socketList[i]).GetHashCode() + " déconnecté");
                    }
                }
                else
                {
                    socketList.Remove((Socket)socketList[i]);
                    i--;
                }
            }
        }*/


    }
}
