using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class ConsecutiveStations
    {
        public int StationKey1 { get; set; }
        public int StationKey2 { get; set; }
        public double Distance { get; set; }
        public double AverageTime { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
