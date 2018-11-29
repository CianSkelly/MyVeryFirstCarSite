using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyVeryFirstCarSite.Areas.Admin.Models;
using MyVeryFirstCarSite.Controllers;
using MyVeryFirstCarSite.Entities;
using MyVeryFirstCarSiteTest.TestContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyVeryFirstCarSite.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            HomeController controller = new HomeController();
            //Act
            ViewResult result = controller.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void SearchTest()
        {
            //Arrange
            HomeController controller = new HomeController();
            //Act
            ViewResult result = controller.Search() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task SearchIndexTest1Async()
        {
            //Arrange
                //creating the fake database with vehicles, tdb = test database
                TestApplicationDbContext tdb = new TestApplicationDbContext();
                Vehicle veh1 = new Vehicle
                {
                    Description = "Test vehicle 1",
                    Price = 1000
                };
                Vehicle veh2 = new Vehicle
                {
                    Description = "Test vehicle 2",
                    Price = 1100
                };
                Vehicle veh3 = new Vehicle
                {
                    Description = "Test vehicle 3",
                    Price = 1250
                };

            //adding the three vehicles to the "fake" database
            tdb.Vehicles.Add(veh1);
                tdb.Vehicles.Add(veh2);
                tdb.Vehicles.Add(veh3);

            VehicleSearchModel priceSearch = new VehicleSearchModel
            {
                Price = 1200
            };

            //creating a controller using the test db
            HomeController controller = new HomeController(tdb);

            //Act
            //call controller searchIndexMethod
            var viewResult = await controller.SearchIndex(priceSearch) as ViewResult;
            var result = viewResult.ViewData.Model as IEnumerable<VehicleModel>;

            //Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod()]
        public async Task SearchIndexTest2Async()
        {
            //Arrange
            //creating the fake database with vehicles, tdb = test database
            TestApplicationDbContext tdb = new TestApplicationDbContext();
            Vehicle veh1 = new Vehicle
            {
                Description = "Test vehicle 1",
                ManufacturerYear = 1999
            };
            Vehicle veh2 = new Vehicle
            {
                Description = "Test vehicle 2",
                ManufacturerYear = 1998
            };
            Vehicle veh3 = new Vehicle
            {
                Description = "Test vehicle 3",
                ManufacturerYear = 2001
            };

            //adding the three vehicles to the "fake" database
            tdb.Vehicles.Add(veh1);
            tdb.Vehicles.Add(veh2);
            tdb.Vehicles.Add(veh3);

            VehicleSearchModel yearSearch = new VehicleSearchModel
            {
                MaxManufacturerYear = 2000
            };

            //creating a controller using the test db
            HomeController controller = new HomeController(tdb);

            //Act
            //call controller searchIndexMethod
            var viewResult = await controller.SearchIndex(yearSearch) as ViewResult;
            var result = viewResult.ViewData.Model as IEnumerable<VehicleModel>;

            //Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod()]
        public void AboutTest()
        {

        }

        [TestMethod()]
        public void ContactTest()
        {

        }
    }
}