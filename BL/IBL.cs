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
        BusLine GetBusLine(int busLineKey);
        IEnumerable<BusLine> GetBusLinesBy(Predicate<BusLine> predicate);
        IEnumerable<BusLine> GetAllBusLines();
        void AddBusLine(BusLine busline);
        void AddStationToLine(BusLineStation sKey);
        void AddStationToLine(int sKey, int position);
        void DeleteBusLine(BusLine busLine);
        void DeleteBusLine(int busLineKey);
        void DeleteStationFromLine(BusLineStation bls);
        void DeleteStationFromLine(int key);
        void UpdateBusLine(BusLine busline);
        void UpdateStation(int LineKey, Action<BusLine> update); 
        #endregion

        #region BusLineStation
        BusLineStation GetBusLineStationByKey(int line, int stationKey);
        IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine);
        void AddBusLineStation(BusLineStation bus);
        void UpdateBusLineStation(BusLineStation bus);
        void UpdateBusLineStation(int line, int stationKey, Action<BusLineStation> update); //method that knows to updt specific fields in Person
        void DeleteBusLineStation(int line, int stationKey);
        #endregion

        #region Station
        Station GetStation(int stationKey);
        IEnumerable<Station> GetAllStations();
        void AddStation(Station station);
        void UpdateStation(Station station);
        void UpdateStation(int stationKey, Action<BusLineStation> update); //method that knows to updt specific fields in Person
        void DeleteStation(int stationKey);
        #endregion

    }
}
