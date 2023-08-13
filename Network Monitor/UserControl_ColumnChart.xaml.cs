using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using Network_Monitor.Converter;

namespace Network_Monitor
{
    public partial class UserControl_ColumnChart : UserControl, INotifyPropertyChanged
    {
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken token;
        public string unit;
        NetworkInterface activeNetworkInterface;
        ChartValues<double> sent = new ChartValues<double> { };
        ChartValues<double> received = new ChartValues<double> { };
        ChartValues<double> total = new ChartValues<double> { };

        public string Unit
        {
            get => unit;
            set
            {
                if (value != unit)
                {
                    PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Unit)));
                }

                unit = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public UserControl_ColumnChart()
        {
            InitializeComponent();
            token = source.Token;
            activeNetworkInterface = NetworkLibrary.Network.GetActiveInterface(MainWindow.CurrentNiType);

            if (activeNetworkInterface != null)
            {
                long key = activeNetworkInterface.GetIPv4Statistics().BytesSent;
                
                if (activeNetworkInterface.GetIPv4Statistics().BytesReceived > key)
                {
                    key = activeNetworkInterface.GetIPv4Statistics().BytesReceived;
                }

                int divider = DataConverter.GetDivider(key);
                Unit = DataConverter.GetUnitWithKey(divider).ToString();
                sent.Insert(0, DataConverter.Adjust(activeNetworkInterface.GetIPv4Statistics().BytesSent, divider));
                received.Insert(0, DataConverter.Adjust(activeNetworkInterface.GetIPv4Statistics().BytesReceived, divider));
                total.Insert(0, DataConverter.Adjust(activeNetworkInterface.GetIPv4Statistics().BytesSent + activeNetworkInterface.GetIPv4Statistics().BytesReceived, divider));
            }

            SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Sent",
                        Values = sent
                    },
                    new ColumnSeries
                    {
                        Title = "Received",
                        Values = received
                    },
                    new ColumnSeries
                    {
                        Title = "Total",
                        Values = total
                    }
                };

            //also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            //Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        private async Task<int> Animate(NetworkInterface active)
        {
            if (active == null)
            {
                return 0;
            }

            await Task.Run(() => {
                while (true)
                {
                    if (active == null)
                    {
                        break;
                    }

                    sent.Clear();
                    received.Clear();
                    total.Clear();

                    long key = active.GetIPv4Statistics().BytesSent;

                    if (active.GetIPv4Statistics().BytesReceived > key)
                    {
                        key = active.GetIPv4Statistics().BytesReceived;
                    }

                    int divider = DataConverter.GetDivider(key);
                    Unit = DataConverter.GetUnitWithKey(divider).ToString();
                    sent.Insert(0, DataConverter.Adjust(active.GetIPv4Statistics().BytesSent, divider));
                    received.Insert(0, DataConverter.Adjust(active.GetIPv4Statistics().BytesReceived, divider));
                    total.Insert(0, DataConverter.Adjust(active.GetIPv4Statistics().BytesSent + active.GetIPv4Statistics().BytesReceived, divider));

                    Thread.Sleep(200);
                }
            }, token);

            return 0;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private async void ColumnChart_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await Animate(activeNetworkInterface);
        }
    }
}