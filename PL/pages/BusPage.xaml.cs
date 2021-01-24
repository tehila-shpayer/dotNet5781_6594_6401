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
            mainGrid.DataContext = MainWindow.Language;
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
        private void cbBuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }
        void Sort()
        {
            MainWindow.busesCollection = new ObservableCollection<Bus>(from b in App.bl.GetAllBusesOrderedBy(cbBuses.Text)
                                                                       select PoBoAdapter.BusPoBoAdapter(b));
            lbBuses.DataContext = MainWindow.busesCollection;
        }

        #region Buttons
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

        private void TreatButton_Click(object sender, RoutedEventArgs e)
        {
            var bus = MainWindow.busesCollection[lbBuses.SelectedIndex];
            BO.Bus busBO = App.bl.GetBus(bus.LicenseNumber);
            busBO.KM += bus.BeforeTreatKM;
            busBO.BeforeTreatKM = 0;
            busBO.LastTreatment = DateTime.Now;
            App.bl.UpdateBus(busBO);
            MainWindow.busesCollection[lbBuses.SelectedIndex] = PoBoAdapter.BusPoBoAdapter(busBO);
        }

        private void RefuelButton_Click(object sender, RoutedEventArgs e)
        {
            App.bl.UpdateBus((lbBuses.SelectedItem as Bus).LicenseNumber, b => b.Fuel = 1200);
            MainWindow.busesCollection[lbBuses.SelectedIndex].Fuel = 1200;
        }
        #endregion
    }
}
