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

namespace dotNet5781_03B_6594_6401
{
    /// <summary>
    /// Interaction logic for BusInfo.xaml
    /// </summary>
    public partial class BusInfo : Window
    {
        public BusInfo(int index)
        {
            InitializeComponent();
            Bus b = MainWindow.windowBuses[index];
            ln.Content = b.LicenseNumberFormat;
            sd.Content = b.RunningDate;
            km.Content = b.KM;
            ltkm.Content = b.BeforeTreatKM;
            ltd.Content = b.LastTreatment;
            f.Content = b.Fuel;
        }
    }
}
