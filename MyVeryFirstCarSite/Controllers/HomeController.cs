using MyVeryFirstCarSite.Areas.Admin.Extensions;
using MyVeryFirstCarSite.Areas.Admin.Models;
using MyVeryFirstCarSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyVeryFirstCarSite.Controllers
{
    public class HomeController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        //needed the below code to reference te fake data for unit 
        //test as opposed to referencing the actual database data
        private IApplicationDbContext db = new ApplicationDbContext();

        //added for unit testing
        public HomeController() { }

        //added for unit testing
        public HomeController(IApplicationDbContext context)
        {
            db = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        //// SEARCH: Admin/Vehicle/SearchIndex
        [AllowAnonymous]
        public async Task<ActionResult> SearchIndex(VehicleSearchModel searchModel)
        {
            var result = db.Vehicles.AsQueryable();
            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.Make))
                {
                    result = result.Where(t => t.VehicleType.Make.Equals(searchModel.Make));
                }
                if (!string.IsNullOrEmpty(searchModel.Model))
                {
                    result = result.Where(t => t.VehicleType.Model.Equals(searchModel.Model));
                }
                if (searchModel.Price.HasValue)
                {
                    result = result.Where(t => t.Price <= searchModel.Price);
                }
                if (searchModel.MinManufacturerYear.HasValue)
                {
                    result = result.Where(t => t.ManufacturerYear >= searchModel.MinManufacturerYear);
                }
                if (searchModel.MaxManufacturerYear.HasValue)
                {
                    result = result.Where(t => t.ManufacturerYear <= searchModel.MaxManufacturerYear);
                }
                if (searchModel.CountySoldFrom.HasValue)
                {
                    result = result.Where(t => t.CountySoldFrom == searchModel.CountySoldFrom);
                }

            }
            var vehicles = await result.ToListAsync();
            var model = await vehicles.Convert(db);
            //return View(model);
            return View(model.OrderBy(t => t.Price));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}