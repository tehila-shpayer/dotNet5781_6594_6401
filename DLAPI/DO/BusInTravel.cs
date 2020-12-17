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
        public int BusLicenseNumber { get; set; }
        public int BusLineNumber { get; set; }
        public String LineStartingTime { get; set; }
        public String BusStartingTime { get; set; }
        public int LastStationKey { get; set; }
        public String LastStationTime { get; set; }
        public String NextStationTime { get; set; }
        public String Driver { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
