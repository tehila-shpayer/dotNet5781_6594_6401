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
using System.Threading;
using System.ComponentModel;

namespace dotNet5781_03B_6594_6401
{
    /// <summary>
    /// Interaction logic for BusDisplayWindowxaml.xaml
    /// </summary>
    public partial class BusDisplayWindowxaml : Window
    {
        Bus bus;
        public BusDisplayWindowxaml(Bus b)
        {
            InitializeComponent();
            bus = b;
            //Bind displayed information with recieved bus
            grid1.DataContext = b;
            DataContext = b;
        }

        private void TreatmentButton_Click(object sender, RoutedEventArgs e)
        {
            Bus b = bus;
            //If bus isn't occupied
            if (!b.IsBusBusy())
            {
                //Apdate status
                b.BusStatus = Status.Treatment;
                //Send bus to the process
                b.activity.RunWorkerAsync(0);
            }
        }

        private void RefuelButton_Click(object sender, RoutedEventArgs e)
        {
            Bus b = bus;
            //If bus isn't occupied
            if (!b.IsBusBusy())
            {
                //Apdate status
                b.BusStatus = Status.Refueling;
                //Send bus to the process
                b.activity.RunWorkerAsync(0);
            }
        }
    }
}
