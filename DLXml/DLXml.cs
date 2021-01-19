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
    class DLXml : IDL
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
        #endregion

        //#region Person


        //public void UpdatePerson(int id, Action<DO.Person> update)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion Person

        //#region Student
        //public DO.Student GetStudent(int id)
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    DO.Student stu = ListStudents.Find(p => p.ID == id);
        //    if (stu != null)
        //        return stu; //no need to Clone()
        //    else
        //        throw new DO.BadPersonIdException(id, $"bad student id: {id}");
        //}
        //public void AddStudent(DO.Student student)
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    if (ListStudents.FirstOrDefault(s => s.ID == student.ID) != null)
        //        throw new DO.BadPersonIdException(student.ID, "Duplicate student ID");

        //    if (GetPerson(student.ID) == null)
        //        throw new DO.BadPersonIdException(student.ID, "Missing person ID");

        //    ListStudents.Add(student); //no need to Clone()

        //    XMLTools.SaveListToXMLSerializer(ListStudents, studentsPath);

        //}
        //public IEnumerable<DO.Student> GetAllStudents()
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    return from student in ListStudents
        //           select student; //no need to Clone()
        //}
        //public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    return from student in ListStudents
        //           select generate(student.ID, GetPerson(student.ID).Name);
        //}

        //public IEnumerable<object> GetStudentListWithSelectedFields(Func<DO.Student, object> generate)
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    return from student in ListStudents
        //           select generate(student);
        //}
        //public void UpdateStudent(DO.Student student)
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    DO.Student stu = ListStudents.Find(p => p.ID == student.ID);
        //    if (stu != null)
        //    {
        //        ListStudents.Remove(stu);
        //        ListStudents.Add(student); //no nee to Clone()
        //    }
        //    else
        //        throw new DO.BadPersonIdException(student.ID, $"bad student id: {student.ID}");

        //    XMLTools.SaveListToXMLSerializer(ListStudents, studentsPath);
        //}

        //public void UpdateStudent(int id, Action<DO.Student> update)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteStudent(int id)
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    DO.Student stu = ListStudents.Find(p => p.ID == id);

        //    if (stu != null)
        //    {
        //        ListStudents.Remove(stu);
        //    }
        //    else
        //        throw new DO.BadPersonIdException(id, $"bad student id: {id}");

        //    XMLTools.SaveListToXMLSerializer(ListStudents, studentsPath);
        //}
        //#endregion Student

        #region Bus
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

        //#region BusLine
        //public BusLine GetBusLine(int busLineKey)
        //{
        //    BusLine line = DataSource.ListBusLines.Find(b => b.Key == busLineKey);
        //    if (line != null)
        //        return line.Clone();
        //    else
        //        throw new ArgumentNotFoundException($"Bus not found with license number: {busLineKey}");
        //}
        //public IEnumerable<BusLine> GetBusLinesBy(Predicate<BusLine> predicate)
        //{
        //    var AllBuseLinesBy = from line in DataSource.ListBusLines
        //                         where predicate(line)
        //                         select line.Clone();
        //    return AllBuseLinesBy;
        //}
        //public IEnumerable<BusLine> GetAllBusLines()
        //{
        //    var AllBuseLines = from line in DataSource.ListBusLines
        //                       select line.Clone();
        //    return AllBuseLines;
        //}
        //public int AddBusLine(BusLine bus)
        //{
        //    bus.Key = BusLine.BUS_LINE_KEY++;
        //    //if (DataSource.ListBusLines.FirstOrDefault(l => l.Key == bus.Key) != null)
        //    //    throw new InvalidInformationException("Duplicate bus line key");
        //    DataSource.ListBusLines.Add(bus.Clone());
        //    return bus.Key;
        //}
        //public void UpdateBusLine(BusLine line)
        //{
        //    int indexOfBusLineToUpdate = DataSource.ListBusLines.FindIndex(s => s.Key == line.Key);
        //    if (indexOfBusLineToUpdate == -1)
        //        throw new ArgumentNotFoundException($"BusLine not found with key: {line.Key}");
        //    DataSource.ListBusLines[indexOfBusLineToUpdate] = line;
        //}
        //public void UpdateBusLine(int busLineKey, Action<BusLine> update)//method that knows to updt specific fields in BusLine
        //{
        //    BusLine bus = DataSource.ListBusLines.Find(b => b.Key == busLineKey);
        //    if (bus != null)
        //        update(bus);
        //    else
        //        throw new ArgumentNotFoundException($"Bus not found with license number: {busLineKey}");
        //}
        //public void DeleteBusLine(int busLineKey)
        //{
        //    BusLine bus = DataSource.ListBusLines.Find(b => b.Key == busLineKey);
        //    if (bus == null)
        //        throw new ArgumentNotFoundException($"Bus not found with license number: {busLineKey}");
        //    DataSource.ListBusLines.Remove(bus);
        //}

        //#endregion

        //#region BusLineStation
        //public IEnumerable<BusLineStation> GetAllBusLineStations()
        //{
        //    var AllBusLineStations = from bls in DataSource.ListBusLineStations
        //                             select bls.Clone();
        //    return AllBusLineStations;
        //}
        //public IEnumerable<BusLineStation> GetAllBusLineStationsBy(Predicate<BusLineStation> predicate)
        //{
        //    var AllBusLineStations = from bls in DataSource.ListBusLineStations
        //                             where predicate(bls)
        //                             select bls.Clone();
        //    return AllBusLineStations;
        //}
        //public BusLineStation GetBusLineStationBy(Predicate<BusLineStation> predicate)
        //{
        //    BusLineStation busLineStation = DataSource.ListBusLineStations.Find(b => predicate(b));
        //    if (busLineStation != null)
        //        return busLineStation.Clone();
        //    else
        //        throw new ArgumentNotFoundException($"Bus line station required was not found.");
        //}
        //public BusLineStation GetBusLineStationByKey(int line, int stationKey)
        //{
        //    BusLineStation busLineStation = DataSource.ListBusLineStations.Find(b => b.BusLineKey == line && b.StationKey == stationKey);
        //    if (busLineStation != null)
        //        return busLineStation.Clone();
        //    else
        //        throw new ArgumentNotFoundException($"Bus line station of line {line} and station {stationKey} was not found.");
        //}
        //public IEnumerable<BusLineStation> GetAllStationsOfLine(int busLine)
        //{
        //    var AllStationsOfLine = from station in DataSource.ListBusLineStations
        //                            where station.BusLineKey == busLine
        //                            select station.Clone();
        //    return AllStationsOfLine.OrderBy(bls => bls.Position);
        //}
        //public void AddBusLineStation(BusLineStation station)
        //{
        //    if (DataSource.ListBusLineStations.FirstOrDefault(s => s.BusLineKey == station.BusLineKey && s.StationKey == station.StationKey) != null)
        //        throw new InvalidInformationException("Duplicate station bus line number and station key");
        //    if (DataSource.ListStations.FirstOrDefault(s => s.Key == station.StationKey) == null)
        //        throw new InvalidInformationException($"No station with key {station.StationKey} exists.");
        //    if (DataSource.ListBusLines.FirstOrDefault(b => b.Key == station.BusLineKey) == null)
        //        throw new InvalidInformationException($"No Bus Line with key {station.BusLineKey} exists.");
        //    DataSource.ListBusLineStations.Add(station.Clone());
        //}
        //public void UpdateBusLineStation(BusLineStation station)
        //{
        //    int index = DataSource.ListBusLineStations.FindIndex(s => s.BusLineKey == station.BusLineKey && s.StationKey == station.StationKey);
        //    if (index != -1)
        //        DataSource.ListBusLineStations[index] = station;
        //    else
        //        throw new ArgumentNotFoundException($"Bus station of line {station.BusLineKey} and station {station.StationKey} was not found.");
        //}
        //public void UpdateBusLineStation(int line, int stationKey, Action<BusLineStation> update) //method that knows to updt specific fields in Person
        //{
        //    BusLineStation busLineStation = DataSource.ListBusLineStations.Find(s => s.BusLineKey == line && s.StationKey == stationKey);
        //    if (busLineStation != null)
        //        update(busLineStation);
        //    else
        //        throw new ArgumentNotFoundException($"Bus station of line {line} and station {stationKey} was not found.");
        //}
        //public void DeleteBusLineStation(int line, int stationKey)
        //{
        //    BusLineStation busLineStation = DataSource.ListBusLineStations.Find(b => b.BusLineKey == line && b.StationKey == stationKey);
        //    if (busLineStation == null)
        //        throw new ArgumentNotFoundException($"Bus station of line {line} and station {stationKey} was not found.");
        //    DataSource.ListBusLineStations.Remove(busLineStation);
        //}
        //public void DeleteBusLineStationsByStation(int stationKey)
        //{
        //    DataSource.ListBusLineStations.RemoveAll(bls => bls.StationKey == stationKey);
        //}
        //public void DeleteBusLineStationsByLine(int lineKey)
        //{
        //    DataSource.ListBusLineStations.RemoveAll(bls => bls.BusLineKey == lineKey);
        //}
        //#endregion

        #region ConsecutiveStations
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
                           Distance = double.Parse(cs.Element("Distance").Value),
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
                throw new DO.InvalidInformationException("Duplicate stations keys.");

            XElement consecutiveStationsElem = new XElement("ConsecutiveStations", new XElement("StationKey1", consecutiveStations.StationKey1.ToString()),
                                  new XElement("StationKey2", consecutiveStations.StationKey2.ToString()),
                                  new XElement("Distance", consecutiveStations.Distance.ToString()),
                                  new XElement("AverageTime", consecutiveStations.AverageTime.ToString()));
            
            consecutiveStationsRootElem.Add(consecutiveStationsElem);

            XmlTools.SaveListToXMLElement(consecutiveStationsRootElem, consecutiveStationsPath);
        }
        public void AddConsecutiveStations(int stationKey1, int stationKey2)
        {
            if (stationKey1 == stationKey2)
                throw new InvalidInformationException("Duplicate station!");
            Station station1 = GetStation(stationKey1);
            Station station2 = GetStation(stationKey2);
            AddConsecutiveStations(CalculateConsecutiveStations(station1, station2));
        }
        ConsecutiveStations CalculateConsecutiveStations(Station station1, Station station2)
        {
            ConsecutiveStations consecutiveStations = new DO.ConsecutiveStations();
            consecutiveStations.StationKey1 = station1.Key;
            consecutiveStations.StationKey2 = station2.Key;
            GeoCoordinate location1 = new GeoCoordinate(station1.Latitude, station1.Longitude);//מיקום התחנה המחושבת
            GeoCoordinate location2 = new GeoCoordinate(station2.Latitude, station2.Longitude);//מיקום התחנה הקודמת
            double distance = location1.GetDistanceTo(location2);//חישוב מרחק
            consecutiveStations.Distance = distance;
            Random rand = new Random();
            int speed = rand.Next(30, 60);
            int time = (int)Math.Ceiling(distance / (speed * 1000 / 60));//חישוב זמן בהנחה שמהירות האוטובוס היא מספר בין 30 - 60 קמ"ש
            consecutiveStations.AverageTime = time;
            return consecutiveStations;
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


            IEnumerable<XElement> consecutiveStations = (from cs in consecutiveStationsRootElem.Elements()
                                                         where int.Parse(cs.Element("StationKey1").Value) == stationKey ||
                                                         int.Parse(cs.Element("StationKey2").Value) == stationKey
                                                         select cs);            
            foreach(var cs in consecutiveStations)
                cs.Remove(); //<==>   Remove consecutive stations from consecutiveStationsRootElem

            XmlTools.SaveListToXMLElement(consecutiveStationsRootElem, consecutiveStationsPath);
        }
        #endregion

        //#region LineSchedule
        //public LineSchedule GetLineSchedule(int line, TimeSpan startTime)
        //{
        //    LineSchedule lineSchedule = DataSource.ListLineSchedules.FirstOrDefault(ls => ls.LineKey == line && ls.StartTime == startTime);
        //    if (lineSchedule == null)
        //        throw new DO.ArgumentNotFoundException($"Line schedule of line {line} and starting time {startTime.ToString("HH:mm")} not found.");
        //    return lineSchedule.Clone();
        //}
        //public IEnumerable<LineSchedule> GetAllLineSchedules()
        //{
        //    var lineSchedules = from ls in DataSource.ListLineSchedules
        //                        select ls;
        //    return lineSchedules;
        //}
        //public IEnumerable<LineSchedule> GetAllLineSchedulesOfLine(int Line)
        //{
        //    var lineSchedules = from ls in DataSource.ListLineSchedules
        //                        where ls.LineKey == Line
        //                        select ls;
        //    return lineSchedules;
        //}
        //public void AddLineSchedule(LineSchedule lineSchedule)
        //{
        //    if (DataSource.ListLineSchedules.FirstOrDefault(ls => ls.LineKey == lineSchedule.LineKey && ls.StartTime == lineSchedule.StartTime) != null)
        //        throw new InvalidInformationException($"There is already a line schedule {lineSchedule.LineKey} that start at {lineSchedule.StartTime.ToString()}");
        //    DataSource.ListLineSchedules.Add(lineSchedule);
        //}
        //public void UpdateLineSchedule(LineSchedule lineSchedule)
        //{
        //    int indexOfLineScheduleToUpdate = DataSource.ListLineSchedules.FindIndex(ls => ls.LineKey == lineSchedule.LineKey && ls.StartTime.ToString("HH:mm") == lineSchedule.StartTime.ToString("HH:mm"));
        //    if (indexOfLineScheduleToUpdate == -1)
        //        throw new ArgumentNotFoundException($"Line schedule of line {lineSchedule.LineKey} and starting time {lineSchedule.StartTime.ToString("HH:mm")} not found.");

        //    DataSource.ListLineSchedules[indexOfLineScheduleToUpdate] = lineSchedule;
        //}
        //public void UpdateLineSchedule(int line, TimeSpan startTime, Action<LineSchedule> update)
        //{
        //    LineSchedule lineSchedule = GetLineSchedule(line, startTime);
        //    update(lineSchedule);
        //}
        //public void DeleteLineSchedule(int line, TimeSpan startTime)
        //{
        //    LineSchedule lineSchedule = GetLineSchedule(line, startTime);
        //    DataSource.ListLineSchedules.Remove(lineSchedule);
        //}
        //#endregion

        #region Station
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
            station.Key = Station.STATION_KEY++;
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

        //#region User

        //public User GetUser(string userName)
        //{
        //    User user = DataSource.ListUsers.Find(s => s.UserName == userName);
        //    if (user != null)
        //        return user.Clone();
        //    else
        //        throw new ArgumentNotFoundException($"User not found with user name: {userName}");
        //}
        //public User GetUser(string userName, string password)
        //{
        //    User user = DataSource.ListUsers.Find(s => s.UserName == userName && s.Password == password);
        //    if (user != null)
        //        return user.Clone();
        //    else
        //        throw new ArgumentNotFoundException($"User not found with user name: {userName}");
        //}
        //public IEnumerable<User> GetAllUsers()
        //{
        //    var AllUsers = from user in DataSource.ListUsers
        //                   select user.Clone();
        //    return AllUsers;
        //}
        //public void AddUser(User user)
        //{
        //    if (DataSource.ListUsers.FirstOrDefault(s => s.UserName == user.UserName) != null)
        //        throw new InvalidInformationException("Duplicate user name");
        //    DataSource.ListUsers.Add(user.Clone());
        //}
        //public void UpdateUser(User user)
        //{
        //    int indexOfUserToUpdate = DataSource.ListUsers.FindIndex(s => s.UserName == user.UserName);
        //    if (indexOfUserToUpdate >= 0)
        //        DataSource.ListUsers[indexOfUserToUpdate] = user;
        //    else
        //        throw new ArgumentNotFoundException($"User not found with license number: {user.UserName}");
        //}
        //public void UpdateUser(string userName, Action<User> update)
        //{
        //    int indexOfUserToUpdate = DataSource.ListUsers.FindIndex(s => s.UserName == userName);
        //    if (indexOfUserToUpdate >= 0)
        //    {
        //        update(DataSource.ListUsers[indexOfUserToUpdate]);
        //    }
        //    else
        //        throw new ArgumentNotFoundException($"User not found with user name: {userName}");
        //}
        //public void DeleteUser(string userName)
        //{
        //    User user = DataSource.ListUsers.Find(b => b.UserName == userName);
        //    if (user == null)
        //        throw new ArgumentNotFoundException($"User not found with user name: {userName}");
        //    DataSource.ListUsers.Remove(user);
        //}
        //#endregion

    }
}