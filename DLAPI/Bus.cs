using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Bus
    {
        //Bus properties:
        public string LicenseNumber { get; set; }
        public DateTime RunningDate { get; set; }
        public DateTime LastTreatment { get; set; }
        public int Fuel { get; set; }
        public int KM { get; set; }
        public int BeforeTreatKM { get; set; }
        public Status Status { get; set; }
        public bool Exist { get; set; }

        //constructors
        //public Bus()//default ctor
        //{ }
        //public Bus(string num, DateTime d = new DateTime(), int f = 0, int km = 0, int bt = 0)//parameters ctor
        //{
        //    LicenseNumber = num;
        //    RunningDate = d;
        //    LastTreatment = d;
        //    Fuel = f;
        //    KM = km;
        //    BeforeTreatKM = bt;
        //    Exist = true;
        //}
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
