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
        static public PL.Bus BusPoBoAdapter(BO.Bus BusBO)
        {
            PL.Bus BusPO = new PL.Bus();
            BusBO.Clone(BusPO);
            
            String s = BusPO.LicenseNumber;
            if (s.Length == 8)
            {
                BusPO.LicenseNumberFormat = $"{s[0]}{s[1]}{s[2]}-{s[3]}{s[4]}-{s[5]}{s[6]}{s[7]}";
            }
            else
            {
                BusPO.LicenseNumberFormat = $"{s[0]}{s[1]}-{s[2]}{s[3]}{s[4]}-{s[5]}{s[6]}";
            }
            BusPO.RunningDateFormat = BusBO.RunningDate.ToString("dd/MM/yyyy");
            BusPO.LastTreatmentFormat = BusBO.LastTreatment.ToString("dd/MM/yyyy");
            BusPO.Time = "";
            //BusPO.activity = new BackgroundWorker();
            //BusPO.activity.DoWork += Activity_DoWork;
            //BusPO.activity.ProgressChanged += Activity_ProgressChanged;
            //BusPO.activity.RunWorkerCompleted += Activity_RunWorkerCompleted;
            //BusPO.activity.WorkerReportsProgress = true;
            //BusPO.timer = new BackgroundWorker();
            //BusPO.timer.DoWork += Timer_DoWork;
            //BusPO.timer.ProgressChanged += Timer_ProgressChanged;
            //BusPO.timer.RunWorkerCompleted += Timer_RunWorkerCompleted;
            //BusPO.timer.WorkerReportsProgress = true;
            return BusPO;
        }
        static public PL.BusLine BusLinePoBoAdapter(BO.BusLine BusLineBO, int SRCKey = 0, int DSTKey = 0)
        {
            PL.BusLine BusLinePO = new PL.BusLine();
            BusLineBO.Clone(BusLinePO);
            BusLinePO.BusLineStations = from bls in BusLineBO.BusLineStations
                                        select PoBoAdapter.BusLineStationPoBoAdapter(bls, SRCKey, DSTKey);
            return BusLinePO;
        }
        static public PL.PresentBusLineForStation PresentBusLineForStationPoBoAdapter(BO.BusLine BusLineBO)
        {
            PL.PresentBusLineForStation PresentBL = new PL.PresentBusLineForStation();
            BusLineBO.Clone(PresentBL);
            PresentBL.NameFirstStation = App.bl.GetStation(BusLineBO.FirstStation).Name;
            PresentBL.NameLastStation = App.bl.GetStation(BusLineBO.LastStation).Name;
            return PresentBL;
        }
        static public PL.BusLineStation BusLineStationPoBoAdapter(BO.BusLineStation BusLineStationBO, int SRCKey, int DSTKey)
        {
            PL.BusLineStation BusLineStationPO = new PL.BusLineStation();
            BusLineStationBO.Clone(BusLineStationPO);
            BusLineStationPO.Name = App.bl.GetStation(BusLineStationBO.StationKey).Name;
            if (BusLineStationPO.Position == 1)
                BusLineStationPO.IsFirstStation = true;
            else
                BusLineStationPO.IsFirstStation = false;
           
            BusLineStationPO.IsSource = (BusLineStationPO.StationKey == SRCKey);
            BusLineStationPO.IsDestination = (BusLineStationPO.StationKey == DSTKey);
            return BusLineStationPO;
        }
        static public PL.Station StationPoBoAdapter(BO.Station StationBO)
        {
            PL.Station StationPO = new PL.Station();
            StationBO.Clone(StationPO);
            StationPO.PresentBusLines = from line in StationBO.BusLines
                                 select PoBoAdapter.PresentBusLineForStationPoBoAdapter(App.bl.GetBusLine(line));
            return StationPO;
        }
        static public PL.User UserPoBoAdapter(BO.User UserBO)
        {
            PL.User UserPO = new PL.User();
            UserBO.Clone(UserPO);
            return UserPO;
        }
        static public LineSchedule LineSchedulePoBoAdapter(BO.LineSchedule LineScheduleBO)
        {
            LineSchedule LineSchedulePO = new LineSchedule();
            LineScheduleBO.Clone(LineSchedulePO);
            LineSchedulePO.LineNumber = App.bl.GetBusLine(LineSchedulePO.LineKey).LineNumber;
            return LineSchedulePO;
        }
        static public PL.BusInTravel BusInTravelPoBoAdapter(BO.BusInTravel BusInTravelBO)
        {
            PL.BusInTravel BusInTravelPO = new PL.BusInTravel();
            BusInTravelBO.Clone(BusInTravelPO);
            var busLine = App.bl.GetBusLine(BusInTravelPO.LineKey);
            BusInTravelPO.LineNumber = busLine.LineNumber;
            BusInTravelPO.LastStationName = App.bl.GetStation(busLine.LastStation).Name;
            return BusInTravelPO;
        }
    }
}
