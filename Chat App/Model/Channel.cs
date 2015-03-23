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

        public Channel()
        {

        }

        public Channel(String name, String uri)
        {
            this.Uri = uri;
            this.Name = name;
        }

    }
}
