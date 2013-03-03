using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdRide.Models;

namespace NerdRide.Tests.Models {

    [TestClass]
    public class RideTest {

        [TestMethod]
        public void Ride_Should_Not_Be_Valid_When_Some_Properties_Incorrect() {

            //Arrange
            Ride Ride = new Ride() {
                Title = "Test title",
                Country = "USA",
                ContactPhone = "BOGUS"
            };

            // Act
            bool isValid = Ride.IsValid;

            //Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Ride_Should_Be_Valid_When_All_Properties_Correct() {
            
            //Arrange
            Ride Ride = new Ride {
                Title = "Test title",
                Description = "Some description",
                EventDate = DateTime.Now,
                HostedBy = "ScottGu",
                Address = "One Microsoft Way",
                Country = "USA",
                ContactPhone = "425-703-8072",
                Latitude = 93,
                Longitude = -92,
            };

            // Act
            bool isValid = Ride.IsValid;

            //Assert
            Assert.IsTrue(isValid);
        }
    }
}
