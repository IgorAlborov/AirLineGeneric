using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirlineEntities;

namespace UnitTestAirline
{
    [TestClass]
    public class UnitTestFlight
    {
        [TestMethod]
        public void TestAddEmptyFlight()
        {
            Flight[] flightSample = { new Flight(), new Flight(), new Flight() };
            int count = 0;
            foreach (var item in flightSample) {
                if (item.FlightFreePlace > 0)
                    count++;
            }

            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void TestAddSampleFlight()
        {
            Flight[] flightSample = { new Flight("sample"), new Flight("sample"), new Flight("sample") };
            int count = 0;
            foreach (var item in flightSample) {
                if (item.PassengersList != null && item.FlightNumber != null)
                    count++;
            }

            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void TestFlightToString()
        {
            NewFlight newFlight = new NewFlight()
            {
                insertFlightCity = "Kharkiv",
                insertFlightDate = DateTime.Parse("2016-05-01 12:00:00"),
                insertFlightDirection = 0,
                insertFlightGate = 0,
                insertFlightNumber = 555,
                insertFlightPriceBusiness = 200,
                insertFlightPriceEconomy = 100,
                insertFlightStatus = 0,
                insertFlightTerminal = 'A'
            };
            Flight flightSample = new Flight(newFlight);
            string sss = flightSample.ToString();
            Assert.AreEqual("|Arrival  |01-05-2016 12:00|555   |Kharkiv        |          A/0|CheckIn   |        100|", sss);
        }

        [TestMethod]
        public void TestFlightGetStatus()
        {
            NewFlight newFlight = new NewFlight()
            {
                insertFlightCity = "Kharkiv",
                insertFlightDate = DateTime.Parse("2016-05-01 12:00:00"),
                insertFlightDirection = 0,
                insertFlightGate = 5,
                insertFlightNumber = 555,
                insertFlightPriceBusiness = 200,
                insertFlightPriceEconomy = 100,
                insertFlightStatus = 0,
                insertFlightTerminal = 'A'
            };
            Flight flightSample = new Flight(newFlight);
            Assert.AreEqual("CheckIn", flightSample.GetFlightStatus());
        }
    }
}
