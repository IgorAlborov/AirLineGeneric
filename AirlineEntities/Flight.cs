using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace AirlineEntities
{
    public class Flight
    {
        //констанкта кол-ва пассажиров в самолете
        public static readonly int FlightCapasity;
        static Flight() {
            FlightCapasity = 5;
        }
        enum DirectionForFlight
        {
            Arrival, Departure
        }
        enum StatusForFlight
        {
            CheckIn, GateClosed, Arrived, DepartedAt, Unknown, Canceled, ExpectedAt, Delayed, InFlight

        }
        DirectionForFlight FlightDirection;
        DateTime FlightDate { get; set; }
        public int? FlightNumber { get; private set; }
        string FlightCity { get; set; }
        private char _flightTerminal;
        public char FlightTerminal {
            get { return _flightTerminal; }
            set {
                if ("ABCDEFG".Contains(value))
                    _flightTerminal = value;
            }
        }
        private byte _flightGate;
        public byte FlightGate {
            get { return _flightGate; }
            set {
                if ((value > 0) && (value < 10))
                    _flightGate = value;
            }
        }
        StatusForFlight FlightStatus;
        public decimal FlightPriceBussiness;
        public decimal FlightPriceEconomy;
        public Passenger[] PassengersList;
        public byte FlightFreePlace {
            get {
                byte i = 0;
                foreach (Passenger item in PassengersList) {
                    if (item == null)
                        i++;
                }
                return i;
            }
        }

        public Flight() {
            PassengersList = new Passenger[FlightCapasity];

        }

        public override string ToString() {
            int i = FlightCapasity - 5;
            return String.Format("|{0,-9}|{1,-16}|{2,-6}|{3,-15}|{4,11}/{5}|{6,-10}|{7,11}|", FlightDirection,
                   FlightDate.ToString("dd-MM-yyyy hh:mm"), FlightNumber, FlightCity, FlightTerminal, FlightGate,
                   FlightStatus, FlightFreePlace);
        }

        public Flight(byte flightDirection, DateTime flightDate, int flightNumber, string flightCity,
            char flightTerminal, int flightGate, byte flightStatus, int flightPriceBusiness,
            int flightPriceEconomy) : this() {
            FlightDirection = (DirectionForFlight)flightDirection;
            FlightDate = flightDate;
            FlightNumber = flightNumber;
            FlightCity = flightCity;
            FlightTerminal = flightTerminal;
            FlightGate = (byte)flightGate;
            FlightStatus = (StatusForFlight)flightStatus;
            FlightPriceBussiness = (decimal)flightPriceBusiness;
            FlightPriceEconomy = (decimal)flightPriceEconomy;
        }

        #region insert sample value
        public Flight(string sample) : this() {
            if (sample == "sample") {
                Random rnd = new Random((int)DateTime.Now.Ticks);
                string[] Citys = { "Kyiv", "Moscow", "London", "Rim", "Minsk", "Antalia", "Warshava" };
                string terminals = "ABCDEFG";
                FlightDirection = (DirectionForFlight)rnd.Next(0, 1);
                Thread.Sleep(50);
                FlightDate = DateTime.Now.AddMinutes(rnd.Next(300));
                Thread.Sleep(50);
                FlightNumber = rnd.Next(100, 999);
                Thread.Sleep(50);
                FlightCity = Citys[rnd.Next(0, Citys.Length)];
                Thread.Sleep(50);
                FlightTerminal = terminals[rnd.Next(0, terminals.Length)];
                Thread.Sleep(50);
                FlightGate = (byte)rnd.Next(1, 9);
                Thread.Sleep(50);
                FlightStatus = (StatusForFlight)rnd.Next(0, 8);
                Thread.Sleep(50);
                FlightPriceBussiness = (decimal)rnd.Next(300, 1000);
                Thread.Sleep(50);
                FlightPriceEconomy = (decimal)rnd.Next(80, 290);
                Thread.Sleep(50);
                int tempPassengerCount = rnd.Next(1, 5);
                for (int i = 0; i < tempPassengerCount; i++) {
                    Thread.Sleep(100);
                    int MaleIndex = rnd.Next(1, 1000) % 2;
                    switch (MaleIndex) {
                        case 0:      //Male
                            PassengersList[i] = new Passenger("male", FlightPriceBussiness, FlightPriceEconomy);
                            break;
                        case 1:     //Female
                            PassengersList[i] = new Passenger("female", FlightPriceBussiness, FlightPriceEconomy);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        #endregion

        public void ChangeStatus(byte statusIndex) {
            if (statusIndex >= 0 && statusIndex <= 8)
                this.FlightStatus = (StatusForFlight)statusIndex;
        }

        public string GetFlightStatus() {
            return this.FlightStatus.ToString();
        }

    }
}

