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
    /// Interaction logic for TravelerMenuPage.xaml
    /// </summary>
    public partial class TravelerMenuPage : Page
    {
        public string userName;
        public User user;
        public Button currentButton;
        Brush mainBlueColor;
        public TravelerMenuPage(User _user)
        {

            InitializeComponent();
            mainBlueColor = profileButton.Background;
            user = _user;
            WelcomPage welcomPage = new WelcomPage();
            currentPage.Content = welcomPage;
            mainGrid.DataContext = MainWindow.Language;
        }
        void changeColors(Button b)
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
            currentPage.Content = new ProfilePage(user);
            changeColors(profileButton);
        }

        private void simulationButton_Click(object sender, RoutedEventArgs e)
        {

            currentPage.Content = new TravelerPage();
            changeColors(simulationButton);
        }

    }
}

