using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    sealed class DalObject : IDL
    {
        static Random rnd = new Random();
        #region singelton
        static readonly DalObject instance = new DalObject();
        static DalObject() { }
        DalObject() => init();
        public static DalObject Instance => instance;
        #endregion

        double temperature;
        object lockDal;
        Thread myThread;
        volatile bool stopFlag = false;

        void init()
        {
            lockDal = new bool?(false);
            temperature = rnd.NextDouble() * 50 - 10;
            (myThread = new Thread(BackgroundAudit)).Start();
        }

        public double GetTemparture(int day)
        {
            temperature += rnd.NextDouble() * 10 - 5;
            return temperature;
        }

        public WindDirection GetWindDirection(int day)
        {
            WindDirection direction = DataSource.directions.Find(d => true);
            var directions = (WindDirections[])Enum.GetValues(typeof(WindDirections));
            direction.direction = directions[rnd.Next(0, directions.Length)];

            return direction.Clone();
        }

        public object GetLock()
        {
            return lockDal;
        }

        public void Shutdown()
        {
            stopFlag = true;
            myThread.Interrupt();
        }

        void BackgroundAudit()
        {
            //Console.WriteLine("Thread start");
            while (!stopFlag)
            {
                try { Thread.Sleep(2000); } catch (ThreadInterruptedException ex) { }
                if (!stopFlag)
                {
                    lock (lockDal)
                    {
                        Thread.Sleep(1000); // non-critical part of work - at start
                        try
                        {
                            //Console.WriteLine("Thread begin processing");
                            Thread.Sleep(1000); // do the critical work
                                                //Console.WriteLine("Thread end processing");
                        }
                        catch (ThreadInterruptedException ex) { }
                    }
                    Thread.Sleep(1000);  // non-critical part of work - at finish
                }
            }
            //Console.WriteLine("Thread finish");
        }
    }
}