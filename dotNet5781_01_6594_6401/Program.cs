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
            //DateTime d = new DateTime(2013, 5, 29);
            //DateTime d2 = new DateTime(2019, 12, 5);

            //Buss b = new Buss("12345678", d);
            //Buss b2 = new Buss("12345678",d2 );
            //why won't you work?????????????????
            List<Buss> busses = new List<Buss>();
            // char c = 'e';
            string s;
            Console.WriteLine("Welcome!");
            do
            {

                Console.WriteLine(@"    Choose one of the following:
            a: Add a new buss
            b: Chose a buss for a ride
            c: Fuel a buss
            d: Print the data of a buss     
            e: exit:");

                s = Console.ReadLine();

                switch (s)
                {
                    case "a":
                        string bussNum;
                        bool flag = false;
                        Console.WriteLine("Enter the buss number:");
                        do
                        {
                            bussNum = Console.ReadLine();
                            try
                            {
                                int bN = int.Parse(bussNum);
                                flag = true;
                            }
                            catch
                            {
                                Console.WriteLine("Enter only a number:");
                            }
                        } while (!flag);

                        Console.WriteLine("Enter the start date of the buss activity:");
                        Console.WriteLine("Enter the year:");
                        int year = ReadYear();

                        Console.WriteLine("Enter the month:");
                        int month = ReadMonth();
                      
                        Console.WriteLine("Enter the day:");
                        int day = ReadDay();
                        DateTime startDate = new DateTime(year, month, day);
                        busses.Add(new Buss(bussNum, startDate));
                        break;
                    case "b":
                        Console.WriteLine("Enter the buss number you wish to ride in: ");
                        s=Console.ReadLine();
                        for (int i = 0; i < busses.Count; i++)
                        {
                            Buss b = busses[i];
                            break;
                        }

                    case "c":
                        break;
                    case "d":
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
            string minM = "The first bus was developed in 1895. Please enter a proper year:";
            string maxM = "The current year is " + DateTime.Now.Year + ". Please enter a proper year:";
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

    }
}
