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

            BusStation bs;

            for (int i = 0; i < 40; i++)//creates 40 stations
            {
                double la, lo;

                la = rand.NextDouble() * (33.3 - 31) + 31;

                lo = rand.NextDouble() * (35.5 - 34.3) + 34.3;

                bs = new BusStation(la, lo, "");
                StationList.Add(bs);
            }
            bs = new BusStation(31.234567, 34.56874, "Talya");
            StationList.Add(bs);
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
                    bls = new BusLineStation(j, 1, 1);
                    bl.AddStation(bls);
                }

                for (int k = 0; k < rand.Next(1, 4); k++)
                {
                    int anotherStationKey = rand.Next(1, 42);
                    while (bl.DidFindStation(anotherStationKey))
                    {
                        anotherStationKey = (anotherStationKey + 4) % 41 + 1;
                    }
                    bls = new BusLineStation(anotherStationKey, 1, 1);

                    bl.AddStation(rand.Next(0, 5 + k), bls);
                }
                lineCollection.Add(bl);
            }
            //Console.WriteLine(lineCollection);

            string s;
            Console.WriteLine("Welcome!\n");
            do
            {
                Console.WriteLine(@"Choose one of the following:
  a: Add a new bus
  b: Choose a bus for a ride
  c: Refuel a bus or send to treatment
  p: Print the data of the buses
  e: exit:");
                s = Console.ReadLine().Trim();

                switch (s)
                {
                    case "a":
                        break;
                    case "b":
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
                                    string lineInStation = "";
                                    foreach (BusLine line in lineCollection)
                                    {
                                        
                                        if (line.DidFindStation(station.BusStationKey))
                                        {
                                            lineInStation += (line.LineNumber + ", ");
                                        } 
                                    }
                                    Console.WriteLine(lineInStation.Substring(0, lineInStation.Length-2)+"\n");
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
    }
}
