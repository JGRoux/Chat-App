﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chat_Library.Model
{
    [Serializable]
    public class Channel
    {
        public String Uri { get; set; }
        public int Port { get; set; }
        public String Name { get; set; }
        [XmlIgnore]
        private List<Client> clientList = new List<Client>();

        public Channel()
        {
        }

        public Channel(String name, String uri)
        {
            this.Uri = uri;
            this.Name = name;
            this.Port = 8000;
        }

        public Client getClient(String name)
        {
            foreach (Client client in this.clientList)
            {
                if (client.Username.Equals(name))
                    return client;
            }
            return null;
        }
    }
}
