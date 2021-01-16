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

        public event Action<TimeSpan> updateTime;
        public TimeSpan time;
        internal volatile bool IsTimerRun;
        public BackgroundWorker timer;
        public Stopwatch stopwatch;
        int rate;
        public Clock(TimeSpan tsStartTime, int simulatorRate)
        {
            rate = simulatorRate;
            time = tsStartTime;
            timer = new BackgroundWorker();
            stopwatch = new Stopwatch();
            timer.DoWork += Timer_DoWork;
            timer.ProgressChanged += Timer_ProgressChanged;
            timer.RunWorkerCompleted += Timer_RunWorkerCompleted;
            timer.WorkerReportsProgress = true;
            updateTime(time);
            IsTimerRun = true;
            timer.RunWorkerAsync();
           // stopwatch.Restart();
        }

        private void Timer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void Timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            time.Add(new TimeSpan(e.ProgressPercentage));
            updateTime(time);
        }

        private void Timer_DoWork(object sender, DoWorkEventArgs e)
        {
            while(IsTimerRun)
            {
                Thread.Sleep(1000);
                timer.ReportProgress(Convert.ToInt32(rate * TimeSpan.TicksPerSecond));
            }
        }
    }
}
