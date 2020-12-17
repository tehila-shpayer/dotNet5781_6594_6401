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
        public static List<BusInTravel> ListBusesInTravel;
        public static List<Station> ListStations;
        public static List<BusLine> ListBusLines;
        public static List<BusLineStation> ListBusLineStations;
        public static List<ConsecutiveStations> ListConsecutiveStations;
        public static List<LineSchedule> ListLineSchedules;
        public static List<User> ListUsers;
        public static List<User> ListUserTravel;

        static DataSource()
        {
            InitAllLists();
        }
        static double GetDistant()
        {
            GeoCoordinate s1 = new GeoCoordinate();//מיקום תחנה 1
            GeoCoordinate s2 = new GeoCoordinate();//מיקום תחנה 2
            return s1.GetDistanceTo(s2);//חישוב מרחק
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

                //10
                new Station { Key = 45385, Name = "סיירת דוכיפת/מחסום חזמא", Latitude = 31.828149, Longitude = 35.252449},
                new Station { Key = 61017, Name = "איזור תעשייה/שער בנימין", Latitude = 31.864862, Longitude = 35.261681},
                new Station { Key = 61002, Name = "תחנת דלק/כוכב יעקב", Latitude = 32.883102, Longitude =35.24615},
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
                new BusLine{LineNumber=949, Area = Areas.YehudaAndShomron, FirstStationKey = 45385, LastStationKey = 60642},
                new BusLine{LineNumber=488, Area = Areas.North, FirstStationKey = 57096, LastStationKey = 57119},
                new BusLine{LineNumber=236, Area = Areas.Hifa, FirstStationKey = 40247, LastStationKey = 40269},
                new BusLine{LineNumber=934, Area = Areas.General, FirstStationKey = 21007, LastStationKey = 22222},
                new BusLine{LineNumber=86, Area = Areas.Center, FirstStationKey = 40912, LastStationKey = 40925},

                new BusLine{LineNumber=40, Area = Areas.General, FirstStationKey = 60642, LastStationKey = 22222},
                //new BusLine{LineNumber=6, Area = Areas.Jerusalem, FirstStationKey = , LastStationKey = },
                //new BusLine{LineNumber=142, Area = Areas.YehudaAndShomron, FirstStationKey = , LastStationKey = },
                //new BusLine{LineNumber=1, Area = Areas.General, FirstStationKey = , LastStationKey = },
                //new BusLine{LineNumber=67, Area = Areas.Jerusalem, FirstStationKey = , LastStationKey = },

            };
            ListBusLineStations = new List<BusLineStation>
            {
                //949
                new BusLineStation{BusLineNumber = 949, StationKey = 45385, Position = 1 },
                new BusLineStation{BusLineNumber = 949, StationKey = 61017, Position = 2 },
                new BusLineStation{BusLineNumber = 949, StationKey = 61002, Position = 3 },
                new BusLineStation{BusLineNumber = 949, StationKey = 60211, Position = 4 },
                new BusLineStation{BusLineNumber = 949, StationKey = 60215, Position = 5 },
                new BusLineStation{BusLineNumber = 949, StationKey = 60216, Position = 6 },
                new BusLineStation{BusLineNumber = 949, StationKey = 60217, Position = 7 },
                new BusLineStation{BusLineNumber = 949, StationKey = 60218, Position = 8 },
                new BusLineStation{BusLineNumber = 949, StationKey = 63691, Position = 9 },
                new BusLineStation{BusLineNumber = 949, StationKey = 60642, Position = 10 },

                //488
                new BusLineStation{ BusLineNumber = 488, StationKey = 57096, Position = 1 },
                new BusLineStation{ BusLineNumber = 488, StationKey = 57097, Position = 2 },
                new BusLineStation{ BusLineNumber = 488, StationKey = 57098, Position = 3 },
                new BusLineStation{ BusLineNumber = 488, StationKey = 57102, Position = 4 },
                new BusLineStation{ BusLineNumber = 488, StationKey = 57105, Position = 5 },
                new BusLineStation{ BusLineNumber = 488, StationKey = 57108, Position = 6 },
                new BusLineStation{ BusLineNumber = 488, StationKey = 57114, Position = 7 },
                new BusLineStation{ BusLineNumber = 488, StationKey = 57115, Position = 8 },
                new BusLineStation{ BusLineNumber = 488, StationKey = 57116, Position = 9 },
                new BusLineStation{ BusLineNumber = 488, StationKey = 57117, Position = 10 },
                new BusLineStation{ BusLineNumber = 488, StationKey = 57118, Position = 11 },
                new BusLineStation{ BusLineNumber = 488, StationKey = 57119, Position = 12 },
                /*
                */
            };
            ListConsecutiveStations = new List<ConsecutiveStations>
            {
                //949
                new ConsecutiveStations{StationKey1 = 45385, StationKey2 = 61017, Distance = 0, AverageTime = 0 }
            };
        }
    }
}
