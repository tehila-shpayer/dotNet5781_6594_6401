using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue == true)//if the bus is ready
            {
                return Visibility.Collapsed;//the ride button is enable
            }
            else
            {
                return Visibility.Hidden;//else, the ride button is not enable
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
}
