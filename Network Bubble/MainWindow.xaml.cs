using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NetworkLibrary.Model;

namespace Network_Bubble
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NIData data = new NIData();
        NetworkInterface activeNetworkInterface;
        public static NetworkInterfaceType CurrentNiType { get; set; }

        private async Task<int> HandleReceived(NetworkInterface active, NIData data)
        {
            await Task.Run(() => {
                while (true)
                {
                    if (active == null)
                    {
                        active = NetworkLibrary.Network.GetActiveInterface(MainWindow.CurrentNiType);

                        if (active == null)
                        {
                            continue;
                        }
                    }

                    data.TotalSent = active.GetIPv4Statistics().BytesSent;
                    data.TotalReceived = active.GetIPv4Statistics().BytesReceived;
                    data.Total = data.TotalReceived + data.TotalSent;

                    if (data.Status != active.OperationalStatus)
                    {
                        data.Status = active.OperationalStatus;
                    }

                    Thread.Sleep(100);
                }
            });

            return 0;
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = data;
            CurrentNiType = NetworkInterfaceType.Wireless80211;

            activeNetworkInterface = NetworkLibrary.Network.GetActiveInterface(MainWindow.CurrentNiType);

            if (activeNetworkInterface != null)
            {
                data.TotalReceived = activeNetworkInterface.GetIPv4Statistics().BytesReceived;
                data.TotalSent = activeNetworkInterface.GetIPv4Statistics().BytesSent;
                data.Total = data.TotalReceived + data.TotalSent;
                data.Speed = activeNetworkInterface.Speed;
                data.Status = activeNetworkInterface.OperationalStatus;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bool LMouseIsDown = Mouse.LeftButton == MouseButtonState.Pressed;

            if (LMouseIsDown)
            {
                DragMove();
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await HandleReceived(activeNetworkInterface, data);
        }

        private void MenuItem_Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
