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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        Bus bus;
        public Window2(Bus b)
        {
            InitializeComponent();
            grid1.DataContext = b;
            bus = b;
        }
        private void addButtonInWindow_Click(object sender, RoutedEventArgs e)
        {
            if (!(bus.RunningDate.Year >= 1896 && bus.RunningDate.Year <= DateTime.Now.Year))
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: Starting date must be after 1896 and before " + DateTime.Now.Year + "!", "Error massege", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (bus.RunningDate > bus.LastTreatment)
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: Starting date can't be later then last treatment date!", "Error massege", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if ((bus.LicenseNumber.Length == 7) && (bus.RunningDate.Year >= 2018))
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: A 7 digit license number bus can't be from later than 2017!", "Error massege", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if ((bus.LicenseNumber.Length == 8) && (bus.RunningDate.Year < 2018))
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: A 8 digit license number bus can't be from earlier than 2018!", "Error massege", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (bus.KM < bus.BeforeTreatKM)
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: Can't have more KM before treatment than general KM!", "Error massege", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if(bus.Fuel > 1200)
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: Fuel can't be over 1200!", "Error massege", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                BusCollection.Add(bus);
                Close();
            }
        }

        private void beforeTreatKMTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            RideWindow.GeneralPerviewKeyDown(sender, e);
        }

        private void fuelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            RideWindow.GeneralPerviewKeyDown(sender, e);
        }

        private void kMTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            RideWindow.GeneralPerviewKeyDown(sender, e);
        }

        private void licenseNumberTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            RideWindow.GeneralPerviewKeyDown(sender, e);
        }
    }
}
