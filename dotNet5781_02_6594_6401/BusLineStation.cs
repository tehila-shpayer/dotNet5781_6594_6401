using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6594_6401
{
    class BusLineStation
    {
        public BusStation Station { get; private set; }
        public float DistanceFromLastStationMeters { get; private set; }
        public int TravelTimeFromLastStationMinutes { get; private set; }
        public BusLineStation(int bsk, double la, double lo, float d, int t, string ad = "")
        {
            try
            {
                Station = new BusStation(bsk, la, lo, ad);
                if (d < 0)
                    throw new ArgumentOutOfRangeException("Distance from last station must be positive!");
                if (t < 0) 
                    throw new ArgumentOutOfRangeException("Travel time from last station must be positive!");
                DistanceFromLastStationMeters = d;
                TravelTimeFromLastStationMinutes = t;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
