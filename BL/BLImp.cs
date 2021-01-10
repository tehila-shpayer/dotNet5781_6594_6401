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
        #region Bus
        public BO.Bus BusDoBoAdapter(DO.Bus BusDO)
        {
            BO.Bus BusBO = new BO.Bus();
            BusDO.Clone(BusBO);
            return BusBO;
        }
        public IEnumerable<Bus> GetAllBuses()
        {
            var AllBuses = from bus in dl.GetAllBuses()
                           select BusDoBoAdapter(bus);
            return AllBuses;
        }
        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            var AllBusesBy = from bus in dl.GetAllBuses()
                             let busBO = BusDoBoAdapter(bus)
                             where predicate(busBO)
                             select busBO;
            return AllBusesBy;
        }
        public Bus GetBus(string LicenseNumber)
        {
            try
            {
                return BusDoBoAdapter(dl.GetBus(LicenseNumber));
            }
            catch (DO.ArgumentNotFoundException ex)
            { throw new BOArgumentNotFoundException("BO exception: Bus not found.", ex); }
        }
        void CheckBusParameters(Bus bus)
        {
            if (bus.LicenseNumber.Length < 7 || bus.LicenseNumber.Length > 8)
            {
                throw new BOInvalidInformationException("Couldn't add bus. invalid information!\nError: The license number has to be a 7 or 8 digit number!");
            }
            //Starting date must be in a reasonable spactrum
            else if (!(bus.RunningDate.Year >= 1896 && bus.RunningDate.Year <= DateTime.Now.Year))
            {
                throw new BOInvalidInformationException("Couldn't add bus. invalid information!\nError: Starting date must be after 1896 and before " + DateTime.Now.Year + "!");
            }
            //Running date must be before last treatment date
            else if (bus.RunningDate > bus.LastTreatment)
            {
                throw new BOInvalidInformationException("Couldn't add bus. invalid information!\nERROR: Starting date can't be later then last treatment date!");
            }
            // length of license number must much running date
            else if ((bus.LicenseNumber.Length == 7) && (bus.RunningDate.Year >= 2018))
            {
                throw new BOInvalidInformationException("Couldn't add bus. invalid information!\nERROR: A 7 digit license number bus can't be from later than 2017!");
            }
            else if ((bus.LicenseNumber.Length == 8) && (bus.RunningDate.Year < 2018))
            {
                throw new BOInvalidInformationException("Couldn't add bus. invalid information!\nERROR: A 8 digit license number bus can't be from earlier than 2018!");
            }
            //General KM must be atleast as KM since last treatment
            else if (bus.KM < bus.BeforeTreatKM)
            {
                throw new BOInvalidInformationException("Couldn't add bus. invalid information!\nERROR: Can't have more KM before treatment than general KM!");
            }
            //Fuel can be maximum 1200
            else if (bus.Fuel > 1200)
            {
                throw new BOInvalidInformationException("Couldn't add bus. invalid information!\nERROR: Fuel can't be over 1200!");
            }
            else if (bus.Fuel < 0)
            {
                throw new BOInvalidInformationException("Couldn't add bus. invalid information!\nERROR: Fuel can't be negative!");
            }
            if ((DateTime.Now - bus.LastTreatment).TotalDays > 365 || bus.BeforeTreatKM >= 20000 || bus.Fuel == 0)
            {
                bus.Status = Status.NotReady;
            }
            else
                bus.Status = Status.Ready;
        }
        

        public void AddBus(Bus bus)
        {
            CheckBusParameters(bus);
            //If all information is valid - add bus to collection
            try
            {
                DO.Bus busDO = new DO.Bus();
                bus.Clone(busDO);
                dl.AddBus(busDO);
            }
            catch (DO.InvalidInformationException ex)
            { throw new BOInvalidInformationException("Couldn't add bus. invalid information!", ex); }
        }
        public void UpdateBus(Bus bus)
        {
            CheckBusParameters(bus);
            try
            {
                DO.Bus BusDO = new DO.Bus();
                bus.Clone(BusDO);
                dl.UpdateBus(BusDO);
            }
            catch (DO.ArgumentNotFoundException ex)
            {
                throw new BOArgumentNotFoundException($"Can't update bus {bus.LicenseNumber}.", ex);
            }
        }
        public void UpdateBus(string LicenseNumber, Action<Bus> update)
        {
            try
            {
                DO.Bus busDO = new DO.Bus();
                Bus busBO = GetBus(LicenseNumber);
                update(busBO);
                busBO.Clone(busDO);
                dl.UpdateBus(busDO);
            }
            catch (DO.ArgumentNotFoundException ex) { throw new BOArgumentNotFoundException($"Can't update bus {LicenseNumber}.", ex); }
        }
        public void DeleteBus(string LicenseNumber)
        {
            try
            {
                dl.DeleteBus(LicenseNumber);
            }
            catch (DO.ArgumentNotFoundException ex) { throw new BOArgumentNotFoundException($"Can't delete bus {LicenseNumber}.", ex); }
        }
        #endregion

        #region User
        public BO.User UserDoBoAdapter(DO.User UserDO)
        {
            BO.User UserBO = new BO.User();
            UserDO.Clone(UserBO);
            return UserBO;
        }
        public User GetUser(string userName)
        {
            try
            {
                return UserDoBoAdapter(dl.GetUser(userName));
            }
            catch (DO.ArgumentNotFoundException ex)
            { throw new BOArgumentNotFoundException("BO exception: User not found.", ex); }
        }
        public User GetUser(string userName, string password)
        {
            try
            {
                return UserDoBoAdapter(dl.GetUser(userName, password));
            }
            catch (DO.ArgumentNotFoundException ex)
            { throw new BOArgumentNotFoundException("BO exception: User not found.", ex); }
        }
        public IEnumerable<User> GetAllUsers()
        {
            var AllUsers = from User in dl.GetAllUsers()
                           select UserDoBoAdapter(User);
            return AllUsers;
        }
        public void CheckUserParameters(User user)
        {
            string name = user.UserName;
            string password = user.Password;
            if (name.Length < 4)
                throw new BOInvalidInformationException("User name is short.");
            if (name.Length > 16)
                throw new BOInvalidInformationException("User name is long.");
            foreach (char key in name)
                if (!char.IsLetterOrDigit(key))
                    throw new BOInvalidInformationException("User name contains invalid keybords.");
            if (password.Length < 4)
                throw new BOInvalidInformationException("Password is short.");
            if (password.Length > 16)
                throw new BOInvalidInformationException("Password is long.");
            foreach (char key in password)
                if (!char.IsLetterOrDigit(key))
                    throw new BOInvalidInformationException("Password contains invalid keybords.");
        }
        public void AddUser(User user)
        {
            CheckUserParameters(user);
            //If all information is valid - add user to collection
            try
            {
                DO.User userDO = new DO.User();
                user.Clone(userDO);
                dl.AddUser(userDO);
            }
            catch (DO.InvalidInformationException ex)
            { throw new BOInvalidInformationException("Couldn't add user!", ex); }
        }
        public void UpdateUser(User user)
        {
            CheckUserParameters(user);
            try
            {
                DO.User UserDO = new DO.User();
                user.Clone(UserDO);
                dl.UpdateUser(UserDO);
            }
            catch (DO.ArgumentNotFoundException ex)
            {
                throw new BOArgumentNotFoundException($"Can't update user {user.UserName}.", ex);
            }
        }
        public void UpdateUser(string userName, Action<User> update)
        {
            try
            {
                DO.User userDO = new DO.User();
                User userBO = GetUser(userName);
                update(userBO);
                userBO.Clone(userDO);
                dl.UpdateUser(userDO);
            }
            catch (DO.ArgumentNotFoundException ex) { throw new BOArgumentNotFoundException($"Can't update user {userName}.", ex); }

        }
        public void DeleteUser(string userName)
        {
            try
            {
                dl.DeleteUser(userName);
            }
            catch (DO.ArgumentNotFoundException ex) { throw new BOArgumentNotFoundException($"Can't delete user {userName}.", ex); }

        }
        #endregion

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
        public int AddStation(Station station)
        {
            CheckStationParameters(station);
            try
            {
                if (station.Latitude >= 31.234567 && station.Longitude <= 34.56874)
                {
                    DO.Station StationDO = new DO.Station();
                    station.Clone(StationDO);
                    return dl.AddStation(StationDO);
                }
                else
                    throw new BOInvalidInformationException($"Can't add station {station.Key}. Location invalid!");
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
                List<int> currentBusLines = new List<int>();
                foreach (int bl in station.BusLines)
                    currentBusLines.Add(bl);
                foreach (int bl in currentBusLines)
                {
                    DeleteStationFromLine(bl, stationKey);
                    if (!station.BusLines.Any())
                        break;
                }
                dl.DeleteBusLineStationsByStation(stationKey);
                dl.DeleteConsecutiveStations(stationKey);
                dl.DeleteStation(stationKey);
            }
            catch (DO.ArgumentNotFoundException ex) { throw new BOArgumentNotFoundException($"Can't delete station {stationKey}.", ex); }
        }
        public string ToStringStation(Station station)
        {
            String s = "";
            s += $"Station Name: {station.Name}\nStation Key: {station.Key}\nLocation: {station.Latitude}°N, {station.Longitude}°E\n";
            if (station.BusLines.Count() == 0)
                return s + "There are no bus lines in this station\n";
            s += "Bus Line in station: ";
            foreach (int b in station.BusLines)
            {
                s += b + ", ";
            }
            return s.Substring(0, s.Length - 2) + "\n";
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
            var AllStationsOfLine = from station in dl.GetAllBusLineStations()
                                    where station.BusLineKey == busLine
                                    select BusLineStationDoBoAdapter(station);
            return AllStationsOfLine;
        }
        public void AddBusLineStation(BusLineStation bls)
        {
            try
            {
                DO.BusLineStation busLineStationDO = new DO.BusLineStation();
                bls.Clone(busLineStationDO);
                dl.AddBusLineStation(busLineStationDO);
            }
            catch (DO.InvalidInformationException ex)
            { throw new BOInvalidInformationException("Can't add bus line station. Invalid information.", ex); }
        }
        public void UpdateBusLineStation(BusLineStation station)
        {
            try
            {
                DO.BusLineStation busLineStationDO = new DO.BusLineStation();
                station.Clone(busLineStationDO);
                dl.UpdateBusLineStation(busLineStationDO);
            }
            catch (DO.InvalidInformationException ex)
            { throw new BOInvalidInformationException("Can't update bus line station. Invalid information.", ex); }
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
            catch (DO.ArgumentNotFoundException ex)
            { throw new BOInvalidInformationException("Can't add bus line station. Invalid information.", ex); }
        }
        public void DeleteBusLineStation(int line, int stationKey)
        {
            try
            {
                dl.DeleteBusLineStation(line, stationKey);
            }
            catch (DO.ArgumentNotFoundException ex)
            { throw new BOArgumentNotFoundException("Can't delete bus line station. Argument was not found.", ex); }
        }
        public string ToStringBusLineStation(BusLineStation bls)
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
            catch (DO.ArgumentNotFoundException ex)
            { throw new BOArgumentNotFoundException("Can't find bus line.", ex); }
        }
        public IEnumerable<BusLine> GetAllBusLines()
        {
            var AllBuseLines = from line in dl.GetAllBusLines()
                               select BusLineDoBoAdapter(line);
            return AllBuseLines;
        }
        public IEnumerable<BusLine> GetAllBusLinesOrderedBy(string orderBy)
        {
            switch (orderBy)
            {
                case "Order by key":
                    return from line in dl.GetAllBusLines()
                           orderby line.Key
                           select BusLineDoBoAdapter(line);
                case "Order by number":
                    return from line in dl.GetAllBusLines()
                           orderby line.LineNumber
                           select BusLineDoBoAdapter(line);
                case "Order by area":
                    return from line in dl.GetAllBusLines()
                           orderby line.Area.ToString()
                           select BusLineDoBoAdapter(line);
                default: return GetAllBusLines();
            }
        }

        public int AddBusLine(BusLine bus)
        {
            try
            {
                bus.BusLineStations = null;
                DO.BusLine BusLineDO = new DO.BusLine();
                bus.Clone(BusLineDO);
                return dl.AddBusLine(BusLineDO);

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
            if (position > busLine.BusLineStations.Count() + 1 || position < 0)//מיקום רלוונטי - כגודל רשימת התחנות
                throw new ArgumentOutOfRangeException($"Can't add the line station\n The index is illegal:\n There are only {busLine.BusLineStations.Count()} stations in line {busLineKey}");
            if (position == 0)
                position = busLine.BusLineStations.Count() + 1;

            BusLineStation prevBusLineStation = (from bls in GetAllStationsOfLine(busLineKey)
                                                 where bls.Position == position - 1
                                                 select bls).FirstOrDefault();
            BusLineStation nextBusLineStation = (from bls in GetAllStationsOfLine(busLineKey)
                                                 where bls.Position == position
                                                 select bls).FirstOrDefault();
            if (prevBusLineStation != null)
            {
                dl.AddConsecutiveStations(prevBusLineStation.StationKey, stationKey);
            }
            if (nextBusLineStation != null)
            {
                dl.AddConsecutiveStations(stationKey, nextBusLineStation.StationKey);
            }

            BO.BusLineStation busLineStationBO = new BO.BusLineStation();//create the new busLineStation
            busLineStationBO.BusLineKey = busLineKey;
            busLineStationBO.StationKey = stationKey;
            busLineStationBO.Position = position;
            AddBusLineStation(busLineStationBO);

            var tmp = from bls in dl.GetAllStationsOfLine(busLineKey)
                      where bls.Position >= position && bls.StationKey != stationKey
                      select bls;

            foreach (DO.BusLineStation bls in tmp)//update the position of the station in the line
            {
                bls.Position += 1;
                dl.UpdateBusLineStation(bls);
            }
            UpdateStation(stationKey, s => s.BusLines.Append(busLineKey));
        }
        public void AddStationToLine2(int busLineKey, int stationKey, int position = 0)
        {
            BusLine busLine = GetBusLine(busLineKey);
            if (position == 0)
                position = busLine.BusLineStations.Count() + 1;
            foreach (DO.BusLineStation bls in dl.GetAllStationsOfLine(busLineKey))
                if (bls.Position >= position)
                    dl.UpdateBusLineStation(bls.BusLineKey, bls.StationKey, b => b.Position += 1);
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
                (bls => bls.BusLineKey == busLineKey && bls.Position == position - 1);
            DO.BusLineStation nextBusLineStationDO = dl.GetBusLineStationBy
                (bls => bls.BusLineKey == busLineKey && bls.Position == position + 1);
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
                dl.DeleteBusLineStationsByLine(busLineKey);
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
            if (busLine.BusLineStations.Count() > 2)
            {
                DO.BusLineStation busLineStationDO = dl.GetBusLineStationByKey(busKey, stationKey);
                int position = busLineStationDO.Position;
                //foreach (BusLineStation bls in busLine.BusLineStations)
                //    if (bls.StationKey == stationKey && bls.BusLineKey == busKey)
                //        bls.IsActive = false;
                if (position != busLine.BusLineStations.Count() && position != 1)
                {//אם זו לא התחנה האחרונה יש לעדכן את פרטי הזמן והמרחק של התחנה הבאה                     
                    DO.BusLineStation prevBusLineStationDO = dl.GetBusLineStationBy
                    (bls => bls.BusLineKey == busKey && bls.Position == position - 1);
                    DO.BusLineStation nextBusLineStationDO = dl.GetBusLineStationBy
                        (bls => bls.BusLineKey == busKey && bls.Position == position + 1);
                    DO.ConsecutiveStations consecutiveStations = CalculateConsecutiveStations
    (dl.GetStation(prevBusLineStationDO.StationKey), dl.GetStation(nextBusLineStationDO.StationKey));
                    if (dl.GetConsecutiveStations(prevBusLineStationDO.StationKey, nextBusLineStationDO.StationKey) == null)
                        dl.AddConsecutiveStations(consecutiveStations);
                }
                dl.DeleteBusLineStation(busKey, stationKey);
                foreach (DO.BusLineStation bls in dl.GetAllStationsOfLine(busKey))
                    if (bls.Position > position)
                        dl.UpdateBusLineStation(bls.BusLineKey, bls.StationKey, b => b.Position -= 1);
            }
            else
                throw new BOInvalidInformationException("Cant delete station,\n bus must have at least 2 stations!");
        }
        public String ToStringBusLine(BusLine b)
        {
            String s = $"Line Key: {b.Key}\nBus line number: {b.LineNumber}\nArea: {b.Area}";
            if (b.BusLineStations.Count() > 0)
            {
                s += $"\nFirst station: {b.FirstStation}, Last Station: {b.LastStation}\n";
                s += "  Stations in line:\n";
                foreach (BusLineStation bls in b.BusLineStations)
                {
                    s += ToStringBusLineStation(bls);
                }
            }
            return s + '\n';
        }
        #endregion

        #region LineSchedule
        public BO.LineSchedule LineScheduleDoBoAdapter(DO.LineSchedule LineScheduleDO)
        {
            BO.LineSchedule LineScheduleBO = new BO.LineSchedule();
            LineScheduleDO.Clone(LineScheduleBO);
            return LineScheduleBO;
        }
        public LineSchedule GetLineSchedule(int lineKey, DateTime startTime)
        {
            try
            {
                DO.LineSchedule LineScheduleDO = dl.GetLineSchedule(lineKey, startTime);
                return LineScheduleDoBoAdapter(LineScheduleDO);
            }
            catch (DO.ArgumentNotFoundException ex)
            {
                throw new BOArgumentNotFoundException($"Can't get line schedule {lineKey} that start at {startTime.ToString("HH:mm")}", ex);
            }
        }
        public IEnumerable<LineSchedule> GetAllLineSchedules()
        {
            var AllLineSchedules = from ls in dl.GetAllLineSchedules()
                              select LineScheduleDoBoAdapter(ls);
            return AllLineSchedules;
        }
        public IEnumerable<LineSchedule> GetAllLineSchedulesOfLine(int Line)
        {
            var AllLineSchedules = from ls in dl.GetAllLineSchedulesOfLine(Line)
                                   select LineScheduleDoBoAdapter(ls);
            return AllLineSchedules;
        }
        public void AddLineSchedule(LineSchedule lineSchedule)
        {
            try
            {
                DO.LineSchedule LineScheduleDO = new DO.LineSchedule();
                lineSchedule.Clone(LineScheduleDO);
                dl.AddLineSchedule(LineScheduleDO);
            }
            catch (DO.InvalidInformationException ex)
            {
                throw new BOInvalidInformationException($"Can't add the line schedule.", ex);
            }
        }
        public void UpdateLineSchedule(LineSchedule lineSchedule)
        {
            try
            {
                DO.LineSchedule LineScheduleDO = new DO.LineSchedule();

                lineSchedule.Clone(LineScheduleDO);
                dl.UpdateLineSchedule(LineScheduleDO);
            }
            catch (DO.ArgumentNotFoundException ex) { throw new BOArgumentNotFoundException($"Can't update line schedule.", ex); }
        }
        public void UpdateLineSchedule(int lineKey, DateTime startTime, Action<LineSchedule> update)
        {
            try
            {
                DO.LineSchedule LineScheduleDO = new DO.LineSchedule();
                LineSchedule LineScheduleBO = GetLineSchedule(lineKey, startTime);
                update(LineScheduleBO);
                LineScheduleBO.Clone(LineScheduleDO);
                dl.UpdateLineSchedule(LineScheduleDO);
            }
            catch (DO.ArgumentNotFoundException ex) { throw new BOArgumentNotFoundException($"Can't update line schedule.", ex); }
        }
        public void DeleteLineSchedule(int lineKey, DateTime startTime)
        {
            try
            {
                dl.DeleteLineSchedule(lineKey, startTime);
            }
            catch (DO.ArgumentNotFoundException ex) { throw new BOArgumentNotFoundException($"Can't delete schedule line {lineKey} that start at {startTime.ToString("HH:mm")}.", ex); }
        }
        #endregion

        #region BusInTravel
        public BusInTravel CreateBusInTravel(string licenseNumber, int line, DateTime startingTime)
        {
            return new BusInTravel();
        }
        #endregion

    }
}

