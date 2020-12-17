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
            BusInTravel busInTravel = DataSource.ListBusesInTravel.Find(b => (b.);

            if (per != null)
                return per.Clone();
            else
                throw new BadPersonIdException(id, $"bad person id: {id}");
        }
        IEnumerable<BusInTravel> GetAllBusInTravelsBy(Predicate<BusInTravel> predicate) { return new IEnumerable<BusInTravel>(); }
        IEnumerable<BusInTravel> GetAllBusInTravels() { }
        void AddBusInTravel(BusInTravel busInTravel) { }
        void DeleteBusInTravel(string licenseNumber, int lineKey, int formalTime) { }

        #endregion

        #region BusLine
        IEnumerable<BusLine> GetBusLinesBy(Predicate<BusLine> predicate) { }
        BusLine GetBusLine(int busLineKey) { }
        IEnumerable<BusLine> GetAllBusLines() { }
        void AddBusLine(BusLine bus) { }
        void UpdateBusLine(BusLine bus) { }
        void UpdateBusLine(int busLineKey, Action<BusLine> update){} //method that knows to updt specific fields in BusLine
        void DeleteBusLine(int busLineKey){}

        #endregion

        #region BusLineStation
        BusLineStation GetBusLineStationByKey(int line, int stationKey){}
        IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine){}
        void AddBusLineStation(BusLineStation bus){}
        void UpdateBusLineStation(BusLineStation bus){}
        void UpdateBusLineStation(int line, int stationKey, Action<BusLineStation> update){} //method that knows to updt specific fields in Person
        void DeleteBusLineStation(int line, int stationKey){}
        #endregion

        #region ConsecutiveStations
        ConsecutiveStations GetConsecutiveStations(int stationKey1, int stationKey2){}
        void AddConsecutiveStations(ConsecutiveStations bus){}
        void UpdateConsecutiveStations(ConsecutiveStations bus){}
        void UpdateConsecutiveStations(int stationKey1, int stationKey2, Action<BusLineStation> update){} //method that knows to updt specific fields in Person
        void DeleteConsecutiveStations(int stationKey1, int stationKey2){}
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
        ConsecutiveStations GetStation(int stationKey){}
        IEnumerable<Station> GetAllStations(){}
        void AddStation(Station station){}
        void UpdateStation(Station station){}
        void UpdateStation(int stationKey, Action<BusLineStation> update){} //method that knows to updt specific fields in Person
        void DeleteStation(int stationKey){}
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