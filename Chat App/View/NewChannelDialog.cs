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
        }

        public TextBox getTxtBoxChannelName(){
            return this.txtBoxChannelName;
        }

        public TextBox getTxtBoxChannelUri()
        {
            return this.txtBoxChannelUri;
        }
    }
}
