using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLAPI
{
    public interface IBL
    {
        #region Simulator
        void StartSimulator(Clock simulatorClock, TimeSpan startTime, int simulatorRate, int Key);
        void StopSimulator();
        #endregion

        #region Bus
        /// <summary>
        /// קבלת כל האוטובוסים ממויינים על פי שדה
        /// </summary>
        /// <param name="orderBy">השדה</param>
        /// <returns></returns>
        IEnumerable<Bus> GetAllBusesOrderedBy(string orderBy);
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
        ///BL לשכבת DL ממיר קו אוטובוס משכבת
        /// </summary>
        /// <param name="BusLineDO">DL קו אוטובוס</param>
        /// <returns>BL קו אוטובוס</returns>
        BO.BusLine BusLineDoBoAdapter(DO.BusLine BusLineDO);
        /// <summary>
        /// מיון כל הקווים עך פי שדה מסויים
        /// </summary>
        /// <param name="orderBy">השדה</param>
        /// <returns>האוסף ממויין</returns>
        IEnumerable<BusLine> GetAllBusLinesOrderedBy(string orderBy);
        /// <summary>
        /// קבלת התחנה הפיזית הקודמת לתחנה מסויימת בקו
        /// </summary>
        /// <param name="lineStationKey">מפתח תחנה</param>
        /// <param name="position">מיקום בקו</param>
        /// <returns>תחנה פיזית</returns>
        Station GetPreviouseStation(int lineStationKey, int position);
        /// <summary>
        /// קבלת תחנת קו ע"פ מפתח קו ומיקום
        /// </summary>
        /// <param name="busKey">מפתח קו</param>
        /// <param name="Position">מיקום בקו</param>
        /// <returns>תחנת קו אוטובוס</returns>
        BusLineStation GetBusLineStation(int busKey, int Position);
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
        BusLine GetBusLine(int busLineKey);
        /// <summary>
        /// הוספת קו למאגר הנתונים
        /// </summary>
        /// <param name="bus"></param>
        /// <returns>מפתח שהתקבל על פי מספר רץ</returns>
        int AddBusLine(BusLine bus);
        /// <summary>
        /// הוספת קו אוטובוס למאגר הנתונים יחד עם 2 תחנות התחלתיות
        /// </summary>
        /// <param name="bus">קו אוטובוס</param>
        /// <param name="stationKey1">תחנה ראשונה בקו</param>
        /// <param name="stationKey2">תחנה שנייה בקו</param>
        /// <returns>מפתח הקו שהתקבל ע"פ מספר רץ</returns>
        int AddBusLine(BusLine bus, int stationKey1, int stationKey2);
        /// <summary>
        /// הוספת תחנת קו לקו אוטובוס
        /// </summary>
        /// <param name="busLineKey">מפתח קו</param>
        /// <param name="stationKey"><מפתח תחנה/param>
        /// <param name="position">מיקום בקו</param>
        void AddStationToLine(int busLineKey, int stationKey, int position = 0);
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
        /// <summary>
        /// מחיקת תחנת קו מקו אוטובוס
        /// </summary>
        /// <param name="busKey">מפתח קו</param>
        /// <param name="stationKey">מפתח תחנה</param>
        void DeleteStationFromLine(int busKey, int stationKey);
        /// <summary>
        /// קבלת מחרוזת לייצוג קו אוטובוס בפלט
        /// (לשימוש בקונסול)
        /// </summary>
        /// <param name="b">קו אוטובוס</param>
        /// <returns>מחרוזת פלט לייצוג הקו</returns>
        String ToStringBusLine(BusLine b);
        #endregion

        #region BusLineStation
        /// <summary>
        ///BL לשכבת DL ממיר של תחנת קו אוטובוס משכבת
        /// </summary>
        /// <param name="BusLineStationDO">DL תחנת קו</param>
        /// <returns>BL תחנת קו</returns>
        BO.BusLineStation BusLineStationDoBoAdapter(DO.BusLineStation BusLineStationDO);
        /// <summary>
        /// קבלת תחנת קו ע"פ מפתח
        /// </summary>
        /// <param name="line">מפתח קו</param>
        /// <param name="stationKey">מפתח תחנה</param>
        /// <returns>תחנת קו</returns>
        BusLineStation GetBusLineStationByKey(int line, int stationKey);
        /// <summary>
        /// קבלת כל תחנות הקו של קו מסויים
        /// </summary>
        /// <param name="busLine"></param>
        /// <returns></returns>
        IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine);
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
        /// מחיקת תחנה קו ממאגר הנתונים
        /// </summary>
        /// <param name="line">מפתח קו</param>
        /// <param name="stationKey">מפתח תחנה</param>
        void DeleteBusLineStation(int line, int stationKey);
        /// <summary>
        ///  קבלת מחרוזת לייצוג תחנת קו
        /// </summary>
        /// <param name="bls">תחנת קו</param>
        /// <returns>מחרוזת הפלט לייצוג תחנת קו</returns>
        string ToStringBusLineStation(BusLineStation bls);
        #endregion

        #region Station
        /// <summary>
        ///  BL לשכבת DL ממיר של תחנה אוטובוס משכבת
        /// </summary>
        /// <param name="StationDO">DL תחנה</param>
        /// <returns>BL תחנה</returns>
        BO.Station StationDoBoAdapter(DO.Station StationDO);
        /// <summary>
        /// קבלת כל התחנות ממויינות ע"פ שדה מסויים
        /// </summary>
        /// <param name="orderBy">השדה</param>
        /// <returns>אוסף ממויין</returns>
        IEnumerable<Station> GetAllStationsOrderedBy(string orderBy);
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
        void UpdateStation(int stationKey, Action<Station> update); //method that knows to updt specific fields in Person
        /// <summary>
        /// מחיקת תחנה ממאגר הנתונים
        /// </summary>
        /// <param name="stationKey">מפתח התחנה</param>
        void DeleteStation(int stationKey);
        /// <summary>
        /// ייצוג תחנה ע"י מחרוזת
        /// (לשימוש בקונסול)
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        string ToStringStation(Station station);
        #endregion

        #region LineSchedule
        /// <summary>
        ///  BL לשכבת DL ממיר של יציאת קו משכבת
        /// </summary>
        /// <param name="StationDO">DL יציאת קו</param>
        /// <returns>BL יציאת קו</returns>
        BO.LineSchedule LineScheduleDoBoAdapter(DO.LineSchedule LineScheduleDO);
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
        /// <summary>
        /// מחיקת יציאת קו ממאגר הנתונים
        /// </summary>
        /// <param name="lineKey"></param>
        /// <param name="startTime"></param>
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

        #region BusInTravel
        /// <summary>
        /// קבלת כול הנסיעות של קווים שיצאו בהתאם לשעת יציאה
        /// </summary>
        /// <param name="stationKey">מפתח התחנה</param>
        /// <param name="startTime">שעת יציאה</param>
        /// <param name="latePrecentage">אחוז איחור של הקו</param>
        /// <returns>אוסף של נסיעות קווים</returns>
        IEnumerable<BusInTravel> GetLineTimingsPerStation(int stationKey, TimeSpan startTime, double latePrecentage);
        #endregion

        #region RoutsAndArrivelTimes
        /// <summary>
        /// קבלת כל הקווים שמגיעים מתחנת מוצא לתחנת יעד
        /// </summary>
        /// <param name="s1">תחנת מוצא</param>
        /// <param name="s2">תחנת יעד</param>
        /// <returns>אוסף קווי אוטובוס</returns>
        IEnumerable<BusLine> FindRoutes(Station s1, Station s2);
        /// <summary>
        /// קבלת כל זמני ההגעה של קו מסויים בין 2 תחנות
        /// </summary>
        /// <param name="lineKey">מפתח הקו</param>
        /// <param name="s1">תחנת מוצא</param>
        /// <param name="s2">תחנת יעד</param>
        /// <returns>אוסף זמני הגעה</returns>
        IEnumerable<ArrivalTimes> GetArrivalTimes(int lineKey, int s1, int s2);
        #endregion
    }
}
