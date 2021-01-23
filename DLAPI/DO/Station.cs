using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace DO
{
    /// <summary>
    /// DLמחלקה לייצוג תחנה פיזית בשכבת ה
    /// </summary>
    public class Station
    {
        static public int STATION_KEY = 71000;
        public int Key { get;set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String Name { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
