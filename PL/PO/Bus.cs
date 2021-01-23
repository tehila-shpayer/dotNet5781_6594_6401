using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Threading;
namespace PL
{
    public class Bus : DependencyObject
    {
        static readonly DependencyProperty LicenseNumberProperty = DependencyProperty.Register("LicenseNumber", typeof(String), typeof(Bus));
        static readonly DependencyProperty RunningDateFormatProperty = DependencyProperty.Register("RunningDateFormat", typeof(String), typeof(Bus));
        static readonly DependencyProperty LastTreatmentFormatProperty = DependencyProperty.Register("LastTreatmentFormat", typeof(String), typeof(Bus));
        static readonly DependencyProperty FuelProperty = DependencyProperty.Register("Fuel", typeof(int), typeof(Bus));
        static readonly DependencyProperty KMProperty = DependencyProperty.Register("KM", typeof(int), typeof(Bus));
        static readonly DependencyProperty BeforeTreatKMProperty = DependencyProperty.Register("BeforeTreatKM", typeof(int), typeof(Bus));
        static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(BO.Status), typeof(Bus));

        static readonly DependencyProperty LicenseNumberFormatProperty = DependencyProperty.Register("LicenseNumberFormat", typeof(String), typeof(Bus));
        static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(String), typeof(Bus));

        public string LicenseNumber { get => (String)GetValue(LicenseNumberProperty); set => SetValue(LicenseNumberProperty, value); }
        //public DateTime RunningDate { get => (DateTime)GetValue(RunningDateProperty); set => SetValue(RunningDateProperty, value); }
        //public DateTime LastTreatment { get => (DateTime)GetValue(LastTreatmentProperty); set => SetValue(LastTreatmentProperty, value); }
        public String RunningDateFormat { get => (String)GetValue(RunningDateFormatProperty); set => SetValue(RunningDateFormatProperty, value); }
        public String LastTreatmentFormat { get => (String)GetValue(LastTreatmentFormatProperty); set => SetValue(LastTreatmentFormatProperty, value); }
        public int Fuel { get => (int)GetValue(FuelProperty); set => SetValue(FuelProperty, value); }
        public int KM
        {
            get => (int)GetValue(KMProperty); set => SetValue(KMProperty, value);
        }
        public int BeforeTreatKM { get => (int)GetValue(BeforeTreatKMProperty); set => SetValue(BeforeTreatKMProperty, value); }
        public BO.Status Status { get => (BO.Status)GetValue(StatusProperty); set => SetValue(StatusProperty, value); }

        public string LicenseNumberFormat { get => (String)GetValue(LicenseNumberFormatProperty); set => SetValue(LicenseNumberFormatProperty, value); }
        public BackgroundWorker activity;
        public BackgroundWorker timer;
        public String Time { get => (String)GetValue(TimeProperty); set => SetValue(TimeProperty, value); }
        //private void Activity_DoWork(object sender, DoWorkEventArgs e)//do the ride, treatment or refueling and sleep for the required time
        //{
        //    switch (Status)
        //    {
        //        case Status.Refueling:
        //            {
        //                timer.RunWorkerAsync(12);
        //                Thread.Sleep(12000);
        //                break;
        //            }
        //        case Status.Treatment:
        //            {
        //                timer.RunWorkerAsync(144);
        //                Thread.Sleep(144000);
        //                break;
        //            }
        //        case Status.Ride:
        //            {
        //                int KMride = (int)e.Argument;
        //                Random rnd = new Random();
        //                double Hours = (double)KMride / rnd.Next(20, 50);
        //                int secondToSleep = (int)(Hours * 6);//the ride time
        //                timer.RunWorkerAsync(secondToSleep);
        //                Thread.Sleep(secondToSleep * 1000);
        //                break;
        //            }
        //        default:
        //            { break; }
        //    }
        //    activity.ReportProgress((int)e.Argument);
        //}
        //private void Activity_ProgressChanged(object sender, ProgressChangedEventArgs e)//do the ride, treatment or refueling
        //{
        //    switch (Status)
        //    {
        //        case Status.Refueling:
        //            {
        //                Refuel();
        //                break;
        //            }
        //        case Status.Treatment:
        //            {
        //                DoTreatment();
        //                break;
        //            }
        //        case Status.Ride:
        //            {
        //                Ride(e.ProgressPercentage);
        //                break;
        //            }
        //        default:
        //            { break; }
        //    }

        //}
        //private void Activity_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    switch (Status)
        //    {
        //        case Status.Refueling:
        //            {
        //                MessageBox.Show("Refuel proccess has successfully ended!", "Fuel Massage", MessageBoxButton.OK, MessageBoxImage.Information);
        //                break;
        //            }
        //        case Status.Treatment:
        //            {
        //                MessageBox.Show("Treating proccess has successfully ended!", "Treatment Massage", MessageBoxButton.OK, MessageBoxImage.Information);
        //                break;
        //            }
        //        case Status.Ride:
        //            {
        //                String s = "Bus " + LicenseNumberFormat + " has successfully finished the ride!";
        //                MessageBox.Show(s, "Ride Massage", MessageBoxButton.OK, MessageBoxImage.Information);
        //                break;
        //            }
        //        default:
        //            { break; }
        //    }
        //    ApdateStatus();//after any ride, refueling or treatment the status is updated
        //}
        //public bool IsBusBusy() { return activity.IsBusy; }//If the bus is in the middle of a ride, treatment or refueling return true
        //public void Timer_DoWork(object sender, DoWorkEventArgs e)//Shows the time left until the bus is ready
        //{
        //    int time = (int)e.Argument;
        //    for (int i = time; i > 0; i--)
        //    {
        //        timer.ReportProgress(i);//update the time each second
        //        Thread.Sleep(1000);
        //    }
        //}
        //public void Timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    int progress = e.ProgressPercentage;
        //    Time = "ready in " + TimeFormat(progress);
        //}
        //public void Timer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    Time = "";
        //}
    }
}
