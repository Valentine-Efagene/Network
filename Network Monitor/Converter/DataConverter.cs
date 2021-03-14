using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Network_Monitor.Converter
{
    class DataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NumberFormatInfo setPrecision = new NumberFormatInfo();
            setPrecision.NumberDecimalDigits = 2;

            long val = (long)value;

            if (val < 1024)
            {
                return val.ToString() + " B";
            }

            if (val >= 1024 && val < 1048576)
            {
                return Math.Round(val / 1024.0, 2).ToString() + " KB";
            }

            if (val >= 1048576 && val < 1073741824)
            {
                return Math.Round(val / 1048576.0, 2).ToString() + " MB";
            }

            if (val >= 1073741824)
            {
                return Math.Round(val / 1073741824.0, 2).ToString() + " GB";
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
