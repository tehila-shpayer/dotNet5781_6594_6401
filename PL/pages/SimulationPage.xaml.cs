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
        bool isTimerRun;
        public SimulationPage()
        {
            InitializeComponent();
            lbStations.DataContext = MainWindow.stationsCollection;
            lbStations.SelectedIndex = 0;
            lvCommingLines.DataContext = App.bl.GetLineTimingsPerStation((lbStations.SelectedItem as Station).Key, new TimeSpan(DateTime.Now.TimeOfDay.Ticks));
            currentStation = lbStations.SelectedItem as Station;
            stopWatch = new Stopwatch();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                worker.ReportProgress(231);
                Thread.Sleep(1000);
            }
        }

        private void lbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbStations.SelectedIndex >= 0)
                StationInfoGrid.DataContext = MainWindow.stationsCollection.ElementAt(lbStations.SelectedIndex);
        }

        private void SimulationButton_Click(object sender, RoutedEventArgs e)
        {
            stopWatch.Restart();
            isTimerRun = true;
            worker.RunWorkerAsync();
        }
        void UpdateTime(TimeSpan ts)
        {
        }
    }
}
