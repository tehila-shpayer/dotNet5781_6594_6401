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
using System.Threading;
using System.Globalization;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        public BO.Bus bus;
        public AddBusWindow()
        {
            InitializeComponent();
            bus = new BO.Bus();
            bus.RunningDate = new DateTime(DateTime.Now.Year, 1, 1);
            bus.LastTreatment = new DateTime(DateTime.Now.Year, 1, 1);
            grid1.DataContext = bus;
            mainGrid.DataContext = MainWindow.Language;
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
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
            try
            {
                App.bl.AddBus(bus);
                MainWindow.busesCollection.Add(PoBoAdapter.BusPoBoAdapter(bus));
                MessageBox.Show($"Bus added successfully!", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BOInvalidInformationException ex)
            {
                MessageBox.Show("Couldn't add bus!\n" + ex.ToString(), "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        /// <summary>
        /// כפתור ביטול
        /// ביטול השינויים וציאה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Input Validation
        /// <summary>
        /// הפרמטרים המתאימים של אוטובוס
        /// חייבים להיות חיוביים שלמים
        /// מספר רישוי, קילומטראז' ודלק
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            PL.PreviewKeyDown.GeneralPerviewKeyDown(sender, e);
        }
        /// <summary>
        /// אוטובוס חייב להיות בעל מספר רישוי
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void licenseNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (licenseNumberTextBox.Text == "")
                addButton.IsEnabled = false;
            else
                addButton.IsEnabled = true;
        }
        #endregion
    }
}
