using System;
using System.Collections.Generic;
using System.Linq;
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
                Console.WriteLine(@"    Choose one of the following:
     a: Add a new buss
     b: Chose a buss for a ride
     c: Fuel a buss
     d: Print the data of a buss     
     e: exit:");
                s = Console.ReadLine();
                
                switch(s)
                {
                    case "a":
                        break;
                }

            } while (s != "e");
        }
    }
}
