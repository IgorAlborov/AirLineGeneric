using AirlineEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineGeneric.Search
{
    interface ISearchManager
    {
        void SearchFlightNumber(List<Flight> FlightList);
        void SearchFlightTicket(List<Flight> FlightList);
        void SearchPassengerName(List<Flight> FlightList);
        void SearchPassengerPasspower(List<Flight> FlightList);
    }
}
