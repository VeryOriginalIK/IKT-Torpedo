using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Torpedo
{
    public static class Network
    {
        public static void Csatlakozas()
        {

            try
            {

                // Establish the remote endpoint 
                // for the socket. This example 
                // uses port 11111 on the local 
                // computer.
                string sajatDomain = Dns.GetHostName(); // Retrive the Name of HOST
                Console.WriteLine(sajatDomain);
                // Get the IP
                IPAddress sajatIp = Dns.GetHostAddresses(sajatDomain)[2];
                Console.WriteLine($"A te IP címed: {sajatIp}");
                Console.WriteLine("Írd be a másik játékos IP címét: ");
                IPAddress ellenfelIP = IPAddress.Parse(Console.ReadLine());
                IPEndPoint celallomas = new IPEndPoint(ellenfelIP, 1001);

                // Creation TCP/IP Socket using 
                // Socket Class Constructor
                Socket sender = new Socket(sajatIp.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);

                try
                {

                    // Connect Socket to the remote 
                    // endpoint using method Connect()
                    sender.Connect(celallomas);

                    // We print EndPoint information 
                    // that we are connected
                    Console.WriteLine($"Csatlakozva {0}-hez ",
                                  sender.RemoteEndPoint.ToString());

                    // Creation of message that
                    // we will send to Server
                    byte[] messageSent = Encoding.ASCII.GetBytes("SZiaaaaaaaaaaaaaaaaaaaaaaa");
                    int byteSent = sender.Send(messageSent);

                    // Data buffer
                    byte[] messageReceived = new byte[1024];

                    // We receive the message using 
                    // the method Receive(). This 
                    // method returns number of bytes
                    // received, that we'll use to 
                    // convert them to string
                    int byteRecv = sender.Receive(messageReceived);
                    Console.WriteLine($"Message from Server -> {0}",
                          Encoding.ASCII.GetString(messageReceived,
                                                     0, byteRecv));

                    // Close Socket using 
                    // the method Close()
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }

                // Manage of Socket's Exceptions
                catch (ArgumentNullException c)
                {

                    Console.WriteLine("Helytelen Ip: {0}", c.ToString());
                }

                catch (SocketException d)
                {
                    Console.WriteLine("Csatlakozási hiba: {0}", d.Message.ToString());
                }

                catch (Exception e)
                {
                    Console.WriteLine("Ismeretlen hiba: {0}", e.ToString());
                }
            }

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }
    }
}