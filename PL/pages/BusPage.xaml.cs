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
            lbBuses.DataContext = MainWindow.busesCollection;
            
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
            UpdateBusWindow updateBusWindow = new UpdateBusWindow(MainWindow.busesCollection[lbBuses.SelectedIndex]);
            updateBusWindow.ShowDialog();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Bus bus = MainWindow.busesCollection[lbBuses.SelectedIndex];
            string key = bus.LicenseNumber;
            try
            {
                MessageBoxResult mbResult = MessageBox.Show($"Are you sure you want to delete \nbus of key {key}?", "DELETE BUS MESSAGE", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (mbResult == MessageBoxResult.Yes)
                {
                    App.bl.DeleteBus(key);
                    MainWindow.busesCollection.Remove(bus);
                    MessageBox.Show($"Bus of key {key} was deleted.", "DELETE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);

                }
                else
                    MessageBox.Show($"Delete of bus was canceled.", "DELETE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);

            }
            catch (BO.BOArgumentNotFoundException ex)
            {
                MessageBox.Show($"{ex.Message}", "DELETE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.No);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow addBusWindow = new AddBusWindow();
            addBusWindow.ShowDialog();
        }
    }
}
