using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// BLמחלקה לייצוג תחנה פיזית בשכבת ה
    /// </summary>
    public class Station
    {
        public int Key { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String Name { get; set; }
        public IEnumerable<int> BusLines { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
