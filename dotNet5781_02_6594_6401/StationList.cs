using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6594_6401
{
    public static class StationList //מחלקה של כול התחנות שיש במערכת
    {
        static public List<BusStation> Stations = new List<BusStation>();
        //בנאי
        static StationList()
        {
            Stations = new List<BusStation>();
        }
        //הוספת תחנה למערכת
        static public void Add(BusStation s)
        {
            Stations.Add(s);
        }
        
        /// <summary>
        /// הסרת תחנה מהמערכת
        /// </summary>
        /// <param name="s">הפונקציה מקבלת כפרמטר תחנה</param>
        static public void Remove(BusStation s)
        {
            Stations.Remove(s);
        }
        /// <summary>
        /// הסרת תחנה מהמערכת
        /// </summary>
        /// <param name="key">פונקציה מקבלת כפרמטר מספר תחנה</param>
        static public void Remove(int key)
        {
            Remove(FindStation(key));
        }
        /// <summary>
        /// חיפוש תחנה במערכת
        /// </summary>
        /// <param name="key">מקבלת כפרמטר מספר תחנה</param>
        /// <returns>BusStation מחזירה אובייקט מסוג </returns>
        static public BusStation FindStation(int key)
        {
            try
            {
                foreach (var station in Stations) //מציאת התחנה ברשימת התחנות
                {
                    if (station.BusStationKey == key)
                        return station;
                }
                throw new DllNotFoundException("Sorry, a station with this number doesn't exist");
            }
            catch (Exception ex) //הדפסת חריגה במקרה ואין תחנה עם מפתח כזה
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        /// <summary>
        /// בדיקה האם קיימת תחנה עם קוד מסויים במערכת
        /// </summary>
        /// <param name="stationKey">מקבלת קוד תחנה</param>
        /// <returns>מחזירה משתנה בוליאני שמציין האם קיימת תחנה עם הקוד המסויים</returns>
        static public bool StationExists(int stationKey)
        {
            foreach (var station in Stations) //חיפוש ברשימת תחנות
            {
                if (station.BusStationKey == stationKey)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// ToString דריסה של פונקציית
        /// הפונקצייה מדפיסה את פרטי כול התחנות הקיימות במערכת
        /// </summary>
        /// <returns></returns>
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
