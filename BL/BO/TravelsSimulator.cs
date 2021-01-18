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
        Clock clock;
        public TravelsSimulator(Clock _clock)
        {
            travels = new BackgroundWorker();
            clock = _clock;
            clock.TimeChanged += TimeChange;
            travels.DoWork += Travels_DoWork;
            travels.ProgressChanged += Travels_ProgressChanged;
            travels.RunWorkerCompleted += Travels_RunWorkerCompleted;
            travels.WorkerReportsProgress = true;
            travels.RunWorkerAsync();
        }
        public void TimeChange(Object sender, BO.ValueChangedEventArgs temp)
        {
            clock.Time = temp.Time;
        }


        private void Travels_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Travels_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Travels_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(30);
        }


    }

}
