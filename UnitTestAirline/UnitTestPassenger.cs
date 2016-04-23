using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirlineEntities;

namespace UnitTestAirline
{
    [TestClass]
    public class UnitTestPassenger
    {
        [TestMethod]
        public void TestAddSamplePassenger()
        {
            Passenger[] passengerSample = { new Passenger("male", 200M, 100M), new Passenger("female", 200M, 100M), new Passenger("male", 200M, 100M) };
            int count = 0;
            foreach (var item in passengerSample) {
                if (item != null)
                    count++;
            }

            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void TestPassengerToString()
        {
            Passenger passengerSample = new Passenger("male", 200M, 100M);
            Passenger passengerSample2 = new Passenger("female", 500M, 300M);
            int count = passengerSample.ToString().Length;
            int count2 = passengerSample.ToString().Length;
            Assert.AreEqual(96, count2);
            Assert.AreEqual(96, count);
        }
    }
}
