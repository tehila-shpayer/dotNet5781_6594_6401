using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineSchedule
    {
        public int BusLineNumer { get; set; }
        public int Frequency { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
