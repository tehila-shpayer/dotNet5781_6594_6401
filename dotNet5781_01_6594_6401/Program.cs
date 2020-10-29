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
//להוסיף לפונקציה רייד שהיא לא יכולה לעבוד גם כשעברה שנה מהטיפול האחרון
//לשנות כול פעם שכתוב Buss ל bus - בוצע!
//car number -> license number - בוצע!
//לסדר בפונקציות את התוכנית הראשית
//להוריד מהצגה של תאריכים את השעה - בוצע!
            List<Bus> buses = new List<Bus>();
            Random r = new Random(DateTime.Now.Millisecond);
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
                        busNum=readBusNum(startDate);
                        buses.Add(new Bus(startDate, busNum));
                        break;
                    case "b":
                        
                        bool flag = false;
                        Console.WriteLine("Enter the bus license you wish to ride in: ");
                        busNum = Console.ReadLine();
                        foreach (Bus b in buses)
                        { 
                            if (b.licenseNumber == busNum)
                            {
                                if (!b.Ride((int)r.Next(1200)))
                                    Console.WriteLine("The system couldn't take this bus for the ride.\nplease make sure you have enough fuel and the bus is after treatment.\n");
                                else
                                    Console.WriteLine("Have a nice ride!\n");
                                flag = true;
                                break;
                            }
                        }
                        if(!flag)
                            Console.WriteLine("Sorry, The bus doesn't exist in the system.\n");
                        break;
                    case "c":
                        bool fl = true;
                        Bus bb=new Bus(new DateTime());
                        Console.WriteLine("Enter the bus license you wish to refuel or treat: ");
                        s = Console.ReadLine();
                        foreach (Bus b in buses)
                        {
                            if (b.licenseNumber == s)
                            {
                                bb = b;
                                break;
                            }

                        }
                        if (bb.licenseNumber == "")
                        {
                            Console.WriteLine("Sorry, The bus doesn't exist in the system.\n");
                            break;
                        }
                        Console.WriteLine("For refueling press f and for tratment press t please.");
                        string c = Console.ReadLine();
                        do
                        {
                            switch (c)
                            {
                                case "f":
                                    bb.RefillFuel();
                                    Console.WriteLine("The fuel tank was successfully refueled!\n");
                                    break;
                                case "t":
                                    bb.DoTreatment();
                                    Console.WriteLine("The bus was successfully treated!\n");
                                    break;
                                default:
                                    fl = false;
                                    break;
                            }
                        } while (!fl);
                            break;
                    case "d":
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
            string minM = "The month number can not be less than 1. Please enter a proper month:";
            string maxM = "The month number can not be more than 12. Please enter a proper month:";
            return ReadSomething(1, 12, minM, maxM);
        }
        static public int ReadDay()
        {
            string minM = "The day number can not be less than 1. Please enter a proper day:";
            string maxM = "The day number can not be more than 31. Please enter a proper day:";
            return ReadSomething(1, 31, minM, maxM);
        }
        static public string readBusNum(DateTime startDate)
        {
            string busNum;
            bool flag = false;
            do
            {
                busNum = Console.ReadLine();
                try
                {
                    int bN = int.Parse(busNum);
                    int sevd = 10000000;
                    int sixd = sevd / 10;
                    int eighd = sevd * 10;
                    int yr = startDate.Year;
                    if (((bN / sevd != 0) && (bN / eighd == 0) && (yr >= 2018)) || ((bN / sixd != 0) && (bN / sevd == 0) && (yr < 2018)))
                        flag = true;
                    else
                        Console.WriteLine("Enter only a 7 or 8 digit number:\nIf start year is after 2018 - 8 digits.\nIf start year is before 2018 - 7 digit.");
                }
                catch
                {
                    Console.WriteLine("Enter only a 7 or 8 digit number:\nIf sart year is after 2018 - 8 digits.\nIf start year is before 2018 - 7 digit.");
                }
            } while (!flag);
            return busNum;
        }

    }
}
