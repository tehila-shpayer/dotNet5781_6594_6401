using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6594_6401
{
    class BusLineStation
    {
        public int StationKey { get; private set; }
        public float DistanceFromLastStationMeters { get; private set; }
        public int TravelTimeFromLastStationMinutes { get; private set; }
        public BusLineStation()
        {
            //default ctor
        }
        //public BusLineStation(BusLineStation s)
        //{
        //    StationKey = s.StationKey;
        //    DistanceFromLastStationMeters = s.DistanceFromLastStationMeters;
        //    TravelTimeFromLastStationMinutes = s.TravelTimeFromLastStationMinutes;
        //}
        public BusLineStation(int key, float d, int t)
        {
            try
            {
                StationKey = key;
                if(!StationList.StationExists(key))
                    throw new ArgumentOutOfRangeException("A station with this key number does not exist!");
                if (d <= 0)
                    throw new ArgumentOutOfRangeException("Distance from last station must be positive!");
                if (t <= 0)
                    throw new ArgumentOutOfRangeException("Travel time from last station must be positive!");
                DistanceFromLastStationMeters = d;
                TravelTimeFromLastStationMinutes = t;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public override String ToString()
        {
            return StationList.FindStation(StationKey).ToString();
        }
    }
}
/*
  מה שהיה קודם:
        public BusStation Station { get; private set; }
        public float DistanceFromLastStationMeters { get; private set; }
        public int TravelTimeFromLastStationMinutes { get; private set; }
        public BusLineStation(BusStation bs, float d, int t)
        {
            try
            {
                Station = new BusStation(bs.BusStationKey, bs.Latitude, bs.Longitude, bs.address);
                if (d <= 0)
                    throw new ArgumentOutOfRangeException("Distance from last station must be positive!");
                if (t <= 0)
                    throw new ArgumentOutOfRangeException("Travel time from last station must be positive!");
                DistanceFromLastStationMeters = d;
                TravelTimeFromLastStationMinutes = t;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public int GetBusStationKey() { return Station.BusStationKey; }
    }
 */
