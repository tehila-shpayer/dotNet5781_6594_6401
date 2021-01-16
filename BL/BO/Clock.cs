using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BO
{
    class Clock
    {
        public event EventHandler updateTime;
        public TimeSpan CurrentTime;
        internal volatile bool Cancel;
        Stopwatch StopWatcher;
    }
}
