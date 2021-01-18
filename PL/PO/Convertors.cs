﻿using System;
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
    public class LanguageToAlignment : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri resourceValue = (Uri)value;
            Uri dictUriEN = new Uri(@"/res/languages/AppString_EN.xaml", UriKind.Relative);
            //Uri dictUriHE = new Uri(@"res/languages/AppString_HE.xaml", UriKind.Relative);
            //ResourceDictionary resDictEN = Application.LoadComponent(dictUriEN) as ResourceDictionary;
            //ResourceDictionary resDictHE = Application.LoadComponent(dictUriHE) as ResourceDictionary;
            if (resourceValue == dictUriEN)
            {
                return FlowDirection.LeftToRight;
            }
            else
            {
                return FlowDirection.RightToLeft;
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