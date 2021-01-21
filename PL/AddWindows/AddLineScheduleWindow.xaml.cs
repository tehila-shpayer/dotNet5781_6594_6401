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
    /// Interaction logic for AddLineScheduleWindow.xaml
    /// </summary>
    public partial class AddLineScheduleWindow : Window
    {
        public BO.LineSchedule lineScheduleBO;
        public AddLineScheduleWindow()
        {
            InitializeComponent();
            lineScheduleBO = new BO.LineSchedule();
            grid1.DataContext = lineScheduleBO;
            mainGrid.DataContext = MainWindow.Language;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllFieldsFull();
        }
        void AllFieldsFull()
        {
            if (lineKeyTextBox.Text != "" && startTimeTimePicker.Text != "" 
                && endTimeTimePicker.Text != "" && freqTextBox.Text != "")
                addButton.IsEnabled = true;
            else
                addButton.IsEnabled = false;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lineScheduleBO.StartTime = TimeSpan.Parse(startTimeTimePicker.SelectedTime.Value.TimeOfDay.ToString());
                lineScheduleBO.EndTime = TimeSpan.Parse(endTimeTimePicker.SelectedTime.Value.TimeOfDay.ToString());
                App.bl.AddLineSchedule(lineScheduleBO);
                MainWindow.lineSchedulesCollection.Add(PoBoAdapter.LineSchedulePoBoAdapter(lineScheduleBO));
                MessageBox.Show($"Line schedule of line {lineScheduleBO.LineKey} was added successfully!", "ADD LINE SCHEDULE MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't add the line schedule!\n" + ex.ToString(), "ADD LINE SCHEDULE MESSAGE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
