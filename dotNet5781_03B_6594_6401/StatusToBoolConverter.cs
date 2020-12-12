using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace dotNet5781_03B_6594_6401
{
    public class StatusToBoolRideConverter : IValueConverter
    {
        public object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
        {
            Status statusValue = (Status)value;
            if (Status.ready == statusValue)
            {
                return true;
            }
            else
            {
                return false;
                //return Visibility.Visible;
            }
        }
        public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StatusToBoolTreatConverter : IValueConverter
    {
        public object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
        {
            Status statusValue = (Status)value;
            if (statusValue == Status.ready || statusValue == Status.notReady)
            {
                return true;
            }
            else
            {
                return false;
                //return Visibility.Visible;
            }
        }
        public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StatusToIconConverter : IValueConverter
    {
        public object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
        {
            Status statusValue = (Status)value;
            switch(statusValue)
            {
                case Status.notReady:
                    return "warning-emoji.png";
                case Status.ready:
                    return "ready status.png";
                case Status.Refueling:
                    return "refuel status.png";
                case Status.Ride:
                    return "ride status.png";
                case Status.Treatment:
                    return "treat status.png";
                default: return "Fasticon-Happy-Bus-Bus-orange.ico";
            }
        }
        public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
