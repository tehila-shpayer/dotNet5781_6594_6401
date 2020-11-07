using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics.Eventing.Reader;

namespace dotNet5781_02_6594_6401
{
    public enum Areas { General, Jerusalem, Center, North, South, Hifa, TelAviv, YehudaAndShomron }
    class BusLine : IComparable
    {

        private LinkedList<BusLineStation> _busLineStation;
        public LinkedList<BusLineStation> BusLineStations { get; private set; }

        public int LineNumber { get; private set; }
        public BusLineStation FirstStation
        { 
            get { return BusLineStations.First.Value; }
        }
        public BusLineStation LastStation 
        {
            get { return BusLineStations.Last.Value; }
        }

        public Areas area { get; private set; }

        public BusLine(int  line, Areas a, LinkedList<BusLineStation> bls = null)
        {
            try
            {
                if (line <= 0)
                    throw new ArgumentOutOfRangeException("Line number must be positive!");
                if (bls != null)
                    BusLineStations = bls;
                else
                    BusLineStations = new LinkedList<BusLineStation>();
                LineNumber = line;
                area = a;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public int CompareTo(object obj)
        {
            BusLine otherBS = (BusLine)obj;
            float otherTime = otherBS.FindTime(otherBS.FirstStation, otherBS.LastStation);
            float thisTime = FindTime(FirstStation, LastStation);
            if (otherTime > thisTime)
                return -1;
            if (thisTime > otherTime)
                return 1;
            return 0;
        }
        public void AddStation(int position, BusLineStation bls)
        {
            this[position] = bls;
        }
        public BusLineStation this[int index]
        {
            get
            {  
                foreach (BusLineStation s in BusLineStations)//לפי קוד תחנה
                {
                    if (s.StationKey == index)
                    {
                        return s;
                    }
                }

                //    int i = 0;
                //    foreach (BusLineStation s in BusLineStations)//לפי מספר ברשימה
                //    {
                //        if (i == index)
                //        {
                //            return s;
                //        }
                //        i++;
                //    }

                Console.WriteLine("There is no station " +index+ " in the list of stations");
                return null;
            }
            set
            {
                if (index == 0)
                {
                    BusLineStations.AddFirst(value);
                    return;
                }

                BusLineStation beforeNew = new BusLineStation();
                
                foreach (BusLineStation s in BusLineStations)//לפי קוד תחנה
                {
                    if (s.StationKey == index)
                    {
                        BusLineStations.AddAfter(BusLineStations.Find(s), value);
                        return;
                    }                 
                }

                //int i = 0;
                //foreach (BusLineStation s in BusLineStations) // לפי מספר ברשימה
                //{
                //    if (i == index)
                //    {
                //        BusLineStations.AddAfter(BusLineStations.Find(s), value);
                //        return;
                //    }
                //    i++;
                //}

                Console.WriteLine("There is no index " + index + " in the list of stations"); 
            }
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
            LinkedList<BusLineStation> newList = new LinkedList<BusLineStation>(BusLineStations);
            this[s1.StationKey]
            //int s1Index = BusLineStations.IndexOf(s1);
            //int s2Index = BusLineStations.IndexOf(s2);
            //List<BusLineStation> newList = (BusLineStations.GetRange(s1Index, s2Index - s1Index+1));
            return new BusLine(LineNumber, area, newList);
        }
        public void DeleteStation(BusLineStation bls)
        {
            BusLineStations.Remove(bls);
        }
        public bool DidFindStation(BusLineStation s)
        {
           return BusLineStations.Contains(s);
        }
        //public BusLineStation FindStation(BusLineStation s)
        //{
        //    BusLineStations.Find(s);
        //    //foreach (BusLineStation station in BusLineStations)
        //    //    if (station.GetBusStationKey() == s.GetBusStationKey())
        //    //        return station;
        //    //return null;
        //}
        public override String ToString()
        {
            String s = "Bus Line: " + LineNumber + "\nArea: " + area + "\nBus stations:\n";
            foreach (var station in BusLineStations)
            {
                s += ("   " + station.ToString() + "\n");
            }
            return s;
        }
    }
}
