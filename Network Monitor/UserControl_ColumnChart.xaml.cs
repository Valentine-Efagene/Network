using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace Network_Monitor
{
    public partial class UserControl_ColumnChart : UserControl
    {
        NetworkInterface activeNetworkInterface;
        ChartValues<long> sent = new ChartValues<long> { 0 };
        ChartValues<long> received = new ChartValues<long> { 0 };

        public UserControl_ColumnChart()
        {
            InitializeComponent();

            activeNetworkInterface = NetworkLibrary.Network.GetActiveInterface(MainWindow.CurrentNiType);

            if (activeNetworkInterface != null)
            {
                sent.Insert(0, activeNetworkInterface.GetIPv4Statistics().BytesSent);
                received.Insert(0, activeNetworkInterface.GetIPv4Statistics().BytesReceived);
            }

            SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Sent",
                        Values = sent
                    }
                };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Received",
                Values = received
            });

            //also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            //Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        private async Task<int> Animate(NetworkInterface active)
        {

            await Task.Run(() => {
                while (true)
                {
                    if (active == null)
                    {
                        break;
                    }

                    sent.Clear();
                    received.Clear();
                    sent.Insert(0, activeNetworkInterface.GetIPv4Statistics().BytesSent);
                    received.Insert(0, activeNetworkInterface.GetIPv4Statistics().BytesReceived);
                    
                    Thread.Sleep(500);
                }
            });

            return 1;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private async void Button_Test_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await Animate(activeNetworkInterface);
        }
    }
}