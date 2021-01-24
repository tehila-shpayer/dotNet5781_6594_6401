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
        public BO.Station stationBO;

        public UpdateStationWindow(Station station)
        {
            InitializeComponent();
            stationBO = App.bl.GetStation(station.Key);
            updatingStation = station;
            grid1.DataContext = stationBO;
            beforeUpdateindex = MainWindow.stationsCollection.IndexOf(updatingStation);
            mainGrid.DataContext = MainWindow.Language;
        }

        #region Buttons
        private void updateStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                stationBO.Key = updatingStation.Key;
                App.bl.UpdateStation(stationBO);
                MainWindow.stationsCollection[beforeUpdateindex] = PoBoAdapter.StationPoBoAdapter(stationBO);
                MessageBox.Show($"Station {stationBO.Key} updated successfully.", "UPDATE STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BOInvalidInformationException ex)
            {
                MessageBox.Show("Can't update the station!\n" + ex.ToString(), "UPDATE STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't update the station!\n" + ex.Message, "UPDATE STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Input Validation
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllFieldsFull();
        }
        void AllFieldsFull()
        {
            if (namerTextBox.Text != "" && latitudeTextBox.Text != "" && longitudeTextBox.Text != "")
                updateButton.IsEnabled = true;
            else
                updateButton.IsEnabled = false;
        }
        #endregion
    }
}
