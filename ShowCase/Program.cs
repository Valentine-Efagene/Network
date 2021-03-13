using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

using NetworkLibrary;
using System.Net;

namespace ShowCase
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return;

            NetworkInterface[] interfaces
                = NetworkInterface.GetAllNetworkInterfaces();


            foreach (NetworkInterface ni in interfaces)
            {
                Console.WriteLine("----------------------------------------------------\n");
                Console.WriteLine("Name: {0}", ni.Name);
                Console.WriteLine("Interface Type: {0}", ni.NetworkInterfaceType);
                Console.WriteLine("Status: {0}", ni.OperationalStatus);
                Console.WriteLine("Speed: {0}", ni.Speed);
                Console.WriteLine("MAC Address: {0}", ni.GetPhysicalAddress());
                Console.WriteLine("Host Name: {0}", Dns.GetHostName());
                Console.WriteLine("IPV4 Address: {0}", Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString());
                Console.WriteLine("IPV6 Address: {0}", Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString());
                Console.WriteLine("Adapter Description: {0}", ni.Description);
                Console.WriteLine("    Bytes Sent: {0}",
                    ni.GetIPv4Statistics().BytesSent);
                Console.WriteLine("    Bytes Received: {0}",
                    ni.GetIPv4Statistics().BytesReceived);
            }
        }
    }
}
