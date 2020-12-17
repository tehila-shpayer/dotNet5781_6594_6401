using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                new Station
                {

                },
                new Station { Key = 24290, Name =  "דרייפוס/דרך צרפת", Latitude=32.801436, Longitude = 34.993051},
                new Station { Key = 68352, Name = "תחנת השמים", Latitude= 32.437864, Longitude = 34.92632},
                //9
                new Station { Key = 40247, Name = "צהל/האקליפטוס", Latitude = 32.439432, Longitude =34.930418 },
                new Station { Key = 40249, Name = "צומת בנימינה", Latitude = 32.518144, Longitude =34.930064},
                new Station { Key = 40252, Name = "צהל/משרדי חברת חשמל", Latitude = 32.440952, Longitude =34.931872},
                new Station { Key = 40253, Name = "צומת מיכאל מעגן", Latitude = 32.557385, Longitude =34.93226},
                new Station { Key = 40257, Name = "יפו/זיסו", Latitude = 32.827351, Longitude =34.987691},
                new Station { Key = 40258, Name = "הנדיב/דרך העצמאות", Latitude = 32.52383, Longitude =34.934878},
                new Station { Key = 40260, Name = "נורית/דרך העצמאות", Latitude = 32.527128, Longitude =34.935506},
                new Station { Key = 40262, Name = "נחל צין/חטיבת הנחל", Latitude = 32.431123 , Longitude =34.936463},
                new Station { Key = 40267, Name = "משטרה/שדרות נילי", Latitude = 32.571946, Longitude =34.938368},

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

                //
            };
        }
    }

}
