using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.Model
{
    public class NIType : ObservableCollection<NetworkInterfaceType>
    {
        public NIType()
        {
            Add(NetworkInterfaceType.Wireless80211);
            Add(NetworkInterfaceType.Ethernet);
        }
    }
}
