using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Windows.Forms;

namespace ServerApp
{
    public class Server
    {
        public static TcpClient client;
        private static TcpListener listener;
        private static string IpString;
        static void Main(string[] args)
        {           
            void Sleep()
            {
                Application.SetSuspendState(PowerState.Suspend, true, true);
            }
            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress address in localIP)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    IpString = address.ToString();
                }
            }
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IpString), 1234);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine(@"    
            ===================================================    
             Started listening requests at: {0} : {1}    
            ===================================================",
            ep.Address, ep.Port);
            client = listener.AcceptTcpClient();
            Console.WriteLine("Connected to client!" + "\n");
            while (client.Connected)
            {
                try
                {
                    const int bytesize = 1024 * 1024;
                    byte[] buffer = new byte[bytesize];
                    string x = client.GetStream().Read(buffer, 0, bytesize).ToString();
                    var data = ASCIIEncoding.ASCII.GetString(buffer);

                    if (data.ToUpper().Contains("SLP2"))
                    {
                        Console.WriteLine("Sleep Mode on!" + "\n");
                        Sleep();
                    }

                }
                catch (Exception)
                {
                    client.Dispose();
                    client.Close();
                }
            }
        }
    }
}
