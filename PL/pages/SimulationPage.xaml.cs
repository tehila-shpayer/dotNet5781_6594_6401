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
        BackgroundWorker worker;
        Station currentStation = new Station();
        Stopwatch stopWatch;
        TimeSpan time;
        public SimulationPage()
        {
            InitializeComponent();
            lbStations.DataContext = MainWindow.stationsCollection;
            lbStations.SelectedIndex = 0;
            lvCommingLines.DataContext = new List<BO.BusInTravel>();
            time = new TimeSpan();
            tbClock.DataContext = time;
            currentStation = lbStations.SelectedItem as Station;
            stopWatch = new Stopwatch();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            mainGrid.DataContext = MainWindow.Language;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            App.bl.StopSimulator();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tbClock.DataContext = time;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            App.bl.StartSimulator(time, 1, UpdateTime);
        }

        private void lbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbStations.SelectedIndex >= 0)
                StationInfoGrid.DataContext = MainWindow.stationsCollection.ElementAt(lbStations.SelectedIndex);
        }

        private void SimulationButton_Click(object sender, RoutedEventArgs e)
        {
            if (SimulationButton.Content.ToString() == "הפעל סימולציה")
            {
                SimulationButton.Content = "עצור סימולציה";
                tbHour.Visibility = Visibility.Collapsed;
                tbMinutes.Visibility = Visibility.Collapsed;
                tbSeconds.Visibility = Visibility.Collapsed;
                time = new TimeSpan(int.Parse(tbHour.Text), int.Parse(tbMinutes.Text), int.Parse(tbSeconds.Text));
                tbClock.DataContext = time;
                tbClock.Visibility = Visibility.Visible;
                worker.RunWorkerAsync();
            }
            else
            {
                SimulationButton.Content = "הפעל סימולציה";
                worker.CancelAsync();
                tbHour.Visibility = Visibility.Visible;
                tbMinutes.Visibility = Visibility.Visible;
                tbSeconds.Visibility = Visibility.Visible;
                tbClock.Visibility = Visibility.Collapsed;
            }
            //stopWatch.Restart();
            //isTimerRun = true;
            //worker.RunWorkerAsync();
            //lvCommingLines.DataContext = App.bl.GetLineTimingsPerStation((lbStations.SelectedItem as Station).Key, new TimeSpan(8,0,0));
        }
        public void UpdateTime(TimeSpan ts)
        {
            worker.ReportProgress(0);
        }
    }
}
