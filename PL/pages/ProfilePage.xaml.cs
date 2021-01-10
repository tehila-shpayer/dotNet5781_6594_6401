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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public string userName;
        public string password;
        public ProfilePage(string un, string pw)
        {
            InitializeComponent();
            userName = un;
            password = pw;
        }

        private void testb_Click(object sender, RoutedEventArgs e)
        {
            oneblock.Visibility = Visibility.Hidden;
            onebox.Visibility = Visibility.Visible;
        }

        private void oldPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbOldPassword.Text == " Old password")
            {
                tbOldPassword.Text = "";
            }
        }

        private void oldPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (oldPassword.Password == "")
            {
                tbOldPassword.Text = " Old Password";
            }
        }

        private void newPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbNewPassword.Text == " New password")
            {
                tbNewPassword.Text = "";
            }
        }

        private void newPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (newPassword.Password == "")
            {
                tbNewPassword.Text = " New Password";
            }
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if ((string)changePasswordButton.Content == "change password")
            {
                changePasswordStack.Visibility = Visibility.Visible;
                changePasswordButton.Content = "save";
            }
            else
            {
                if(oldPassword.Password == password)
                    App.bl.UpdateUser(userName, u => u.Password = newPassword.Password);
                   
            }
        }
    }
}