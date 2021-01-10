using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum Status { Ready, NotReady, Driving, Refueling, Treatment }
    public enum Areas { Center, General, Hifa, Jerusalem, North, South, TelAviv, YehudaAndShomron };
    public enum AuthorizationManagement { Manager, Traveler }

}
