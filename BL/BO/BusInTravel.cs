using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusInTravel
    {
        public static int BUS_TRAVEL_KEY = 0;
        public int Key { get; set; }
        public string BusLicenseNumber { get; set; }
        public int LineKey { get; set; }
        public int StationKey { get; set; }
        public TimeSpan StartTime { get; set; }     
        public TimeSpan TimeLeft { get; set; }
        public String LastStationName { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
