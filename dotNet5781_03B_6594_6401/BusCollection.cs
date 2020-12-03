using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace dotNet5781_03B_6594_6401
{
    static public class BusCollection
    {
        static public int BUS_COUNTER = 0;
        static public List<Bus> buses = new List<Bus>();
        static BusCollection() { buses = new List<Bus>(); }
        static public void Add(Bus bus) { buses.Add(bus); BUS_COUNTER++; }
    }
}
