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
    /// Interaction logic for LineAndScheduleWindow.xaml
    /// </summary>
    public partial class LineAndScheduleWindow : Window
    {
        public LineAndScheduleWindow(int lineKey, int sourceKey, int destinationKey)
        {
            InitializeComponent();
            StationsGrid.DataContext = PoBoAdapter.BusLinePoBoAdapter(App.bl.GetBusLine(lineKey), sourceKey, destinationKey);
            lvCSchedule.DataContext = App.bl.GetArrivalTimes(lineKey, sourceKey, destinationKey);
            mainGrid.DataContext = MainWindow.Language;
        }
    }
}
