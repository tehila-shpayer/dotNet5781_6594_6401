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
    /// Interaction logic for RideWindow.xaml
    /// </summary>
    public partial class RideWindow : Window
    {
        public RideWindow()
        {
            InitializeComponent();
        }
        private void KM_Enter_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t == null) return;
            if (e == null) return;
            if (e.Key == Key.Space || e.Key == Key.Tab) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c)) return;
            if (e.Key == Key.Tab)
                this.Close();
            if (e.Key == Key.Enter)
            {
                Close();
            }
            e.Handled = true;
            return;
        }
        

        private void DoRide()
        {
            String s = KMtextBox.Text;
            int intKM;
            if ((int.TryParse(s, out intKM) == true) && intKM >= 0)
                
            Close();
        }

        private void KMtextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DoRide();
            }
        }
    }
}
