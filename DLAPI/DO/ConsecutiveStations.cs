using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// DLמחלקה לייצוג תחנות עוקבות בקו אוטובוס בשכבת ה
    /// המחלקה כוללת את פרטי המרחק והזמן בין התחנות
    /// </summary>
    public class ConsecutiveStations
    {
        public int StationKey1 { get; set; }
        public int StationKey2 { get; set; }
        public int Distance { get; set; }
        public int AverageTime { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
