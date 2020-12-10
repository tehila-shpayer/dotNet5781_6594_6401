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
    /// Interaction logic for RideWindow.xaml
    /// </summary>
    public partial class RideWindow : Window
    {
        public RideWindow(int index)
        {
            InitializeComponent();
            DataContext = index;
        }
        //private void Rider_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    rider.ReportProgress(0);
        //    int KM = (int)e.Argument;
        //    Random rnd = new Random();
        //    double time =(double) KM / rnd.Next(30, 60);
        //    int timeToSleep = (int)(time * 6000);
        //    Thread.Sleep(timeToSleep);
        //    rider.ReportProgress(KM);
        //}
        //private void Rider_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    int index = (int)DataContext;
        //    int KM = e.ProgressPercentage;
        //    if (KM == 0)
        //    {
        //        BusCollection.windowBuses[index].BusStatus = Status.Ride;
        //    }
        //    else
        //        BusCollection.windowBuses[index].Ride(KM);
        //}
        //private void Rider_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    String s = "Bus " + BusCollection.windowBuses[(int)DataContext].GetLicenseNumberFormat() + " has successfully finished the ride!";
        //    MessageBox.Show(s, "Ride Massage", MessageBoxButton.OK, MessageBoxImage.Information);
        //}

        private void KM_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            GeneralPerviewKeyDown(sender, e);
        }
        static void GeneralPerviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
            {
                if (!Keyboard.IsKeyDown(Key.LeftShift) && !Keyboard.IsKeyDown(Key.RightShift))
                    return;
            }
            e.Handled = true;
            return;
        }
        private void KM_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t == null) return;
            if (e.Key == Key.Enter && t.Text != "")
            {
                String s = KMtextBox.Text;
                int KM = int.Parse(s);
                if (KM <= 0)
                {
                    MessageBox.Show("The KM to ride must be positive!", "Ride Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                Bus currentBus = BusCollection.windowBuses[(int)DataContext];
                if (!currentBus.CanDoRide(KM))
                {
                    MessageBox.Show("The bus must be treated or refueled!", "Ride Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //rider.RunWorkerAsync(KM);
                Bus b = BusCollection.windowBuses[(int)DataContext];
                b.BusStatus = Status.Ride;
                b.activity.RunWorkerAsync(KM); 
                Close();
            }
        }
    }
}
