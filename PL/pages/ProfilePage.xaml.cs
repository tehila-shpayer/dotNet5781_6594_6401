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
using Microsoft.Win32;

namespace PL
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public string userName;
        public User user;
        public BO.User userBO;
        public ProfilePage(User _user)
        {
            InitializeComponent();
            user = _user;
            ProfilGrid.DataContext = user;
            try
            {
                if (user.Picture != null)
                    imgPhoto.Source = new BitmapImage(new Uri(user.Picture));
            }
            catch(Exception ex)
            {
                MessageBox.Show("Can't upload photo.", "PROFILE PHOTO UPLOAD", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            userBO = new BO.User();
            user.Clone(userBO);
            mainGrid.DataContext = MainWindow.Language;
        }
        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordButton.Visibility = Visibility.Collapsed;
            OldPassword.Visibility = Visibility.Visible;
            tbOldPassword.Visibility = Visibility.Visible;
            NewPassword.Visibility = Visibility.Visible;
            tbNewPassword.Visibility = Visibility.Visible;
            saveUndoPasswordButtons.Visibility = Visibility.Visible;
        }

        #region focus
        private void OldPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbOldPassword.Text == " Old Password")
            {
                tbOldPassword.Text = "";
            }
        }
        private void OldPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (OldPassword.Password == "")
            {
                tbOldPassword.Text = " Old Password";
            }
        }
        private void NewPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NewPassword.Password == "")
            {
                tbNewPassword.Text = " New Password";
            }
        }
        private void NewPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbNewPassword.Text == " New Password")
            {
                tbNewPassword.Text = "";
            }
        }
        #endregion
        private void undoChangeButton_Click(object sender, RoutedEventArgs e)
        {
            user = PoBoAdapter.UserPoBoAdapter(userBO);
            ProfilGrid.DataContext = user;
            VisibilityTextBlock();
        }

        private void saveChangeButton_Click(object sender, RoutedEventArgs e)
        {
            user.Clone(userBO);
            App.bl.UpdateUser(userBO);
            VisibilityTextBlock();
        }


        private void undoPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            VisibilityChangePasswordButton();
        }

        private void savePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (Tools.hashPassword(OldPassword.Password + user.Salt) != user.Password)
            {
                MessageBox.Show("Incorect password!\nPlease try again", "UPDATE PASSWORD MESSAGE", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            userBO.Password = NewPassword.Password;
            try
            {
                App.bl.UpdateUser(userBO);
                user = PoBoAdapter.UserPoBoAdapter(userBO);
                VisibilityChangePasswordButton();
            }
            catch (BO.BOInvalidInformationException ex)
            {
                MessageBox.Show("Can't change password!\n" + ex.Message + "\nPlease choose another password", "UPDATE PASSWORD MESSAGE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void uploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }
            else
                return;
            user.Picture = imgPhoto.Source.ToString();
            userBO.Picture = imgPhoto.Source.ToString();
            App.bl.UpdateUser(userBO);
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            editButton.Visibility = Visibility.Collapsed;
            saveUndoChangeButtons.Visibility = Visibility.Visible;
            VisibilityTextBox();
            string s = "eeee";
        }
        private void VisibilityTextBox()
        {
            firstNameTextBlock.Visibility = Visibility.Collapsed;
            lastNameTextBlock.Visibility = Visibility.Collapsed;
            emailTextBlock.Visibility = Visibility.Collapsed;
            phoneNumberTextBlock.Visibility = Visibility.Collapsed;
            addressTextBlock.Visibility = Visibility.Collapsed;

            firstNameTextBox.Visibility = Visibility.Visible;
            lastNameTextBox.Visibility = Visibility.Visible;
            emailTextBox.Visibility = Visibility.Visible;
            phoneNumberTextBox.Visibility = Visibility.Visible;
            addressTextBox.Visibility = Visibility.Visible;
        }
        private void VisibilityTextBlock()
        {
            editButton.Visibility = Visibility;
            saveUndoChangeButtons.Visibility = Visibility.Collapsed;

            firstNameTextBlock.Visibility = Visibility.Visible;
            lastNameTextBlock.Visibility = Visibility.Visible;
            emailTextBlock.Visibility = Visibility.Visible;
            phoneNumberTextBlock.Visibility = Visibility.Visible;
            addressTextBlock.Visibility = Visibility.Visible;

            firstNameTextBox.Visibility = Visibility.Collapsed;
            lastNameTextBox.Visibility = Visibility.Collapsed;
            emailTextBox.Visibility = Visibility.Collapsed;
            phoneNumberTextBox.Visibility = Visibility.Collapsed;
            addressTextBox.Visibility = Visibility.Collapsed;
        }
        private void VisibilityChangePasswordButton()
        {
            OldPassword.Password = "";
            NewPassword.Password = "";
            tbOldPassword.Text = " Old Password";
            tbNewPassword.Text = " New Password";
            ChangePasswordButton.Visibility = Visibility;
            OldPassword.Visibility = Visibility.Collapsed;
            tbOldPassword.Visibility = Visibility.Collapsed;
            NewPassword.Visibility = Visibility.Collapsed;
            tbNewPassword.Visibility = Visibility.Collapsed;
            saveUndoPasswordButtons.Visibility = Visibility.Collapsed;

        }
    }
}
