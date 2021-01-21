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
    /// Interaction logic for UpdateLineScheduleWindow.xaml
    /// </summary>
    public partial class UpdateLineScheduleWindow : Window
    {
        public LineSchedule updatingLineSchedule;
        public int beforeUpdateindex;
        public BO.LineSchedule lineScheduleBO;
        public UpdateLineScheduleWindow(LineSchedule lineSchedule)
        {
            InitializeComponent();
            lineScheduleBO = App.bl.GetLineSchedule(lineSchedule.LineKey, lineSchedule.StartTime);
            updatingLineSchedule = lineSchedule;
            grid1.DataContext = lineScheduleBO;
            beforeUpdateindex = MainWindow.lineSchedulesCollection.IndexOf(updatingLineSchedule);
            mainGrid.DataContext = MainWindow.Language;
        }

        private void lineKeyTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            PL.PreviewKeyDown.GeneralPerviewKeyDown(sender, e);
        }

        private void freqTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            PL.PreviewKeyDown.GeneralPerviewKeyDown(sender, e);
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lineScheduleBO.LineKey = updatingLineSchedule.LineKey;
                lineScheduleBO.StartTime = updatingLineSchedule.StartTime;
                App.bl.UpdateLineSchedule(lineScheduleBO);
                MainWindow.lineSchedulesCollection[beforeUpdateindex] = PoBoAdapter.LineSchedulePoBoAdapter(lineScheduleBO);
                MessageBox.Show($"Line schedule of line {lineScheduleBO.LineKey} updated successfully.", "UPDATE LINE SHEDULE MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BOInvalidInformationException ex)
            {
                MessageBox.Show("Can't update the line schedule!\n" + ex.ToString(), "UPDATE LINE SHEDULE MESSAGE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't update the station!\n" + ex.Message, "UPDATE STATION MESSAGE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllFieldsFull();
        }
        void AllFieldsFull()
        {
            if (endTimeTimePicker.Text != "" && freqTextBox.Text != "")
                updateButton.IsEnabled = true;
            else
                updateButton.IsEnabled = false;
        }
    }
}
