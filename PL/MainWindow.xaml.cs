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
using System.Threading;
using System.Globalization;
using System.Collections.ObjectModel;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Declerations
        public static ObservableCollection<BusLine> busLinesCollection;
        public static ObservableCollection<Station> stationsCollection;
        public static ObservableCollection<Bus> busesCollection;
        public static ObservableCollection<LineSchedule> lineSchedulesCollection;
        public Uri dictUriEN;
        public Uri dictUriHE;
        public Uri dictUriFR;
        public Uri dictUriRU;
        ResourceDictionary resDictEN;
        ResourceDictionary resDictHE;
        ResourceDictionary resDictFR;
        ResourceDictionary resDictRU;
        public static ResourceDictionary Language;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            Height = 600;
            Width = 1024;
            InitializeCollections();
            resDictEN = Application.Current.Resources.MergedDictionaries.FirstOrDefault(a => a.Source.OriginalString == @"/res/languages/AppString_EN.xaml");
            resDictHE = Application.Current.Resources.MergedDictionaries.FirstOrDefault(a => a.Source.OriginalString == @"/res/languages/AppString_HE.xaml");
            resDictFR = Application.Current.Resources.MergedDictionaries.FirstOrDefault(a => a.Source.OriginalString == @"/res/languages/AppString_FR.xaml");
            resDictRU = Application.Current.Resources.MergedDictionaries.FirstOrDefault(a => a.Source.OriginalString == @"/res/languages/AppString_RU.xaml");
            Language = Application.Current.Resources.MergedDictionaries.ElementAt(Application.Current.Resources.MergedDictionaries.Count - 1);
        }
        private void upGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        #region Initializations
        static public void InitializeCollections()
        {
            InitializeBusLines();
            InitializeBuses();
            InitializeStations();
            InitializeLineSchedules();
        }
        static public void InitializeBusLines()
        {
            busLinesCollection = new ObservableCollection<BusLine>((from bl in App.bl.GetAllBusLines()
                                                                   select PoBoAdapter.BusLinePoBoAdapter(bl)).ToList());
        }
        static public void InitializeBuses()
        {
            busesCollection = new ObservableCollection<Bus>(from b in App.bl.GetAllBuses()
                                                                   select PoBoAdapter.BusPoBoAdapter(b));
        }
        static public void InitializeStations()
        {
            stationsCollection = new ObservableCollection<Station>(from s in App.bl.GetAllStations()
                                                                   select PoBoAdapter.StationPoBoAdapter(s));
        }
        static public void InitializeLineSchedules()
        {
            lineSchedulesCollection = new ObservableCollection<LineSchedule>(from ls in App.bl.GetAllLineSchedules()                                                                             select PoBoAdapter.LineSchedulePoBoAdapter(ls));
        }
        #endregion

        #region Buttons
        private void powerButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
            Application.Current.Resources.MergedDictionaries.Remove(resDictEN);
            Application.Current.Resources.MergedDictionaries.Add(resDictHE);
            Language = Application.Current.Resources.MergedDictionaries.ElementAt(Application.Current.Resources.MergedDictionaries.Count - 1);
        }

        private void EnglishBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries.Remove(resDictHE);
            Application.Current.Resources.MergedDictionaries.Add(resDictEN);
            Language = Application.Current.Resources.MergedDictionaries.ElementAt(Application.Current.Resources.MergedDictionaries.Count - 1);
        }
        private void FrenchBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries.Remove(resDictHE);
            Application.Current.Resources.MergedDictionaries.Add(resDictEN);
            Language = Application.Current.Resources.MergedDictionaries.ElementAt(Application.Current.Resources.MergedDictionaries.Count - 1);
        }
        private void RussianBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries.Remove(resDictHE);
            Application.Current.Resources.MergedDictionaries.Add(resDictEN);
            Language = Application.Current.Resources.MergedDictionaries.ElementAt(Application.Current.Resources.MergedDictionaries.Count - 1);
        }

        private void contactUsButton_Click(object sender, RoutedEventArgs e)
        {
            openingPage.Content = new ContactUsPage();
        }
        #endregion
    }
}
