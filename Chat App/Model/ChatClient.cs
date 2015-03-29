using Chat_Client.Controller;
using Chat_Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.Model
{
    public class ChatClient
    {
        public List<Client> clientsList { get; set; }

        public ChatClient()
        {
            this.clientsList = XMLSaver.ReadXml();
        }

        // Add channel to the client.
        public void addClient(String name, String uri)
        {
            this.clientsList.Add(new Client(new Channel(name, uri)));
        }

        // Delete client infos.
        public void delClient(String name)
        {
            this.clientsList.Remove(this.getClient(name));
        }

        // Get client from channel name.
        public Client getClient(String name)
        {
            foreach (Client client in this.clientsList)
            {
                if (client.Channel.Name.Equals(name))
                    return client;
            }
            return null;
        }

        // Save client channel list to xml file.
        public void save()
        {
            XMLSaver.WriteXml(this.clientsList);
        }
    }
}
