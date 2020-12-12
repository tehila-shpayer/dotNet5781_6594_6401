using Microsoft.Win32;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading;
using System.Globalization;


namespace dotNet5781_03B_6594_6401
{
    public enum Status { ready, notReady, Ride, Refueling, Treatment}
    public class Bus :INotifyPropertyChanged
    {
        
        string _licenseNumber = "";
        DateTime _runningDate = new DateTime(2000, 1, 1);
        DateTime _lastTreatment = new DateTime(2000, 1, 1);
        int _fuel = 0;
        int _KM = 0;
        int _beforeTreatKM = 0;
        Status _busStatus;

        public BackgroundWorker activity;      
        public BackgroundWorker timer;
        String _time;
        bool _isAvailibleForRide;
        public event PropertyChangedEventHandler PropertyChanged;

        public string LicenseNumber
        {
            get { return _licenseNumber; }
            set { _licenseNumber = value; }
        }
        public string LicenseNumberFormat { get { return GetLicenseNumberFormat(); } }
        public DateTime RunningDate
        {
            get { return _runningDate; }
            set { _runningDate = value; }
        }
        public DateTime LastTreatment
        {
            get { return _lastTreatment; }
            set { _lastTreatment = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LastTreatment"));
                }
            }
        }
        public int Fuel
        {
            get { return _fuel; }
            set
            {
                _fuel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Fuel"));
                }
            }
        }
        public int KM
        {
            get { return _KM; }
            set 
            { 
                _KM = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("KM"));
                }
            }
        }
        public int BeforeTreatKM
        {
            get { return _beforeTreatKM; }
            set { _beforeTreatKM = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("BeforeTreatKM"));
                }
            }
        }
        
        public String Time
        {
            get { return _time; }
            set {
                _time = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Time"));
                }
            }
        }
        
        
        public Button pressedButton { get; set; }
        public bool IsAvailibleForRide 
        {
            get
            {
                if (_fuel == 0 || NeedTreatment())
                    return false;
                else
                    return true;
            }
        }
        public Status BusStatus
        {
            get { return _busStatus; }
            set { _busStatus = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("BusStatus"));
                }
            }
        }
        public void ApdateStatus()
        {
            if (CanDoRide(0))
                BusStatus = Status.ready;
            else
                BusStatus = Status.notReady;
        }
        
        /// <summary>
        /// returns the license number in the format
        /// xxx-xx-xx or xx-xxx-xx (depends on start date)
        /// </summary>
        /// <returns></returns>
        public string GetLicenseNumberFormat()
        {
            string s = _licenseNumber;
            //if (_runningDate.Year >= 2018)
            if (s.Length == 8)
            {
                s = $"{s[0]}{s[1]}{s[2]}-{s[3]}{s[4]}-{s[5]}{s[6]}{s[7]}";
            }
            else
            {
                s = $"{s[0]}{s[1]}-{s[2]}{s[3]}{s[4]}-{s[5]}{s[6]}";
            }
            return s;
        }
        //constructor
        public Bus(DateTime d = new DateTime(), string num = "00000000",int f=0,int km=0,int bt=0)
        {
            _runningDate = d;
            _lastTreatment = d;
            _licenseNumber = num;
            _runningDate = d;
            Fuel = f;
            _KM = km;
            _beforeTreatKM = bt;
            Time = "";
            if (NeedTreatment())
            {
                BusStatus = Status.notReady;
            }
            activity = new BackgroundWorker();
            activity.DoWork += Activity_DoWork;
            activity.ProgressChanged += Activity_ProgressChanged;
            activity.RunWorkerCompleted += Activity_RunWorkerCompleted;
            activity.WorkerReportsProgress = true;
            timer = new BackgroundWorker();
            timer.DoWork += Timer_DoWork;
            timer.ProgressChanged += Timer_ProgressChanged;
            timer.RunWorkerCompleted += Timer_RunWorkerCompleted;
            timer.WorkerReportsProgress = true;
        }

        private void Activity_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (BusStatus)
            {
                case Status.Refueling:
                    {
                        MessageBox.Show("Refuel proccess has successfully ended!", "Fuel Massage", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case Status.Treatment:
                    {
                        MessageBox.Show("Treating proccess has successfully ended!", "Treatment Massage", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case Status.Ride:
                    {
                        String s = "Bus " + GetLicenseNumberFormat() + " has successfully finished the ride!";
                        MessageBox.Show(s, "Ride Massage", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                default:
                    { break; }
            }
            ((Button)e.Result).IsEnabled = true;
            ApdateStatus();
        }

        private void Activity_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (BusStatus)
            {
                case Status.Refueling:
                    {
                        Refuel();
                        break;
                    }
                case Status.Treatment:
                    {
                        DoTreatment();
                        break;
                    }
                case Status.Ride:
                    {
                        Ride(e.ProgressPercentage);
                        break;
                    }
                default:
                    { break; }
            }
            
        }

        private void Activity_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (BusStatus)
            {
                case Status.Refueling:
                    {
                        timer.RunWorkerAsync(6);
                        Thread.Sleep(6000);
                        break;
                    }
                case Status.Treatment:
                    {
                        timer.RunWorkerAsync(13);
                        Thread.Sleep(13000);
                        break;
                    }
                case Status.Ride:
                    {
                        int KMride = (int)e.Argument;
                        Random rnd = new Random();
                        double Hours = (double) KMride/ rnd.Next(20, 50);
                        int secondToSleep = (int)(Hours * 6);
                        timer.RunWorkerAsync(secondToSleep);
                        Thread.Sleep(secondToSleep*1000);
                        break;
                    }
                default:
                    { break; }
            }
            
            activity.ReportProgress((int)e.Argument);
            Button b = pressedButton;
            e.Result = b;
        }
        public bool IsBusBusy() { return activity.IsBusy; }

        public void Timer_DoWork(object sender, DoWorkEventArgs e)
        {
            int time = (int)e.Argument;
            for (int i = time; i > 0; i--)
            {
                timer.ReportProgress(i);
                Thread.Sleep(1000);
            }
        }
        public void Timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            Time = "ready in "+ TimeFormat(progress);
        }
        public void Timer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Time = "";
        }

        //refuel to the maximum possible - 1200
        public void Refuel()
        {
            Fuel = 1200;
        }
        ///treatment function
        ///does:
        ///-adding to general killometrag
        ///-reseting temprery killometrag
        ///-updating last treatment date
        public void DoTreatment()
        {
            KM += BeforeTreatKM;
            BeforeTreatKM = 0;
            LastTreatment = DateTime.Now;
            if (Fuel == 0)
                Refuel();
            //Console.WriteLine("The bus was successfully treated!\n");
        }
        /// <summary>
        /// Performs a bus ride by updating the 
        /// appropriate bus fields in the system.
        /// </summary>
        /// <param name="rideKM"></param>
        /// <returns>The appropriate message for wether the ride happened or not and why</returns>
        public void Ride(int RideKM)
        {//Check if the ride is allowed and sending messages accordingly
           // if ((_fuel < rideKM) && (NeedTreatment()))
           //     IsAvailibleForRide = false;
           //     //return "The system couldn't take this bus for the ride.\nThe bus doesn't have enough fuel and must to be treated first.\n";
           // if (_fuel < rideKM)
           //     IsAvailibleForRide = false;
           //// return "The system couldn't take this bus for the ride.\nThe bus doesn't have enough fuel\n";
           // if (NeedTreatment())
           //     IsAvailibleForRide = false;
            //return "The system couldn't take this bus for the ride.\nThe bus must to be treated first.\n";
            Fuel -= RideKM; //update of fields if the ride happened
            KM += RideKM;
            BeforeTreatKM += RideKM;
            //return "Have a nice ride!\n";
        }
        public bool CanDoRide(int KMtoRide)
        {
            if (_fuel - KMtoRide < 0 || _beforeTreatKM+KMtoRide >= 20000 || NeedTreatment())
                return false;
            return true;
        }


        ///cheks if the bus needs treatment:
        ///(maximum temprery kilometrag crossed (over 20000)
        ///or a year past since last treatment)
        public bool NeedTreatment()
        {
            return (((DateTime.Now - _lastTreatment).TotalDays > 365) || (_beforeTreatKM >= 20000));
        }
        /// <summary>
        /// Override of ToString: 
        /// printing all data of the bus concerning traveling since last treatment,
        /// license number and start date.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return $"Bus license number: {GetLicenseNumberFormat()}\n" +
                                $"Bus start date: {DateWithoutHour(_runningDate)}\n" +
                                $"Bus state since last tratment on {DateWithoutHour(_lastTreatment)}:\n" +
                                $" Fuel state (KM to go): {_fuel}\n" +
                                $" KM: {_beforeTreatKM}\n";
        }
        // A string of a DateTime varible that doesnt include the time
        static public String DateWithoutHour(DateTime date)
        {
            String dateString = date.Day + "/" + date.Month + "/" + date.Year;
            return dateString;
        }
        static public String TimeFormat(int seconds)
        {
            int hour = seconds / 3600;
            int minute = seconds / 60;
            int sec = seconds % 60;
            return (f(hour) + ":" + f(minute) + ":" + f(sec));
        }
        static public String f(int t)
        {
            String s = "";
            if (t < 10)
                s = "0" + t;
            else
                s = "" + t;
            return s;
        }
    }


}
