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
        void PrintList(List<Flight> FlightList, int enterFlightNumber = -1);
        bool AddToList(List<Flight> FlightList);
        bool DeleteFromList(List<Flight> FlightList, int enterFlightNumber = -1);
        bool EditList(List<Flight> FlightList, int enterFlightNumber = -1);
    }
}
