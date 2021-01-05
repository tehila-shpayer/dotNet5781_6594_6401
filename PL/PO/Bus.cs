using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus
    {
        //Bus properties:
        public string LicenseNumber { get; set; }
        public DateTime RunningDate { get; set; }
        public DateTime LastTreatment { get; set; }
        public int Fuel { get; set; }
        public int KM { get; set; }
        public int BeforeTreatKM { get; set; }
        public Status Status { get; set; }
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
