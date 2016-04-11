using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineEntities;

namespace AirlineGeneric.Search
{
    class AirlineSearch
    {
        public static void SearchMenu(Flight[] FlightList) {
            ISearchManager SearchManager = new SearchManage();
            bool isEnter = true;
            do {
                Console.CursorLeft = 60;
                Console.CursorTop = 0;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("SEARCH MENU                       |");
                Console.CursorLeft = 60;
                Console.WriteLine("----------------------------------|");
                Console.CursorLeft = 60;
                Console.WriteLine("1 - by flight lower economy ticket|");
                Console.CursorLeft = 60;
                Console.WriteLine("----------------------------------|");
                Console.CursorLeft = 60;
                Console.WriteLine("2 - by flight number of passengers|");
                Console.CursorLeft = 60;
                Console.WriteLine("3 - by name or surname passenger  |");
                Console.CursorLeft = 60;
                Console.WriteLine("4 - passenger's passport number   |");
                Console.CursorLeft = 60;
                Console.WriteLine("Any key - Back to MAIN menu       |");
                Console.CursorLeft = 60;
                Console.WriteLine("----------------------------------|");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                switch (Console.ReadKey().KeyChar) {
                    case '2':
                        SearchManager.SearchFlightNumber(FlightList);
                        ClearConsole(35);
                        break;
                    case '1':
                        SearchManager.SearchFlightTicket(FlightList);
                        Console.WriteLine("Press any key to back menu");
                        Console.ReadKey();
                        ClearConsole(35);
                        break;
                    case '3':
                        SearchManager.SearchPassengerName(FlightList);
                        Console.WriteLine("Press any key to back menu");
                        Console.ReadKey();
                        ClearConsole(35);
                        break;
                    case '4':
                        SearchManager.SearchPassengerPasspower(FlightList);
                        Console.WriteLine("Press any key to back menu");
                        Console.ReadKey();
                        ClearConsole(35);
                        break;
                    default:
                        isEnter = false;
                        break;
                }

            } while (isEnter);

        }



        static void ClearConsole(int line) {
            Console.CursorLeft = 0;
            Console.CursorTop = 9;
            for (int i = 1; i < line; i++) {
                Console.WriteLine("                                                                                                     ");
            }
            Console.CursorLeft = 0;
            Console.CursorTop = 9;
        }
    }
}
