using AirlineEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineGeneric.Manager
{

    class PassengerManage : IManager
    {
        #region Print passengers
        public void PrintList(List<Flight> FlightList, int enterFlightNumber = -1) {
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
        public bool AddToList(List<Flight> FlightList) {
            NewPassenger newPassenger = new NewPassenger();

            DateTime DateStart = DateTime.Now, DateEnd = DateTime.Now.AddYears(-120);
            Console.WriteLine("The operation is allowed only for flights in condition 'CheckIn' or 'Delayed'");
            int IndexFlightNumber = EnteredFlightNumber(FlightList);
            if (IndexFlightNumber == -1)
                return false;

            if (FlightList[IndexFlightNumber].FlightFreePlace == 0) {
                Console.WriteLine("On this flight there are no free places");
                return false;
            }


            if (FlightList[IndexFlightNumber].GetFlightStatus() != "CheckIn" &&
                FlightList[IndexFlightNumber].GetFlightStatus() != "Delayed") {
                Console.WriteLine("Flight status does not allow additional passengers.");
                return false;
            }

            int freeCell = FlightList[IndexFlightNumber].PassengersList.FindIndex(x => x == null);
            
            bool isCorrect = true;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter first name: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                newPassenger.insertPassengerFirstname = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(newPassenger.insertPassengerFirstname))
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
                newPassenger.insertPassengerLastname = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(newPassenger.insertPassengerLastname))
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
                newPassenger.insertPassengerNationality = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(newPassenger.insertPassengerNationality))
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
                newPassenger.insertPassengerPasspower = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(newPassenger.insertPassengerPasspower))
                    Console.WriteLine("Strange empty passport.Please repeat");
                else
                    isCorrect = false;
                bool isPassport = true;
                foreach (char item in newPassenger.insertPassengerPasspower) {
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
                bool isDate = DateTime.TryParse(Console.ReadLine(), out newPassenger.insertPassengerBirthday);
                if (isDate && newPassenger.insertPassengerBirthday < DateStart && newPassenger.insertPassengerBirthday > DateEnd) {
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
                        newPassenger.insertPassengerSex = 0;
                        isCorrect = false;
                        break;
                    case 'f':
                    case 'F':
                        newPassenger.insertPassengerSex = 1;
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
                        newPassenger.insertPassengerTicket = 0;
                        isCorrect = false;
                        break;
                    case 'e':
                    case 'E':
                        newPassenger.insertPassengerTicket = 1;
                        isCorrect = false;
                        break;
                    default:
                        Console.Write("Press 'B' or 'E'");
                        break;
                }
            } while (isCorrect);
            Console.WriteLine();



            newPassenger.priceBussiness = FlightList[IndexFlightNumber].FlightPriceBussiness;
            newPassenger.priceEconomy = FlightList[IndexFlightNumber].FlightPriceEconomy;
            if (freeCell != -1)
                FlightList[IndexFlightNumber].PassengersList[freeCell] = new Passenger(newPassenger);
            else
                FlightList[IndexFlightNumber].PassengersList.Add(new Passenger(newPassenger));
            LogManager.WriteToLog($"Add passenger {newPassenger.insertPassengerFirstname} {newPassenger.insertPassengerLastname} into flight {FlightList[IndexFlightNumber].FlightNumber}");
            return true;
        }
        #endregion

        #region Delete passenger
        public bool DeleteFromList(List<Flight> FlightList, int enterFlightNumber = -1) {
            Console.WriteLine("The operation is allowed only for flights in condition 'CheckIn' or 'Delayed'");
            int IndexFlightNumber = enterFlightNumber;
            if (enterFlightNumber == -1)
                IndexFlightNumber = EnteredFlightNumber(FlightList);

            if (IndexFlightNumber == -1)
                return false;

            if (FlightList[IndexFlightNumber].GetFlightStatus() != "CheckIn" &&
                FlightList[IndexFlightNumber].GetFlightStatus() != "Delayed") {
                Console.WriteLine("Flight status does not allow passengers to remove");
                return false;
            }

            if (enterFlightNumber == -1)
                this.PrintList(FlightList, IndexFlightNumber);
            if (FlightList[IndexFlightNumber].PassengersList.Count == 0) {
                Console.WriteLine("On this flight no tickets sold");
                return false;
            }

            bool isEnter = true;
            do {
                CleanerManager.CheckBorder();
                int indexPassenger;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter number passenger for delete: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                bool isCorrectValue = int.TryParse(Console.ReadLine(), out indexPassenger);
                if (isCorrectValue) {
                    if (indexPassenger > 0 && indexPassenger <= (FlightList[IndexFlightNumber].PassengersList.Count+1)) {
                        indexPassenger--;
                        FlightList[IndexFlightNumber].PassengersList.RemoveAt(indexPassenger);
                        LogManager.WriteToLog($"Remove passenger from flight {FlightList[IndexFlightNumber].FlightNumber}");
                        isEnter = false;
                        FlightList[IndexFlightNumber].PassengersList.TrimExcess();
                    } else
                        Console.WriteLine("Number passenger out of range");
                } else
                    Console.WriteLine("Number passenger is not digit");
            } while (isEnter);

            return true;
        }
        #endregion

        #region Edit passenger (Lastname,firstName,Birthday)
        public bool EditList(List<Flight> FlightList, int enterFlightNumber = -1) {
            Console.WriteLine("The operation is allowed only for flights in condition 'CheckIn' or 'Delayed'");
            int IndexFlightNumber = enterFlightNumber;
            if (enterFlightNumber == -1)
                IndexFlightNumber = EnteredFlightNumber(FlightList);

            if (IndexFlightNumber == -1)
                return false;

            if (FlightList[IndexFlightNumber].GetFlightStatus() != "CheckIn" &&
                FlightList[IndexFlightNumber].GetFlightStatus() != "Delayed") {
                Console.WriteLine("Flight status does not allow passengers to change");
                return false;
            }
            if (enterFlightNumber == -1)
                this.PrintList(FlightList, IndexFlightNumber);

            if (FlightList[IndexFlightNumber].PassengersList.Count == 0) {
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
                    if (tempFlightNumber > 0 && tempFlightNumber <= (FlightList[IndexFlightNumber].PassengersList.Count+1)) {
                        tempFlightNumber--;

                        Passenger item = FlightList[IndexFlightNumber].PassengersList[tempFlightNumber--];

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

        static int EnteredFlightNumber(List<Flight> FlightList) {
            bool isEnter = true;
            int IndexFlightNumber = 0, tempFlightNumber;
            do {
                CleanerManager.CheckBorder();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter flight number: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                bool isCorrectValue = int.TryParse(Console.ReadLine(), out tempFlightNumber);
                if (isCorrectValue) {
                    if (tempFlightNumber == 1)
                        return IndexFlightNumber = -1;
                    IndexFlightNumber = FlightList.FindIndex(x => x.FlightNumber == tempFlightNumber);
                    if (IndexFlightNumber == -1)
                        Console.WriteLine("Number not find.Please entered correct number or '1' for Quit.");
                    else
                        isEnter = false;
                } else
                    Console.WriteLine("The number entered not digit");
            } while (isEnter);
            return IndexFlightNumber;
        }


    }
}
