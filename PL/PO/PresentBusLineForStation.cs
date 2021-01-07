using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class PresentBusLineForStation : DependencyObject
    {
        static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(int), typeof(PresentBusLineForStation));
        static readonly DependencyProperty LineNumberProperty = DependencyProperty.Register("LineNumber", typeof(int), typeof(PresentBusLineForStation));
        static readonly DependencyProperty NameFirstStationProperty = DependencyProperty.Register("NameFirstStation", typeof(String), typeof(PresentBusLineForStation));
        static readonly DependencyProperty NameLastStationProperty = DependencyProperty.Register("NameLastStation", typeof(String), typeof(PresentBusLineForStation));
        public int Key { get => (int)GetValue(KeyProperty); set => SetValue(KeyProperty, value); }
        public int LineNumber { get => (int)GetValue(LineNumberProperty); set => SetValue(LineNumberProperty, value); }
        public String NameFirstStation { get => (String)GetValue(NameFirstStationProperty); set => SetValue(NameFirstStationProperty, value); }
        public String NameLastStation { get => (String)GetValue(NameLastStationProperty); set => SetValue(NameLastStationProperty, value); }
    }
}
