using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusLineStation
    {
        public int BusLineKey { get; set; }
        public int StationKey { get; set; }
        public int Position { get; set; }
        public double DistanceFromLastStationMeters { get; set; }
        public int TravelTimeFromLastStationMinutes { get; set; }

        public bool _isActive = true;
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
