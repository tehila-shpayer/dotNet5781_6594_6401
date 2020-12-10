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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;

namespace dotNet5781_03B_6594_6401
{
    /// <summary>
    /// Interaction logic for BusInfo.xaml
    /// </summary>
    public partial class BusInfo : Window
    {
        //BackgroundWorker fueler;
        //BackgroundWorker treater;
        Bus b;
        BackgroundWorker timer;
        public BusInfo(int index)
        {
            InitializeComponent();
            b = BusCollection.windowBuses[index];
            ln.Content = b.LicenseNumberFormat;
            sd.Content = b.RunningDate;
            km.Content = b.KM;
            ltkm.Content = b.BeforeTreatKM;
            ltd.Content = b.LastTreatment;
            f.Content = b.Fuel;
            //fueler = new BackgroundWorker();
            //fueler.DoWork += Fueler_DoWork;
            //fueler.ProgressChanged += Fueler_ProgressChanged;
            //fueler.RunWorkerCompleted += Fueler_RunWorkerCompleted;
            //fueler.WorkerReportsProgress = true;
            //treater = new BackgroundWorker();
            //treater.DoWork += Treater_DoWork; ;
            //treater.ProgressChanged += Treater_ProgressChanged; ;
            //treater.RunWorkerCompleted += Treater_RunWorkerCompleted; ;
            //treater.WorkerReportsProgress = true;
            RefuelButton.DataContext = index;
            TreatmentButton.DataContext = index;

            timer = new BackgroundWorker();
            timer.DoWork += Timer_DoWork;
            timer.ProgressChanged += Timer_ProgressChanged;
            timer.RunWorkerCompleted += Timer_RunWorkerCompleted;
            timer.WorkerReportsProgress = true;
        }
        public void Timer_DoWork(object sender, DoWorkEventArgs e)
        {
            int time = (int)e.Argument;
            for (int i = time; i > 0; i--)
            {
                timer.ReportProgress(i);
                Thread.Sleep(1000);
            }
        }
        public void Timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            //Content = progress + "second";
            
        }
        public void Timer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Tlable.Content = "now!";
        }

        //private void Treater_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    MessageBox.Show("Treating proccess has successfully ended!", "Treatment Massage", MessageBoxButton.OK, MessageBoxImage.Information);
        //    ltkm.Content = b.BeforeTreatKM;
        //    ltd.Content = b.LastTreatment;
        //    TreatmentButton.IsEnabled = true;
        //}

        //private void Treater_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    BusCollection.windowBuses[e.ProgressPercentage].DoTreatment();

        //}

        //private void Treater_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Thread.Sleep(144000);
        //    treater.ReportProgress((int)e.Argument);
        //}

        private void TreatmentButton_Click(object sender, RoutedEventArgs e)
        {
            //if (treater.IsBusy != true)
            //{
            //    treater.RunWorkerAsync(TreatmentButton.DataContext);
            //    TreatmentButton.IsEnabled = false;
            //}
            Bus b = BusCollection.windowBuses[(int)TreatmentButton.DataContext];
            if (!b.IsBusBusy())
            {
                TreatmentButton.IsEnabled = false;
                b.BusStatus = Status.Treatment;
                b.pressedButton = TreatmentButton;
                b.activity.RunWorkerAsync(0);
            }
        }

        private void RefuelButton_Click(object sender, RoutedEventArgs e)
        {
            //if (fueler.IsBusy != true)
            //{
            //    fueler.RunWorkerAsync(RefuelButton.DataContext);
            //    RefuelButton.IsEnabled = false;

            //}
           // Button RefuelButton = (Button)sender;
            Bus b = BusCollection.windowBuses[(int)RefuelButton.DataContext];
            if (!b.IsBusBusy())
            {
                RefuelButton.IsEnabled = false;
                b.BusStatus = Status.Refueling;
                b.pressedButton = RefuelButton;
                b.activity.RunWorkerAsync(0);
            }
        }
        //    private void Fueler_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    MessageBox.Show("Refuel proccess has successfully ended!", "Fuel Massage", MessageBoxButton.OK, MessageBoxImage.Information);
        //    f.Content = b.Fuel;
        //    RefuelButton.IsEnabled = true;
        //}

        //private void Fueler_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    BusCollection.windowBuses[e.ProgressPercentage].Refuel();


        //}

        //private void Fueler_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Thread.Sleep(12000);
        //    fueler.ReportProgress((int)e.Argument);
        //}

    }
}
