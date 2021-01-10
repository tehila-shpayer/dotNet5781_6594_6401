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
            addStationComboBox.DataContext = MainWindow.stationsCollection;
            addStationComboBox.DisplayMemberPath = "  ShowNameKey  ";
            stationsComboBox.DataContext = updatingBusLine.BusLineStations;
            stationsComboBox.DisplayMemberPath = "  ShowNameKey  ";
            stationsComboBox.SelectedIndex = 0;
            addStationComboBox.SelectedIndex = 0;
            positionsComboBox.SelectedIndex = 0;
            areaComboBox.SelectedIndex = (int)busLine.Area;
            grid1.DataContext = updatingBusLine;
        }
        private void lineNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string t = lineNumberTextBox.Text;
            if (t != "" && int.Parse(t) > 0)
                addButton.IsEnabled = true;
            else
                addButton.IsEnabled = false;
        }

        private void lineNumberTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            PL.PreviewKeyDown.GeneralPerviewKeyDown(sender, e);
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.BusLine busLine = new BO.BusLine();
                busLine = App.bl.GetBusLine(updatingBusLine.Key);
                busLine.LineNumber = int.Parse(lineNumberTextBox.Text);
                busLine.Area = (BO.Areas)(Areas)AreasString.IndexOf(areaComboBox.SelectedItem.ToString());
                App.bl.UpdateBusLine(busLine);
                updatingBusLine = PoBoAdapter.BusLinePoBoAdapter(App.bl.GetBusLine(busLine.Key));
                MainWindow.busLinesCollection[beforeUpdateindex] = updatingBusLine;
                MessageBox.Show($"Bus line updated successfully.", "UPDATE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BOInvalidInformationException ex)
            {
                MessageBox.Show("Can't update bus line\n" + ex.ToString(), "UPDATE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void addBusLineStationButton_Click(object sender, RoutedEventArgs e)
        {
            int busKey = updatingBusLine.Key;
            int stationKey = (addStationComboBox.SelectedItem as Station).Key;
            int position = (int)positionsComboBox.SelectedItem;
            try
            {
                AddBusLineStation.AddBusLineStationToLine(busKey, stationKey, position);
                updatingBusLine = PoBoAdapter.BusLinePoBoAdapter(App.bl.GetBusLine(updatingBusLine.Key));
            }
            catch (BO.BOInvalidInformationException ex)
            {
                MessageBox.Show("Can't add bus line station\n" + ex.ToString(), "UPDATE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
