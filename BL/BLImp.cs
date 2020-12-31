using System;
using BLAPI;
using DLAPI;
using BO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
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
            var AllStations = from station in dl.GetAllStations()
                              select StationDoBoAdapter(station);
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
        void DeleteStation(int stationKey)
        {
            try
            {
                dl.DeleteStation(stationKey);
                dl.DeleteBusLineStationsByStation(stationKey);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region BusLineStation
        BO.BusLineStation BusLineStationDoBoAdapter(DO.BusLineStation BusLineStationDO)
        {
            BO.BusLineStation BusLineStationBO = new BO.BusLineStation();

            BusLineStationDO.Clone(BusLineStationBO);
            DO.BusLineStation SecondBusLineStationDO = dl.GetBusLineStationBy(s => s.Position == BusLineStationDO.Position-1 && s.BusLineKey == BusLineStationDO.BusLineKey);
            if (BusLineStationDO.Position == 1)
            {
                BusLineStationBO.DistanceFromLastStationMeters = 0;
                BusLineStationBO.TravelTimeFromLastStationMinutes = 0;
                return BusLineStationBO;
            }
            BusLineStationBO.DistanceFromLastStationMeters = dl.GetConsecutiveStations(BusLineStationDO.StationKey, SecondBusLineStationDO.StationKey).Distance;
            BusLineStationBO.TravelTimeFromLastStationMinutes = dl.GetConsecutiveStations(BusLineStationDO.StationKey, SecondBusLineStationDO.StationKey).AverageTime;
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
            try
            {
                return BusLineDoBoAdapter(dl.GetBusLine(busLineKey));
            }
            catch(DO.ArgumentNotFoundException<BusLine> ex)
            {

            }
        }
        IEnumerable<BusLine> GetBusLinesBy(Predicate<BusLine> predicate)
        {
            try
            {
                return from bl in dl.GetAllBusLines()
                       let BusLineBO = BusLineDoBoAdapter(bl)
                       where predicate(BusLineBO)
                       select BusLineBO;
            }
            catch (DO.ArgumentNotFoundException<BusLine> ex) { }
        }
        IEnumerable<BusLine> GetAllBusLines()
        {
            var AllBuseLines = from line in DataSource.ListBusLines
                               select line.Clone();
            return AllBuseLines;
        }
        void AddBusLine(BusLine bus)
        {
            try
            {
                DO.BusLine BusLineDO = new DO.BusLine();
                bus.Clone(BusLineDO);
                dl.AddBusLine(BusLineDO);
            }
            catch (DO.InvalidInformationException<BusLine> ex) { }
        }
        void AddStationToLine(int busLineKey, Station station, int position = 0)
        {
            BusLine busLine = GetBusLine(busLineKey);
            if (position == 0)
                position = busLine.BusLineStations.Count();
            //if(dl.GetStation(station.Key) == null)
            //    throw
            //if (position > bus.BusLineStations.Count() || position < 0)//מיקום רלוונטי - כגודל רשימת התחנות
            //    throw 
            DO.BusLineStation busLineStationDO = new DO.BusLineStation();
            busLineStationDO.BusLineKey = busLineKey;
            busLineStationDO.StationKey = station.Key;
            busLineStationDO.Position = position;
            dl.AddBusLineStation(busLineStationDO);
            BO.BusLineStation busLineStationBO = new BO.BusLineStation();
            busLineStationBO = BusLineStationDoBoAdapter(busLineStationDO);
            busLine.BusLineStations.Append(busLineStationBO);
            if (position != busLine.BusLineStations.Count())
            {//אם זו לא התחנה האחרונה יש לעדכן את פרטי הזמן והמרחק של התחנה הבאה 
                foreach (BusLineStation s in busLine.BusLineStations)
                {
                    if (s.Position == position + 1)
                    {
                        UpdateBusLineStation(s.BusLineKey, s.StationKey, bls => bls = BusLineStationDoBoAdapter(busLineStationDO));
                    }
                }
            }
        }
        void UpdateBusLine(BusLine bus)
        {
            try
            {
                DO.BusLine BusLineDO = new DO.BusLine();
                bus.Clone(BusLineDO);
                dl.UpdateBusLine(BusLineDO);
            }
            catch (DO.ArgumentNotFoundException<BusLine> ex) { }
        }
        void UpdateBusLine(int busLineKey, Action<BusLine> update)
        {
            try
            {
                DO.BusLine BusLineDO = new DO.BusLine();
                BusLine BusLineBO = GetBusLine(busLineKey);
                update(BusLineBO);
                BusLineBO.Clone(BusLineDO);
                dl.UpdateBusLine(BusLineDO);
            }
            catch (DO.ArgumentNotFoundException<BusLine> ex) { }
        }
        void DeleteBusLine(int busLineKey)
        {
            try
            {
                dl.DeleteBusLine(busLineKey);
            }
            catch (DO.ArgumentNotFoundException<BusLine> ex) { }
        }
        void DeleteStationFromLine(int busKey, int stationKey)        {
            BusLine busLine = GetBusLine(busKey);
            DO.BusLineStation busLineStationDO = new DO.BusLineStation();
            busLineStationDO.BusLineKey = busLineKey;
            busLineStationDO.StationKey = station.Key;
            busLineStationDO.Position = position;
            dl.AddBusLineStation(busLineStationDO);
            BO.BusLineStation busLineStationBO = new BO.BusLineStation();
            busLineStationBO = BusLineStationDoBoAdapter(busLineStationDO);
            busLine.BusLineStations.Append(busLineStationBO);
            if (position != busLine.BusLineStations.Count())
            {//אם זו לא התחנה האחרונה יש לעדכן את פרטי הזמן והמרחק של התחנה הבאה 
                foreach (BusLineStation s in busLine.BusLineStations)
                {
                    if (s.Position == position + 1)
                    {
                        UpdateBusLineStation(s.BusLineKey, s.StationKey, bls => bls = BusLineStationDoBoAdapter(busLineStationDO));
                    }
                }
            }
        }
        #endregion

    }
}
