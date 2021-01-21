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
    /// Interaction logic for PlanJourneyPage.xaml
    /// </summary>
    public partial class PlanJourneyPage : Page
    {
        public PlanJourneyPage()
        {
            InitializeComponent();
            cbSource.DataContext = MainWindow.stationsCollection;
            cbDestination.DataContext = MainWindow.stationsCollection;
            cbSource.DisplayMemberPath = "  ShowNameKey  ";
            cbDestination.DisplayMemberPath = "  ShowNameKey  ";
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (cbSource.SelectedItem == cbDestination.SelectedItem)
            {
                MessageBox.Show($"Please choose different stations!", "SEARCH MESSAGE", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Station source = cbSource.SelectedItem as Station;
            Station destination = cbDestination.SelectedItem as Station;
            BO.Station s1 = new BO.Station();
            BO.Station s2 = new BO.Station();
            source.Clone(s1);
            destination.Clone(s2);
            IEnumerable<BO.BusLine> busLines = App.bl.FindRoutes(s1, s2);
            lbLines.DataContext = (from l in App.bl.FindRoutes(s1, s2)
                                  select PoBoAdapter.PresentBusLineForStationPoBoAdapter(l)).ToList();
        }

        private void cbSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDestination.SelectedItem != null)
                searchButton.IsEnabled = true;
        }

        private void cbDestination_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSource.SelectedItem != null)
                searchButton.IsEnabled = true;
        }
    }
}
