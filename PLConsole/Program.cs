using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLAPI;


namespace PLConsole
{
    public class Program
    {
        static public IBL bl = BLFactory.GetBL("1");
        static void Main(string[] args)
        {
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
                            AddToCollection();//הוספה למערכת
                            break;
                        case "d":
                            DeleteFromCollection();//מחיקה מהמערכת
                            break;
                        case "s":
                            SearchInCollection();//חיפוש במערכת
                            break;
                        case "p"://הדפסת נתונים
                            PrintDataOfCollection();
                            break;
                        case "e"://יציאה
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                }
                catch (Exception ex)// תופס חריגות הקשורות לקלטים לא מתאימים הקשורים לאוטובוסים/תחנות 
                {
                    Console.WriteLine(ex);
                }
            } while (s != "e");


        }
        public static void AddToCollection()
        {
            Console.WriteLine("  a: Add a new bus line\n  b: Add a station to a bus line\n  c: Create a new station");
            string a = Console.ReadLine().Trim();
            switch (a)
            {
                case "a"://הוספת קו אוטובוס חדש
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
                    BO.BusLine busLine = new BO.BusLine();
                    busLine.area = (BO.Areas)intArea;
                    busLine.LineKey = BO.BusLine.BUS_LINE_NUMBER;
                    BO.BusLine.BUS_LINE_NUMBER++;

                     .Add(new BusLine((Areas)intArea));
                    Console.WriteLine($"New bus line {BusLine.BUS_LINE_NUMBER} was added to collection!");
                    break;
                case "b"://הוספת תחנה קיימת למסלול של קו אוטובוס
                    BusesInSystem(lineCollection);
                    Console.WriteLine("Please enter the bus number for adding a station:");
                    string stringNum = Console.ReadLine();
                    int num = int.Parse(stringNum);
                    BusLine bus = lineCollection[num];
                    Console.WriteLine("Please enter station's key to add:");
                    int key = ReadStationKey();
                    if (lineCollection[num].DidFindStation(key))//אם התחנה כבר נמצאת במסלול הקו - זורק חריגה
                    {
                        throw new BusException($"Station {key} is already in bus line {num}");
                    }
                    Console.WriteLine("Do you want to set station's location in the bus list of station?\n Press 'y' if you want to\n Else press any key:");
                    String answer = Console.ReadLine();
                    if (answer == "y")//אם המשתמש רוצה להכניס את התחנה במקום מסויים במסלול
                    {
                        Console.WriteLine($"Please enter location of station in the line (index):\nEnter 0 to set the station as a first station\nEnter 1 to set the station as a second station\netc...\n(Bus line has {lineCollection[num].BusLineStations.Count} stations)");
                        string location = Console.ReadLine();
                        int loc = int.Parse(location);
                        bus.AddStation(key, loc);//הוספת התחנה למקום המבוקש
                    }
                    else//אם המשתמש לא בחר מיקום מסויים, התחנה מתווספת לסוף המסלול
                    { bus.AddStation(key); }
                    Console.WriteLine($"Station {key} was added to bus {num}!");
                    break;
                case "c"://הוספת תחנה חדשה למערכת
                    Console.WriteLine("Please enter station's location:\n Latitude:");
                    string stringLocation = Console.ReadLine();
                    double latitude = double.Parse(stringLocation);
                    Console.WriteLine("Longitude:");
                    stringLocation = Console.ReadLine();
                    double longitude = double.Parse(stringLocation);
                    Console.WriteLine("Do you want to set station's address?\n Press 'y' if you want to\n Else press any key:");
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
                case "a"://מחיקת קו אוטובוס
                    BusesInSystem(lineCollection);
                    Console.WriteLine("Please enter the bus number to delete:");
                    stringBus = Console.ReadLine();
                    busNum = int.Parse(stringBus);
                    lineCollection.Delete(lineCollection[busNum]);
                    Console.WriteLine($"Bus line {busNum} was removed from collection!");
                    break;
                case "b"://מחיקת תחנה ממסלול של קו אוטובוס
                    BusesInSystem(lineCollection);
                    Console.WriteLine("Please enter the bus number to delete from:");
                    stringBus = Console.ReadLine();
                    busNum = int.Parse(stringBus);
                    BusLine bus = lineCollection[busNum];
                    if (lineCollection[busNum].BusLineStations.Count == 0)//אם אין תחנות בקו זה- נזרקת חריגה
                    { Console.WriteLine($"Bus line {busNum} has no stations."); break; }
                    string stations = "Stations in the bus: ";//הדפסת כל מספרי התחנות שקו האוטובוס עובר בהן
                    foreach (BusLineStation station in lineCollection[busNum].BusLineStations)
                    {
                        stations += station.StationKey + " ";
                    }
                    Console.WriteLine(stations + "\n");
                    Console.WriteLine("Please enter station key to delete:");
                    int stationNum = ReadStationKey();
                    if (!bus.DidFindStation(stationNum))//אם התחנה כלל לא נמצאת במסלול הקו
                        throw new BusException($"Bus line {busNum} has no station {stationNum}");
                    bus.DeleteStation(stationNum);
                    Console.WriteLine($"Station {stationNum} was removed from bus {busNum}!");
                    break;
                case "c"://מחיקת תחנת אוטובוס מהמערכת
                    Console.WriteLine("Please enter the station number to delete:");
                    stationNum = ReadStationKey();
                    if (BusesInStation(lineCollection, stationNum) != "\n")//אם יש קווי אוטובוס העוברים בתחנה זו
                    {
                        //אזהרה למשתמש: מחיקת התחנה תמחק אותה ממסלולי האוטובוסים
                        Console.WriteLine("There are buses that pass in this station!\nAre you sure you want to delete it?\nThis action will remove this station from all buses");
                        Console.WriteLine(" Press 'y' to continue\n Else press any key");
                        String answer = Console.ReadLine();
                        if (answer != "y")//אם המשתמש לא רוצה למחוק את התחנה
                        {
                            Console.WriteLine("The delete operation was canceled");//ביטול פעולת המחיקה
                            break;
                        }
                        //אחרת, אם המשתמש בכל זאת רוצה למחוק את התחנה
                        foreach (BusLine busLine in lineCollection)//מחיקת התחנה ממסלולי האוטובוסים שעוברים בה
                        {
                            if (busLine.DidFindStation(stationNum))
                                busLine.DeleteStation(stationNum);
                        }
                    }
                    StationList.Remove(stationNum);//מחיקת התחנה מהמערכת
                    Console.WriteLine($"Station {stationNum} was removed from the system!");
                    break;
                default: Console.WriteLine("ERROR"); break;
            }

        }
        public static void SearchInCollection(BusLineCollection lineCollection)
        {
            Console.WriteLine("  a: Print all buses that pass a station\n  b: Print optional routes to destination");
            string s = Console.ReadLine().Trim();
            switch (s)
            {
                case "a": //הדפסת כל הקווים העוברים בתחנה מסויימת           
                    Console.WriteLine("Enter station key to search:");
                    int key = ReadStationKey();
                    Console.Write("The bus lines in this station: ");
                    PrintBusesInStation(lineCollection, key);
                    break;
                case "b": //הדפסת מסלולי נסיעה אפשריים בהינתן תחנות מוצא ויעד
                    bool flag = false;
                    Console.WriteLine("Please enter starting station key:");
                    int startStationNum = ReadStationKey();
                    Console.WriteLine("Please enter destination station key:");
                    int destStationNum = ReadStationKey();
                    BusLineCollection BusesToDstination = new BusLineCollection();
                    foreach (BusLine bus in lineCollection)
                    {
                        if (bus.DidFindStation(startStationNum) && bus.DidFindStation(destStationNum))//אם ישנו אוטובוס שעובר גם בתחנת המוצא וגם ביעד
                        {
                            BusLineStation start = bus.getStationFromKey(startStationNum);
                            BusLineStation destination = bus.getStationFromKey(destStationNum);
                            BusLine busLine;
                            if (bus.IstationPrior(start, destination))//אם האוטובוס עובר קודם בתחנת המוצא ולאחר מכן בתחנת היעד
                            {
                                flag = true;
                                busLine = bus.GetSubBusLine(start, destination);
                                BusesToDstination.Add(busLine);//הוספת האוטובוס לאוסף קווי האוטובוס המתאימים
                            }
                        }
                    }
                    if (flag)//אם נמצא לפחות אוטובוס אחד שמגיע מתחנת המוצא ליעד
                    {
                        //הדפסת מסלולי הנסיעה האפשריים, ממויינים לפי זמן הנסיעה
                        Console.WriteLine($"The possible routes to take from station {startStationNum} to station {destStationNum}:");
                        foreach (BusLine bus in BusesToDstination.BusLinesSortedByGeneralTravelTime())
                        {
                            Console.WriteLine(bus);
                            BusLineStation first = bus.getStationFromKey(bus.FirstStation);
                            BusLineStation last = bus.getStationFromKey(bus.LastStation);
                            Console.WriteLine($"Time: {bus.FindTime(first, last)} minutes.\n");
                        }
                    }
                    else//אם אין אף אוטובוס שמגיע מתחנת המוצא ליעד  
                        Console.WriteLine($"Sorry, there are no routes from station {startStationNum} to station {destStationNum}");
                    break;
                default: Console.WriteLine("ERROR"); break;
            }
        }

        public static void PrintDataOfCollection(BusLineCollection lineCollection)
        {
            Console.WriteLine("  a: print all bus lines\n  b: print all stations\n  c: print data of one bus line");
            string p = Console.ReadLine().Trim();
            switch (p)
            {
                case "a"://הדפסת הנתונים על כל קווי האוטובוס
                    Console.WriteLine(lineCollection);
                    break;
                case "b"://הדפסת הנתונים על כל התחנות
                    foreach (BusStation station in StationList.Stations)
                    {
                        Console.WriteLine(station);

                        Console.Write("   The bus lines in this station: ");
                        PrintBusesInStation(lineCollection, station.BusStationKey);
                    }
                    break;
                case "c"://הדפסת נתונים על קו אוטובוס מסויים
                    BusesInSystem(lineCollection);
                    Console.WriteLine("Please enter the bus number to print: ");
                    string stringNum = Console.ReadLine();
                    int busNum = int.Parse(stringNum);
                    Console.WriteLine(lineCollection[busNum]);
                    break;
                default: Console.WriteLine("ERROR"); break;
            }
        }
        public static void PrintBusesInStation(BusLineCollection lineCollection, int key)//מדפיס את כל האוטובוסים העוברים בתחנה מסויימת
        {
            Console.WriteLine(BusesInStation(lineCollection, key));
        }
        public static String BusesInStation(BusLineCollection lineCollection, int key)//מחזיר מחרוזת המתארת את כל האוטובוסים העוברים בתחנה מסויימת
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

    }
    }
}
