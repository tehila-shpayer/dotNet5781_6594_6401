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
        public static ObservableCollection<Bus> busesCollection = new ObservableCollection<Bus>();

        public MainWindow()
        {
            InitializeComponent();
            Height = 600;
            Width = 1024;
            InitializeCollections();
        }
        static public void InitializeCollections()
        {
            InitializeBusLines();
            InitializeBuses();
            InitializeStations();
        }
        static public void InitializeBusLines()
        {
            busLinesCollection.Clear();
            foreach (BO.BusLine bl in App.bl.GetAllBusLines())
            {
                busLinesCollection.Add(PoBoAdapter.BusLinePoBoAdapter(bl));
            }
        }
        static public void InitializeBuses()
        {
            busesCollection.Clear();
            foreach (BO.Bus b in App.bl.GetAllBuses())
            {
                busesCollection.Add(PoBoAdapter.BusPoBoAdapter(b));
            }
        }
        static public void InitializeStations()
        {
            stationsCollection.Clear();
            foreach (BO.Station s in App.bl.GetAllStations())
            {
                stationsCollection.Add(PoBoAdapter.StationPoBoAdapter(s));
            }
        }
    private void powerButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void upGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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

        private void HebrewBtn_Click(object sender, RoutedEventArgs e)
        {
            Uri dictUri = new Uri(@"res/languages/AppString_HE.xaml", UriKind.Relative);
            ResourceDictionary resDict = Application.LoadComponent(dictUri) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(resDict);
        }

        private void EnglishBtn_Click(object sender, RoutedEventArgs e)
        {
            Uri dictUri = new Uri(@"res/languages/AppString_EN.xaml", UriKind.Relative);
            ResourceDictionary resDict = Application.LoadComponent(dictUri) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(resDict);
        }
    }
}
