using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_6594_6401
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcom6594();
            welcome6401();
            Console.WriteLine("123456");
            Console.ReadKey();
        }

        static partial void welcome6401();

        private static void welcom6594()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0} , welcome to my first console application", name);
        }
    }
}
