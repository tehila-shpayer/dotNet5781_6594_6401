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
using System.ComponentModel;
using System.Threading;

namespace dotNet5781_03B_6594_6401
{
    /// <summary>
    /// Interaction logic for RideWindow.xaml
    /// </summary>
    public partial class RideWindow : Window
    {
        public RideWindow(int index)
        {
            InitializeComponent();
            DataContext = index;//The index of the bus that going to ride
        }
        private void KM_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            GeneralPerviewKeyDown(sender, e);
        }
        public static void GeneralPerviewKeyDown(object sender, KeyEventArgs e)//allow to enter only digits to the textBox
        {
            TextBox t = sender as TextBox;
            if (t == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
            {
                if (!Keyboard.IsKeyDown(Key.LeftShift) && !Keyboard.IsKeyDown(Key.RightShift))
                    return;
            }
            e.Handled = true;//if it is not a digit or a control key, the letter would not be writen
            return;
        }
        private void KM_KeyDown(object sender, KeyEventArgs e)//make the ride when 
        {
            TextBox t = sender as TextBox;
            if (t == null) return;
            if (e.Key == Key.Enter && t.Text != "")
            {
                String stringKM = KMtextBox.Text;
                int KM = int.Parse(stringKM);
                if (KM <= 0)
                {
                    MessageBox.Show("The KM to ride must be positive!", "Ride Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                if (KM >1200)
                {
                    MessageBox.Show("The KM can not be more than 1200!", "Ride Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Bus currentBus = BusCollection.windowBuses[(int)DataContext];
                if (!currentBus.CanDoRide(KM))
                {
                    String message = "The bus has enough fuel only for " +currentBus.Fuel + " KM!";
                    MessageBox.Show(message, "Ride Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //if the ride can be done:
                Bus b = BusCollection.windowBuses[(int)DataContext];
                b.BusStatus = Status.Ride;
                b.activity.RunWorkerAsync(KM); 
                Close();
            }
        }
    }
}
