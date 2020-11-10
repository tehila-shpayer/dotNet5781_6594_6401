using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics.Eventing.Reader;
using System.Device.Location;

namespace dotNet5781_02_6594_6401
{
    public enum Areas { General, Jerusalem, Center, North, South, Hifa, TelAviv, YehudaAndShomron }
    class BusLine : IComparable
    {
        public static int BUS_LINE_NUMBER = 0;
        public List<BusLineStation> BusLineStations { get; private set; }

        public int LineNumber { get; private set; }
        public int FirstStation
        { 
            get { return BusLineStations.ElementAt(0).StationKey; }
        }
        public int LastStation 
        {
            get { return BusLineStations.ElementAt(BusLineStations.Count-1).StationKey; }
        }

        public Areas area { get; private set; }

        public BusLine(Areas a, List<BusLineStation> bls = null)
        { 
            try
            {
                BUS_LINE_NUMBER++;
                //if (line <= 0)
                //    throw new ArgumentOutOfRangeException("Line number must be positive!");
                if (bls != null)
                    BusLineStations = bls;
                else
                    BusLineStations = new List<BusLineStation>();
                LineNumber = BUS_LINE_NUMBER;
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
            float otherTime = otherBS.FindTime(new BusLineStation(otherBS.FirstStation, 1, 1), new BusLineStation(otherBS.LastStation, 1, 1));
            float thisTime = FindTime(new BusLineStation (FirstStation,1,1),new BusLineStation (LastStation,1,1));
            if (otherTime > thisTime)
                return -1;
            if (thisTime > otherTime)
                return 1;
            return 0;
        }
        //public void AddStation(int sKey)
        //{
        //    try
        //    {
        //        if (!StationList.StationExists(sKey))
        //            throw new KeyNotFoundException("A station with this number does not exist!");
        //        BusStation NewStation = StationList.FindStation(sKey);
        //        BusStation PreviousStation = StationList.FindStation(this[BusLineStations.Count - 1].StationKey);
        //        GeoCoordinate locationOfNew = new GeoCoordinate(NewStation.Latitude, NewStation.Longitude);
        //        GeoCoordinate locationOfPre = new GeoCoordinate(PreviousStation.Latitude, PreviousStation.Longitude);
        //        double distance = locationOfNew.GetDistanceTo(locationOfPre);
        //        int time = Convert.ToInt32(distance / (80000 / 60));
        //        BusLineStation busLineStation = new BusLineStation(sKey, distance, time);
        //        BusLineStations.Add(busLineStation);
        //    }
        //    catch (Exception ex) { Console.WriteLine(ex.Message); }
        //}
        public void AddStation(int sKey,int position=0)
        {
            try
            {
                BusLineStation busLineStation = GetBusLineStation(sKey, position);
                BusLineStations.Insert(position,busLineStation);
                if ((position+1) != BusLineStations.Count)
                {
                    this[position] = GetBusLineStation(this[position].StationKey, position);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        public BusLineStation GetBusLineStation(int sKey, int position = 0)
        {
            if (!StationList.StationExists(sKey))
                throw new KeyNotFoundException("A station with this number does not exist!");

            if (position == 0)
                return new BusLineStation(sKey, 0, 0);

            BusStation NewStation = StationList.FindStation(sKey);
            BusStation PreviousStation = StationList.FindStation(this[position - 1].StationKey);
            GeoCoordinate locationOfNew = new GeoCoordinate(NewStation.Latitude, NewStation.Longitude);
            GeoCoordinate locationOfPre = new GeoCoordinate(PreviousStation.Latitude, PreviousStation.Longitude);
            double distance = locationOfNew.GetDistanceTo(locationOfPre);
            int time = Convert.ToInt32(distance / (80000 / 60));
            return new BusLineStation(sKey, distance, time);
        }
        public BusLineStation this[int index]
        {
            get
            {
                BusLineStation station = BusLineStations.ElementAt(index);

                //    int i = 0;
                //    foreach (BusLineStation s in BusLineStations)//לפי מספר ברשימה
                //    {
                //        if (i == index)
                //        {
                //            return s;
                //        }
                //        i++;
                //    }
                if (station == null)
                    Console.WriteLine("There is no station " + index + " in the list of stations");
                return station;
            }
            set
            {
                BusLineStations[index] = value;
                //if (index == 0)
                //{
                //    BusLineStations.Insert(0,value);
                //    return;
                //}

                //BusLineStation beforeNew = new BusLineStation();
                
                //foreach (BusLineStation s in BusLineStations)//לפי קוד תחנה
                //{
                //    if (s.StationKey == index)
                //    {
                //        BusLineStations.Insert(;
                //        return;
                //    }                 
                //}

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

               // Console.WriteLine("There is no index " + index + " in the list of stations"); 
            }
        }

        public double FindDistance(BusLineStation s1, BusLineStation s2)
        {
            try
            {
                double totalDistance = 0;
                BusLine bs = GetSubBusLine(s1, s2);
                foreach (BusLineStation station in bs.BusLineStations)
                    totalDistance += station.DistanceFromLastStationMeters;
                return (totalDistance - s1.DistanceFromLastStationMeters);
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }
        public int FindTime(BusLineStation s1, BusLineStation s2)
        {
            int totalTime = 0;
            BusLine bs = GetSubBusLine(s1, s2);
            foreach (BusLineStation station in bs.BusLineStations)
                totalTime += station.TravelTimeFromLastStationMinutes;
            return (totalTime - s1.TravelTimeFromLastStationMinutes);
        }
        public BusLine GetSubBusLine(BusLineStation s1, BusLineStation s2)
        {
            try
            {
                int s1Index = BusLineStations.IndexOf(s1);
                int s2Index = BusLineStations.IndexOf(s2);
                if (s1Index == -1 || s2Index == -1)
                    throw new NullReferenceException("Invalid input. One of the stations does not exist in the bus line!");
                List<BusLineStation> newList = (BusLineStations.GetRange(s1Index, s2Index - s1Index + 1));
                return new BusLine(area, newList);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Invalid input. First sation must be prior to second station!");
                return null;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Invalid input. One of the stations does not exist in the bus line!");
                return null;
            }
        }
        public void DeleteStation(BusLineStation bls)
        {
            BusLineStations.Remove(bls);
        }
        public bool DidFindStation(BusLineStation s)
        {
           return BusLineStations.Contains(s);
        }
        public bool DidFindStation(int key)
        {
            foreach (var bls in BusLineStations)
            {
                if (bls.StationKey == key)
                    return true;
            }
            return false;
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
