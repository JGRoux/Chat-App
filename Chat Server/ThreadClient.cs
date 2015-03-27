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
        private Connection connection;
        private List<Channel> channelsList;

        public ThreadClient(Connection connection, List<Channel> channelsList)
        {
            this.connection = connection;
            this.channelsList = channelsList;
            Thread newThreadClient = new Thread(threadClientMethod);
            newThreadClient.Start();
        }

        private void threadClientMethod()
        {
            Message message;
            //this.connection.sendMessage(new Message("test"));
            while (this.connection.isAvailable())
            {
                if ((message = this.connection.getMessage()) != null)
                {
                    if (message.cmd.Equals("Auth"))
                        this.authClient(message);
                    else if (message.cmd.Equals("ReqClients"))
                        this.reqClients();
                }
                
            }
        }

        // Authentification du client
        private void authClient(Message message)
        {
            foreach (Channel channel in this.channelsList)
                if (channel.Uri.Equals(message.getArg("channel")))
                {
                    // Si la channel existe on test le login
                    this.checkCredentials(message, channel);
                    return;
                }

            if (this.client == null)
            {
                // Si la channel n'existe pas on la crée et on ajoute l'utilisateur sur la channel
                Console.WriteLine("Create new channel:" + message.getArg("channel"));
                Channel channel = new Channel(null, message.getArg("channel"));
                this.channelsList.Add(channel);
                this.addClientToChannel(message, channel);
            }
        }

        // Vérification des logins fournies par le client
        private void checkCredentials(Message message, Channel channel)
        {
            Client client;
            if ((client = channel.getClient(message.getArg("username"))) != null)
            {
                // Client already exist on channel so we check the password
                if (client.Password.Equals(message.getArg("password")))
                {
                    this.client = client;
                    this.client.isConnected = true;
                    this.client.Connection = this.connection;
                    this.client.Connection.sendMessage(new Message("Connected"));
                }
                else
                {
                    this.client.Connection.sendMessage(new Message("Refused"));
                }
            }
            else
            {
                // Client does not exits on channel so we add the client to the channel
                this.addClientToChannel(message, channel);
            }
        }

        private void addClientToChannel(Message message, Channel channel)
        {
            Console.WriteLine("Add new client " + message.getArg("username") + " to channel " + message.getArg("channel"));
            this.client = new Client(channel);
            this.client.setCredentials(message.getArg("username"), message.getArg("password"));
            this.client.isConnected = true;
            this.client.Connection = this.connection;
            channel.addClient(this.client);
            this.client.Connection.sendMessage(new Message("Connected"));
        }

        private void reqClients()
        {
            Message message = new Message("ClientsList");
            foreach (Client client in this.client.Channel.getClientsList())
                message.addArgument("name", client.Username);
            this.client.Connection.sendMessage(message);
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
