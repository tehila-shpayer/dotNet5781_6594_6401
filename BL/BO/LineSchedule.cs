using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
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
