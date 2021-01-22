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
using System.Diagnostics;

namespace PL
{
    /// <summary>
    /// Interaction logic for TravelerPage.xaml
    /// </summary>
    public partial class TravelerPage : Page
    {
        public static ObservableCollection<BusInTravel> busesInTravelCollection = new ObservableCollection<BusInTravel>();
        Station currentStation = new Station();
        Stopwatch stopWatch;
        TimeSpan time;
        BO.Clock clock;
        double latePrecentage;
        public TravelerPage()
        {
            InitializeComponent();
            lbStations.DataContext = MainWindow.stationsCollection;
            lbStations.SelectedIndex = 0;
            List<string> OrderByString = new List<string> { "Order by key", "Order by name" };
            cbStations.DataContext = OrderByString;
            cbStations.SelectedIndex = 0;
            mainGrid.DataContext = MainWindow.Language;
            time = new TimeSpan();
            tbClock.DataContext = time;

            currentStation = lbStations.SelectedItem as Station;
            stopWatch = new Stopwatch();
        }

        private void stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbStations.SelectedIndex >= 0)
                StationInfoGrid.DataContext = MainWindow.stationsCollection.ElementAt(lbStations.SelectedIndex);
        }
        private void cbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }
        void Sort()
        {
            MainWindow.stationsCollection = new ObservableCollection<Station>(from s in App.bl.GetAllStationsOrderedBy(cbStations.SelectedItem.ToString())
                                                                              select PoBoAdapter.StationPoBoAdapter(s));
            lbStations.DataContext = MainWindow.stationsCollection;
        }
        public void TimeChange(Object sender, BO.ValueChangedEventArgs temp)
        {
            tbClock.DataContext = temp.Time;
            busesInTravelCollection.Clear();
            int i = 0;
            foreach (var lineTiming in App.bl.GetLineTimingsPerStation((lbStations.SelectedItem as Station).Key, temp.Time, latePrecentage))
            {
                i++;
                if (i > 5)
                    break;
                busesInTravelCollection.Add(PoBoAdapter.BusInTravelPoBoAdapter(lineTiming));
            }

            lvCommingLines.DataContext = busesInTravelCollection;
            BusInTravel lastBus = busesInTravelCollection.FirstOrDefault(bit => bit.TimeLeft == new TimeSpan(0, 0, 0));
            if (lastBus != null)
                spLastBus.DataContext = lastBus;
        }

        private void lbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            spLastBus.DataContext = null;
            if (lbStations.SelectedIndex >= 0)
                StationInfoGrid.DataContext = MainWindow.stationsCollection.ElementAt(lbStations.SelectedIndex);
        }

        private void SimulationButton_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            //if (simulationButtonContent.Text == "הפעל")
            latePrecentage = (double)rand.Next(85, 115) / (double)100;
            cbStations.IsEnabled = false;
            clock = new BO.Clock(new TimeSpan(0, 0, 0), 1);

            SimulationButton.Visibility = Visibility.Collapsed;
            StopButton.Visibility = Visibility.Visible;
            clock.TimeChanged += this.TimeChange;
            spLastBus.DataContext = null;
            //simulationButtonContent.Text = "עצור";
            tbHour.Visibility = Visibility.Collapsed;
            tbDots1.Visibility = Visibility.Collapsed;
            tbMinutes.Visibility = Visibility.Collapsed;
            tbDots2.Visibility = Visibility.Collapsed;
            tbSeconds.Visibility = Visibility.Collapsed;
            tbRate.Visibility = Visibility.Collapsed;
            tbClock.Visibility = Visibility.Visible;
            tblRate.Visibility = Visibility.Visible;
            time = new TimeSpan(int.Parse(tbHour.Text), int.Parse(tbMinutes.Text), int.Parse(tbSeconds.Text));
            tbClock.DataContext = time;
            clock.startTime = time;
            clock.Time = time;
            App.bl.StartSimulator(clock, time, int.Parse(tbRate.Text), (lbStations.SelectedItem as Station).Key);
            //lvCommingLines.DataContext = App.bl.GetLineTimingsPerStation((lbStations.SelectedItem as Station).Key, new TimeSpan(8,0,0));
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            cbStations.IsEnabled = true;
            spLastBus.DataContext = null;
            StopButton.Visibility = Visibility.Collapsed;
            SimulationButton.Visibility = Visibility.Visible;
            clock.TimeChanged -= this.TimeChange;
            tbHour.Text = tbClock.Text.Substring(0, 2);
            tbMinutes.Text = tbClock.Text.Substring(3, 2);
            tbSeconds.Text = tbClock.Text.Substring(6, 2);
            tbHour.Visibility = Visibility.Visible;
            tbDots1.Visibility = Visibility.Visible;
            tbMinutes.Visibility = Visibility.Visible;
            tbDots2.Visibility = Visibility.Visible;
            tbSeconds.Visibility = Visibility.Visible;
            tbRate.Visibility = Visibility.Visible;
            tbClock.Visibility = Visibility.Collapsed;
            tblRate.Visibility = Visibility.Collapsed;
            App.bl.StopSimulator();

        }
    }
}


