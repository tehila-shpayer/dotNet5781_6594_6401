using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// BLמחלקה לייצוג קו אוטובוס בשכבת ה
    /// </summary>
    public class BusLine 
    {
        public int Key { get; set; }
        public int LineNumber { get; set; }
        public Areas Area { get; set; }
        public int FirstStation //הפנייה לתחנה ראשונה
        {
            get { if (BusLineStations == null) return -1;
                return BusLineStations.ElementAt(0).StationKey; }
        }
        public int LastStation //הפנייה לתחנה אחרונה
        {
            get {if (BusLineStations == null) return -1;
                return BusLineStations.ElementAt(BusLineStations.Count() - 1).StationKey; }
        }
        public IEnumerable<BusLineStation> BusLineStations { get; set; }

        // ToString דריסה של 
        public override String ToString()
        {
            return this.ToStringProperty();
        }
    }
}
