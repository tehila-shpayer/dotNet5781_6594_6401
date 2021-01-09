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
            lbBusLines.DataContext = MainWindow.busLinesCollection;
            List<string> AreasString = new List<string> { "All", "General", "Jerusalem", "Center", "North", "South", "Hifa", "TelAviv", "YehudaAndShomron" };
            List<string> OrderByString = new List<string> { "Order by key", "Order by number", "Order by area"};
            areas.DataContext = AreasString;
            cbBusLines.DataContext = OrderByString;
            //cbBusLines.SelectedIndex = 0;
        }

        private void lbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbBusLines.SelectedIndex >= 0)
                BusLineInfoGrid.DataContext = MainWindow.busLinesCollection.ElementAt(lbBusLines.SelectedIndex);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            //for (int i=0; i< MainWindow.busLinesCollection.Count();i++)
            //{
            //    BusLine item = MainWindow.busLinesCollection[i];
            //    ListBoxItem bus = (ListBoxItem)lbBusLines.ItemContainerGenerator.ContainerFromIndex(i);
            //    String searchS = searchBox.Text;
            //    int num = searchS.Length;
            //    //Show only buses which there license number have the typed perfix
            //    if (bus != null)
            //    {
            //        if ((num <= item.LineNumber.ToString().Length && searchS == (item as BusLine).LineNumber.ToString().Substring(0, num)))
            //        {
            //            bus.Visibility = Visibility.Visible;
            //        }
            //        else
            //            bus.Visibility = Visibility.Collapsed;
            //    }
            //}
            foreach (var item in MainWindow.busLinesCollection)
            {
                ListBoxItem bus = (ListBoxItem)lbBusLines.ItemContainerGenerator.ContainerFromItem(item);
                String searchS = searchBox.Text;
                int num = searchS.Length;
                //Show only buses which there license number have the typed perfix
                if (bus != null)
                {
                    if ((num <= item.LineNumber.ToString().Length && searchS == (item as BusLine).LineNumber.ToString().Substring(0, num)))
                    {
                        bus.Visibility = Visibility.Visible;
                    }
                    else
                        bus.Visibility = Visibility.Collapsed;
                }
            }
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
            AddBusLineWindow addBusLineWindow = new AddBusLineWindow();
            addBusLineWindow.ShowDialog();
            Sort();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateBusLineWindow update = new UpdateBusLineWindow(MainWindow.busLinesCollection[lbBusLines.SelectedIndex]);
            update.ShowDialog();
        }

        private void areas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in MainWindow.busLinesCollection)
            {
                ListBoxItem bus = (ListBoxItem)lbBusLines.ItemContainerGenerator.ContainerFromItem(item);
                int selectedArea = areas.SelectedIndex;
                //Show only buses from the same area
                if (selectedArea == 0 || (selectedArea - 1) == (int)item.Area)
                {
                    bus.Visibility = Visibility.Visible;
                }
                else
                    bus.Visibility = Visibility.Collapsed;
            }
        }

        private void addStationButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedStation = (sender as Button).DataContext as BusLineStation;
            AddBusLineStation addBusLineStation = new AddBusLineStation(selectedStation);
            addBusLineStation.ShowDialog();
        }

        private void deleteStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = 0;
                var selectedStation = (sender as Button).DataContext as BusLineStation;
                BO.BusLine busLineBO = App.bl.GetBusLine(selectedStation.BusLineKey);
                BusLine busLinePO = PoBoAdapter.BusLinePoBoAdapter(busLineBO);
                foreach (BusLine bl in MainWindow.busLinesCollection)
                {
                    if (bl.Key == busLinePO.Key)
                    {
                        index = MainWindow.busLinesCollection.IndexOf(bl);
                        break;
                    }
                }
                App.bl.DeleteStationFromLine(selectedStation.BusLineKey, selectedStation.StationKey);
                busLineBO = App.bl.GetBusLine(selectedStation.BusLineKey);
                busLinePO = PoBoAdapter.BusLinePoBoAdapter(busLineBO);
                MainWindow.busLinesCollection[index] = busLinePO;
                MessageBox.Show($"Station {selectedStation.StationKey} was successfully\ndeleted from line {selectedStation.BusLineKey}", "ADD STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }
        void Sort()
        {
            var collection = new ObservableCollection<BusLine>();
            switch (cbBusLines.SelectedItem.ToString())
            {
                case "Order by key":
                    foreach (BusLine bus in MainWindow.busLinesCollection.OrderBy(bl => bl.Key))
                        collection.Add(bus);
                    break;
                case "Order by number":
                    foreach (BusLine bus in MainWindow.busLinesCollection.OrderBy(bl => bl.LineNumber))
                        collection.Add(bus); break;
                case "Order by area":
                    foreach (BusLine bus in MainWindow.busLinesCollection.OrderBy(bl => bl.Area.ToString()))
                        collection.Add(bus); break;
                default: break;
            }
            MainWindow.busLinesCollection.Clear();
            foreach (BusLine bus in collection)
                MainWindow.busLinesCollection.Add(bus);
        }
    }
}
