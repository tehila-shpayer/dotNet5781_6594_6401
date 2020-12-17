using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class UserTravel
    {
        public string UserName { get; set; }
        public int BusLineKey { get; set; }
        public int UpStationKey { get; set; } 
        public int DownStationKey { get; set; }
        public string UpTime { get; set; }
        public string DownTime { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty<UserTravel>();
        }

    }
}
