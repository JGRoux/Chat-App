namespace Chat_Client.View
{
    partial class ConnectionTab
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxChannels = new System.Windows.Forms.ComboBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.btnAddChannel = new System.Windows.Forms.Button();
            this.lblPwd = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtBoxPwd = new System.Windows.Forms.TextBox();
            this.txtBoxUsername = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxChannels
            // 
            this.comboBoxChannels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChannels.FormattingEnabled = true;
            this.comboBoxChannels.Location = new System.Drawing.Point(92, 78);
            this.comboBoxChannels.Name = "comboBoxChannels";
            this.comboBoxChannels.Size = new System.Drawing.Size(196, 21);
            this.comboBoxChannels.Sorted = true;
            this.comboBoxChannels.TabIndex = 0;
            this.comboBoxChannels.SelectedIndexChanged += new System.EventHandler(this.comboBoxChannels_SelectedIndexChanged);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.btnAddChannel);
            this.mainPanel.Controls.Add(this.lblPwd);
            this.mainPanel.Controls.Add(this.lblUsername);
            this.mainPanel.Controls.Add(this.txtBoxPwd);
            this.mainPanel.Controls.Add(this.txtBoxUsername);
            this.mainPanel.Controls.Add(this.btnConnect);
            this.mainPanel.Controls.Add(this.comboBoxChannels);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(353, 321);
            this.mainPanel.TabIndex = 1;
            // 
            // btnAddChannel
            // 
            this.btnAddChannel.BackgroundImage = global::Chat_Client.Properties.Resources.icon_add;
            this.btnAddChannel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddChannel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddChannel.Location = new System.Drawing.Point(58, 76);
            this.btnAddChannel.Name = "btnAddChannel";
            this.btnAddChannel.Size = new System.Drawing.Size(28, 23);
            this.btnAddChannel.TabIndex = 6;
            this.btnAddChannel.UseVisualStyleBackColor = true;
            this.btnAddChannel.Click += new System.EventHandler(this.btnAddChannel_Click);
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.Location = new System.Drawing.Point(57, 134);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(59, 13);
            this.lblPwd.TabIndex = 5;
            this.lblPwd.Text = "Password :";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(55, 105);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(61, 13);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Username :";
            // 
            // txtBoxPwd
            // 
            this.txtBoxPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxPwd.Location = new System.Drawing.Point(122, 131);
            this.txtBoxPwd.Name = "txtBoxPwd";
            this.txtBoxPwd.PasswordChar = '*';
            this.txtBoxPwd.Size = new System.Drawing.Size(166, 20);
            this.txtBoxPwd.TabIndex = 3;
            // 
            // txtBoxUsername
            // 
            this.txtBoxUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxUsername.Location = new System.Drawing.Point(122, 105);
            this.txtBoxUsername.Name = "txtBoxUsername";
            this.txtBoxUsername.Size = new System.Drawing.Size(166, 20);
            this.txtBoxUsername.TabIndex = 2;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConnect.Location = new System.Drawing.Point(58, 157);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(230, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // ConnectionTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "ConnectionTab";
            this.Size = new System.Drawing.Size(353, 321);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxChannels;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtBoxPwd;
        private System.Windows.Forms.TextBox txtBoxUsername;
        private System.Windows.Forms.Button btnAddChannel;
    }
}
