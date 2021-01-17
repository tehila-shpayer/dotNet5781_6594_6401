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
using System.Net.Mail;
using System.Net;
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
        static Random rand;
        private void forgotPassword_Click(object sender, RoutedEventArgs e)
        {
            if (userName.Text == " User name")
            {
                ProblemMessage.Text = "First enter your user name";
                spProblem.Visibility = Visibility.Visible;
                return;
            }
            spProblem.Visibility = Visibility.Collapsed;
            try
            {
                BO.User user = App.bl.GetUser(userName.Text);

                String newPassword = "";
                rand = new Random();
                for (int i = 0; i < 6; i++)//create a random password
                {
                    int n = rand.Next(0, 62);
                    if (n <= 9)
                        newPassword += n;
                    else if (n < 36)
                        newPassword += (char)(n + 55);
                    else if (n < 62)
                        newPassword += (char)(n + 61);
                }
                user.Password = newPassword;
                App.bl.UpdateUser(user);

                MailMessage mail = new MailMessage();
                mail.To.Add(user.Email);
                mail.From = new MailAddress("netivimcompany@gmail.com", "חברת נתיבים");
                mail.Subject = "אתחול סיסמה";
                mail.Body = "שלום, "
                    +"הסיסמה החדשה שלך היא: " + newPassword;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("netivimcompany@gmail.com", "100project");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                MessageBox.Show("A new password has been sent to you by email\nYou must use this password to log in.", "Security Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BOArgumentNotFoundException)
            {
                ProblemMessage.Text = "User name are incorrect. please try again";
                spProblem.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            spProblem.Visibility = Visibility.Collapsed;
        }


        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Text == " Password")
            {
                tbPassword.Text = "";
            }
            spProblem.Visibility = Visibility.Collapsed;
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
            try
            {
                BO.User user = App.bl.GetUser(userName.Text, Password.Password);
                if (user.AuthorizationManagement == BO.AuthorizationManagement.Manager)
                    NavigationService.Navigate(new ManagerPage(PoBoAdapter.UserPoBoAdapter(user)));
                else
                    NavigationService.Navigate(new TravelerPage(userName.Text, Password.Password));
            }
            catch (BO.BOArgumentNotFoundException ex)
            {
                ProblemMessage.Text = "User name or password are incorrect.\n try again";
                spProblem.Visibility = Visibility.Visible;
            }
            //BO.User user = App.bl.GetUser(userName.Text, Password.Password);
            //NavigationService.Navigate(new ManagerPage(PoBoAdapter.UserPoBoAdapter(new BO.User())));
        }

        private void NewAccountButton_Click(object sender, RoutedEventArgs e)
        {
           currentPage.Content = new SignUpPage();
        }
    }
}
