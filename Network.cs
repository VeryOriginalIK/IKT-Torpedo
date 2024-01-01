
//using System;
//using System.IO;
//using System.Net;
//using System.Net.NetworkInformation;
//using System.Net.Sockets;
//using System.Text;

//namespace Torpedo
//{
//    public static class Network
//    {
//        internal class HalozatiAdatok
//        {
//            public static string sajatDomain = Dns.GetHostName();
//            public static IPAddress sajatIp = Dns.GetHostAddresses(sajatDomain)[2];
//            public static IPAddress ellenfelIP = IPAddress.Parse("192.168.1.2");
//            public static IPEndPoint celallomas;
//            public static TcpClient kuldes = new TcpClient();
//            public static TcpListener listener = new(sajatIp, 1000);
//            public static TcpClient fogadas = new TcpClient();
//            public static NetworkStream stream = fogadas.GetStream();
//            public static StreamReader olvas = new StreamReader(stream);
//            public static StreamWriter ir = new StreamWriter(stream);
//        }

//        public static void Csatlakozas()
//        {
//            // Retrive the Name of HOST
//            Console.WriteLine(HalozatiAdatok.sajatDomain);
//            // Get the IP

//            Console.WriteLine($"A te IP címed: {HalozatiAdatok.sajatIp}");
//            Console.WriteLine("Írd be a másik játékos IP címét: ");
//            HalozatiAdatok.ellenfelIP = IPAddress.Parse(Console.ReadLine());
//            HalozatiAdatok.celallomas = new IPEndPoint(HalozatiAdatok.ellenfelIP, 1001);
//            HalozatiAdatok.kuldes.Connect(HalozatiAdatok.celallomas);
//            Console.WriteLine("Meghívó elküldve...");
//            if (HalozatiAdatok.kuldes.Connected)
//                Console.WriteLine("Kapcsolat kész!");
//        }

//        public static void Kuldes(string tipp)
//        {
//            HalozatiAdatok.ir.AutoFlush = true;
//            HalozatiAdatok.ir.Write(tipp);
//        }

//        public static string Fogadas()
//        {
//            string szoveg = HalozatiAdatok.olvas.ReadLine();
//            return szoveg;
//        }
//    }
//}