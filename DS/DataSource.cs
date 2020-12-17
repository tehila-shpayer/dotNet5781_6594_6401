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
        public static List<Station> ListStations;
        public static List<BusLine> ListBusLines;
        public static List<User> ListUsers;


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
                new Station { Key =40247, Name = "צהל/האקליפטוס", Latitude = 32.439432, Longitude =34.930418 },
                new Station { Key = 40249, Name = "צומת בנימינה", Latitude = 32.518144, Longitude =34.930064},
                new Station { Key = 40252, Name = "צהל/משרדי חברת חשמל", Latitude = 32.440952, Longitude =34.931872},
                new Station { Key = 40253, Name = "צומת מיכאל מעגן", Latitude = 32.557385, Longitude =34.93226},
                new Station { Key = 40257, Name = "יפו/זיסו", Latitude = 32.827351, Longitude =34.987691},
                new Station { Key = 40258, Name = "הנדיב/דרך העצמאות", Latitude = 32.52383, Longitude =34.934878},
                new Station { Key = 40260, Name = "נורית/דרך העצמאות", Latitude = 32.527128, Longitude =34.935506},
                new Station { Key = 40262, Name = "נחל צין/חטיבת הנחל", Latitude = 32.431123 , Longitude =34.936463},
                new Station { Key = 40267, Name = "משטרה/שדרות נילי", Latitude = 32.571946, Longitude =34.938368}
            };
        }
    }

}
