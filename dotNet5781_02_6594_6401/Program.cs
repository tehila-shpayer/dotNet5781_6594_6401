using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

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

            Random rand = new Random(DateTime.Now.Millisecond);

            BusStation bs;

            for (int i = 0; i < 40; i++)
            {
                double la, lo;
             
                la = (rand.Next(31, 34)) + (rand.NextDouble());
                if (la > 33.3)
                    la--;
                lo = (rand.Next(34, 36)) + (rand.NextDouble());
                if (lo < 34.3)
                    lo++;
                if (lo > 35.5)
                    lo--;
                
                bs = new BusStation(la, lo, "");
                StationList.Add(bs);
            }
            bs = new BusStation(31.234567, 34.56874, "Kohav HaShahar");
            StationList.Add(bs);
           // Console.WriteLine(StationList.ToString());

            
            BusLine bl;
            BusLineCollection lineCollection = new BusLineCollection();
            for (int i = 0; i < 10; i++)
            {
                int Intarea = (rand.Next(0, 7));

                bl = new BusLine((Areas)Intarea);
                lineCollection.Add(bl);
            }
            Console.WriteLine(lineCollection);
            

            //BusLineStation bss1 = new BusLineStation(2, 20,10);
            //BusLineStation bss2 = new BusLineStation(3, 30,15);
            ////BusLineStation bss3 = new BusLineStation(123456, 40, 20);
            ////BusLineStation bss4 = new BusLineStation(728, 50, 25);

            //Console.WriteLine(bss);
            //Console.WriteLine(bss1);
            //Console.WriteLine(bss2);
            //BusLine busLine = new BusLine(143, Areas.Center);
            //busLine.AddStation(0,bss);
            //busLine.AddStation(1,bss1);
            //busLine.AddStation(2,bss2);
            //Console.WriteLine(busLine);
            //Console.WriteLine(busLine.FindDistance(bss, bss2));
            //Console.WriteLine(busLine.FirstStation);
            //Console.WriteLine(busLine.LastStation);
            //Console.WriteLine(busLine.FindTime(bss1,bss2));
            //BusLineCollection BLC = new BusLineCollection();
            //BLC.Add(busLine);
            //Console.WriteLine(BLC[143]);
            //List<BusLine> bl=BLC.BusLineInStationList(bs.BusStationKey);
            //Console.WriteLine(bl[0]);
            //BLC.Delete(busLine);
            //Console.WriteLine(BLC.BusLineInStationList(bs.BusStationKey));
        }
    }
}
