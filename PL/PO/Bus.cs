using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PL
{
    public class Bus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //Bus properties:
        public string LicenseNumber
        {
            get { return LicenseNumber; }
            set
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LicenseNumber"));
                }
            }
        }
        public DateTime RunningDate { get; set; }
        public DateTime LastTreatment
        {
            get { return LastTreatment; }
            set
            {

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LastTreatment"));
                }
            }
        }
        public int Fuel { get; set; }
        public int KM { get; set; }
        public int BeforeTreatKM { get; set; }
        public BO.Status Status { get; set; }
        public bool IsActive { get; set; }
    }
}
