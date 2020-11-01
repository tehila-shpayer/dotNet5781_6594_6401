using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6594_6401
{
    public enum Areas { Jerusalem, Center, North, South, Hifa, TelAviv, YehudaAndShomron }
    class BusLine : IComparable
    {
        public List<BusLineStation> BusLineStations { get; private set; }
        public int LineNumber { get; private set; }
        public BusLineStation FirstStation { get; private set; }
        public BusLineStation LastStation { get; private set; }

        public Areas area { get; private set; }

        public BusLine(int  ln, Areas a, List<BusLineStation> bls = null)
        {
            try
            {
                if (ln <= 0)
                    throw new ArgumentOutOfRangeException("Line number must be positive!");
                if (bls != null)
                    BusLineStations = bls;
                else
                    BusLineStations = new List<BusLineStation>();
                LineNumber = ln;
                area = a;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public int CompareTo(object obj)
        {
            BusLine bs = (BusLine)obj;
            float otherTime = bs.FindTime(bs.FirstStation, bs.LastStation);
            float thisTime = FindTime(FirstStation, LastStation);
            if (otherTime > thisTime)
                return -1;
            if (thisTime > otherTime)
                return 1;
            return 0;
        }
        public void AddStation(int position, BusLineStation bls)
        {
            if (DidFindStation(bls)==false)
                BusLineStations.Insert(position, bls);
            if (position == 0)
                FirstStation = bls;
            if (position == BusLineStations.Count-1)
                LastStation = bls;
        }
        public float FindDistance(BusLineStation s1, BusLineStation s2)
        {
            float totalDistance = 0;
            BusLine bs = GetSubBusLine(s1, s2);
            foreach (BusLineStation station in bs.BusLineStations)
                totalDistance += station.DistanceFromLastStationMeters;
            return (totalDistance-s1.DistanceFromLastStationMeters);
        }
        public float FindTime(BusLineStation s1, BusLineStation s2)
        {
            float totalTime = 0;
            BusLine bs = GetSubBusLine(s1, s2);
            foreach (BusLineStation station in bs.BusLineStations)
                totalTime += station.TravelTimeFromLastStationMinutes;
            return (totalTime - s1.TravelTimeFromLastStationMinutes);
        }
        //public List<BusLineStation> GetSubBusLineStationsList(BusLineStation s1, BusLineStation s2)
        //{
        //    int s1Index = BusLineStations.IndexOf(s1);
        //    int s2Index = BusLineStations.IndexOf(s2);
        //    List<BusLineStation> newList = (BusLineStations.GetRange(s1Index, s2Index - s1Index+1));
        //    return newList;
        //}
        public BusLine GetSubBusLine(BusLineStation s1, BusLineStation s2)
        {
            int s1Index = BusLineStations.IndexOf(s1);
            int s2Index = BusLineStations.IndexOf(s2);
            List<BusLineStation> newList = (BusLineStations.GetRange(s1Index, s2Index - s1Index+1));
            return new BusLine(LineNumber, area, newList);
        }
        public void DeleteStation(BusLineStation bls)
        {
            BusLineStations.Remove(bls);
        }
        public bool DidFindStation(BusLineStation s)
        {
            if(FindStation(s)==null)
                    return false;
            return true;
        }
        public BusLineStation FindStation(BusLineStation s)
        {
            foreach (BusLineStation station in BusLineStations)
                if (station.GetBusStationKey() == s.GetBusStationKey())
                    return station;
            return null;
        }
        public override String ToString()
        {
            List<string> s2 = new List<string>();
            string s3 = "";
            string s4 = "";
            foreach (BusLineStation station in BusLineStations)
            {
                s2.Add(station.GetBusStationKey().ToString());
                s3 += station.GetBusStationKey().ToString() + " ";
            }
            s2.Reverse();
            foreach (string s5 in s2)
                s4 += s5 +" ";
            String s = "Bus Line: " + LineNumber + "\nArea: " + area + "\nBus stops line on the way forth: " + s3 + "\nBus stops line on the way back: " + s4;
            return s;
        }
    }
}
