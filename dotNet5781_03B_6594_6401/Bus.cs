﻿using Microsoft.Win32;
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
    public class Bus
    {
        int _KM = 0;
        string _licenseNumber = "";
        int _fuel = 0;
        int _beforeTreatKM = 0;
        DateTime _runningDate = new DateTime();
        DateTime _lastTreatment = new DateTime();
        public BackgroundWorker activity;        
        Status _busStatus;
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
            set { _busStatus = value; }
        }
        public void ApdateStatus()
        {
            if (CanDoRide(0))
                BusStatus = Status.ready;
            else
                BusStatus = Status.notReady;
        }
        public string LicenseNumberFormat { get { return GetLicenseNumberFormat(); } }
        public int KM
        {
            get { return _KM; }
            set { _KM = value; }
        }
        public DateTime LastTreatment
        {
            get { return _lastTreatment; }
            set { _lastTreatment = value; }
        }
        public string LicenseNumber
        {
            get { return _licenseNumber; }
            set { _licenseNumber = value; }
        }
        public int Fuel
        {
            get { return _fuel; }
            set { _fuel = value; }
        }
        public int BeforeTreatKM
        {
            get { return _beforeTreatKM; }
            set { _beforeTreatKM = value; }
        }
        public DateTime RunningDate
        {
            get { return _runningDate; }
            set { _runningDate = value; }
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
            _fuel = f;
            _KM = km;
            _beforeTreatKM = bt;
            if (NeedTreatment())
            {
                _busStatus = Status.notReady;
            }
            activity = new BackgroundWorker();
            activity.DoWork += Activity_DoWork;
            activity.ProgressChanged += Activity_ProgressChanged;
            activity.RunWorkerCompleted += Activity_RunWorkerCompleted;
            activity.WorkerReportsProgress = true;
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
                        Thread.Sleep(12000);
                        break;
                    }
                case Status.Treatment:
                    {
                        Thread.Sleep(144000);
                        break;
                    }
                case Status.Ride:
                    {
                        Random rnd = new Random();
                        double time = (double)50 / rnd.Next(30, 60);
                        int timeToSleep = (int)(time * 6000);
                        Thread.Sleep(timeToSleep);
                        break;
                    }
                default:
                    { break; }
            }
            //Thread.Sleep(12000);
            activity.ReportProgress((int)e.Argument);
            Button b = pressedButton;
            e.Result = b;
        }
        public bool IsBusBusy() { return activity.IsBusy; }

        //refuel to the maximum possible - 1200
        public void Refuel()
        {
            _fuel = 1200;
        }
        ///treatment function
        ///does:
        ///-adding to general killometrag
        ///-reseting temprery killometrag
        ///-updating last treatment date
        public void DoTreatment()
        {
            _KM += _beforeTreatKM;
            _beforeTreatKM = 0;
            _lastTreatment = DateTime.Now;
            if (_fuel == 0)
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
            _fuel -= RideKM; //update of fields if the ride happened
            _KM += RideKM;
            _beforeTreatKM += RideKM;
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

    }


}
