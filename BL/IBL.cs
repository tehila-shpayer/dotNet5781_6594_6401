using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLAPI
{
    public interface IBL
    {
        #region BusLine
        BO.BusLine BusLineDoBoAdapter(DO.BusLine BusLineDO);
        BusLine GetBusLine(int busLineKey);
        IEnumerable<BusLine> GetBusLinesBy(Predicate<BusLine> predicate);
        IEnumerable<BusLine> GetAllBusLines();
        void AddBusLine(BusLine busline);
        void AddStationToLine(int busLineKey, Station station, int position = 0);
        void DeleteBusLine(int busLineKey);
        void DeleteStationFromLine(int busKey, int stationKey);
        void UpdateBusLine(BusLine busline);
        void UpdateBusLine(int busLineKey, Action<BusLine> update);
        #endregion

        #region BusLineStation
        BO.BusLineStation BusLineStationDoBoAdapter(DO.BusLineStation BusLineStationDO);
        BusLineStation GetBusLineStationByKey(int line, int stationKey);
        IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine);
        void AddBusLineStation(BusLineStation bus);
        void UpdateBusLineStation(BusLineStation bus);
        void UpdateBusLineStation(int line, int stationKey, Action<BusLineStation> update); //method that knows to updt specific fields in Person
        void DeleteBusLineStation(int line, int stationKey);
        #endregion

        #region Station
        BO.Station StationDoBoAdapter(DO.Station StationDO);
        Station GetStation(int stationKey);
        IEnumerable<Station> GetAllStations();
        void AddStation(Station station);
        void UpdateStation(Station station);
        void UpdateStation(int stationKey, Action<BusLineStation> update); //method that knows to updt specific fields in Person
        void DeleteStation(int stationKey);
        #endregion

    }
}
