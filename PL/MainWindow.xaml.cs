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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void powerButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            //InitializeComponent();
        }

        private void upGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void languageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
            
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            openingPage.Content = new LoginPage();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            openingPage.Content = new LoginPage();
        }
    }
}
