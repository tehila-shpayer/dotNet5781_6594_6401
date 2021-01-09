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
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        public BO.Bus bus;
        public AddBusWindow()
        {
            InitializeComponent();
            bus = new BO.Bus();
            //bus.Status = BO.Status.NotReady;
            bus.RunningDate = DateTime.Now;
            bus.LastTreatment = DateTime.Now;
            grid1.DataContext = bus;
        }

        private void addBusButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bus busPO = new Bus();
                App.bl.AddBus(bus);
                busPO = PoBoAdapter.BusPoBoAdapter(bus);
                MainWindow.busesCollection.Add(busPO);
                MessageBox.Show($"Bus added successfully!", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch( BO.BOInvalidInformationException ex)
            {
                MessageBox.Show($"{ex.Message}", "ADD BUS MESSAGE", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }

        }

        private void lastTreatmentFormatTextBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //UpdateStatus();
        }

        private void beforeTreatKMTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //UpdateStatus();
        }
        void UpdateStatus()
        {
            //if (bus.BeforeTreatKM > 20000 || (DateTime.Now - bus.LastTreatment).TotalDays > 365)
            //    statusTextBlock.Text = "Not Ready";
            //else
            //    statusTextBlock.Text = "Ready";
        }

        private void licenseNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (licenseNumberTextBox.Text == "")
                addBusButton.IsEnabled = false;
            else
                addBusButton.IsEnabled = true;
        }
    }
}
