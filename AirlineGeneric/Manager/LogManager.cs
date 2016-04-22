using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AirlineGeneric.Manager
{
    class LogManager
    {
        
        public static void WriteToLog(string message)
        {
            TraceSource ts = new TraceSource("TraceSourceApp");
            ts.TraceEvent(TraceEventType.Information, 0, message);
            ts.Flush();
            ts.Close();
        }
    }
}
