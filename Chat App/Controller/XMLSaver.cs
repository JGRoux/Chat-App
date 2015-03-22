using Chat_Client.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chat_Client.Controller
{
    // Class that allow to save/read IRC channels with infos to xml file

    public static class XMLSaver
    {
        private static String filename = "infos.xml";

        // Write IRC channels list to xml file
        public static void WriteXml(List<Channel> channels)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Channel>));
            using (StreamWriter sw = new StreamWriter(filename))
            {
                xs.Serialize(sw, channels);
            }
        }

        // Read xml file and return channels list
        public static List<Channel> ReadXml()
        {
            List<Channel> channels = null;
            XmlSerializer xs = new XmlSerializer(typeof(List<Channel>));
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    channels = xs.Deserialize(sr) as List<Channel>;
                }
            }
            catch
            {
                channels = new List<Channel>();
            }
            return channels;
        }
    }
}
