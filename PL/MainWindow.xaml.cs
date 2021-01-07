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
using System.Net;
using System.Net.Mail;
using BLAPI;
using System.Collections.ObjectModel;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<BusLine> busLinesCollection = new ObservableCollection<BusLine>();
        public static ObservableCollection<Station> stationsCollection = new ObservableCollection<Station>();
        public MainWindow()
        {
            InitializeComponent();
            foreach(BO.BusLine bl in App.bl.GetAllBusLines())
            {
                PL.BusLine busLinePL = new BusLine();
                busLinesCollection.Add(PoBoAdapter.BusLinePoBoAdapter(bl));
            }
            foreach (BO.Station s in App.bl.GetAllStations())
            {
                //PL.Station stationPL = new Station();
                stationsCollection.Add(PoBoAdapter.StationPoBoAdapter(s));
            }
            
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
