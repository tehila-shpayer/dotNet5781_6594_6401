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
    /// <summary>
    /// מחלקה לייצוג שעון סימולציה
    /// השעון מושקף ע"י הממשק הגרפי
    /// הוא מופעל ע"י תהליכון בעת הפעלת הסימולציה
    /// </summary>
    public class ValueChangedEventArgs : EventArgs
    {
        public readonly TimeSpan Time;
        public ValueChangedEventArgs(TimeSpan time)
        {
            Time = time;
        }
    }
    public delegate void TimeChangedEventHandler(Object sender, ValueChangedEventArgs args);

    public class Clock
    {
        #region singelton
        static readonly Clock instance = new Clock();
        static Clock() { }// static ctor to ensure instance init is done just before first usage
        Clock() { } // default => private
        public static Clock Instance { get => instance; }// The public Instance property to use
        #endregion

        TimeSpan _time;

        public event Action<TimeSpan> ClockObserver;
        public event TimeChangedEventHandler TimeChanged;
        virtual protected void OnValueChanged(ValueChangedEventArgs args)
        {
            if (TimeChanged != null)
            {
                TimeChanged(this, args);
            }
        }

        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                _time = value;
                ValueChangedEventArgs args = new ValueChangedEventArgs(Time);
                OnValueChanged(args);
            }
        }

        public TimeSpan startTime { get; set; }
        public int rate { get; set; }

        internal volatile bool IsTimerRun;
        public Clock(TimeSpan tsStartTime, int simulatorRate)
        {
            startTime = tsStartTime;
            Time = tsStartTime;
            IsTimerRun = true;
            rate = simulatorRate;
        }
    }
}
