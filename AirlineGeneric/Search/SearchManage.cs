using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineEntities;
using AirlineGeneric.Manager;

namespace AirlineGeneric.Search
{
    class SearchManage : ISearchManager {

        #region Search passengers on flight number
        public void SearchFlightNumber(Flight[] FlightList) {
            IManager FlightsManager = new FlightManage();
            IManager PassengerManager = new PassengerManage();
            bool isEnter = true, isFind = false;
            int EnterFlightNumber = 0, tempFlightNumber;
            int stepItem = 0;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter flight number: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                bool isCorrectValue = int.TryParse(Console.ReadLine(), out tempFlightNumber);
                if (isCorrectValue) {
                    foreach (Flight item in FlightList) {
                        if (item != null && item.FlightNumber != null) {
                            if (item.FlightNumber == tempFlightNumber) {
                                isEnter = false;
                                isFind = true;
                                EnterFlightNumber = stepItem;
                                PassengerManager.PrintList(FlightList, EnterFlightNumber);
                            }
                        }
                        stepItem++;
                    }
                    if (!isFind) {
                        Console.WriteLine("Flight number not found.");
                        Console.ReadKey();
                        isEnter = false;
                    }
                } else
                    Console.WriteLine("The number is not digit");
            } while (isEnter);

            if (isFind) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("What to do with the results: (E)dit, (D)elete, or nothing(Enter) ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                switch (Console.ReadKey().KeyChar) {
                    case 'e':
                    case 'E':
                        Console.WriteLine();
                        if (PassengerManager.EditList(FlightList, EnterFlightNumber))
                            Console.WriteLine("Passenger successfully modified");
                        else
                            Console.WriteLine("Passenger NOT modified");
                        Console.WriteLine("Press any key to back menu");
                        Console.ReadKey();
                        CleanerManager.ClearConsole(35);
                        break;
                    case 'd':
                    case 'D':
                        Console.WriteLine();
                        if (PassengerManager.DeleteFromList(FlightList, EnterFlightNumber))
                            Console.WriteLine("Passenger successfully deleted");
                        else
                            Console.WriteLine("Passenger NOT deleted");
                        Console.WriteLine("Press any key to back menu");
                        Console.ReadKey();
                        CleanerManager.ClearConsole(35);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Search flight lower economy ticket
        public void SearchFlightTicket(Flight[] FlightList) {
            int tempFlightTicket;
            Console.WriteLine("Search only for flights in condition 'CheckIn' or 'Delayed'");
            bool isEnter = true, isFind = false;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter the maximum price per economy ticket: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                bool isCorrectValue = int.TryParse(Console.ReadLine(), out tempFlightTicket);
                if (isCorrectValue) {
                    foreach (Flight item in FlightList) {
                        if (item != null && (item.GetFlightStatus() == "CheckIn" || item.GetFlightStatus() == "Delayed")) {
                            if (item.FlightFreePlace > 0) {
                                if (tempFlightTicket >= item.FlightPriceEconomy) {
                                    if (!isFind) {
                                        Console.WriteLine("---------------------------------------------------------------------------------------");
                                        Console.WriteLine("|Direction|    Date/Time   |Number|     City      |Terminal/Gate|  Status  |Free places|");
                                        Console.WriteLine("|---------|----------------|------|---------------|-------------|----------|-----------|");
                                    }
                                    Console.WriteLine($"=== Ticket price economy: {item.FlightPriceEconomy,13:C}");
                                    Console.WriteLine(item.ToString());
                                    isFind = true;
                                }
                            }
                        }
                    }
                    if (isFind) {
                        Console.WriteLine("----------------------------------------------------------------------------------------");
                        isEnter = false;
                    } else {
                        Console.WriteLine("No matching flights.");
                        isEnter = false;
                    }
                } else
                    Console.WriteLine("The entered price is not digit");
            } while (isEnter);
        }
        #endregion

        #region Search passenger by name or surname
        public void SearchPassengerName(Flight[] FlightList) {
            bool isEnter = true, isFind = false;
            string insertPassengerPartName;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Search on all flights partial matches or last name");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter the name or part of it: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                insertPassengerPartName = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(insertPassengerPartName)) {
                    foreach (Flight itemFlight in FlightList) {
                        if (itemFlight != null) {
                            foreach (Passenger itemPassenger in itemFlight.PassengersList) {
                                if (itemPassenger != null) {
                                    string ippf = itemPassenger.PassengerFirstname.ToUpper();
                                    string ippl = itemPassenger.PassengerLastname.ToUpper();
                                    if (ippf.Contains(insertPassengerPartName.ToUpper()) ||
                                        ippl.Contains(insertPassengerPartName.ToUpper())) {
                                        if (!isFind) {
                                            Console.WriteLine("---------------------------------------------------------------------------------------------------");
                                            Console.WriteLine("| NF|   FirsName   |    LastName    | Birthday |Passport|  Sex  |Nationality| Class  | Cost ticket |");
                                            Console.WriteLine("|---|--------------|----------------|----------|--------|-------|-----------|--------|-------------|");
                                        }
                                        isFind = true;
                                        Console.Write($"|{itemFlight.FlightNumber,-3}");
                                        Console.WriteLine(itemPassenger.ToString());
                                    }
                                }
                            }
                            isEnter = false;

                        }
                    }
                    if (isFind)
                        Console.WriteLine("---------------------------------------------------------------------------------------------------");
                    else
                        Console.WriteLine("Matching names not found");
                } else {
                    Console.WriteLine("Entering an empty search canceled");
                    isEnter = false;
                }
            } while (isEnter);
        }
        #endregion

        #region Search passenger's passport number
        public void SearchPassengerPasspower(Flight[] FlightList) {
            bool isEnter = true, isFind = false;
            string insertPassengerPartPassport;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Search on all flights partial matches or last name");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter the passpower or part of it: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                insertPassengerPartPassport = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(insertPassengerPartPassport)) {
                    foreach (Flight itemFlight in FlightList) {
                        if (itemFlight != null) {
                            foreach (Passenger itemPassenger in itemFlight.PassengersList) {
                                if (itemPassenger != null) {
                                    string ippp = itemPassenger.PassengerPasspower.ToUpper();
                                    if (ippp.Contains(insertPassengerPartPassport.ToUpper())) {
                                        if (!isFind) {
                                            Console.WriteLine("---------------------------------------------------------------------------------------------------");
                                            Console.WriteLine("| NF|   FirsName   |    LastName    | Birthday |Passport|  Sex  |Nationality| Class  | Cost ticket |");
                                            Console.WriteLine("|---|--------------|----------------|----------|--------|-------|-----------|--------|-------------|");
                                        }
                                        isFind = true;
                                        Console.Write($"|{itemFlight.FlightNumber,-3}");
                                        Console.WriteLine(itemPassenger.ToString());
                                    }
                                }
                            }
                            isEnter = false;

                        }
                    }
                    if (isFind)
                        Console.WriteLine("---------------------------------------------------------------------------------------------------");
                    else
                        Console.WriteLine("Matching names not found");
                } else {
                    Console.WriteLine("Entering an empty search canceled");
                    isEnter = false;
                }
            } while (isEnter);
        }
        #endregion
    }
}

