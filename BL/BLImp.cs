using System;
using BLAPI;
using DLAPI;
using BO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Device.Location;

namespace BL
{
    class BLImp : IBL //internal
    {
        IDL dl = DLFactory.GetDL();
        #region Station

        public BO.Station StationDoBoAdapter(DO.Station StationDO)
        {
            BO.Station StationBO = new BO.Station();

            StationDO.Clone(StationBO);

            StationBO.BusLines = dl.GetAllLinesInStation(StationBO.Key);
            return StationBO;
        }
        public Station GetStation(int stationKey)
        {
            try
            {
                DO.Station StationDO = dl.GetStation(stationKey);
                return StationDoBoAdapter(StationDO);
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<Station> GetAllStations()
        {
            var AllStations = from station in dl.GetAllStations()
                              select StationDoBoAdapter(station);
            return AllStations;
        }
        public void AddStation(Station station)
        {
            if (station.Latitude > 90 || station.Latitude < -90)
            {
                throw new Exception();
            }
            if (station.Longitude > 180 || station.Longitude < -180)
            {
                throw new Exception();
            }
            try
            {
                DO.Station StationDO = new DO.Station();
                station.Clone(StationDO);
                dl.AddStation(StationDO);
            }
            catch
            {
                throw;
            }
        }
        public void UpdateStation(Station station)
        {
            DO.Station StationDO = new DO.Station();
            station.Clone(StationDO);
            dl.UpdateStation(StationDO);
        }
        public void UpdateStation(int stationKey, Action<Station> update)//method that knows to updt specific fields in Station
        {
            try
            {
                DO.Station StationDO = new DO.Station();
                Station StationBO = GetStation(stationKey);
                update(StationBO);
                StationBO.Clone(StationDO);
                dl.UpdateStation(StationDO);
            }
            catch (DO.ArgumentNotFoundException<BusLine> ex) { throw ex; }
        }
        public void DeleteStation(int stationKey)
        {
            try
            { 
                Station station = GetStation(stationKey);
                foreach(int bl in station.BusLines)
                {
                    DeleteStationFromLine(bl, stationKey);
                }
                dl.DeleteBusLineStationsByStation(stationKey);
                dl.DeleteConsecutiveStations(stationKey);
                dl.DeleteStation(stationKey);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region BusLineStation
        public BO.BusLineStation BusLineStationDoBoAdapter(DO.BusLineStation BusLineStationDO)
        {
            BO.BusLineStation BusLineStationBO = new BO.BusLineStation();

            BusLineStationDO.Clone(BusLineStationBO);
            DO.BusLineStation SecondBusLineStationDO = dl.GetBusLineStationBy(s => s.Position == BusLineStationDO.Position - 1 && s.BusLineKey == BusLineStationDO.BusLineKey);
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
        public BusLineStation GetBusLineStationByKey(int line, int stationKey)
        {
            BusLineStation busLineStation = DataSource.ListBusLineStations.Find(b => b.BusLineKey == line && b.StationKey == stationKey);
            if (busLineStation != null)
                return busLineStation.Clone();
            else
                throw new ArgumentNotFoundException<int>(line, $"Bus station of line {line} and station {stationKey} was not found.");
        }
        public IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine)
        {
            var AllStationsOfLine = from station in DataSource.ListBusLineStations
                                    where station.BusLineKey == busLine
                                    select station.Clone();
            return AllStationsOfLine;
        }
        public void AddBusLineStation(BusLineStation station)
        {
            if (DataSource.ListBusLineStations.FirstOrDefault(s => s.BusLineKey == station.BusLineKey && s.StationKey == station.StationKey) != null)
                throw new InvalidInformationException<int>(station.BusLineKey, "Duplicate station bus line number and station key");
            DataSource.ListBusLineStations.Add(station.Clone());
        }
        public void UpdateBusLineStation(BusLineStation station)
        {
            BusLineStation busLineStation = DataSource.ListBusLineStations.Find(s => s.BusLineKey == station.BusLineKey && s.StationKey == station.StationKey);
            if (busLineStation != null)
                busLineStation = station;
            else
                throw new ArgumentNotFoundException<int>(station.BusLineKey, $"Bus station of line {station.BusLineKey} and station {station.StationKey} was not found.");
        }
        public void UpdateBusLineStation(int line, int stationKey, Action<BusLineStation> update) //method that knows to updt specific fields in Person
        {
            BusLineStation busLineStation = DataSource.ListBusLineStations.Find(s => s.BusLineKey == line && s.StationKey == stationKey);
            if (busLineStation != null)
                update(busLineStation);
            else
                throw new ArgumentNotFoundException<int>(line, $"Bus station of line {line} and station {stationKey} was not found.");
        }
        public void DeleteBusLineStation(int line, int stationKey)
        {
            BusLineStation busLineStation = DataSource.ListBusLineStations.Find(b => b.BusLineKey == line && b.StationKey == stationKey);
            if (busLineStation == null)
                throw new ArgumentNotFoundException<int>(line, $"Bus station of line {line} and station {stationKey} was not found.");
            DataSource.ListBusLineStations.Remove(busLineStation);
        }
        #endregion

        #region BusLine
        public BO.BusLine BusLineDoBoAdapter(DO.BusLine BusLineDO)
        {
            BO.BusLine BusLineBO = new BO.BusLine();

            BusLineDO.Clone(BusLineBO);

            BusLineBO.BusLineStations = from blsDO in dl.GetAllStationsOfLine(BusLineBO.LineKey)
                                        select BusLineStationDoBoAdapter(blsDO);
            return BusLineBO;
        }
        public BusLine GetBusLine(int busLineKey)
        {
            try
            {
                return BusLineDoBoAdapter(dl.GetBusLine(busLineKey));
            }
            catch (DO.ArgumentNotFoundException<BusLine> ex)
            {

            }
        }
        public IEnumerable<BusLine> GetBusLinesBy(Predicate<BusLine> predicate)
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
        public IEnumerable<BusLine> GetAllBusLines()
        {
            var AllBuseLines = from line in DataSource.ListBusLines
                               select line.Clone();
            return AllBuseLines;
        }
        public void AddBusLine(BusLine bus)
        {
            try
            {
                DO.BusLine BusLineDO = new DO.BusLine();
                bus.Clone(BusLineDO);
                dl.AddBusLine(BusLineDO);
            }
            catch (DO.InvalidInformationException<BusLine> ex) { }
        }
        DO.ConsecutiveStations CalculateConsecutiveStations(DO.Station station1, DO.Station station2)
        {
            Random rand = new Random();
            int speed = rand.Next(30, 60);
            DO.ConsecutiveStations consecutiveStations = new DO.ConsecutiveStations();
            consecutiveStations.StationKey1 = station1.Key;
            consecutiveStations.StationKey2 = station2.Key;
            GeoCoordinate locationOfFirst = new GeoCoordinate(station1.Latitude, station1.Longitude);//מיקום התחנה המחושבת
            GeoCoordinate locationOfSecond = new GeoCoordinate(station2.Latitude, station2.Longitude);//מיקום התחנה הקודמת
            double distance = locationOfFirst.GetDistanceTo(locationOfSecond);//חישוב מרחק
            int time = Convert.ToInt32(distance / (speed * 1000 / 60));//חישוב זמן בהנחה שמהירות האוטובוס היא מספר בין 30 - 60 קמ"ש
            consecutiveStations.Distance = distance;
            consecutiveStations.AverageTime = time;
            return consecutiveStations;
        }
        public void AddStationToLine(int busLineKey, int stationKey, int position = 0)
        {
            BusLine busLine = GetBusLine(busLineKey);
            if (position == 0)
                position = busLine.BusLineStations.Count();
            DO.ConsecutiveStations consecutiveStationsFromPrev;
            DO.ConsecutiveStations consecutiveStationsToNext;
            //if(dl.GetStation(station.Key) == null)
            //    throw
            //if (position > bus.BusLineStations.Count() || position < 0)//מיקום רלוונטי - כגודל רשימת התחנות
            //    throw 
            DO.BusLineStation busLineStationDO = new DO.BusLineStation();
            busLineStationDO.BusLineKey = busLineKey;
            busLineStationDO.StationKey = stationKey;
            busLineStationDO.Position = position;
            dl.AddBusLineStation(busLineStationDO);
            BO.BusLineStation busLineStationBO = new BO.BusLineStation();
            busLineStationBO = BusLineStationDoBoAdapter(busLineStationDO);
            busLine.BusLineStations.Append(busLineStationBO);
            DO.BusLineStation prevBusLineStationDO = dl.GetBusLineStationBy
                (bls => bls.BusLineKey == busLineKey && bls.StationKey == stationKey
                && bls.Position == position - 1);
            DO.BusLineStation nextBusLineStationDO = dl.GetBusLineStationBy
                (bls => bls.BusLineKey == busLineKey && bls.StationKey == stationKey
                && bls.Position == position + 1);
            DO.ConsecutiveStations consecutiveStations = new DO.ConsecutiveStations();
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
        public void UpdateBusLine(BusLine bus)
        {
            try
            {
                DO.BusLine BusLineDO = new DO.BusLine();
                bus.Clone(BusLineDO);
                dl.UpdateBusLine(BusLineDO);
            }
            catch (DO.ArgumentNotFoundException<BusLine> ex) { }
        }
        public void UpdateBusLine(int busLineKey, Action<BusLine> update)
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
        public void DeleteBusLine(int busLineKey)
        {
            try
            {
                dl.DeleteBusLine(busLineKey);
            }
            catch (DO.ArgumentNotFoundException<BusLine> ex) { }
        }
        public void DeleteStationFromLine(int busKey, int stationKey)
        {
            BusLine busLine = GetBusLine(busKey);
            dl.DeleteBusLineStation(busKey, stationKey);
            DO.BusLineStation busLineStationDO = dl.GetBusLineStationByKey(busKey, stationKey);
            //foreach (BusLineStation bls in busLine.BusLineStations)
            //    if (bls.StationKey == stationKey && bls.BusLineKey == busKey)
            //        bls.IsActive = false;
            if (busLineStationDO.Position != busLine.BusLineStations.Count())
            {//אם זו לא התחנה האחרונה יש לעדכן את פרטי הזמן והמרחק של התחנה הבאה 
                foreach (BusLineStation s in busLine.BusLineStations)
                {
                    if (s.Position == busLineStationDO.Position + 1)
                    {
                        UpdateBusLineStation(s.BusLineKey, s.StationKey, bls => bls = BusLineStationDoBoAdapter(busLineStationDO));
                    }
                }
            }
        }
        #endregion

    }
}
 
