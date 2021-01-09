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
    /// Interaction logic for UpdateBusLineWindow.xaml
    /// </summary>
    public partial class UpdateBusLineWindow : Window
    {
        public List<string> AreasString = new List<string> { "Center", "General", "Hifa", "Jerusalem", "North", "South", "TelAviv", "YehudaAndShomron" };
        public List<int> Positions = new List<int>();
        public BusLine updatingBusLine;
        public int beforeUpdateindex;
        //public BO.BusLine busLineBO;
        public UpdateBusLineWindow(BusLine busLine)
        {
            InitializeComponent();
            //busLineBO = new BO.BusLine();
            //busLineBO = App.bl.GetBusLine(busLine.Key);
            updatingBusLine = busLine;
            for (int i = 1; i < busLine.BusLineStations.Count() + 1; i++)
                Positions.Add(i);
            beforeUpdateindex = MainWindow.busLinesCollection.IndexOf(busLine);
            areaComboBox.DataContext = AreasString;
            positionsComboBox.DataContext = Positions;
            addStationsComboBox.DataContext = MainWindow.stationsCollection;
            addStationsComboBox.DisplayMemberPath = "  Key  ";
            stationsComboBox.DataContext = busLine.BusLineStations;
            stationsComboBox.DisplayMemberPath = "  StationKey  ";
            stationsComboBox.SelectedIndex = 0;
            addStationsComboBox.SelectedIndex = 0;
            positionsComboBox.SelectedIndex = 0;
            areaComboBox.SelectedIndex = (int)busLine.Area;
            grid1.DataContext = busLine;
        //    firstStationTextBlock.Text = busLine.FirstStation.ToString();
        //    lastStationTextBlock.Text = busLine.LastStation.ToString();
        //    keyTextBlock.Text = busLine.Key.ToString();
        //    lineNumberTextBox.Text = busLine.LineNumber.ToString();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.BusLine busLineBO = new BO.BusLine();
                busLineBO = App.bl.GetBusLine(updatingBusLine.Key);
                busLineBO.LineNumber = int.Parse(lineNumberTextBox.Text);
                busLineBO.Area = (BO.Areas)(Areas)AreasString.IndexOf(areaComboBox.SelectedItem.ToString());
                App.bl.UpdateBusLine(busLineBO);
                //BusLine busLinePO = PoBoAdapter.BusLinePoBoAdapter(busLine);
                MainWindow.busLinesCollection[beforeUpdateindex] = PoBoAdapter.BusLinePoBoAdapter(busLineBO);
                MessageBox.Show($"Bus updated successfully.", "UPDATE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BOInvalidInformationException ex)
            {
                MessageBox.Show($"Can't update bus line.", "UPDATE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void addBusLineStationButton_Click(object sender, RoutedEventArgs e)
        {
            int busKey = updatingBusLine.Key;
            int stationKey = (addStationsComboBox.SelectedItem as Station).Key;
            int position = (int)positionsComboBox.SelectedItem;
            AddBusLineStation.AddBusLineStationToLine(busKey, stationKey, position);
        }

        //private void addStationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (addStationsComboBox.SelectedItem != null && positionsComboBox.SelectedItem != null)
        //        addBusLineStationButton.IsEnabled = true;
        //}

        //private void positionsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (addStationsComboBox.SelectedItem != null && positionsComboBox.SelectedItem != null)
        //        addBusLineStationButton.IsEnabled = true;
        //}

        //private void areaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    areaTextBox.Text = areaComboBox.SelectedItem.ToString();
        //}
    }
}
