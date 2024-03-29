﻿using NetworkLibrary.Model;
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
        NIData data = new NIData();
        NetworkInterface activeNetworkInterface;

        private async Task<int> HandleReceived(NetworkInterface active)
        {
            if (active == null) {
                return 0;
            };

            await Task.Run(()=> {
                Debug.WriteLine("In");
                while(true)
                {
                    if (active == null)
                    {
                        continue;
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

        public UserControl_Dashboard()
        {
            InitializeComponent();
            this.DataContext = data;

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

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await HandleReceived(activeNetworkInterface);
        }
    }
}
