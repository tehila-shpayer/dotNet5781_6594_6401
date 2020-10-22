using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_6401_6594
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcom6401();
            welcome6594();
            Console.ReadKey();
        }

        static partial void welcome6594();

        private static void welcom6401()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0} , welcome to my first console application", name);
        }
    }
}
