using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BLAPI;

namespace PL
{
    public static class PoBoAdapter
    {
        static public  PL.BusLine BusLineDoBoAdapter(BO.BusLine BusLineBO)
        {
           PL.BusLine BusLinePO = new PL.BusLine();
           BusLineBO.Clone(BusLinePO);
            return BusLinePO;
        }
    }
}
