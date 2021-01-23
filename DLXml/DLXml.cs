using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using DLAPI;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DL
{
    //מימוש חוזה הממשק IDL ע"י Xml
    //תקציר פונקציות בהגדרת הממשק
    sealed class DLXml : IDL
    {
        #region singelton
        static readonly DLXml instance = new DLXml();
        static DLXml() { }// static ctor to ensure instance init is done just before first usage
        DLXml() { } // default => private
        public static DLXml Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files

        string busesPath = @"BusesXml.xml"; //XElement
        string stationsPath = @"StationsXml.xml"; //XMLSerializer
        string busLinesPath = @"BusLinesXml.xml"; //XMLSerializer
        string busLineStationsPath = @"BusLineStationsXml.xml"; //XMLSerializer
        string consecutiveStationsPath = @"ConsecutiveStationsXml.xml"; //XElement
        string usersPath = @"UsersXml.xml"; //XMLSerializer
        string lineSchedulesPath = @"LineSchedulesXml.xml"; //XElement
        string runningNumbersPath = @"RunningNumbersXml.xml";

        #endregion

        #region Bus XElement
        public IEnumerable<Bus> GetAllBuses()
        {
            XElement busesRootElem = XmlTools.LoadListFromXMLElement(busesPath);

            return from b in busesRootElem.Elements()
                   select new Bus()
                   {
                       LicenseNumber = b.Element("LicenseNumber").Value,
                       RunningDate = DateTime.Parse(b.Element("RunningDate").Value),
                       LastTreatment = DateTime.Parse(b.Element("LastTreatment").Value),
                       Fuel = Int32.Parse(b.Element("Fuel").Value),
                       KM = Int32.Parse(b.Element("KM").Value),
                       BeforeTreatKM = Int32.Parse(b.Element("BeforeTreatKM").Value),
                       Status = (Status)Enum.Parse(typeof(Status), b.Element("Status").Value),
                   };
        }
        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            XElement busesRootElem = XmlTools.LoadListFromXMLElement(busesPath);

            return from b in busesRootElem.Elements()
                   let bus = new Bus()
                   {
                       LicenseNumber = b.Element("LicenseNumber").Value,
                       RunningDate = DateTime.Parse(b.Element("RunningDate").Value),
                       LastTreatment = DateTime.Parse(b.Element("LastTreatment").Value),
                       Fuel = Int32.Parse(b.Element("Fuel").Value),
                       KM = Int32.Parse(b.Element("KM").Value),
                       BeforeTreatKM = Int32.Parse(b.Element("BeforeTreatKM").Value),
                       Status = (Status)Enum.Parse(typeof(Status), b.Element("Status").Value),
                   }
                   where predicate(bus)
                   select bus;
        }
        public Bus GetBus(string licenseNumber)
        {
            XElement busesRootElem = XmlTools.LoadListFromXMLElement(busesPath);

            Bus bus = (from b in busesRootElem.Elements()
                        where b.Element("LicenseNumber").Value == licenseNumber
                        select new Bus()
                        {
                            LicenseNumber = b.Element("LicenseNumber").Value,
                            RunningDate = DateTime.Parse(b.Element("RunningDate").Value),
                            LastTreatment = DateTime.Parse(b.Element("LastTreatment").Value),
                            Fuel = Int32.Parse(b.Element("Fuel").Value),
                            KM = Int32.Parse(b.Element("KM").Value),
                            BeforeTreatKM = Int32.Parse(b.Element("BeforeTreatKM").Value),
                            Status = (Status)Enum.Parse(typeof(Status), b.Element("Status").Value),
                        }
                        ).FirstOrDefault();

            if (bus == null)
                throw new ArgumentNotFoundException($"Bus {bus.LicenseNumber} not found.");
            return bus;
        }
        public void AddBus(Bus bus)
        {
            XElement busesRootElem = XmlTools.LoadListFromXMLElement(busesPath);

            XElement bus1 = (from b in busesRootElem.Elements()
                             where b.Element("LicenseNumber").Value == bus.LicenseNumber
                             select b).FirstOrDefault();

            if (bus1 != null)
                throw new DO.InvalidInformationException("Duplicate bus license number");

            XElement busElem = new XElement("Bus", new XElement("LicenseNumber", bus.LicenseNumber),
                                  new XElement("RunningDate", bus.RunningDate.ToString()),
                                  new XElement("LastTreatment", bus.LastTreatment.ToString()),
                                  new XElement("Fuel", bus.Fuel.ToString()),
                                  new XElement("KM", bus.KM.ToString()),
                                  new XElement("BeforeTreatKM", bus.BeforeTreatKM.ToString()),
                                  new XElement("Status", bus.Status.ToString()));

            busesRootElem.Add(busElem);

            XmlTools.SaveListToXMLElement(busesRootElem, busesPath);
        }
        public void UpdateBus(Bus bus)
        {
            XElement busesRootElem = XmlTools.LoadListFromXMLElement(busesPath);

            XElement bus1 = (from b in busesRootElem.Elements()
                             where b.Element("LicenseNumber").Value == bus.LicenseNumber
                             select b).FirstOrDefault();

            if (bus1 != null)
            {
                bus1.Element("LicenseNumber").Value = bus.LicenseNumber;
                bus1.Element("RunningDate").Value = bus.RunningDate.ToString();
                bus1.Element("LastTreatment").Value = bus.LastTreatment.ToString();
                bus1.Element("Fuel").Value = bus.Fuel.ToString();
                bus1.Element("KM").Value = bus.KM.ToString();
                bus1.Element("BeforeTreatKM").Value = bus.BeforeTreatKM.ToString();
                bus1.Element("Status").Value = bus.Status.ToString();

                XmlTools.SaveListToXMLElement(busesRootElem, busesPath);
            }
            else
                throw new ArgumentNotFoundException($"Bus {bus.LicenseNumber} not found.");
        }
        public void UpdateBus(string licenseNumber, Action<Bus> update) //method that knows to updt specific fields in Bus
        {
            XElement busesRootElem = XmlTools.LoadListFromXMLElement(busesPath);

            XElement bus = (from b in busesRootElem.Elements()
                             where b.Element("LicenseNumber").Value == licenseNumber
                             select b).FirstOrDefault();

            if (bus != null)
            {
                Bus bus1 = GetBus(licenseNumber);
                update(bus1);
                bus.Element("LicenseNumber").Value = bus1.LicenseNumber;
                bus.Element("RunningDate").Value = bus1.RunningDate.ToString();
                bus.Element("LastTreatment").Value = bus1.LastTreatment.ToString();
                bus.Element("Fuel").Value = bus1.Fuel.ToString();
                bus.Element("KM").Value = bus1.KM.ToString();
                bus.Element("BeforeTreatKM").Value = bus1.BeforeTreatKM.ToString();
                bus.Element("Status").Value = bus1.Status.ToString();

                XmlTools.SaveListToXMLElement(busesRootElem, busesPath);
            }
            else
                throw new ArgumentNotFoundException($"Bus {licenseNumber} not found.");
        }
        public void DeleteBus(string licenseNumber)
        {
            XElement busesRootElem = XmlTools.LoadListFromXMLElement(busesPath);

            XElement bus = (from b in busesRootElem.Elements()
                             where b.Element("LicenseNumber").Value == licenseNumber
                             select b).FirstOrDefault();

            if (bus != null)
            {
                bus.Remove(); //<==>   Remove bus from busesRootElem

                XmlTools.SaveListToXMLElement(busesRootElem, busesPath);
            }
            else
                throw new ArgumentNotFoundException($"Bus {licenseNumber} not found.");
        }
        #endregion

        #region BusLine XMLSerializer
        public BusLine GetBusLine(int busLineKey)
        {
            List<BusLine> ListBusLines = XmlTools.LoadListFromXMLSerializer<BusLine>(busLinesPath);
            BusLine line = ListBusLines.Find(b => b.Key == busLineKey);
            if (line != null)
                return line;
            else
                throw new ArgumentNotFoundException($"Bus line not found with key: {busLineKey}");
        }
        public IEnumerable<BusLine> GetBusLinesBy(Predicate<BusLine> predicate)
        {
            List<BusLine> ListBusLines = XmlTools.LoadListFromXMLSerializer<BusLine>(busLinesPath);
            return from line in ListBusLines
                   where predicate(line)
                   select line;
        }
        public IEnumerable<BusLine> GetAllBusLines()
        {
            List<BusLine> ListBusLines = XmlTools.LoadListFromXMLSerializer<BusLine>(busLinesPath);
            return from line in ListBusLines
                   select line;
        }
        public int AddBusLine(BusLine line)
        {
            List<BusLine> ListBusLines = XmlTools.LoadListFromXMLSerializer<BusLine>(busLinesPath);
            List<int> ListKeys = XmlTools.LoadListFromXMLSerializer<int>(runningNumbersPath);
            line.Key = ListKeys[1]++;
            XmlTools.SaveListToXMLSerializer(ListKeys, runningNumbersPath);
            ListBusLines.Add(line);
            XmlTools.SaveListToXMLSerializer(ListBusLines, busLinesPath);
            return line.Key;
        }
        public void UpdateBusLine(BusLine line)
        {
            List<BusLine> ListBusLines = XmlTools.LoadListFromXMLSerializer<BusLine>(busLinesPath);
            int indexOfBusLineToUpdate = ListBusLines.FindIndex(l => l.Key == line.Key);
            if (indexOfBusLineToUpdate == -1)
                throw new ArgumentNotFoundException($"Bus line not found with key: {line.Key}");
            ListBusLines[indexOfBusLineToUpdate] = line;
            XmlTools.SaveListToXMLSerializer(ListBusLines, busLinesPath);
        }
        public void UpdateBusLine(int lineKey, Action<BusLine> update)//method that knows to updt specific fields in BusLine
        {
            List<BusLine> ListBusLines = XmlTools.LoadListFromXMLSerializer<BusLine>(busLinesPath);
            int indexOfBusLineToUpdate = ListBusLines.FindIndex(l => l.Key == lineKey);
            if (indexOfBusLineToUpdate == -1)
                throw new ArgumentNotFoundException($"Bus line not found with key: {lineKey}");
            BusLine line = ListBusLines.Find(b => b.Key == lineKey);
            update(line);
            ListBusLines[indexOfBusLineToUpdate] = line;
            XmlTools.SaveListToXMLSerializer(ListBusLines, busLinesPath);
        }
        public void DeleteBusLine(int busLineKey)
        {
            List<BusLine> ListBusLines = XmlTools.LoadListFromXMLSerializer<BusLine>(busLinesPath);
            BusLine line = ListBusLines.Find(b => b.Key == busLineKey);
            if (line == null)
                throw new ArgumentNotFoundException($"Bus line not found with key: {busLineKey}");
            ListBusLines.Remove(line);
            XmlTools.SaveListToXMLSerializer(ListBusLines, busLinesPath);
        }

        #endregion

        #region BusLineStation XMLSerializer
        public IEnumerable<BusLineStation> GetAllBusLineStations()
        {
            List<BusLineStation> ListBusLineStations = XmlTools.LoadListFromXMLSerializer<BusLineStation>(busLineStationsPath);
            return from bls in ListBusLineStations
                   select bls;
        }
        public IEnumerable<BusLineStation> GetAllBusLineStationsBy(Predicate<BusLineStation> predicate)
        {
            List<BusLineStation> ListBusLineStations = XmlTools.LoadListFromXMLSerializer<BusLineStation>(busLineStationsPath);
            return from bls in ListBusLineStations
                   where predicate(bls)
                   select bls;
        }
        public BusLineStation GetBusLineStationBy(Predicate<BusLineStation> predicate)
        {
            List<BusLineStation> ListBusLineStations = XmlTools.LoadListFromXMLSerializer<BusLineStation>(busLineStationsPath);
            BusLineStation busLineStation = ListBusLineStations.Find(b => predicate(b));
            if (busLineStation != null)
                return busLineStation;
            return null;
        }
        public BusLineStation GetBusLineStationByKey(int line, int stationKey)
        {
            List<BusLineStation> ListBusLineStations = XmlTools.LoadListFromXMLSerializer<BusLineStation>(busLineStationsPath);
            BusLineStation busLineStation = ListBusLineStations.Find(b => b.BusLineKey == line && b.StationKey == stationKey);
            if (busLineStation != null)
                return busLineStation;
            else
                throw new ArgumentNotFoundException($"Bus line station of line {line} and station {stationKey} was not found.");
        }
        public IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine)
        {
            List<BusLineStation> ListBusLineStations = XmlTools.LoadListFromXMLSerializer<BusLineStation>(busLineStationsPath);
            var AllStationsOfLine = from bls in ListBusLineStations
                                    where bls.BusLineKey == busLine
                                    select bls;
            return AllStationsOfLine.OrderBy(bls => bls.Position);
        }
        public void AddBusLineStation(BusLineStation lineStation)
        {
            List<BusLineStation> ListBusLineStations = XmlTools.LoadListFromXMLSerializer<BusLineStation>(busLineStationsPath);
            if (ListBusLineStations.FirstOrDefault(s => s.BusLineKey == lineStation.BusLineKey && s.StationKey == lineStation.StationKey) != null)
                throw new InvalidInformationException("Duplicate bus line station");
            GetStation(lineStation.StationKey);//if not found, an exception is thrown
            GetBusLine(lineStation.BusLineKey);//if not found, an exception is thrown
            ListBusLineStations.Add(lineStation);
            XmlTools.SaveListToXMLSerializer(ListBusLineStations, busLineStationsPath);
        }
        public void UpdateBusLineStation(BusLineStation station)
        {
            List<BusLineStation> ListBusLineStations = XmlTools.LoadListFromXMLSerializer<BusLineStation>(busLineStationsPath);
            int index = ListBusLineStations.FindIndex(s => s.BusLineKey == station.BusLineKey && s.StationKey == station.StationKey);
            if (index == -1)
                throw new ArgumentNotFoundException($"Bus station of line {station.BusLineKey} and station {station.StationKey} was not found.");
            ListBusLineStations[index] = station;          
            XmlTools.SaveListToXMLSerializer(ListBusLineStations, busLineStationsPath);
        }
        public void UpdateBusLineStation(int line, int stationKey, Action<BusLineStation> update) //method that knows to updt specific fields in Person
        {
            List<BusLineStation> ListBusLineStations = XmlTools.LoadListFromXMLSerializer<BusLineStation>(busLineStationsPath);
            int index = ListBusLineStations.FindIndex(s => s.BusLineKey == line && s.StationKey == stationKey);
            if (index == -1)
                throw new ArgumentNotFoundException($"Bus station of line {line} and station {stationKey} was not found.");
            BusLineStation lineStation = ListBusLineStations.Find(s => s.BusLineKey == line && s.StationKey == stationKey);
            update(lineStation);
            ListBusLineStations[index] = lineStation;
            XmlTools.SaveListToXMLSerializer(ListBusLineStations, busLineStationsPath);
        }
        public void DeleteBusLineStation(int line, int stationKey)
        {
            List<BusLineStation> ListBusLineStations = XmlTools.LoadListFromXMLSerializer<BusLineStation>(busLineStationsPath);
            BusLineStation busLineStation = ListBusLineStations.Find(b => b.BusLineKey == line && b.StationKey == stationKey);
            if (busLineStation == null)
                throw new ArgumentNotFoundException($"Bus station of line {line} and station {stationKey} was not found.");
            ListBusLineStations.Remove(busLineStation);
            XmlTools.SaveListToXMLSerializer(ListBusLineStations, busLineStationsPath);
        }
        public void DeleteBusLineStationsByStation(int stationKey)
        {
            List<BusLineStation> ListBusLineStations = XmlTools.LoadListFromXMLSerializer<BusLineStation>(busLineStationsPath);
            ListBusLineStations.RemoveAll(bls => bls.BusLineKey == stationKey);
            XmlTools.SaveListToXMLSerializer(ListBusLineStations, busLineStationsPath);
        }
        public void DeleteBusLineStationsByLine(int lineKey)
        {
            List<BusLineStation> ListBusLineStations = XmlTools.LoadListFromXMLSerializer<BusLineStation>(busLineStationsPath);
            ListBusLineStations.RemoveAll(bls => bls.BusLineKey == lineKey);
            XmlTools.SaveListToXMLSerializer(ListBusLineStations, busLineStationsPath);
        }
        #endregion

        #region ConsecutiveStations XElement
        public ConsecutiveStations GetConsecutiveStations(int stationKey1, int stationKey2)
        {
            XElement consecutiveStationsRootElem = XmlTools.LoadListFromXMLElement(consecutiveStationsPath);

            ConsecutiveStations consecutiveStations = (from cs in consecutiveStationsRootElem.Elements()
                       where int.Parse(cs.Element("StationKey1").Value) == stationKey1 &&
                       int.Parse(cs.Element("StationKey2").Value) == stationKey2
                       select new ConsecutiveStations()
                       {
                           StationKey1 = int.Parse(cs.Element("StationKey1").Value),
                           StationKey2 = int.Parse(cs.Element("StationKey2").Value),
                           Distance = int.Parse(cs.Element("Distance").Value),
                           AverageTime = int.Parse(cs.Element("AverageTime").Value),
                       }).FirstOrDefault();

            if (consecutiveStations == null)
                throw new ArgumentNotFoundException($"Consecutive stations of first station  {stationKey1} and second station {stationKey2} was not found.");
            return consecutiveStations;
        }
        public void AddConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            XElement consecutiveStationsRootElem = XmlTools.LoadListFromXMLElement(consecutiveStationsPath);

            XElement consecutiveStations1 = (from cs in consecutiveStationsRootElem.Elements()
                                             where int.Parse(cs.Element("StationKey1").Value) == consecutiveStations.StationKey1 &&
                                             int.Parse(cs.Element("StationKey2").Value) == consecutiveStations.StationKey2
                                             select cs).FirstOrDefault();

            if (consecutiveStations1 != null)
                return;

            XElement consecutiveStationsElem = new XElement("ConsecutiveStations", new XElement("StationKey1", consecutiveStations.StationKey1.ToString()),
                                  new XElement("StationKey2", consecutiveStations.StationKey2.ToString()),
                                  new XElement("Distance", consecutiveStations.Distance.ToString()),
                                  new XElement("AverageTime", consecutiveStations.AverageTime.ToString()));
            
            consecutiveStationsRootElem.Add(consecutiveStationsElem);

            XmlTools.SaveListToXMLElement(consecutiveStationsRootElem, consecutiveStationsPath);
        }
        public void UpdateConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            XElement consecutiveStationsRootElem = XmlTools.LoadListFromXMLElement(consecutiveStationsPath);

            XElement consecutiveStations1 = (from cs in consecutiveStationsRootElem.Elements()
                                             where int.Parse(cs.Element("StationKey1").Value) == consecutiveStations.StationKey1 &&
                                             int.Parse(cs.Element("StationKey2").Value) == consecutiveStations.StationKey2
                                             select cs).FirstOrDefault();

            if (consecutiveStations1 != null)
            {
                consecutiveStations1.Element("StationKey1").Value = consecutiveStations.StationKey1.ToString();
                consecutiveStations1.Element("StationKey2").Value = consecutiveStations.StationKey2.ToString();
                consecutiveStations1.Element("Distance").Value = consecutiveStations.Distance.ToString();
                consecutiveStations1.Element("AverageTime").Value = consecutiveStations.AverageTime.ToString();
 
                XmlTools.SaveListToXMLElement(consecutiveStationsRootElem, consecutiveStationsPath);
            }
            else
                throw new ArgumentNotFoundException($"Consecutive stations not found with keys: {consecutiveStations.StationKey1} and {consecutiveStations.StationKey2}.");
        }
        public void UpdateConsecutiveStations(int stationKey1, int stationKey2, Action<ConsecutiveStations> update) //method that knows to updt specific fields in Person
        {
            XElement consecutiveStationsRootElem = XmlTools.LoadListFromXMLElement(consecutiveStationsPath);

            XElement consecutiveStations = (from cs in consecutiveStationsRootElem.Elements()
                                             where int.Parse(cs.Element("StationKey1").Value) == stationKey1 &&
                                             int.Parse(cs.Element("StationKey2").Value) == stationKey2
                                             select cs).FirstOrDefault();

            if (consecutiveStations != null)
            {
                ConsecutiveStations cs = GetConsecutiveStations(stationKey1, stationKey2);
                update(cs);
                consecutiveStations.Element("StationKey1").Value = cs.StationKey1.ToString();
                consecutiveStations.Element("StationKey2").Value = cs.StationKey2.ToString();
                consecutiveStations.Element("Distance").Value = cs.Distance.ToString();
                consecutiveStations.Element("AverageTime").Value = cs.AverageTime.ToString();

                XmlTools.SaveListToXMLElement(consecutiveStationsRootElem, consecutiveStationsPath);
            }
            else
                throw new ArgumentNotFoundException($"Consecutive stations not found with keys: {stationKey1} and {stationKey2}.");
        }
        public void DeleteConsecutiveStations(int stationKey1, int stationKey2)
        {
            XElement consecutiveStationsRootElem = XmlTools.LoadListFromXMLElement(consecutiveStationsPath);

            XElement consecutiveStations = (from cs in consecutiveStationsRootElem.Elements()
                                            where int.Parse(cs.Element("StationKey1").Value) == stationKey1 &&
                                            int.Parse(cs.Element("StationKey2").Value) == stationKey2
                                            select cs).FirstOrDefault();
            if (consecutiveStations != null)
            {
                consecutiveStations.Remove(); //<==>   Remove consecutive stations from consecutiveStationsRootElem

                XmlTools.SaveListToXMLElement(consecutiveStationsRootElem, consecutiveStationsPath);
            }
            else
                throw new ArgumentNotFoundException($"Consecutive stations of first station  {stationKey1} and second station {stationKey2} was not found.");
        }
        public void DeleteConsecutiveStations(int stationKey)
        {
            XElement consecutiveStationsRootElem = XmlTools.LoadListFromXMLElement(consecutiveStationsPath);


            List<XElement> consecutiveStations = (from cs in consecutiveStationsRootElem.Elements()
                                                  select cs).ToList();

            //<==>   Remove consecutive stations asocieted with station of key stationKey
            consecutiveStations.RemoveAll(cs => int.Parse(cs.Element("StationKey1").Value) == stationKey ||
                                                  int.Parse(cs.Element("StationKey2").Value) == stationKey);

            XmlTools.SaveListToXMLElement(consecutiveStationsRootElem, consecutiveStationsPath);
        }
        #endregion

        #region LineSchedule XElement
        public LineSchedule GetLineSchedule(int lineKey, TimeSpan startTime)
        {
            XElement lineScheduleRootElem = XmlTools.LoadListFromXMLElement(lineSchedulesPath);

            LineSchedule schedule = (from ls in lineScheduleRootElem.Elements()
                       where int.Parse(ls.Element("LineKey").Value) == lineKey &&
                       TimeSpan.Parse(ls.Element("StartTime").Value) == startTime 
                       select new LineSchedule()
                       {
                           LineKey = int.Parse(ls.Element("LineKey").Value),
                           StartTime = TimeSpan.Parse(ls.Element("StartTime").Value),
                           EndTime = TimeSpan.Parse(ls.Element("EndTime").Value),
                           Frequency = Int32.Parse(ls.Element("Frequency").Value),
                       }).FirstOrDefault();

            if (schedule == null)
                throw new DO.ArgumentNotFoundException($"Line schedule of line {lineKey} and starting time {startTime.ToString("HH:mm")} not found.");
            return schedule;
        }
        public IEnumerable<LineSchedule> GetAllLineSchedules()
        {
            XElement lineScheduleRootElem = XmlTools.LoadListFromXMLElement(lineSchedulesPath);

            return from ls in lineScheduleRootElem.Elements()
                   select new LineSchedule()
                   {
                       LineKey = int.Parse(ls.Element("LineKey").Value),
                       StartTime = TimeSpan.Parse(ls.Element("StartTime").Value),
                       EndTime = TimeSpan.Parse(ls.Element("EndTime").Value),
                       Frequency = Int32.Parse(ls.Element("Frequency").Value),
                   };
        }
        public IEnumerable<LineSchedule> GetAllLineSchedulesOfLine(int lineKey)
        {
            XElement lineScheduleRootElem = XmlTools.LoadListFromXMLElement(lineSchedulesPath);

            return from ls in lineScheduleRootElem.Elements()
                   where int.Parse(ls.Element("LineKey").Value) == lineKey
                   select new LineSchedule()
                   {
                       LineKey = int.Parse(ls.Element("LineKey").Value),
                       StartTime = TimeSpan.Parse(ls.Element("StartTime").Value),
                       EndTime = TimeSpan.Parse(ls.Element("EndTime").Value),
                       Frequency = Int32.Parse(ls.Element("Frequency").Value),
                   };
        }
        public void AddLineSchedule(LineSchedule lineSchedule)
        {
            GetBusLine(lineSchedule.LineKey);
            XElement lineScheduleRootElem = XmlTools.LoadListFromXMLElement(lineSchedulesPath);

            XElement lineSchedule1 = (from ls in lineScheduleRootElem.Elements()
                                      where int.Parse(ls.Element("LineKey").Value) == lineSchedule.LineKey &&
                                      TimeSpan.Parse(ls.Element("StartTime").Value) == lineSchedule.StartTime
                                      select ls).FirstOrDefault();
            if (lineSchedule1 != null)
                throw new InvalidInformationException($"There is already a line schedule {lineSchedule.LineKey} that start at {lineSchedule.StartTime}");

            XElement lineScheduleElem = new XElement("LineSchedule", new XElement("LineKey", lineSchedule.LineKey.ToString()),
                                  new XElement("StartTime", lineSchedule.StartTime.ToString()),
                                  new XElement("EndTime", lineSchedule.EndTime.ToString()),
                                  new XElement("Frequency", lineSchedule.Frequency.ToString()));

            lineScheduleRootElem.Add(lineScheduleElem);

            XmlTools.SaveListToXMLElement(lineScheduleRootElem, lineSchedulesPath);
        }
        public void UpdateLineSchedule(LineSchedule lineSchedule)
        {
            XElement lineScheduleRootElem = XmlTools.LoadListFromXMLElement(lineSchedulesPath);

            XElement lineSchedule1 = (from ls in lineScheduleRootElem.Elements()
                                      where int.Parse(ls.Element("LineKey").Value) == lineSchedule.LineKey &&
                                      TimeSpan.Parse(ls.Element("StartTime").Value) == lineSchedule.StartTime
                                      select ls).FirstOrDefault();

            if (lineSchedule1 != null)
            {
                lineSchedule1.Element("LineKey").Value = lineSchedule.LineKey.ToString();
                lineSchedule1.Element("StartTime").Value = lineSchedule.StartTime.ToString();
                lineSchedule1.Element("EndTime").Value = lineSchedule.EndTime.ToString();
                lineSchedule1.Element("Frequency").Value = lineSchedule.Frequency.ToString();

                XmlTools.SaveListToXMLElement(lineScheduleRootElem, lineSchedulesPath);
            }
            else
                throw new ArgumentNotFoundException($"Line schedule of line {lineSchedule.LineKey} and starting time {lineSchedule.StartTime.ToString("HH:mm")} not found.");
        }      
        public void DeleteLineSchedule(int lineKey, TimeSpan startTime)
        {
            XElement lineScheduleRootElem = XmlTools.LoadListFromXMLElement(lineSchedulesPath);

            XElement lineScheduleElem = (from ls in lineScheduleRootElem.Elements()
                                      where int.Parse(ls.Element("LineKey").Value) == lineKey &&
                                      TimeSpan.Parse(ls.Element("StartTime").Value) == startTime
                                      select ls).FirstOrDefault();

            if (lineScheduleElem != null)
            {
                lineScheduleElem.Remove();
                XmlTools.SaveListToXMLElement(lineScheduleRootElem, lineSchedulesPath);
            }
            else
                throw new ArgumentNotFoundException($"Line schedule of line {lineKey} and starting time {startTime.ToString("HH:mm")} not found.");
        }
        #endregion

        #region Station XMLSerializer
        public Station GetStation(int stationKey)
        {
            List<Station> ListStations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            Station station = ListStations.Find(s => s.Key == stationKey);
            if (station != null)
                return station;
            else
                throw new ArgumentNotFoundException($"Station not found with key: {stationKey}");
        }
        public IEnumerable<Station> GetAllStations()
        {
            List<Station> ListStations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            return from station in ListStations
                   select station;
        }
        public int AddStation(Station station)
        {
            List<Station> ListStations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            List<int> ListKeys = XmlTools.LoadListFromXMLSerializer<int>(runningNumbersPath);
            station.Key = ListKeys[0]++;
            XmlTools.SaveListToXMLSerializer(ListKeys, runningNumbersPath);
            ListStations.Add(station);
            XmlTools.SaveListToXMLSerializer(ListStations, stationsPath);
            return station.Key;
        }
        public void UpdateStation(Station station)
        {
            List<Station> ListStations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            int indexOfStationToUpdate = ListStations.FindIndex(s => s.Key == station.Key);
            if (indexOfStationToUpdate == -1)
                throw new ArgumentNotFoundException($"Station not found with key: {station.Key}");
            ListStations[indexOfStationToUpdate] = station;
            XmlTools.SaveListToXMLSerializer(ListStations, stationsPath);
        }
        public void UpdateStation(int stationKey, Action<Station> update)//method that knows to updt specific fields in Station
        {
            List<Station> ListStations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            int indexOfStationToUpdate = ListStations.FindIndex(s => s.Key == stationKey);
            if (indexOfStationToUpdate == -1)
                throw new ArgumentNotFoundException($"Station not found with key: {stationKey}");
            Station station = ListStations.Find(s => s.Key == stationKey);
            update(station);
            ListStations[indexOfStationToUpdate] = station;
            XmlTools.SaveListToXMLSerializer(ListStations, stationsPath);
        }

        public void DeleteStation(int stationKey)
        {
            List<Station> ListStations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            Station station = ListStations.Find(s => s.Key == stationKey);
            if (station == null)
                throw new ArgumentNotFoundException($"Station: {stationKey} not found!");
            ListStations.Remove(station);
            XmlTools.SaveListToXMLSerializer(ListStations, stationsPath);
        }
        public IEnumerable<int> GetAllLinesInStation(int stationKey)
        {
            List<BusLineStation> ListBusLineStations = XmlTools.LoadListFromXMLSerializer<BusLineStation>(busLineStationsPath);
            var allLines = from bls in ListBusLineStations
                           where bls.StationKey == stationKey
                           select bls.BusLineKey;
            return allLines;
        }
        #endregion

        #region User XMLSerializer
        public User GetUser(string userName)
        {
            List<User> ListUsers = XmlTools.LoadListFromXMLSerializer<User>(usersPath);
            User user = ListUsers.Find(u => u.UserName == userName);
            if (user != null)
                return user;
            else
                throw new ArgumentNotFoundException($"User not found with user name: {userName}");
        }
        public User GetUser(string userName, string password)
        {
            List<User> ListUsers = XmlTools.LoadListFromXMLSerializer<User>(usersPath);
            User user = ListUsers.Find(u => u.UserName == userName && u.Password == password);
            if (user != null)
                return user;
            else
                throw new ArgumentNotFoundException($"User {userName} with the password not found");
        }
        public IEnumerable<User> GetAllUsers()
        {
            List<User> ListUsers = XmlTools.LoadListFromXMLSerializer<User>(usersPath);
            return from user in ListUsers
                   select user;
        }
        public void AddUser(User user)
        {
            List<User> ListUsers = XmlTools.LoadListFromXMLSerializer<User>(usersPath);
            if (ListUsers.FirstOrDefault(u => u.UserName == user.UserName) != null)
                throw new InvalidInformationException("Duplicate user name");
            ListUsers.Add(user);
            XmlTools.SaveListToXMLSerializer(ListUsers, usersPath);
        }
        public void UpdateUser(User user)
        {
            List<User> ListUsers = XmlTools.LoadListFromXMLSerializer<User>(usersPath);
            int indexOfUserToUpdate = ListUsers.FindIndex(s => s.UserName == user.UserName);
            if (indexOfUserToUpdate == -1)
                throw new ArgumentNotFoundException($"User {user.UserName} not found");
            ListUsers[indexOfUserToUpdate] = user;
            XmlTools.SaveListToXMLSerializer(ListUsers, usersPath);
        }
        public void DeleteUser(string userName)
        {
            List<User> ListUsers = XmlTools.LoadListFromXMLSerializer<User>(usersPath);
            User user = ListUsers.Find(b => b.UserName == userName);
            if (user == null)
                throw new ArgumentNotFoundException($"User not found with user name: {userName}");
            ListUsers.Remove(user);
            XmlTools.SaveListToXMLSerializer(ListUsers, usersPath);
        }
        #endregion

    }
}