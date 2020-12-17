using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlAPI;
using BO;

namespace PLConsole
{
    class Program
    {
        static IBL bl;
        static void Main(string[] args)
        {
            bl = BlFactory.GetBl(1);
            bl.Shutdown();
        }
    }
}
