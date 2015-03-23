using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat_Server
{
    class ThreadClient
    {
        private Socket clientSocket;
        string nickname;
        bool connected;

        //Data recieved from the sockets
        byte[] msg;
        string msgString;

        public ThreadClient(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            connected = true;
            Thread newThreadClient = new Thread(threadClientMethod);
            newThreadClient.Start();
        }

        private void threadClientMethod()
        {
            while (true)
            {
                //Test to see if socket is connected
                //if it s in readmode, and there is no available data, the connexion s terminated
                if (((Socket)clientSocket).Poll(10, SelectMode.SelectRead) && ((Socket)clientSocket).Available == 0)
                {
                    clientSocket.Close();
                    connected = false;
                    return;
                }
                //Availale contient la quantité de données recues du réseau et disponibles pour la lecture
                if (clientSocket.Available > 0)
                {
                    int paquetsReceived = 0;
                    long sequence = 0;
                    string Nick = null;
                    string msgBuffer = "";
                    while (clientSocket.Available > 0)
                    {
                        msg = new byte[clientSocket.Available];

                        //Reçoit le message et le stocke dans msg
                        clientSocket.Receive(msg, msg.Length, SocketFlags.None);

                        //Convertie les données reçues en String
                        msgBuffer = System.Text.Encoding.UTF8.GetString(msg);


                        //If this is the start of the message received
                        if (paquetsReceived == 0)
                        {
                            //Réupère la séquence qui correspond aux 6 premiers caractères du message de la socket
                            string seq = msgBuffer.Substring(0, 6);
                            sequence = Convert.ToInt64(seq);
                            //Récupération du pseudo: les 9 prochains caractères
                            Nick = msgBuffer.Substring(6, 15);
                            msgString = Nick.Trim() + " wrote:" + msgBuffer.Substring(20, (msgBuffer.Length - 20)) + "\n";
                        }
                        else
                        {
                            msgString += msgBuffer;
                        }

                        if (sequence == 1)
                        {
                            nickname = Nick;
                            msgString = Nick.Trim() + " vient de se connecter";
                            msg = System.Text.Encoding.UTF8.GetBytes(msgString);
                         
                        }

                        paquetsReceived++;
                    }
                    Console.WriteLine(msgString);
                }
                Thread.Sleep(10);
            }
        }
    }
}
