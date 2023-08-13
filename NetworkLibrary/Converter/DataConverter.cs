using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NetworkLibrary.Converter
{
    public class DataConverter : IValueConverter
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

        public static string GetUnit(double value)
        {
            if (value < 1024)
            {
                return "B";
            }

            if (value >= 1024 && value < 1048576)
            {
                return "KB";
            }

            if (value >= 1048576 && value < 1073741824)
            {
                return "MB";
            }

            if (value >= 1073741824)
            {
                return "GB";
            }

            return "B";
        }

        public static DataUnit GetUnit(long value)
        {
            if (value < 1024)
            {
                return DataUnit.B;
            }

            if (value >= 1024 && value < 1048576)
            {
                return DataUnit.KB;
            }

            if (value >= 1048576 && value < 1073741824)
            {
                return DataUnit.MB;
            }

            if (value >= 1073741824)
            {
                return DataUnit.GB;
            }

            return DataUnit.B;
        }

        public static DataUnit GetUnitWithKey(int divider)
        {
            switch (divider)
            {
                case 1:
                    return DataUnit.B;
                case 1024:
                    return DataUnit.KB;
                case 1048576:
                    return DataUnit.MB;
                case 1073741824:
                    return DataUnit.GB;
                default:
                    return DataUnit.B;
            }
        }

        public static int GetDivider(long value)
        {
            if (value < 1024)
            {
                return 1;
            }

            if (value >= 1024 && value < 1048576)
            {
                return 1024;
            }

            if (value >= 1048576 && value < 1073741824)
            {
                return 1048576;
            }

            if (value >= 1073741824)
            {
                return 1073741824;
            }

            return 1;
        }

        public static double Adjust(long value, int divider)
        {
            NumberFormatInfo setPrecision = new NumberFormatInfo();
            setPrecision.NumberDecimalDigits = 2;
            double res = value / (double)divider;
            return (int)res == value ? value : Math.Round(res, 2);
        }

        public static double GetUnitAdjustedValue(long value)
        {
            NumberFormatInfo setPrecision = new NumberFormatInfo();
            setPrecision.NumberDecimalDigits = 2;

            if (value < 1024)
            {
                return value;
            }

            if (value >= 1024 && value < 1048576)
            {
                return Math.Round(value / 1024.0, 2);
            }

            if (value >= 1048576 && value < 1073741824)
            {
                return Math.Round(value / 1048576.0, 2);
            }

            if (value >= 1073741824)
            {
                return Math.Round(value / 1073741824.0, 2);
            }

            return value;
        }
    }

    public enum DataUnit
    {
        B,
        KB,
        MB,
        GB
    }
}
