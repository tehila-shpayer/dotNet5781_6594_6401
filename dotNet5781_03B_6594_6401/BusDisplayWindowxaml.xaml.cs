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
            grid1.DataContext = b;
            bus = b;
        }

        private void TreatmentButton_Click(object sender, RoutedEventArgs e)
        {
            //if (treater.IsBusy != true)
            //{
            //    treater.RunWorkerAsync(TreatmentButton.DataContext);
            //    TreatmentButton.IsEnabled = false;
            //}
            Bus b = bus; ;
            if (!b.IsBusBusy())
            {
                //TreatmentButton.IsEnabled = false;
                b.BusStatus = Status.Treatment;
                //b.pressedButton = TreatmentButton;
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
            Bus b = bus;
            if (!b.IsBusBusy())
            {
                //RefuelButton.IsEnabled = false;
                b.BusStatus = Status.Refueling;
                //b.pressedButton = RefuelButton;
                b.activity.RunWorkerAsync(0);
            }
        }
    }
}
