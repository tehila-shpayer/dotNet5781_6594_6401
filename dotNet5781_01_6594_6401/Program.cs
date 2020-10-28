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
            List<Buss> busses;
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
                        break;
                }

            } while (s != "e");
        }

    }
}
