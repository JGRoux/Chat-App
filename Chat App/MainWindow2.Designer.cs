namespace Chat_App
{
    partial class MainWindow2
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Chat = new System.Windows.Forms.TabPage();
            this.msgArea = new System.Windows.Forms.RichTextBox();
            this.chatBody = new System.Windows.Forms.RichTextBox();
            this.sendMsg = new System.Windows.Forms.Button();
            this.Connexion = new System.Windows.Forms.TabPage();
            this.Nick = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ServerHost = new System.Windows.Forms.TextBox();
            this.Connect = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.Chat.SuspendLayout();
            this.Connexion.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Chat);
            this.tabControl1.Controls.Add(this.Connexion);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(536, 336);
            this.tabControl1.TabIndex = 8;
            // 
            // Chat
            // 
            this.Chat.Controls.Add(this.msgArea);
            this.Chat.Controls.Add(this.chatBody);
            this.Chat.Controls.Add(this.sendMsg);
            this.Chat.Location = new System.Drawing.Point(4, 22);
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(528, 310);
            this.Chat.TabIndex = 0;
            this.Chat.Text = "Chat";
            // 
            // msgArea
            // 
            this.msgArea.Location = new System.Drawing.Point(0, 200);
            this.msgArea.MaxLength = 2048;
            this.msgArea.Name = "msgArea";
            this.msgArea.Size = new System.Drawing.Size(512, 64);
            this.msgArea.TabIndex = 9;
            this.msgArea.Text = "";
            // 
            // chatBody
            // 
            this.chatBody.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatBody.Location = new System.Drawing.Point(0, 8);
            this.chatBody.Name = "chatBody";
            this.chatBody.ReadOnly = true;
            this.chatBody.Size = new System.Drawing.Size(512, 192);
            this.chatBody.TabIndex = 8;
            this.chatBody.Text = "";
            // 
            // sendMsg
            // 
            this.sendMsg.BackColor = System.Drawing.Color.IndianRed;
            this.sendMsg.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.sendMsg.Location = new System.Drawing.Point(208, 272);
            this.sendMsg.Name = "sendMsg";
            this.sendMsg.Size = new System.Drawing.Size(75, 23);
            this.sendMsg.TabIndex = 5;
            this.sendMsg.Text = "Envoyer";
            this.sendMsg.UseVisualStyleBackColor = false;
            this.sendMsg.Click += new System.EventHandler(this.SendMessage);
            // 
            // Connexion
            // 
            this.Connexion.Controls.Add(this.Nick);
            this.Connexion.Controls.Add(this.label2);
            this.Connexion.Controls.Add(this.label1);
            this.Connexion.Controls.Add(this.ServerHost);
            this.Connexion.Controls.Add(this.Connect);
            this.Connexion.Location = new System.Drawing.Point(4, 22);
            this.Connexion.Name = "Connexion";
            this.Connexion.Size = new System.Drawing.Size(528, 310);
            this.Connexion.TabIndex = 1;
            this.Connexion.Text = "Connexion";
            // 
            // Nick
            // 
            this.Nick.BackColor = System.Drawing.Color.LightGray;
            this.Nick.Location = new System.Drawing.Point(160, 48);
            this.Nick.Name = "Nick";
            this.Nick.Size = new System.Drawing.Size(128, 20);
            this.Nick.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Votre pseudo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Entrez le nom du serveur";
            // 
            // ServerHost
            // 
            this.ServerHost.BackColor = System.Drawing.Color.LightGray;
            this.ServerHost.Location = new System.Drawing.Point(160, 16);
            this.ServerHost.Name = "ServerHost";
            this.ServerHost.Size = new System.Drawing.Size(128, 20);
            this.ServerHost.TabIndex = 7;
            // 
            // Connect
            // 
            this.Connect.BackColor = System.Drawing.Color.IndianRed;
            this.Connect.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Connect.Location = new System.Drawing.Point(16, 96);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(104, 23);
            this.Connect.TabIndex = 6;
            this.Connect.Text = "Se connecter";
            this.Connect.UseVisualStyleBackColor = false;
            this.Connect.Click += new System.EventHandler(this.ConnectClick);
            // 
            // MainWindow2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 353);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainWindow2";
            this.Text = "MainWindow2";
            this.tabControl1.ResumeLayout(false);
            this.Chat.ResumeLayout(false);
            this.Connexion.ResumeLayout(false);
            this.Connexion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Chat;
        private System.Windows.Forms.RichTextBox msgArea;
        private System.Windows.Forms.RichTextBox chatBody;
        private System.Windows.Forms.Button sendMsg;
        private System.Windows.Forms.TabPage Connexion;
        private System.Windows.Forms.TextBox Nick;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ServerHost;
        private System.Windows.Forms.Button Connect;
    }
}