using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Chat_Library.Model
{
    [DataContract]
    [Serializable]
    public class Message
    {
        [DataMember(Name = "cmd")]
        public String cmd { get; set; }

        [DataMember]
        public List<Argument> args { get; set; }

        public Message()
        {
        }

        public Message(String cmd)
        {
            this.cmd = cmd;
        }

        // A message has several arguments: an argument is known by a name and a content.
        public void addArgument(String name, String content)
        {
            if (this.args == null)
                this.args = new List<Argument>();
            this.args.Add(new Argument(name, content));
        }

        // To get the first content of the argument called by name.
        public String getArg(String name)
        {
            foreach (Argument arg in this.args)
            {
                if (arg.name.Equals(name))
                    return arg.content;
            }
            return null;
        }

        // To get the list of contents of the argument called by name.
        public List<String> getArgContents(String name)
        {
            List<String> contentsList = new List<String>();
            if (this.args != null)
                foreach (Argument arg in this.args)
                {
                    if (arg.name.Equals(name))
                        contentsList.Add(arg.content);
                }
            return contentsList;
        }

        public class Argument
        {
            public String name { get; set; }
            public String content { get; set; }

            public Argument()
            {

            }

            public Argument(String name, String content)
            {
                this.name = name;
                this.content = content;
            }
        }
    }
}
