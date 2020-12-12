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
                bus = new Bus(s, new DateTime(year, rand.Next(1, 13), rand.Next(1, 30)), fuel, KM, bt);
                if (i % 6 != 0 && i % 7 != 0)
                {
                    bus.DoTreatment();
                }
                bus.ApdateStatus();
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
        public void RefuelButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBus = (sender as Button).DataContext as Bus;
            //Button RefuelButton = (Button)sender;
            if (!selectedBus.IsBusBusy())
            {
                //RefuelButton.IsEnabled = false;
                selectedBus.BusStatus = Status.Refueling;
                //selectedBus.pressedButton = RefuelButton;
                selectedBus.activity.RunWorkerAsync(0);
            }
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 window1 = new Window2(new Bus());
            window1.ShowDialog();
        }
        private void busesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //foreach (Window win in Application.Current.Windows)
            //{
            //    if (win is BusDisplayWindowxaml && BusDisplayWindowxaml.bus == BusCollection.windowBuses[busesList.SelectedIndex])
            //    {
            //        win.WindowState = System.Windows.WindowState.Normal;
            //        win.Focus();
            //        //win.Activate();
            //        return;
            //    }
            //} 
            
            BusDisplayWindowxaml busInfo = new BusDisplayWindowxaml(BusCollection.windowBuses[busesList.SelectedIndex]);
            busInfo.Show();
        }
        private void rideButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBus = (sender as Button).DataContext as Bus;
            if (!selectedBus.IsBusBusy())
            {
                //Button rideButton = (Button)sender;
                //rideButton.IsEnabled = false;
                RideWindow rideWindow = new RideWindow(BusCollection.windowBuses.IndexOf(selectedBus));
                //selectedBus.pressedButton = rideButton;
                rideWindow.Show();
            }
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