﻿using System;
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
        public BO.Station Station;

        public UpdateStationWindow(Station station)
        {
            InitializeComponent();
            Station = App.bl.GetStation(station.Key);
            updatingStation = station;
            grid1.DataContext = Station;
            beforeUpdateindex = MainWindow.stationsCollection.IndexOf(updatingStation);
        }
        private void updateStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Station.Key = updatingStation.Key;
                App.bl.UpdateStation(Station);
                MainWindow.stationsCollection[beforeUpdateindex] = PoBoAdapter.StationPoBoAdapter(App.bl.GetStation(Station.Key));
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
