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
            if (LN.Text != "")
            {
                string s = LN.Text;
                int number;
                if ((int.TryParse(s, out number) == true) && (s.Length == 8 || s.Length == 7))
                    bus.LicenseNumber = s;
                else
                    LN.Text = "";
                return;
            }
            SD.Text = "day/month/year";
        }
        private void SD_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SD.Text != "")
            {
                string s = SD.Text;
                DateTime d = new DateTime();
                if (DateTime.TryParse(s, out d)&&d.Year>1896&&d.Year<DateTime.Now.Year)
                    bus.RunningDate = d;
                else
                    SD.Text = "";
                return;
            }
            SD.Text = "day/month/year";
        }
        private void SD_Initialized(object sender, EventArgs e)
        {
            // SD.Text = "day/month/year";           
        }

        private void LN_Initialized(object sender, EventArgs e)
        {
            // LN.Text = "8 or 6 digits";
        }

        private void F_LostFocus(object sender, RoutedEventArgs e)
        {
            string s = F.Text;
            int number;
            if ((int.TryParse(s, out number) == true) && number >= 0)
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
            if (DateTime.TryParse(s, out d) && d.Year > 1896 && d.Year < DateTime.Now.Year)
                bus.LastTreatment = d;
            else
                LTD.Text = "";
        }

        private void KM_LostFocus(object sender, RoutedEventArgs e)
        {
            string s = KM.Text;
            int number;
            if ((int.TryParse(s, out number) == true) && number >= 0)
                bus.BeforeTreatKM = number;
            else
                KM.Text = "";
        }
        private void addButtonInWindow_Click(object sender, RoutedEventArgs e)
        {
            BusCollection.Add(bus);
            MainWindow.windowBuses.Add(bus);
            Close();
        }
    }
}
