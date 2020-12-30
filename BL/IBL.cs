using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLAPI
{
    public interface IBL
    {
        #region BusLine
        BusLine GetBusLine();
        IEnumerable<BusLine> GetAllBusLines();
        void AddBusLine(BusLine busline);
        void AddStationToLine(BusLineStation sKey);
        void AddStationToLine(int sKey, int position);
        void DeleteBusLine(BusLine busLine);
        void DeleteBusLine(int busLineNumber);
        void DeleteStationFromLine(BusLineStation bls);
        void DeleteStationFromLine(int key);
        void UpdateBusLine(BusLine busline);
        #endregion

    }
}
