using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NerdRide.Controllers;
using NerdRide.Helpers;
using NerdRide.Models;
using NerdRide.Tests.Fakes;
using System.Web.Routing;
using System.Web.Security;

namespace NerdRide.Tests.Controllers {
 
    [TestClass]
    public class RidesControllerTest {
		private const int NumberOfCountries = 256;

        RidesController CreateRidesController() {
            var testData = FakeRideData.CreateTestRides();
            var repository = new FakeRideRepository(testData);

            return new RidesController(repository);
        }

        RidesController CreateRidesControllerAs(string userName) {

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = CreateRidesController();
            controller.ControllerContext = mock.Object;

            return controller;
        }


        [TestMethod]
        public void DetailsAction_Should_Return_View_For_Ride() {

            // Arrange
            var controller = CreateRidesController();

            // Act
            var result = controller.Details(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DetailsAction_Should_Return_NotFoundView_For_BogusRide() {

            // Arrange
            var controller = CreateRidesController();

            // Act
			var result = controller.Details(999) as ActionResult;

            // Assert
			Assert.IsInstanceOfType(result, typeof(FileNotFoundResult));
			Assert.AreEqual("No Ride found for that id", ((FileNotFoundResult)result).Message);
        }

        [TestMethod]
        public void EditAction_Should_Return_View_For_ValidRide() {

            // Arrange
            var controller = CreateRidesControllerAs("SomeUser");

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Ride));
        }

        [TestMethod]
        public void EditAction_Should_Return_View_For_InValidOwner() {

            // Arrange
            var controller = CreateRidesControllerAs("SomeOtherUser");

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.AreEqual(result.ViewName, "InvalidOwner");
        }

        [TestMethod]
        public void EditAction_Should_Redirect_When_Update_Successful() {

            // Arrange
            var controller = CreateRidesControllerAs("SomeUser");
            int id = 1;

            FormCollection formValues = new FormCollection() {
                { "Ride.Title", "Another value" },
                { "Ride.Description", "Another description" }
            };

            controller.ValueProvider = formValues.ToValueProvider();
            
            // Act
            var result = controller.Edit(id, formValues) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Details", result.RouteValues["Action"]);
            Assert.AreEqual(id, result.RouteValues["id"]);
        }

        [TestMethod]
        public void EditAction_Should_Redisplay_With_Errors_When_Update_Fails() {

            // Arrange
            var controller = CreateRidesControllerAs("SomeUser");
            int id = 1;

            FormCollection formValues = new FormCollection() {
                { "Ride.EventDate", "Bogus date value!!!"}
            };

            controller.ValueProvider = formValues.ToValueProvider();

            // Act
            var result = controller.Edit(id, formValues) as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Expected redisplay of view");
            Assert.IsTrue(result.ViewData.ModelState.Sum(p => p.Value.Errors.Count) > 0, "Expected Errors");
        }

        [TestMethod]
        public void IndexAction_Should_Return_View() {

            // Arrange
            var controller = CreateRidesControllerAs("robcon");

            // Act
            var result = controller.Index(string.Empty, 0);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void IndexAction_Returns_TypedView_Of_List_Ride() {
            // Arrange
            var controller = CreateRidesControllerAs("robcon");

            // Act
            ViewResult result = (ViewResult)controller.Index(null, 0);

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IList<Ride>), "Index does not have an IList<Ride> as a ViewModel");
        }


        [TestMethod]
        public void IndexAction_Should_Return_PagedList() {
            
            // Arrange
            var controller = CreateRidesControllerAs("robcon");

            // Act
            //Get first page
            ViewResult result = (ViewResult)controller.Index(null, 0);
            
            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(PaginatedList<Ride>));
        }


        [TestMethod]
        public void IndexAction_Should_Return_PagedList_With_Total_of_101_And_Total_10_Pages() {

            // Arrange
            var controller = CreateRidesControllerAs("robcon");

            // Act
            // Get first page
            ViewResult result = (ViewResult)controller.Index(null, 0);
            PaginatedList<Ride> list = result.ViewData.Model as PaginatedList<Ride>;

            // Assert
            Assert.AreEqual(101, list.TotalCount);
            Assert.AreEqual(5, list.TotalPages);
        }

        [TestMethod]
        public void IndexAction_Should_Return_PagedList_With_Total_of_101_And_Total_5_Pages_Given_Null()
        {

            // Arrange
            var controller = CreateRidesControllerAs("robcon");

            // Act
            // Get first page
            ViewResult result = (ViewResult)controller.Index(null, null);
            PaginatedList<Ride> list = result.ViewData.Model as PaginatedList<Ride>;

            // Assert
            Assert.AreEqual(101, list.TotalCount);
            Assert.AreEqual(5, list.TotalPages);
        }

		[TestMethod]
		public void IndexAction_With_Ride_Just_Started_Should_Show_Ride()
		{
			// Arrange 
			var testData = FakeRideData.CreateTestRides();
			var Ride = FakeRideData.CreateRide();
			Ride.EventDate = DateTime.Now.AddHours(-1);
			Ride.Title = "Ride which just started";
			testData.Add(Ride);
			var repository = new FakeRideRepository(testData);

			var controller = new RidesController(repository);

			// Act
			// Get first page
			ViewResult result = (ViewResult)controller.Index(null, null);
			PaginatedList<Ride> list = result.ViewData.Model as PaginatedList<Ride>;

			// Assert
			Assert.AreEqual("Ride which just started", list.First().Title);
		}

        [TestMethod]
        public void IndexAction_With_Search_Term_Should_Filter()
        {
            // Arrange 
            string searchterm = "Ride we will be searching for (spaghetti)";

            var testData = FakeRideData.CreateTestRides();
            var Ride = FakeRideData.CreateRide();
            Ride.Title = searchterm;
            testData.Add(Ride);
            var repository = new FakeRideRepository(testData);

            var controller = new RidesController(repository);

            // Act
            // Get first page
            ViewResult result = (ViewResult)controller.Index("etti", null);
            PaginatedList<Ride> list = result.ViewData.Model as PaginatedList<Ride>;

            // Assert
            Assert.AreEqual(searchterm, list.First().Title);
        }

		[TestMethod]
        public void DetailsAction_Should_Return_ViewResult() {

            // Arrange
            var controller = CreateRidesControllerAs("scottgu");

            // Act
            var result = controller.Details(1);

            // Assert
            Assert.IsNotNull(result, "There is no Details action");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        
        //[TestMethod]
        //public void DownloadCalendarAction_Should_Return_ContentResult() {
            
        //    // Arrange
        //    var mockResponse = new Mock<System.Web.HttpResponseBase>();
        //    var mockContext = new Mock<System.Web.HttpContextBase>();
        //    mockContext.Setup(p => p.Response).Returns(mockResponse.Object);
        //    var requestContext = new RequestContext(mockContext.Object, new RouteData());

        //    var controller = CreateRidesController();
        //    controller.ControllerContext = new ControllerContext(requestContext, controller);

        //    // Act
        //    var result = controller.DownloadCalendar(1);

        //    // Assert
        //    Assert.IsNotNull(result, "There is no DownloadCalendar action");
        //    Assert.IsInstanceOfType(result, typeof(ContentResult));
        //}

        [TestMethod]
        public void DetailsAction_Should_Return_FileNotFoundResult_For_NullRideId() {
            // Arrange
            var controller = CreateRidesControllerAs("scottgu");

            // Act
            FileNotFoundResult result = (FileNotFoundResult)controller.Details(null);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DetailsAction_Should_Return_FileNotFoundResult_For_Ride_999() {

            // Arrange
            var controller = CreateRidesControllerAs("scottgu");

            // Act
            FileNotFoundResult result = (FileNotFoundResult)controller.Details(999);

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void DetailsAction_Should_Have_ViewModel_Is_Ride() {

            // Arrange
            var controller = CreateRidesControllerAs("scottgu");

            // Act
            ViewResult result = (ViewResult)controller.Details(1);

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Ride));

        }

        [TestMethod]
        public void DetailsAction_Should_Return_Ride_HostedBy_SomeUser() {

            // Arrange
            var controller = CreateRidesControllerAs("SomeUser");

            // Act

            //the mocked user is "SomeUser", who also owns the Ride
            ViewResult result = (ViewResult)controller.Details(1);
            Ride model = result.ViewData.Model as Ride;
            
            // Assert
            
            //scottgu, our mock user, is the host in the fake
            Assert.IsTrue(model.IsHostedBy("SomeUser"));
        }

        [TestMethod]
        public void CreateAction_Should_Return_ViewResult() {

            // Arrange
            var controller = CreateRidesControllerAs("scottgu");

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CreateAction_Should_Return_RideFormViewModel() {
            
            // Arrange
            var controller = CreateRidesControllerAs("scottgu");
            
            // Act
            ViewResult result = (ViewResult)controller.Create();

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(RideFormViewModel));
        }

        [TestMethod]
        public void CreateAction_Should_Return_RideFormViewModel_With_New_Ride_And_Countries_List() {

            // Arrange
            var controller = CreateRidesControllerAs("scottgu");

            // Act
            ViewResult result = (ViewResult)controller.Create();
            RideFormViewModel model = (RideFormViewModel)result.ViewData.Model;
            
            // Assert
            Assert.IsNotNull(model.Ride);
            Assert.AreEqual(NumberOfCountries, model.Countries.Count());
        }

        [TestMethod]
        public void CreateAction_Should_Return_RideFormViewModel_With_New_Ride_7_Days_In_Future() {
            
            // Arrange
            var controller = CreateRidesControllerAs("scottgu");
            
            // Act
            ViewResult result = (ViewResult)controller.Create();
            
            // Assert
            RideFormViewModel model = (RideFormViewModel)result.ViewData.Model;
            Assert.IsTrue(model.Ride.EventDate > DateTime.Today.AddDays(6) && model.Ride.EventDate < DateTime.Today.AddDays(8));
        }

        [TestMethod]
        public void CreateAction_With_New_Ride_Should_Return_View_And_Repo_Should_Contain_New_Ride()
        {
            // Arrange 
            var mock = new Mock<ControllerContext>();

            var nerdIdentity = FakeIdentity.CreateIdentity("SomeUser");
            var testData = FakeRideData.CreateTestRides();
            var repository = new FakeRideRepository(testData);
            var controller = new RidesController(repository);
            controller.ControllerContext = mock.Object;
            mock.SetupGet(p => p.HttpContext.User.Identity).Returns(nerdIdentity);

            var Ride = FakeRideData.CreateRide();

            // Act
            ActionResult result = (ActionResult)controller.Create(Ride);

            // Assert
            Assert.AreEqual(102, repository.FindAllRides().Count());
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void EditAction_Should_Return_ViewResult() {
            
            // Arrange
            var controller = CreateRidesControllerAs("robcon");

            // Act
            var result = controller.Edit(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void EditAction_Returns_InvalidOwner_View_When_Not_SomeUser() {
            
            // Arrange
            var controller = CreateRidesControllerAs("robcon");

            // Act
            ViewResult result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.AreEqual("InvalidOwner", result.ViewName);
        }

        [TestMethod]
        public void EditAction_Uses_RideFormViewModel() {

            // Arrange
            var controller = CreateRidesControllerAs("SomeUser");

            // Act
            ViewResult result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Ride));
        }

        [TestMethod]
        public void EditAction_Retrieves_Ride_1_From_Repo() {

            // Arrange
            var controller = CreateRidesControllerAs("SomeUser");

            // Act
            ViewResult result = controller.Edit(1) as ViewResult;

            // Assert
            var model = result.ViewData.Model as Ride;
            Assert.AreEqual(1, model.RideID);
        }

        [TestMethod]
        public void EditAction_Saves_Changes_To_Ride_1()
        {
            // Arrange
            var repo = new FakeRideRepository(FakeRideData.CreateTestRides());
            var controller = CreateRidesControllerAs("SomeUser");
            var form = FakeRideData.CreateRideFormCollection();
            form["Ride.Description"] = "New, Updated Description";
            controller.ValueProvider = form.ToValueProvider();

            // Act
            ActionResult result = (ActionResult)controller.Edit(1, form);
            ViewResult detailResult = (ViewResult)controller.Details(1);
            var Ride = detailResult.ViewData.Model as Ride;

            // Assert
            Assert.AreEqual(5, controller.ModelState.Count);
            Assert.AreEqual("New, Updated Description", Ride.Description);
        }

        [TestMethod]
        public void EditAction_Fails_With_Wrong_Owner() {
            
            // Arrange
            var repo = new FakeRideRepository(FakeRideData.CreateTestRides());
            var controller = CreateRidesControllerAs("fred");
            var form = FakeRideData.CreateRideFormCollection();
            controller.ValueProvider = form.ToValueProvider();

            // Act
            ViewResult result = (ViewResult)controller.Edit(1, form);

            // Assert
            Assert.AreEqual("InvalidOwner", result.ViewName);
        }

		//Unit test is invalid until phone number verification is turned back on
		//[TestMethod]
		//public void RidesController_Edit_Post_Should_Fail_Given_Bad_US_Phone_Number() {
            
		//    // Arrange
		//    var controller = CreateRidesControllerAs("someuser");
		//    var form = FakeRideData.CreateRideFormCollection();
		//    form["ContactPhone"] = "foo"; //BAD
		//    controller.ValueProvider = form.ToValueProvider();

		//    // Act
		//    var result = controller.Edit(1, form);

		//    // Assert
		//    Assert.IsInstanceOfType(result, typeof(ViewResult));
		//    var viewResult = (ViewResult)result;
		//    Assert.IsFalse(viewResult.ViewData.ModelState.IsValid);
		//    Assert.AreEqual(1, viewResult.ViewData.ModelState.Sum(p => p.Value.Errors.Count), "Expected Errors");
		//    ModelState m = viewResult.ViewData.ModelState["ContactPhone"];
		//    Assert.IsTrue(m.Errors.Count == 1);
		//}


        [TestMethod]
        public void DeleteAction_Should_Return_View()
        {
            // Arrange
            var controller = CreateRidesControllerAs("someuser");

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DeleteAction_Should_Return_NotFound_For_999()
        {

            // Arrange
            var controller = CreateRidesControllerAs("scottgu");

            // Act
            ViewResult result = controller.Delete(999) as ViewResult;

            // Assert
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void DeleteAction_Should_Return_InvalidOwner_For_Robcon()
        {

            // Arrange
            var controller = CreateRidesControllerAs("robcon");

            // Act
            ViewResult result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.AreEqual("InvalidOwner", result.ViewName);
        }

        [TestMethod]
        public void DeleteAction_Should_Delete_Ride_1_And_Returns_Deleted_View() {

            // Arrange
            var controller = CreateRidesControllerAs("SomeUser");

            // Act
            ViewResult result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.AreNotEqual("NotFound", result.ViewName);
            Assert.AreNotEqual("InvalidOwner", result.ViewName);
        }

        [TestMethod]
        public void DeleteAction_With_Confirm_Should_Delete_Ride_1_And_Returns_Deleted_View()
        {

            // Arrange
            var controller = CreateRidesControllerAs("SomeUser");

            // Act
            ViewResult result = controller.Delete(1, String.Empty) as ViewResult;

            // Assert
            Assert.AreNotEqual("NotFound", result.ViewName);
            Assert.AreNotEqual("InvalidOwner", result.ViewName);
        }

        
        [TestMethod]
        public void DeleteAction_Should_Fail_With_NotFound_Given_Invalid_Ride()
        {

            // Arrange
            var controller = CreateRidesControllerAs("robcon");

            // Act
            ViewResult result = controller.Delete(200, "") as ViewResult;

            // Assert
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void DeleteAction_Should_Fail_With_InvalidOwner_Given_Wrong_User()
        {

            // Arrange
            var controller = CreateRidesControllerAs("scottha");

            // Act
            ViewResult result = controller.Delete(1, "") as ViewResult;

            // Assert
            Assert.AreEqual("InvalidOwner", result.ViewName);
        }
    }
}
