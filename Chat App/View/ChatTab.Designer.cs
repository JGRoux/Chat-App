namespace Chat_Client
{
    partial class ChatTab
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.txtBoxDiscussion = new System.Windows.Forms.RichTextBox();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.pictureButton = new System.Windows.Forms.Button();
            this.txtBoxMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.txtBoxDiscussion);
            this.splitContainer.Panel1MinSize = 200;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.listBoxUsers);
            this.splitContainer.Panel2MinSize = 100;
            this.splitContainer.Size = new System.Drawing.Size(441, 338);
            this.splitContainer.SplitterDistance = 250;
            this.splitContainer.TabIndex = 1;
            // 
            // txtBoxDiscussion
            // 
            this.txtBoxDiscussion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxDiscussion.Location = new System.Drawing.Point(0, 0);
            this.txtBoxDiscussion.Name = "txtBoxDiscussion";
            this.txtBoxDiscussion.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtBoxDiscussion.Size = new System.Drawing.Size(250, 338);
            this.txtBoxDiscussion.TabIndex = 0;
            this.txtBoxDiscussion.Text = "";
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.Location = new System.Drawing.Point(0, 0);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(187, 338);
            this.listBoxUsers.Sorted = true;
            this.listBoxUsers.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.Transparent;
            this.panelBottom.Controls.Add(this.pictureButton);
            this.panelBottom.Controls.Add(this.txtBoxMessage);
            this.panelBottom.Controls.Add(this.btnSend);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 338);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panelBottom.Size = new System.Drawing.Size(441, 35);
            this.panelBottom.TabIndex = 0;
            // 
            // pictureButton
            // 
            this.pictureButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureButton.BackgroundImage = global::Chat_Client.Properties.Resources.picture;
            this.pictureButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureButton.Location = new System.Drawing.Point(333, 6);
            this.pictureButton.Name = "pictureButton";
            this.pictureButton.Size = new System.Drawing.Size(30, 23);
            this.pictureButton.TabIndex = 2;
            this.pictureButton.UseVisualStyleBackColor = true;
            this.pictureButton.Click += new System.EventHandler(this.pictureButton_Click);
            // 
            // txtBoxMessage
            // 
            this.txtBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxMessage.Location = new System.Drawing.Point(0, 8);
            this.txtBoxMessage.Margin = new System.Windows.Forms.Padding(0);
            this.txtBoxMessage.Name = "txtBoxMessage";
            this.txtBoxMessage.Size = new System.Drawing.Size(330, 20);
            this.txtBoxMessage.TabIndex = 1;
            this.txtBoxMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxMessage_KeyDown);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Location = new System.Drawing.Point(366, 6);
            this.btnSend.Margin = new System.Windows.Forms.Padding(0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ChatTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelBottom);
            this.Name = "ChatTab";
            this.Size = new System.Drawing.Size(441, 373);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.ListBox listBoxUsers;
        public System.Windows.Forms.TextBox txtBoxMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox txtBoxDiscussion;
        private System.Windows.Forms.Button pictureButton;

    }
}
