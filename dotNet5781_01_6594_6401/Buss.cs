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
        public DateTime lastTreatment
        {
            get { return _lastTreatment; }
        }
        public string licenseNumber
        {
            get { return _licenseNumber; }
        }
        public int fuel
        {
            get { return _fuel; }
        }
        public int beforeTreatKM
        {
            get { return _beforeTreatKM; }
        }
        public DateTime runningDate
        {
            get { return _runningDate; }
        }
        public string getLicenseNumberFormat()
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
            _KM += beforeTreatKM;
            _beforeTreatKM = 0;
            _lastTreatment = DateTime.Now;
            Console.WriteLine("The bus was successfully treated!\n");
        }
        public String Ride(int rideKM)
        {
            if ((_fuel < rideKM) && (NeedTreatment()))
                return "The system couldn't take this bus for the ride.\nThe bus doesn't have enough fuel and has to get a treatment.\n";
            if (_fuel < rideKM)
                return "The system couldn't take this bus for the ride.\nThe bus doesn't have enough fuel\n";
            if (NeedTreatment())
                return "The system couldn't take this bus for the ride.\nThe bus has to get a treatment.\n";
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
            return (((DateTime.Now - lastTreatment).TotalDays > 365) || (_beforeTreatKM > 20000));
        }
        public override String ToString()
        {
            return $"Bus license number: {getLicenseNumberFormat()}\n" +
                                $"Bus start date: {dateWithoutHour(runningDate)}\n" +
                                $"Bus state since last tratment on {dateWithoutHour(lastTreatment)}:\n" +
                                $" Fuel state (KM to go): {fuel}\n" +
                                $" KM: {beforeTreatKM}\n";
        }
        static public String dateWithoutHour(DateTime date)
        {
            String dateString = date.Day + "/" + date.Month + "/" + date.Year;
            return dateString;
        }
    }

}
