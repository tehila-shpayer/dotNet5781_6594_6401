using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics.Eventing.Reader;
using System.Device.Location;
using System.Linq.Expressions;
using DO;


namespace DS
{
    public static class DataSource
    {
        public static List<Bus> ListBuses;
        //public static List<BusInTravel> ListBusesInTravel;
        public static List<Station> ListStations;
        public static List<BusLine> ListBusLines;
        public static List<BusLineStation> ListBusLineStations;
        public static List<ConsecutiveStations> ListConsecutiveStations;
        public static List<LineSchedule> ListLineSchedules;
        public static List<User> ListUsers;
        //public static List<User> ListUserTravel;

        static DataSource()
        {
            InitAllLists();
        }
        static int GetDistance(double x1, double y1, double x2, double y2)
        {
            GeoCoordinate s1 = new GeoCoordinate(x1, y1);//מיקום תחנה 1
            GeoCoordinate s2 = new GeoCoordinate(x2, y2);//מיקום תחנה 2
            return Convert.ToInt32(s1.GetDistanceTo(s2) * 1.3 + 1);//חישוב מרחק
        }
        static int GetDistance(int stationKey1, int stationKey2)
        {
            Station station1 = ListStations.Find(s => s.Key == stationKey1);
            Station station2 = ListStations.Find(s => s.Key == stationKey2);
            GeoCoordinate s1 = new GeoCoordinate(station1.Latitude, station1.Longitude);//מיקום תחנה 1
            GeoCoordinate s2 = new GeoCoordinate(station2.Latitude, station2.Longitude);//מיקום תחנה 2
            return Convert.ToInt32( s1.GetDistanceTo(s2) * 1.3 + 1);//חישוב מרחק
        }
        static int GetTime(double distance)
        {
            Random rand = new Random();
            int speed = 50;
            int time = Convert.ToInt32(distance / (speed * 1000 / 60));//חישוב זמן בהנחה שמהירות האוטובוס היא מספר בין 30 - 60 קמ"ש
            return time;
        }
        static int GetTime(int stationKey1, int stationKey2)
        {
            return GetTime(GetDistance(stationKey1, stationKey2));
        }
        static void InitAllLists()
        {
            ListBuses = new List<Bus>
            {
                new Bus
                {
                    LicenseNumber = "7106301", RunningDate = new DateTime(2003, 1, 15), LastTreatment = new DateTime(2020, 12, 15),
                    Fuel = 790, KM = 18085, BeforeTreatKM = 300, Status = Status.Ready, IsActive = true
                },
                new Bus
                {
                    LicenseNumber = "86613936", RunningDate = new DateTime(2019, 8, 22), LastTreatment = new DateTime(2020, 12, 17),
                    Fuel = 951, KM = 9104, BeforeTreatKM = 45, Status = Status.Ready, IsActive = true
                },
                new Bus
                {
                    LicenseNumber = "77905516", RunningDate = new DateTime(2020, 4, 2), LastTreatment = new DateTime(2021, 1, 4),
                    Fuel = 497, KM = 31514, BeforeTreatKM = 14732, Status = Status.Ready, IsActive = true
                },
                new Bus
                {
                    LicenseNumber = "3413956", RunningDate = new DateTime(1989, 7, 18), LastTreatment = new DateTime(2020, 7, 25),
                    Fuel = 865, KM = 27591, BeforeTreatKM = 2161, Status = Status.Ready, IsActive = true
                },
                new Bus
                {
                    LicenseNumber = "4158077", RunningDate = new DateTime(2003, 11, 3), LastTreatment = new DateTime(2019, 6, 13),
                    Fuel = 487, KM = 81937, BeforeTreatKM = 5632, Status = Status.Ready, IsActive = true
                },
                new Bus
                {
                    LicenseNumber = "3529843", RunningDate = new DateTime(2012, 3, 10), LastTreatment = new DateTime(2020, 8, 21),
                    Fuel = 1152, KM = 6473, BeforeTreatKM = 3412, Status = Status.Ready, IsActive = true
                },
                new Bus
                {
                    LicenseNumber = "41962883", RunningDate = new DateTime(2019, 2, 19), LastTreatment = new DateTime(2021, 1, 2),
                    Fuel = 742, KM = 64390, BeforeTreatKM = 17245, Status = Status.Ready, IsActive = true
                },
                new Bus
                {
                    LicenseNumber = "6803734", RunningDate = new DateTime(2017, 11, 9), LastTreatment = new DateTime(2019, 12, 28),
                    Fuel = 100, KM = 22379, BeforeTreatKM = 13728, Status = Status.Ready, IsActive = true
                },
                new Bus
                {
                    LicenseNumber = "9392463", RunningDate = new DateTime(2007, 8, 16), LastTreatment = new DateTime(2020, 3, 26),
                    Fuel = 1048, KM = 6473, BeforeTreatKM = 2894, Status = Status.Ready, IsActive = true
                },
                new Bus
                {
                    LicenseNumber = "18058327", RunningDate = new DateTime(2018, 5, 15), LastTreatment = new DateTime(2020, 7, 1),
                    Fuel = 654, KM = 46739, BeforeTreatKM = 19367, Status = Status.Ready, IsActive = true
                }
            };

            ListStations = new List<Station>
            { 
                //10
                new Station { Key = 40247, Name = "צהל/האקליפטוס", Latitude = 32.439432, Longitude =34.930418 },
                new Station { Key = 40249, Name = "צומת בנימינה", Latitude = 32.518144, Longitude =34.930064},
                new Station { Key = 40252, Name = "צהל/משרדי חברת חשמל", Latitude = 32.440952, Longitude =34.931872},
                new Station { Key = 40253, Name = "צומת מיכאל מעגן", Latitude = 32.557385, Longitude =34.93226},
                new Station { Key = 40257, Name = "יפו/זיסו", Latitude = 32.827351, Longitude =34.987691},
                new Station { Key = 40258, Name = "הנדיב/דרך העצמאות", Latitude = 32.52383, Longitude =34.934878},
                new Station { Key = 40260, Name = "נורית/דרך העצמאות", Latitude = 32.527128, Longitude =34.935506},
                new Station { Key = 40262, Name = "נחל צין/חטיבת הנחל", Latitude = 32.431123 , Longitude =34.936463},
                new Station { Key = 40267, Name = "משטרה/שדרות נילי", Latitude = 32.571946, Longitude =34.938368},
                new Station { Key = 40269, Name = "דרך העצמאות/אירית", Latitude = 32.522086, Longitude =34.938185},

                //10
                new Station { Key = 21007, Name = "שלגיה/צומת תפוח", Latitude=32.560122, Longitude = 34.908777},
                new Station { Key = 21008, Name = "שלגיה/בית הגמדים", Latitude=32.562344, Longitude = 34.90215},
                new Station { Key = 24290, Name = "אגרבה/ארמון הקיסר", Latitude=32.801436, Longitude = 34.993051},
                new Station { Key = 27121, Name = "קינג קרוס/רציף 9 ושלושה רבעים", Latitude = 32.796435, Longitude = 35.531956},
                new Station { Key = 27126, Name = "אזקבן/הסוהרסנים", Latitude= 32.794848, Longitude = 35.029825},
                new Station { Key = 28352, Name = "אליס/ארץ הפלאות", Latitude= 32.437864, Longitude = 34.92632},
                new Station { Key = 28353, Name = "מוזיאון הכובעים/ארץ הפלאות", Latitude= 32.435004, Longitude = 34.91982},
                new Station { Key = 20996, Name = "קמלוט/המלך ארתור", Latitude= 32.564004, Longitude = 34.12930},
                new Station { Key = 20011, Name = "ליליפוט/בלפוסקו", Latitude= 32.008004, Longitude = 35.06983},
                new Station { Key = 22222, Name = "אנדר ויגין/לוזיטניה", Latitude= 32.400000, Longitude = 34.900000},

                //13
                new Station { Key = 40912, Name = "כצנלסון/אלכסנדר זייד", Latitude = 32.707282, Longitude =35.123845},
                new Station { Key = 40913, Name = "כיכר הציונות", Latitude = 32.711688, Longitude =35.125015},
                new Station { Key = 40914, Name = "חנה סנש/יזרעאל", Latitude = 32.710934, Longitude =35.129506},
                new Station { Key = 40915, Name = "חנה סנש/בן צבי", Latitude = 32.711127, Longitude =35.12738},
                new Station { Key = 40916, Name = "צומת קרית טבעון/האלונים", Latitude = 32.712736, Longitude =35.125069},
                new Station { Key = 40917, Name = "אלונים/הרימונים", Latitude = 32.715943, Longitude =35.124415},
                new Station { Key = 40918, Name = "אלונים/סמטת הלבנה", Latitude = 32.716826, Longitude =35.126544},
                new Station { Key = 40919, Name = "בית ספר רימונים", Latitude = 32.716995, Longitude =35.128928},
                new Station { Key = 40920, Name = "אלונים/בריכה", Latitude = 32.718823, Longitude =35.129646},
                new Station { Key = 40922, Name = "אלונים/כיכר בן גוריון", Latitude = 32.722066, Longitude =35.130372},
                new Station { Key = 40923, Name = "שקדים/האלה", Latitude = 32.723692, Longitude =35.133621},
                new Station { Key = 40924, Name = "שקדים/הסיגליות", Latitude = 32.725614, Longitude =35.135896},
                new Station { Key = 40925, Name = "בי''ס מיתרים", Latitude = 32.725906, Longitude =35.13691},

                //10 31.87301167871785, 35.26042916122732
                new Station { Key = 45385, Name = "סיירת דוכיפת/מחסום חזמא", Latitude = 31.828335, Longitude = 35.250612},
                new Station { Key = 61017, Name = "איזור תעשייה/שער בנימין", Latitude = 31.865017, Longitude = 35.261373},
                new Station { Key = 61002, Name = "תחנת דלק/כוכב יעקב", Latitude = 31.873011, Longitude =35.260429},
                new Station { Key = 60211, Name = "שדרות אביר יעקב/מעייני הישועה", Latitude = 31.883013, Longitude =35.247681},
                new Station { Key = 60215, Name = "חגווי סלע/מעייני הישועה", Latitude = 31.878496, Longitude = 35.243613},
                new Station { Key = 60216, Name = "משכנות הרועים/נהר שלום", Latitude = 31.873910, Longitude =35.243681},
                new Station { Key = 60217, Name = "משכנות הרועים/אבני חפץ", Latitude = 31.889875, Longitude =35.249011},
                new Station { Key = 60218, Name = "דרך כוכב יעקב", Latitude = 31.883867, Longitude =35.247120},

                new Station { Key = 63691, Name = "כוכב השחר/יציאה", Latitude = 31.956391, Longitude = 35.341945},
                new Station { Key = 60642, Name = "כוכב השחר ב", Latitude = 31.960264, Longitude = 35.348199},

                //12
                new Station { Key = 57096, Name = "מוזיאון", Latitude = 32.721517, Longitude = 35.567316},
                new Station { Key = 57097, Name = "מסעף מושבת כנרת", Latitude = 32.718670, Longitude = 35.560517},
                new Station { Key = 57098, Name = "מסעף אלומות", Latitude = 32.713917, Longitude = 35.545848},
                new Station { Key = 57102, Name = "מגרש ספורט", Latitude = 32.709703, Longitude = 35.501414},
                new Station { Key = 57105, Name = "מסעף סמדר", Latitude = 32.714889, Longitude = 35.493985},
                new Station { Key = 57108, Name = "ספסאף/אדיגה", Latitude = 32.722196, Longitude = 35.444308},
                new Station { Key = 57114, Name = "ויצמן/המשור", Latitude = 32.780171, Longitude = 35.502854},
                new Station { Key = 57115, Name = "הנשיא וייצמן/המברג", Latitude = 32.779985, Longitude = 35.499877},
                new Station { Key = 57116, Name = "מסעף אזור תעשיה", Latitude = 32.779076, Longitude = 35.498645},
                new Station { Key = 57117, Name = "בית חולים פוריה", Latitude = 32.751998, Longitude = 35.537857},
                new Station { Key = 57118, Name = "בית עלמין הזורעים", Latitude = 32.766760, Longitude = 35.524681},
                new Station { Key = 57119, Name = "אלחדיף/הופיין", Latitude = 31.960264, Longitude = 35.538987},

            };
            ListBusLines = new List<BusLine>
            {
                new BusLine{Key = 1784, LineNumber=949, Area = Areas.YehudaAndShomron},
                new BusLine{Key = 1785, LineNumber=488, Area = Areas.North},
                new BusLine{Key = 1786, LineNumber=236, Area = Areas.Hifa},
                new BusLine{Key = 1787, LineNumber=934, Area = Areas.General},
                new BusLine{Key = 1788, LineNumber=86, Area = Areas.Center},

                new BusLine{Key = 1789, LineNumber=400, Area = Areas.North},
                new BusLine{Key = 1790, LineNumber=949, Area = Areas.YehudaAndShomron},
                new BusLine{Key = 1791, LineNumber=218, Area = Areas.General},
                new BusLine{Key = 1792, LineNumber=143, Area = Areas.General},
                new BusLine{Key = 1793, LineNumber=86, Area = Areas.Center},

            };
            ListBusLineStations = new List<BusLineStation>
            {
                //949
                new BusLineStation{BusLineKey = 1784, StationKey = 45385, Position = 1 },
                new BusLineStation{BusLineKey = 1784, StationKey = 61017, Position = 2 },
                new BusLineStation{BusLineKey = 1784, StationKey = 61002, Position = 3 },
                new BusLineStation{BusLineKey = 1784, StationKey = 60211, Position = 4 },
                new BusLineStation{BusLineKey = 1784, StationKey = 60215, Position = 5 },
                new BusLineStation{BusLineKey = 1784, StationKey = 60216, Position = 6 },
                new BusLineStation{BusLineKey = 1784, StationKey = 60217, Position = 7 },
                new BusLineStation{BusLineKey = 1784, StationKey = 60218, Position = 8 },
                new BusLineStation{BusLineKey = 1784, StationKey = 63691, Position = 9 },
                new BusLineStation{BusLineKey = 1784, StationKey = 60642, Position = 10 },

                //488
                new BusLineStation{ BusLineKey = 1785, StationKey = 57096, Position = 1 },
                new BusLineStation{ BusLineKey = 1785, StationKey = 57097, Position = 2 },
                new BusLineStation{ BusLineKey = 1785, StationKey = 57098, Position = 3 },
                new BusLineStation{ BusLineKey = 1785, StationKey = 57102, Position = 4 },
                new BusLineStation{ BusLineKey = 1785, StationKey = 57105, Position = 5 },
                new BusLineStation{ BusLineKey = 1785, StationKey = 57108, Position = 6 },
                new BusLineStation{ BusLineKey = 1785, StationKey = 57114, Position = 7 },
                new BusLineStation{ BusLineKey = 1785, StationKey = 57115, Position = 8 },
                new BusLineStation{ BusLineKey = 1785, StationKey = 57116, Position = 9 },
                new BusLineStation{ BusLineKey = 1785, StationKey = 57117, Position = 10 },
                new BusLineStation{ BusLineKey = 1785, StationKey = 57118, Position = 11 },
                new BusLineStation{ BusLineKey = 1785, StationKey = 57119, Position = 12 },

                //236
                new BusLineStation{BusLineKey = 1786, StationKey = 40247, Position=1 },
                new BusLineStation{BusLineKey = 1786, StationKey = 40249, Position=2 },
                new BusLineStation{BusLineKey = 1786, StationKey = 40252, Position=3 },
                new BusLineStation{BusLineKey = 1786, StationKey = 40253, Position=4 },
                new BusLineStation{BusLineKey = 1786, StationKey = 40257, Position=5 },
                new BusLineStation{BusLineKey = 1786, StationKey = 40258, Position=6 },
                new BusLineStation{BusLineKey = 1786, StationKey = 40260, Position=7 },
                new BusLineStation{BusLineKey = 1786, StationKey = 40262, Position=8 },
                new BusLineStation{BusLineKey = 1786, StationKey = 40267, Position=9 },
                new BusLineStation{BusLineKey = 1786, StationKey = 40269, Position=10 },

                //934
                new BusLineStation{BusLineKey = 1787, StationKey = 21007, Position=1 },
                new BusLineStation{BusLineKey = 1787, StationKey = 21008, Position=2 },
                new BusLineStation{BusLineKey = 1787, StationKey = 24290, Position=3 },
                new BusLineStation{BusLineKey = 1787, StationKey = 27121, Position=4 },
                new BusLineStation{BusLineKey = 1787, StationKey = 27126, Position=5 },
                new BusLineStation{BusLineKey = 1787, StationKey = 28352, Position=6 },
                new BusLineStation{BusLineKey = 1787, StationKey = 28353, Position=7 },
                new BusLineStation{BusLineKey = 1787, StationKey = 20996, Position=8 },
                new BusLineStation{BusLineKey = 1787, StationKey = 20011, Position=9 },
                new BusLineStation{BusLineKey = 1787, StationKey = 22222, Position=10 },

                //86
                new BusLineStation{BusLineKey = 1788, StationKey = 40912, Position=1 },
                new BusLineStation{BusLineKey = 1788, StationKey = 40913, Position=2 },
                new BusLineStation{BusLineKey = 1788, StationKey = 40914, Position=3 },
                new BusLineStation{BusLineKey = 1788, StationKey = 40915, Position=4 },
                new BusLineStation{BusLineKey = 1788, StationKey = 40916, Position=5 },
                new BusLineStation{BusLineKey = 1788, StationKey = 40917, Position=6 },
                new BusLineStation{BusLineKey = 1788, StationKey = 40918, Position=7 },
                new BusLineStation{BusLineKey = 1788, StationKey = 40919, Position=8 },
                new BusLineStation{BusLineKey = 1788, StationKey = 40920, Position=9 },
                new BusLineStation{BusLineKey = 1788, StationKey = 40922, Position=10 },
                new BusLineStation{BusLineKey = 1788, StationKey = 40923, Position=11 },
                new BusLineStation{BusLineKey = 1788, StationKey = 40924, Position=12 },
                new BusLineStation{BusLineKey = 1788, StationKey = 40925, Position=13},

                //400
                new BusLineStation{BusLineKey = 1789, StationKey = 40247, Position=1 },
                new BusLineStation{BusLineKey = 1789, StationKey = 40249, Position=2 },
                new BusLineStation{BusLineKey = 1789, StationKey = 40252, Position=3 },
                new BusLineStation{BusLineKey = 1789, StationKey = 40253, Position=4 },
                new BusLineStation{BusLineKey = 1789, StationKey = 40257, Position=5 },
                new BusLineStation{BusLineKey = 1789, StationKey = 57096, Position=6 },
                new BusLineStation{BusLineKey = 1789, StationKey = 57098, Position=7 },
                new BusLineStation{BusLineKey = 1789, StationKey = 57105, Position=8 },
                new BusLineStation{BusLineKey = 1789, StationKey = 57108, Position=9 },
                new BusLineStation{BusLineKey = 1789, StationKey = 57116, Position=10 },
                new BusLineStation{BusLineKey = 1789, StationKey = 57117, Position=11 },

                //949 - 2nd direction
                new BusLineStation{BusLineKey = 1790, StationKey = 60642, Position = 1 },
                new BusLineStation{BusLineKey = 1790, StationKey = 63691, Position = 2 },
                new BusLineStation{BusLineKey = 1790, StationKey = 60218, Position = 3 },
                new BusLineStation{BusLineKey = 1790, StationKey = 60217, Position = 4 },
                new BusLineStation{BusLineKey = 1790, StationKey = 60216, Position = 5 },
                new BusLineStation{BusLineKey = 1790, StationKey = 60215, Position = 6 },
                new BusLineStation{BusLineKey = 1790, StationKey = 60211, Position = 7 },
                new BusLineStation{BusLineKey = 1790, StationKey = 61002, Position = 8 },
                new BusLineStation{BusLineKey = 1790, StationKey = 61017, Position = 9 },
                new BusLineStation{BusLineKey = 1790, StationKey = 45385, Position = 10 },

                //218
                new BusLineStation{BusLineKey = 1791, StationKey = 40247, Position=1 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40249, Position=2 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40257, Position=3 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40258, Position=4 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40262, Position=5 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40267, Position=6 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40269, Position=7 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40912, Position=8 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40913, Position=9 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40914, Position=10 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40918, Position=11 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40919, Position=12 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40920, Position=13 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40924, Position=14 },
                new BusLineStation{BusLineKey = 1791, StationKey = 40925, Position=15 },

                //143
                new BusLineStation{BusLineKey = 1792, StationKey = 45385, Position = 1 },
                new BusLineStation{BusLineKey = 1792, StationKey = 61017, Position = 2 },
                new BusLineStation{BusLineKey = 1792, StationKey = 61002, Position = 3 },
                new BusLineStation{BusLineKey = 1792, StationKey = 60211, Position = 4 },
                new BusLineStation{BusLineKey = 1792, StationKey = 60217, Position = 5 },
                new BusLineStation{BusLineKey = 1792, StationKey = 60218, Position = 6 },
                new BusLineStation{BusLineKey = 1792, StationKey = 60642, Position = 7 },
                new BusLineStation{BusLineKey = 1792, StationKey = 40925, Position = 8 },
                new BusLineStation{BusLineKey = 1792, StationKey = 40923, Position = 9 },
                new BusLineStation{BusLineKey = 1792, StationKey = 40922, Position = 10 },
                new BusLineStation{BusLineKey = 1792, StationKey = 40920, Position = 11 },
                new BusLineStation{BusLineKey = 1792, StationKey = 40918, Position = 12 },
                new BusLineStation{BusLineKey = 1792, StationKey = 40916, Position = 13 },
                new BusLineStation{BusLineKey = 1792, StationKey = 40913, Position = 14 },
                new BusLineStation{BusLineKey = 1792, StationKey = 40912, Position = 15 },

                //86 - 2nd direction
                new BusLineStation{BusLineKey = 1793, StationKey = 40925, Position=1 },
                new BusLineStation{BusLineKey = 1793, StationKey = 40924, Position=2 },
                new BusLineStation{BusLineKey = 1793, StationKey = 40923, Position=3 },
                new BusLineStation{BusLineKey = 1793, StationKey = 40922, Position=4 },
                new BusLineStation{BusLineKey = 1793, StationKey = 40920, Position=5 },
                new BusLineStation{BusLineKey = 1793, StationKey = 40919, Position=6 },
                new BusLineStation{BusLineKey = 1793, StationKey = 40918, Position=7 },
                new BusLineStation{BusLineKey = 1793, StationKey = 40917, Position=8 },
                new BusLineStation{BusLineKey = 1793, StationKey = 40916, Position=9 },
                new BusLineStation{BusLineKey = 1793, StationKey = 40915, Position=10 },
                new BusLineStation{BusLineKey = 1793, StationKey = 40914, Position=11 },
                new BusLineStation{BusLineKey = 1793, StationKey = 40913, Position=12 },
                new BusLineStation{BusLineKey = 1793, StationKey = 40912, Position=13},

            };

            ListConsecutiveStations = new List<ConsecutiveStations>();
            foreach (Station s1 in ListStations)
            {
                foreach (Station s2 in ListStations)
                {
                    if (!(s1.Key == s2.Key))
                    {
                        ListConsecutiveStations.Add(new ConsecutiveStations { StationKey1 = s1.Key, StationKey2 = s2.Key, Distance = GetDistance(s1.Key, s2.Key), AverageTime = GetTime(s1.Key, s2.Key) });
                    }
                }
            }

            ListLineSchedules = new List<LineSchedule>
            {
                new LineSchedule{LineKey = 1, Frequency = 20, StartTime=new DateTime(1,1,1,8,0,0), EndTime=new DateTime(1,1,1,17,20,0)},
                new LineSchedule{LineKey = 1, Frequency = 30, StartTime=new DateTime(1,1,1,17,20,0), EndTime=new DateTime(1,1,1,22,20,0)},
                new LineSchedule{LineKey = 2, Frequency = 90, StartTime=new DateTime(1,1,1,7,30,0), EndTime=new DateTime(1,1,1,0,0,0)},
                //new LineSchedule{LineKey = 1, Frequency = 20, StartTime=new DateTime(1,1,1,8,0,0), EndTime=new DateTime(1,1,1,0,0,0)},
            };
            ListUsers = new List<User>
            {
                new User{UserName = "shpayer", Password = "Rif1234",Email="tehila1742@gmail.com", AuthorizationManagement = AuthorizationManagement.Manager, FirstName="Tehila", LastName = "Shpayer", Address="כוכב יעקב דבש וחלב", PhoneNumber="0543844738"},
                new User{UserName = "hodaya", Password = "hodaya1000", AuthorizationManagement = AuthorizationManagement.Traveler},
                new User{UserName = "girl9", Password = "shyomi78", AuthorizationManagement = AuthorizationManagement.Traveler},
                new User{UserName = "oalbo", Password = "theBest111", AuthorizationManagement = AuthorizationManagement.Traveler},
                new User{UserName = "sara", Password = "1", Email="saramalka2003@gmail.com", AuthorizationManagement = AuthorizationManagement.Manager, FirstName="שרה מלכה", LastName = "חמו", Address="כוכב השחר מול נבו", PhoneNumber="0544325928"},
                new User{UserName = "iyov", Password = "missreble", AuthorizationManagement = AuthorizationManagement.Traveler},
                new User{UserName = "mobileFord12", Password = "sportscar13", AuthorizationManagement = AuthorizationManagement.Traveler},
                new User{UserName = "Lucky13Number", Password = "cazino666", AuthorizationManagement = AuthorizationManagement.Traveler},

            };
        }
    }
}
