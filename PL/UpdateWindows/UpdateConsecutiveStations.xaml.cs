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
    /// Interaction logic for UpdateConsecutiveStations.xaml
    /// </summary>
    public partial class UpdateConsecutiveStations : Window
    {
        public BO.BusLineStation blsBO = new BO.BusLineStation();
        BO.Station station1;
        BO.Station station2;
        public UpdateConsecutiveStations(BusLineStation bls)
        {
            InitializeComponent();
            bls.Clone(blsBO);
            station1 = App.bl.GetPreviouseStation(bls.BusLineKey, bls.Position);
            station2 = App.bl.GetStation(bls.StationKey);
            tbTitle.DataContext = bls;
            spFirstStation.DataContext = station1;
            spSecondStation.DataContext = station2;
            spDistanceAndTime.DataContext = blsBO;
            mainGrid.DataContext = MainWindow.Language;
        }

        #region Buttons
        private void saveChangeButton_Click(object sender, RoutedEventArgs e)
        {
            App.bl.UpdateBusLineStation(blsBO);
            BO.BusLineStation b = App.bl.GetBusLineStation(blsBO.BusLineKey, blsBO.Position);
            MainWindow.InitializeBusLines();
            Close();
        }

        private void mapButton1_Click(object sender, RoutedEventArgs e)
        {
            MapWindow mapWindow = new MapWindow(PoBoAdapter.StationPoBoAdapter(station1));
            mapWindow.Show();
        }

        private void mapButton2_Click(object sender, RoutedEventArgs e)
        {
            MapWindow mapWindow = new MapWindow(PoBoAdapter.StationPoBoAdapter(station2));
            mapWindow.Show();
        }
        #endregion
    }
}
