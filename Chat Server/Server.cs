using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace Chat_Server
{
    public class Server
    {
        private Socket serverSocket;
        private ArrayList socketList = new ArrayList();
        private ArrayList readList = new ArrayList();

        //Data recieved from the sockets
        byte[] msg;
        string msgString;

        public Server()
        {
            // Create the server socket with the TCP protocol.
            this.serverSocket = new Socket(AddressFamily.InterNetwork,
                             SocketType.Stream,
                             ProtocolType.Tcp); 
        }

        public void Start()
        {
            //Démarrage du thread avant la première connexion client
            //Thread listenClients = new Thread(new ThreadStart(listenSockets));
            //listenClients.Start();

            IPHostEntry ipHostEntry = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostEntry.AddressList[0];
            Console.WriteLine("IP=" + ipAddress.ToString());
            //On lie la socket au point de communication
            serverSocket.Bind(new IPEndPoint(ipAddress, 8000));

            while (true)
            {
                Socket newClientSocket = listenAndAcceptSocket();

                socketList.Add(newClientSocket);

                new ThreadClient(newClientSocket, socketList);
                //Thread threadNewClient = new Thread(new ThreadStart(threadClient));

            }
        }


        // Keeps listening and accepts the connection of 1 client.
        public Socket listenAndAcceptSocket()
        {
            

            this.serverSocket.Listen(10);
            Console.WriteLine("Waiting for a new connection...");
            Socket newClientSocket = this.serverSocket.Accept();

            Console.WriteLine("New Client:" + newClientSocket.GetHashCode());
            return newClientSocket;
        }

        private void listenSockets()
        {
            while (true)
            {
                //readList is a buffer list, that will contain the available sockets in the lsit
                //We empty the buffer
                readList.Clear();
                //Et on la reremplit avec les sockets actuellement connectées
                for (int i = 0; i < socketList.Count; i++)
                {
                    readList.Add((Socket)socketList[i]);
                }

                if (readList.Count > 0)
                {
                    //Select vérifie ici l'accessibilité en lecture des Sockets
                    //Seules les sockets valides resteront dans readList
                    Socket.Select(readList, null, null, 1000);

                    for (int i = 0; i < readList.Count; i++)
                    {
                        //Availale contient la quantité de données recues du réseau et disponibles pour la lecture
                        if (((Socket)readList[i]).Available > 0)
                        {
                            int paquetsReceived = 0;
                            long sequence = 0;
                            string Nick = null;
                            string msgBuffer ="";
                            while (((Socket)readList[i]).Available > 0)
                            {
                                msg = new byte[((Socket)readList[i]).Available];

                                //Reçoit le message et le stocke dans msg
                                ((Socket)readList[i]).Receive(msg, msg.Length, SocketFlags.None);

                                //Convertie les données reçues en String
                                msgBuffer = System.Text.Encoding.UTF8.GetString(msg);


                                //If this is the start of the message received
                                if (paquetsReceived == 0)
                                {
                                    //Réupère la séquence qui correspond aux 6 premiers caractères du message de la socket
                                    string seq = msgString.Substring(0, 6);
                                    sequence = Convert.ToInt64(seq);
                                    //Récupération du pseudo: les 9 prochains caractères
                                    Nick = msgString.Substring(6, 15);
                                    msgString = Nick.Trim() + " wrote:" + msgBuffer.Substring(20, (msgBuffer.Length - 20)) + "\n";
                                }
                                else
                                {
                                    msgString += msgBuffer;
                                }
                                if (sequence == 1)
                                {
                                    msgString =  Nick.Trim() + " vient de se connecter\\par";
                                    msg = System.Text.Encoding.UTF8.GetBytes(msgString);
                                   
                                }

                                paquetsReceived++;
                            }
                        }
                    }
                }
            }

        }

    }
}