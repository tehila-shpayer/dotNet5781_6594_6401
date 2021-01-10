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
        public Button currentButton;
        Brush mainBlueColor;
        public ManagerPage(string un, string pw)
        {
            InitializeComponent();
            userName = un;
            password = pw;
            mainBlueColor = profileButton.Background;
        }
        void changeColors( Button b)
        {
            b.Background = Brushes.White;
            b.Foreground = mainBlueColor;
            if (currentButton != null && currentButton != b)
            {
                currentButton.Background = mainBlueColor;
                currentButton.Foreground = Brushes.White;
            }
            currentButton = b;
        }
        private void profileButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Content = new ProfilePage(userName, password);
            changeColors(profileButton);
        }

        private void busLinesButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Content = new BusLinePage();
            changeColors(busLinesButton);
        }

        private void stationsButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Content = new StationPage();
            changeColors(stationsButton);
        }

        private void busesButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Content = new BusPage();
            changeColors(busesButton);
        }

        private void configurationButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
