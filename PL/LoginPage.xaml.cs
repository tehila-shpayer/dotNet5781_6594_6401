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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void forgotPassword_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void forgotPassword_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void userName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (userName.Text == " User name")
            {
                userName.Text = "";
                userName.Foreground = Brushes.Black;
            }
        }


        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Text == " Password")
            {
                tbPassword.Text = "";
            }
        }

        private void userName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (userName.Text == "")
            {
                userName.Text = " User name";
                userName.Foreground = Brushes.Gray;
            }
        }

        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Password == "")
            {
                tbPassword.Text = " Password";
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                currentPage.NavigationService.Navigate(new ManagerPage());
            }
        }
    }
}
