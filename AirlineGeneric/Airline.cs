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

        const int ClearLineConsole = 37;

        public Airline(byte flightsCount)
        {
            FlightList = new List<Flight>(flightsCount);

            #region insert sample value
#if (SAMPLEVALUE)
            for (int i = 1; i <= flightsCount; i++) {
                FlightList.Add(new Flight("sample"));
                Thread.Sleep(100);
            }
            FlightList.Sort();
            LogManager.WriteToLog("Added random values");
#endif
            #endregion
        }

        Action<List<Flight>> printList, printListDouble;
        Func<List<Flight>, bool> addToList, deleteFromList, editList;


        public void ManagerMenu(string FlightOrPassenger)
        {
            string entiti;
            if (FlightOrPassenger == "flight") {
                entiti = "   FLIGHT";
                printList = x => FlightsManager.PrintList(x);
                printListDouble = printList;
                addToList = x => FlightsManager.AddToList(x);
                deleteFromList = x => FlightsManager.DeleteFromList(x);
                editList = x => FlightsManager.EditList(x);
            } else {
                entiti = "PASSENGER";
                printList = x => FlightsManager.PrintList(x);
                printListDouble = printList;
                printListDouble += x => PassengerManager.PrintList(x);
                addToList = x => PassengerManager.AddToList(x);
                deleteFromList = x => PassengerManager.DeleteFromList(x);
                editList = x => PassengerManager.EditList(x);
            }
            bool isEnter = true;
            do {
                Console.CursorLeft = 30;
                Console.CursorTop = 0;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(@"CONTROL MENU {0}S    |", entiti);
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
                switch (Console.ReadKey().KeyChar) {
                    case '1':
                        printListDouble(FlightList);
                        Console.WriteLine("Press any key to back menu");
                        Console.ReadKey();
                        CleanerManager.ClearConsole(ClearLineConsole);
                        break;
                    case '2':
                        printList(FlightList);
                        if (addToList(FlightList)) {
                            Console.WriteLine($"{entiti} successfully added");
                            printList(FlightList);
                        } else
                            Console.WriteLine($"{entiti} not added");
                        Console.WriteLine("Press any key to back menu");
                        Console.ReadKey();
                        CleanerManager.ClearConsole(ClearLineConsole);
                        break;
                    case '3':
                        printList(FlightList);
                        if (deleteFromList(FlightList))
                            Console.WriteLine($"{entiti} successfully deleted");
                        else
                            Console.WriteLine($"{entiti} NOT deleted");
                        Console.WriteLine("Press any key to back menu");
                        Console.ReadKey();
                        CleanerManager.ClearConsole(ClearLineConsole);
                        break;
                    case '4':
                        printList(FlightList);
                        if (editList(FlightList))
                            Console.WriteLine($"{entiti} successfully modified");
                        else
                            Console.WriteLine($"{entiti} NOT modified");
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
            } while (isEnter);
        }
    }
}
