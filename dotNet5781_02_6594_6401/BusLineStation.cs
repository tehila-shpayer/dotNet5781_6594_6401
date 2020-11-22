using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace dotNet5781_02_6594_6401
{
    public class BusLineStation
    {
        public int StationKey { get; private set; }
        public double DistanceFromLastStationMeters { get; set; }
        public int TravelTimeFromLastStationMinutes { get; set; }
        public BusLineStation()
        {
            //default ctor
        }
        /// <summary>
        /// בנאי עם פרמטרים
        /// </summary>
        /// <param name="key">קוד התחנה</param>
        /// <param name="d">מרחק מתחנה קודמת</param>
        /// <param name="t">זמן נסיעה מתחנה קודמת</param>
        public BusLineStation(int key, double d=0, int t=0)
        {
            try
            {
                if(!StationList.StationExists(key))
                    throw new ArgumentOutOfRangeException("A station with this key number does not exist!");
                if (d < 0)
                    throw new ArgumentOutOfRangeException("Distance from last station must be positive!");
                if (t < 0)
                    throw new ArgumentOutOfRangeException("Travel time from last station must be positive!");
                DistanceFromLastStationMeters = d;
                TravelTimeFromLastStationMinutes = t;
                StationKey = key;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

         }
        //ToString דריסה של 
        public override String ToString()
        {
            return StationList.FindStation(StationKey).ToString();
        }
    }
}

