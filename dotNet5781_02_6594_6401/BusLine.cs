using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics.Eventing.Reader;
using System.Device.Location;
using System.Linq.Expressions;

namespace dotNet5781_02_6594_6401
{
    public enum Areas { General, Jerusalem, Center, North, South, Hifa, TelAviv, YehudaAndShomron }
    public class BusLine : IComparable //מחלקה ברת השוואה של קו אוטובוס
    {
        public static int BUS_LINE_NUMBER = 0; //משתה רץ של מספרים של האוטובוסים
        //משתנה שמציין האם הקו אוטובוס הוא תת קו של קו אחר
        public bool SubLineOf { get; private set; } 
        public List<BusLineStation> BusLineStations { get; private set; }

        public int LineNumber { get; private set; }
        public int FirstStation //הפנייה ךתחנה ראשונה
        { 
            get { return BusLineStations.ElementAt(0).StationKey; }
        }
        public int LastStation //הפנייה לתחנה אחרונה
        {
            get { return BusLineStations.ElementAt(BusLineStations.Count-1).StationKey; }
        }

        public Areas area { get; private set; }
        /// <summary>
        /// בנאי עם פרמטרים
        /// </summary>
        /// <param name="a">איזור הקו</param>
        /// <param name="bls">רשימת תחנות</param>
        /// <param name="subBusOf">משתנה בוליאני שבודק האם הקו הוא תת קו</param>
        public BusLine(Areas a, List<BusLineStation> bls = null, int subBusOf=0)
        {
            if (((int)a > 7) || ((int)a < 0))//אימות שהאיזור בטווח הנכון
                throw new BusException("Area number must be between 0 and 7");
            BUS_LINE_NUMBER++;
            if (bls != null) //אם יש תחנות כלשהם
                    BusLineStations = bls;
                else
                    BusLineStations = new List<BusLineStation>();
                if (subBusOf == 0) //אם הקו לא תת קו
                    LineNumber = BUS_LINE_NUMBER;
                else //אם הקו הוא תת קו
                {
                    LineNumber = subBusOf; //הקו מקבל את אותו מספר  
                    SubLineOf = true;      //מהקו ממנו הוא לקוח
            }
                area = a;
        }
        /// <summary>
        /// פונקציית השוואה בין שני קווים 
        /// על פי הזמן הכולל שלוקח ליסוע בין התחנה הראשונה לאחרונה
        /// </summary>
        /// <param name="obj">הקו האחר</param>
        /// <returns>מספר הקו</returns>
        public int CompareTo(object obj)
        {
            BusLine otherBS = (BusLine)obj;//המרה לסוג קו אוטובוס
            //זמן כולל של הקו האחר
            float otherTime = otherBS.FindTime(otherBS.getStationFromKey(otherBS.FirstStation), otherBS.getStationFromKey(otherBS.LastStation));
            //זמן כללי של הקו הנוכחי
            float thisTime = FindTime(getStationFromKey(FirstStation),getStationFromKey(LastStation));
            //השוואה
            if (otherTime > thisTime)
                return -1;
            if (thisTime > otherTime)
                return 1;
            return 0;
        }
        /// <summary>
        /// הוספת תחנה לקו על פי קוד תחנה
        /// הפונקצייה מקבלת התחנה שיש להוסיף 
        /// ומוסיפה אותה לרשימת התחנות בקו
        /// </summary>
        /// <param name="sKey">קוד התחנה להוספה</param>
        public void AddStation(int sKey)
        {
            BusLineStation busLineStation = GetBusLineStationToAdd(sKey, BusLineStations.Count);
            BusLineStations.Add(busLineStation);
        }
        /// <summary>
        /// הוספת תחנה לקו על פי קוד תחנה
        ///הפונקצייה מאפשרת בחירת המיקום ברשימת התחנות
        /// </summary>
        /// <param name="sKey">קוד התחנה</param>
        /// <param name="position">מיקום ברשימה</param>
        public void AddStation(int sKey,int position)
        {
            if (position > BusLineStations.Count)//מיקום רלוונטי - כגודל רשימת התחנות
                throw new BusException($"The Bus line has only {BusLineStations.Count} stations");
                if (!StationList.StationExists(sKey))//אימות שהתחנה קיימת במערכת
                    throw new KeyNotFoundException("A station with this number does not exist!");
                //קבלת תחנה מסוג תחנת קו אוטובוס על פי המיקום ברשימה
                BusLineStation busLineStation = GetBusLineStationToAdd(sKey, position);
                //הוספת התחנה לקו
                BusLineStations.Insert(position,busLineStation);
                if (position != BusLineStations.Count-1)
                {//אם זו לא התחנה האחרונה יש לעדכן את פרטי הזמן והמרחק של התחנה הבאה 
                    this[position+1] = GetBusLineStationToAdd(this[position + 1].StationKey, position + 1);
                }
           
        }
        /// <summary>
        /// מחיקת תחנה על פי התחנה עצמה
        /// </summary>
        /// <param name="bls">התחנה למחיקה</param>
        public void DeleteStation(BusLineStation bls)
        {
            int index = BusLineStations.IndexOf(bls);
            BusLineStations.Remove(bls);
            //אם זו לא התחנה האחרונה יש לעדכן את פרטי הזמן והמרחק של התחנה הבאה
            //(שמיקומה הוא של התחנה שנמחקה עכשיו) 
                if (index < BusLineStations.Count)
                this[index] = GetBusLineStationToAdd(this[index].StationKey, index);
        }
        /// <summary>
        /// מחיקת תחנה על קוד תחנה
        /// שימוש בפונקציית מחיקה על פי התחנה עצמה
        /// </summary>
        /// <param name="key">קוד התחנה</param>
        public void DeleteStation(int key)
        {
            DeleteStation(getStationFromKey(key));

        }
        /// <summary>
        /// חישוב הזמן והמרחק מהתחנה הקודמת של תחנה כלשהי 
        /// על פי מיקומה בקו האוטובוס
        /// </summary>
        /// <param name="sKey">קוד התחנה</param>
        /// <param name="position">מיקומה בקו</param>
        /// <returns>משתנה מסוג תחנת קו אוטובוס שמכיל את הזמן והמרחק המחושבים</returns>
        public BusLineStation GetBusLineStationToAdd(int sKey, int position)
        {
            if (position == 0)//לתחנה ראשונה אין מרחק או זמן מתחנה קודמת
               return new BusLineStation(sKey, 0, 0);
            if (position - 1 >= 0)
            {
                BusStation NewStation = StationList.FindStation(sKey);//התחנה המחושבת
                BusStation PreviousStation = StationList.FindStation(this[position - 1].StationKey);//התחנה הקודמת
                GeoCoordinate locationOfNew = new GeoCoordinate(NewStation.Latitude, NewStation.Longitude);//מיקום התחנה המחושבת
                GeoCoordinate locationOfPre = new GeoCoordinate(PreviousStation.Latitude, PreviousStation.Longitude);//מיקום התחנה הקודמת
                double distance = locationOfNew.GetDistanceTo(locationOfPre);//חישוב מרחק
                int time = Convert.ToInt32(distance / (80000 / 60));//חישוב זמן בהנחה שמהירות ממוצעת של אוטובוס היא 80 קמש
                return new BusLineStation(sKey, distance, time);
            }
            return null;
        }
        /// <summary>
        /// קבלת התחנה עצמה מקוד תחנה
        /// </summary>
        /// <param name="sKey">קוד התחנה</param>
        /// <returns>פרטי התחנה</returns>
        public BusLineStation getStationFromKey(int sKey)
        {
            foreach (BusLineStation station in BusLineStations)
                if (station.StationKey == sKey)
                    return station;
            
            return null;
        }
        /// <summary>
        /// אינדקסר 
        /// </summary>
        /// <param name="index">האינדקס שמוכנס</param>
        /// <returns></returns>
        public BusLineStation this[int index]
        {
            get //מחזיר את התחנה במקום האינדקס ברשימת התחנות
            {                
                BusLineStation station = BusLineStations.ElementAt(index);
                if (station == null)
                    Console.WriteLine("There is no station " + index + " in the list of stations");
                return station;
            }
            set//מציב תחנה שמתקבלת בווליו במקום האינדקס ברשימה
            {
                BusLineStations[index] = value;
            }
        }
        /// <summary>
        /// מציאת מרחק בין שתי תחנות
        /// </summary>
        /// <param name="s1">תחנת מוצא</param>
        /// <param name="s2">תחנת יעד</param>
        /// <returns>מרחק</returns>
        public double FindDistance(BusLineStation s1, BusLineStation s2)
        {
            double totalDistance = 0;
            int s1Index = BusLineStations.IndexOf(s1);
            int s2Index = BusLineStations.IndexOf(s2);
            //סכימת המרחק מבין תחנה לתחנה בין תחנת המוצא לתחנת היעד
            for (int i = s1Index + 1; i <= s2Index; i++)
                totalDistance += BusLineStations[i].DistanceFromLastStationMeters;
            return totalDistance;
        }
        /// <summary>
        /// מציאת זמן בין שתי תחנות
        /// </summary>
        /// <param name="s1">תחנת מוצא</param>
        /// <param name="s2">תחנת יעד</param>
        /// <returns>זמן</returns>
        public int FindTime(BusLineStation s1, BusLineStation s2)
        {
            int totalTime = 0;
            int s1Index = BusLineStations.IndexOf(s1);
            int s2Index = BusLineStations.IndexOf(s2);
            //סכימת הזמן מבין תחנה לתחנה בין תחנת המוצא לתחנת היעד
            for (int i=s1Index+1;i<=s2Index;i++)
                totalTime += BusLineStations[i].TravelTimeFromLastStationMinutes;
            return totalTime;
        }
        /// <summary>
        /// הפונקצייה מחזירה תת קו אוטובוס מתחנה כלשהי ותחנה אחרת בקו אוטובוס מסויים
        /// </summary>
        /// <param name="s1">תחנת התחלה</param>
        /// <param name="s2">תחנה סופית</param>
        /// <returns>תת קו אוטובוס</returns>
        public BusLine GetSubBusLine(BusLineStation s1, BusLineStation s2)
        {
            try
            {//מציאת מיקום 2 התחנות ברשימה
                int s1Index = BusLineStations.IndexOf(s1);
                int s2Index = BusLineStations.IndexOf(s2);
                // אם התחנות קיימות בקו
                if (s1Index == -1 || s2Index == -1)
                    throw new NullReferenceException();
                //בניית רשימת קווי אוטובוס מתחנה ראשונה לאחרונה
                List<BusLineStation> newList = (BusLineStations.GetRange(s1Index, s2Index - s1Index + 1));
                //לתחנה ראשונה אין תחנה קודמת - אין מרחק וזמן ממנה
                newList[0].DistanceFromLastStationMeters = 0;
                newList[0].TravelTimeFromLastStationMinutes = 0;
                //החזרת תת הקו
                return new BusLine(area, newList,LineNumber);
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
        /// <summary>
        /// בדיקה האם תחנה א קודמת לתחנה ב בקו האוטובוס
        /// </summary>
        /// <param name="s1">תחנה א</param>
        /// <param name="s2">תחנה ב</param>
        /// <returns>משתנה בוליאני - מחזיר נכון אם תחנה א קודמת לתחנה ב</returns>
        public bool IstationPrior(BusLineStation s1, BusLineStation s2)
        {
            //השוואת האינדקסים
            return (BusLineStations.IndexOf(s1) < BusLineStations.IndexOf(s2));
        }
        /// <summary>
        /// בדיקה האם תחנה קיימת בקו
        /// על פי התחנה עצמה
        /// </summary>
        /// <param name="s">התחנה</param>
        /// <returns>האם התחנה בקו</returns>
        public bool DidFindStation(BusLineStation s)
        {
           return BusLineStations.Contains(s);
        }
        /// <summary>
        /// בדיקה האם תחנה קיימת בקו
        /// על פי קוד תחנה
        /// </summary>
        /// <param name="s">קוד תחנה</param>
        /// <returns>האם התחנה בקו</returns>

        public bool DidFindStation(int key)
        {
            foreach (var bls in BusLineStations)
            {
                if (bls.StationKey == key)
                    return true;
            }
            return false;
        }
        // ToString דריסה של 
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
