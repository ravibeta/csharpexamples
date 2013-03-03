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
    public class RSVPControllerTest {

        RSVPController CreateRSVPController() {
            var testData = FakeRideData.CreateTestRides();
            var repository = new FakeRideRepository(testData);

            return new RSVPController(repository);
        }

        RSVPController CreateRSVPControllerAs(string userName)
        {

            var mock = new Mock<ControllerContext>();
            var nerdIdentity = FakeIdentity.CreateIdentity("SomeUser");
            mock.SetupGet(p => p.HttpContext.User.Identity).Returns(nerdIdentity);

            var controller = CreateRSVPController();
            controller.ControllerContext = mock.Object;

            return controller;
        }

        [TestMethod]
        public void RegisterAction_Should_Return_Content()
        {
            // Arrange
            var controller = CreateRSVPControllerAs("scottha");

            // Act
            var result = controller.Register(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ContentResult));
        }
    }
}
