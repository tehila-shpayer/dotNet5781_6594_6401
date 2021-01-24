using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace PL
{
    /// <summary>
    /// PLמחלקה לייצוג זמני יציאה של קו אוטובוס בשכבת ה
    /// </summary>
    public class LineSchedule : DependencyObject
    {
        static readonly DependencyProperty LineKeyProperty = DependencyProperty.Register("LineKey", typeof(int), typeof(LineSchedule));
        static readonly DependencyProperty FrequencyProperty = DependencyProperty.Register("Frequency", typeof(int), typeof(LineSchedule));
        static readonly DependencyProperty StartTimeProperty = DependencyProperty.Register("StartTime", typeof(TimeSpan), typeof(LineSchedule));
        static readonly DependencyProperty EndTimeProperty = DependencyProperty.Register("EndTime", typeof(TimeSpan), typeof(LineSchedule));
        static readonly DependencyProperty LineNumberProperty = DependencyProperty.Register("LineNumber", typeof(int), typeof(LineSchedule));
        public int LineKey { get => (int)GetValue(LineKeyProperty); set => SetValue(LineKeyProperty, value); }
        public int Frequency { get => (int)GetValue(FrequencyProperty); set => SetValue(FrequencyProperty, value); }
        public TimeSpan StartTime { get => (TimeSpan)GetValue(StartTimeProperty); set => SetValue(StartTimeProperty, value); }
        public TimeSpan EndTime { get => (TimeSpan)GetValue(EndTimeProperty); set => SetValue(EndTimeProperty, value); }
        public int LineNumber { get => (int)GetValue(LineNumberProperty); set => SetValue(LineNumberProperty, value); }
    }
}
