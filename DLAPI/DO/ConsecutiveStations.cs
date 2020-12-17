using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class ConsecutiveStations
    {
        public int StationKey1 { get; private set; }
        public int StationKey2 { get; private set; }
        public double Distance { get; private set; }
        public double AverageTime { get; private set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
