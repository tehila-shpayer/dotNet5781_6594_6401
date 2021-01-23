using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// DLמחלקה לייצוג זמני יציאה של קו אוטובוס בשכבת ה
    /// </summary>
    public class LineSchedule
    {
        public int LineKey { get; set; }
        public int Frequency { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
