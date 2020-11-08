﻿using System;
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
            BusStation bs = new BusStation(158745, 31.234567, 34.56789, "");
            BusStation bs1 = new BusStation(121212, 31.234567, 34.56789, "רח' פלוני אלמוני 12, תל חורף");
            BusStation bs2 = new BusStation(546897, 31.234567, 34.56789, "רח' פלוני אלמוני 12, תל חורף");
            StationList.Add(bs);
            StationList.Add(bs1);
            StationList.Add(bs2);
            BusLineStation bss = new BusLineStation(158745, 10,5);
            BusLineStation bss1 = new BusLineStation(121212, 20,10);
            BusLineStation bss2 = new BusLineStation(546897, 30,15);
            //BusLineStation bss3 = new BusLineStation(123456, 40, 20);
            //BusLineStation bss4 = new BusLineStation(728, 50, 25);

            Console.WriteLine(bss);
            Console.WriteLine(bss1);
            Console.WriteLine(bss2);
            BusLine busLine = new BusLine(143, Areas.Center);
            busLine.AddStation(0,bss);
            busLine.AddStation(1,bss1);
            busLine.AddStation(2,bss2);
            Console.WriteLine(busLine);
            Console.WriteLine(busLine.FindDistance(bss, bss2));
            Console.WriteLine(busLine.FirstStation);
            Console.WriteLine(busLine.LastStation);
            Console.WriteLine(busLine.FindTime(bss1,bss2));
            BusLineCollection BLC = new BusLineCollection();
            BLC.Add(busLine);
            //יש שגיאת ריצה מכאןת צריך לבדוק מה הבעיה במחלקת אוסף
            Console.WriteLine(BLC[143]);
            Console.WriteLine(BLC.BusLineInStationList(bs.BusStationKey));
            BLC.Delete(busLine);
            Console.WriteLine(BLC.BusLineInStationList(bs.BusStationKey));






            //StationList.Add(bs);






        }
    }
}
