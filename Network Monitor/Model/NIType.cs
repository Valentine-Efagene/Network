using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Network_Monitor.Model
{
    class NIType : ObservableCollection<NetworkInterfaceType>
    {
        public NIType()
        {
            Add(NetworkInterfaceType.Wireless80211);
            Add(NetworkInterfaceType.Ethernet);
        }
    }
}
