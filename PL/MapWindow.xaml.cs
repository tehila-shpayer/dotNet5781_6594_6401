using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Device.Location;

namespace PL
{
    /// <summary>
    /// Interaction logic for MapWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        public MapWindow(Station station)
        {
            InitializeComponent();
            map.Center = new Microsoft.Maps.MapControl.WPF.Location(station.Latitude, station.Longitude);
            mapCanves.DataContext = map.Center;
            //wbMaps.Source = $"tps://www.google.com/maps/search/?api=1&query=+{station.Latitude}+,+{station.Longitude}";
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
