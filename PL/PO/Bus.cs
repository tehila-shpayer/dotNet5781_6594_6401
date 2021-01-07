using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
namespace PL
{
    public class Bus : DependencyObject
    {
        static readonly DependencyProperty LicenseNumberProperty = DependencyProperty.Register("LicenseNumber", typeof(String), typeof(Bus));
        //static readonly DependencyProperty RunningDateProperty = DependencyProperty.Register("RunningDate", typeof(DateTime), typeof(Bus));
        //static readonly DependencyProperty LastTreatmentProperty = DependencyProperty.Register("LastTreatment", typeof(DateTime), typeof(Bus));
        static readonly DependencyProperty RunningDateFormatProperty = DependencyProperty.Register("RunningDateFormat", typeof(String), typeof(Bus));
        static readonly DependencyProperty LastTreatmentFormatProperty = DependencyProperty.Register("LastTreatmentFormat", typeof(String), typeof(Bus));
        static readonly DependencyProperty FuelProperty = DependencyProperty.Register("Fuel", typeof(int), typeof(Bus));
        static readonly DependencyProperty KMProperty = DependencyProperty.Register("KM", typeof(int), typeof(Bus));
        static readonly DependencyProperty BeforeTreatKMProperty = DependencyProperty.Register("BeforeTreatKM", typeof(int), typeof(Bus));
        static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(BO.Status), typeof(Bus));

        static readonly DependencyProperty LicenseNumberFormatProperty = DependencyProperty.Register("LicenseNumberFormat", typeof(String), typeof(Bus));
        public string LicenseNumber { get => (String)GetValue(LicenseNumberProperty); set => SetValue(LicenseNumberProperty, value); }
        //public DateTime RunningDate { get => (DateTime)GetValue(RunningDateProperty); set => SetValue(RunningDateProperty, value); }
        //public DateTime LastTreatment { get => (DateTime)GetValue(LastTreatmentProperty); set => SetValue(LastTreatmentProperty, value); }
        public String RunningDateFormat { get => (String)GetValue(RunningDateFormatProperty); set => SetValue(RunningDateFormatProperty, value); }
        public String LastTreatmentFormat { get => (String)GetValue(LastTreatmentFormatProperty); set => SetValue(LastTreatmentFormatProperty, value); }
        public int Fuel { get => (int)GetValue(FuelProperty); set => SetValue(FuelProperty, value); }
        public int KM
        {
            get => (int)GetValue(KMProperty); set => SetValue(KMProperty, value);
        }
        public int BeforeTreatKM { get => (int)GetValue(BeforeTreatKMProperty); set => SetValue(BeforeTreatKMProperty, value); }
        public BO.Status Status { get => (BO.Status)GetValue(StatusProperty); set => SetValue(StatusProperty, value); }

        public string LicenseNumberFormat { get => (String)GetValue(LicenseNumberFormatProperty); set => SetValue(LicenseNumberFormatProperty, value); }
    }
}
