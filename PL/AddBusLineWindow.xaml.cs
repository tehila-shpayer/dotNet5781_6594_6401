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
        public BO.BusLine busLineBO;
        public List<string> AreasString = new List<string> { "Center", "General", "Hifa", "Jerusalem", "North", "South", "TelAviv", "YehudaAndShomron" };
        public AddBusLineWindow()
        {
            InitializeComponent();
            busLineBO = new BO.BusLine();
            areaComboBox.DataContext = AreasString;
            areaComboBox.SelectedIndex = 0;
            firstStationComboBox.DataContext = MainWindow.stationsCollection;
            lastStationComboBox.DataContext = MainWindow.stationsCollection;
            firstStationComboBox.DisplayMemberPath = "  Key  ";
            lastStationComboBox.DisplayMemberPath = "  Key  ";
            firstStationComboBox.SelectedIndex = 0;
            lastStationComboBox.SelectedIndex = 0;
            grid1.DataContext = busLineBO;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (App.bl.GetStation(int.Parse(firstStationTextBox.Text)) == null)
                //    MessageBox.Show($"Can't add bus line. First station doesnt exist", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //if (App.bl.GetStation(int.Parse(lastStationTextBox.Text)) == null)
                //    MessageBox.Show($"Can't add bus line. Last station doesnt exist", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //busLineBO.LineNumber = int.Parse(lineNumberTextBox.Text);
                //busLineBO.Area = (BO.Areas)(Areas)AreasString.IndexOf(areaComboBox.SelectedItem.ToString());
                //busLineBO.Key = App.bl.AddBusLine(busLineBO);
                //var x = firstStationComboBox.SelectedItem.ToString();
                //int s1 = (firstStationComboBox.SelectedItem as Station).Key;
                //App.bl.AddStationToLine(busLineBO.Key, s1);
                //App.bl.AddStationToLine(busLineBO.Key, int.Parse(lastStationComboBox.SelectedItem.ToString()));
                //busLineBO = App.bl.GetBusLine(busLineBO.Key);
                ////BusLine busLinePO = PoBoAdapter.BusLinePoBoAdapter(busLine);
                //MainWindow.busLinesCollection.Add(PoBoAdapter.BusLinePoBoAdapter(busLineBO));
                //MessageBox.Show($"Bus added successfully!", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                busLineBO.Area = (BO.Areas)(Areas)AreasString.IndexOf(areaComboBox.SelectedItem.ToString());
                busLineBO.Key = App.bl.AddBusLine(busLineBO);
                App.bl.AddStationToLine(busLineBO.Key, (firstStationComboBox.SelectedItem as Station).Key);
                App.bl.AddStationToLine(busLineBO.Key, (lastStationComboBox.SelectedItem as Station).Key);
                busLineBO = App.bl.GetBusLine(busLineBO.Key);
                MainWindow.busLinesCollection.Add(PoBoAdapter.BusLinePoBoAdapter(busLineBO));
                MessageBox.Show($"Bus added successfully!", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Can't add bus line. Invalid information", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
