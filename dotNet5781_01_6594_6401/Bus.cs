using Microsoft.Win32;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6594_6401
{
    class Bus
    {
        int _KM;
        string _licenseNumber;
        int _fuel;
        int _beforeTreatKM;
        DateTime _runningDate;
        DateTime _lastTreatment;
        public int KM
        { 
            get { return _KM; }
        }
        public DateTime LastTreatment
        {
            get { return _lastTreatment; }
        }
        public string LicenseNumber
        {
            get { return _licenseNumber; }
        }
        public int Fuel
        {
            get { return _fuel; }
        }
        public int BeforeTreatKM
        {
            get { return _beforeTreatKM; }
        }
        public DateTime RunningDate
        {
            get { return _runningDate; }
        }
        public string GetLicenseNumberFormat()
        {
            string s = _licenseNumber;
            if (_runningDate.Year >= 2018)
            {
                 s = $"{s[0]}{s[1]}{s[2]}-{s[3]}{s[4]}-{s[5]}{s[6]}{s[7]}";
            }
            else
            {
                s = $"{s[0]}{s[1]}-{s[2]}{s[3]}{s[4]}-{s[5]}{s[6]}";
            }
            return s;
        }
        public Bus(DateTime d, string num="")
        {
            _lastTreatment = d;
            _licenseNumber = num;
            _runningDate = d;
            _fuel = 0;
            _KM = 0;
            _beforeTreatKM = 0;
        }
        public void Refuel() { 
            _fuel = 1200;
            Console.WriteLine("The fuel tank was successfully refueled!\n");
        }
        public void DoTreatment()
        {
            _KM += _beforeTreatKM;
            _beforeTreatKM = 0;
            _lastTreatment = DateTime.Now;
            Console.WriteLine("The bus was successfully treated!\n");
        }
        public String Ride(int rideKM)
        {
            if ((_fuel < rideKM) && (NeedTreatment()))
                return "The system couldn't take this bus for the ride.\nThe bus doesn't have enough fuel and must to be treated first.\n";
            if (_fuel < rideKM)
                return "The system couldn't take this bus for the ride.\nThe bus doesn't have enough fuel\n";
            if (NeedTreatment())
                return "The system couldn't take this bus for the ride.\nThe bus must to be treated first.\n";
            _fuel -= rideKM;
            _KM += rideKM;
            _beforeTreatKM += rideKM;
            return "Have a nice ride!\n";
        }
        public void RefuelAndTreat()
        {
            Refuel();
            DoTreatment();
        }
        public bool NeedTreatment()
        {
            return (((DateTime.Now - _lastTreatment).TotalDays > 365) || (_beforeTreatKM > 20000));
        }
        public override String ToString()
        {
            return $"Bus license number: {GetLicenseNumberFormat()}\n" +
                                $"Bus start date: {DateWithoutHour(_runningDate)}\n" +
                                $"Bus state since last tratment on {DateWithoutHour(_lastTreatment)}:\n" +
                                $" Fuel state (KM to go): {_fuel}\n" +
                                $" KM: {_beforeTreatKM}\n";
        }
        static public String DateWithoutHour(DateTime date)
        {
            String dateString = date.Day + "/" + date.Month + "/" + date.Year;
            return dateString;
        }
    }

}
