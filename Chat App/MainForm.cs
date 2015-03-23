using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace Chat_Client
{
    public partial class MainForm : Form
    {
        public Socket ClientSocket = null;
        public Thread DataReceived = null;

        public string NickName = null;
        public long sequence = 0;
        public int numberMsg = 0;
      
        
        private delegate void ClearMsg();
        private delegate void DisplayMsg(string msg);

        private void ClearMsgArea()
        {
            msgArea.Clear();
        }

        private void DisplayMsgChatBody(string msg)
        {
            chatBody.Text += "\n" + msg;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        //Cette méthode traite le message à envoyer sur le serveur
        void SendMessage(object sender, System.EventArgs e)
        {
            //On vérifie que le client est bien connecté
            if (ClientSocket == null || !ClientSocket.Connected)
            {
                MessageBox.Show("Vous n'êtes pas connecté");
                return;
            }
            try
            {
                if (msgArea.Text != "")
                {

                    //Chaque message est précédé d'un numéro de séquence, ce qui permet
                    //de vérifier si le client vient de se connecter ou non


                    SendMsg(GetSequence() + NickName + msgArea.Text);
                  
                    msgArea.Clear();
                }

            }
            catch (Exception E)
            {
                MessageBox.Show("SendMessage:" + E.Message);
            }
        }

            //Cette méthode envoie le message sur le serveur
		void SendMsg(string message)
		{
			
			byte[] msg = System.Text.Encoding.UTF8.GetBytes(message);
			int DtSent=ClientSocket.Send(msg,msg.Length,SocketFlags.None);
				
			if(DtSent == 0)
			{
				MessageBox.Show("Aucune donnée n'a été envoyée");
			}	
		}

        //Cette méthode permet de récupérer l'adresse ip du serveur sur lequel
        //on désire se connecter
        private String GetAdr()
        {
            try
            {
                IPHostEntry iphostentry = Dns.GetHostByName(ServerHost.Text);
                String IPStr = "";
                foreach (IPAddress ipaddress in iphostentry.AddressList)
                {
                    IPStr = ipaddress.ToString();
                    return IPStr;
                }

            }
            catch (SocketException E)
            {
                MessageBox.Show(E.Message);
            }

            return "";
        }		


        //Cette méthode est appelée par un thread à part qui lit constament la socket
        //pour voir si le serveur essaye d'écrire dessus
        private void CheckData()
        {
            try
            {
                while (true)
                {

                    if (ClientSocket.Connected)
                    {
                        //Poll Détermine si la socket est en mode lecture
                        if (ClientSocket.Poll(10, SelectMode.SelectRead) && ClientSocket.Available == 0)
                        {
                            //La connexion a été clôturée par le serveur ou bien un problème
                            //réseau est apparu
                            MessageBox.Show("La connexion au serveur est interrompue. Essayez avec un autre pseudo");
                            Connect.Enabled = true;
                            Thread.CurrentThread.Abort();
                        }
                        //Si la socket a des données à lire							
                        if (ClientSocket.Available > 0)
                        {
                            string messageReceived = null;

                            while (ClientSocket.Available > 0)
                            {
                               byte[] msg = new Byte[ClientSocket.Available];
                               //Réception des données
                               ClientSocket.Receive(msg, 0, ClientSocket.Available, SocketFlags.None);
                               messageReceived = System.Text.Encoding.UTF8.GetString(msg).Trim();
                               //On concatène les données reçues(max 4ko) dans une variable de la classe
                              
                            }
                            try
                            {
                                //On remplit le richtextbox avec les données reçues lorsqu'on a tout réceptionné
                               
                                this.Invoke(new DisplayMsg(DisplayMsgChatBody), messageReceived); 
                                //this.BringToFront();

                            }
                            catch (Exception E)
                            {
                                MessageBox.Show(E.Message);
                            }

                        }
                    }
                    //On temporise pendant 10 millisecondes, ceci pour éviter
                    //que le micro processeur s'emballe
                    Thread.Sleep(10);
                }

            }
            catch
            {
                //Ce thread étant susceptible d'être arrêté à tout moment
                //on catch l'exception afin de ne pas afficher un message à l'utilisateur
                Thread.ResetAbort();
            }
        }

        void ConnectClick(object sender, System.EventArgs e)
        {
            if (Nick.Text == "")
            {
                MessageBox.Show("Le pseudo ne peut être null");
                return;
            }
            if (ServerHost.Text == "")
            {
                MessageBox.Show("Le nom du serveur ne peut être null");
                return;
            }
            //On formatte le pseudo sur une longueur de 15 caractères
            NickName = Nick.Text.Trim();
            if (NickName.Length < 15)
            {
                char pad = Convert.ToChar(" ");
                NickName = NickName.PadRight(15, pad);
            }
            else if (NickName.Length > 15)
            {
                MessageBox.Show("Le pseudo doit être de 15 caractères maximum");
                return;
            }
            //Chaque message sera précédé d'un numéro de sequence
            //Le numéro de séquence 1 servira à identifier le pseudo
            //côté serveur.
            sequence = 0;

            IPAddress ip = IPAddress.Parse(GetAdr());
            IPEndPoint ipEnd = new IPEndPoint(ip, 8000);
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                ClientSocket.Connect(ipEnd);
                if (ClientSocket.Connected)
                {
                    SendMsg(GetSequence() + NickName);
                    Connect.Enabled = false;

                }


            }
            catch (SocketException E)
            {
                MessageBox.Show("Connection" + E.Message);
            }
            try
            {
                DataReceived = new Thread(new ThreadStart(CheckData));
                DataReceived.Start();
            }
            catch (Exception E)
            {
                MessageBox.Show("Démarrage Thread" + E.Message);
            }
        }

        //Cette méthode génère le numéro de séquence collé en 
		//entête du message envoyé au serveur
		string GetSequence()
		{
			sequence++;
			string msgSeq=Convert.ToString(sequence);
			char pad=Convert.ToChar("0");
			msgSeq=msgSeq.PadLeft(6,pad);
			return msgSeq;
		}


        //Cette méthode est exécutée à lorsque l'on quitte l'application.
        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Si le thread recevant les données a été démarré, on l'arrête
            if (DataReceived != null)
            {
                try
                {
                    DataReceived.Abort();
                    DataReceived.Join();
                }
                catch (Exception E)
                {
                    MessageBox.Show("Arrêt Thread" + E.Message);
                }

            }
            if (ClientSocket != null && ClientSocket.Connected)
            {

                try
                {
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close();
                    if (ClientSocket.Connected)
                    {
                        MessageBox.Show("Erreur: " + Convert.ToString(System.Runtime.InteropServices.Marshal.GetLastWin32Error()));
                    }

                }
                catch (SocketException SE)
                {
                    MessageBox.Show("SE" + SE.Message);
                }

            }

        }

    }
}
