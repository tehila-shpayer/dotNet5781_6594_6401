using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// מחלקה לייצוג שעות הגעה של קו אוטובוס
    /// המחלקה היא לייצוג מסלול מסויים
    /// שעת יציאה, שעת הגעה לתחנת מוצא ולתחנת יעד
    /// </summary>
    public class ArrivalTimes
    {
        public TimeSpan Start { get; set; }
        public TimeSpan SourceArrive { get; set; }
        public TimeSpan DestinationArrive { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
