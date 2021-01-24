using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// BLמחלקה לייצוג תחנת קו אוטובוס בשכבת ה
    /// המחלקה כוללת את פרטי המרחק והזמן בין התחנה הנוכחית לתחנה הקודמת
    /// </summary>
    public class BusLineStation
    {
        public int BusLineKey { get; set; }
        public int StationKey { get; set; }
        public int Position { get; set; }
        public int DistanceFromLastStationMeters { get; set; }
        public int TravelTimeFromLastStationMinutes { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
