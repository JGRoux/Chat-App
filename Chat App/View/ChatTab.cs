using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat_Client.Model;

namespace Chat_Client
{
    public partial class ChatTab : UserControl
    {
        private Channel channel;

        public ChatTab(Channel channel)
        {
            InitializeComponent();
            this.channel = channel;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }
    }
}
