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
            while (this.connection.isAvailable())
            {
                if ((message = this.connection.getMessage()) != null)
                {
                    if (message.cmd.Equals("Auth"))
                        this.authClient(message);
                    else if (message.cmd.Equals("ReqClients"))
                        this.reqClients();
                    else if (message.cmd.Equals("Broadcast"))
                        this.broadcastMessage(message);
                }
            }
        }

        // Client authentification.
        private void authClient(Message message)
        {
            foreach (Channel channel in this.channelsList)
                if (channel.Uri.Equals(message.getArg("channel")))
                {
                    // If channel exists we test the password.
                    this.checkCredentials(message, channel);
                    return;
                }

            if (this.client == null)
            {
                // If the channel does not exist, we create it and we add the user to the channel.
                Console.WriteLine("Create new channel:" + message.getArg("channel"));
                Channel channel = new Channel(null, message.getArg("channel"));
                this.channelsList.Add(channel);
                this.addClientToChannel(message, channel);
            }
        }

        // Check logins given by the client.
        private void checkCredentials(Message message, Channel channel)
        {
            Client client;
            if ((client = channel.getClient(message.getArg("username"))) != null)
            {
                // Client already exist on channel so we check the password.
                if (client.Password.Equals(message.getArg("password")))
                {
                    this.client = client;
                    this.client.isConnected = true;
                    this.client.Connection = this.connection;
                    this.sendWelcome(channel, message.getArg("username"));
                    this.client.Connection.sendMessage(new Message("Connected"));
                }
                else
                {
                    this.client.Connection.sendMessage(new Message("Refused"));
                }
            }
            else
            {
                // Client does not exits on channel so we add the client to the channel.
                this.addClientToChannel(message, channel);
            }
        }

        private void sendWelcome(Channel channel, String username)
        {
            int i = 0;
            foreach (Client client in channel.getClientsList())
                if (client.isConnected)
                    i++;
            Message message = new Message("NewMessage");
            message.addArgument("text", "Welcome on channel " + channel.Name);
            message.addArgument("text", "There are currently " + i.ToString() +" users connected");
            this.client.Connection.sendMessage(message);
            message = new Message("NewConnected");
            message.addArgument("name", username);
            this.broadcastMessage(message);
        }

        // Add a new client in the channel.
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

        // Send all clients name connected to channel.
        private void reqClients()
        {
            Message message = new Message("ClientsList");
            foreach (Client client in this.client.Channel.getClientsList())
                if (client.isConnected && client != this.client)
                {
                    message.addArgument("name", client.Username);
                    Console.WriteLine("Req:" + client.Username);
                }
            this.client.Connection.sendMessage(message);
        }

        // Send message to all clients connected to channel.
        private void broadcastMessage(Message message)
        {
            message.cmd = "NewMessage";
            message.addArgument("name", this.client.Username);
            foreach (Client client in this.client.Channel.getClientsList())
                if (client != this.client)
                    client.Connection.sendMessage(message);
        }
    }
}
