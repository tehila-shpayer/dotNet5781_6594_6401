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

namespace PL
{
    /// <summary>
    /// Interaction logic for LineSchedulePage.xaml
    /// </summary>
    public partial class LineSchedulePage : Page
    {
        public LineSchedulePage()
        {
            InitializeComponent();
            lbLineSchedules.DataContext = MainWindow.lineSchedulesCollection;
            lbLineSchedules.SelectedIndex = 0;
            ScheduleInfoGrid.DataContext = MainWindow.lineSchedulesCollection.ElementAt(0);
            mainGrid.DataContext = MainWindow.Language;
            //linescb.DataContext = from ls in MainWindow.lineSchedulesCollection
            //                      group line = ls.Clone()
            //                      where MainWindow.lineSchedulesCollection.Remove(ls)
            //                      where !(MainWindow.lineSchedulesCollection.Contains(line))
            //                      select line.LineNumber;
        }

        private void lbLineSchedules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbLineSchedules.SelectedIndex >= 0)
                ScheduleInfoGrid.DataContext = MainWindow.lineSchedulesCollection.ElementAt(lbLineSchedules.SelectedIndex);
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            int index = lbLineSchedules.SelectedIndex;
            try
            {
                UpdateLineScheduleWindow updateLineScheduleWindow = new UpdateLineScheduleWindow(MainWindow.lineSchedulesCollection[lbLineSchedules.SelectedIndex]);
                updateLineScheduleWindow.ShowDialog();
                //Sort();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Please choose a bus!", "UPDATE BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.No);
            }
            lbLineSchedules.SelectedIndex = index;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LineSchedule lineSchedule = lbLineSchedules.SelectedItem as LineSchedule;
                int key = lineSchedule.LineKey;
                MessageBoxResult mbResult = MessageBox.Show($"Are you sure you want to delete bus {key}?", "DELETE BUS MESSAGE", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (mbResult == MessageBoxResult.Yes)
                {
                    App.bl.DeleteLineSchedule(key, lineSchedule.StartTime);
                    MainWindow.lineSchedulesCollection.Remove(lineSchedule);
                    MessageBox.Show($"Line schedule of line {key} was deleted.", "DELETE LINE SCHEDULE MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);

                }
                else
                    MessageBox.Show($"Delete operation was canceled.", "DELETE LINE SCHEDULE MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);

            }
            catch (BO.BOArgumentNotFoundException ex)
            {
                MessageBox.Show($"{ex.Message}", "DELETE LINE SCHEDULE MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.No);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Please choose a bus!", "DELETE LINE SCHEDULE MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.No);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddLineScheduleWindow addLineScheduleWindow = new AddLineScheduleWindow();
            addLineScheduleWindow.ShowDialog();
        }

        private void linescb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var groupByLineNumber = from ls in MainWindow.lineSchedulesCollection
                                    group ls by ls.LineNumber into newGroup
                                    //orderby newGroup.Key
                                    select newGroup;
            lbLineSchedules.DataContext = groupByLineNumber.FirstOrDefault(ls => ls.Key == int.Parse(linescb.SelectedItem.ToString()));
        }

        private void cbLineSchedule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    //    void filter()
    //    {
    //        foreach (var item in MainWindow.lineSchedulesCollection)
    //        {
    //            ListBoxItem ls = (ListBoxItem)lbLineSchedules.ItemContainerGenerator.ContainerFromItem(item);
    //            //ContainerFromItem(item);
    //            //String searchS = searchBox.Text;
    //            //int num = searchS.Length;

    //            if (ls != null)
    //            {
    //                if (checkBusLineNumber(item) && checkLineNumberInSearchBox(item))
    //                {
    //                    bus.Visibility = Visibility.Visible;
    //                }
    //                else
    //                    bus.Visibility = Visibility.Collapsed;
    //            }
    //        }
    //    }
    //    bool checkLineNumberInSearchBox(BusLine item)
    //    {
    //        String searchS = searchBox.Text;
    //        int num = searchS.Length;
    //        return (num <= item.LineNumber.ToString().Length
    //               && searchS == (item as BusLine).LineNumber.ToString().Substring(0, num));
    //    }
    //    bool checkLineNumber(BusLine item)
    //    {
    //        var queryLastNames =    from student in students
    //group student by student.LastName into newGroup
    //orderby newGroup.Key
    //select newGroup;
    //    }

    }
}
