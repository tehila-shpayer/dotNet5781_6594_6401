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
    class Buss
    {
        int _KM;
        string _carNumber;
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
        public string carNumber
        {
            get { return _carNumber; }
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
        public string getCarNumberFormat()
        {
            string s = _carNumber;
            if (_runningDate.Year >= 2018)
            {
                 s = $"{s[0]}{s[1]}{s[2]}-{s[3]}{s[4]}-{s[5]}{s[6]}{s[7]}";
                //Console.WriteLine("{0} its after 2018", num);
            }
            else
            {
                s = $"{s[0]}{s[1]}-{s[2]}{s[3]}{s[4]}-{s[5]}{s[6]}";
                //Console.WriteLine("{0} its before 2018", num);
            }
            return s;
        }
        public Buss(DateTime d,string num="")
        {
            _lastTreatment = d;
            _carNumber = num;
            _runningDate = d;
            _fuel = 0;
            _KM = 0;
            _beforeTreatKM = 0;
        }
        public void RefillFuel() { _fuel = 1200; }
        public void DoTreatment()
        {
            _KM += beforeTreatKM;
            _beforeTreatKM = 0;
            _lastTreatment = DateTime.Now;
        }
        public bool Ride(int rideKM)
        {
            if (_fuel < rideKM || _beforeTreatKM > 20000)
                return false;
            _fuel -= rideKM;
            _KM += rideKM;
            _beforeTreatKM += rideKM;
            return true;
        }
        public override String ToString()
        {
            return $"Bus license number: {getCarNumberFormat()}\n" +
                                $"Bus start date: {runningDate}\n" +
                                $"Bus state since last tratment on {lastTreatment}:\n" +
                                $" Fuel state (KM to go): {fuel}\n" +
                                $" KM: {beforeTreatKM}\n";
        }
    }
}
