using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_App.View
{
    public partial class ConnectionTab : UserControl
    {
        public ConnectionTab()
        {
            InitializeComponent();
        }

        public ComboBox getComboBox()
        {
            return this.comboBox1;
        }
    }
}
