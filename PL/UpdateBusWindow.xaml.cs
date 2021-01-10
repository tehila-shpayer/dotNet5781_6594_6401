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

namespace PL
{
    /// <summary>
    /// Interaction logic for UpdateBusWindow.xaml
    /// </summary>
    public partial class UpdateBusWindow : Window
    {
        public Bus updatingBus;
        public int beforeUpdateindex;
        public BO.Bus busBO;
        public UpdateBusWindow(Bus bus)
        {
            InitializeComponent();
            busBO = new BO.Bus();
            busBO = App.bl.GetBus(bus.LicenseNumber);
            updatingBus = bus;
            grid1.DataContext = busBO;
            beforeUpdateindex = MainWindow.busesCollection.IndexOf(updatingBus);

        }

        private void updateBusButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.bl.UpdateBus(busBO);
                //updatingBus = 
                MainWindow.busesCollection[beforeUpdateindex] = PoBoAdapter.BusPoBoAdapter(busBO);
                MessageBox.Show($"Bus updated successfully.", "UPDATE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BOInvalidInformationException ex)
            {
                MessageBox.Show($"Can't update bus.", "UPDATE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "UPDATE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
