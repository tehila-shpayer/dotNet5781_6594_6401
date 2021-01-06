using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusLine
    {
        public static int BUS_LINE_KEY = 1794;
        public int Key { get; set; }
        public int LineNumber { get;set; }
        public Areas Area { get; set; }
        public bool _isActive = true;
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
