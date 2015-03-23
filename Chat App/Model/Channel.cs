using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.Model
{
    [Serializable]
    public class Channel
    {
        public String Uri { get; set; }
        public String Name { get; set; }
        public String Username { get; set; }
        public String HashedPwd { get; set; }

        public Channel()
        {

        }

        public Channel(String name, String uri)
        {
            this.Uri = uri;
            this.Name = name;
        }

        public void setCredentials(String username, String pwd){
            this.Username = Username;
            this.HashedPwd = this.encrypt(pwd);
        }

        private String encrypt(String pwd)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(pwd);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }

    }
}
