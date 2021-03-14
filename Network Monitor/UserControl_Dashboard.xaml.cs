using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Network_Monitor
{
    /// <summary>
    /// Interaction logic for UserControl_Dashboard.xaml
    /// </summary>
    public partial class UserControl_Dashboard : UserControl
    {
        Model.NIData data = new Model.NIData();
        NetworkInterface activeNetworkInterface;

        private async Task<int> HandleReceived(NetworkInterface active)
        {

            await Task.Run(()=> {
                while(true)
                {
                    if (active == null)
                    {
                        continue;
                    }

                    data.TotalSent = active.GetIPv4Statistics().BytesSent;
                    data.TotalReceived = active.GetIPv4Statistics().BytesReceived;

                    if (data.Status != active.OperationalStatus)
                    {
                        data.Status = active.OperationalStatus;
                    }

                    Thread.Sleep(100);
                }
            });

            return 1;
        }

        public UserControl_Dashboard()
        {
            InitializeComponent();
            this.DataContext = data;

            activeNetworkInterface = NetworkLibrary.Network.GetActiveInterface(MainWindow.CurrentNiType);

            if (activeNetworkInterface != null)
            {
                data.TotalReceived = activeNetworkInterface.GetIPv4Statistics().BytesReceived;
                data.TotalSent = activeNetworkInterface.GetIPv4Statistics().BytesSent;
                data.Speed = activeNetworkInterface.Speed;
                data.Status = activeNetworkInterface.OperationalStatus;
            }
        }

        private async void Button_Test_Click(object sender, RoutedEventArgs e)
        {
            await HandleReceived(activeNetworkInterface);
        }
    }
}
