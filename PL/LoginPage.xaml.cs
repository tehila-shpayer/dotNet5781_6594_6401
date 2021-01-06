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
using BLAPI;

namespace PL
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        //static IBL bl = BLFactory.GetBL("1");
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
        #region focus
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
        #endregion
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    BO.User user =  App.bl.GetUser(userName.Text, Password.Password);
            //    if(user.AuthorizationManagement == BO.AuthorizationManagement.Manager)
                    currentPage.NavigationService.Navigate(new ManagerPage(userName.Text, Password.Password));
            //    else
            //        currentPage.NavigationService.Navigate(new TravelerPage(userName.Text, Password.Password));
            //}
            //catch (BO.BOArgumentNotFoundException ex)
            //{
            //    spProblem.Visibility = Visibility.Visible;
            //}
        }

        private void NewAccountButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Content = new SignUpPage();
        }
    }
}
