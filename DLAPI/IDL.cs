using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DLAPI
{
    //CRUD Logic
    public interface IDL
    {
        #region Bus
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        Bus GetBus(string LicenseNumber);
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void UpdateBus(string LicenseNumber, Action<Bus> update); //method that knows to updt specific fields in Person
        void DeleteBus(string LicenseNumber);
        #endregion

        #region BusInTravel
        //BusInTravel GetBusInTravel(int key);
        //IEnumerable<BusInTravel> GetAllBusInTravelsBy(Predicate<BusInTravel> predicate);
        //IEnumerable<BusInTravel> GetAllBusInTravels();
        //void AddBusInTravel(BusInTravel busInTravel);
        //void DeleteBusInTravel(string licenseNumber, int lineKey, int formalTime);
        //void UpdateBusInTravel(BusInTravel bus);
        //void UpdateBusInTravel(int key, Action<BusInTravel> update); //method that knows to updt specific fields in BusInTravel
        #endregion

        #region BusLine
        IEnumerable<BusLine> GetBusLinesBy(Predicate<BusLine> predicate);
        BusLine GetBusLine(int busLineKey );
        IEnumerable<BusLine> GetAllBusLines();
        int AddBusLine(BusLine bus);
        void UpdateBusLine(BusLine bus);
        void UpdateBusLine(int busLineKey, Action<BusLine> update); //method that knows to updt specific fields in BusLine
        void DeleteBusLine(int busLineKey);

        #endregion

        #region BusLineStation
        BusLineStation GetBusLineStationBy(Predicate<BusLineStation> predicate);
        BusLineStation GetBusLineStationByKey( int line, int stationKey);
        IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine);
        IEnumerable<BusLineStation> GetAllBusLineStations();
        IEnumerable<BusLineStation> GetAllBusLineStationsBy(Predicate<BusLineStation> predicate);
        void AddBusLineStation(BusLineStation bus);
        void UpdateBusLineStation(BusLineStation bus);
        void UpdateBusLineStation(int line, int stationKey, Action<BusLineStation> update); //method that knows to updt specific fields in Person
        void DeleteBusLineStation(int line, int stationKey);
        void DeleteBusLineStationsByStation(int stationKey);
        void DeleteBusLineStationsByLine(int lineKey);
        #endregion

        #region ConsecutiveStations
        ConsecutiveStations GetConsecutiveStations(int stationKey1, int stationKey2);
        void AddConsecutiveStations(ConsecutiveStations consecutiveStations);
        void AddConsecutiveStations(int stationKey1, int stationKey2);
        void UpdateConsecutiveStations(ConsecutiveStations bus);
        void UpdateConsecutiveStations(int stationKey1, int stationKey2, Action<ConsecutiveStations> update); //method that knows to updt specific fields in Person
        void DeleteConsecutiveStations(int stationKey1, int stationKey2);
        void DeleteConsecutiveStations(int stationKey);//delete all consecutive stations with the given key
        #endregion

        #region Station
        Station GetStation(int stationKey);
        IEnumerable<Station> GetAllStations();
        int AddStation(Station station);
        void UpdateStation(Station station);
        void UpdateStation(int stationKey, Action<Station> update); //method that knows to updt specific fields in Person
        void DeleteStation(int stationKey);
        IEnumerable<int> GetAllLinesInStation(int busLineKey);
        #endregion

        #region LineSchedule
        LineSchedule GetLineSchedule(int line, TimeSpan startTime);
        IEnumerable<LineSchedule> GetAllLineSchedules();
        IEnumerable<LineSchedule> GetAllLineSchedulesOfLine(int Line);
        void AddLineSchedule(LineSchedule lineSchedule);
        void UpdateLineSchedule(LineSchedule lineSchedule);
        void UpdateLineSchedule(int line, TimeSpan startTime, Action<LineSchedule> update); //method that knows to updt specific fields in Person
        void DeleteLineSchedule(int line, TimeSpan startTime);
        #endregion

        #region User
        User GetUser(string userName);
        User GetUser(string userName, string password);
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void UpdateUser(string userName, Action<User> update); //method that knows to updt specific fields in Person
        void DeleteUser(string userName);
        #endregion
    }
}