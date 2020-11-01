using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6594_6401
{
    enum Areas { Jerusalem, Center, North, South, }
    class BusLine
    {
        public List<BusLineStation> BusLineStations { get; private set; }
        public int LineNumber { get; private set; }
        public BusStation FirstStation { get; private set; }
        public BusStation LastStation { get; private set; }

        public Areas area { get; private set; }

       public override String ToString()
        {
            String s = "Bus Line: "+LineNumber+"\nArea: "+area +""
            return s;
        }
    }
}
