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
        int i = 0;
        public MainWindow()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            Bus bus=new Bus();
            for (int i = 0; i < 10; i++)
            {
                string s = rand.Next(1000000, 99999999).ToString();
                if (i % 4 == 0)
                    bus = new Bus(new DateTime(i + 2000, i+1, (i * i + 1) % 29 + 1), s, i * 100, 19000, 19000);
                else if (i % 5 == 0)
                    bus = new Bus(new DateTime(i + 2000, (i * i + 1) % 12, (i * i + 1) % 29 + 1), s, i * 10);
                else
                    bus = new Bus(new DateTime(i + 2000, (i * i + 1) % 12, (i * i) % 29 + 1), s);
                BusCollection.Add(bus);
            }
            InitializeComponent();
        }

        private void output_KeyDown(object sender, KeyEventArgs e)
        {
            output.Text = BusCollection.buses.ElementAt(i).ToString();
            i++;
        }
    }
}
