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
        static public  PL.BusLine BusLinePoBoAdapter(BO.BusLine BusLineBO)
        {
           PL.BusLine BusLinePO = new PL.BusLine();
           BusLineBO.Clone(BusLinePO);
            BusLinePO.BusLineStations = from bls in BusLineBO.BusLineStations
                                        select PoBoAdapter.BusLineStationPoBoAdapter(bls);
            return BusLinePO;
        }
        static public PL.BusLineStation BusLineStationPoBoAdapter(BO.BusLineStation BusLineStationBO)
        {
            PL.BusLineStation BusLineStationPO = new PL.BusLineStation();
            BusLineStationBO.Clone(BusLineStationPO);
            BusLineStationPO.Name = App.bl.GetStation(BusLineStationBO.StationKey).Name;
            return BusLineStationPO;
        }
        static public PL.Station StationPoBoAdapter(BO.Station StationBO)
        {
            PL.Station StationPO = new PL.Station();
            StationBO.Clone(StationPO);
            int l = 1791;
            BO.BusLine BusLineBO = App.bl.GetBusLine(l);
            string source = App.bl.GetStation(BusLineBO.FirstStation).Name;
            string destination = App.bl.GetStation(BusLineBO.LastStation).Name;
            int number = BusLineBO.LineNumber;
            string show = $"{number}#{source} -> {destination}";
            StationPO.BusLines = from line in StationBO.BusLines
                                 select line;
            StationPO.BusLinesAllData = from line in StationBO.BusLines
                                 select PoBoAdapter.BusLinePoBoAdapter(App.bl.GetBusLine(line));
            return StationPO;
        }
    }
}
