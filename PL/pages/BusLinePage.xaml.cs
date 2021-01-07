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
            busLines.DataContext = MainWindow.busLinesCollection;
            List<string> AreasString = new List<string> { "General", "Jerusalem", "Center", "North", "South", "Hifa", "TelAviv", "YehudaAndShomron" };
            areas.DataContext = AreasString;
        }

        private void busLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (busLines.SelectedIndex >= 0)
                busLineStationdlb.DataContext = MainWindow.busLinesCollection.ElementAt(busLines.SelectedIndex).BusLineStations;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var item in MainWindow.busLinesCollection)
            {
                ListBoxItem bus = (ListBoxItem)busLines.ItemContainerGenerator.ContainerFromItem(item);
                String searchS = searchBox.Text;
                int num = searchS.Length;
                //Show only buses which there license number have the typed perfix
                if ((num <= item.LineNumber.ToString().Length && searchS == (item as BusLine).LineNumber.ToString().Substring(0, num)))
                {
                    bus.Visibility = Visibility.Visible;
                }
                else
                    bus.Visibility = Visibility.Collapsed;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            BusLine busLine = MainWindow.busLinesCollection[busLines.SelectedIndex];
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
            AddBus addBus = new AddBus();
            addBus.ShowDialog();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateBusLineWindow update = new UpdateBusLineWindow(MainWindow.busLinesCollection[busLines.SelectedIndex]);
            update.ShowDialog();
        }

        private void areas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in MainWindow.busLinesCollection)
            {
                ListBoxItem bus = (ListBoxItem)busLines.ItemContainerGenerator.ContainerFromItem(item);
                int selectedArea = areas.SelectedIndex;
                //Show only buses from the same area
                if (selectedArea == (int)item.Area)
                {
                    bus.Visibility = Visibility.Visible;
                }
                else
                    bus.Visibility = Visibility.Collapsed;
            }
        }
    }
}
