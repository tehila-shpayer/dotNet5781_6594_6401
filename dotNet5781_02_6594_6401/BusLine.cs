using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6594_6401
{
    enum Areas { Jerusalem, Center, North, South, Hifa, TelAviv, YehudaAndShomron }
    class BusLine
    {
        public List<BusLineStation> BusLineStations { get; private set; }
        public int LineNumber { get; private set; }
        public BusStation FirstStation { get; private set; }
        public BusStation LastStation { get; private set; }

        public Areas area { get; private set; }

        public BusLine(List<BusLineStation> bls, int ln, BusStation first, BusStation last)
        {
            try
            {
                if (ln <=0)
                    throw new ArgumentOutOfRangeException("Line number must be positive!");
                bls = new List<BusLineStation>();
                LineNumber = ln;
                FirstStation = first;
                LastStation = last;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

       public override String ToString()
        {
            String s = "Bus Line: "+LineNumber+"\nArea: "+area +""
            return s;
        }
    }
}
