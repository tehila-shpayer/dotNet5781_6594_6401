﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6594_6401
{
    public class BusStation//מחלקה המייצגת תחנת אוטובוס
    {
        public static int BUS_STATION_NUMBER=0; //משתנה רץ למספרי רישוי של אוטובוסים
        public int BusStationKey  { get; private set;}
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string address { get; private set; }
        public BusStation(double la, double lo, string ad="")//יצירת תחנה חדשה
        {
            //אם מיקום התחנה לא תקין - נזרקת חריגה
           if((la > 90) || (la<-90))
              throw new BusException("Bus station Latitude number must be in the range [-90,90]!");
           if ((lo > 180) || (lo < -180))
              throw new BusException("Bus station Longitude number must be in the range [-180,180]!");

            BUS_STATION_NUMBER++;
            string key = BUS_STATION_NUMBER.ToString();
            if (key.Length > 6)
                throw new BusException("Bus station key number must be of maximum 6 digit!");
            BusStationKey = BUS_STATION_NUMBER;
            Latitude = la;
            Longitude = lo;
            address = ad;
        }
        public override string ToString()//הדפסת נתוני התחנה
        {
            string num = BusStationKey.ToString();
            num += ".";
            if (num.Length < 3)
            {
                num = num.Insert(0, " ");
                num += " ";
            }

            //while (num.Length < 3) { num += " "; }
            string s = $"Station Code: {num} Location: {XDigitsAfterPoint(Latitude, 5)}°N {XDigitsAfterPoint(Longitude, 5)}°E";
            if (address != "")
                s+= " Address: " +address;
            return s;
        }
        public string XDigitsAfterPoint(double d, int digitsAfterPoint)
        //מקבל מספר ממשי ומחזיר מחרוזת המייצגת אותו עד ספרה מסויימת אחרי הנקודה
        {
            string s = d.ConvertToString();
            for (int i = 0; i < 3 + digitsAfterPoint; i++)
                if (s.Length < 3 + digitsAfterPoint)
                    s += "0";
            s = s.Substring(0, 3+digitsAfterPoint);
            return s;
        }
       

    }

    static class ExtensionDouble
    {
        //פונקציית הרחבה: המרה ממספר ממשי למחרוזת
        public static string ConvertToString(this double thisDouble)
        {
            double d = thisDouble;
            string s = "";

            s += (int)d;
            s += ".";
            d = d - (int)d;
            int i = 0;
            while ((d != 0) && (i < 15))
            {
                d *= 10;
                s += (int)d;
                d = d - (int)d;
                ++i;
            }
            return s;
        }
    }
}
