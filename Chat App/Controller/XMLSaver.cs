using Chat_Client.Model;
using Chat_Library.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chat_Client.Controller
{
    // Class that allow to save/read IRC channels with infos to xml file.
    public static class XMLSaver
    {
        private static String filename = "infos.xml";

        // Serialize
        // Writes IRC channels list to xml file.
        public static void WriteXml(List<Client> clients)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Client>));
            using (StreamWriter sw = new StreamWriter(filename))
            {
                xs.Serialize(sw, clients);
            }
        }
        // Deserialize.
        // Reads xml file and return channels list.
        public static List<Client> ReadXml()
        {
            List<Client> clients = null;
            XmlSerializer xs = new XmlSerializer(typeof(List<Client>));
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    clients = xs.Deserialize(sr) as List<Client>;
                }
            }
            catch
            {
                clients = new List<Client>();
            }
            return clients;
        }
    }
}
