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
    /// Interaction logic for UpdateStationWindow.xaml
    /// </summary>
    public partial class UpdateStationWindow : Window
    {
        public Station updatingStation;
        public int beforeUpdateindex;

        public UpdateStationWindow(Station station)
        {
            InitializeComponent();
            updatingStation = station;
            grid1.DataContext = station;
            beforeUpdateindex = MainWindow.stationsCollection.IndexOf(updatingStation);
        }
        private void updateStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Station Station = new BO.Station();
                Station.Key = updatingStation.Key;
                Station = App.bl.GetStation(Station.Key);
                Station.Latitude = double.Parse(latitudeTextBox.Text);
                Station.Longitude = double.Parse(longitudeTextBox.Text);
                Station.Name = namerTextBox.Text;
                App.bl.UpdateStation(Station);
                updatingStation = PoBoAdapter.StationPoBoAdapter(App.bl.GetStation(Station.Key));
                //Station StationPO = PoBoAdapter.StationPoBoAdapter(Station);
                MainWindow.stationsCollection[beforeUpdateindex] = updatingStation;
                MessageBox.Show($"Station updated successfully.", "UPDATE STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BOInvalidInformationException ex)
            {
                MessageBox.Show($"Can't update station.", "UPDATE STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "UPDATE STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
