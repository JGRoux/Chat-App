namespace Chat_Client.View
{
    partial class NewChannelDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewChannelDialog));
            this.txtBoxChannelName = new System.Windows.Forms.TextBox();
            this.txtBoxChannelUri = new System.Windows.Forms.TextBox();
            this.lblChannelName = new System.Windows.Forms.Label();
            this.lblChannelUri = new System.Windows.Forms.Label();
            this.btnAddChannel = new System.Windows.Forms.Button();
            this.toolTipChannelUri = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBoxChannelUriHelp = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChannelUriHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBoxChannelName
            // 
            this.txtBoxChannelName.Location = new System.Drawing.Point(64, 23);
            this.txtBoxChannelName.Name = "txtBoxChannelName";
            this.txtBoxChannelName.Size = new System.Drawing.Size(208, 20);
            this.txtBoxChannelName.TabIndex = 0;
            // 
            // txtBoxChannelUri
            // 
            this.txtBoxChannelUri.Location = new System.Drawing.Point(64, 49);
            this.txtBoxChannelUri.Name = "txtBoxChannelUri";
            this.txtBoxChannelUri.Size = new System.Drawing.Size(208, 20);
            this.txtBoxChannelUri.TabIndex = 1;
            // 
            // lblChannelName
            // 
            this.lblChannelName.AutoSize = true;
            this.lblChannelName.Location = new System.Drawing.Point(12, 26);
            this.lblChannelName.Name = "lblChannelName";
            this.lblChannelName.Size = new System.Drawing.Size(35, 13);
            this.lblChannelName.TabIndex = 2;
            this.lblChannelName.Text = "Name";
            // 
            // lblChannelUri
            // 
            this.lblChannelUri.AutoSize = true;
            this.lblChannelUri.Location = new System.Drawing.Point(12, 52);
            this.lblChannelUri.Name = "lblChannelUri";
            this.lblChannelUri.Size = new System.Drawing.Size(26, 13);
            this.lblChannelUri.TabIndex = 3;
            this.lblChannelUri.Text = "URI";
            // 
            // btnAddChannel
            // 
            this.btnAddChannel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAddChannel.Location = new System.Drawing.Point(197, 75);
            this.btnAddChannel.Name = "btnAddChannel";
            this.btnAddChannel.Size = new System.Drawing.Size(75, 23);
            this.btnAddChannel.TabIndex = 4;
            this.btnAddChannel.Text = "Add";
            this.btnAddChannel.UseVisualStyleBackColor = true;
            // 
            // toolTipChannelUri
            // 
            this.toolTipChannelUri.IsBalloon = true;
            // 
            // pictureBoxChannelUriHelp
            // 
            this.pictureBoxChannelUriHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxChannelUriHelp.Image = global::Chat_Client.Properties.Resources.ic_info_outline_48px_48;
            this.pictureBoxChannelUriHelp.Location = new System.Drawing.Point(38, 49);
            this.pictureBoxChannelUriHelp.Name = "pictureBoxChannelUriHelp";
            this.pictureBoxChannelUriHelp.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxChannelUriHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxChannelUriHelp.TabIndex = 5;
            this.pictureBoxChannelUriHelp.TabStop = false;
            this.toolTipChannelUri.SetToolTip(this.pictureBoxChannelUriHelp, "Channel URI format is \"server/channel\"\r\n(Ex: www.myserver.com/mychannel)");
            // 
            // NewChannelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 110);
            this.Controls.Add(this.pictureBoxChannelUriHelp);
            this.Controls.Add(this.btnAddChannel);
            this.Controls.Add(this.lblChannelUri);
            this.Controls.Add(this.lblChannelName);
            this.Controls.Add(this.txtBoxChannelUri);
            this.Controls.Add(this.txtBoxChannelName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewChannelDialog";
            this.Text = "New Channel";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChannelUriHelp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxChannelName;
        private System.Windows.Forms.TextBox txtBoxChannelUri;
        private System.Windows.Forms.Label lblChannelName;
        private System.Windows.Forms.Label lblChannelUri;
        private System.Windows.Forms.Button btnAddChannel;
        private System.Windows.Forms.ToolTip toolTipChannelUri;
        private System.Windows.Forms.PictureBox pictureBoxChannelUriHelp;
    }
}