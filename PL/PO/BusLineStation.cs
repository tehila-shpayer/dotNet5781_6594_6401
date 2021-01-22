using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace PL
{
    public class BusLineStation :DependencyObject
    {
        static readonly DependencyProperty BusLineKeyProperty = DependencyProperty.Register("BusLineKey", typeof(int), typeof(BusLineStation));
        static readonly DependencyProperty StationKeyProperty = DependencyProperty.Register("StationKey", typeof(int), typeof(BusLineStation));
        static readonly DependencyProperty PositionProperty = DependencyProperty.Register("PositionKey", typeof(int), typeof(BusLineStation));
        static readonly DependencyProperty DistanceFromLastStationMetersProperty = DependencyProperty.Register("DistanceFromLastStationMeters", typeof(int), typeof(BusLineStation));
        static readonly DependencyProperty TravelTimeFromLastStationMinutesProperty = DependencyProperty.Register("TravelTimeFromLastStationMinutesKey", typeof(int), typeof(BusLineStation));
        static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(String), typeof(BusLineStation));

        public int BusLineKey { get => (int)GetValue(BusLineKeyProperty); set => SetValue(BusLineKeyProperty, value); }
        public int StationKey { get => (int)GetValue(StationKeyProperty); set => SetValue(StationKeyProperty, value); }
        public int Position { get => (int)GetValue(PositionProperty); set => SetValue(PositionProperty, value); }
        public int DistanceFromLastStationMeters { get => (int)GetValue(DistanceFromLastStationMetersProperty); set => SetValue(DistanceFromLastStationMetersProperty, value); }
        public int TravelTimeFromLastStationMinutes { get => (int)GetValue(TravelTimeFromLastStationMinutesProperty); set => SetValue(TravelTimeFromLastStationMinutesProperty, value); }
        public String Name { get => (String)GetValue(NameProperty); set => SetValue(NameProperty, value); }
        public bool IsFirstStation { get; set; }
        public String ShowNameKey { get { return $"{Name} {StationKey}"; } }
        public bool IsSource { get; set; }
        public bool IsDestination { get; set; }
    }
}
