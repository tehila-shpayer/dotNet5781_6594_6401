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
    public partial class BusLinePage : Page
    {
        public BusLinePage()
        {
            
            InitializeComponent();
            //MainWindow.InitializeCollections();
            lbBusLines.DataContext = MainWindow.busLinesCollection;
            List<string> AreasString = new List<string> { "All", "Center", "General", "Hifa", "Jerusalem", "North", "South", "TelAviv", "YehudaAndShomron" };
            List<string> OrderByString = new List<string> { "Order by key", "Order by number", "Order by area" };
            areas.DataContext = AreasString;
            areas.SelectedIndex = 0;
            cbBusLines.DataContext = OrderByString;
            cbBusLines.SelectedIndex = 0;
            mainGrid.DataContext = MainWindow.Language;
        }
        private void lbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbBusLines.SelectedIndex >= 0)
                BusLineInfoGrid.DataContext = lbBusLines.SelectedItem;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter();
        }
        void filter()
        {
            foreach (var item in MainWindow.busLinesCollection)
            {
                ListBoxItem bus = (ListBoxItem)lbBusLines.ItemContainerGenerator.ContainerFromItem(item);
                //ContainerFromItem(item);
                //String searchS = searchBox.Text;
                //int num = searchS.Length;

                if (bus != null)
                {
                    if (checkBusLineArea(item) && checkBusLineNumberInSearchBox(item))
                    {
                        bus.Visibility = Visibility.Visible;
                    }
                    else
                        bus.Visibility = Visibility.Collapsed;
                }
            }
        }
        bool checkBusLineNumberInSearchBox(BusLine item)
        {
            String searchS = searchBox.Text;
            int num = searchS.Length;
            return (num <= item.LineNumber.ToString().Length
                   && searchS == (item as BusLine).LineNumber.ToString().Substring(0, num));
        }
        bool checkBusLineArea(BusLine item)
        {
            int selectedArea = areas.SelectedIndex;
            return (selectedArea == 0 || (selectedArea - 1) == (int)item.Area);
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            BusLine busLine = MainWindow.busLinesCollection[lbBusLines.SelectedIndex];
            int key = busLine.Key;
            try
            {
                MessageBoxResult mbResult = MessageBox.Show($"Are you sure you want to delete \nbus line of key {key}?", "DELETE BUS", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (mbResult == MessageBoxResult.Yes)
                {
                    App.bl.DeleteBusLine(key);
                    MainWindow.busLinesCollection.Remove(busLine);
                    MessageBox.Show($"Bus of key {key} was deleted.", "DELETE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);

                }
                else
                    MessageBox.Show($"Operation was canceled.", "DELETE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);

            }
            catch (BO.BOArgumentNotFoundException ex)
            {
                MessageBox.Show($"Bus of key {key} was not found.", "DELETE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            int index = lbBusLines.Items.Count;
            AddBusLineWindow addBusLineWindow = new AddBusLineWindow();
            addBusLineWindow.ShowDialog();
            MainWindow.InitializeBusLines();
            MainWindow.InitializeStations();
            lbBusLines.DataContext = MainWindow.busLinesCollection;
            //Sort(index);
            lbBusLines.SelectedIndex = index;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            int index = lbBusLines.SelectedIndex;
            try
            {
                UpdateBusLineWindow update = new UpdateBusLineWindow(MainWindow.busLinesCollection[lbBusLines.SelectedIndex]);
                update.ShowDialog();
                //Sort(lbBusLines.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Please choose a bus line!", "UPDATE BUS LINE MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.No);
            }
            lbBusLines.SelectedIndex = index;
        }

        private void areas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter();
        }
        //void ShowByArea()
        //{
        //    foreach (var item in MainWindow.busLinesCollection)
        //    {
        //        ListBoxItem bus = lbBusLines.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
        //        int selectedArea = areas.SelectedIndex;
        //        //Show only buses from the same area
        //        if (bus != null)
        //        {
        //            if (selectedArea == 0 || (selectedArea - 1) == (int)item.Area)
        //                bus.Visibility = Visibility.Visible;
        //            else
        //                bus.Visibility = Visibility.Collapsed;
        //        }
        //    }
        //}

        private void addStationButton_Click(object sender, RoutedEventArgs e)
        {
            int index = lbBusLines.SelectedIndex;
            var selectedStation = (sender as Button).DataContext as BusLineStation;
            AddBusLineStation addBusLineStation = new AddBusLineStation(selectedStation);
            addBusLineStation.ShowDialog();
            MainWindow.InitializeBusLines();
            MainWindow.InitializeStations();
            lbBusLines.DataContext = MainWindow.busLinesCollection;
            lbBusLines.SelectedIndex = index;
        }

        private void deleteStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = lbBusLines.SelectedIndex;
                var selectedStation = (sender as Button).DataContext as BusLineStation;
                App.bl.DeleteStationFromLine(selectedStation.BusLineKey, selectedStation.StationKey);
                MainWindow.InitializeBusLines();
                lbBusLines.DataContext = MainWindow.busLinesCollection;
                lbBusLines.SelectedIndex = index;
                MessageBox.Show($"Station {selectedStation.StationKey} was successfully\ndeleted from line {selectedStation.BusLineKey}", "ADD STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BOArgumentNotFoundException ex)
            {
                MessageBox.Show("Cant delete station\n" + ex.ToString(), "DELETE BUS LINE MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch(BO.BOInvalidInformationException ex)
            {
                MessageBox.Show("Cant delete station\n" + ex.ToString(), "DELETE BUS LINE MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show($"Please choose a bus line!", "DELETE BUS LINE MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.No);
            }
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }
        void Sort()
        {
            //MainWindow.busLinesCollection.Clear();
            //foreach (BO.BusLine bus in App.bl.GetAllBusLinesOrderedBy(cbBusLines.SelectedItem.ToString()))
            //    MainWindow.busLinesCollection.Add(PoBoAdapter.BusLinePoBoAdapter(bus));
            ////ShowByArea();
            //lbBusLines.SelectedIndex = index;
            MainWindow.busLinesCollection = new ObservableCollection<BusLine>(from bl in App.bl.GetAllBusLinesOrderedBy(cbBusLines.SelectedItem.ToString())
                                                                              select PoBoAdapter.BusLinePoBoAdapter(bl));
            lbBusLines.DataContext = MainWindow.busLinesCollection;
            filter();
        }
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            int index = lbBusLines.SelectedIndex;
            var selectedStation = (sender as Button).DataContext as BusLineStation;
            UpdateConsecutiveStations updateConsecutiveStations = new UpdateConsecutiveStations(selectedStation);
            updateConsecutiveStations.ShowDialog();
            lbBusLines.DataContext = MainWindow.busLinesCollection;
            lbBusLines.SelectedIndex = index;
        }
    }
}
