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
        /// <summary>
        /// קבלת כל האוטובוסים על ידי שימוש בשאילתה
        /// </summary>
        /// <returns>אוסף של אוטובוסים IEnamurable<Bus></returns>
        IEnumerable<Bus> GetAllBuses();
        /// <summary>
        /// כל האוטובטסים העונים על תנאי
        /// </summary>
        /// <param name="predicate">תנאי</param>
        /// <returns>אוסף של אוטובוסים IEnamurable<Bus></returns>
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        /// <summary>
        /// אוטובוס על פי מפתח
        /// </summary>
        /// <param name="LicenseNumber">מספר רישוי - מפתח</param>
        /// <returns>Bus</returns>
        Bus GetBus(string LicenseNumber);
        /// <summary>
        /// הוספת אוטובוס למאגר הנתונים
        /// </summary>
        /// <param name="bus"></param>
        void AddBus(Bus bus);
        /// <summary>
        /// עדכון אוטובוס על ידי החלפת האובייקט כולו
        /// </summary>
        /// <param name="bus"></param>
        void UpdateBus(Bus bus);
        /// <summary>
        /// עדכון אוטובוס על ידי פונקציה
        /// </summary>
        /// <param name="LicenseNumber"></param>
        /// <param name="update"></param>
        void UpdateBus(string LicenseNumber, Action<Bus> update); 
        /// <summary>
        /// מחיקת אוטובוס ממאגר הנתונים
        /// </summary>
        /// <param name="LicenseNumber"></param>
        void DeleteBus(string LicenseNumber);
        #endregion

        #region BusLine
        /// <summary>
        /// קבלת כל הקווים על ידי שימוש בשאילתה
        /// </summary>
        /// <returns>אוסף של קווי אוטובוס IEnamurable<BusLine></returns>
        IEnumerable<BusLine> GetAllBusLines();
        /// <summary>
        /// כל הקווים העונים על תנאי
        /// </summary>
        /// <param name="predicate">תנאי</param>
        /// <returns>אוסף של קווי אוטובוס IEnamurable<BusLine></returns>
        IEnumerable<BusLine> GetBusLinesBy(Predicate<BusLine> predicate);
        /// <summary>
        /// קבלת קו על פי מפתח
        /// </summary>
        /// <param name="busLineKey"></param>
        /// <returns>מפתח קו</returns>
        BusLine GetBusLine(int busLineKey );
        /// <summary>
        /// הוספת קו למאגר הנתונים
        /// </summary>
        /// <param name="bus"></param>
        /// <returns>מפתח שהתקבל על פי מספר רץ</returns>
        int AddBusLine(BusLine bus);
        /// <summary>
        /// עדכון קו אוטובוס ע"י החלפת האובייקט כולו
        /// </summary>
        /// <param name="bus"></param>
        void UpdateBusLine(BusLine bus);
        /// <summary>
        /// עדכון קו אוטובוס ע"י פונקציה
        /// </summary>
        /// <param name="busLineKey">מפתח הקו</param>
        /// <param name="update">הדלגט</param>
        void UpdateBusLine(int busLineKey, Action<BusLine> update); //method that knows to updt specific fields in BusLine
       /// <summary>
       /// מחיקת קו ממאגר הנתונים
       /// </summary>
       /// <param name="busLineKey"></param>
        void DeleteBusLine(int busLineKey);

        #endregion

        #region BusLineStation
        /// <summary>
        /// קבלת תחנת קו ע"פ תנאי
        /// </summary>
        /// <param name="predicate">תנאי</param>
        /// <returns>תחנת קו</returns>
        BusLineStation GetBusLineStationBy(Predicate<BusLineStation> predicate);
        /// <summary>
        /// קבלת תחנת קו ע"פ מפתח
        /// </summary>
        /// <param name="line">מפתח קו</param>
        /// <param name="stationKey">מפתח תחנה</param>
        /// <returns>תחנת קו</returns>
        BusLineStation GetBusLineStationByKey( int line, int stationKey);
        /// <summary>
        /// קבלת כל תחנות הקו של קו מסויים
        /// </summary>
        /// <param name="busLine"></param>
        /// <returns></returns>
        IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine);
        /// <summary>
        /// קבלת כל תחנות הקו במאגר הנתונים
        /// </summary>
        /// <returns>אוסף תחנות קו</returns>
        IEnumerable<BusLineStation> GetAllBusLineStations();
        /// <summary>
        /// קבלת כל תחנותת הקו שעונות על תנאי
        /// </summary>
        /// <param name="predicate">תנאי</param>
        /// <returns>אוסף תחנות קו</returns>
        IEnumerable<BusLineStation> GetAllBusLineStationsBy(Predicate<BusLineStation> predicate);
        /// <summary>
       /// הוספת תחנת קו למאגר הנתונים
       /// </summary>
       /// <param name="bus">תחנת קו להוספה</param>
        void AddBusLineStation(BusLineStation bus);
        /// <summary>
        /// עדכון תחנת קו ע"י החלפת האובייקט כולו
        /// </summary>
        /// <param name="bus"></param>
        void UpdateBusLineStation(BusLineStation bus);
        /// <summary>
        /// עדכון תחנת קו ע"י פונקציה
        /// </summary>
        /// <param name="line">מפתח קו</param>
        /// <param name="stationKey">מפתח תחנה</param>
        /// <param name="update">דלגט עדכון</param>
        void UpdateBusLineStation(int line, int stationKey, Action<BusLineStation> update); //method that knows to updt specific fields in Person
       /// <summary>
       /// מחיקת תחנה ממאגר הנתונים
       /// </summary>
       /// <param name="line">מפתח קו</param>
       /// <param name="stationKey">מפתח תחנה</param>
        void DeleteBusLineStation(int line, int stationKey);
        /// <summary>
        /// מחיקת כל תחנות הקו בעלות מפתח תחנה מסויים
        /// </summary>
        /// <param name="stationKey">מפתח התחנה</param>
        void DeleteBusLineStationsByStation(int stationKey);
        /// <summary>
        /// מחיקת כל תחנות הקו של קו מסויים
        /// </summary>
        /// <param name="lineKey">מפתח הקו</param>
        void DeleteBusLineStationsByLine(int lineKey);
        #endregion

        #region ConsecutiveStations
        /// <summary>
        /// קבלת תחנות עוקבות ע"י מפתח
        /// </summary>
        /// <param name="stationKey1">מפתח תחנה ראשונה</param>
        /// <param name="stationKey2">מפתח תחנה שנייה</param>
        /// <returns>תחנות עוקבות</returns>
        ConsecutiveStations GetConsecutiveStations(int stationKey1, int stationKey2);
        /// <summary>
        /// הוספת תחנות עוקבות למאגר הנתונים
        /// </summary>
        /// <param name="consecutiveStations">תחנות עוקבות להוספה</param>
        void AddConsecutiveStations(ConsecutiveStations consecutiveStations);
        /// <summary>
        /// עדכון תחנות עוקבות ע"י החלפת האובייקט כולו
        /// </summary>
        /// <param name="bus">תחנת הקו המעודכנת</param>
        void UpdateConsecutiveStations(ConsecutiveStations bus);
        /// <summary>
        /// עדכון תחנות עוקבות ע"י פונקציה
        /// </summary>
        /// <param name="stationKey1">מפתח תחנה ראשונה</param>
        /// <param name="stationKey2">מפתח תחנה שנייה</param>
        /// <param name="update">דלגט עדכון</param>
        void UpdateConsecutiveStations(int stationKey1, int stationKey2, Action<ConsecutiveStations> update); //method that knows to updt specific fields in Person
        /// <summary>
        /// מחיקת תחנות עוקבות ממאגר הנתונים ע"פ מפתח מלא
        /// </summary>
        /// <param name="stationKey1">מפתח תחנה ראשונה</param>
        /// <param name="stationKey2">מפתח תחנה שנייה</param>
        void DeleteConsecutiveStations(int stationKey1, int stationKey2);
        /// <summary>
        /// מחיקת תחנות עוקבות המכילות מפתח תחנה מסויים
        /// </summary>
        /// <param name="stationKey">מפתח נתחנה</param>
        void DeleteConsecutiveStations(int stationKey);//delete all consecutive stations with the given key
        #endregion

        #region Station
        /// <summary>
        /// קבלת תחנה ע"פ מפתח
        /// </summary>
        /// <param name="stationKey">מפתח התחנה</param>
        /// <returns>התחנה המתקבלת</returns>
        Station GetStation(int stationKey);
        /// <summary>
        /// קבלת כל התחנות
        /// </summary>
        /// <returns>אוסף תחנות IEnamurable<Station></returns>
        IEnumerable<Station> GetAllStations();
        /// <summary>
        /// הוספת תחנה למאגר הנתונים
        /// </summary>
        /// <param name="station">תחנה להוספה</param>
        /// <returns>מפתח התחנה שהתקבל על ידי מספר רץ</returns>
        int AddStation(Station station);
        /// <summary>
        /// עדכון תחנה ע"י החלפת האובייקט כולו
        /// </summary>
        /// <param name="station">התחנה המעודכנת</param>
        void UpdateStation(Station station);
        /// <summary>
        /// עדכון תחנה ע"י פונקציה
        /// </summary>
        /// <param name="stationKey">מפתח התחנה</param>
        /// <param name="update">דלגט עדכון</param>
        void UpdateStation(int stationKey, Action<Station> update); 
        /// <summary>
        /// מחיקת תחנה ממאגר הנתונים
        /// </summary>
        /// <param name="stationKey">מפתח התחנה</param>
        void DeleteStation(int stationKey);
        /// <summary>
        /// קבלת כול הקווים שעוברים בתחנה מסויימת
        /// </summary>
        /// <param name="busLineKey">מפתח התחנה</param>
        /// <returns>אוסף מפתחות הקוים שעוברים בתחנה</returns>
        IEnumerable<int> GetAllLinesInStation(int busLineKey);
        #endregion

        #region LineSchedule
        /// <summary>
        /// קבלת יציאת קו ע"י מפתח
        /// </summary>
        /// <param name="line">מפתח הקו</param>
        /// <param name="startTime">שעת יציאה</param>
        /// <returns>יציאת קו</returns>
        LineSchedule GetLineSchedule(int line, TimeSpan startTime);
        /// <summary>
        /// קבלת כל יציאות הקווים
        /// </summary>
        /// <returns>אוסף יציאות קווים</returns>
        IEnumerable<LineSchedule> GetAllLineSchedules();
        /// <summary>
        /// קבלת כל יציאות הקווים של קו מסויים
        /// </summary>
        /// <param name="Line">מפתח הקו</param>
        /// <returns>אוסף יציאות קווים</returns>
        IEnumerable<LineSchedule> GetAllLineSchedulesOfLine(int Line);
        /// <summary>
        /// הוספת יציאת קו למאגר הנתונים
        /// </summary>
        /// <param name="lineSchedule">יציאת הקו להוספה</param>
        void AddLineSchedule(LineSchedule lineSchedule);
        /// <summary>
        /// עדכון יציאת קו ע"י החלפת האובייקט כולו
        /// </summary>
        /// <param name="lineSchedule">האובייקט המעודכן</param>
        void UpdateLineSchedule(LineSchedule lineSchedule);
        void DeleteLineSchedule(int lineKey, TimeSpan startTime);
        #endregion

        #region User
        /// <summary>
        /// קבלת פרטי משתמש על פי שם משתמש
        /// </summary>
        /// <param name="userName">שם משתמש</param>
        /// <returns>המשתמש</returns>
        User GetUser(string userName);
        /// <summary>
        /// קבלת פרטי משתמש ע"פ שם משתמש וסיסמא
        /// </summary>
        /// <param name="userName">שם משתמש</param>
        /// <param name="password">סיסמא</param>
        /// <returns>המשתמש</returns>
        User GetUser(string userName, string password);
        /// <summary>
        /// קבלת כל המשתמשים
        /// </summary>
        /// <returns>אוסף משתמשים</returns>
        IEnumerable<User> GetAllUsers();
        /// <summary>
        /// הוספת משתמש למאגר הנתונים
        /// </summary>
        /// <param name="user">משתמש להוספה</param>
        void AddUser(User user);
        /// <summary>
        /// עדכון משתמש ע"י החלפת האובייקט כולו
        /// </summary>
        /// <param name="user">משתמש מעודכן</param>
        void UpdateUser(User user);
        /// <summary>
        /// מחיקת משתמש ממאגר הנתונים
        /// </summary>
        /// <param name="userName">המשתמש</param>
        void DeleteUser(string userName);
        #endregion
    }
}