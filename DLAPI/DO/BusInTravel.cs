using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusInTravel
    {
        public static int BUS_TRAVEL_KEY = 0;
        public string BusLicenseNumber { get; set; }
        public int BusLineNumber { get; set; }
        public String Driver { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
