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
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBusLineWindow : Window
    {
        public List<string> AreasString = new List<string> { "General", "Jerusalem", "Center", "North", "South", "Hifa", "TelAviv", "YehudaAndShomron" };
        public AddBusLineWindow()
        {
            InitializeComponent();
            areaComboBox.DataContext = AreasString;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.bl.GetStation(int.Parse(firstStationTextBox.Text)) == null)
                    MessageBox.Show($"Can't add bus line. First station doesnt exist", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                if (App.bl.GetStation(int.Parse(lastStationTextBox.Text)) == null)
                    MessageBox.Show($"Can't add bus line. Last station doesnt exist", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation); 
                BO.BusLine busLine = new BO.BusLine();
                busLine.LineNumber = int.Parse(lineNumberTextBox.Text);
                busLine.Area = (BO.Areas)(Areas)AreasString.IndexOf(areaComboBox.SelectedItem.ToString());
                busLine.Key = App.bl.AddBusLine(busLine);
                App.bl.AddStationToLine(busLine.Key, int.Parse(firstStationTextBox.Text));
                App.bl.AddStationToLine(busLine.Key, int.Parse(lastStationTextBox.Text));
                busLine = App.bl.GetBusLine(busLine.Key);
                //BusLine busLinePO = PoBoAdapter.BusLinePoBoAdapter(busLine);
                MainWindow.busLinesCollection.Add(PoBoAdapter.BusLinePoBoAdapter(busLine));
                MessageBox.Show($"Bus added successfully!", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch ( BO.BOInvalidInformationException ex) 
            {
                MessageBox.Show($"Can't add bus line. Invalid information", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
