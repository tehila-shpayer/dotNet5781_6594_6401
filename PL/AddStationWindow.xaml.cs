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

namespace PL
{
    /// <summary>
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        public AddStationWindow()
        {
            InitializeComponent();
        }

        private void addStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Station station = new BO.Station();
                station.Latitude = int.Parse(latitudeTextBox.Text);
                station.Longitude = int.Parse(longitudeTextBox.Text);
                station.Name = namerTextBox.Text;
                station.Key = App.bl.AddStation(station);
                station = App.bl.GetStation(station.Key);
                MainWindow.stationsCollection.Add(PoBoAdapter.StationPoBoAdapter(station));
                MessageBox.Show($"Station added successfully!", "ADD STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BOInvalidInformationException ex)
            {
                MessageBox.Show($"Can't add station line. Invalid information", "ADD STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
