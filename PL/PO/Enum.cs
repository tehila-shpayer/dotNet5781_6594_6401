using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    /// <summary>
    /// טיפוס לייצוג סטטוס של אוטובוס פיזי
    /// </summary>
    public enum Status { Ready, NotReady, Driving, Refueling, Treatment }
    /// <summary>
    /// טיפוס לייצוג איזורים של קוי אוטובוס
    /// </summary>
    public enum Areas { Center, General, Hifa, Jerusalem, North, South, TelAviv, YehudaAndShomron };
    /// <summary>
    /// טיפוס לייצוג הרשאת גישה של משתמש
    /// </summary>
    public enum AuthorizationManagement { Manager, Traveler }

}
