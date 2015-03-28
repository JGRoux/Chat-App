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

        // Authentification du client
        private void authClient(Message message)
        {
            foreach (Channel channel in this.channelsList)
                if (channel.Uri.Equals(message.getArg("channel")))
                {
                    // If channel exist we test the password
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

        // Check logins given by the client
        private void checkCredentials(Message message, Channel channel)
        {
            Client client;
            if ((client = channel.getClient(message.getArg("username"))) != null)
            {
                // Client already exist on channel so we check the password
                if (client.Password.Equals(message.getArg("password")))
                {
                    this.setConnectedClient(client, channel);
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

        private void setConnectedClient(Client client, Channel channel)
        {
            Console.WriteLine("Client " + client.Username + " is now connected to channel "+ channel.Name);
            this.client = client;
            this.client.isConnected = true;
            this.client.Connection = this.connection;
            this.client.Connection.sendMessage(new Message("Connected"));
            this.sendWelcome(channel, this.client.Username);
        }

        private void sendWelcome(Channel channel, String username)
        {
            Message message = new Message("NewMessage");
            message.addArgument("text", "Welcome on channel " + channel.Uri);
            this.client.Connection.sendMessage(message);
            this.broadcastClientConnected(username);
        }

        // Add a new client in the channel
        private void addClientToChannel(Message message, Channel channel)
        {
            Console.WriteLine("Add new client " + message.getArg("username") + " to channel " + message.getArg("channel"));
            Client client = new Client(channel);
            client.setCredentials(message.getArg("username"), message.getArg("password"));
            channel.addClient(client);
            this.setConnectedClient(client, channel);
        }

        private void broadcastClientConnected(String username)
        {
            Message message = new Message("NewConnected");
            message.addArgument("name", username);
            this.broadcastMessage(message);
        }

        // Send all clients name connected to channel
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

        // Send message to all clients connected to channel
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
