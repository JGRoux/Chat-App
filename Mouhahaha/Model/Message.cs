using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Chat_Library.Model
{
    [DataContract]
    public class Message
    {
        [DataMember(Name = "text")]
        public String text { get; set; }

        public Message()
        {
        }

        public Message(String text)
        {
            this.text = text;
        }
    }
}
