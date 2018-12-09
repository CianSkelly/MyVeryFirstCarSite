using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyVeryFirstCarSite.Areas.Admin.Controllers;
using MyVeryFirstCarSite.Entities;
using MyVeryFirstCarSiteTest.TestContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyVeryFirstCarSite.Areas.Admin.Controllers.Tests
{
    [TestClass()]
    public class VehicleControllerTests
    {

        [TestMethod()]
        public void IndexTest()
        {

        }

        [TestMethod()]
        public void DetailsTest()
        {

        }

        [TestMethod()]
        public async Task CanCreateVehicleTestAsync()
        {
            //Arrange
            TestApplicationDbContext tdb = new TestApplicationDbContext();
            Vehicle vehicleToBeCreated = new Vehicle
            {
                Title = "Nissan Micra",
                Price = 1250,
                CubicCapicity = CC.cc1000,
                FuelType = Fuel.Petrol
            };

            //creating a controller
            VehicleController controller = new VehicleController(tdb);

            //Act
            //call controller searchIndexMethod
            var result = await controller.Create(vehicleToBeCreated);
            //var result = viewResult.ViewData.Model as IEnumerable<VehicleModel>;

            //Assert
            //we are checking that a view called Index is returned
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            //view Result for the cannot create
        }

        [TestMethod()]
        public async Task CannotDuplicateVehicleTestAsync()
        {
            //Arrange
            //creating the fake vehicle that I'll try duplicate, tdb = test database
            TestApplicationDbContext tdb = new TestApplicationDbContext();
            Vehicle vehicleToNotBeDuplicated = new Vehicle
            {
                Description = "Vehicle with same description",
                Title = "Nissan Micra",
                Price = 1500,
                CubicCapicity = CC.cc1000,
                FuelType = Fuel.Petrol
            };

            Vehicle vehicleToTryDuplicate = new Vehicle
            {
                Description = "Vehicle with same description",
                Title = "Nissan Micra",
                Price = 1250,
                CubicCapicity = CC.cc1000,
                FuelType = Fuel.Petrol
            };

            //adding the vehicles to the "fake" database
            //I shouldn't be able to do this
            tdb.Vehicles.Add(vehicleToTryDuplicate);

            //creating a controller
            VehicleController controller = new VehicleController(tdb);

            //Act
            //call controller searchIndexMethod
            var result = await controller.Create(vehicleToNotBeDuplicated);
            //var result = viewResult.ViewData.Model as IEnumerable<VehicleModel>;

            //Assert
            //we are checking that a view called Index is returned
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            //view Result for the cannot create
        }

        [TestMethod()]
        public void CreateTest1()
        {

        }

        [TestMethod()]
        public void EditTest()
        {

        }

        [TestMethod()]
        public void EditTest1()
        {

        }

        [TestMethod()]
        public void DeleteTest()
        {

        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {

        }
    }
}