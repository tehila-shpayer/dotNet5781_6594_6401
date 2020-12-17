using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Station
    {
        static public int STATION_KEY = 2100;
        public int Key { get;set; }
        public double Latitude { get;set; }
        public double Longitude { get; set; }
        public String Name { get; set; }

        public bool _isActive = true;
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
