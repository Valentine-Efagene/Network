using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

using NetworkLibrary;
using System.Net;
using System.ComponentModel;

namespace ShowCase
{
    class Program
    {
        public string received = "N/A";

        public async Task<int> HandleReceived(NetworkInterface active)
        {
            await Task.Run(() => {
                while (true)
                {
                    received = active.GetIPv4Statistics().BytesReceived.ToString();
                    Task.Delay(5000);
                    //Console.WriteLine(received);
                }
            });

            return 1;
        }

        static void Main(string[] args)
        {
            NetworkInterface activeNetworkInterface = NetworkLibrary.Network.GetActiveInterface(NetworkInterfaceType.Wireless80211);
            BackgroundWorker b = new BackgroundWorker();
            Program p = new Program();
            //await p.HandleReceived(activeNetworkInterface);
            string a = Console.ReadLine();
        }
    }
}
