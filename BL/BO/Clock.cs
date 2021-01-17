using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;


namespace BO
{
    class Clock
    {
        #region singelton
        static readonly Clock instance = new Clock();
        static Clock() { }// static ctor to ensure instance init is done just before first usage
        Clock() { } // default => private
        public static Clock Instance { get => instance; }// The public Instance property to use
        #endregion

        public event Action<TimeSpan> ClockObserver;
        public TimeSpan Time { get; set; }
        public TimeSpan startTime { get; set; }
        public int rate { get; set; }
        internal volatile bool IsTimerRun;
        public Clock(TimeSpan tsStartTime, int simulatorRate)
        {
            Time = tsStartTime;
            IsTimerRun = true;
            rate = simulatorRate;
        }
        public void UpdateTime(TimeSpan ts)
        {
            ClockObserver(ts);
        }
    }
}
