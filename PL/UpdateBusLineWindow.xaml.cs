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
        public List<string> AreasString = new List<string> { "General", "Jerusalem", "Center", "North", "South", "Hifa", "TelAviv", "YehudaAndShomron" };
        public BusLine updatingBusLine;
        public UpdateBusLineWindow(BusLine busLine)
        {
            InitializeComponent();
            updatingBusLine = busLine;
            areaComboBox.DataContext = AreasString;
            stationsComboBox.DataContext = updatingBusLine.BusLineStations;
            stationsComboBox.DisplayMemberPath = "  StationKey  ";
            areaComboBox.SelectedIndex = (int)busLine.Area;
            firstStationTextBlock.Text = busLine.FirstStation.ToString();
            lastStationTextBlock.Text = busLine.LastStation.ToString();
            keyTextBlock.Text = busLine.Key.ToString();
            lineNumberTextBox.Text = busLine.LineNumber.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busLineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busLineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busLineViewSource.Source = [generic data source]
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.BusLine busLine = new BO.BusLine();
                int beforeUpdateindex = MainWindow.busLinesCollection.IndexOf(updatingBusLine);
                busLine.Key = updatingBusLine.Key;
                busLine = App.bl.GetBusLine(busLine.Key);
                busLine.LineNumber = int.Parse(lineNumberTextBox.Text);
                busLine.Area = (BO.Areas)(Areas)AreasString.IndexOf(areaComboBox.SelectedItem.ToString());
                App.bl.UpdateBusLine(busLine);
                updatingBusLine = PoBoAdapter.BusLinePoBoAdapter(App.bl.GetBusLine(busLine.Key));
                //BusLine busLinePO = PoBoAdapter.BusLinePoBoAdapter(busLine);
                MainWindow.busLinesCollection[beforeUpdateindex] = updatingBusLine;
                MessageBox.Show($"Bus updated successfully.", "UPDATE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BOInvalidInformationException ex)
            {
                MessageBox.Show($"Can't update bus line.", "UPDATE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
