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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Bus bus = new Bus();
        public Window1()
        {
            InitializeComponent();
        }
               
        private void LN_LostFocus(object sender, RoutedEventArgs e)
        {
                string s = LN.Text;
                int number;
                if ((int.TryParse(s, out number) == true) && (s.Length == 8 || s.Length == 7))
                    bus.LicenseNumber = s;
                else
                    LN.Text = "";
        }
        private void SD_LostFocus(object sender, RoutedEventArgs e)
        {
                string s = SD.Text;
                DateTime d = new DateTime();
                if (DateTime.TryParse(s, out d)&&d.Y  \ear>1896&&d.Year<DateTime.Now.Year)
                    bus.RunningDate = d;
                else
                    SD.Text = "";
        }

        private void F_LostFocus(object sender, RoutedEventArgs e)
        {
            string s = F.Text;
            int number;
            if ((int.TryParse(s, out number) == true) && number >= 0 && number <=1200)
                bus.Fuel = number;
            else
                F.Text = "";
        }

        private void LTKM_LostFocus(object sender, RoutedEventArgs e)
        {
            string s = LTKM.Text;
            int number;
            if ((int.TryParse(s, out number) == true) && number >= 0)
                bus.BeforeTreatKM = number;
            else
                LTKM.Text = "";
        }

        private void LTD_LostFocus(object sender, RoutedEventArgs e)
        {
            string s = LTD.Text;
            DateTime d = new DateTime();
            if (DateTime.TryParse(s, out d) && (d.Year > 1896) && (d.Year <= DateTime.Now.Year))
                bus.LastTreatment = d;
            else
                LTD.Text = "";
        }

        private void KM_LostFocus(object sender, RoutedEventArgs e)
        {
            string s = KM.Text;
            int number;
            if ((int.TryParse(s, out number) == true) && number >= 0)
                bus.KM = number;
            else
                KM.Text = "";
        }
        private void addButtonInWindow_Click(object sender, RoutedEventArgs e)
        {

            if (!(bus.RunningDate.Year >= 1896 && bus.RunningDate.Year <= DateTime.Now.Year))
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n Error: Starting date must be after 1896 and before" + DateTime.Now.Year + "!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (bus.RunningDate > bus.LastTreatment)
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n -Error: Starting date can't be later then last treatment date!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                SD.Text = ""; LTD.Text = "";
            }
            else if ((bus.LicenseNumber.Length == 7) && (bus.RunningDate.Year >= 2018))
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n -Error: A 7 digit license number bus can't be from later than 2017!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                LN.Text = ""; SD.Text = "";
            }
            else if ((bus.LicenseNumber.Length == 8) && (bus.RunningDate.Year < 2018))
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n -Error: A 8 digit license number bus can't be from earlier than 2018!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                LN.Text = ""; SD.Text = "";
            }
            else if (bus.KM < bus.BeforeTreatKM)
            {
                MessageBox.Show("Couldn't add bus. invalid information!\n -Error: Can't have more KM before treatment than general KM!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                KM.Text = ""; LTKM.Text = "";
            }
            else
            {
                BusCollection.Add(bus);
                MainWindow.windowBuses.Add(bus);
                Close();
            }
        }
        private void RD_Click(object sender, RoutedEventArgs e)
        {
            calander.Visibility = Visibility.Visible;
            calander.IsEnabled = true;

        }

        private void calander_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            bus.RunningDate = (DateTime)calander.SelectedDate;
            calander.Visibility = Visibility.Hidden;
            calander.IsEnabled = false;
        }

    }
}
