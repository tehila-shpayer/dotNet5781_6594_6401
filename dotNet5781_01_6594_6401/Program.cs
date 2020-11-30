using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6594_6401
{
    static public class BusCollection
    {
        static public int BUS_COUNTER = 0;
        static public List<Bus> buses = new List<Bus>();
        static BusCollection() { buses = new List<Bus>(); }
        static public void Add(Bus bus) { buses.Add(bus); BUS_COUNTER++; }
    }
    class Program
    //Tehila Shpayer 325236594. Sarah Malka Hamou 325266401.
    //The program is a system for representing buses.
    //It allows the user to add a bus, make a ride, refuel and treat a bus, or print the data on all the buses.
    {

        public static void RandomInitializationBus()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            Bus bus = new Bus();
            for (int i = 0; i < 10; i++)
            {
                String s;
                int year;
                if (i % 2 == 0)
                {
                    s = rnd.Next(1000000, 9999999).ToString();
                    year = rnd.Next(1895, 2018);
                }
                else
                {
                    s = rnd.Next(10000000, 99999999).ToString();
                    year = rnd.Next(2018, DateTime.Now.Year + 1);
                }
                int KM = rnd.Next(0, 10000);
                int bt = rnd.Next(0, Max(rnd.Next(0, 20000), KM));

                bus = new Bus(new DateTime(year, rnd.Next(1, 13), rnd.Next(1, 32)), s, rnd.Next(0, 1201), KM, bt);
                //bus.DoTreatment();
                BusCollection.Add(bus);
            }
        }
        public static int Max(int a, int b)
        {
            if (a < b)
                return a;
            return b;
        }
        static void Main(string[] args)
        {
            List<Bus> buses = new List<Bus>();
            RandomInitializationBus();
            Random rand = new Random(DateTime.Now.Millisecond);
            string s;
            Console.WriteLine("Welcome!\n");
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
                    case "a"://Add a new bus to the system
                        Console.WriteLine("Enter the start date of the bus activity:");
                        Console.WriteLine("Enter the year:");
                        int year = ReadYear();//get a proper year

                        Console.WriteLine("Enter the month:");
                        int month = ReadMonth();//get a proper month

                        Console.WriteLine("Enter the day:");
                        int day = ReadDay();//get a proper day

                        DateTime startDate = new DateTime(year, month, day);
                        Console.WriteLine("Enter the bus number:");
                        busNum = ReadBusNum(startDate);//get a proper license number

                        Bus newBus = new Bus(startDate, busNum);
                        buses.Add(newBus);
                        Console.WriteLine("You successfully added the bus to the system!\nDo you want it to start runnig?\nPress 1 to refuel and treatment, else press any key");
                        string one = Console.ReadLine();
                        if (one == "1")//if the user wants to refuel and treat the new bus 
                            newBus.RefuelAndTreat();
                        break;
                    case "b"://Make a ride
                        bool flag = false;
                        Console.WriteLine("Enter the bus license you wish to ride in: ");
                        busNum = Console.ReadLine();
                        foreach (Bus b in buses)//finds the requested bus in the system
                        { 
                            if (b.LicenseNumber == busNum)
                            {
                                Console.WriteLine(b.Ride((int)(rand.Next(1,1201))));
                                //commit a ride of a rundom number of KM (between 1 and 1200) if possible
                                flag = true;
                                break;
                            }
                        }
                        if(!flag)//if the bus is not in the system
                            Console.WriteLine("Sorry, The bus doesn't exist in the system.\n");
                        break;
                    case "c"://Refuels a bus or sends to treatment
                        bool fl = true;
                        Bus helpBus=new Bus(new DateTime());
                        Console.WriteLine("Enter the bus license you wish to refuel or treat:");
                        s = Console.ReadLine();
                        foreach (Bus b in buses)//finds the requested bus in the system
                        {
                            if (b.LicenseNumber == s)
                            {
                                helpBus = b;
                                break;
                            }
                        }
                        if (helpBus.LicenseNumber == "")//if the bus is not in the system
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
                                case "f"://Refuels the bus
                                    helpBus.Refuel();
                                    break;
                                case "t"://Sends the bus to treatment
                                    helpBus.DoTreatment();
                                    break;
                                case "ft"://Refuels and sends the bus to treatment
                                    helpBus.RefuelAndTreat();
                                    break;
                                default:
                                    fl = false;
                                    Console.WriteLine("You can only press f, t or ft!");
                                    break;
                            }
                        } while (!fl);
                            break;
                    case "d"://Prints the data of the buses in the system
                        if (buses==null)//if there are no buses in the system
                        {
                            Console.WriteLine("Sorry, there are no buses in the system.");
                            break;
                        }
                        Console.WriteLine("The data of all buses in the system:");
                        foreach (Bus b in buses)
                        {
                            Console.WriteLine(b);//Prints the data of the bus
                        }
                        break;
                    case "e"://exit
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }

            } while (s != "e");
        }
        static public int ReadSomething(int min, int max, string minMessage, string maxMessage)
        //gets a proper input - according to the minimum and maximum allowed values received, and prints an appropriate message 
        {
            string dateString;
            int dateInt= 0;
            bool flag = false;
            do //try to get the date while it doesn't match the allowed values
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
                catch//if the input is not a number
                {
                    Console.WriteLine("Enter only a number:");
                }
            } while (!flag);

            return dateInt;//return the date as a number
        }
        static public int ReadYear()//Gets a proper year - between 1895 and the current year
        {
            string minM = "The first bus was developed in 1895! Please enter a proper year:";
            string maxM = "The current year is " + DateTime.Now.Year + "! Please enter a proper year:";
            return ReadSomething(1895, DateTime.Now.Year, minM, maxM);
        }
        static public int ReadMonth()//Gets a proper month - a number between 1 and 12
        {
            string minM = "The month number can not be under 1. Please enter a proper month:";
            string maxM = "The month number can not be over 12. Please enter a proper month:";
            return ReadSomething(1, 12, minM, maxM);
        }
        static public int ReadDay()//Gets a proper day - a number between 1 and 31
        {
            string minM = "The day number can not be under 1. Please enter a proper day:";
            string maxM = "The day number can not be over 31. Please enter a proper day:";
            return ReadSomething(1, 31, minM, maxM);
        }
        static public string ReadBusNum(DateTime startDate)
        //Gets a proper input for the license number - a 7 or 8 digit number (according to the start year of the bus)
        {
            string busNumString;
            bool flag = false;
            do //try to get the license number while it doesn't match the allowed values
            {
                busNumString = Console.ReadLine();
                try
                {
                    int busNumInt = int.Parse(busNumString);
                    int busNumLength = busNumString.Length;
                    int startYear = startDate.Year;              
                    if (((busNumLength==8) && (startYear >= 2018)) || ((busNumLength==7)&& (startYear < 2018)))
                        flag = true;
                    else
                        Console.WriteLine("Enter only a 7 or 8 digit number:\nIf start year is after (including) 2018 - 8 digits.\nIf start year is before 2018 - 7 digit.");
                }
                catch//if the input is not a number
                {
                    Console.WriteLine("Enter only a 7 or 8 digit number:\nIf start year is after (including) 2018 - 8 digits.\nIf start year is before 2018 - 7 digit.");
                }
            } while (!flag);

            return busNumString;//return the license number as a string
        }
    }
}
