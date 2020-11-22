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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using dotNet5781_02_6594_6401;

namespace dotNet_03A_5781_6594_6401
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {            
            Random rand = new Random(DateTime.Now.Millisecond);
            // private BusLine currentDisplayBusLine;; 
            /*private void ShowBusLine(int index)
            {
                currentDisplayBusLine = busLines[index];
                UpGrid.DataContext = currentDisplayBusLine;
                lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
            }

            private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                //ShowBusLine((cbBusLines.SelectedValue as BusLine).BusLineNum);
            }*/


            //creates 40 stations:
            //BusStation bs;
            
            /*
                bs = new BusStation(31.234567, 34.56874, "Talya");
                StationList.Add(bs);
                for (int i = 0; i < 39; i++)
                {
                    double la, lo;

                    la = rand.NextDouble() * (33.3 - 31) + 31;

                    lo = rand.NextDouble() * (35.5 - 34.3) + 34.3;

                    bs = new BusStation(la, lo, "");

            (bs);
                }

                BusLine bl;*/
            //BusLineCollection lineCollection = new BusLineCollection();
            // craetes 10 bus lines:
            /*for (int i = 1; i <= 10; i++)
            {
                int Intarea = rand.Next(0, 8);
                bl = new BusLine((Areas)Intarea);
                for (int j = i * 4 - 3; j <= i * 4; j++)
                {
                    bl.AddStation(j);
                }

                for (int k = 0; k < rand.Next(1, 4); k++)
                {
                    int anotherStationKey = rand.Next(1, 41);
                    while (bl.DidFindStation(anotherStationKey))
                    {
                        anotherStationKey = (anotherStationKey + 4) % 40 + 1;
                    }

                    bl.AddStation(anotherStationKey, rand.Next(0, 5 + k));
                }
                lineCollection.Add(bl);
            }*/

            InitializeComponent();
            //cbBusLines.ItemsSource = lineCollection;
            //cbBusLines.DisplayMemberPath = " BusLineNum ";
            //cbBusLines.SelectedIndex = 0;
            //ShowBusLine(cbBusLines.SelectedIndex);
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
