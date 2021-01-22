﻿using System;
using BLAPI;
using DLAPI;
using BO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Device.Location;
using System.ComponentModel;
using System.Diagnostics;

namespace BL
{
    class BLImp : IBL //internal
    {
        #region singelton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }// static ctor to ensure instance init is done just before first usage
        BLImp() { } // default => private
        public static BLImp Instance { get => instance; }// The public Instance property to use
        #endregion

        IDL dl = DLFactory.GetDL();

        #region BLFunctions
        int GetDistance(int stationKey1, int stationKey2)
        {
            Station station1 = GetStation(stationKey1);
            Station station2 = GetStation(stationKey2);
            GeoCoordinate s1 = new GeoCoordinate(station1.Latitude, station1.Longitude);//מיקום תחנה 1
            GeoCoordinate s2 = new GeoCoordinate(station2.Latitude, station2.Longitude);//מיקום תחנה 2
            return Convert.ToInt32(s1.GetDistanceTo(s2) * 1.5 + 1);//חישוב מרחק
        }
        int GetTime(double distance)
        {
            Random rand = new Random();
            int speed = rand.Next(30, 60);
            int time = Convert.ToInt32(distance / (speed * 1000 / 60) + 1);//חישוב זמן בהנחה שמהירות האוטובוס היא מספר בין 30 - 60 קמ"ש
            return time;
        }
        int GetTime(int stationKey1, int stationKey2)
        {
            return GetTime(GetDistance(stationKey1, stationKey2));
        }
        #endregion

        #region Simulator
        BackgroundWorker timer = new BackgroundWorker();
        Clock simulatorClock = new Clock(new TimeSpan(0, 0, 0), 1);
        public void StartSimulator(Clock clock, TimeSpan startTime, int simulatorRate, int Key)
        {
            timer = new BackgroundWorker();
            timer.DoWork += Timer_DoWork;
            timer.ProgressChanged += Timer_ProgressChanged;
            timer.RunWorkerCompleted += Timer_RunWorkerCompleted;
            timer.WorkerReportsProgress = true;
            timer.WorkerSupportsCancellation = true;
            simulatorClock = clock;
            clock.rate = simulatorRate;
            timer.RunWorkerAsync();

        }

        private void Timer_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();
            //Clock simulatorClock = e.Argument as Clock;
            while (simulatorClock.IsTimerRun)
            {
                TimeSpan ts = new TimeSpan(simulatorClock.startTime.Ticks + stopwatch.ElapsedTicks * simulatorClock.rate);
                timer.ReportProgress((int)(ts.TotalSeconds));
                //Thread.Sleep(1000/simulatorClock.rate);
                Thread.Sleep(1000);
            }
            stopwatch.Stop();
        }
        private void Timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeSpan ts = new TimeSpan();
            ts = TimeSpan.FromSeconds(e.ProgressPercentage);
            simulatorClock.Time = new TimeSpan(ts.Hours, ts.Minutes, ts.Seconds);
        }
        private void Timer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            simulatorClock.IsTimerRun = false;
        }
        public void StopSimulator()
        {
            simulatorClock.IsTimerRun = false;
        }
        #endregion

        #region Bus
        public BO.Bus BusDoBoAdapter(DO.Bus BusDO)
        {
            BO.Bus BusBO = new BO.Bus();
            BusDO.Clone(BusBO);
            return BusBO;
        }
        public IEnumerable<Bus> GetAllBusesOrderedBy(string orderBy)
        {
            switch (orderBy)
            {
                case "Order by license number":
                    return from b in dl.GetAllBuses()
                           orderby b.LicenseNumber
                           select BusDoBoAdapter(b);
                case "Order by status":
                    return from b in dl.GetAllBuses()
                           orderby b.Status
                           select BusDoBoAdapter(b);
                default: return GetAllBuses();
            }
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
                throw new BOInvalidInformationException("The license number has to be a 7 or 8 digit number!");
            }
            //Starting date must be in a reasonable spactrum
            else if (!(bus.RunningDate.Year >= 1896 && bus.RunningDate.Year <= DateTime.Now.Year))
            {
                throw new BOInvalidInformationException("Starting date must be after 1896 and before " + DateTime.Now.Year + "!");
            }
            //Running date must be before last treatment date
            else if (bus.RunningDate > bus.LastTreatment)
            {
                throw new BOInvalidInformationException("Starting date can't be later then last treatment date!");
            }
            // length of license number must much running date
            else if ((bus.LicenseNumber.Length == 7) && (bus.RunningDate.Year >= 2018))
            {
                throw new BOInvalidInformationException("A 7 digit license number bus must be from earlier than 2018!");
            }
            else if ((bus.LicenseNumber.Length == 8) && (bus.RunningDate.Year < 2018))
            {
                throw new BOInvalidInformationException("A 8 digit license number bus must from later than 2017!");
            }
            //General KM must be atleast as KM since last treatment
            else if (bus.KM < bus.BeforeTreatKM)
            {
                throw new BOInvalidInformationException("Can't have more KM before treatment than general KM!");
            }
            //Fuel can be maximum 1200
            else if (bus.Fuel > 1200)
            {
                throw new BOInvalidInformationException("Fuel can't be over 1200!");
            }
            else if (bus.Fuel < 0)
            {
                throw new BOInvalidInformationException("Fuel can't be negative!");
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
                throw new BOInvalidInformationException("User name is too short, it mustbe at least 4 characters long.");
            if (name.Length > 16)
                throw new BOInvalidInformationException("User name is too long. it must be at most 16 characters long.");
            foreach (char key in name)
                if (!char.IsLetterOrDigit(key))
                    throw new BOInvalidInformationException("User name contains invalid keybords.");
            if (password.Length < 4)
                throw new BOInvalidInformationException("Password is too short, it mustbe at least 4 characters long.");
            if (password.Length > 16)
                throw new BOInvalidInformationException("Password is too long, it must be at most 16 characters long.");
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
                user.Password = Tools.hashPassword(user.Password + user.Salt);
                DO.User userDO = new DO.User();
                user.Clone(userDO);
                dl.AddUser(userDO);
            }
            catch (DO.InvalidInformationException ex)
            { throw new BOInvalidInformationException("Couldn't add user!", ex); }
        }
        public void UpdateUser(User user)
        {
            string oldPassword = dl.GetUser(user.UserName).Password;
            if (user.Password != oldPassword)
            {
                CheckUserParameters(user);
                user.Password = Tools.hashPassword(user.Password + user.Salt); 
            }
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
        public IEnumerable<Station> GetAllStationsOrderedBy(string orderBy)
        {
            switch (orderBy)
            {
                case "Order by key":
                    return from s in dl.GetAllStations()
                           orderby s.Key
                           select StationDoBoAdapter(s);
                case "Order by name":
                    return from s in dl.GetAllStations()
                           orderby s.Name
                           select StationDoBoAdapter(s);
                default: return GetAllStations();
            }
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
                throw new BOInvalidInformationException($"Invalid latitude");
            }
            if (station.Longitude > 180 || station.Longitude < -180)
            {
                throw new BOInvalidInformationException($"Invalid longitude");
            }
            if (station.Latitude < 29.3 || station.Latitude > 33.5 || station.Longitude < 33.7 || station.Longitude > 36.3)
                throw new BOInvalidInformationException($"Location must be in Israel!");
        }
        public int AddStation(Station station)
        {
            CheckStationParameters(station);
            try
            {
                DO.Station StationDO = new DO.Station();
                station.Clone(StationDO);
                return dl.AddStation(StationDO);

            }
            catch (DO.InvalidInformationException ex)
            {
                throw new BOInvalidInformationException($"Can't add station.", ex);
            }
        }
        public void UpdateStation(Station station)
        {
            CheckStationParameters(station);
            try
            {
                DO.Station StationDO = dl.GetStation(station.Key);
                if (station.Latitude != StationDO.Latitude || station.Longitude != StationDO.Longitude)
                {
                    foreach (DO.BusLineStation bls in dl.GetAllBusLineStationsBy(ls => ls.StationKey == station.Key))
                    {
                        DO.ConsecutiveStations cs = new DO.ConsecutiveStations();
                        DO.BusLineStation nextLineStation = dl.GetBusLineStationBy(ls => ls.BusLineKey == bls.BusLineKey && ls.Position == bls.Position + 1);
                        DO.BusLineStation prevLineStation = dl.GetBusLineStationBy(ls => ls.BusLineKey == bls.BusLineKey && ls.Position == bls.Position - 1);
                        StationDO = dl.GetStation(station.Key);
                        if (prevLineStation != null)
                        {
                            dl.UpdateConsecutiveStations(CalculateConsecutiveStations(dl.GetStation(prevLineStation.StationKey), StationDO));
                        }
                        if (nextLineStation != null)
                        {
                            dl.UpdateConsecutiveStations(CalculateConsecutiveStations(StationDO, dl.GetStation(nextLineStation.StationKey)));
                        }
                    }
                }
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
            BusLineStationBO.DistanceFromLastStationMeters = (int)dl.GetConsecutiveStations(SecondBusLineStationDO.StationKey, BusLineStationDO.StationKey).Distance;
            BusLineStationBO.TravelTimeFromLastStationMinutes = dl.GetConsecutiveStations(SecondBusLineStationDO.StationKey, BusLineStationDO.StationKey).AverageTime;
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
            { throw ex; }
        }
        public void UpdateBusLineStation(BusLineStation station)
        {
            try
            {
                DO.BusLineStation busLineStationDO = new DO.BusLineStation();
                station.Clone(busLineStationDO);
                Station s = GetPreviouseStation(station.BusLineKey, station.Position);
                dl.UpdateConsecutiveStations(s.Key, station.StationKey, cs => { cs.AverageTime = station.TravelTimeFromLastStationMinutes; cs.Distance = station.DistanceFromLastStationMeters; });
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
        public Station GetPreviouseStation(int lineStationKey, int position)
        {
            if (position == 1)
                throw new BOArgumentNotFoundException($"Can't find previouse station of station in position 1");
            BusLineStation bls = GetBusLineStation(lineStationKey, position);
            BusLineStation prev = GetBusLineStation(bls.BusLineKey, bls.Position - 1);
            if (prev == null)
                throw new BOArgumentNotFoundException($"Can't find previouse station");
            return GetStation(prev.StationKey);
        }
        public BusLineStation GetBusLineStation(int busKey, int Position)
        {
            foreach (BusLineStation bls in GetAllStationsOfLine(busKey))
                if (bls.Position == Position)
                    return bls;
            return null;
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
        public int AddBusLine(BusLine bus, int stationKey1, int stationKey2)
        {
            int key = AddBusLine(bus);
            AddStationToLine(key, stationKey1);
            AddStationToLine(key, stationKey2);
            return key;
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
            int time = Convert.ToInt32(distance / (speed * 1000 / 60) + 1);//חישוב זמן בהנחה שמהירות האוטובוס היא מספר בין 30 - 60 קמ"ש
            consecutiveStations.Distance = (int)distance;
            consecutiveStations.AverageTime = time;
            return consecutiveStations;
        }
        DO.ConsecutiveStations CalculateConsecutiveStations(int s1, int s2)
        {
            DO.ConsecutiveStations consecutiveStations = new DO.ConsecutiveStations();
            consecutiveStations.StationKey1 = s1;
            consecutiveStations.StationKey2 = s2;
            int d = GetDistance(s1, s2);
            consecutiveStations.Distance = d;
            consecutiveStations.AverageTime = GetTime(d);
            return consecutiveStations;
        }
        public void AddStationToLine(int busLineKey, int stationKey, int position = 0)
        {
            try
            {
                BusLine busLine = GetBusLine(busLineKey);
                if (position > busLine.BusLineStations.Count() + 1 || position < 0)//מיקום רלוונטי - כגודל רשימת התחנות
                    throw new ArgumentOutOfRangeException($"The index is illegal:\n There are only {busLine.BusLineStations.Count()} stations in line {busLineKey}");
                if (position == 0)
                    position = busLine.BusLineStations.Count() + 1;

                BusLineStation prevBusLineStation = (from bls in GetAllStationsOfLine(busLineKey)
                                                     where bls.Position == position - 1
                                                     select bls).FirstOrDefault();
                BusLineStation nextBusLineStation = (from bls in GetAllStationsOfLine(busLineKey)
                                                     where bls.Position == position
                                                     select bls).FirstOrDefault();

                BO.BusLineStation busLineStationBO = new BO.BusLineStation();//create the new busLineStation
                busLineStationBO.BusLineKey = busLineKey;
                busLineStationBO.StationKey = stationKey;
                busLineStationBO.Position = position;
                AddBusLineStation(busLineStationBO);
                if (prevBusLineStation != null)
                {
                    dl.AddConsecutiveStations(CalculateConsecutiveStations(prevBusLineStation.StationKey, stationKey));
                }
                if (nextBusLineStation != null)
                {
                    dl.AddConsecutiveStations(CalculateConsecutiveStations(stationKey, nextBusLineStation.StationKey));
                }


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
            catch (DO.InvalidInformationException ex)
            {
                throw new BOInvalidInformationException($"The station { stationKey } already exits in this line!", ex);
            }
            catch (Exception)
            {
                throw;
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
                throw new BOInvalidInformationException("bus must have at least 2 stations!");
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
        public LineSchedule GetLineSchedule(int lineKey, TimeSpan startTime)
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
        IEnumerable<TimeSpan> GetAllTimesOfSchedule(LineSchedule schedule)
        {
            for (TimeSpan i = schedule.StartTime; i < schedule.EndTime; i += new TimeSpan(0, schedule.Frequency, 0))
                yield return i;
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
            catch (DO.ArgumentNotFoundException ex)
            {
                throw new BOArgumentNotFoundException($"Can't add the line schedule. " + ex.Message);
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
        public void UpdateLineSchedule(int lineKey, TimeSpan startTime, Action<LineSchedule> update)
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
        public void DeleteLineSchedule(int lineKey, TimeSpan startTime)
        {
            try
            {
                dl.DeleteLineSchedule(lineKey, startTime);
            }
            catch (DO.ArgumentNotFoundException ex) { throw new BOArgumentNotFoundException($"Can't delete schedule line {lineKey} that start at {startTime.ToString("HH:mm")}.", ex); }
        }
        #endregion

        #region BusInTravel
        public BusInTravel CreateBusInTravel(TimeSpan time, LineSchedule lineSchedule, TimeSpan i, Station station, double latePrecentage, string licenseNumber = "00000000")
        {
            BusInTravel bit = new BusInTravel();
            bit.Key = BusInTravel.BUS_TRAVEL_KEY++;
            bit.BusLicenseNumber = licenseNumber;
            bit.LineKey = lineSchedule.LineKey;
            bit.StationKey = station.Key;
            //bit.StartTime = lineSchedule.StartTime + new TimeSpan(0, i * lineSchedule.Frequency, 0);
            bit.StartTime = TimeSpan.FromSeconds(i.TotalSeconds * mix(lineSchedule.LineKey) / 100);
            bit.TimeLeft = GetTimeLeft(bit, time);
            if (bit.TimeLeft < new TimeSpan(0, 0, 0) || bit.TimeLeft > new TimeSpan(1, 30, 0))
                return null;
            return bit;
        }
        int mix(int lineKey)
        {
            return Convert.ToInt32((Math.Log(Math.Sqrt(lineKey) * 500) * Math.PI)) % 10 + 95;
        }
        TimeSpan GetTimeLeft(BusInTravel bit, TimeSpan time)
        {
            return GetTimeFromFirstStation(bit.LineKey, bit.StationKey) - (time - bit.StartTime);
        }
        TimeSpan GetTimeFromFirstStation(int lineKey, int stationKey)
        {
            BusLine line = GetBusLine(lineKey);
            int totalMinutes = 0;
            foreach (BusLineStation bls in line.BusLineStations)
            {
                totalMinutes += bls.TravelTimeFromLastStationMinutes;
                if (bls.StationKey == stationKey)
                    break;
            }
            return new TimeSpan(0, totalMinutes, 0);
        }
        public IEnumerable<BusInTravel> GetLineTimingsPerStation(int stationKey, TimeSpan time, double latePrecentage)
        {
            Station station = GetStation(stationKey);
            IEnumerable<BusInTravel> busInTravels = new List<BusInTravel>();
            var lineSchedules = from bl in station.BusLines
                                let schedules = GetAllLineSchedulesOfLine(bl)
                                from ls in schedules
                                where Between(ls, time)
                                select ls;
            foreach (LineSchedule schedule in lineSchedules)
            {
                //for (int i = 0; schedule.StartTime + new TimeSpan(0, i * schedule.Frequency -30, 0) < time; i++)
                //{
                //    BusInTravel busInTravel = CreateBusInTravel(schedule, i, station);
                //    if (busInTravel != null)
                //        busInTravels = busInTravels.Append(busInTravel);
                //}
                for (TimeSpan i = GetFirstTravelTime(schedule, time); i < time + new TimeSpan(1, 0, 0) && i <= schedule.EndTime; i += new TimeSpan(0, schedule.Frequency, 0))
                {
                    BusInTravel busInTravel = CreateBusInTravel(time, schedule, i, station, latePrecentage);
                    if (busInTravel != null)
                        busInTravels = busInTravels.Append(busInTravel);
                }
            }
            return busInTravels.OrderBy(s => s.TimeLeft);
        }
        TimeSpan GetFirstTravelTime(LineSchedule lineSchedule, TimeSpan time)
        {
            TimeSpan tmp = lineSchedule.StartTime;
            //if (time > lineSchedule.StartTime)
            //{
            //    while (time - new TimeSpan(1, 0, 0) > tmp)
            //    {
            //        tmp += new TimeSpan(0, lineSchedule.Frequency, 0);
            //    }
            //    return lineSchedule.StartTime;
            //}

            while (time - new TimeSpan(1, 0, 0) > tmp)
            {
                tmp += new TimeSpan(0, lineSchedule.Frequency, 0);
            }
            return tmp;
        }
        bool Between(LineSchedule lineSchedule, TimeSpan time)
        {
            return (lineSchedule.StartTime < time && lineSchedule.EndTime > time) || lineSchedule.StartTime < time - new TimeSpan(1, 0, 0) && lineSchedule.EndTime > time - new TimeSpan(1, 0, 0) || lineSchedule.StartTime < time + new TimeSpan(1, 0, 0) && lineSchedule.EndTime > time + new TimeSpan(1, 0, 0);
        }
        TimeSpan Max(TimeSpan t1, TimeSpan t2)
        {
            if (t1 > t2)
                return t1;
            return t2;
        }
        #endregion

        public IEnumerable<BusLine> FindRoutes(Station s1, Station s2)
        {
            s1.BusLines = dl.GetAllLinesInStation(s1.Key);
            s2.BusLines = dl.GetAllLinesInStation(s2.Key);
            return from line1 in s1.BusLines
                        from line2 in s2.BusLines
                        where line1 == line2 && GetBusLine(line1).BusLineStations.First(bls => bls.StationKey == s1.Key).Position < GetBusLine(line1).BusLineStations.First(bls => bls.StationKey == s2.Key).Position
                        select GetBusLine(line1);
        }

        public IEnumerable<ArrivalTimes> GetArrivalTimes(int lineKey, int s1, int s2)
        {
            return from ls in GetAllLineSchedulesOfLine(lineKey)
                    from t in GetAllTimesOfSchedule(ls)
                    select new ArrivalTimes
                    { Start = t, SourceArrive = t + GetTimeFromFirstStation(lineKey, s1), DestinationArrive = t + GetTimeFromFirstStation(lineKey, s2) };

        }
    }
}

