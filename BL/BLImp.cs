using System;
using BLAPI;
using DLAPI;
using BO;
using System.Linq;
using System.Threading;

namespace BL
{
    class BLImp : IBL //internal
    {
        IDL dl = DLFactory.GetDL();
        #region Station

        BO.Station StationDoBoAdapter(DO.Station StationDO)
        {
            BO.Station StationBO = new BO.Station();

            StationDO.Clone(StationBO);

            StationBO.BusLines = dl.GetAllLinesInStation(StationBO.Key);
            return StationBO;
        }
        Station GetStation(int stationKey)
        {
            dl.AddStation(stationKey);
            if (station != null)
                return station.Clone();
            else
                throw new ArgumentNotFoundException<int>(station.Key, $"Station not found with key: {stationKey}");
        }
        IEnumerable<Station> GetAllStations()
        {
            var AllStations = from station in DataSource.ListStations
                              select station.Clone();
            return AllStations;
        }
        void AddStation(Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(s => s.Key == station.Key) != null)
                throw new InvalidInformationException<int>(station.Key, "Duplicate station key");
            DataSource.ListStations.Add(station.Clone());
        }
        void UpdateStation(Station station)
        {

        }
        void UpdateStation(int stationKey, Action<Station> update) { } //method that knows to updt specific fields in Station
        void DeleteStation(int stationKey) { }
        #endregion

        #region BusLineStation
        BO.BusLineStation BusLineStationDoBoAdapter(DO.BusLineStation BusLineStationDO)
        {
            BO.BusLineStation BusLineStationBO = new BO.BusLineStation();

            BusLineStationDO.Clone(BusLineStationBO);

            return BusLineStationBO;
        }
        BusLineStation GetBusLineStationByKey(int line, int stationKey)
        {
            BusLineStation busLineStation = DataSource.ListBusLineStations.Find(b => b.BusLineKey == line && b.StationKey == stationKey);
            if (busLineStation != null)
                return busLineStation.Clone();
            else
                throw new ArgumentNotFoundException<int>(line, $"Bus station of line {line} and station {stationKey} was not found.");
        }
        IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine)
        {
            var AllStationsOfLine = from station in DataSource.ListBusLineStations
                                    where station.BusLineKey == busLine
                                    select station.Clone();
            return AllStationsOfLine;
        }
        void AddBusLineStation(BusLineStation station)
        {
            if (DataSource.ListBusLineStations.FirstOrDefault(s => s.BusLineKey == station.BusLineKey && s.StationKey == station.StationKey) != null)
                throw new InvalidInformationException<int>(station.BusLineKey, "Duplicate station bus line number and station key");
            DataSource.ListBusLineStations.Add(station.Clone());
        }
        void UpdateBusLineStation(BusLineStation station)
        {
            BusLineStation busLineStation = DataSource.ListBusLineStations.Find(s => s.BusLineKey == station.BusLineKey && s.StationKey == station.StationKey);
            if (busLineStation != null)
                busLineStation = station;
            else
                throw new ArgumentNotFoundException<int>(station.BusLineKey, $"Bus station of line {station.BusLineKey} and station {station.StationKey} was not found.");
        }
        void UpdateBusLineStation(int line, int stationKey, Action<BusLineStation> update) //method that knows to updt specific fields in Person
        {
            BusLineStation busLineStation = DataSource.ListBusLineStations.Find(s => s.BusLineKey == line && s.StationKey == stationKey);
            if (busLineStation != null)
                update(busLineStation);
            else
                throw new ArgumentNotFoundException<int>(line, $"Bus station of line {line} and station {stationKey} was not found.");
        }
        void DeleteBusLineStation(int line, int stationKey)
        {
            BusLineStation busLineStation = DataSource.ListBusLineStations.Find(b => b.BusLineKey == line && b.StationKey == stationKey);
            if (busLineStation == null)
                throw new ArgumentNotFoundException<int>(line, $"Bus station of line {line} and station {stationKey} was not found.");
            DataSource.ListBusLineStations.Remove(busLineStation);
        }
        #endregion

        #region BusLine
        BO.BusLine BusLineDoBoAdapter(DO.BusLine BusLineDO)
        {
            BO.BusLine BusLineBO = new BO.BusLine();

            BusLineDO.Clone(BusLineBO);

            BusLineBO.BusLineStations = from blsDO in dl.GetAllStationsOfLine(BusLineBO.LineKey)
                                        select BusLineStationDoBoAdapter(blsDO);
            return BusLineBO;
        }
        BusLine GetBusLine(int busLineKey)
        {
            return BusLineDoBoAdapter(dl.GetBusLine(busLineKey))
        }
        IEnumerable<BusLine> GetBusLinesBy(Predicate<BusLine> predicate)
        {
            var AllBuseLinesBy = from line in DataSource.ListBusLines
                                 where predicate(line)
                                 select line.Clone();
            return AllBuseLinesBy;
        }
        IEnumerable<BusLine> GetAllBusLines()
        {
            var AllBuseLines = from line in DataSource.ListBusLines
                               select line.Clone();
            return AllBuseLines;
        }
        void AddBusLine(BusLine bus)
        {
            if (DataSource.ListBusLines.FirstOrDefault(l => l.Key == bus.Key) != null)
                throw new InvalidInformationException<int>(bus.Key, "Duplicate bus line key");
            DataSource.ListBusLines.Add(bus.Clone());
        }
        void UpdateBusLine(BusLine line)
        {
            BusLine bus = DataSource.ListBusLines.Find(b => b.Key == line.Key);
            if (bus != null)
                bus = line;
            else
                throw new ArgumentNotFoundException<int>(line.Key, $"Bus not found with license number: {line.Key}");
        }
        void UpdateBusLine(int busLineKey, Action<BusLine> update)//method that knows to updt specific fields in BusLine
        {
            BusLine bus = DataSource.ListBusLines.Find(b => b.Key == busLineKey);
            if (bus != null)
                update(bus);
            else
                throw new ArgumentNotFoundException<int>(busLineKey, $"Bus not found with license number: {busLineKey}");
        }
        void DeleteBusLine(int busLineKey)
        {
            BusLine bus = DataSource.ListBusLines.Find(b => b.Key == busLineKey);
            if (bus == null)
                throw new ArgumentNotFoundException<int>(busLineKey, $"Bus not found with license number: {busLineKey}");
            DataSource.ListBusLines.Remove(bus);
        }
        #endregion

    }
}
