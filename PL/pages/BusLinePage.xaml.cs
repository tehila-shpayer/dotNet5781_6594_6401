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
    }
}
