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
    /// Interaction logic for DeleteBusWindow.xaml
    /// </summary>
    public partial class AddBusLineStation : Window
    {
        public BusLineStation selectedStation = new BusLineStation();
        public AddBusLineStation(BusLineStation selectedBusLineStation)
        {
            InitializeComponent();
            selectedStation = selectedBusLineStation;
        }

        private void tbStationKey_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            PL.PreviewKeyDown.GeneralPerviewKeyDown(sender, e);
        }

        private void tbStationKey_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            //if (t == null) return;
            if (e.Key == Key.Enter && t.Text != "")
            {
                AddBusLineStationToLine(selectedStation.BusLineKey, int.Parse(t.Text), selectedStation.Position + 1 );
                //try
                //{
                //    int index = 0;
                //    BO.BusLine busLineBO = App.bl.GetBusLine(selectedStation.BusLineKey);
                //    BusLine busLinePO = PoBoAdapter.BusLinePoBoAdapter(busLineBO);
                //    foreach (BusLine bl in MainWindow.busLinesCollection)
                //    {
                //        if (bl.Key == busLinePO.Key)
                //        {
                //            index = MainWindow.busLinesCollection.IndexOf(bl);
                //            break;
                //        }
                //    }
                //    App.bl.AddStationToLine(selectedStation.BusLineKey, int.Parse(t.Text), selectedStation.Position + 1);
                //    busLineBO = App.bl.GetBusLine(selectedStation.BusLineKey);
                //    busLinePO = PoBoAdapter.BusLinePoBoAdapter(busLineBO);
                //    MainWindow.busLinesCollection[index] = busLinePO;
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show($"{ex.Message}", "ADD STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //}
                Close();
            }
        }
        public static void AddBusLineStationToLine(int busKey, int stationKey, int position)
        {
            try
            {
                //App.bl.AddStationToLine(busKey, stationKey, position);
                //MainWindow.busLinesCollection.Clear();
                //foreach (BO.BusLine bl in App.bl.GetAllBusLines())
                //{
                //    MainWindow.busLinesCollection.Add(PoBoAdapter.BusLinePoBoAdapter(bl));
                //}
                int index = 0;
                BO.BusLine busLineBO = App.bl.GetBusLine(busKey);
                BusLine busLinePO = PoBoAdapter.BusLinePoBoAdapter(busLineBO);
                foreach (BusLine bl in MainWindow.busLinesCollection)
                {
                    if (bl.Key == busLinePO.Key)
                    {
                        index = MainWindow.busLinesCollection.IndexOf(bl);
                        break;
                    }
                }
                App.bl.AddStationToLine(busKey, stationKey, position);
                busLineBO = App.bl.GetBusLine(busKey);
                busLinePO = PoBoAdapter.BusLinePoBoAdapter(busLineBO);
                MainWindow.busLinesCollection[index] = busLinePO;
                MessageBox.Show($"Station {stationKey} was successfully\n added to line {busLinePO.LineNumber}", "ADD STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ADD STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
