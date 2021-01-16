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

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulationPage.xaml
    /// </summary>
    public partial class SimulationPage : Page
    {
        BackgroundWorker worker;
        public SimulationPage()
        {
            InitializeComponent();
            lbStations.DataContext = MainWindow.stationsCollection;
            lbStations.SelectedIndex = 0;

            worker = new BackgroundWorker();
            //worker.DoWork += Worker_DoWork;
            //worker.ProgressChanged += Worker_ProgressChanged;
            //worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //worker.WorkerReportsProgress = true;
        }

        private void lbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbStations.SelectedIndex >= 0)
                StationInfoGrid.DataContext = MainWindow.stationsCollection.ElementAt(lbStations.SelectedIndex);
        }

        private void SimulationButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
