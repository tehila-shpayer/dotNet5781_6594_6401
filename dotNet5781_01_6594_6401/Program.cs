using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6594_6401
{
    class Program
    {
        
        static void Main(string[] args)
        {
//סיימתי הרוב, חסר:
//אפשרות גם לתדלק וגם לתת טיפול
//הצעה לעשות זאת לאחר הוספת אוטובוס
//לסדר בפונקציות את התוכנית הראשית

            List<Bus> buses = new List<Bus>();
            Random rand = new Random(DateTime.Now.Millisecond);
            string s;
            Console.WriteLine("Welcome!");
            do
            {

                Console.WriteLine(@"Choose one of the following:
  a: Add a new bus
  b: Choose a bus for a ride
  c: Refuel a bus or send to treatment
  d: Print the data of the buses
  e: exit:");

                s = Console.ReadLine();
                string busNum;
                switch (s)
                {
                    case "a":
                        Console.WriteLine("Enter the start date of the bus activity:");
                        Console.WriteLine("Enter the year:");
                        int year = ReadYear();

                        Console.WriteLine("Enter the month:");
                        int month = ReadMonth();
                      
                        Console.WriteLine("Enter the day:");
                        int day = ReadDay();
                        DateTime startDate = new DateTime(year, month, day);
                        Console.WriteLine("Enter the bus number:");
                        busNum = ReadBusNum(startDate);
                        Bus newBus = new Bus(startDate, busNum);
                        buses.Add(newBus);
                        Console.WriteLine("You successfully added the bus to the system!\ndo you want it to start runnig? press 1 to refuel and treatment else press any key");
                        string one = Console.ReadLine();
                        if (one == "1")
                            newBus.RefuelAndTreat();
                        break;
                    case "b":
                        bool flag = false;
                        Console.WriteLine("Enter the bus license you wish to ride in: ");
                        busNum = Console.ReadLine();
                        foreach (Bus b in buses)
                        { 
                            if (b.LicenseNumber == busNum)
                            {
                                Console.WriteLine(b.Ride((int)(rand.Next(1200))));
                                flag = true;
                                break;
                            }
                        }
                        if(!flag)
                            Console.WriteLine("Sorry, The bus doesn't exist in the system.\n");
                        break;
                    case "c":
                        bool fl = true;
                        Bus helpBus=new Bus(new DateTime());
                        Console.WriteLine("Enter the bus license you wish to refuel or treat: ");
                        s = Console.ReadLine();
                        foreach (Bus b in buses)
                        {
                            if (b.LicenseNumber == s)
                            {
                                helpBus = b;
                                break;
                            }
                        }
                        if (helpBus.LicenseNumber == "")
                        {
                            Console.WriteLine("Sorry, The bus doesn't exist in the system.\n");
                            break;
                        }
                        Console.WriteLine(@"choose one of the following:
  f: For refueling 
  t: For treatment
  ft:For both refueling an treatment:");
                        string c = Console.ReadLine();
                        do
                        {
                            switch (c)
                            {
                                case "f":
                                    helpBus.Refuel();
                                    break;
                                case "t":
                                    helpBus.DoTreatment();
                                    break;
                                case "ft":
                                    helpBus.RefuelAndTreat();
                                    break;
                                default:
                                    fl = false;
                                    Console.WriteLine("You can only press f, t or ft!");
                                    break;
                            }
                        } while (!fl);
                            break;
                    case "d":
                        if (buses==null)
                        {
                            Console.WriteLine("Sorry, there are no buses in the system.");
                        }
                        Console.WriteLine("The data of all buses in the system:");
                        foreach (Bus b in buses)
                        {
                            Console.WriteLine(b);
                        }
                        break;
                    case "e":
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }

            } while (s != "e");
        }

        static public int ReadSomething(int min, int max, string minMessage, string maxMessage)
        {
            string dateString;
            int dateInt= 0;
            bool flag = false;
            do
            {
                dateString = Console.ReadLine();
                try
                {
                    dateInt = int.Parse(dateString);

                    if (dateInt > max)
                    {
                        Console.WriteLine(maxMessage);
                    }
                    else
                    {
                        if (dateInt < min)
                        {
                            Console.WriteLine(minMessage);
                        }
                        else
                            flag = true;
                    }
                }
                catch
                {
                    Console.WriteLine("Enter only a number:");
                }
            } while (!flag);
            return dateInt;
        }
        static public int ReadYear()
        {
            string minM = "The first bus was developed in 1895! Please enter a proper year:";
            string maxM = "The current year is " + DateTime.Now.Year + "! Please enter a proper year:";
            return ReadSomething(1895, DateTime.Now.Year, minM, maxM);
        }
        static public int ReadMonth()
        {
            string minM = "The month number can not be under 1. Please enter a proper month:";
            string maxM = "The month number can not be over 12. Please enter a proper month:";
            return ReadSomething(1, 12, minM, maxM);
        }
        static public int ReadDay()
        {
            string minM = "The day number can not be under 1. Please enter a proper day:";
            string maxM = "The day number can not be over 31. Please enter a proper day:";
            return ReadSomething(1, 31, minM, maxM);
        }
        static public string ReadBusNum(DateTime startDate)
        {
            string busNum;
            bool flag = false;
            do
            {
                busNum = Console.ReadLine();
                try
                {
                    int bN = int.Parse(busNum);
                    int busNumLength = busNum.Length;
                    int yr = startDate.Year;              
                    if (((busNumLength==8) && (yr >= 2018)) || ((busNumLength==7)&& (yr < 2018)))
                        flag = true;
                    else
                        Console.WriteLine("Enter only a 7 or 8 digit number:\nIf start year is after (including) 2018 - 8 digits.\nIf start year is before 2018 - 7 digit.");
                }
                catch
                {
                    Console.WriteLine("Enter only a 7 or 8 digit number:\nIf start year is after (including) 2018 - 8 digits.\nIf start year is before 2018 - 7 digit.");
                }
            } while (!flag);
            return busNum;
        }


    }
}
