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
           BusLinePO.BusLineStations = from bls in BusLineBO.BusLineStations
                                       select PoBoAdapter.BusLineStationDoBoAdapter(bls);
            return BusLinePO;
        }
        static public PL.BusLineStation BusLineStationDoBoAdapter(BO.BusLineStation BusLineStationBO)
        {
            PL.BusLineStation BusLineStationPO = new PL.BusLineStation();
            BusLineStationBO.Clone(BusLineStationPO);
            BusLineStationPO.Name = App.bl.GetStation(BusLineStationBO.StationKey).Name;
            return BusLineStationPO;
        }
        static public PL.Station StationDoBoAdapter(BO.Station StationBO)
        {
            PL.Station StationPO = new PL.Station();
            StationBO.Clone(StationPO);
            //StationPO.BusLines = from line in StationBO.BusLines
            //                     select line;
            return StationPO;
        }
    }
}
