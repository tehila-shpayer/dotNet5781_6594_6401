using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusLine //: IComparable //מחלקה ברת השוואה של קו אוטובוס
    {
        public static int BUS_LINE_NUMBER = 1; //משתה רץ של מספרים של האוטובוסים
        //משתנה שמציין האם הקו אוטובוס הוא תת קו של קו אחר
        public int Key { get; set; }
        public int LineNumber { get; set; }
        public Areas Area { get; set; }
        public int FirstStation //הפנייה ךתחנה ראשונה
        {
            get { return BusLineStations.ElementAt(0).StationKey; }
        }
        public int LastStation //הפנייה לתחנה אחרונה
        {
            get { return BusLineStations.ElementAt(BusLineStations.Count() - 1).StationKey; }
        }
        public IEnumerable<BusLineStation> BusLineStations { get; set; }

        // ToString דריסה של 
        public override String ToString()
        {
            return this.ToStringProperty();
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
                //if (station == null)
                    //Console.WriteLine("There is no station " + index + " in the list of stations");
                return station;
            }
        }
    }
}
