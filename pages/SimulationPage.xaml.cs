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
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulationPage.xaml
    /// </summary>
    public partial class SimulationPage : Page
    {
        //BackgroundWorker worker;
        public static ObservableCollection<BusInTravel> busesInTravelCollection = new ObservableCollection<BusInTravel>();
        Station currentStation = new Station();
        Stopwatch stopWatch;
        TimeSpan time;
        BO.Clock clock;
        double latePrecentage = 1;
        public SimulationPage()
        {
            InitializeComponent();
            //clock = new BO.Clock(new TimeSpan(0, 0, 0), 1);
            //clock.TimeChanged += this.TimeChange;

            lbStations.DataContext = MainWindow.stationsCollection;
            lbStations.SelectedIndex = 0;
            //lvCommingLines.DataContext = new List<BO.BusInTravel>();
           // lvCommingLines.DataContext = busesInTravelCollection;

            time = new TimeSpan();
            tbClock.DataContext = time;

            currentStation = lbStations.SelectedItem as Station;
            stopWatch = new Stopwatch();
            //worker = new BackgroundWorker();
            //worker.DoWork += Worker_DoWork;
            //worker.ProgressChanged += Worker_ProgressChanged;
            //worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //worker.WorkerReportsProgress = true;
            //worker.WorkerSupportsCancellation = true;
            mainGrid.DataContext = MainWindow.Language;
            
        }
        public void TimeChange(Object sender, BO.ValueChangedEventArgs temp)
        {
            tbClock.DataContext = temp.Time;
            busesInTravelCollection.Clear();
            foreach (var lineTiming in App.bl.GetLineTimingsPerStation((lbStations.SelectedItem as Station).Key, temp.Time, latePrecentage))
                busesInTravelCollection.Add(PoBoAdapter.BusInTravelPoBoAdapter(lineTiming));
            lvCommingLines.DataContext = busesInTravelCollection;
        }
        //private void Worker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    //App.bl.StartSimulator(time, 1, UpdateTime);
        //    //App.bl.StartSimulator(time, 1, (lbStations.SelectedItem as Station).Key);
        //}
        //private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    tbClock.DataContext = time;
        //}
        //private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    App.bl.StopSimulator();
        //}

        private void lbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbStations.SelectedIndex >= 0)
                StationInfoGrid.DataContext = MainWindow.stationsCollection.ElementAt(lbStations.SelectedIndex);
        }

        private void SimulationButton_Click(object sender, RoutedEventArgs e)
        {
            if (SimulationButton.Content.ToString() == "הפעל סימולציה")
            {
                clock = new BO.Clock(new TimeSpan(0, 0, 0), 1);
                clock.TimeChanged += this.TimeChange;

                SimulationButton.Content = "עצור סימולציה";
                tbHour.Visibility = Visibility.Collapsed;
                tbMinutes.Visibility = Visibility.Collapsed;
                tbSeconds.Visibility = Visibility.Collapsed;

                time = new TimeSpan(int.Parse(tbHour.Text), int.Parse(tbMinutes.Text), int.Parse(tbSeconds.Text));              
                tbClock.DataContext = time;
                tbClock.Visibility = Visibility.Visible;
                clock.startTime = time;
                clock.Time = time;
                App.bl.StartSimulator(clock, time, int.Parse(tbRate.Text), (lbStations.SelectedItem as Station).Key);
                
            }
            else
            {
                SimulationButton.Content = "הפעל סימולציה";
                clock.TimeChanged -= this.TimeChange;
                tbHour.Visibility = Visibility.Visible;
                tbMinutes.Visibility = Visibility.Visible;
                tbSeconds.Visibility = Visibility.Visible;
                tbClock.Visibility = Visibility.Collapsed;
                App.bl.StopSimulator();
            }
            //stopWatch.Restart();
            //isTimerRun = true;
            //worker.RunWorkerAsync();
            //lvCommingLines.DataContext = App.bl.GetLineTimingsPerStation((lbStations.SelectedItem as Station).Key, new TimeSpan(8,0,0));
        }
        //public void UpdateTime(TimeSpan ts)
        //{
        //    worker.ReportProgress(0);
        //}
    }
}
