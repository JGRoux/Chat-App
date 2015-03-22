using Chat_Client.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.Model
{
    class ChatClient
    {
        public List<Channel> channelsList {get; set;}

        public ChatClient()
        {
            this.channelsList = XMLSaver.ReadXml();
        }

         // Add channel to the client
        public void addChannel(String name, String uri)
        {
            this.channelsList.Add(new Channel(name, uri));
        }

        // Delete channel from the client
        public void delAlbum(String name)
        {
            this.channelsList.Remove(this.getChannel(name));
        }

        // Get channel from the client
        public Channel getChannel(String name)
        {
            foreach (Channel channel in this.channelsList)
            {
                if (channel.Name.Equals(name))
                    return channel;
            }
            return null;
        }

        // Save client channel list to xml file
        public void save()
        {
            XMLSaver.WriteXml(this.channelsList);
        }
    }
}
