using AirlineEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineGeneric.Manager
{
    class PassengerManage:IManager
    {
        #region Print passengers
        public void PrintList(Flight[] FlightList, int enterFlightNumber = -1) {
            int EnterFlightNumber = enterFlightNumber;
            if (enterFlightNumber == -1)
                EnterFlightNumber = EnteredFlightNumber(FlightList);

            if (EnterFlightNumber != -1) {
                Console.WriteLine("---------------------------------------------------------------------------------------------------");
                Console.WriteLine("|   |   FirsName   |    LastName    | Birthday |Passport|  Sex  |Nationality| Class  | Cost ticket |");
                Console.WriteLine("|---|--------------|----------------|----------|--------|-------|-----------|--------|-------------|");
                int passengerNumber = 1;
                foreach (Passenger item in FlightList[EnterFlightNumber].PassengersList) {
                    if (item != null) {
                        Console.Write($"|{passengerNumber,-3}");
                        Console.WriteLine(item.ToString());
                        passengerNumber++;
                    }
                }
                Console.WriteLine("---------------------------------------------------------------------------------------------------");
            }
        }
        #endregion

        #region Add passenger
        public bool AddToList(Flight[] FlightList) {
            string insertPassengerFirstname, insertPassengerLastname,
                insertPassengerNationality, insertPassengerPasspower;
            DateTime insertPassengerBirthday, DateStart = DateTime.Now, DateEnd = DateTime.Now.AddYears(-120);
            Console.WriteLine("The operation is allowed only for flights in condition 'CheckIn' or 'Delayed'");
            int EnterFlightNumber = EnteredFlightNumber(FlightList);
            if (EnterFlightNumber == -1)
                return false;
            int insertPassengerSex = 0, insertPassengerTicket = 0;

            if (FlightList[EnterFlightNumber].FlightFreePlace == 0) {
                Console.WriteLine("On this flight there are no free places");
                return false;
            }


            if (FlightList[EnterFlightNumber].GetFlightStatus() != "CheckIn" &&
                FlightList[EnterFlightNumber].GetFlightStatus() != "Delayed") {
                Console.WriteLine("Flight status does not allow additional passengers.");
                return false;
            }

            int freeCell = 0;
            if (FlightList[EnterFlightNumber].PassengersList != null) {
                for (int i = 0; i < FlightList[EnterFlightNumber].PassengersList.Length; i++) {
                    if (FlightList[EnterFlightNumber].PassengersList[i] == null) {
                        freeCell = i;
                        break;
                    }
                }
            }

            bool isCorrect = true;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter first name: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                insertPassengerFirstname = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(insertPassengerFirstname))
                    Console.WriteLine("Strange empty first name.Please repeat");
                else
                    isCorrect = false;
            } while (isCorrect);

            isCorrect = true;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter last name: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                insertPassengerLastname = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(insertPassengerLastname))
                    Console.WriteLine("Strange empty last name.Please repeat");
                else
                    isCorrect = false;
            } while (isCorrect);

            isCorrect = true;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter nationality: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                insertPassengerNationality = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(insertPassengerNationality))
                    Console.WriteLine("Strange empty nationality.Please repeat");
                else
                    isCorrect = false;
            } while (isCorrect);

            isCorrect = true;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter passpower: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                insertPassengerPasspower = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(insertPassengerPasspower))
                    Console.WriteLine("Strange empty passport.Please repeat");
                else
                    isCorrect = false;
                bool isPassport = true;
                foreach (char item in insertPassengerPasspower) {
                    if (!char.IsLetterOrDigit(item))
                        isPassport = false;
                }
                if (!isPassport) {
                    Console.WriteLine("Incoorect symbol in passport");
                    isCorrect = true;
                }
            } while (isCorrect);

            isCorrect = true;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Enter birthday date passenger (format YYYY-MM-DD): ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                bool isDate = DateTime.TryParse(Console.ReadLine(), out insertPassengerBirthday);
                if (isDate && insertPassengerBirthday < DateStart && insertPassengerBirthday > DateEnd) {
                    isCorrect = false;
                } else {
                    Console.WriteLine("Incorrect date(not yet born or long dead) or format. Please repeat");
                }
            } while (isCorrect);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Select sex passenger (M)ale, (F)emale");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            isCorrect = true;
            do {
                switch (Console.ReadKey().KeyChar) {
                    case 'm':
                    case 'M':
                        insertPassengerSex = 0;
                        isCorrect = false;
                        break;
                    case 'f':
                    case 'F':
                        insertPassengerSex = 1;
                        isCorrect = false;
                        break;
                    default:
                        Console.Write("Press 'M' or 'F'");
                        break;
                }
            } while (isCorrect);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Select class ticket passenger (B)usiness, (E)conomy");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            isCorrect = true;
            do {
                switch (Console.ReadKey().KeyChar) {
                    case 'b':
                    case 'B':
                        insertPassengerTicket = 0;
                        isCorrect = false;
                        break;
                    case 'e':
                    case 'E':
                        insertPassengerTicket = 1;
                        isCorrect = false;
                        break;
                    default:
                        Console.Write("Press 'B' or 'E'");
                        break;
                }
            } while (isCorrect);
            Console.WriteLine();

            FlightList[EnterFlightNumber].PassengersList[freeCell] = new Passenger(insertPassengerFirstname, insertPassengerLastname,
                insertPassengerNationality, insertPassengerPasspower, insertPassengerBirthday, insertPassengerSex, insertPassengerTicket,
                FlightList[EnterFlightNumber].FlightPriceBussiness, FlightList[EnterFlightNumber].FlightPriceEconomy);
            return true;
        }
        #endregion

        #region Delete passenger
        public bool DeleteFromList(Flight[] FlightList, int enterFlightNumber = -1) {
            Console.WriteLine("The operation is allowed only for flights in condition 'CheckIn' or 'Delayed'");
            int EnterFlightNumber = enterFlightNumber;
            if (enterFlightNumber == -1)
                EnterFlightNumber = EnteredFlightNumber(FlightList);

            if (EnterFlightNumber == -1)
                return false;

            if (FlightList[EnterFlightNumber].GetFlightStatus() != "CheckIn" &&
                FlightList[EnterFlightNumber].GetFlightStatus() != "Delayed") {
                Console.WriteLine("Flight status does not allow passengers to remove");
                return false;
            }

            if (enterFlightNumber == -1)
                this.PrintList(FlightList, EnterFlightNumber);
            int maxPassengerNumber = FlightList[EnterFlightNumber].PassengersList.Length - FlightList[EnterFlightNumber].FlightFreePlace;
            if (maxPassengerNumber == 0) {
                Console.WriteLine("On this flight no tickets sold");
                return false;
            }

            bool isEnter = true;
            do {
                CleanerManager.CheckBorder();
                int tempFlightNumber;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter number passenger for delete: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                bool isCorrectValue = int.TryParse(Console.ReadLine(), out tempFlightNumber);
                if (isCorrectValue) {
                    if (tempFlightNumber > 0 && tempFlightNumber <= maxPassengerNumber) {
                        tempFlightNumber--;
                        FlightList[EnterFlightNumber].PassengersList[tempFlightNumber] = null;
                        isEnter = false;
                    } else
                        Console.WriteLine("Number passenger out of range");
                } else
                    Console.WriteLine("Number passenger is not digit");
            } while (isEnter);

            Passenger temp;
            for (int i = 0; i < FlightList[EnterFlightNumber].PassengersList.Length; i++) {
                for (int j = 0; j < FlightList[EnterFlightNumber].PassengersList.Length - 1; j++) {
                    if (FlightList[EnterFlightNumber].PassengersList[j] == null && FlightList[EnterFlightNumber].PassengersList[j + 1] != null) {
                        temp = FlightList[EnterFlightNumber].PassengersList[j];
                        FlightList[EnterFlightNumber].PassengersList[j] = FlightList[EnterFlightNumber].PassengersList[j + 1];
                        FlightList[EnterFlightNumber].PassengersList[j + 1] = temp;
                    }
                }
            }

            return true;
        }
        #endregion

        #region Edit passenger (Lastname,firstName,Birthday)
        public bool EditList(Flight[] FlightList, int enterFlightNumber = -1) {
            Console.WriteLine("The operation is allowed only for flights in condition 'CheckIn' or 'Delayed'");
            int EnterFlightNumber = enterFlightNumber;
            if (enterFlightNumber == -1)
                EnterFlightNumber = EnteredFlightNumber(FlightList);

            if (EnterFlightNumber == -1)
                return false;

            if (FlightList[EnterFlightNumber].GetFlightStatus() != "CheckIn" &&
                FlightList[EnterFlightNumber].GetFlightStatus() != "Delayed") {
                Console.WriteLine("Flight status does not allow passengers to change");
                return false;
            }
            if (enterFlightNumber == -1)
                this.PrintList(FlightList, EnterFlightNumber);

            int maxPassengerNumber = FlightList[EnterFlightNumber].PassengersList.Length - FlightList[EnterFlightNumber].FlightFreePlace;
            if (maxPassengerNumber == 0) {
                Console.WriteLine("On this flight no tickets sold");
                return false;
            }

            bool isEnter = true;
            do {
                CleanerManager.CheckBorder();
                int tempFlightNumber;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter number passenger for edit parameters: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                bool isCorrectValue = int.TryParse(Console.ReadLine(), out tempFlightNumber);
                if (isCorrectValue) {
                    if (tempFlightNumber > 0 && tempFlightNumber <= maxPassengerNumber) {
                        tempFlightNumber--;

                        Passenger item = FlightList[EnterFlightNumber].PassengersList[tempFlightNumber--];

                        CleanerManager.CheckBorder();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Current first name: {item.PassengerFirstname}");
                        Console.Write("Enter first name or press Enter to skip changes: ");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        string insertPassengerFirstname = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(insertPassengerFirstname))
                            Console.WriteLine("Firstname is skip changes.");
                        else {
                            item.PassengerFirstname = insertPassengerFirstname;
                        }

                        CleanerManager.CheckBorder();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Current last name: {item.PassengerLastname}");
                        Console.Write("Enter last name or press Enter to skip changes:: ");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        string insertPassengerLastname = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(insertPassengerLastname))
                            Console.WriteLine("Firstname is skip changes.");
                        else {
                            item.PassengerLastname = insertPassengerLastname;
                        }

                        bool isCorrect = true;
                        DateTime DateStart = DateTime.Now, DateEnd = DateTime.Now.AddYears(-120), insertPassengerBirthday;
                        do {
                            CleanerManager.CheckBorder();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Enter birthday date passenger (format YYYY-MM-DD) or tomorrow's date to skip changes: ");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            bool isDate = DateTime.TryParse(Console.ReadLine(), out insertPassengerBirthday);
                            if (isDate) {
                                if (insertPassengerBirthday < DateStart && insertPassengerBirthday > DateEnd) {
                                    item.PassengerBirthday = insertPassengerBirthday;
                                    isCorrect = false;
                                } else {
                                    Console.WriteLine("Entered date is out of range. Birthday skip changes.");
                                    isCorrect = false;
                                }
                            } else {
                                Console.WriteLine("Incorrect date format. Please repeat");
                            }
                        } while (isCorrect);


                        isEnter = false;
                    } else
                        Console.WriteLine("Number passenger out of range");
                } else
                    Console.WriteLine("Number passenger is not digit");
            } while (isEnter);


            return true;
        }
        #endregion

        static int EnteredFlightNumber(Flight[] FlightList) {
            bool isEnter = true, isFind = false;
            int EnterFlightNumber = 0, tempFlightNumber;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter flight number: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                bool isCorrectValue = int.TryParse(Console.ReadLine(), out tempFlightNumber);
                if (isCorrectValue) {
                    if (tempFlightNumber == 1)
                        return EnterFlightNumber = -1;
                    int stepItem = 0;
                    foreach (Flight item in FlightList) {
                        if (item != null && item.FlightNumber != null) {
                            if (item.FlightNumber == tempFlightNumber) {
                                isEnter = false;
                                isFind = true;
                                EnterFlightNumber = stepItem;
                            }
                        }
                        stepItem++;
                    }
                    if (!isFind)
                        Console.WriteLine("Number not find.Please entered correct number or '1' for Quit.");
                } else
                    Console.WriteLine("The number entered not digit");
            } while (isEnter);
            return EnterFlightNumber;
        }


        public void ArrangeList(Flight[] FlightList) {
            throw new NotImplementedException();
        }
    }
}
