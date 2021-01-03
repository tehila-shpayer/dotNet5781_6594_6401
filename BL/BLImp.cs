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
            catch (DO.ArgumentNotFoundException ex)
            {
                throw new BOArgumentNotFoundException($"Can't get station {stationKey}", ex);
            }
        }
        public IEnumerable<Station> GetAllStations()
        {
            var AllStations = from station in dl.GetAllStations()
                              select StationDoBoAdapter(station);
            return AllStations;
        }
        void CheckStationParameters(Station station)
        {
            if (station.Latitude > 90 || station.Latitude < -90)
            {
                throw new BOInvalidInformationException($"Can't add station {station.Key}. Invalid latitude");
            }
            if (station.Longitude > 180 || station.Longitude < -180)
            {
                throw new BOInvalidInformationException($"Can't add station {station.Key}. Invalid longitude");
            }
        }
        public void AddStation(Station station)
        {

            CheckStationParameters(station);
            try
            {
                DO.Station StationDO = new DO.Station();
                station.Clone(StationDO);
                dl.AddStation(StationDO);
            }
            catch (DO.InvalidInformationException ex)
            {
                throw new BOInvalidInformationException($"Can't add station {station.Key}.", ex);
            }
        }
        public void UpdateStation(Station station)
        {
            CheckStationParameters(station);
            try
            {
                DO.Station StationDO = new DO.Station();
                station.Clone(StationDO);
                dl.UpdateStation(StationDO);
            }
            catch (DO.ArgumentNotFoundException ex)
            {
                throw new BOArgumentNotFoundException($"Can't update station {station.Key}.", ex);
            }
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
            catch (DO.ArgumentNotFoundException ex) { throw new BOArgumentNotFoundException($"Can't update station {stationKey}.", ex); }
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
            catch (DO.ArgumentNotFoundException ex) { throw new BOArgumentNotFoundException($"Can't delete station {stationKey}.", ex); }
        }
        #endregion

        #region BusLineStation
        public BO.BusLineStation BusLineStationDoBoAdapter(DO.BusLineStation BusLineStationDO)
        {
            BO.BusLineStation BusLineStationBO = new BO.BusLineStation();

            BusLineStationDO.Clone(BusLineStationBO);
            if (BusLineStationDO.Position == 1)
            {
                BusLineStationBO.DistanceFromLastStationMeters = 0;
                BusLineStationBO.TravelTimeFromLastStationMinutes = 0;
                return BusLineStationBO;
            }

            DO.BusLineStation SecondBusLineStationDO = dl.GetBusLineStationBy(s => s.Position == BusLineStationDO.Position - 1 && s.BusLineKey == BusLineStationDO.BusLineKey);
            BusLineStationBO.DistanceFromLastStationMeters = dl.GetConsecutiveStations(SecondBusLineStationDO.StationKey, BusLineStationDO.StationKey).Distance;
            BusLineStationBO.TravelTimeFromLastStationMinutes = dl.GetConsecutiveStations(SecondBusLineStationDO.StationKey, BusLineStationDO.StationKey).AverageTime;
            //BusLineStationBO.DistanceFromLastStationMeters = dl.GetConsecutiveStations(BusLineStationDO.StationKey, SecondBusLineStationDO.StationKey).Distance;
            //BusLineStationBO.TravelTimeFromLastStationMinutes = dl.GetConsecutiveStations(BusLineStationDO.StationKey, SecondBusLineStationDO.StationKey).AverageTime;
            return BusLineStationBO;
        }
        public BusLineStation GetBusLineStationByKey(int line, int stationKey)
        {
            try
            {
                return BusLineStationDoBoAdapter(dl.GetBusLineStationByKey(line, stationKey));
            }
            catch (DO.ArgumentNotFoundException ex)
            { throw new BOArgumentNotFoundException($"Can't get bus line station {stationKey}", ex); }
        }
        public IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine)
        {
            try
            {
                var AllStationsOfLine = from station in dl.GetAllBusLineStations()
                                        where station.BusLineKey == busLine
                                        select BusLineStationDoBoAdapter(station);
                return AllStationsOfLine;
            }
            catch (DO.ArgumentNotFoundException ex) { throw; }          
        }
        public void AddBusLineStation(BusLineStation bls)
        {
            try
            {
                DO.BusLineStation busLineStationDO = new DO.BusLineStation();
                bls.Clone(busLineStationDO);
                dl.AddBusLineStation(busLineStationDO);
            }
            catch (DO.InvalidInformationException ex) { throw; }
        }
        public void UpdateBusLineStation(BusLineStation station)
        {
            try
            {
                DO.BusLineStation busLineStationDO = new DO.BusLineStation();
                station.Clone(busLineStationDO);
                dl.UpdateBusLineStation(busLineStationDO);
            }
            catch (DO.InvalidInformationException ex) { throw; }
        }
        public void UpdateBusLineStation(int line, int stationKey, Action<BusLineStation> update) //method that knows to updt specific fields in Person
        {
            try
            {
                DO.BusLineStation BusLineStationDO = new DO.BusLineStation();
                BusLineStation BusLineStationBO = GetBusLineStationByKey(line, stationKey);
                update(BusLineStationBO);
                BusLineStationBO.Clone(BusLineStationDO);
                dl.UpdateBusLineStation(BusLineStationDO);
            }
            catch (DO.ArgumentNotFoundException ex) { }
        }
        public void DeleteBusLineStation(int line, int stationKey)
        {
            try
            {
                dl.DeleteBusLineStation(line, stationKey);
            }
            catch (DO.ArgumentNotFoundException ex) { }
        }
        public string ToStringForBusLine(BusLineStation bls)
        {
            DO.Station station = dl.GetStation(bls.StationKey);
            String s = "";
            if (bls.Position != 1)
                s += $@"

     |
     | {(int)(bls.DistanceFromLastStationMeters)} meters, {bls.TravelTimeFromLastStationMinutes} minutes
     |

";
            s += $"    {bls.Position}. Station key: {bls.StationKey}, Name: {station.Name}";
            return s;
        }
        #endregion

        #region BusLine
        public BO.BusLine BusLineDoBoAdapter(DO.BusLine BusLineDO)
        {
            BO.BusLine BusLineBO = new BO.BusLine();

            BusLineDO.Clone(BusLineBO);

            BusLineBO.BusLineStations = from blsDO in dl.GetAllStationsOfLine(BusLineBO.Key)
                                        select BusLineStationDoBoAdapter(blsDO);
            return BusLineBO;
        }
        public BusLine GetBusLine(int busLineKey)
        {
            try
            {
                return BusLineDoBoAdapter(dl.GetBusLine(busLineKey));
            }
            catch (DO.ArgumentNotFoundException ex)
            {
                throw new BOArgumentNotFoundException($"Can't get bus line {busLineKey}", ex);
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
            catch (DO.ArgumentNotFoundException ex) { throw; }
        }
        public IEnumerable<BusLine> GetAllBusLines()
        {
            var AllBuseLines = from line in dl.GetAllBusLines()
                               select BusLineDoBoAdapter(line);
            return AllBuseLines;
        }
        public void AddBusLine(BusLine bus)
        {
            try
            {
                bus.BusLineStations = null;
                DO.BusLine BusLineDO = new DO.BusLine();
                bus.Clone(BusLineDO);
                dl.AddBusLine(BusLineDO);
            }
            catch (DO.InvalidInformationException ex)
            {
                throw new BOInvalidInformationException($"Can't add bus line {bus.Key}.", ex);
            }
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
                position = busLine.BusLineStations.Count() + 1;

            //busLine = GetBusLine(busLineKey);//updates busLine to a bus line with new positions
            BO.BusLineStation busLineStationBO = new BO.BusLineStation();
            busLineStationBO.BusLineKey = busLineKey;
            busLineStationBO.StationKey = stationKey;
            busLineStationBO.Position = position;
            busLineStationBO.DistanceFromLastStationMeters = 0;
            busLineStationBO.TravelTimeFromLastStationMinutes = 0;

            //BusLineStation prevBusLineStation = busLine[position - 2];//return the busLine at the 0-based index
            //BusLineStation nextBusLineStation = busLine[position - 1];//return the busLine at the 0-based index
            BusLineStation prevBusLineStation = (from bls in GetAllStationsOfLine(busLineKey)
                                                 where bls.Position == position - 1
                                                 select bls).FirstOrDefault();
            BusLineStation nextBusLineStation = (from bls in GetAllStationsOfLine(busLineKey)
                                                 where bls.Position == position
                                                 select bls).FirstOrDefault();
            
            
            if (prevBusLineStation != null)
            {
                dl.AddConsecutiveStations(prevBusLineStation.StationKey, stationKey);
                DO.ConsecutiveStations cs = dl.GetConsecutiveStations(prevBusLineStation.StationKey, stationKey);
                busLineStationBO.DistanceFromLastStationMeters = cs.Distance;
                busLineStationBO.TravelTimeFromLastStationMinutes = cs.AverageTime;
            }
            AddBusLineStation(busLineStationBO);

            if (nextBusLineStation != null)
            {
                dl.AddConsecutiveStations(stationKey, nextBusLineStation.StationKey);
                DO.ConsecutiveStations cs = dl.GetConsecutiveStations(stationKey, nextBusLineStation.StationKey);
                nextBusLineStation.DistanceFromLastStationMeters = cs.Distance;
                nextBusLineStation.TravelTimeFromLastStationMinutes = cs.AverageTime;
                UpdateBusLineStation(nextBusLineStation);
            }
            var tmp = from bls in dl.GetAllStationsOfLine(busLineKey)
                      where bls.Position >= position && bls.StationKey != stationKey
                      select bls;

            foreach (DO.BusLineStation bls in tmp)
            //if (bls.Position >= position && bls.StationKey != stationKey)
            {
                bls.Position += 1;
                dl.UpdateBusLineStation(bls);
            }

        }
        public void AddStationToLine2(int busLineKey, int stationKey, int position = 0)
        {
            BusLine busLine = GetBusLine(busLineKey);
            if (position == 0)
                position = busLine.BusLineStations.Count() + 1;
            foreach (DO.BusLineStation bls in dl.GetAllStationsOfLine(busLineKey))
                if (bls.Position >= position)
                    bls.Position += 1;
            DO.ConsecutiveStations consecutiveStationsFromPrev;
            DO.ConsecutiveStations consecutiveStationsToNext;
            //if(dl.GetStation(station.Key) == null)
            //    throw
            //if (position > bus.BusLineStations.Count() + 1 || position < 0)//מיקום רלוונטי - כגודל רשימת התחנות
            //    throw
            
            DO.BusLineStation busLineStationDO = new DO.BusLineStation();
            busLineStationDO.BusLineKey = busLineKey;
            busLineStationDO.StationKey = stationKey;
            busLineStationDO.Position = position;

            dl.AddBusLineStation(busLineStationDO);

            BO.BusLineStation busLineStationBO = new BO.BusLineStation();
            //busLineStationBO = BusLineStationDoBoAdapter(busLineStationDO);
            //busLine.BusLineStations.Append(busLineStationBO);
            DO.BusLineStation prevBusLineStationDO = dl.GetBusLineStationBy
                (bls => bls.BusLineKey == busLineKey && bls.StationKey == stationKey
                && bls.Position == position - 1);
            DO.BusLineStation nextBusLineStationDO = dl.GetBusLineStationBy
                (bls => bls.BusLineKey == busLineKey && bls.StationKey == stationKey
                && bls.Position == position + 1);
            if (position != 1)
            {
                if (dl.GetConsecutiveStations(prevBusLineStationDO.StationKey, busLineStationDO.StationKey) != null)
                {
                    consecutiveStationsFromPrev = CalculateConsecutiveStations
                        (dl.GetStation(prevBusLineStationDO.StationKey), dl.GetStation(busLineStationDO.StationKey));
                    dl.AddConsecutiveStations(consecutiveStationsFromPrev);
                }
            }
            if (position != busLine.BusLineStations.Count())
            {//אם זו לא התחנה האחרונה יש לעדכן את פרטי הזמן והמרחק של התחנה הבאה 
                consecutiveStationsToNext = CalculateConsecutiveStations
(dl.GetStation(busLineStationDO.StationKey), dl.GetStation(nextBusLineStationDO.StationKey));
                if (dl.GetConsecutiveStations(prevBusLineStationDO.StationKey, busLineStationDO.StationKey) != null)
                    dl.AddConsecutiveStations(consecutiveStationsToNext);
            //    foreach (BusLineStation s in busLine.BusLineStations)
            //        if (s.Position == position + 1)
            //            UpdateBusLineStation(s.BusLineKey, s.StationKey, bls => bls = BusLineStationDoBoAdapter(busLineStationDO));
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
            catch (DO.ArgumentNotFoundException ex)
            {
                throw new BOArgumentNotFoundException($"Can't update bus line {bus.Key}.", ex);
            }
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
            catch (DO.ArgumentNotFoundException ex)
            {
                throw new BOArgumentNotFoundException($"Can't update bus line {busLineKey}.", ex);
            }
        }
        public void DeleteBusLine(int busLineKey)
        {
            try
            {
                dl.DeleteBusLine(busLineKey);
            }
            catch (DO.ArgumentNotFoundException ex)
            {
                throw new BOArgumentNotFoundException($"Can't delete bus line {busLineKey}.", ex);
            }
        }
        public void DeleteStationFromLine(int busKey, int stationKey)
        {
            BusLine busLine = GetBusLine(busKey);
            DO.BusLineStation busLineStationDO = dl.GetBusLineStationByKey(busKey, stationKey);
            int position = busLineStationDO.Position;
            //foreach (BusLineStation bls in busLine.BusLineStations)
            //    if (bls.StationKey == stationKey && bls.BusLineKey == busKey)
            //        bls.IsActive = false;
            if (busLineStationDO.Position != busLine.BusLineStations.Count())
            {//אם זו לא התחנה האחרונה יש לעדכן את פרטי הזמן והמרחק של התחנה הבאה 
                DO.BusLineStation prevBusLineStationDO = dl.GetBusLineStationBy
                (bls => bls.BusLineKey == busKey && bls.StationKey == stationKey
                && bls.Position == position - 1);
                DO.BusLineStation nextBusLineStationDO = dl.GetBusLineStationBy
                    (bls => bls.BusLineKey == busKey && bls.StationKey == stationKey
                    && bls.Position == position + 1);
                DO.ConsecutiveStations consecutiveStations = CalculateConsecutiveStations
(dl.GetStation(prevBusLineStationDO.StationKey), dl.GetStation(nextBusLineStationDO.StationKey));
                if (dl.GetConsecutiveStations(prevBusLineStationDO.StationKey, nextBusLineStationDO.StationKey) != null)
                    dl.AddConsecutiveStations(consecutiveStations);
            }
            dl.DeleteBusLineStation(busKey, stationKey);
            foreach (DO.BusLineStation bls in dl.GetAllStationsOfLine(busKey))
                if (bls.Position > position)
                    bls.Position -= 1;
        }
        public String ToStringBusLine(BusLine b)
        {
            String s = $"Line Key: {b.Key}\nBus line number: {b.LineNumber}\nArea: {b.Area}";
            if (b.BusLineStations.Count()>0)
            {
                s += $"\nFirst station: {b.FirstStation}, Last Station: {b.LastStation}\n";
                s += "  Stations in line:\n";
                foreach (BusLineStation bls in b.BusLineStations)
                {
                    s += ToStringForBusLine(bls);
                }
            }
            return s + '\n';
        }
        #endregion

    }
}
 
