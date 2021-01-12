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
        public UpdateConsecutiveStations(BusLineStation bls)
        {
            InitializeComponent();
            BO.Station station1 = App.bl.GetPreviouseStation(bls.BusLineKey, bls.Position);
            BO.Station station2 = App.bl.GetStation(bls.StationKey);
            tbFirstPosition.DataContext = station1;
            tbSecondPosition.DataContext = station2;
            spFirstStation.DataContext = station1;
            spSecondStation.DataContext = station2;
        }
    }
}
