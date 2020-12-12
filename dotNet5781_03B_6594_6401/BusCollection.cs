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
        static public ObservableCollection<Bus> windowBuses = new ObservableCollection<Bus>();
        static BusCollection() { windowBuses = new ObservableCollection<Bus>(); }
        static public void Add(Bus bus) { bus.ApdateStatus(); windowBuses.Add(bus); BUS_COUNTER++; }
    }
}
