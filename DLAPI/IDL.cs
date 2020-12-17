using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DLAPI
{
    public interface IDL
    {
        double GetTemparture(int day);
        WindDirection GetWindDirection(int day);

        object GetLock();
        void Shutdown();
    }
}