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
    /// Interaction logic for AddBusLineStationWindow.xaml
    /// </summary>
    public partial class AddBusLineStation : Window
    {
        public BusLineStation selectedStation = new BusLineStation();
        public AddBusLineStation(BusLineStation selectedBusLineStation)
        {
            InitializeComponent();
            selectedStation = selectedBusLineStation;
            cbStationKey.DataContext = MainWindow.stationsCollection;
            cbStationKey.DisplayMemberPath = "  ShowNameKey  ";
            mainGrid.DataContext = MainWindow.Language;
        }
        /// <summary>
        /// פונקציה סטטית
        /// הוספת תחנה לקו אוטובוס
        /// </summary>
        /// <param name="busKey">מפתח הקו</param>
        /// <param name="stationKey">מפתח התחנה</param>
        /// <param name="position">מיקום התחנה בקו</param>
        public static void AddBusLineStationToLine(int busKey, int stationKey, int position)
        {
            try
            {
                App.bl.AddStationToLine(busKey, stationKey, position);
                MainWindow.InitializeBusLines(); //PLעדכון האוסף ב
                MessageBox.Show($"Station {stationKey} was successfully\n added to line of key {busKey}", "ADD STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
            catch (BO.BOInvalidInformationException ex)
            {
                MessageBox.Show("Can't add bus line station\n" + ex.ToString(), "UPDATE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ADD STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        #region Buttons
        /// <summary>
        /// כפתור הוספה
        /// שמירת השינויים ויציאה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddBusLineStationToLine(selectedStation.BusLineKey, (cbStationKey.SelectedItem as Station).Key, selectedStation.Position);
            Close();
        }
        /// <summary>
        /// כפתור בטול
        /// ביטול השינויים וציאה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}
