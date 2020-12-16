using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusLine
    {
        public static int BUS_LINE_KEY = 0;
        public int LineNumber { get; private set; }
        public Areas Area { get; private set; }
        public int FirstStationKey { get; private set; }
        public int LastStationKey { get; private set; }
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
