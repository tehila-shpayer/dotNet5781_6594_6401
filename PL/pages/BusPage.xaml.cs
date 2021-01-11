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
    public partial class BusPage : Page
    {
        public BusPage()
        {
            InitializeComponent();
            MainWindow.InitializeBuses();
            lbBuses.DataContext = MainWindow.busesCollection;
            List<string> OrderByString = new List<string> { "Order by license number", "Order by status" };
            cbBuses.DataContext = OrderByString;
            cbBuses.SelectedIndex = 0;
        }

        private void stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbBuses.SelectedIndex >= 0)
                BusInfoGrid.DataContext = MainWindow.busesCollection.ElementAt(lbBuses.SelectedIndex);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var item in MainWindow.busesCollection)
            {
                ListBoxItem bus = (ListBoxItem)lbBuses.ItemContainerGenerator.ContainerFromItem(item);
                String searchS = searchBox.Text;
                int num = searchS.Length;
                //Show only buses which there license number have the typed perfix
                //if ((num <= item.LineNumber.ToString().Length && searchS == (item as BusLine).LineNumber.ToString().Substring(0, num)))
                if (bus != null)
                {
                    if (num <= item.LicenseNumber.Length && searchS == item.LicenseNumber.Substring(0, num))
                    {
                        bus.Visibility = Visibility.Visible;
                    }
                    else
                        bus.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            int index = lbBuses.SelectedIndex;
            try
            {
                UpdateBusWindow updateBusWindow = new UpdateBusWindow(MainWindow.busesCollection[lbBuses.SelectedIndex]);
                updateBusWindow.ShowDialog();
                Sort();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Please choose a bus!", "UPDATE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.No);
            }
            lbBuses.SelectedIndex = index;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                Bus bus = MainWindow.busesCollection[lbBuses.SelectedIndex];
                string key = bus.LicenseNumber;
                MessageBoxResult mbResult = MessageBox.Show($"Are you sure you want to delete bus {key}?", "DELETE BUS MESSAGE", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (mbResult == MessageBoxResult.Yes)
                {
                    App.bl.DeleteBus(key);
                    MainWindow.busesCollection.Remove(bus);
                    MessageBox.Show($"Bus {key} was deleted.", "DELETE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);

                }
                else
                    MessageBox.Show($"Delete operation was canceled.", "DELETE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);

            }
            catch (BO.BOArgumentNotFoundException ex)
            {
                MessageBox.Show($"{ex.Message}", "DELETE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.No);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Please choose a bus!", "DELETE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.No);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow addBusWindow = new AddBusWindow();
            addBusWindow.ShowDialog();
            Sort();
        }

        private void cbBuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }
        void Sort()
        {
            var collection = new ObservableCollection<Bus>();
            switch (cbBuses.SelectedItem.ToString())
            {
                case "Order by license number":
                    foreach (Bus bus in MainWindow.busesCollection.OrderBy(b => int.Parse(b.LicenseNumber)))
                        collection.Add(bus);
                    break;
                case "Order by status":
                    foreach (Bus bus in MainWindow.busesCollection.OrderBy(b => b.Status.ToString()))
                        collection.Add(bus); break;
                default: break;
            }
            MainWindow.busesCollection.Clear();
            foreach (Bus bus in collection)
                MainWindow.busesCollection.Add(bus);
        }
    }
}
