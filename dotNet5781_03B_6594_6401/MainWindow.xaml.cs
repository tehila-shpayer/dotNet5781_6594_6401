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
using System.Globalization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;


namespace dotNet5781_03B_6594_6401
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public void RandomInitializationBus()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            Bus bus = new Bus();
            for (int i = 1; i <= 12; i++)
            {
                String s;
                int year;
                if (i % 2 == 0)
                {
                    s = rand.Next(1000000, 9999999).ToString();
                    year = rand.Next(1895, 2018);
                }
                else
                {
                    s = rand.Next(10000000, 99999999).ToString();
                    year = rand.Next(2018, DateTime.Now.Year + 1);
                }
                int KM = rand.Next(0, 30000);
                int bt = rand.Next(0, Min(rand.Next(0, 20000), KM));
                int fuel = rand.Next(0, 1201);
                if (i % 5 == 3)
                {
                    fuel = 0;
                }
                bus = new Bus(new DateTime(year, rand.Next(1, 13), rand.Next(1, 30)), s, fuel, KM, bt);
                if (i % 6 != 0 && i % 7 != 0)
                {
                    bus.DoTreatment();
                }
                BusCollection.Add(bus);
            }

        }
        public int Min(int a, int b)
        {
            if (a < b)
                return a;
            return b;
        }

        public MainWindow()
        {
            RandomInitializationBus();
            DataContext = busesList;
            InitializeComponent();
            busesList.DataContext = BusCollection.windowBuses;
            busesList.SelectedIndex = 0;
        }
        public void TimeButton_Click(object sender, RoutedEventArgs e)
        {
            Button timeButton = (Button)sender;
            BusCollection.windowBuses[busesList.SelectedIndex].timer.RunWorkerAsync(5);
        }
        public void RefuelButton_Click(object sender, RoutedEventArgs e)
        {
            Button RefuelButton = (Button)sender;
            Bus b = BusCollection.windowBuses[busesList.SelectedIndex];
            if (!b.IsBusBusy())
            {
                //b.timer.RunWorkerAsync(12);
                RefuelButton.IsEnabled = false;
                b.BusStatus = Status.Refueling;
                b.pressedButton = RefuelButton;
                b.activity.RunWorkerAsync(0);
            }
        }
        //private void Fueler_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    MessageBox.Show("Refuel proccess has successfully ended!", "Fuel Massage", MessageBoxButton.OK, MessageBoxImage.Information);
        //    Button refuel = (Button)e.Result;
        //    refuel.IsEnabled = true;
        //    BusCollection.windowBuses[busesList.SelectedIndex].BusStatus = Status.ready;

        //}

        //private void Fueler_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    BusCollection.windowBuses[e.ProgressPercentage].Refuel();
        //}

        //private void Fueler_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Button refuel = (Button)e.Argument;
        //    fueler.ReportProgress(0);
        //    Thread.Sleep(12000);
        //    e.Result = refuel;

        //}
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.ShowDialog();
        }
        private void busesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BusInfo busInfo = new BusInfo(busesList.SelectedIndex);
            busInfo.Show();
        }
        private void rideButton_Click(object sender, RoutedEventArgs e)
        {
            Bus b = BusCollection.windowBuses[busesList.SelectedIndex];
            if (!b.IsBusBusy())
            {
                Button rideButton = (Button)sender;
                rideButton.IsEnabled = false;
                RideWindow rideWindow = new RideWindow(busesList.SelectedIndex);
                b.pressedButton = rideButton;
                rideWindow.Show();
            }
            //rideButton.IsEnabled = true;
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var item in BusCollection.windowBuses)
            {
                ListBoxItem bus = (ListBoxItem)busesList.ItemContainerGenerator.ContainerFromItem(item);
                String searchS = searchBox.Text;
                int num = searchS.Length;
                if ((num <= item.LicenseNumber.Length && searchS == (item as Bus).LicenseNumber.Substring(0, num)) || (num <= item.LicenseNumberFormat.Length && searchS == (item as Bus).LicenseNumberFormat.Substring(0, num)))
                {
                    bus.Visibility = Visibility.Visible;
                }
                else
                    bus.Visibility = Visibility.Collapsed;
            }
        }
    }

}