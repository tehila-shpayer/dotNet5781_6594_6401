using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    /// <summary>
    /// PLמחלקה לייצוג קו אוטובוס בשכבת ה
    /// </summary>
    public class BusLine : DependencyObject
    {
        static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(int), typeof(BusLine));
        static readonly DependencyProperty LineNumberProperty = DependencyProperty.Register("LineNumber", typeof(int), typeof(BusLine));
        static readonly DependencyProperty AreaProperty = DependencyProperty.Register("Area", typeof(BO.Areas), typeof(BusLine));
        static readonly DependencyProperty FirstStationProperty = DependencyProperty.Register("FirstStation", typeof(int), typeof(BusLine));
        static readonly DependencyProperty LastStationProperty = DependencyProperty.Register("LastStation", typeof(int), typeof(BusLine));
        static readonly DependencyProperty BusLineStationsProperty = DependencyProperty.Register("BusLineStations", typeof(IEnumerable<BusLineStation>), typeof(BusLine));
        public int Key { get => (int)GetValue(KeyProperty); set => SetValue(KeyProperty, value); }
        public int LineNumber { get => (int)GetValue(LineNumberProperty); set => SetValue(LineNumberProperty, value); }
        public BO.Areas Area { get => (BO.Areas)GetValue(AreaProperty); set => SetValue(AreaProperty, value); }
        public int FirstStation { get => (int)GetValue(FirstStationProperty); set => SetValue(FirstStationProperty, value); }
        public int LastStation { get => (int)GetValue(LastStationProperty); set => SetValue(LastStationProperty, value); }
        public IEnumerable<BusLineStation> BusLineStations { get => (IEnumerable<BusLineStation>)GetValue(BusLineStationsProperty); set => SetValue(BusLineStationsProperty, value); }
    }
}
