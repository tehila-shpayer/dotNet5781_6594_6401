using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Globalization;

namespace dotNet5781_02_6594_6401
{
    class Program
    {

        static void Main(string[] args)
        {
            //חסר:
            //לטפל בחריגות בכול הפונקציות - לבדוק שהפרמטרים שמקבלים קבילים
            //שיניתי את תחנה ראשונה ואחרונה להיות תחנת אוטובוס כי הייתי חייבת וזה לא משנה ככ
            //זהו נראה לי:)

            Random rand = new Random(DateTime.Now.Millisecond);

            //creates 40 stations
            BusStation bs;
            bs = new BusStation(31.234567, 34.56874, "Talya");
            StationList.Add(bs);
            for (int i = 0; i < 39; i++)
            {
                double la, lo;

                la = rand.NextDouble() * (33.3 - 31) + 31;

                lo = rand.NextDouble() * (35.5 - 34.3) + 34.3;

                bs = new BusStation(la, lo, "");
                StationList.Add(bs);
            }
            //Console.WriteLine(StationList.ToString());
           
            BusLine bl;
            BusLineCollection lineCollection = new BusLineCollection();
            for (int i = 1; i <= 10; i++) // craetes 10 bus lines
            {
                int Intarea = (rand.Next(0, 8));
                bl = new BusLine((Areas)Intarea);
                BusLineStation bls;
                for (int j = i * 4 - 3; j <= i * 4; j++)
                {
                    bls = new BusLineStation(j);
                    bl.AddStation(bls.StationKey);
                }

                for (int k = 0; k < rand.Next(1, 4); k++)
                {
                    int anotherStationKey = rand.Next(1, 41);
                    while (bl.DidFindStation(anotherStationKey))
                    {
                        anotherStationKey = (anotherStationKey + 4) % 40 + 1;
                    }
                    bls = new BusLineStation(anotherStationKey, 1, 1);

                    bl.AddStation(bls.StationKey, rand.Next(0, 5 + k));
                }
                lineCollection.Add(bl);
            }            
            string s;
            Console.WriteLine("Welcome!\n");
            do
            {
                Console.WriteLine(@"Choose one of the following actions to do on the collection:
  a: Add 
  d: Delete 
  s: Search
  p: Print
  e: Exit:");
                s = Console.ReadLine().Trim();

                switch (s)
                {
                    case "a":
                        AddToCollection(lineCollection);
                        break;
                    case "d":
                        DeleteFromCollection(lineCollection);
                        break;
                    case "s":
                        SearchInCollection(lineCollection);
                        break;
                    case "p":
                        Console.WriteLine("  a: print all bus line\n  b: print data of all stations");
                        string p = Console.ReadLine();
                        switch (p)
                        {
                            case "a":
                                Console.WriteLine(lineCollection);
                                break;
                            case "b":
                                foreach (BusStation station in StationList.Stations)
                                {
                                    Console.WriteLine(station);
                                    
                                    Console.Write("   The bus lines in this station: ");
                                    PrintBusesInStation(lineCollection, station.BusStationKey);
                                }
                                break;
                            default: Console.WriteLine("ERROR\n"); break;
                        }
                        break;
                    case "e":
                        break;
                    default:
                        Console.WriteLine("ERROR\n");
                        break;
                }
            } while (s != "e");
        }
        public static void AddToCollection(BusLineCollection lineCollection)
        {
            try
            {
                Console.WriteLine("  a: Add new bus line\n  b: Add new station to a bus line");
                string a = Console.ReadLine().Trim();
                switch (a)
                {
                    case "a":

                        Console.WriteLine(@"Choose one of the following areas for the bus:
  0: General
  1: Jerusalem
  2: Center
  3: North
  4: South
  5: Hifa
  6: TelAviv
  7: YehudaAndShomron");
                        string area = Console.ReadLine().Trim();
                        int intArea = int.Parse(area);
                        if (intArea < 0 || intArea > 7)
                            throw new ArgumentOutOfRangeException("Area must be from the list!");
                        lineCollection.Add(new BusLine((Areas)intArea));
                        Console.WriteLine($"New bus line {BusLine.BUS_LINE_NUMBER} was added to collection!");
                        break;
                    case "b":
                        Console.WriteLine("Please enter the bus number for the station:");
                        string stringNum = Console.ReadLine();
                        int num = int.Parse(stringNum);
                        if (!lineCollection.IsBus(num))
                            throw new ArgumentException("There is no bus line " + lineCollection[num].LineNumber + " in the bus line collection!");
                        int key;
                        Console.WriteLine("Do you to create a new station?\n press YES.");
                        string answer = Console.ReadLine();
                        if (answer == "YES")
                        {
                            Console.WriteLine("Please enter station's location:\n Latitude:");
                            string stringLa = Console.ReadLine();
                            int la = int.Parse(stringLa);
                            double latitude = Convert.ToDouble(la);
                            Console.WriteLine("Longitude:");
                            string stringLo = Console.ReadLine();
                            int lo = int.Parse(stringLo);
                            double longitude = Convert.ToDouble(lo);
                            Console.WriteLine("Do you want to set station's address?\n Press YES.");
                            answer = Console.ReadLine();
                            string address = "";
                            if (answer == "YES")
                            {
                                Console.WriteLine("Please enter station's address:");
                                address = Console.ReadLine();
                            }
                            StationList.Add(new BusStation(latitude, longitude, address));
                            key = BusStation.BUS_STATION_NUMBER;
                        }
                        else
                        {
                            Console.WriteLine("Please enter station's key:");
                            string stringKey = Console.ReadLine();
                            key = int.Parse(stringKey);
                        }
                        if (lineCollection[num].DidFindStation(key))
                        {
                            Console.WriteLine($"This station is already in bus line {num}");
                            break;
                        }
                        Console.WriteLine("Do you want to set station's location in the bus list of station?\n Press YES.");
                        answer = Console.ReadLine();
                        if (answer == "YES")
                        {
                            Console.WriteLine("Please enter location of station in the line (index):");
                            string location = Console.ReadLine();
                            int loc = int.Parse(location);
                            lineCollection[num].AddStation(key, loc);
                        }
                        else
                        { lineCollection[num].AddStation(key); }
                        Console.WriteLine($"Station {key} was added to bus {num}!");
                        break;
                    default: Console.WriteLine("ERROR\n"); break;
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        public static void DeleteFromCollection(BusLineCollection lineCollection)
        {
            Console.WriteLine("  a: Delete a bus line\n  b: Delete a station from a bus line");
            string a = Console.ReadLine().Trim();
            Console.WriteLine("Please enter the bus number to delete/delete from:");
            string stringBus = Console.ReadLine();
            int busNum = int.Parse(stringBus);
            switch (a)
            {
                case "a":
                    lineCollection.Delete(lineCollection[busNum]);
                    Console.WriteLine($"Bus line {busNum} was removed from collection!");
                    break;
                case "b":
                    Console.WriteLine("Please enter station key to delete:");
                    string stringStation = Console.ReadLine();
                    int stationNum = int.Parse(stringStation);
                    BusLine bus = lineCollection[busNum];
                    bus.DeleteStation(bus.getStationFromKey(stationNum));
                    Console.WriteLine($"Station {stationNum} was removed from bus {busNum}!");
                    break;
                default: Console.WriteLine("ERROR\n"); break;
            }
        }
        public static void SearchInCollection(BusLineCollection lineCollection)
        {
            Console.WriteLine("  a: Print all buses that pass a station\n  b: Print optinal routes to destination");
            string s = Console.ReadLine().Trim();
            switch (s)
            {
                case "a":                    
                    Console.WriteLine("Enter station key to search:");
                    string stringKey = Console.ReadLine();
                    int key = int.Parse(stringKey);
                    Console.Write("   The bus lines in this station: ");
                    PrintBusesInStation(lineCollection,key);
                    break;
                case "b":
                    bool flag = false;
                    Console.WriteLine("Please enter starting station key:");
                    string stringStation = Console.ReadLine();
                    int startStationNum = int.Parse(stringStation);
                    Console.WriteLine("Please enter destination station key:");
                    stringStation = Console.ReadLine();
                    int destStationNum = int.Parse(stringStation);
                    BusLineCollection BusesToDstination = new BusLineCollection();
                    foreach (BusLine bus in lineCollection)
                    {
                        if (bus.DidFindStation(startStationNum) && bus.DidFindStation(destStationNum))
                        {
                            BusLineStation start = bus.getStationFromKey(startStationNum);
                            BusLineStation destination = bus.getStationFromKey(destStationNum);
                            BusLine busLine;
                            if (bus.IstationPrior(start, destination))
                            {
                                flag = true;
                                busLine = bus.GetSubBusLine(start, destination);
                                BusesToDstination.Add(busLine);
                            }
                        }
                    }
                    if (flag)
                    {
                        Console.WriteLine("The possible routes to take from start station to destination:");
                        foreach (BusLine bus in BusesToDstination.BusLinesSortedByGeneralTravelTime())
                        {
                            Console.WriteLine(bus);
                            BusLineStation first = bus.getStationFromKey(bus.FirstStation);
                            BusLineStation last = bus.getStationFromKey(bus.LastStation);
                            Console.WriteLine($"Time: {bus.FindTime(first,last)} minutes.\n");
                        }
                    }
                    break;
                default: Console.WriteLine("ERROR\n"); break;
            }
        }
        public static void PrintBusesInStation(BusLineCollection lineCollection, int key)
        {
            string s = "";
            foreach (BusLine bus in lineCollection.BusLineInStationList(key))
                s += bus.LineNumber + ", ";
            if (s != "")
                s = s.Substring(0, s.Length - 2);
            Console.WriteLine(s + "\n");
        }
    }
}
