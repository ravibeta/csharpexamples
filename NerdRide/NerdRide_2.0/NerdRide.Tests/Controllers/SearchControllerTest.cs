using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdRide.Controllers;
using System.Web.Mvc;
using NerdRide.Models;
using NerdRide.Tests.Fakes;
using Moq;
using NerdRide.Helpers;
using System.Web.Routing;

namespace NerdRide.Tests.Controllers {
 
    [TestClass]
    public class SearchControllerTest {

        SearchController CreateSearchController() {
            var testData = FakeRideData.CreateTestRides();
            var repository = new FakeRideRepository(testData);

            return new SearchController(repository);
        }

        [TestMethod]
        public void SearchByLocationAction_Should_Return_Json()
        {

            // Arrange
            var controller = CreateSearchController();

            // Act
            var result = controller.SearchByLocation(99, -99);

            // Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void SearchByLocationAction_Should_Return_JsonRides()
        {

            // Arrange
            var controller = CreateSearchController();

            // Act
            var result = (JsonResult)controller.SearchByLocation(99, -99);

            // Assert
            Assert.IsInstanceOfType(result.Data, typeof(List<JsonRide>));
            var Rides = (List<JsonRide>)result.Data;
            Assert.AreEqual(101, Rides.Count);
        }

        [TestMethod]
        public void GetMostPopularRidesAction_WithLimit_Returns_Expected_Rides()
        {

            // Arrange
            var controller = CreateSearchController();

            // Act
            var result = (JsonResult)controller.GetMostPopularRides(5);

            // Assert
            Assert.IsInstanceOfType(result.Data, typeof(List<JsonRide>));
            var Rides = (List<JsonRide>)result.Data;
            Assert.AreEqual(5, Rides.Count);
        }

        [TestMethod]
        public void GetMostPopularRidesAction_WithNoLimit_Returns_Expected_Rides()
        {

            // Arrange
            var controller = CreateSearchController();

            // Act
            var result = (JsonResult)controller.GetMostPopularRides(null);

            // Assert
            Assert.IsInstanceOfType(result.Data, typeof(List<JsonRide>));
            var Rides = (List<JsonRide>)result.Data;
            Assert.AreEqual(40, Rides.Count);
        }


    }
}
