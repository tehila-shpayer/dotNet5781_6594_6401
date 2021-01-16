using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;

namespace BO
{
    class TravelsSimulator
    {
        BackgroundWorker travels; 
        public TravelsSimulator()
        {
            //travels = new BackgroundWorker();
            //simulatorClock = new Clock(startTime, simulatorRate);
            //travels.DoWork += Travels_DoWork;
            //travels.ProgressChanged += Travels_ProgressChanged;
            //travels.RunWorkerCompleted += Travels_RunWorkerCompleted;
            //travels.WorkerReportsProgress = true;
            //simulatorClock.updateTime += updateTime;
            //travels.RunWorkerAsync(travels);
        }
    }
   
    //int GetTimeFromFirstStationInMillySeconds(BusLineStation bls)
    //{
    //    int timeInMinutes = 0;
    //    Stopwatch stopwatch = new Stopwatch();
    //    BusLineStation tempBls = new BusLineStation();
    //    for (int i = 2; i <= bls.Position; i++)
    //    {
    //        tempBls = GetBusLineStation(bls.BusLineKey, i);
    //        timeInMinutes += tempBls.TravelTimeFromLastStationMinutes;
    //    }
    //    return timeInMinutes * 1000 * 60;
    //}


    //private void Travels_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    //{
    //    throw new NotImplementedException();
    //}

    //private void Travels_ProgressChanged(object sender, ProgressChangedEventArgs e)
    //{
    //    throw new NotImplementedException();
    //}

    //private void Travels_DoWork(object sender, DoWorkEventArgs e)
    //{
    //    Thread.Sleep(30);
    //}

    //public List<BusInTravel> GetLineTimingsPerStation(int stationKey, TimeSpan startTime)
    //{
    //    Station station = GetStation(stationKey);
    //    List<BusInTravel> busInTravels = new List<BusInTravel>();
    //    foreach (int line in station.BusLines)
    //    {
    //        foreach (LineSchedule ls in GetAllLineSchedulesOfLine(line))
    //        {
    //            if (ls.StartTime.TotalSeconds <= startTime.TotalSeconds && startTime.TotalSeconds <= ls.EndTime.TotalSeconds)
    //            {
    //                BusLineStation bls = GetBusLineStationByKey(ls.LineKey, stationKey);
    //                String lastStationName = GetStation(GetBusLine(ls.LineKey).LastStation).Name;
    //                int frequenciesCount = Convert.ToInt32((ls.EndTime.TotalMinutes - startTime.TotalMinutes) / ls.Frequency) + 1;
    //                for (int i = 0; i < frequenciesCount; i++)
    //                {
    //                    BusInTravel bit = new BusInTravel();
    //                    Random rand = new Random();
    //                    var buses = GetAllBuses();
    //                    int busIndex = rand.Next(0, buses.Count());
    //                    string licenseNumber = buses.ElementAt(busIndex).LicenseNumber;
    //                    bit.Key = BusInTravel.BUS_TRAVEL_KEY++;
    //                    bit.BusLicenseNumber = licenseNumber;
    //                    bit.LineKey = ls.LineKey;
    //                    bit.StationKey = stationKey;
    //                    bit.StartTime = new TimeSpan(startTime.Ticks + ls.Frequency * i * TimeSpan.TicksPerMinute);
    //                    bit.LastStationName = lastStationName;
    //                    bit.TimeLeft = new TimeSpan(GetTimeFromFirstStationInMillySeconds(bls) * TimeSpan.TicksPerMillisecond);
    //                }
    //            }
    //        }
    //    }
    //    return busInTravels;
    //}


}
