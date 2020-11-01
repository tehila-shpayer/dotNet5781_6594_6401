using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6594_6401
{
    class Program
    {
        static void Main(string[] args)
        {
            BusStation bs = new BusStation(158745, 31.234567, 34.56789, "רח' פלוני אלמוני 12, תל חורף");
            Console.WriteLine(bs);
            BusLineStation bss = new BusLineStation(158745, 31.234567, 34.56789,-18,54, "רח' פלוני אלמוני 12, תל חורף");
        }
    }
}
