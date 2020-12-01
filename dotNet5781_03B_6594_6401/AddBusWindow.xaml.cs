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
           // F.Text = bus.LicenseNumber;
        }

        /*private void LN_LostFocus(object sender, RoutedEventArgs e)
        {
           /* string s = LN.Text;
            int number;
            if ((int.TryParse(s, out number) == true) && (s.Length == 8 || s.Length == 6))
                bus.LicenseNumber = s;
            else
                LN.Text = "";
        }

        private void SD_LostFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void SD_LostFocus_1(object sender, RoutedEventArgs e)
        {
            /* string s = SD.Text;
            DateTime d = new DateTime();
            if (DateTime.TryParse(s, out d))
                bus.RunningDate = d;
        }*/
    }
