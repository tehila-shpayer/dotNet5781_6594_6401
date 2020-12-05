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
        static public ObservableCollection<Bus> windowBuses = new ObservableCollection<Bus>();
        BackgroundWorker fueler;

        public void RandomInitializationBus()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            Bus bus = new Bus();
            for (int i = 0; i < 10; i++)
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
                int KM = rand.Next(0, 10000);
                int bt = rand.Next(0, Max(rand.Next(0, 20000), KM));
                int fuel = rand.Next(0, 1201);
                if (i % 4 == 1)
                {
                    fuel = 0;
                }
                bus = new Bus(new DateTime(year, rand.Next(1, 13), rand.Next(1, 32)), s, fuel, KM, bt);
                if (i % 3 == 0)
                {
                    bus.DoTreatment();
                }
                BusCollection.Add(bus);
            }

        }
        public int Max(int a, int b)
        {
            if (a < b)
                return a;
            return b;
        }
        public MainWindow()
        {
            RandomInitializationBus();
            DataContext = busesList;
            foreach (Bus item in BusCollection.buses)
                windowBuses.Add(item);
            InitializeComponent();
            //busesList.ItemsSource = windowBuses;
            busesList.DataContext = windowBuses;
           // busesList.DisplayMemberPath = " LicenseNumberFormat ";
            busesList.SelectedIndex = 0;
            fueler = new BackgroundWorker();
            fueler.DoWork += Fueler_DoWork;
            fueler.ProgressChanged += Fueler_ProgressChanged;
            fueler.RunWorkerCompleted += Fueler_RunWorkerCompleted;
            fueler.WorkerReportsProgress = true;
        }
        public void RefuelButton_Click(object sender, RoutedEventArgs e)
        {
            Button RefuelButton = (Button)sender;
            if (fueler.IsBusy != true)
            {
                RefuelButton.IsEnabled = false;
                fueler.RunWorkerAsync(RefuelButton);
            }
        }
        private void Fueler_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Refuel proccess has successfully ended!", "Fuel Massage", MessageBoxButton.OK, MessageBoxImage.Information);
            Button refuel = (Button)e.Result;
            refuel.IsEnabled = true;
        }

        private void Fueler_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            windowBuses[busesList.SelectedIndex].Refuel();
        }

        private void Fueler_DoWork(object sender, DoWorkEventArgs e)
        {
            Button refuel = (Button)e.Argument;
            fueler.ReportProgress(0);
            Thread.Sleep(12000);
            e.Result = refuel;

        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.ShowDialog();
        }
        private void busesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BusInfo busInfo = new BusInfo(busesList.SelectedIndex);
            busInfo.Show();
            //busInformation.Text = BusCollection.buses.ElementAt(busesList.SelectedIndex).LongToString();
        }
        private void rideButton_Click(object sender, RoutedEventArgs e)
        {
            RideWindow rideWindow = new RideWindow();
            rideWindow.Show();
        }
        private void refuelButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }

}