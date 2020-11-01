using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6594_6401
{
    class Program
    {
        static void Main(string[] args)
        {
            //חסר:
            //לטפל בחריגות בכול הפונקציות - לבדוק שהפרמטרים שמקבלים קבילים
            //שיניתי את תחנה ראשונה ואחרונה להיות תחנת אוטובוס כי הייתי חייבת וזה לא משנה ככ
            //זהו נראה לי:)
            BusStation bs = new BusStation(158745, 31.234567, 34.56789, "רח' פלוני אלמוני 12, תל חורף");
            BusStation bs1 = new BusStation(121212, 31.234567, 34.56789, "רח' פלוני אלמוני 12, תל חורף");
            BusStation bs2 = new BusStation(546897, 31.234567, 34.56789, "רח' פלוני אלמוני 12, תל חורף");
            Console.WriteLine(bs);
            BusLineStation bss = new BusLineStation(bs,18,54);
            BusLineStation bss1 = new BusLineStation(bs2,19,55);
            BusLineStation bss2 = new BusLineStation(bs1,59,100);
            Areas a = new Areas();
            a = Areas.Center;
            BusLine busLine = new BusLine(143, a);
            busLine.AddStation(0,bss);
            busLine.AddStation(0,bss1);
            busLine.AddStation(0,bss2);
            Console.WriteLine(busLine);
            Console.WriteLine(busLine.FindDistance(bss2, bss));





        }
    }
}
