using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace NetworkLibrary.Model
{
    public class NIData : INotifyPropertyChanged
    {
        // Backing Stores
        public long total;
        public long totalSent;
        public long totalReceived;
        public OperationalStatus status;
        public long speed;

        // Properties
        public long Total
        {
            get => total;
            set
            {
                if (value != total)
                {
                    PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Total)));
                }

                total = value;
            }
        }

        public long TotalSent
        {
            get => totalSent;
            set
            {
                if (value != totalSent)
                {
                    PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(TotalSent)));
                }

                totalSent = value;
            }
        }

        public long TotalReceived
        {
            get => totalReceived;
            set
            {
                if (value != totalReceived)
                {
                    PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(TotalReceived)));
                }

                totalReceived = value;
            }
        }

        public OperationalStatus Status
        {
            get => status;
            set
            {
                if (value != status)
                {
                    PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Status)));
                }

                status = value;
            }
        }

        public long Speed
        {
            get => speed;
            set
            {
                if (value != speed)
                {
                    PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Speed)));
                }

                speed = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
