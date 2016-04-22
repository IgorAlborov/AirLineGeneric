using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using AirlineGeneric.Manager;

namespace AirlineGeneric
{
    public class Program
    {
        static void Main(string[] args) {

            CultureInfo ci = new CultureInfo("en");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            LogManager.WriteToLog("Start application 'AirlineGeneric'");
            

            //start value flight
            const byte FlightsCount = 5;
            Airline AlborovAirline = new Airline(FlightsCount);
            

            #region Main menu
            bool isEnter = true;
            Console.SetWindowSize(110, 50);
            do {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("============================|");
                Console.WriteLine(@"Please, select an action    |
  1 - Flights management    |
  2 - Passenger management  |
                            |
                            |
                            |
  Q - Exit app              |");
                Console.WriteLine("============================|");
                Console.ForegroundColor = ConsoleColor.DarkGray;


                switch (Console.ReadKey().KeyChar) {
                    case '1':
                        AlborovAirline.ManagerMenu("flight");
                        break;
                    case '2':
                        AlborovAirline.ManagerMenu("passenger");
                        break;
                    case 'q':
                    case 'Q':
                        LogManager.WriteToLog("Quit from application 'AirlineGeneric'");
                        isEnter = false;
                        break;
                    default:
                        break;
                }
            } while (isEnter);
            #endregion


        }
    }
}
