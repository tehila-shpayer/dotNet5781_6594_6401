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
    /// Add bus window
    /// </summary>
    public partial class Window2 : Window
    {
        Bus bus;
        public Window2(Bus b)
        {
            InitializeComponent();
            //Bind Added bus information to recived bus
            grid1.DataContext = b;
            bus = b;
        }
        /// <summary>
        /// verfication of typed information and adding bus to collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButtonInWindow_Click(object sender, RoutedEventArgs e)
        {
            //License number must bo of 7 or 8 digits
            if (bus.LicenseNumber.Length < 7 || bus.LicenseNumber.Length>8)
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: the license number has to be a 7 or 8 digit number!", "Error massege", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            //Starting date must be in a reasonable spactrum
            else if (!(bus.RunningDate.Year >= 1896 && bus.RunningDate.Year <= DateTime.Now.Year))
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: Starting date must be after 1896 and before " + DateTime.Now.Year + "!", "Error massege", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            //Running date must be before last treatment date
            else if (bus.RunningDate > bus.LastTreatment)
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: Starting date can't be later then last treatment date!", "Error massege", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            // length of license number must much running date
            else if ((bus.LicenseNumber.Length == 7) && (bus.RunningDate.Year >= 2018))
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: A 7 digit license number bus can't be from later than 2017!", "Error massege", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if ((bus.LicenseNumber.Length == 8) && (bus.RunningDate.Year < 2018))
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: A 8 digit license number bus can't be from earlier than 2018!", "Error massege", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            //General KM must be atleast as KM since last treatment
            else if (bus.KM < bus.BeforeTreatKM)
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: Can't have more KM before treatment than general KM!", "Error massege", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            //Fuel can be maximum 1200
            else if(bus.Fuel > 1200)
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: Fuel can't be over 1200!", "Error massege", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            //If all information is valid - add bus to collection
            else
            {
                BusCollection.Add(bus);
                Close();
            }
        }

        //Allowing only numbers in text boxes
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
