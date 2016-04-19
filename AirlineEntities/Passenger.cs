using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace AirlineEntities
{
    public struct NewPassenger
    {
        public string insertPassengerFirstname, insertPassengerLastname,
                        insertPassengerNationality, insertPassengerPasspower;
        public DateTime insertPassengerBirthday;
        public int insertPassengerSex, insertPassengerTicket;
        public decimal priceBussiness, priceEconomy;
    }

    enum ClassForTicket { Business, Economy }
    struct Ticket
    {
        ClassForTicket TicketClass;
        decimal TicketPrice;
        public Ticket(ClassForTicket ticketClass, decimal ticketPrice) {
            TicketClass = ticketClass;
            TicketPrice = ticketPrice;
        }
        public override string ToString() {
            if (TicketClass == ClassForTicket.Business)
                return String.Format("Business|{0,13:C}", TicketPrice);
            else
                return String.Format("Economy |{0,13:C}", TicketPrice);
        }
    }
    public class Passenger : IComparable<Passenger>
    {
        enum SexForPassenger { Male, Female }
        private string _passengerFirstname;
        public string PassengerFirstname {
            get { return _passengerFirstname; }
            set {
                if (value != null)
                    _passengerFirstname = value;
            }
        }
        private string _passengerLastname;
        public string PassengerLastname {
            get { return _passengerLastname; }
            set {
                if (value != null)
                    _passengerLastname = value;
            }
        }
        string PassengerNationality { get; set; }
        private string _passengerPasspower;
        public string PassengerPasspower {
            get { return _passengerPasspower; }
            private set { _passengerPasspower = value; }
        }
        private DateTime _passengerBirthday;
        public DateTime PassengerBirthday {
            get { return _passengerBirthday; }
            set {
                if (value != null && value < DateTime.Now)
                    _passengerBirthday = value;
            }
        }
        SexForPassenger PassengerSex;
        Ticket PassengerTicket;

        public override string ToString() {
            return String.Format("|{0,-14}|{1,-16}|{2,-10}|{3,-8}|{4,7}|{5,-11}|{6,10}|", PassengerFirstname,
                   PassengerLastname, PassengerBirthday.ToString("dd-MM-yyyy"), PassengerPasspower, PassengerSex,
                   PassengerNationality, PassengerTicket.ToString());
        }

        public int CompareTo(Passenger other) {
            if (other == null)
                return -1;
            return PassengerLastname.CompareTo(other.PassengerLastname);
        }

        #region insert sample value
        public Passenger(string sex, decimal bussinesTicket, decimal economTicket) {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            string[] MaleFirstName = { "Rufus", "Bear", "Dakota", "Fido",
                                "Vanya", "Samuel", "Koani", "Volodya"};
            string[] FemaleFirstName = { "Maggie", "Penny", "Saya", "Princess",
                                  "Abby", "Laila", "Sadie", "Olivia" };
            string[] MaleLastName = { "Ivanov", "Petrov", "Sidorov", "Kafa",
                                "Grusha", "Vorobey", "Grishko"};
            string[] FemaleLastName = { "Ivanova", "Petrova", "Sidorova", "Gracheva",
                                  "Lastochkina", "Krasivaya"};
            string[] Nationality = { "Ukrainian", "Russian", "German", "Serbian", "Balerusian", "Slovak" };
            string[] SerialPassport = { "EA", "GB", "MK", "QW", "ED" };
            switch (sex) {
                case "male":
                    PassengerFirstname = MaleFirstName[rnd.Next(0, MaleFirstName.Length)];
                    PassengerLastname = MaleLastName[rnd.Next(0, MaleLastName.Length)];
                    PassengerNationality = Nationality[rnd.Next(0, Nationality.Length)];
                    PassengerPasspower = SerialPassport[rnd.Next(0, SerialPassport.Length)] + rnd.Next(111111, 999999).ToString();
                    PassengerBirthday = DateTime.Now.AddDays(-rnd.Next(7000, 22000));
                    PassengerSex = SexForPassenger.Male;
                    if ((rnd.Next(1, 1000) % 2) == 0)
                        PassengerTicket = new Ticket(ClassForTicket.Business, bussinesTicket);
                    else
                        PassengerTicket = new Ticket(ClassForTicket.Economy, economTicket);
                    break;
                case "female":
                    PassengerFirstname = FemaleFirstName[rnd.Next(0, FemaleFirstName.Length)];
                    PassengerLastname = FemaleLastName[rnd.Next(0, FemaleLastName.Length)];
                    PassengerNationality = Nationality[rnd.Next(0, Nationality.Length)];
                    PassengerPasspower = SerialPassport[rnd.Next(0, SerialPassport.Length)] + rnd.Next(111111, 999999).ToString();
                    PassengerBirthday = DateTime.Now.AddDays(-rnd.Next(7000, 22000));
                    PassengerSex = SexForPassenger.Female;
                    if ((rnd.Next(1, 1000) % 2) == 0)
                        PassengerTicket = new Ticket(ClassForTicket.Business, bussinesTicket);
                    else
                        PassengerTicket = new Ticket(ClassForTicket.Economy, economTicket);
                    break;
                default:
                    break;
            }
        }

        #endregion

        
        public Passenger( NewPassenger passenger) {
            PassengerFirstname = passenger.insertPassengerFirstname;
            PassengerLastname = passenger.insertPassengerLastname;
            PassengerNationality = passenger.insertPassengerNationality;
            PassengerPasspower = passenger.insertPassengerPasspower;
            PassengerBirthday = passenger.insertPassengerBirthday;
            PassengerSex = (SexForPassenger)passenger.insertPassengerSex;
            if (passenger.insertPassengerTicket == 0)
                PassengerTicket = new Ticket(ClassForTicket.Business, passenger.priceBussiness);
            else
                PassengerTicket = new Ticket(ClassForTicket.Economy, passenger.priceEconomy);
        }


    }
}
