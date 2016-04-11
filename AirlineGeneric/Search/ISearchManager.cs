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
        void SearchFlightNumber(Flight[] FlightList);
        void SearchFlightTicket(Flight[] FlightList);
        void SearchPassengerName(Flight[] FlightList);
        void SearchPassengerPasspower(Flight[] FlightList);
    }
}
