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
        BusInTravel GetBusInTravel(int key);
        IEnumerable<BusInTravel> GetAllBusInTravelsBy(Predicate<BusInTravel> predicate);
        IEnumerable<BusInTravel> GetAllBusInTravels();
        void AddBusInTravel(BusInTravel busInTravel);
        void DeleteBusInTravel(int key);
        void UpdateBusInTravel(BusInTravel bus);
        void UpdateBusInTravel(int key, Action<BusInTravel> update); //method that knows to updt specific fields in BusInTravel


        #endregion

        #region BusLine
        IEnumerable<BusLine> GetBusLinesBy(Predicate<BusLine> predicate);
        BusLine GetBusLine(int busLineKey );
        IEnumerable<BusLine> GetAllBusLines();
        void AddBusLine(BusLine bus);
        void UpdateBusLine(BusLine bus);
        void UpdateBusLine(int busLineKey, Action<BusLine> update); //method that knows to updt specific fields in BusLine
        void DeleteBusLine(int busLineKey);

        #endregion

        #region BusLineStation
        BusLineStation GetBusLineStationBy(Predicate<BusLineStation> predicate);
        BusLineStation GetBusLineStationByKey( int line, int stationKey);
        IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine);
        void AddBusLineStation(BusLineStation bus);
        void UpdateBusLineStation(BusLineStation bus);
        void UpdateBusLineStation(int line, int stationKey, Action<BusLineStation> update); //method that knows to updt specific fields in Person
        void DeleteBusLineStation(int line, int stationKey);
        void DeleteBusLineStationsByStation(int stationKey);
        #endregion

        #region ConsecutiveStations
        ConsecutiveStations GetConsecutiveStations(int stationKey1, int stationKey2);
        void AddConsecutiveStations(ConsecutiveStations bus);
        void UpdateConsecutiveStations(ConsecutiveStations bus);
        void UpdateConsecutiveStations(int stationKey1, int stationKey2, Action<BusLineStation> update); //method that knows to updt specific fields in Person
        void DeleteConsecutiveStations(int stationKey1, int stationKey2);
        #endregion

        #region LineSchedule
        LineSchedule GetLineSchedule(int line, int startTime);
        IEnumerable<LineSchedule> GetAllLineScheduleOfLine(int Line);
        void AddLineSchedule(LineSchedule lineSchedule);
        void UpdateLineSchedule(LineSchedule lineSchedule);
        void UpdateLineSchedule(int line, int startTime, Action<LineSchedule> update); //method that knows to updt specific fields in Person
        void DeleteLineSchedule(int line, int startTime);
        #endregion

        #region Station
        Station GetStation(int stationKey);
        IEnumerable<Station> GetAllStations();
        void AddStation(Station station);
        void UpdateStation(Station station);
        void UpdateStation(int stationKey, Action<BusLineStation> update); //method that knows to updt specific fields in Person
        void DeleteStation(int stationKey);
        IEnumerable<int> GetAllLinesInStation(int busLineKey);
        #endregion

        #region User
        User GetUser(string userName);
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void UpdateUser(string userName, Action<User> update); //method that knows to updt specific fields in Person
        void DeleteUser(string userName);
        #endregion

        #region UserTravel
        UserTravel GetUserTravel(int id);
        IEnumerable<UserTravel> GetAllUserTravels();
        void AddUserTravel(UserTravel user);
        void DeleteUserTravel(int id);
        #endregion
    }
}