using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using DS;

namespace DL
{
    sealed class DLObject : IDL    //internal
    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance property to use
        #endregion

        #region Bus
        IEnumerable<Bus> GetAllBuses()
        {
            var AllBuses = from bus in DataSource.ListBuses
                           select bus.Clone();
            return AllBuses;
        }
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            var AllBusesBy = from bus in DataSource.ListBuses
                             where predicate(bus)
                             select bus.Clone();
            return AllBusesBy;
        }
        Bus GetBus(string LicenseNumber)
        {
            Bus bus = DataSource.ListBuses.Find(b => b.LicenseNumber==LicenseNumber);
            if (bus != null)
                return bus.Clone();
            else
                throw new ArgumentNotFoundException<string>(bus.LicenseNumber, $"Bus not found with license number: {bus.LicenseNumber}");
        }
        void AddBus(Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(p => p.LicenseNumber == bus.LicenseNumber) != null)
                throw new InvalidInformationException<string>(bus.LicenseNumber, "Duplicate bus license number");
            DataSource.ListBuses.Add(bus.Clone());
        }
        void UpdateBus(Bus bus)
        {
            Bus bus1 = DataSource.ListBuses.Find(b => b.LicenseNumber == bus.LicenseNumber);
            if (bus1 != null)
                bus1 = bus;
            else
                throw new ArgumentNotFoundException<string>(bus1.LicenseNumber, $"Bus not found with license number: {bus.LicenseNumber}");
        }
        void UpdateBus(string LicenseNumber, Action<Bus> update) //method that knows to updt specific fields in Bus
        {
            Bus bus = DataSource.ListBuses.Find(b => b.LicenseNumber == LicenseNumber);
            if (bus != null)
                update(bus);
            else
                throw new ArgumentNotFoundException<string>(LicenseNumber, $"Bus not found with license number: {LicenseNumber}");
        }
        void DeleteBus(string LicenseNumber)
        {
            Bus bus = DataSource.ListBuses.Find(b => b.LicenseNumber == LicenseNumber);
            if (bus == null)
                throw new ArgumentNotFoundException<string>(LicenseNumber, $"Bus not found with license number: {LicenseNumber}");
            DataSource.ListBuses.Remove(bus);
        }
        #endregion

        #region BusInTravel
        BusInTravel GetBusInTravel(int key) 
        {
            BusInTravel busInTravel = DataSource.ListBusesInTravel.Find(b => b.Key== key);
            if (busInTravel != null)
                return busInTravel.Clone();
            else
                throw new ArgumentNotFoundException<int>(key, $"Bus in travel with key {key} not found.");
        }
        IEnumerable<BusInTravel> GetAllBusInTravelsBy(Predicate<BusInTravel> predicate) 
        {
            var AllBusesInTravelsBy = from bus in DataSource.ListBusesInTravel
                                      where predicate(bus)
                                      select bus.Clone();
            return AllBusesInTravelsBy;
        }
        IEnumerable<BusInTravel> GetAllBusInTravels()
        {
            var AllBusesInTravels = from bus in DataSource.ListBusesInTravel
                           select bus.Clone();
            return AllBusesInTravels;
        }
        void AddBusInTravel(BusInTravel busInTravel) { }
        void DeleteBusInTravel(string licenseNumber, int lineKey, int formalTime) { }
        void UpdateBusInTravel(BusInTravel bus) { }
        void UpdateBusInTravel(int key, Action<BusInTravel> update) { } //method that knows to updt specific fields in BusInTravel

        #endregion

        #region BusLine
        BusLine GetBusLine(int busLineKey)
        {
            BusLine line = DataSource.ListBusLines.Find(b => b.Key == busLineKey);
            if (line != null)
                return line.Clone();
            else
                throw new ArgumentNotFoundException<int>(busLineKey, $"Bus not found with license number: {busLineKey}");
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

        #region BusLineStation
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

        #region ConsecutiveStations
        ConsecutiveStations GetConsecutiveStations(int stationKey1, int stationKey2)
        {
            ConsecutiveStations consecutiveStations = DataSource.ListConsecutiveStations.Find(cs => cs.StationKey1 == stationKey1 && cs.StationKey2 == stationKey2);
            if (consecutiveStations != null)
                return consecutiveStations.Clone();
            else
                throw new ArgumentNotFoundException<int>(stationKey1, $"Consecutive stations of first station  {stationKey1} and second station {stationKey2} was not found.");
        }
        void AddConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            if (DataSource.ListConsecutiveStations.FirstOrDefault(s => s.StationKey1 == consecutiveStations.StationKey1 && s.StationKey2 == consecutiveStations.StationKey2) != null)
                throw new InvalidInformationException<int>(consecutiveStations.StationKey1, "Duplicate pair of stations");
            DataSource.ListConsecutiveStations.Add(consecutiveStations.Clone());
        }
        void UpdateConsecutiveStations(ConsecutiveStations stations)
        {
            ConsecutiveStations consecutiveStations = DataSource.ListConsecutiveStations.Find(cs => cs.StationKey1 == stations.StationKey1 && cs.StationKey2 == stations.StationKey2);
            if (consecutiveStations != null)
                consecutiveStations = stations;
            else
                throw new ArgumentNotFoundException<int>(stations.StationKey1, $"Consecutive stations of first station  {stations.StationKey1} and second station {stations.StationKey1} were not found.");
        }
        void UpdateConsecutiveStations(int stationKey1, int stationKey2, Action<ConsecutiveStations> update) //method that knows to updt specific fields in Person
        {
            ConsecutiveStations consecutiveStations = DataSource.ListConsecutiveStations.Find(cs => cs.StationKey1 == stationKey1 && cs.StationKey2 == stationKey2);
            if (consecutiveStations != null)
                update(consecutiveStations);
            else
                throw new ArgumentNotFoundException<int>(stationKey1, $"Consecutive stations of first station  {stationKey1} and second station {stationKey2} were not found.");
        }
        void DeleteConsecutiveStations(int stationKey1, int stationKey2)
        {
            ConsecutiveStations consecutiveStations = DataSource.ListConsecutiveStations.Find(s => s.StationKey1 == stationKey1 && s.StationKey2 == stationKey2);
            if (consecutiveStations == null)
                throw new ArgumentNotFoundException<int>(stationKey1, $"Consecutive stations of first station  {stationKey1} and second station {stationKey2} was not found.");
            DataSource.ListConsecutiveStations.Remove(consecutiveStations);
        }
        #endregion

        #region LineSchedule
        LineSchedule GetLineSchedule(int line, int startTime){}
        IEnumerable<LineSchedule> GetAllLineScheduleOfLine(int Line){}
        void AddLineSchedule(LineSchedule lineSchedule){}
        void UpdateLineSchedule(LineSchedule lineSchedule){}
        void UpdateLineSchedule(int line, int startTime, Action<LineSchedule> update){} //method that knows to updt specific fields in Person
        void DeleteLineSchedule(int line, int startTime){}
        #endregion

        #region Station
        Station GetStation(int stationKey)
        {
            Station station = DataSource.ListStations.Find(s => s.Key == stationKey);
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
        void UpdateStation(int stationKey, Action<Station> update){ } //method that knows to updt specific fields in Station
        void DeleteStation(int stationKey){}
        IEnumerable<int> GetAllLinesInStation(int stationKey)
        {
            var allLines = from station in DataSource.ListBusLineStations
                           where station.StationKey == stationKey && station.Position == 1
                           select station.BusLineKey;
            return allLines;
        }
        #endregion

        #region User
        User GetUser(string userName){}
        IEnumerable<User> GetAllUsers(){}
        void AddUser(User user){}
        void UpdateUser(User user){}
        void UpdateUser(string userName, Action<User> update){} //method that knows to updt specific fields in Person
        void DeleteUser(string userName){}
        #endregion

        #region UserTravel
        UserTravel GetUserTravel(int id){}
        IEnumerable<UserTravel> GetAllUserTravels(){}
        void AddUserTravel(UserTravel user){}
        void DeleteUserTravel(int id){}
        #endregion
    }
}