using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    ///לייצוג סטטוס של אוטובוס פיזי Enum טיפוס 
    /// </summary>
    public enum Status { Ready, NotReady, Driving, Refueling, Treatment }
    /// <summary>
    ///לייצוג איזורים של קווי אוטובוס Enum טיפוס 
    /// </summary>
    public enum Areas { Center, General, Hifa, Jerusalem, North, South, TelAviv, YehudaAndShomron };
    /// <summary>
    ///לייצוג הרשאת גישה של משתמש Enum טיפוס 
    /// </summary>
    public enum AuthorizationManagement { Manager, Traveler }

}
