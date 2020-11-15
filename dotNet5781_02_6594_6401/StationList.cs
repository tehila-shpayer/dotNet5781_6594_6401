using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6594_6401
{
    static class StationList //מחלקה של כול התחנות שיש במערכת
    {
        static public List<BusStation> Stations = new List<BusStation>();
        static StationList()
        {
            Stations = new List<BusStation>();
        }
        static public void Add(BusStation s)
        {
            Stations.Add(s);
        }
        static public void Remove(BusStation s)
        {
            Stations.Remove(s);
        }
        static public void Remove(int key)
        {
            Remove(FindStation(key));
        }

        static public BusStation FindStation(int key)
        {
            try
            {
                foreach (var station in Stations)
                {
                    if (station.BusStationKey == key)
                        return station;
                }
                throw new DllNotFoundException("Sorry, a station with this number doesn't exist");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        static public bool StationExists(int stationKey)
        {
            foreach (var station in Stations)
            {
                if (station.BusStationKey == stationKey)
                    return true;
            }
            return false;
        }
        static public new String ToString()
        {
            string s = "The Stations in the system:\n\n";
            foreach (var station in StationList.Stations)
            {
                s += station.ToString();
                s += "\n\n";
            }
            return s;
        }
    }
}
