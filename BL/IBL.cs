﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLAPI
{
    public interface IBL
    {
        //void StartSimulator(TimeSpan startTime, int simulatorRate, Action<TimeSpan> updateTime);
        void StartSimulator(Clock simulatorClock, TimeSpan startTime, int simulatorRate, int Key);
        void StopSimulator();

        //List<BusInTravel> GetLineTimingsPerStation(int stationKey, TimeSpan startTime);

        #region Bus
        IEnumerable<Bus> GetAllBusesOrderedBy(string orderBy);
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        Bus GetBus(string LicenseNumber);
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void UpdateBus(string LicenseNumber, Action<Bus> update); //method that knows to updt specific fields in Person
        void DeleteBus(string LicenseNumber);
        #endregion

        #region User
        User GetUser(string userName);
        User GetUser(string userName, string password);
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void UpdateUser(string userName, Action<User> update);
        void DeleteUser(string userName);
        #endregion

        #region BusLine
        BO.BusLine BusLineDoBoAdapter(DO.BusLine BusLineDO);
        BusLine GetBusLine(int busLineKey);
        IEnumerable<BusLine> GetBusLinesBy(Predicate<BusLine> predicate);
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetAllBusLinesOrderedBy(string orderBy);
        Station GetPreviouseStation(int lineStationKey, int position);
        BusLineStation GetBusLineStation(int busKey, int Position);
        int AddBusLine(BusLine busline);
        int AddBusLine(BusLine bus, int stationKey1, int stationKey2);
        void AddStationToLine(int busLineKey, int stationKey, int position = 0);
        void DeleteBusLine(int busLineKey);
        void DeleteStationFromLine(int busKey, int stationKey);
        void UpdateBusLine(BusLine busline);
        void UpdateBusLine(int busLineKey, Action<BusLine> update);
        String ToStringBusLine(BusLine b);
        #endregion

        #region BusLineStation
        BO.BusLineStation BusLineStationDoBoAdapter(DO.BusLineStation BusLineStationDO);
        BusLineStation GetBusLineStationByKey(int line, int stationKey);
        IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine);
        void AddBusLineStation(BusLineStation bus);
        void UpdateBusLineStation(BusLineStation bus);
        void UpdateBusLineStation(int line, int stationKey, Action<BusLineStation> update); //method that knows to updt specific fields in Person
        void DeleteBusLineStation(int line, int stationKey);
        string ToStringBusLineStation(BusLineStation bls);
        #endregion

        #region Station
        BO.Station StationDoBoAdapter(DO.Station StationDO);
        IEnumerable<Station> GetAllStationsOrderedBy(string orderBy);
        Station GetStation(int stationKey);
        IEnumerable<Station> GetAllStations();
        int AddStation(Station station);
        void UpdateStation(Station station);
        void UpdateStation(int stationKey, Action<Station> update); //method that knows to updt specific fields in Person
        void DeleteStation(int stationKey);
        string ToStringStation(Station station);
        #endregion

        #region LineSchedule
        BO.LineSchedule LineScheduleDoBoAdapter(DO.LineSchedule LineScheduleDO);
        LineSchedule GetLineSchedule(int lineKey, TimeSpan startTime);
        IEnumerable<LineSchedule> GetAllLineSchedules();
        IEnumerable<LineSchedule> GetAllLineSchedulesOfLine(int Line);
        void AddLineSchedule(LineSchedule lineSchedule);
        void UpdateLineSchedule(LineSchedule lineSchedule);
        void UpdateLineSchedule(int lineKey, TimeSpan startTime, Action<LineSchedule> update);
        void DeleteLineSchedule(int lineKey, TimeSpan startTime);
        #endregion

        #region BusInTravel
        IEnumerable<BusInTravel> GetLineTimingsPerStation(int stationKey, TimeSpan startTime, double latePrecentage);
        //BusInTravel GetBusInTravel(int key);
        //IEnumerable<BusInTravel> GetAllBusesInTravel(Station s, TimeSpan t);
        //IEnumerable<BusInTravel> GetAllBusInTravelsBy(Predicate<BusInTravel> predicate);
        //BusInTravel CreateBusInTravel(LineSchedule lineSchedule, Station station, string licenseNumber = "00000000");
        //void DeleteBusInTravel(string licenseNumber, int lineKey, int formalTime);
        //void UpdateBusInTravel(BusInTravel bus);
        //void UpdateBusInTravel(int key, Action<BusInTravel> update); //method that knows to updt specific fields in BusInTravel
        #endregion

    }
}
