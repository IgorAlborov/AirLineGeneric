using AirlineEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineGeneric.Manager
{
    interface IArrangeList
    {
        void ArrangeList(Flight[] FlightList);
    }
}
