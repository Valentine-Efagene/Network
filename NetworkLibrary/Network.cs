using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary
{

    /// <summary>
    /// Handles all network interactions.
    /// </summary>
    public class Network
    {
        
        /// <summary>
        /// Gets all available network interfaces.
        /// </summary>
        /// <returns>List of available network interfaces</returns>
        public static NetworkInterface[] GetNetworkInterfaces()
        {
            return NetworkInterface.GetIsNetworkAvailable() ? NetworkInterface.GetAllNetworkInterfaces() : null;
        }

        /// <summary>
        /// Get sent bytes count.
        /// </summary>
        /// <param name="networkInterface"></param>
        /// <returns></returns>
        public long GetBytesSent(NetworkInterface networkInterface)
        {
            return networkInterface.GetIPv4Statistics().BytesSent;
        }

        /// <summary>
        /// Get received bytes count.
        /// </summary>
        /// <param name="networkInterface"></param>
        /// <returns></returns>
        public long GetBytesReceived(NetworkInterface networkInterface)
        {
            return networkInterface.GetIPv4Statistics().BytesReceived;
        }

        public static NetworkInterface GetActiveInterface(NetworkInterfaceType type)
        {
            NetworkInterface[] networkInterfaces = GetNetworkInterfaces();

            if (networkInterfaces == null)
            {
                return null;
            }

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if(networkInterface.NetworkInterfaceType == type && networkInterface.OperationalStatus == OperationalStatus.Up && networkInterface.Speed != -1)
                {
                    return networkInterface;
                }
            }

            return null;
        }
    }
}
