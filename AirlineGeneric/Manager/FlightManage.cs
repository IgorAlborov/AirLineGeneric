using AirlineEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirlineGeneric.Manager
{
    class FlightManage:IManager,IArrangeList
    {
        #region Print Flights table
        public void PrintList(Flight[] FlightList, int enterFlightNumber = 0) {
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("|Direction|    Date/Time   |Number|     City      |Terminal/Gate|  Status  |Free places|");
            Console.WriteLine("|---------|----------------|------|---------------|-------------|----------|-----------|");
            foreach (Flight item in FlightList) {
                if (item != null) {
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine("----------------------------------------------------------------------------------------");

        }
        #endregion

        #region Sort flights table
        public void ArrangeList(Flight[] FlightList) {
            Flight temp = new Flight();
            for (int i = 0; i < FlightList.Length; i++) {
                for (int j = 0; j < FlightList.Length - 1; j++) {
                    if (FlightList[j] == null && FlightList[j + 1] != null) {
                        temp = FlightList[j];
                        FlightList[j] = FlightList[j + 1];
                        FlightList[j + 1] = temp;
                    }
                }
            }

        }
        #endregion

        #region Add flight
        public bool AddToList(Flight[] FlightList) {
            int FreeCell = -1;
            for (int i = 0; i < FlightList.Length; i++) {
                if (FlightList[i] == null) {
                    FreeCell = i;
                    break;
                }
            }
            if (FreeCell >= 0) {
                byte insertFlightDirection = 0, insertFlightStatus = 0;
                DateTime DateStart = DateTime.Now, DateEnd = DateTime.Now.AddYears(1), insertFlightDate;
                int insertFlightNumber, insertFlightGate, insertFlightPriceBusiness, insertFlightPriceEconomy;
                string insertFlightCity;
                char insertFlightTerminal;

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Select direction flight (A)rrival, (D)eparture");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                bool isCorrect = true;
                do {
                    switch (Console.ReadKey().KeyChar) {
                        case 'a':
                        case 'A':
                            insertFlightDirection = 0;
                            isCorrect = false;
                            break;
                        case 'd':
                        case 'D':
                            insertFlightDirection = 1;
                            isCorrect = false;
                            break;
                        default:
                            Console.Write("Press 'A' or 'D'");
                            break;
                    }
                } while (isCorrect);
                Console.WriteLine();

                isCorrect = true;
                do {
                    CleanerManager.CheckBorder();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Enter date (format YYYY-MM-DD hh:mm): ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    bool isDate = DateTime.TryParse(Console.ReadLine(), out insertFlightDate);
                    if (isDate && insertFlightDate > DateStart && insertFlightDate < DateEnd) {
                        isCorrect = false;
                    } else {
                        Console.WriteLine("Incorrect date(less than this or more than one year) or format. Please repeat");
                    }
                } while (isCorrect);

                isCorrect = true;
                do {
                    CleanerManager.CheckBorder();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Enter Number flight [100..999]: ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    bool isDate = int.TryParse(Console.ReadLine(), out insertFlightNumber);
                    if (isDate && insertFlightNumber > 99 && insertFlightNumber < 1000) {
                        isCorrect = false;
                        foreach (Flight item in FlightList) {
                            if (item != null && item.FlightNumber == insertFlightNumber) {
                                Console.WriteLine("A flight number already exists");
                                isCorrect = true;
                            }
                        }

                    } else {
                        Console.WriteLine("-Incorrect flight number. Please repeat");
                    }
                } while (isCorrect);

                isCorrect = true;
                do {
                    CleanerManager.CheckBorder();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Enter City: ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    insertFlightCity = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(insertFlightCity))
                        Console.WriteLine("Strange empty city.Please repeat");
                    else
                        isCorrect = false;
                } while (isCorrect);

                insertFlightTerminal = EnterTerminal();
                Console.WriteLine();
                insertFlightGate = EnterGate();

                insertFlightStatus = EnterStatus();
                Console.WriteLine();

                insertFlightPriceBusiness = EnterPriceTicket("Business");
                Console.WriteLine();

                insertFlightPriceEconomy = EnterPriceTicket("Economy");
                Console.WriteLine();

                if (insertFlightPriceEconomy > insertFlightPriceBusiness) {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("WARNING!!! Price Economy large Price business!!!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Thread.Sleep(300);
                }
                FlightList[FreeCell] = new Flight(insertFlightDirection, insertFlightDate, insertFlightNumber,
                    insertFlightCity, insertFlightTerminal, insertFlightGate, insertFlightStatus,
                    insertFlightPriceBusiness, insertFlightPriceEconomy);
                return true;
            } else {
                Console.WriteLine("Your flight table is filled. Operation canceled.");
                return false;
            }
        }
        #endregion

        #region Delete flight
        public bool DeleteFromList(Flight[] FlightList, int enterFlightNumber = -1) {
            bool isDelete = false;
            bool isCorrect = true;
            int insertFlightNumber;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("A flight number you want to delete?: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                bool isDate = int.TryParse(Console.ReadLine(), out insertFlightNumber);
                if (isDate) {
                    for (int i = 0; i < FlightList.Length; i++) {
                        if (FlightList[i] != null && FlightList[i].FlightNumber == insertFlightNumber) {
                            Console.WriteLine("We are sure that you want to delete the entry?(y/N)");
                            switch (Console.ReadKey().KeyChar) {
                                case 'y':
                                case 'Y':
                                    FlightList[i] = null;
                                    isDelete = true;
                                    isCorrect = false;
                                    break;
                                default:
                                    isCorrect = false;
                                    break;
                            }
                        }
                    }
                    if (!isDelete && isCorrect) {
                        Console.WriteLine("Flight number not found. Re-enter (y/N)");
                        switch (Console.ReadKey().KeyChar) {
                            case 'y':
                            case 'Y':
                                break;
                            default:
                                isCorrect = false;
                                break;
                        }
                    }

                } else {
                    Console.WriteLine("Incorrect flight number. Re-enter (y/N)");
                    switch (Console.ReadKey().KeyChar) {
                        case 'y':
                        case 'Y':
                            break;
                        default:
                            isCorrect = false;
                            break;
                    }
                }
            } while (isCorrect);
            this.ArrangeList(FlightList);
            return isDelete;
        }
        #endregion

        #region Edit flight

        public bool EditList(Flight[] FlightList, int enterFlightNumber = -1) {
            bool isEdit = false, isCorrect = true, isDate = false;
            int insertFlightNumber;
            do {
                CleanerManager.CheckBorder();
                if (enterFlightNumber == -1) {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("A flight number you want to modify?: ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    isDate = int.TryParse(Console.ReadLine(), out insertFlightNumber);
                } else {
                    insertFlightNumber = (int)FlightList[enterFlightNumber].FlightNumber;
                    isDate = true;
                }
                if (isDate) {
                    for (int i = 0; i < FlightList.Length; i++) {
                        if (FlightList[i] != null && FlightList[i].FlightNumber == insertFlightNumber) {
                            Console.WriteLine("What are we going to modify?(T)erminal,(G)ate,(S)tatus,(P)rice Business/Economy or other for Quit");
                            switch (Console.ReadKey().KeyChar) {
                                case 't':
                                case 'T':
                                    FlightList[i].FlightTerminal = EnterTerminal();
                                    isEdit = true;
                                    isCorrect = false;
                                    break;
                                case 'g':
                                case 'G':
                                    FlightList[i].FlightGate = (byte)EnterGate();
                                    isEdit = true;
                                    isCorrect = false;
                                    break;
                                case 's':
                                case 'S':
                                    FlightList[i].ChangeStatus(EnterStatus());
                                    isEdit = true;
                                    isCorrect = false;
                                    break;
                                case 'p':
                                case 'P':
                                    Console.WriteLine($"Current Business price ticket: {FlightList[i].FlightPriceBussiness:C}");
                                    Console.WriteLine($"Current Economy price ticket: {FlightList[i].FlightPriceEconomy:C}");
                                    FlightList[i].FlightPriceBussiness = (decimal)EnterPriceTicket("Business");
                                    FlightList[i].FlightPriceEconomy = (decimal)EnterPriceTicket("Economy");
                                    isEdit = true;
                                    isCorrect = false;
                                    break;
                                default:
                                    isEdit = true;
                                    Console.WriteLine("Editing canceled");
                                    isCorrect = false;
                                    break;
                            }
                        }
                    }
                    if (!isEdit) {
                        Console.WriteLine("Flight number not found.");
                        isCorrect = false;
                    }

                } else {
                    Console.WriteLine("Incorrect flight number. Re-enter (y/N)");
                    switch (Console.ReadKey().KeyChar) {
                        case 'y':
                        case 'Y':
                            break;
                        default:
                            isCorrect = false;
                            break;
                    }
                }
            } while (isCorrect);
            return isEdit;
        }

        #endregion



        static char EnterTerminal() {
            bool isCorrect = true;
            char insertFlightTerminal;
            //string tempTerminals = "ABCDEFG";
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter flight Terminal [A,B,C,D,E,F or G]: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                insertFlightTerminal = Console.ReadKey().KeyChar;
                if (Char.IsLetter(insertFlightTerminal)) {
                    if ("ABCDEFG".Contains(insertFlightTerminal))
                        isCorrect = false;
                }
                if (isCorrect)
                    Console.WriteLine("-Incorrect flight letter terminal. Please repeat");
            } while (isCorrect);
            return insertFlightTerminal;

        }

        static int EnterGate() {
            bool isCorrect = true;
            int insertFlightGate;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter flight Gateway [1..9]: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                bool isDate = int.TryParse(Console.ReadLine(), out insertFlightGate);
                if (isDate && insertFlightGate > 0 && insertFlightGate < 10) {
                    isCorrect = false;
                } else {
                    Console.WriteLine("Incorrect flight gate. Please repeat");
                }
            } while (isCorrect);
            return insertFlightGate;
        }

        static byte EnterStatus() {
            byte insertFlightStatus = 100;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"Choise flight status (C)heckin, (G)ate closed, (A)rrived, 
    (D)eparted at, (E)xpected at, De(L)ayed, (I)n flight, (U)nknown, Ca(N)celed: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            bool isCorrect = true;
            do {
                CleanerManager.CheckBorder();
                switch (Console.ReadKey().KeyChar) {
                    case 'c':
                    case 'C':
                        insertFlightStatus = 0;
                        isCorrect = false;
                        break;
                    case 'g':
                    case 'G':
                        insertFlightStatus = 1;
                        isCorrect = false;
                        break;
                    case 'a':
                    case 'A':
                        insertFlightStatus = 2;
                        isCorrect = false;
                        break;
                    case 'd':
                    case 'D':
                        insertFlightStatus = 3;
                        isCorrect = false;
                        break;
                    case 'u':
                    case 'U':
                        insertFlightStatus = 4;
                        isCorrect = false;
                        break;
                    case 'n':
                    case 'N':
                        insertFlightStatus = 5;
                        isCorrect = false;
                        break;
                    case 'e':
                    case 'E':
                        insertFlightStatus = 6;
                        isCorrect = false;
                        break;
                    case 'l':
                    case 'L':
                        insertFlightStatus = 7;
                        isCorrect = false;
                        break;
                    case 'i':
                    case 'I':
                        insertFlightStatus = 8;
                        isCorrect = false;
                        break;
                    default:
                        Console.WriteLine("Incorrect choise. Press 'C','G','A','D','E','L','I','U' or 'N'");
                        break;
                }
            } while (isCorrect);
            return insertFlightStatus;
        }

        static int EnterPriceTicket(string ClassTicket) {
            bool isCorrect = true;
            int insertFlightPrice;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"Enter the cost of the ticket {ClassTicket} class [1..999]: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                bool isDate = int.TryParse(Console.ReadLine(), out insertFlightPrice);
                if (isDate && insertFlightPrice > 0 && insertFlightPrice < 1000) {
                    isCorrect = false;
                } else {
                    Console.WriteLine("Incorrect value price. Please repeat");
                }
            } while (isCorrect);
            return insertFlightPrice;
        }

    }
}
