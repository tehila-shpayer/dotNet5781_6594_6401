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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for BusLinePage.xaml
    /// </summary>
    public partial class StationPage : Page
    {
        public StationPage()
        {

            //MainWindow.stationsCollection.Clear();
            //foreach (BO.Station s in App.bl.GetAllStations())
            //    MainWindow.stationsCollection.Add(PoBoAdapter.StationPoBoAdapter(s));
            InitializeComponent();
            lbStations.DataContext = MainWindow.stationsCollection;
            List<string> OrderByString = new List<string> { "Order by key", "Order by name" };
            cbStations.DataContext = OrderByString;
            cbStations.SelectedIndex = 0;

        }

        private void stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbStations.SelectedIndex >= 0)
                StationInfoGrid.DataContext = MainWindow.stationsCollection.ElementAt(lbStations.SelectedIndex);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var item in MainWindow.stationsCollection)
            {
                ListBoxItem station = (ListBoxItem)lbStations.ItemContainerGenerator.ContainerFromItem(item);
                String searchS = searchBox.Text;
                int num = searchS.Length;
                //Show only buses which there license number have the typed perfix
                //if ((num <= item.LineNumber.ToString().Length && searchS == (item as BusLine).LineNumber.ToString().Substring(0, num)))
                if (station != null)
                {
                    if (num <= item.Name.Length && searchS == item.Name.Substring(0, num))
                    {
                        station.Visibility = Visibility.Visible;
                    }
                    else
                        station.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateStationWindow updateStationWindow = new UpdateStationWindow(MainWindow.stationsCollection[lbStations.SelectedIndex]);
                updateStationWindow.ShowDialog();
                Sort();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Please choose a bus line!", "UPDATE STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.No);
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Station station = MainWindow.stationsCollection[lbStations.SelectedIndex];
            int key = station.Key;
            try
            {
                MessageBoxResult mbResult = MessageBox.Show($"Are you sure you want to delete \nstation of key {key}?", "DELETE STATION MESSAGE", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (mbResult == MessageBoxResult.Yes)
                {
                    App.bl.DeleteStation(key);
                    MainWindow.stationsCollection.Remove(station);
                    MessageBox.Show($"Station of key {key} was deleted.", "DELETE STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);

                }
                else
                    MessageBox.Show($"Delete of station was canceled.", "DELETE STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);

            }
            catch (BO.BOArgumentNotFoundException ex)
            {
                MessageBox.Show($"{ex.Message}", "DELETE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Please choose a station!", "DELETE STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.No);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddStationWindow addStationWindow = new AddStationWindow();
            addStationWindow.ShowDialog();
            Sort();
        }

        private void cbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }
        void Sort()
        {
            var collection = new ObservableCollection<Station>();
            switch (cbStations.SelectedItem.ToString())
            {
                case "Order by key":
                    foreach (Station station in MainWindow.stationsCollection.OrderBy(s => s.Key))
                        collection.Add(station);
                    break;
                case "Order by name":
                    foreach (Station station in MainWindow.stationsCollection.OrderBy(s => s.Name))
                        collection.Add(station); break;
                default: break;
            }
            MainWindow.stationsCollection.Clear();
            foreach (Station station in collection)
                MainWindow.stationsCollection.Add(station);
        }

        private void mapButton_Click(object sender, RoutedEventArgs e)
        {
            MapWindow mapWindow = new MapWindow();
            mapWindow.Show();
        }
    }
}
