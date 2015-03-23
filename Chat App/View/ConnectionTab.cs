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

namespace Chat_Client.View
{
    public partial class ConnectionTab : UserControl
    {
        private ChatClient chatClient;
        public event EventHandler connect;

        public ConnectionTab(ChatClient chatClient)
        {
            InitializeComponent();
            this.chatClient = chatClient;
        }

        public ComboBox getComboBox()
        {
            return this.comboBoxChannels;
        }

        public TextBox getTxtBoxUsername()
        {
            return this.txtBoxUsername;
        }

        public TextBox getTxtBoxPwd()
        {
            return this.txtBoxPwd;
        }

        // Set action when user click on the Connect button
        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.connect(this, e);
        }

        // Display a new form to add a channel when clicking on the add button
        private void btnAddChannel_Click(object sender, EventArgs e)
        {
            NewChannelDialog dialog = new NewChannelDialog();
            if (ChannelCheck(dialog))
            {
                int index = this.getComboBox().Items.Add(dialog.getTxtBoxChannelName().Text);
                this.getComboBox().SelectedIndex = index;
                this.chatClient.addChannel(dialog.getTxtBoxChannelName().Text, dialog.getTxtBoxChannelUri().Text);
                this.chatClient.save();
            }
            dialog.Dispose();
        }

        // Check the validity of the channels parameters entered
        private Boolean ChannelCheck(NewChannelDialog dialog)
        {
            // Display dialog while a correct name is entered or user quit
            do
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    Channel channel;
                    // Channel name or Uri cannot be emtpy and 2 channel can't have the same name nor Uri
                    if (dialog.getTxtBoxChannelName().Text.Equals(""))
                        MessageBox.Show("Please enter a channel name !");
                    else if (this.chatClient.getChannel(dialog.getTxtBoxChannelName().Text) != null)
                        MessageBox.Show("Another channel has already this name !");
                    else if (dialog.getTxtBoxChannelUri().Text.Equals(""))
                        MessageBox.Show("Please enter an Uri !");
                    else if ((channel = this.chatClient.getChannel(dialog.getTxtBoxChannelName().Text)) != null)
                        MessageBox.Show("The channel " + channel.Name + " has already this Uri !");
                    else
                        return true;
                }
                else
                    return false;
            } while (this.chatClient.getChannel(dialog.getTxtBoxChannelName().Text) != null || dialog.getTxtBoxChannelName().Text.Equals("") || dialog.getTxtBoxChannelUri().Text.Equals("") || this.chatClient.getChannel(dialog.getTxtBoxChannelName().Text) != null);
            return false;
        }

        // Autoset username and password when user select channel in combobox
        private void comboBoxChannels_SelectedIndexChanged(object sender, EventArgs e)
        {
            Channel channel = this.chatClient.getChannel(this.comboBoxChannels.Text);
            if (channel.Username != null && channel.HashedPwd != null)
            {
                this.txtBoxUsername.Text = channel.Username;
                this.txtBoxPwd.Text = channel.HashedPwd;
            }
        }
    }
}
