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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        public string userName;
        public string password;
        public ManagerPage(string un, string pw)
        {
            InitializeComponent();
            userName = un;
            password = pw;
        }
        private void profileButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Content = new Profile(userName, password);
        }

        private void busLinesButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Content = new BusLinePage();

        }

        private void stationsButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Content = new StationPage();
        }

        private void busesButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Content = new BusPage();
        }

        private void configurationButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}