using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6594_6401
{
    class BusStation
    {
        public int BusStationKey  { get; private set;}
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string address { get; private set; }
        public BusStation(int bsk, double la, double lo, string ad="")
        {
            try
            {            
                string key = bsk.ToString();
                if (key.Length != 6)
                    throw new ArgumentException("Bus station key number must be of 6 digit!");
                if((la > 90) || (la<-90))
                    throw new ArgumentOutOfRangeException("Bus station Latitude number must be in the range [-90,90]!");
                if ((lo > 180) || (lo < -180))
                    throw new ArgumentOutOfRangeException("Bus station Longitude number must be in the range [-180,180]!");
                BusStationKey = bsk;
                Latitude = la;
                Longitude = lo;
                address = ad;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public override string ToString()
        {
            return $"Bus Station Code: {BusStationKey}, {Latitude}°N {Longitude}°E";    
        }

    }
}
