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
    /// The main window of the app
    /// </summary>

    public partial class MainWindow : Window
    {
        /// <summary>
        /// Random selection of 10 buses
        /// Add bubses to static bus collection class
        /// </summary>
        public void RandomInitializationBus()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            Bus bus = new Bus();
            for (int i = 1; i <= 18; i++)
            {
                String s;
                int year;
                //5 buses from before 2018 - 7 digit license number
                if (i % 2 == 0)
                {
                    s = rand.Next(1000000, 9999999).ToString();
                    year = rand.Next(1895, 2018);
                }
                //5 buses from 2018 and on - 8 digit license number
                else
                {
                    s = rand.Next(10000000, 99999999).ToString();
                    year = rand.Next(2018, DateTime.Now.Year + 1);
                }
                int KM = rand.Next(0, 30000);
                int bt = rand.Next(0, Min(rand.Next(0, 20000), KM));//KM can't be smaller than bt
                int fuel = rand.Next(0, 1201);
                //some buses have low fuel
                if (i % 5 == 3)
                {
                    fuel = 0;
                }
                bus = new Bus(s, new DateTime(year, rand.Next(1, 13), rand.Next(1, 30)), fuel, KM, bt);
                //some buses are after treatment
                if (i % 6 != 0 && i % 7 != 0)
                {
                    bus.DoTreatment();
                }
                bus.ApdateStatus();
                BusCollection.Add(bus);
            }
        }
        /// <summary>
        /// Choosin the minimum out of 2 numbers
        /// </summary>
        /// <param name="a">first number</param>
        /// <param name="b">ssecond number</param>
        /// <returns>minimum</returns>
        public int Min(int a, int b)
        {
            if (a < b)
                return a;
            return b;
        }

        public MainWindow()
        {
            //Set buses information
            RandomInitializationBus();
            InitializeComponent();
            //Bind buses listBox to buses in the system
            busesList.DataContext = BusCollection.windowBuses;
            busesList.SelectedIndex = 0;
        }
        /// <summary>
        /// Refuling the specific bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefuelButton_Click(object sender, RoutedEventArgs e)
        {
            //The bus that the button is in his line (his dataTemplate)
            var selectedBus = (sender as Button).DataContext as Bus;
            //If the bus isn't already refueling
            if (!selectedBus.IsBusBusy())
            {
                //Apdate status
                selectedBus.BusStatus = Status.Refueling;
                //Send to bus's process
                selectedBus.activity.RunWorkerAsync(0);
            }
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            //Open adding bus window
            Window2 window1 = new Window2(new Bus());
            window1.ShowDialog();
        }
        private void busesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {          
            //open showing bus details window
            BusDisplayWindowxaml busInfo = new BusDisplayWindowxaml(BusCollection.windowBuses[busesList.SelectedIndex]);
            busInfo.Show();
        }
        private void rideButton_Click(object sender, RoutedEventArgs e)
        {
            //The bus that the button is in his line (his dataTemplate)
            var selectedBus = (sender as Button).DataContext as Bus;
            //If the bus isn't already driving
            if (!selectedBus.IsBusBusy())
            {
                //Open riding window
                RideWindow rideWindow = new RideWindow(BusCollection.windowBuses.IndexOf(selectedBus));
                rideWindow.Show();
            }
        }
        /// <summary>
        /// Search text box - text changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var item in BusCollection.windowBuses)
            {
                ListBoxItem bus = (ListBoxItem)busesList.ItemContainerGenerator.ContainerFromItem(item);
                String searchS = searchBox.Text;
                int num = searchS.Length;
                //Show only buses which there license number have the typed perfix
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