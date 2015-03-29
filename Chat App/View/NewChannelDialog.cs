using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_Client.View
{
    public partial class NewChannelDialog : Form
    {
        public NewChannelDialog()
        {
            InitializeComponent();
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.txtBoxChannelUri, "Type the server address and the channel name separated with a '/', e.g., 127.0.0.1/myChannel");
            toolTip1.AutomaticDelay = 200;
            toolTip1.AutoPopDelay = 10000;
        }

        public TextBox getTxtBoxChannelName()
        {
            return this.txtBoxChannelName;
        }

        public TextBox getTxtBoxChannelUri()
        {
            return this.txtBoxChannelUri;
        }
    }
}
