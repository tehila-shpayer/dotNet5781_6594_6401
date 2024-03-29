﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using DLAPI;
using DO;
using DS;

namespace DL
{
    //מימוש ממשק החוזה IDL ע"י Object 
    //תקציר פונקציות בהגדרת הממשק
    sealed class DL : IDL    //internal
    {
        #region singelton
        static readonly DL instance = new DL();
        static DL() { }// static ctor to ensure instance init is done just before first usage
        DL() { } // default => private
        public static DL Instance { get => instance; }// The public Instance property to use
        #endregion

        #region Bus
        public IEnumerable<Bus> GetAllBuses()
        {
            var AllBuses = from bus in DataSource.ListBuses
                           select bus.Clone();
            return AllBuses;
        }
        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            var AllBusesBy = from bus in DataSource.ListBuses
                             where predicate(bus)
                             select bus.Clone();
            return AllBusesBy;
        }
        public Bus GetBus(string LicenseNumber)
        {
            Bus bus = DataSource.ListBuses.Find(b => b.LicenseNumber == LicenseNumber);
            if (bus != null)
                return bus.Clone();
            else
                throw new ArgumentNotFoundException($"Bus {bus.LicenseNumber} not found.");
        }
        public void AddBus(Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(p => p.LicenseNumber == bus.LicenseNumber) != null)
                throw new InvalidInformationException("Duplicate bus license number");
            DataSource.ListBuses.Add(bus.Clone());
        }
        public void UpdateBus(Bus bus)
        {
            int indexOfBusToUpdate = DataSource.ListBuses.FindIndex(s => s.LicenseNumber == bus.LicenseNumber);
            if (indexOfBusToUpdate >= 0)
                DataSource.ListBuses[indexOfBusToUpdate] = bus;
            else
                throw new ArgumentNotFoundException($"Bus {bus.LicenseNumber} not found.");
        }
        public void UpdateBus(string LicenseNumber, Action<Bus> update) //method that knows to updt specific fields in Bus
        {
            int indexOfBusToUpdate = DataSource.ListBuses.FindIndex(s => s.LicenseNumber == LicenseNumber);
            if (indexOfBusToUpdate >= 0)
            {
                update(DataSource.ListBuses[indexOfBusToUpdate]);
            }
            else
                throw new ArgumentNotFoundException($"Bus {LicenseNumber} not found.");
        }
        public void DeleteBus(string LicenseNumber)
        {
            Bus bus = DataSource.ListBuses.Find(b => b.LicenseNumber == LicenseNumber);
            if (bus == null)
                throw new ArgumentNotFoundException($"Bus {bus.LicenseNumber} not found.");
            DataSource.ListBuses.Remove(bus);
        }
        #endregion

        #region BusLine
        public BusLine GetBusLine(int busLineKey)
        {
            BusLine line = DataSource.ListBusLines.Find(b => b.Key == busLineKey);
            if (line != null)
                return line.Clone();
            else
                throw new ArgumentNotFoundException($"Bus not found with license number: {busLineKey}");
        }
        public IEnumerable<BusLine> GetBusLinesBy(Predicate<BusLine> predicate)
        {
            var AllBuseLinesBy = from line in DataSource.ListBusLines
                                 where predicate(line)
                                 select line.Clone();
            return AllBuseLinesBy;
        }
        public IEnumerable<BusLine> GetAllBusLines()
        {
            var AllBuseLines = from line in DataSource.ListBusLines
                               select line.Clone();
            return AllBuseLines;
        }
        public int AddBusLine(BusLine bus)
        {
            bus.Key = BusLine.BUS_LINE_KEY++;
            //if (DataSource.ListBusLines.FirstOrDefault(l => l.Key == bus.Key) != null)
            //    throw new InvalidInformationException("Duplicate bus line key");
            DataSource.ListBusLines.Add(bus.Clone());
            return bus.Key;
        }
        public void UpdateBusLine(BusLine line)
        {
            int indexOfBusLineToUpdate = DataSource.ListBusLines.FindIndex(s => s.Key == line.Key);
            if (indexOfBusLineToUpdate == -1)
                throw new ArgumentNotFoundException($"BusLine not found with key: {line.Key}");
            DataSource.ListBusLines[indexOfBusLineToUpdate] = line;
        }
        public void UpdateBusLine(int busLineKey, Action<BusLine> update)//method that knows to updt specific fields in BusLine
        {
            BusLine bus = DataSource.ListBusLines.Find(b => b.Key == busLineKey);
            if (bus != null)
                update(bus);
            else
                throw new ArgumentNotFoundException($"Bus not found with license number: {busLineKey}");
        }
        public void DeleteBusLine(int busLineKey)
        {
            BusLine bus = DataSource.ListBusLines.Find(b => b.Key == busLineKey);
            if (bus == null)
                throw new ArgumentNotFoundException($"Bus not found with license number: {busLineKey}");
            DataSource.ListBusLines.Remove(bus);
        }

        #endregion

        #region BusLineStation
        public IEnumerable<BusLineStation> GetAllBusLineStations()
        {
            var AllBusLineStations = from bls in DataSource.ListBusLineStations
                                     select bls.Clone();
            return AllBusLineStations;
        }
        public IEnumerable<BusLineStation> GetAllBusLineStationsBy(Predicate<BusLineStation> predicate)
        {
            var AllBusLineStations = from bls in DataSource.ListBusLineStations
                                     where predicate(bls)
                                     select bls.Clone();
            return AllBusLineStations;
        }
        public BusLineStation GetBusLineStationBy(Predicate<BusLineStation> predicate)
        {
            BusLineStation busLineStation = DataSource.ListBusLineStations.Find(b => predicate(b));
            if (busLineStation != null)
                return busLineStation.Clone();
            else
                throw new ArgumentNotFoundException($"Bus line station required was not found.");
        }
        public BusLineStation GetBusLineStationByKey(int line, int stationKey)
        {
            BusLineStation busLineStation = DataSource.ListBusLineStations.Find(b => b.BusLineKey == line && b.StationKey == stationKey);
            if (busLineStation != null)
                return busLineStation.Clone();
            else
                throw new ArgumentNotFoundException($"Bus line station of line {line} and station {stationKey} was not found.");
        }
        public IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine)
        {
            var AllStationsOfLine = from station in DataSource.ListBusLineStations
                                    where station.BusLineKey == busLine
                                    select station.Clone();
            return AllStationsOfLine.OrderBy(bls => bls.Position);
        }
        public void AddBusLineStation(BusLineStation station)
        {
            if (DataSource.ListBusLineStations.FirstOrDefault(s => s.BusLineKey == station.BusLineKey && s.StationKey == station.StationKey) != null)
                throw new InvalidInformationException("Duplicate station bus line number and station key");
            if (DataSource.ListStations.FirstOrDefault(s => s.Key == station.StationKey) == null)
                throw new InvalidInformationException($"No station with key {station.StationKey} exists.");
            if (DataSource.ListBusLines.FirstOrDefault(b => b.Key == station.BusLineKey) == null)
                throw new InvalidInformationException($"No Bus Line with key {station.BusLineKey} exists.");
            DataSource.ListBusLineStations.Add(station.Clone());
        }
        public void UpdateBusLineStation(BusLineStation station)
        {
            int index = DataSource.ListBusLineStations.FindIndex(s => s.BusLineKey == station.BusLineKey && s.StationKey == station.StationKey);
            if (index != -1)
                DataSource.ListBusLineStations[index] = station;
            else
                throw new ArgumentNotFoundException($"Bus station of line {station.BusLineKey} and station {station.StationKey} was not found.");
        }
        public void UpdateBusLineStation(int line, int stationKey, Action<BusLineStation> update) //method that knows to updt specific fields in Person
        {
            BusLineStation busLineStation = DataSource.ListBusLineStations.Find(s => s.BusLineKey == line && s.StationKey == stationKey);
            if (busLineStation != null)
                update(busLineStation);
            else
                throw new ArgumentNotFoundException($"Bus station of line {line} and station {stationKey} was not found.");
        }
        public void DeleteBusLineStation(int line, int stationKey)
        {
            BusLineStation busLineStation = DataSource.ListBusLineStations.Find(b => b.BusLineKey == line && b.StationKey == stationKey);
            if (busLineStation == null)
                throw new ArgumentNotFoundException($"Bus station of line {line} and station {stationKey} was not found.");
            DataSource.ListBusLineStations.Remove(busLineStation);
        }
        public void DeleteBusLineStationsByStation(int stationKey)
        {
            DataSource.ListBusLineStations.RemoveAll(bls => bls.StationKey == stationKey);
        }
        public void DeleteBusLineStationsByLine(int lineKey)
        {
            DataSource.ListBusLineStations.RemoveAll(bls => bls.BusLineKey == lineKey);
        }
        #endregion

        #region ConsecutiveStations
        public ConsecutiveStations GetConsecutiveStations(int stationKey1, int stationKey2)
        {
            ConsecutiveStations consecutiveStations = DataSource.ListConsecutiveStations.Find(cs => cs.StationKey1 == stationKey1 && cs.StationKey2 == stationKey2);
            if (consecutiveStations != null)
                return consecutiveStations.Clone();
            else
                throw new ArgumentNotFoundException($"Consecutive stations of first station  {stationKey1} and second station {stationKey2} was not found.");
        }
        public void AddConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            DataSource.ListConsecutiveStations.Add(consecutiveStations.Clone());
        }
        //public void AddConsecutiveStations(int stationKey1, int stationKey2)
        //{
        //    if (stationKey1 == stationKey2)
        //        throw new InvalidInformationException("Duplicate station!");
        //    Station station1 = GetStation(stationKey1);
        //    Station station2 = GetStation(stationKey2);
        //    AddConsecutiveStations(CalculateConsecutiveStations(station1, station2));
        //}
        //ConsecutiveStations CalculateConsecutiveStations(Station station1, Station station2)
        //{
        //    ConsecutiveStations consecutiveStations = new DO.ConsecutiveStations();
        //    consecutiveStations.StationKey1 = station1.Key;
        //    consecutiveStations.StationKey2 = station2.Key;
        //    GeoCoordinate location1 = new GeoCoordinate(station1.Latitude, station1.Longitude);//מיקום התחנה המחושבת
        //    GeoCoordinate location2 = new GeoCoordinate(station2.Latitude, station2.Longitude);//מיקום התחנה הקודמת
        //    double distance = location1.GetDistanceTo(location2);//חישוב מרחק
        //    consecutiveStations.Distance = (int)distance;
        //    Random rand = new Random();
        //    int speed = rand.Next(30, 60);
        //    int time = (int)Math.Ceiling(distance / (speed * 1000 / 60));//חישוב זמן בהנחה שמהירות האוטובוס היא מספר בין 30 - 60 קמ"ש
        //    consecutiveStations.AverageTime = time;
        //    return consecutiveStations;
        //}
        public void UpdateConsecutiveStations(ConsecutiveStations stations)
        {
            int indexOfConsecutiveStationToUpdate = DataSource.ListConsecutiveStations.FindIndex(s => s.StationKey1 == stations.StationKey1 && s.StationKey2 == stations.StationKey2);
            if (indexOfConsecutiveStationToUpdate == -1)
                throw new ArgumentNotFoundException($"Consecutive stations not found with keys: {stations.StationKey1} and {stations.StationKey2}.");
            DataSource.ListConsecutiveStations[indexOfConsecutiveStationToUpdate] = stations;
        }
        public void UpdateConsecutiveStations(int stationKey1, int stationKey2, Action<ConsecutiveStations> update) //method that knows to updt specific fields in Person
        {
            int indexOfConsecutiveStationToUpdate = DataSource.ListConsecutiveStations.FindIndex(s => s.StationKey1 == stationKey1 && s.StationKey2 == stationKey2);
            if (indexOfConsecutiveStationToUpdate == -1)
                throw new ArgumentNotFoundException($"Consecutive stations of first station  {stationKey1} and second station {stationKey2} were not found.");
            ConsecutiveStations consecutiveStations = DataSource.ListConsecutiveStations[indexOfConsecutiveStationToUpdate];
            update(consecutiveStations);
            DataSource.ListConsecutiveStations[indexOfConsecutiveStationToUpdate] = consecutiveStations;
        }
        public void DeleteConsecutiveStations(int stationKey1, int stationKey2)
        {
            ConsecutiveStations consecutiveStations = DataSource.ListConsecutiveStations.Find(s => s.StationKey1 == stationKey1 && s.StationKey2 == stationKey2);
            if (consecutiveStations == null)
                throw new ArgumentNotFoundException($"Consecutive stations of first station  {stationKey1} and second station {stationKey2} was not found.");
            DataSource.ListConsecutiveStations.Remove(consecutiveStations);
        }
        public void DeleteConsecutiveStations(int stationKey)
        {
            try
            {
                GetStation(stationKey);
                DataSource.ListConsecutiveStations.RemoveAll(cs => stationKey == cs.StationKey1 || stationKey == cs.StationKey2);
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region LineSchedule
        public LineSchedule GetLineSchedule(int line, TimeSpan startTime)
        {
            LineSchedule lineSchedule = DataSource.ListLineSchedules.FirstOrDefault(ls => ls.LineKey == line && ls.StartTime == startTime);
            if (lineSchedule == null)
                throw new DO.ArgumentNotFoundException($"Line schedule of line {line} and starting time {startTime.ToString("HH:mm")} not found.");
            return lineSchedule.Clone();
        }
        public IEnumerable<LineSchedule> GetAllLineSchedules()
        {
            var lineSchedules = from ls in DataSource.ListLineSchedules
                                select ls;
            return lineSchedules;
        }
        public IEnumerable<LineSchedule> GetAllLineSchedulesOfLine(int Line)
        {
            var lineSchedules = from ls in DataSource.ListLineSchedules
                                where ls.LineKey == Line
                                select ls;
            return lineSchedules;
        }
        public void AddLineSchedule(LineSchedule lineSchedule)
        {
            if (DataSource.ListLineSchedules.FirstOrDefault(ls => ls.LineKey == lineSchedule.LineKey && ls.StartTime == lineSchedule.StartTime) != null)
                throw new InvalidInformationException($"There is already a line schedule {lineSchedule.LineKey} that start at {lineSchedule.StartTime.ToString()}");
            DataSource.ListLineSchedules.Add(lineSchedule);
        }
        public void UpdateLineSchedule(LineSchedule lineSchedule)
        {
            int indexOfLineScheduleToUpdate = DataSource.ListLineSchedules.FindIndex(ls => ls.LineKey == lineSchedule.LineKey && ls.StartTime.ToString("HH:mm") == lineSchedule.StartTime.ToString("HH:mm"));
            if (indexOfLineScheduleToUpdate == -1)
                throw new ArgumentNotFoundException($"Line schedule of line {lineSchedule.LineKey} and starting time {lineSchedule.StartTime.ToString("HH:mm")} not found.");

            DataSource.ListLineSchedules[indexOfLineScheduleToUpdate] = lineSchedule;
        }
        public void DeleteLineSchedule(int line, TimeSpan startTime)
        {
            LineSchedule lineSchedule = GetLineSchedule(line, startTime);
            DataSource.ListLineSchedules.Remove(lineSchedule);
        }
        #endregion

        #region Station
        public Station GetStation(int stationKey)
        {
            Station station = DataSource.ListStations.Find(s => s.Key == stationKey);
            if (station != null)
                return station.Clone();
            else
                throw new ArgumentNotFoundException($"Station not found with key: {stationKey}");
        }
        public IEnumerable<Station> GetAllStations()
        {
            var AllStations = from station in DataSource.ListStations
                              select station.Clone();
            return AllStations;
        }
        public int AddStation(Station station)
        {
            station.Key = Station.STATION_KEY++;
            DataSource.ListStations.Add(station.Clone());
            return station.Key;
        }
        public void UpdateStation(Station station)
        {
            int indexOfStationToUpdate = DataSource.ListStations.FindIndex(s => s.Key == station.Key);
            if (indexOfStationToUpdate == -1)
                throw new ArgumentNotFoundException($"Station not found with key: {station.Key}");
            DataSource.ListStations[indexOfStationToUpdate] = station;
        }
        public void UpdateStation(int stationKey, Action<Station> update)//method that knows to updt specific fields in Station
        {
            int indexOfStationToUpdate = DataSource.ListStations.FindIndex(s => s.Key == stationKey);
            Station station = DataSource.ListStations.Find(s => s.Key == stationKey);
            if (station == null)
                throw new ArgumentNotFoundException($"Station not found with key: {stationKey}");
            update(station);
            DataSource.ListStations[indexOfStationToUpdate] = station;

        }
        public void DeleteStation(int stationKey)
        {
            Station station = DataSource.ListStations.Find(s => s.Key == stationKey);
            if (station == null)
                throw new ArgumentNotFoundException($"Station: {stationKey} not found!");
            DataSource.ListStations.Remove(station);
        }
        public IEnumerable<int> GetAllLinesInStation(int stationKey)
        {
            var allLines = from bls in DataSource.ListBusLineStations
                           where bls.StationKey == stationKey
                           select bls.BusLineKey;
            return allLines;
        }
        #endregion

        #region User

        public User GetUser(string userName)
        {
            User user = DataSource.ListUsers.Find(s => s.UserName == userName);
            if (user != null)
                return user.Clone();
            else
                throw new ArgumentNotFoundException($"User not found with user name: {userName}");
        }
        public User GetUser(string userName, string password)
        {
            User user = DataSource.ListUsers.Find(s => s.UserName == userName && s.Password == password);
            if (user != null)
                return user.Clone();
            else
                throw new ArgumentNotFoundException($"User not found with user name: {userName}");
        }
        public IEnumerable<User> GetAllUsers()
        {
            var AllUsers = from user in DataSource.ListUsers
                           select user.Clone();
            return AllUsers;
        }
        public void AddUser(User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(s => s.UserName == user.UserName) != null)
                throw new InvalidInformationException("Duplicate user name");
            DataSource.ListUsers.Add(user.Clone());
        }
        public void UpdateUser(User user)
        {
            int indexOfUserToUpdate = DataSource.ListUsers.FindIndex(s => s.UserName == user.UserName);
            if (indexOfUserToUpdate >= 0)
                DataSource.ListUsers[indexOfUserToUpdate] = user;
            else
                throw new ArgumentNotFoundException($"User not found with license number: {user.UserName}");
        }
        public void DeleteUser(string userName)
        {
            User user = DataSource.ListUsers.Find(b => b.UserName == userName);
            if (user == null)
                throw new ArgumentNotFoundException($"User not found with user name: {userName}");
            DataSource.ListUsers.Remove(user);
        }
        #endregion
    }
}