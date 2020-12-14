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
    //convert the status of the bus to bool, in order to determine whether the ride button is enable or not
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Status statusValue = (Status)value;
            if (Status.ready == statusValue)//if the bus is ready
            {
                return true;//the ride button is enable
            }
            else
            {
                return false;//else, the ride button is not enable
            }
        }
        //The opposite converter (not used in our program)
        public object ConvertBack(object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StatusToBoolTreatConverter : IValueConverter
    //convert the status of the bus to bool, in order to determine whether the refuel and treat buttons are enable or not
    {
        public object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
        {
            Status statusValue = (Status)value;
            if (statusValue == Status.ready || statusValue == Status.notReady)//if the bus is ready or not ready
            {
                return true;//the button is enable
            }
            else //if the bus is busy
            {
                return false;//the button is not enable
            }
        }
        //The opposite converter (not used in our program)
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
    //convert the status of the bus to a string that represent an Icon, in order to determine what icon is appropriate for the bus
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
        //The opposite converter (not used in our program)
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
