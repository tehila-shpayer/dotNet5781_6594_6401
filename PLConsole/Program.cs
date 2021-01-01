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
        static IBL bl = BLFactory.GetBL("1");
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
                            //DeleteFromCollection();//מחיקה מהמערכת
                            break;
                        case "s":
                            //SearchInCollection();//חיפוש במערכת
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
                    int intArea = int.Parse(Console.ReadLine().Trim());
                    BO.BusLine busLine = new BO.BusLine();
                    busLine.Area = (BO.Areas)intArea;
                    Console.WriteLine("enter bus line number: ");
                    int lineNumber = int.Parse(Console.ReadLine());
                    busLine.LineNumber = lineNumber;
                    bl.AddBusLine(busLine);
                    Console.WriteLine($"New bus line {lineNumber} was added to collection!");
                    break;
                case "b"://הוספת תחנה קיימת למסלול של קו אוטובוס
                    Console.WriteLine("Please enter the bus line key for adding a station:");
                    int lineKey = int.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter station's key to add:");
                    int stationKey = int.Parse(Console.ReadLine());
                    Console.WriteLine("Do you want to set station's location in the bus list of station?\n Press 'y' if you want to\n Else press any key:");
                    String answer = Console.ReadLine();
                    if (answer == "y")//אם המשתמש רוצה להכניס את התחנה במקום מסויים במסלול
                    {
                        Console.WriteLine($"Please enter location of station in the line (index):\nEnter 0 to set the station as a first station\nEnter 1 to set the station as a second station\netc...\n(Bus line has x stations)");
                        int loc = int.Parse(Console.ReadLine());
                        bl.AddStationToLine(lineKey, stationKey, loc);//הוספת התחנה למקום המבוקש
                    }
                    else//אם המשתמש לא בחר מיקום מסויים, התחנה מתווספת לסוף המסלול
                    { bl.AddStationToLine(lineKey, stationKey); }
                    Console.WriteLine($"Station {stationKey} was added to bus line {lineKey}!");
                    break;
                case "c"://הוספת תחנה חדשה למערכת
                    Console.WriteLine("Please enter station's location:\n Latitude:");
                    double latitude = double.Parse(Console.ReadLine());
                    Console.WriteLine("Longitude:");
                    double longitude = double.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter station's address:");
                    string address = Console.ReadLine();
                    BO.Station station = new BO.Station();
                    station.Latitude = latitude;
                    station.Longitude = longitude;
                    station.Name = address;
                    bl.AddStation(station);
                    Console.WriteLine($"New bus station was added to the system!");
                    break;
                default: Console.WriteLine("ERROR\n"); break;
            }
        }
        public static void PrintDataOfCollection()
        {
            Console.WriteLine("  a: print all bus lines\n  b: print all stations\n  c: print data of one bus line");
            string p = Console.ReadLine().Trim();
            switch (p)
            {
                case "a"://הדפסת הנתונים על כל קווי האוטובוס
                    Console.WriteLine("bus lines:");
                    var busLines = from busLine in bl.GetAllBusLines()
                                   select bl.ToStringBusLine(busLine);
                    foreach (String busLine in busLines)
                        Console.WriteLine(busLine);
                    break;
                case "b"://הדפסת הנתונים על כל התחנות
                    var stations = from station in bl.GetAllStations()
                                   select station.ToString();
                    foreach (String station in stations)
                        Console.WriteLine(station);
                    break;
                case "c"://הדפסת נתונים על קו אוטובוס מסויים
                    Console.WriteLine("enter bus line key: ");
                    string lineNumber = Console.ReadLine();
                    int intLineNumber = int.Parse(lineNumber);
                    BO.BusLine b = bl.GetBusLine(intLineNumber);
                    Console.WriteLine(bl.ToStringBusLine(b));
                    break;
                default: Console.WriteLine("ERROR"); break;
            }
        }
    }
}
    
       
        //    public static void DeleteFromCollection(BusLineCollection lineCollection)
        //    {
        //        Console.WriteLine("  a: Delete a bus line\n  b: Delete a station from a bus line\n  c: Delete a station from the system");
        //        string a = Console.ReadLine().Trim();

        //        String stringBus = "";
        //        int busNum;

        //        switch (a)
        //        {
        //            case "a"://מחיקת קו אוטובוס
        //                Console.WriteLine("Please enter the bus number to delete:");
        //                stringBus = Console.ReadLine();
        //                busNum = int.Parse(stringBus);
        //                lineCollection.Delete(lineCollection[busNum]);
        //                Console.WriteLine($"Bus line {busNum} was removed from collection!");
        //                break;
        //            case "b"://מחיקת תחנה ממסלול של קו אוטובוס
        //                BusesInSystem(lineCollection);
        //                Console.WriteLine("Please enter the bus number to delete from:");
        //                stringBus = Console.ReadLine();
        //                busNum = int.Parse(stringBus);
        //                BusLine bus = lineCollection[busNum];
        //                if (lineCollection[busNum].BusLineStations.Count == 0)//אם אין תחנות בקו זה- נזרקת חריגה
        //                { Console.WriteLine($"Bus line {busNum} has no stations."); break; }
        //                string stations = "Stations in the bus: ";//הדפסת כל מספרי התחנות שקו האוטובוס עובר בהן
        //                foreach (BusLineStation station in lineCollection[busNum].BusLineStations)
        //                {
        //                    stations += station.StationKey + " ";
        //                }
        //                Console.WriteLine(stations + "\n");
        //                Console.WriteLine("Please enter station key to delete:");
        //                int stationNum = ReadStationKey();
        //                if (!bus.DidFindStation(stationNum))//אם התחנה כלל לא נמצאת במסלול הקו
        //                    throw new BusException($"Bus line {busNum} has no station {stationNum}");
        //                bus.DeleteStation(stationNum);
        //                Console.WriteLine($"Station {stationNum} was removed from bus {busNum}!");
        //                break;
        //            case "c"://מחיקת תחנת אוטובוס מהמערכת
        //                Console.WriteLine("Please enter the station number to delete:");
        //                stationNum = ReadStationKey();
        //                if (BusesInStation(lineCollection, stationNum) != "\n")//אם יש קווי אוטובוס העוברים בתחנה זו
        //                {
        //                    //אזהרה למשתמש: מחיקת התחנה תמחק אותה ממסלולי האוטובוסים
        //                    Console.WriteLine("There are buses that pass in this station!\nAre you sure you want to delete it?\nThis action will remove this station from all buses");
        //                    Console.WriteLine(" Press 'y' to continue\n Else press any key");
        //                    String answer = Console.ReadLine();
        //                    if (answer != "y")//אם המשתמש לא רוצה למחוק את התחנה
        //                    {
        //                        Console.WriteLine("The delete operation was canceled");//ביטול פעולת המחיקה
        //                        break;
        //                    }
        //                    //אחרת, אם המשתמש בכל זאת רוצה למחוק את התחנה
        //                    foreach (BusLine busLine in lineCollection)//מחיקת התחנה ממסלולי האוטובוסים שעוברים בה
        //                    {
        //                        if (busLine.DidFindStation(stationNum))
        //                            busLine.DeleteStation(stationNum);
        //                    }
        //                }
        //                StationList.Remove(stationNum);//מחיקת התחנה מהמערכת
        //                Console.WriteLine($"Station {stationNum} was removed from the system!");
        //                break;
        //            default: Console.WriteLine("ERROR"); break;
        //        }

        //    }
        //    public static void SearchInCollection(BusLineCollection lineCollection)
        //    {
        //        Console.WriteLine("  a: Print all buses that pass a station\n  b: Print optional routes to destination");
        //        string s = Console.ReadLine().Trim();
        //        switch (s)
        //        {
        //            case "a": //הדפסת כל הקווים העוברים בתחנה מסויימת           
        //                Console.WriteLine("Enter station key to search:");
        //                int key = ReadStationKey();
        //                Console.Write("The bus lines in this station: ");
        //                PrintBusesInStation(lineCollection, key);
        //                break;
        //            case "b": //הדפסת מסלולי נסיעה אפשריים בהינתן תחנות מוצא ויעד
        //                bool flag = false;
        //                Console.WriteLine("Please enter starting station key:");
        //                int startStationNum = ReadStationKey();
        //                Console.WriteLine("Please enter destination station key:");
        //                int destStationNum = ReadStationKey();
        //                BusLineCollection BusesToDstination = new BusLineCollection();
        //                foreach (BusLine bus in lineCollection)
        //                {
        //                    if (bus.DidFindStation(startStationNum) && bus.DidFindStation(destStationNum))//אם ישנו אוטובוס שעובר גם בתחנת המוצא וגם ביעד
        //                    {
        //                        BusLineStation start = bus.getStationFromKey(startStationNum);
        //                        BusLineStation destination = bus.getStationFromKey(destStationNum);
        //                        BusLine busLine;
        //                        if (bus.IstationPrior(start, destination))//אם האוטובוס עובר קודם בתחנת המוצא ולאחר מכן בתחנת היעד
        //                        {
        //                            flag = true;
        //                            busLine = bus.GetSubBusLine(start, destination);
        //                            BusesToDstination.Add(busLine);//הוספת האוטובוס לאוסף קווי האוטובוס המתאימים
        //                        }
        //                    }
        //                }
        //                if (flag)//אם נמצא לפחות אוטובוס אחד שמגיע מתחנת המוצא ליעד
        //                {
        //                    //הדפסת מסלולי הנסיעה האפשריים, ממויינים לפי זמן הנסיעה
        //                    Console.WriteLine($"The possible routes to take from station {startStationNum} to station {destStationNum}:");
        //                    foreach (BusLine bus in BusesToDstination.BusLinesSortedByGeneralTravelTime())
        //                    {
        //                        Console.WriteLine(bus);
        //                        BusLineStation first = bus.getStationFromKey(bus.FirstStation);
        //                        BusLineStation last = bus.getStationFromKey(bus.LastStation);
        //                        Console.WriteLine($"Time: {bus.FindTime(first, last)} minutes.\n");
        //                    }
        //                }
        //                else//אם אין אף אוטובוס שמגיע מתחנת המוצא ליעד  
        //                    Console.WriteLine($"Sorry, there are no routes from station {startStationNum} to station {destStationNum}");
        //                break;
        //            default: Console.WriteLine("ERROR"); break;
        //        }
        //    }

  
        //    public static void PrintBusesInStation(BusLineCollection lineCollection, int key)//מדפיס את כל האוטובוסים העוברים בתחנה מסויימת
        //    {
        //        Console.WriteLine(BusesInStation(lineCollection, key));
        //    }
        //    public static String BusesInStation(BusLineCollection lineCollection, int key)//מחזיר מחרוזת המתארת את כל האוטובוסים העוברים בתחנה מסויימת
        //    {
        //        String s = "";
        //        try
        //        {
        //            foreach (BusLine bus in lineCollection.BusLineInStationList(key))
        //                s += bus.LineNumber + ", ";
        //            if (s != "")
        //            {
        //                s = s.Substring(0, s.Length - 2);
        //            }
        //        }
        //        catch (BusException)
        //        {
        //        }
        //        return s + "\n";
        //    }

        //}
    

