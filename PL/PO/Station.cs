using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BO
{
    public class Station : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Key
        {
            get { return Key; }
            set
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Key"));
                }
            }
        }
        public double Latitude
        {
            get { return Latitude; }
            set
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Latitude"));
                }
            }
        }
        public double Longitude
        {
            get { return Longitude; }
            set
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Longitude"));
                }
            }
        }
        public String Name
        {
            get { return Name; }
            set
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }
        public IEnumerable<int> BusLines
        {
            get { return BusLines; }
            set
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("BusLines"));
                }
            }
        }

        public bool _isActive = true;
        public bool IsActive { get; set; }
    }
}
