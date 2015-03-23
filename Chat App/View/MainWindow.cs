using Chat_App.View;
using Chat_Client.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_Client
{
    public partial class MainWindow : Form
    {
        private ChatClient chatClient;
        private Socket clientSocket;
        private String serverHost;
        private int serverPort;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.chatClient = new ChatClient();
            this.setConnectionTab();
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == this.tabControl.TabPages.Count - 1)
            {
                this.tabControl.TabPages.Insert(this.tabControl.TabPages.Count - 1, "New Tab     ");
                this.tabControl.SelectedIndex = e.TabPageIndex;
                this.setConnectionTab();
            }
        }

        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            RectangleF tabTextArea = RectangleF.Empty;

            if (e.Index != this.tabControl.TabCount - 1)
            {
                tabTextArea = (RectangleF)this.tabControl.GetTabRect(e.Index);
                using (Bitmap bmp = new Bitmap(Chat_Client.Properties.Resources.icon_close))
                {
                    if (e.Index == this.tabControl.SelectedIndex)
                    {
                        e.Graphics.DrawImage(bmp, tabTextArea.X + tabTextArea.Width - 16, 5, 13, 13);
                        //e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Bounds);
                    }
                    else
                        e.Graphics.DrawImage(bmp, tabTextArea.X + tabTextArea.Width - 16, 7, 13, 13);
                }
            }
            else
            {
                tabTextArea = (RectangleF)this.tabControl.GetTabRect(e.Index);
                using (Bitmap bmp = new Bitmap(Chat_Client.Properties.Resources.icon_add))
                {
                    e.Graphics.DrawImage(bmp, tabTextArea.X + tabTextArea.Width / 2 - 8, 5, 13, 13);
                }
            }
            //This code will render a "x" mark at the end of the Tab caption.
            Font BoldFont = new Font(tabControl.Font, FontStyle.Bold);
            e.Graphics.DrawString(this.tabControl.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 5, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        private void tabControl_MouseDown(object sender, MouseEventArgs e)
        {

            RectangleF tabTextArea = (RectangleF)this.tabControl.GetTabRect(this.tabControl.SelectedIndex);
            tabTextArea = new RectangleF(tabTextArea.X + tabTextArea.Width - 16, 5, 13, 13);
            Point pt = new Point(e.X, e.Y);
            if (tabTextArea.Contains(pt))
            {
                this.tabControl.TabPages.RemoveAt(this.tabControl.SelectedIndex);
            }
        }

        private void setConnectionTab()
        {
            ConnectionTab tab = new ConnectionTab();
            tab.Dock = System.Windows.Forms.DockStyle.Fill;

            foreach (Channel channel in this.chatClient.channelsList)
                tab.getComboBox().Items.Add(channel.Name);

            this.tabControl.TabPages[this.tabControl.SelectedIndex].Controls.Add(tab);
        }

        private void setMessage(){
            
        }

        

        

        
    }
}
