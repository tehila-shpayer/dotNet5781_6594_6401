﻿using System;
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
                int Intarea = rand.Next(0, 8);
                bl = new BusLine((Areas)Intarea);
                for (int j = i * 4 - 3; j <= i * 4; j++)
                {
                    bl.AddStation(j);
                }

                for (int k = 0; k < rand.Next(1, 4); k++)
                {
                    int anotherStationKey = rand.Next(1, 41);
                    while (bl.DidFindStation(anotherStationKey))
                    {
                        anotherStationKey = (anotherStationKey + 4) % 40 + 1;
                    }

                    bl.AddStation(anotherStationKey, rand.Next(0, 5 + k));
                }
                lineCollection.Add(bl);
            }    
            
            string s;
            Console.WriteLine("Welcome!");
              do
              {

                Console.WriteLine(@"
Choose one of the following actions to do on the collection:
  a: Add 
  d: Delete 
  s: Search
  p: Print
  e: Exit:");
                s = Console.ReadLine().Trim();
                try
                {
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
                                default: Console.WriteLine("ERROR"); break;
                            }
                            break;
                        case "e":
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                }
                catch (BusException ex)
                {
                    Console.WriteLine(ex);
                }
                catch (FormatException)
                {
                    Console.WriteLine("ERROR! You have to enter a number");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
              } while (s != "e");
        

       }
        public static void AddToCollection(BusLineCollection lineCollection)
        {
                Console.WriteLine("  a: Add a new bus line\n  b: Add a station to a bus line\n  c: Create a new station");
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
                        lineCollection.Add(new BusLine((Areas)intArea));
                        Console.WriteLine($"New bus line {BusLine.BUS_LINE_NUMBER} was added to collection!");
                        break;
                    case "b":
                        BusesInSystem(lineCollection);
                        Console.WriteLine("Please enter the bus number for adding a station:");
                        string stringNum = Console.ReadLine();
                        int num = int.Parse(stringNum);
                        BusLine bus = lineCollection[num];
                        Console.WriteLine("Please enter station's key to add:");
                        int key = ReadStationKey();
                        if (lineCollection[num].DidFindStation(key))
                        {
                            throw new BusException($"Station {key} is already in bus line {num}");
                        }
                        Console.WriteLine("Do you want to set station's location in the bus list of station?\n Press y if you want to\n Else press any key:");
                        String answer = Console.ReadLine();
                        if (answer == "y")
                        {
                            Console.WriteLine($"Please enter location of station in the line (index):\nEnter 0 to set the station as a first station\nEnter 1 to set the station as a second station\netc...\n(Bus line has {lineCollection[num].BusLineStations.Count} stations)");
                            string location = Console.ReadLine();
                            int loc = int.Parse(location);
                            bus.AddStation(key, loc);
                        }
                        else
                        { bus.AddStation(key); }
                        Console.WriteLine($"Station {key} was added to bus {num}!");
                        break;
                    case "c":
                        Console.WriteLine("Please enter station's location:\n Latitude:");
                        string stringLocation = Console.ReadLine();
                        double latitude = double.Parse(stringLocation);
                        Console.WriteLine("Longitude:");
                        stringLocation = Console.ReadLine();
                        double longitude = double.Parse(stringLocation);
                        Console.WriteLine("Do you want to set station's address?\n Press y if you want to\n Else press any key:");
                        answer = Console.ReadLine();
                        string address = "";
                        if (answer == "y")
                        {
                            Console.WriteLine("Please enter station's address:");
                            address = Console.ReadLine();
                        }
                        StationList.Add(new BusStation(latitude, longitude, address));
                        Console.WriteLine($"New bus station {BusStation.BUS_STATION_NUMBER} was added to the system!");
                        break;
                    default: Console.WriteLine("ERROR\n"); break;
                }
        }
        public static void DeleteFromCollection(BusLineCollection lineCollection)
        {
                Console.WriteLine("  a: Delete a bus line\n  b: Delete a station from a bus line\n  c: Delete a station from the system");
                string a = Console.ReadLine().Trim();

                String stringBus = "";
                int busNum;
            
            switch (a)
                {
                    case "a":
                    BusesInSystem(lineCollection);
                    Console.WriteLine("Please enter the bus number to delete:");
                        stringBus = Console.ReadLine();
                        busNum = int.Parse(stringBus);
                        lineCollection.Delete(lineCollection[busNum]);
                        Console.WriteLine($"Bus line {busNum} was removed from collection!");
                        break;
                    case "b":
                    BusesInSystem(lineCollection);
                    Console.WriteLine("Please enter the bus number to delete from:");
                        stringBus = Console.ReadLine();
                        busNum = int.Parse(stringBus);
                        BusLine bus = lineCollection[busNum];
                        if(lineCollection[busNum].BusLineStations.Count==0)
                        { Console.WriteLine($"Bus line {busNum} has no stations."); break; }
                        string stations = "Stations in the bus: ";
                        foreach (BusLineStation station in lineCollection[busNum].BusLineStations)
                        stations += station.StationKey + " ";
                        Console.WriteLine(stations + "\n");
                        Console.WriteLine("Please enter station key to delete:");
                        int stationNum = ReadStationKey();
                        if (!bus.DidFindStation(stationNum))
                            throw new BusException($"Bus line {busNum} has no station {stationNum}");
                        bus.DeleteStation(stationNum);
                        Console.WriteLine($"Station {stationNum} was removed from bus {busNum}!");
                        break;
                case "c":
                    Console.WriteLine("Please enter the station number to delete:");
                    stationNum = ReadStationKey();
                    if (BusesInStation(lineCollection, stationNum) != "\n")
                    {
                        Console.WriteLine("There are buses that pass in this station!\nAre you shure you want to delete it?\nThis action will remove this station from all buses");
                        Console.WriteLine(" Press 'y' to continue\n Else press any key");
                        String answer = Console.ReadLine();
                        if (answer != "y")
                        {
                            Console.WriteLine("The delete operation was canceled");
                            break;
                        }
                        foreach (BusLine busLine in lineCollection)
                        {
                            if (busLine.DidFindStation(stationNum))
                                busLine.DeleteStation(stationNum);
                        }
                    }
                    StationList.Remove(stationNum);
                    Console.WriteLine($"Station {stationNum} was removed from the system!");
                    break;
                    default: Console.WriteLine("ERROR"); break;
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
                    int key = ReadStationKey(); 
                    Console.Write("The bus lines in this station: ");
                    PrintBusesInStation(lineCollection, key);
                    break;
                case "b":
                    bool flag = false;
                    Console.WriteLine("Please enter starting station key:");
                    int startStationNum = ReadStationKey();
                    Console.WriteLine("Please enter destination station key:");
                    int destStationNum = ReadStationKey();
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
                        Console.WriteLine($"The possible routes to take from station {startStationNum} to station {destStationNum}:");
                        foreach (BusLine bus in BusesToDstination.BusLinesSortedByGeneralTravelTime())
                        {
                            Console.WriteLine(bus);
                            BusLineStation first = bus.getStationFromKey(bus.FirstStation);
                            BusLineStation last = bus.getStationFromKey(bus.LastStation);
                            Console.WriteLine($"Time: {bus.FindTime(first,last)} minutes.\n");
                        }
                    }
                    else
                        Console.WriteLine($"Sorry, there are no routes from station {startStationNum} to station {destStationNum}");
                    break;
                default: Console.WriteLine("ERROR"); break;
            }
        }
        public static void PrintBusesInStation(BusLineCollection lineCollection, int key)
        {
            Console.WriteLine(BusesInStation(lineCollection, key));
        }
        public static String BusesInStation(BusLineCollection lineCollection,int key)
        {
            String s = "";
            try
            {
                foreach (BusLine bus in lineCollection.BusLineInStationList(key))
                    s += bus.LineNumber + ", ";
                if (s != "")
                {
                    s = s.Substring(0, s.Length - 2);
                }
            }
            catch (BusException)
            {
            }
            return s + "\n";
        }
        public static int ReadStationKey()
        {
            string stringKey = Console.ReadLine();
            int key = int.Parse(stringKey);
            if (!StationList.StationExists(key))
                throw new BusException($"Station {key} doesn't exist in the system");
            return key;
        }
        public static void BusesInSystem(BusLineCollection lineCollection)
        {
            string buses = "The bus lines in the system: ";
            foreach (BusLine b in lineCollection)
                buses += b.LineNumber + " ";
            Console.WriteLine(buses + "\n");
        }
    }

    [Serializable]
    class BusException : Exception
    {
        public BusException() : base() { }
        public BusException(String message) : base(message) { }
        public BusException(String message, Exception inner) : base(message, inner) { }
       // public BusLineException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return "ERROR! " + Message;
        }
    };
}
