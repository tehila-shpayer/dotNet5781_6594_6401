using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    public class Station : DependencyObject
    {
        static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(int), typeof(Station));
        static readonly DependencyProperty LatitudeProperty = DependencyProperty.Register("Latitude", typeof(double), typeof(Station));
        static readonly DependencyProperty LongitudeProperty = DependencyProperty.Register("Longitude", typeof(double), typeof(Station));
        static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(String), typeof(Station));
        static readonly DependencyProperty BusLinesProperty = DependencyProperty.Register("BusLines", typeof(IEnumerable<int>), typeof(Station));
        static readonly DependencyProperty PresentBusLinesProperty = DependencyProperty.Register("PresentBusLines", typeof(IEnumerable<PresentBusLineForStation>), typeof(Station));

        public int Key { get => (int)GetValue(KeyProperty); set => SetValue(KeyProperty, value); }
        public double Latitude { get => (double)GetValue(LatitudeProperty); set => SetValue(LatitudeProperty, value); }
        public double Longitude { get => (double)GetValue(LongitudeProperty); set => SetValue(LongitudeProperty, value); }

        public String Name { get => (String)GetValue(NameProperty); set => SetValue(NameProperty, value); }
        public IEnumerable<int> BusLines { get => (IEnumerable<int>)GetValue(BusLinesProperty); set => SetValue(BusLinesProperty, value); }
        public IEnumerable<PresentBusLineForStation> PresentBusLines { get => (IEnumerable<PresentBusLineForStation>)GetValue(PresentBusLinesProperty); set => SetValue(PresentBusLinesProperty, value); }
    }
}
