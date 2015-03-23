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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewChannelDialog));
            this.txtBoxChannelName = new System.Windows.Forms.TextBox();
            this.txtBoxChannelUri = new System.Windows.Forms.TextBox();
            this.lblChannelName = new System.Windows.Forms.Label();
            this.lblChannelUri = new System.Windows.Forms.Label();
            this.btnAddChannel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBoxChannelName
            // 
            this.txtBoxChannelName.Location = new System.Drawing.Point(95, 23);
            this.txtBoxChannelName.Name = "txtBoxChannelName";
            this.txtBoxChannelName.Size = new System.Drawing.Size(177, 20);
            this.txtBoxChannelName.TabIndex = 0;
            // 
            // txtBoxChannelUri
            // 
            this.txtBoxChannelUri.Location = new System.Drawing.Point(95, 49);
            this.txtBoxChannelUri.Name = "txtBoxChannelUri";
            this.txtBoxChannelUri.Size = new System.Drawing.Size(177, 20);
            this.txtBoxChannelUri.TabIndex = 1;
            // 
            // lblChannelName
            // 
            this.lblChannelName.AutoSize = true;
            this.lblChannelName.Location = new System.Drawing.Point(12, 26);
            this.lblChannelName.Name = "lblChannelName";
            this.lblChannelName.Size = new System.Drawing.Size(77, 13);
            this.lblChannelName.TabIndex = 2;
            this.lblChannelName.Text = "Channel Name";
            // 
            // lblChannelUri
            // 
            this.lblChannelUri.AutoSize = true;
            this.lblChannelUri.Location = new System.Drawing.Point(12, 52);
            this.lblChannelUri.Name = "lblChannelUri";
            this.lblChannelUri.Size = new System.Drawing.Size(62, 13);
            this.lblChannelUri.TabIndex = 3;
            this.lblChannelUri.Text = "Channel Uri";
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
            // NewChannelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 110);
            this.Controls.Add(this.btnAddChannel);
            this.Controls.Add(this.lblChannelUri);
            this.Controls.Add(this.lblChannelName);
            this.Controls.Add(this.txtBoxChannelUri);
            this.Controls.Add(this.txtBoxChannelName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewChannelDialog";
            this.Text = "New Channel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxChannelName;
        private System.Windows.Forms.TextBox txtBoxChannelUri;
        private System.Windows.Forms.Label lblChannelName;
        private System.Windows.Forms.Label lblChannelUri;
        private System.Windows.Forms.Button btnAddChannel;
    }
}