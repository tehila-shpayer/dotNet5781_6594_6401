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
            //lbStations.SelectedIndex = 0;
        }
        public void TimeChange(Object sender, BO.ValueChangedEventArgs temp)
        {
            tbClock.DataContext = temp.Time;
            busesInTravelCollection.Clear();
            int i = 0;
            foreach (var lineTiming in App.bl.GetLineTimingsPerStation((lbStations.SelectedItem as Station).Key, temp.Time))
            {
                i++;
                if (i > 5)
                    break;
                busesInTravelCollection.Add(PoBoAdapter.BusInTravelPoBoAdapter(lineTiming));
            }

            lvCommingLines.DataContext = busesInTravelCollection;
            BusInTravel lastBus = busesInTravelCollection.FirstOrDefault(bit => bit.TimeLeft == new TimeSpan(0, 0, 0));
            if (lastBus != null)
                LastBusGrid.DataContext = lastBus;
        }

        private void lbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //LastBusGrid.DataContext = null;
            if (lbStations.SelectedIndex >= 0)
                StationInfoGrid.DataContext = MainWindow.stationsCollection.ElementAt(lbStations.SelectedIndex);
        }

        private void SimulationButton_Click(object sender, RoutedEventArgs e)
        {
            if (simulationButtonContent.Text == "הפעל")
            {
                cbStations.IsEnabled = false;
                clock = new BO.Clock(new TimeSpan(0, 0, 0), 1);
                clock.TimeChanged += this.TimeChange;
                //LastBusGrid.DataContext = null;
                simulationButtonContent.Text = "עצור";
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

            }
            else
            {
                cbStations.IsEnabled = true;
                //LastBusGrid.DataContext = null;
                simulationButtonContent.Text = "הפעל";
                clock.TimeChanged -= this.TimeChange;
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
            //stopWatch.Restart();
            //isTimerRun = true;
            //worker.RunWorkerAsync();
            //lvCommingLines.DataContext = App.bl.GetLineTimingsPerStation((lbStations.SelectedItem as Station).Key, new TimeSpan(8,0,0));
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            cbStations.IsEnabled = true;
            //LastBusGrid.DataContext = null;
            simulationButtonContent.Text = "הפעל";
            clock.TimeChanged -= this.TimeChange;
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


