#define SAMPLEVALUE
using AirlineEntities;
using AirlineGeneric.Manager;
using AirlineGeneric.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirlineGeneric
{
    class Airline
    {
        List<Flight> FlightList;
        IManager FlightsManager = new FlightManage();
        IManager PassengerManager = new PassengerManage();
        //IArrangeList ListManager = new FlightManage();

        const int ClearLineConsole = 37;

        public Airline(byte flightsCount) {
            FlightList = new List<Flight>(flightsCount);

            #region insert sample value
#if (SAMPLEVALUE)
            for (int i = 1; i <= flightsCount; i++) {
                FlightList.Add(new Flight("sample"));
                Thread.Sleep(100);
            }
            FlightList.Sort();
#endif
            #endregion
        }

        public void ManagerMenu(string FlightOrPassenger) {
            bool isEnter = true;
            do {
                Console.CursorLeft = 30;
                Console.CursorTop = 0;
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (FlightOrPassenger == "flight")
                    Console.WriteLine("CONTROL MENU FLIGHTS       |");
                if (FlightOrPassenger == "passenger")
                    Console.WriteLine("CONTROL MENU PASSENGERS    |");
                Console.CursorLeft = 30;
                Console.WriteLine("---------------------------|");
                Console.CursorLeft = 30;
                Console.WriteLine("1 - Print                  |");
                Console.CursorLeft = 30;
                Console.WriteLine("2 - Add                    |");
                Console.CursorLeft = 30;
                Console.WriteLine("3 - Delete                 |");
                Console.CursorLeft = 30;
                Console.WriteLine("4 - Edit                   |");
                Console.CursorLeft = 30;
                Console.WriteLine("5 - Search                 |");
                Console.CursorLeft = 30;
                Console.WriteLine("Any key - Back to MAIN menu|");
                Console.CursorLeft = 30;
                Console.WriteLine("---------------------------|");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (FlightOrPassenger == "flight") {
                    switch (Console.ReadKey().KeyChar) {
                        case '1':
                            FlightsManager.PrintList(FlightList);
                            Console.WriteLine("Press any key to back menu");
                            Console.ReadKey();
                            CleanerManager.ClearConsole(ClearLineConsole);
                            break;
                        case '2':
                            if (FlightsManager.AddToList(FlightList)) {
                                Console.WriteLine("Flight successfully added");
                                FlightsManager.PrintList(FlightList);
                            } else
                                Console.WriteLine("Flight not added");
                            Console.WriteLine("Press any key to back menu");
                            Console.ReadKey();
                            CleanerManager.ClearConsole(ClearLineConsole);
                            break;
                        case '3':
                            FlightsManager.PrintList(FlightList);
                            if (FlightsManager.DeleteFromList(FlightList))
                                Console.WriteLine("Flight successfully deleted");
                            else
                                Console.WriteLine("Flight NOT deleted");
                            Console.WriteLine("Press any key to back menu");
                            Console.ReadKey();
                            CleanerManager.ClearConsole(ClearLineConsole);
                            break;
                        case '4':
                            FlightsManager.PrintList(FlightList);
                            if (FlightsManager.EditList(FlightList))
                                Console.WriteLine("Flight successfully modified");
                            else
                                Console.WriteLine("Flight NOT modified");
                            Console.WriteLine("Press any key to back menu");
                            Console.ReadKey();
                            CleanerManager.ClearConsole(ClearLineConsole);
                            break;
                        case '5':
                            AirlineSearch.SearchMenu(FlightList);
                            CleanerManager.ClearSearchMenu();
                            CleanerManager.ClearConsole(ClearLineConsole);
                            break;
                        default:
                            Console.Clear();
                            isEnter = false;
                            break;
                    }
                }
                if (FlightOrPassenger == "passenger") {
                    switch (Console.ReadKey().KeyChar) {
                        case '1':
                            FlightsManager.PrintList(FlightList);
                            PassengerManager.PrintList(FlightList);
                            Console.WriteLine("Press any key to back menu");
                            Console.ReadKey();
                            CleanerManager.ClearConsole(ClearLineConsole);
                            break;
                        case '2':
                            FlightsManager.PrintList(FlightList);
                            if (PassengerManager.AddToList(FlightList)) {
                                Console.WriteLine("Passenger successfully added");
                            } else
                                Console.WriteLine("Passenger not added");
                            Console.WriteLine("Press any key to back menu");
                            Console.ReadKey();
                            CleanerManager.ClearConsole(ClearLineConsole);
                            break;
                        case '3':
                            FlightsManager.PrintList(FlightList);
                            if (PassengerManager.DeleteFromList(FlightList))
                                Console.WriteLine("Passenger successfully deleted");
                            else
                                Console.WriteLine("Passenger NOT deleted");
                            Console.WriteLine("Press any key to back menu");
                            Console.ReadKey();
                            CleanerManager.ClearConsole(ClearLineConsole);
                            break;
                        case '4':
                            FlightsManager.PrintList(FlightList);
                            if (PassengerManager.EditList(FlightList))
                                Console.WriteLine("Passenger successfully modified");
                            else
                                Console.WriteLine("Passenger NOT modified");
                            Console.WriteLine("Press any key to back menu");
                            Console.ReadKey();
                            CleanerManager.ClearConsole(ClearLineConsole);
                            break;
                        case '5':
                            AirlineSearch.SearchMenu(FlightList);
                            CleanerManager.ClearSearchMenu();
                            CleanerManager.ClearConsole(35);
                            break;
                        default:
                            Console.Clear();
                            isEnter = false;
                            break;
                    }
                }
            } while (isEnter);
        }
    }
}
