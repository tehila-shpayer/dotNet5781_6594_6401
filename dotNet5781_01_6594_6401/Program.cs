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
            List<Buss> busses;
            // char c = 'e';
            string s;
            Console.WriteLine("Welcome!");
            do
            {

                Console.WriteLine(@"
   Choose one of the following:
     a: Add a new buss
     b: Chose a buss for a ride
     c: Fuel a buss
     d: Print the data            
     e: exit:");

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
                       
                        //Console.WriteLine("Enter the month:");
                        //string month = Console.ReadLine();
                        //Console.WriteLine("Enter the day:");
                        //string day = Console.ReadLine();
                        //DateTime startDate = new DateTime(int.Parse(year), month, day);

                        break;
                    case "b":
                        break;
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


        static int ReadYear()
        {
            string yearString;          
            int yearInt = 0;
            bool flag = false;
            do
            {
                yearString = Console.ReadLine();
                try
                {
                    yearInt = int.Parse(yearString);
                    if (yearInt < 1895)
                    {
                        Console.WriteLine("The first bus was developed in 1895.");
                        Console.WriteLine("Enter a proper year:");
                    }
                    else
                    {
                        if (yearInt > DateTime.Now.Year)
                        {
                            Console.WriteLine("The current year is {0}", DateTime.Now.Year + ".");
                            Console.WriteLine("Enter a proper year:");
                        }
                        else
                            flag = true;
                    }
                }
                catch 
                {
                    Console.WriteLine("Enter a number:");
                }
            } while (!flag) ;
            return yearInt;
        }

    }
}
