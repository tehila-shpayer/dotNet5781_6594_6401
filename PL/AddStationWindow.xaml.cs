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
        public BO.Station stationBO;
        public AddStationWindow()
        {
            InitializeComponent();
            stationBO = new BO.Station();
            grid1.DataContext = stationBO;
        }

        private void namerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllFieldsFull();
        }

        private void latitudeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllFieldsFull();
        }

        private void longitudeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllFieldsFull();
        }
        void AllFieldsFull()
        {
            if (namerTextBox.Text != "" && latitudeTextBox.Text != "" && longitudeTextBox.Text != "")
                addButton.IsEnabled = true;
            else
                addButton.IsEnabled = false;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                stationBO.Key = App.bl.AddStation(stationBO);
                stationBO = App.bl.GetStation(stationBO.Key);
                MainWindow.stationsCollection.Add(PoBoAdapter.StationPoBoAdapter(stationBO));
                MessageBox.Show($"Station {stationBO.Key} added successfully!", "ADD STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't add the station!\n" + ex.ToString(), "ADD STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
