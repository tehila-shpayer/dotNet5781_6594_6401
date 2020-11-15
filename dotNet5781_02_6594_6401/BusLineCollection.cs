using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6594_6401
{
    class BusLineCollection : IEnumerable //מחלקה המייצגת אוסף של קווי אוטובוס
    {
        public List<BusLine> BusLines { get; private set; }
        public BusLineCollection()
        {
            BusLines = new List<BusLine>();
        }
        public IEnumerator GetEnumerator()//מימוש איטרטור לאוסף
        {
            foreach (var item in BusLines)
            {
                yield return item;
            }
        }
        public void Add(BusLine b)//הוספת קו אוטובוס למערכת
        {
            try
            {
                if (BusLines.Find(busLine => busLine.LineNumber == b.LineNumber) != null)//אם האוטובוס כבר קיים
                    throw new ArgumentException("Sorry, this bus number already exists in the collection.");
                BusLines.Add(b);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Delete(BusLine b)//מחיקת קו אוטובוס מהמערכת
        {
            BusLines.Remove(b);
        }
        public List<BusLine> BusLineInStationList(int StationKey)//מחזיר רשימה של אוטובוסים העוברים בתחנה מסויימת
        {
            List<BusLine> busLinesList = new List<BusLine>();
            {
                foreach (BusLine Item in BusLines)
                {
                    if (Item.DidFindStation(Item.getStationFromKey(StationKey)))
                        busLinesList.Add(Item);
                }
                if (busLinesList.Count==0)//אם אין אוטובוסים שעוברים בתחנה הנתונה
                {
                    throw new BusException("No buses stop in this station!");
                }                
            }
            return busLinesList;
        }
        public List<BusLine> BusLinesSortedByGeneralTravelTime()//מיון קווי האוטובוס באוסף לפי זמן נסיעה
        {
           BusLines.Sort();
           return new List<BusLine>(BusLines);
        }
        public BusLine this[int index]//מחזיר קו אוטובוס בהינתן מספר הקו
        {
            get
            {
                BusLine busLine = BusLines.Find(x => x.LineNumber == index);
                if (busLine==null)
                   throw new BusException("There is no bus line " + index + " in the system!");
                return busLine;
            }
        }
        
        public override String ToString()//הדפסת הנתונים על כל קווי האוטובוס במערכת
        {
            string s = "";
            foreach (var line in BusLines)
            {
                s += line.ToString();
                s += "\n";
            }
            return s;
        }
    }
}
