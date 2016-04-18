using AirlineEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AirlineGeneric.Manager
{
    interface IManager
    {
        void PrintList(Flight[] FlightList, int enterFlightNumber = -1);
        bool AddToList(Flight[] FlightList);
        bool DeleteFromList(Flight[] FlightList, int enterFlightNumber = -1);
        bool EditList(Flight[] FlightList, int enterFlightNumber = -1);
    }
}
