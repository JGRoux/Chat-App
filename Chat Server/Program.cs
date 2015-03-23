﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.IO;

namespace Chat_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server startServer = new Server();

            startServer.Start();
        }
    }

    public class Server
    {
        private Socket serverSocket;
        private ArrayList clientSocketList = new ArrayList();

        //Liste de correspondance entre les pseudos et leurs sockets
        public Hashtable NickList = new Hashtable();
        ArrayList lectureList = new ArrayList();

        string msgString = null;
        string msgDeconnecte = null;
        byte[] msg;
        public StreamWriter logW = null;

        // Keeps listening and accepts the connection of 1 client.
        private Socket listenAndAcceptSocket()
        {
            this.serverSocket.Listen(1);
            Socket newClientSocket = this.serverSocket.Accept();

            return newClientSocket;
        }

        public void Start()
        {
            //Démarrage du Serveur
            //On commence par récupérer l'adresse ip de l'hôte d'entrée
            IPHostEntry ipHostEntry = Dns.Resolve(Dns.GetHostName());
            //Le serveur aura pour adresse la première adresse disponible
            IPAddress ipAddress = ipHostEntry.AddressList[0];
            Console.WriteLine("IP : " + ipAddress.ToString());
            Socket clientSocket = null;
            //Création de la socket coté serveur
            this.serverSocket = new Socket(AddressFamily.InterNetwork,
                             SocketType.Stream,
                             ProtocolType.Tcp); //On utilise le protocole TCP
            try
            {
                //On lie la socket au serveur, port 8000
                this.serverSocket.Bind(new IPEndPoint(ipAddress, 8000));
                //On la positionne en écoute
                this.serverSocket.Listen(10);
                //Démarrage du thread avant la première connexion client
                Thread getReadClients = new Thread(new ThreadStart(getRead));
                getReadClients.Start();
                Thread updateThread = new Thread(new ThreadStart(CheckConnectedClients));
                updateThread.Start();
                //Boucle infinie
                while (true)
                {
                    Console.WriteLine("Attente d'une nouvelle connexion...");
                    //L'exécution du thread courant est bloquée jusqu'à ce qu'un
                    //nouveau client se connecte
                    clientSocket = this.serverSocket.Accept();
                    Console.WriteLine("Nouveau client:" + clientSocket.GetHashCode());
                    //Ajout de la socket du nouveau client, à la liste des scokets
                    clientSocketList.Add(clientSocket);
                }
            }
            catch (SocketException E)
            {
                Console.WriteLine(E.Message);
            }
        }

        //Thread vérifiant que les clients connectés le sont toujours
        private void CheckConnectedClients()
        {
            /* Etant donné que la propriété .Connected d'une socket n'est pas
             * mise à jour lors de la déconnexion d'un client sans que l'on ait
             * prélablement essayé de lire ou d'écrire sur cette socket, cette méthode
             * parvient à déterminer si une socket cliente s'est déconnectée grâce à la méthode
             * poll. On effectue un poll en lecture sur la socket, si le poll retourne vrai et que
             * le nombre de bytes disponible est 0, il s'agit d'une connexion terminée*/

            while (true)
            {

                for (int i = 0; i < clientSocketList.Count; i++)
                {
                    //Renvoie vrai si la socket passée est à l'état demandé
                    //Si la socket est en lecture, et qu'il n'y a pas de bits disponibles, c'est que la connexion est terminée
                    if (((Socket)clientSocketList[i]).Poll(10, SelectMode.SelectRead) && ((Socket)clientSocketList[i]).Available == 0)
                    {
                            Console.WriteLine("Client " + ((Socket)clientSocketList[i]).GetHashCode() + " déconnecté");
                            removeNick(((Socket)clientSocketList[i]));
                            ((Socket)clientSocketList[i]).Close();
                            clientSocketList.Remove(((Socket)clientSocketList[i]));
                            i--;
                     }
                }
                Thread.Sleep(5);
            }

        }

        private void removeNick(Socket Resource)
        {
            Console.Write("DECONNEXION DE:" + NickList[Resource]);
            msgDeconnecte = ((string)NickList[Resource]).Trim() + "vient de se déconnecter!";
            Thread DiscInfoToAll = new Thread(new ThreadStart(infoToAll));
            DiscInfoToAll.Start();
            DiscInfoToAll.Join();
            NickList.Remove(Resource);
        }

        private void getRead()
        {
            //Boucle infinie car cette thread s'execute en permanence
            while (true)
            {
                //On vide la liste de sockets connectées
                lectureList.Clear();
                //Et on la reremplit avec les sockets actuellement connectées
                for (int i = 0; i < clientSocketList.Count; i++)
                {
                    lectureList.Add((Socket)clientSocketList[i]);
                }

                if (lectureList.Count > 0)
                {
                    //Select vérifie ici l'accessibilité en lecture des Sockets
                    //Seules les sockets valides resteront dans lectureList
                    Socket.Select(lectureList, null, null, 1000);

                    for (int i = 0; i < lectureList.Count; i++)
                    {
                        //Availale contient la quantité de données recues du réseau et disponibles pour la lecture
                        if (((Socket)lectureList[i]).Available > 0)
                        {
                           
                            int paquetsReceived = 0;
                            long sequence = 0;
                            string Nick = null;
                            string formattedMsg = null;
                            while (((Socket)lectureList[i]).Available > 0)
                            {
                                msg = new byte[((Socket)lectureList[i]).Available];

                                //Reçoit le message et le stocke dans msg
                                ((Socket)lectureList[i]).Receive(msg, msg.Length, SocketFlags.None);

                                //Convertie les données reçues en String
                                msgString = System.Text.Encoding.UTF8.GetString(msg);
                                if (paquetsReceived == 0)
                                {
                                    //Réupère la séquence qui correspond aux 6 premiers caractères du message de la socket
                                    string seq = msgString.Substring(0, 6);
                                    sequence = Convert.ToInt64(seq);
                                    //Récupération du pseudo: les 9 prochains caractères
                                    Nick = msgString.Substring(6, 15);
                                    msgString = Nick.Trim() + " a ecrit:" + msgString.Substring(20, (msgString.Length - 20)) + "\n";
                                }
                                
                                msg = System.Text.Encoding.UTF8.GetBytes(msgString);

                                if (sequence == 1)
                                {
                                    NickList.Add((Socket)lectureList[i], Nick);
                                    //getConnected();
                                    string message = Nick.Trim() + " vient de se connecter\\par";
                                    msg = System.Text.Encoding.UTF8.GetBytes(message);
                                }

                                Thread forwardingThread = new Thread(new ThreadStart(sendMsg));
                                forwardingThread.Start();
                                forwardingThread.Join();
                                paquetsReceived++;

                            }
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }

        private void sendMsg()
        {

            for (int i = 0; i < clientSocketList.Count; i++)
            {
                if (((Socket)clientSocketList[i]).Connected)
                {
                    try
                    {
                        int bytesSent = ((Socket)clientSocketList[i]).Send(msg, msg.Length, SocketFlags.None);
                    }

                    catch
                    {
                        Console.Write(((Socket)clientSocketList[i]).GetHashCode() + " déconnecté");
                    }
                }
                else
                {
                    clientSocketList.Remove((Socket)clientSocketList[i]);
                    i--;
                }
            }
        }

        private void infoToAll()
        {
            for (int i = 0; i < clientSocketList.Count; i++)
            {
                if (((Socket)clientSocketList[i]).Connected)
                {
                    try
                    {
                        //byte[] msg=System.Text.Encoding.UTF8.GetBytes(message);
                        
                        // Ligne dessous commentée car ne compile pas.
                        //byte[] msg = System.Text.Encoding.UTF8.GetBytes(msgDisconnected);
                        int bytesSent = ((Socket)clientSocketList[i]).Send(msg, msg.Length, SocketFlags.None);
                        Console.WriteLine("Writing to:" + clientSocketList.Count.ToString());
                    }

                    catch
                    {
                        Console.Write(((Socket)clientSocketList[i]).GetHashCode() + " déconnecté");
                    }
                }
                else
                {
                    clientSocketList.Remove((Socket)clientSocketList[i]);
                    i--;
                }
            }
        }
    }
}