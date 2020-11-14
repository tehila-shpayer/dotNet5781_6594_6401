using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6594_6401
{
    class BusLineCollection : IEnumerable
    {
        public List<BusLine> BusLines { get; private set; }
        public BusLineCollection()
        {
            BusLines = new List<BusLine>();
        }
        public IEnumerator GetEnumerator()
        {
            foreach (var item in BusLines)
            {
                yield return item;
            }
        }
        public void Add(BusLine b)
        {
            try
            {
                if (BusLines.Find(busLine => busLine.LineNumber == b.LineNumber) != null)
                    throw new ArgumentException("Sorry, this bus number already exists in the collection.");
                BusLines.Add(b);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Delete(BusLine b)
        {
            BusLines.Remove(b);
        }
        public List<BusLine> BusLineInStationList(int StationKey)
        {
            List<BusLine> busLinesList = new List<BusLine>();
            try
            {
                foreach (BusLine Item in BusLines)
                {
                    if (Item.DidFindStation(Item.getStationFromKey(StationKey)))
                        busLinesList.Add(Item);
                }
                if (busLinesList.Count==0)
                {
                    throw new NullReferenceException("No buses stop in this station!\n");
                }                
            }
            catch (NullReferenceException ex) { Console.WriteLine(ex.Message); }
            return busLinesList;
        }
        public List<BusLine> BusLinesSortedByGeneralTravelTime()
        {
           BusLines.Sort();
           return new List<BusLine>(BusLines);
        }
        public BusLine this[int index]
        {
            get
            {
                BusLine busLine = BusLines.Find(x => x.LineNumber == index);
                if (busLine==null)
                   throw new BusException("There is no bus line " + index + " in the system!");
                return busLine;
            }
        }
        
        public override String ToString()
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
