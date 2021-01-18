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
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
        }
        #region focus
        private void UserName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UserName.Text == " User name")
            {
                UserName.Text = "";
                UserName.Foreground = Brushes.Black;
            }
        }
        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Email.Text == " Email")
            {
                Email.Text = "";
                Email.Foreground = Brushes.Black;
            }
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Text == " Password")
            {
                tbPassword.Text = "";
            }
        }
        private void ConfirmPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbConfirmPassword.Text == " Confirm Password")
            {
                tbConfirmPassword.Text = "";
            }
        }

        private void UserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UserName.Text == "")
            {
                UserName.Text = " User name";
                UserName.Foreground = Brushes.Gray;
            }
        }
        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Email.Text == "")
            {
                Email.Text = " Email";
                Email.Foreground = Brushes.Gray;
            }
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Password == "")
            {
                tbPassword.Text = " Password";
            }
        }
        private void ConfirmPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ConfirmPassword.Password == "")
            {
                tbConfirmPassword.Text = " Confirm Password";
            }
        }
        #endregion
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            spProblem.Visibility = Visibility.Hidden;
            ProblemMessage.Text = "";
            if (UserName.Text == " User name" || Email.Text == " Email" || Password.Password == "" || ConfirmPassword.Password=="")
            {
                ProblemMessage.Text = "You must fill in all the fields!";
                spProblem.Visibility = Visibility.Visible;
                return;
            }
            if (Password.Password != ConfirmPassword.Password)
            {
                ProblemMessage.Text = "The passwordss don't match!";
                spProblem.Visibility = Visibility.Visible;
                return;
            }
            try
            {
                BO.User user = new BO.User();
                user.UserName = UserName.Text;
                user.Email = Email.Text;
                user.Password = Password.Password;
                user.AuthorizationManagement = BO.AuthorizationManagement.Traveler;
                App.bl.AddUser(user);
                if (user.AuthorizationManagement == BO.AuthorizationManagement.Traveler)
                    currentPage.NavigationService.Navigate(new TravelerMenuPage(PoBoAdapter.UserPoBoAdapter(new BO.User())));
                else
                    currentPage.NavigationService.Navigate(new ManagerPage(PoBoAdapter.UserPoBoAdapter(new BO.User())));
            }
            catch (BO.BOInvalidInformationException ex)
            {
                if (ex.InnerException != null)
                { ProblemMessage.Text = ex.InnerException.Message + "\n" + ex.Message + " try another user-name"; }
                else
                    ProblemMessage.Text = ex.Message + "\nPlease try again";
                spProblem.Visibility = Visibility.Visible;
                return;
            }
            catch (Exception ex)
            {
                ProblemMessage.Text = ex.Message;
                spProblem.Visibility = Visibility.Visible;
                return;
            }
        }
    }
}
