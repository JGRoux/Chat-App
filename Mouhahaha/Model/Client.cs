using Chat_Library.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chat_Library.Model
{
    [Serializable]
    public class Client
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public Channel Channel { get; set; }
        [XmlIgnore]
        public Boolean isConnected { get; set; }
        [XmlIgnore]
        public Connection Connection { get; set; }

        public Client()
        {
        }

        public Client(Channel channel)
        {
            this.Channel = channel;
        }

        public void setCredentials(String username, String pwd)
        {
            this.Username = username;
            this.Password = pwd;
        }

        private String encrypt(String pwd)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(pwd);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }
    }
}
