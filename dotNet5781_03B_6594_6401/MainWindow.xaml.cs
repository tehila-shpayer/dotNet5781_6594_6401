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

                bus = new Bus(new DateTime(year, rand.Next(1, 13), rand.Next(1, 32)), s, rand.Next(0, 1201), KM, bt);
                bus.DoTreatment();
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

            InitializeComponent();
            busesList.ItemsSource = BusCollection.buses;


        }
        int i = 0;
        private void output_KeyDown(object sender, KeyEventArgs e)
        {
            busInformation.Text = BusCollection.buses.ElementAt(i).ToString();
            i++;
        }

        private void busesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            busInformation.Text = BusCollection.buses.ElementAt(busesList.SelectedIndex).LongToString();
            i++;
            if (i == BusCollection.buses.Count - 1)
                i = 0;
        }
    }


}
