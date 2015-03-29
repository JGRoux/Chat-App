using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Collections;
using Chat_Library.Controller;
using Chat_Library.Model;

namespace Chat_Server
{
    class ThreadClient
    {
        private Client client;
        private Connection connection;

        // The list of the client's channels.
        private List<Channel> channelsList;
        private System.Timers.Timer timerOut, timerCheck;

        public ThreadClient(Connection connection, List<Channel> channelsList)
        {
            this.connection = connection;
            this.channelsList = channelsList;
            Thread newThreadClient = new Thread(threadClientMethod);
            newThreadClient.IsBackground = true;
            newThreadClient.Start();

            this.timerOut = new System.Timers.Timer();
            this.timerOut.Elapsed += new ElapsedEventHandler(TimeOut);
            this.timerOut.Interval = 300000; // 5min
            this.timerOut.Enabled = true;

            this.timerCheck = new System.Timers.Timer();
            this.timerCheck.Elapsed += new ElapsedEventHandler(checkConnection);
            this.timerCheck.Interval = 2000; // 2sec
            this.timerCheck.Enabled = true;
        }

        // The method launched in the thread.
        private void threadClientMethod()
        {
            Message message;
            while (true)
            {
                try
                {
                    if ((message = this.connection.getMessage()) != null)
                    {
                        // The available commands.
                        if (message.cmd.Equals("Auth"))
                            this.authClient(message);
                        else if (message.cmd.Equals("ReqClients"))
                            this.reqClients();
                        else if (message.cmd.Equals("Broadcast"))
                            this.broadcastIncomingMessage(message);
                        else if (message.cmd.Equals("NewPrivateChat"))
                            this.newPrivateChat(message);


                        //Reset the timer
                        this.timerOut.Stop();
                        this.timerOut.Start();
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine("sd");
                    this.closeConnection();
                    return;
                }
            }
        }


        private void newPrivateChat(Message message)
        {
            Console.WriteLine("Create new private channel:" + this.client.Channel.Uri + ": " + this.client.Username + " & " + message.getArg("name"));
            Channel channel = new Channel(null, this.client.Channel.Uri + ": " + this.client.Username + " & " + message.getArg("name"));
            this.channelsList.Add(channel);
            Client tmpReceiver = this.client.Channel.getClient(message.getArg("name"));
            Client sender = new Client(channel);
            Client receiver = new Client(channel);
            sender.setCredentials(this.client.Username, this.client.Password);
            receiver.setCredentials(tmpReceiver.Username, tmpReceiver.Password);
            channel.addClient(sender);
            channel.addClient(receiver);

            Message msg = new Message("NewPrivateChat");
            msg.addArgument("name", this.client.Username);
            tmpReceiver.Connection.sendMessage(msg);
        }

        //
        private void checkConnection(object source, ElapsedEventArgs e)
        {
            if (!this.connection.isAvailable())
                this.closeConnection();
        }

        private void TimeOut(object source, ElapsedEventArgs e)
        {
            this.closeConnection();
        }

        // Close connection
        private void closeConnection()
        {
            this.client.isConnected = false;
            this.broadcastClientDisconnected();
            this.connection.closeSocket();
            this.timerCheck.Stop();
            this.timerOut.Stop();
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

        // Indicate client connected
        private void setConnectedClient(Client client, Channel channel)
        {
            Console.WriteLine("Client " + client.Username + " is now connected to channel " + channel.Uri);
            this.client = client;
            this.client.isConnected = true;
            this.client.Connection = this.connection;
            this.client.Connection.sendMessage(new Message("Connected"));
            this.sendWelcome(channel, this.client.Username);
        }

        // Welcome channel message
        private void sendWelcome(Channel channel, String username)
        {
            Message message = new Message("NewMessage");
            message.addArgument("text", "Welcome on channel " + channel.Uri);
            this.client.Connection.sendMessage(message);
            this.broadcastClientConnected();
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

        // Send clients name connected to channel
        private void reqClients()
        {
            Message message = new Message("ClientsList");
            foreach (Client client in this.client.Channel.getClientsList())
                if (client.isConnected && client != this.client)
                    message.addArgument("name", client.Username);
            this.client.Connection.sendMessage(message);
        }

        // Broadcast that the client is connected
        private void broadcastClientConnected()
        {
            Message message = new Message("NewClient");
            message.addArgument("name", this.client.Username);
            this.broadcastMessage(message);
        }

        // Broadcats that the client has disconnected
        private void broadcastClientDisconnected()
        {
            Message message = new Message("RemoveClient");
            message.addArgument("name", this.client.Username);
            this.broadcastMessage(message);
        }

        // Transform incoming message to broadcast it
        private void broadcastIncomingMessage(Message message)
        {
            message.cmd = "NewMessage";
            message.addArgument("name", this.client.Username);
            this.broadcastMessage(message);
        }

        // Send message to all clients connected to channel
        private void broadcastMessage(Message message)
        {
            foreach (Client client in this.client.Channel.getClientsList())
                if (client != this.client)
                    client.Connection.sendMessage(message);
        }
    }
}
