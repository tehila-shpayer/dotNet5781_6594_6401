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
    /// <summary>
    /// ממיר ממשתנה בוליאני לתכונת נראות
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue == true)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
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
    /// <summary>
    /// ממיר מהשפה בשימוש באפליקציה לכיוון 
    /// </summary>
    public class LanguageToAlignment : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri resourceValue = (Uri)value;
            Uri dictUriHE = new Uri(@"/res/languages/AppString_HE.xaml", UriKind.Relative);
            if (resourceValue == dictUriHE)
            {
                return FlowDirection.RightToLeft;
            }
            else
            {
                return FlowDirection.LeftToRight;
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
    public class TrueToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue == true)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
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
