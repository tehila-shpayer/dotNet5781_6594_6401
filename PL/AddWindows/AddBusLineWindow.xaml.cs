﻿using System;
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
            firstStationComboBox.DisplayMemberPath = "  ShowNameKey  ";
            lastStationComboBox.DisplayMemberPath = "  ShowNameKey  ";
            firstStationComboBox.SelectedIndex = 0;
            lastStationComboBox.SelectedIndex = 0;
            grid1.DataContext = busLineBO;
            mainGrid.DataContext = MainWindow.Language;
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
                int firstKey = (firstStationComboBox.SelectedItem as Station).Key;
                int lastKey = (lastStationComboBox.SelectedItem as Station).Key;
                if (firstKey == lastKey)
                    throw new InvalidOperationException("ERROR: Invalid Information\nFirst station and last station must be different!");
                busLineBO.Area = (BO.Areas)(Areas)AreasString.IndexOf(areaComboBox.SelectedItem.ToString());
                busLineBO.Key = App.bl.AddBusLine(busLineBO, firstKey, lastKey);
                MessageBox.Show($"Bus line added successfully!", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
                
            }
            catch (BO.BOArgumentNotFoundException ex)
            {
                MessageBox.Show($"Can't add bus line\n" + ex.ToString(), "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can't add bus line\n" +ex.Message, "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
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

        #region Input Validation
        /// <summary>
        ///  קו אוטובוס חייב להיות בעל מספר כלשהו
        /// </summary>
        private void lineNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string t = lineNumberTextBox.Text;
            if (t != "" && int.Parse(t) > 0)
                addButton.IsEnabled = true;
            else
                addButton.IsEnabled = false;
        }
        /// <summary>
        /// מספר קו אוטובוס חייב להיות מספר חיובי שלם
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lineNumberTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            PL.PreviewKeyDown.GeneralPerviewKeyDown(sender, e);
        }
        #endregion
    }
}
