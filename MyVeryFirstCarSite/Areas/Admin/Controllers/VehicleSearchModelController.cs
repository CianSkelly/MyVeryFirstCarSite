using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyVeryFirstCarSite.Areas.Admin.Models;
using MyVeryFirstCarSite.Models;

namespace MyVeryFirstCarSite.Areas.Admin.Controllers
{
    public class VehicleSearchModelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/VehicleSearchModel
        public async Task<ActionResult> Index()
        {
            return View(await db.VehicleModels.ToListAsync());
        }

        // GET: Admin/VehicleSearchModel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await db.VehicleModels.FindAsync(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // GET: Admin/VehicleSearchModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/VehicleSearchModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,ImageUrl,VehicleLinkTextId,VehicleTypeId,Price,CubicCapicity,FuelType,Colour,CountySoldFrom,ManufacturerYear,NumberOfPreviousOwners")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                db.VehicleModels.Add(vehicleModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vehicleModel);
        }

        // GET: Admin/VehicleSearchModel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await db.VehicleModels.FindAsync(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // POST: Admin/VehicleSearchModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,ImageUrl,VehicleLinkTextId,VehicleTypeId,Price,CubicCapicity,FuelType,Colour,CountySoldFrom,ManufacturerYear,NumberOfPreviousOwners")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vehicleModel);
        }

        // GET: Admin/VehicleSearchModel/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await db.VehicleModels.FindAsync(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // SEARCH: Admin/Vehicle/Search
        [AllowAnonymous]
        public ActionResult Search(VehicleSearchModel searchModel)
        {
            var result = db.Vehicles.AsQueryable();
            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.Title))
                {
                    result = result.Where(t => t.Title.Contains(searchModel.Title));
                }
                if (!string.IsNullOrEmpty(searchModel.Price))
                {
                    result = result.Where(t => t.Price.Contains(searchModel.Price));
                }

                //if (!string.IsNullOrEmpty(searchModel.ManufacturerYear))
                //{
                //    result = result.Where(t => t.ManufacturerYear.Contains(searchModel.ManufacturerYear));
                //}

                if (searchModel.CountySoldFrom.HasValue)
                {
                    result = result.Where(t => t.CountySoldFrom == searchModel.CountySoldFrom);
                }
                if (searchModel.CubicCapicity.HasValue)
                {
                    result = result.Where(t => t.CubicCapicity == searchModel.CubicCapicity);
                }
                if (searchModel.FuelType.HasValue)
                {
                    result = result.Where(t => t.FuelType == searchModel.FuelType);
                }
            }
            return View("Index", result.OrderByDescending(t => t.Title));
        }

        // POST: Admin/VehicleSearchModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VehicleModel vehicleModel = await db.VehicleModels.FindAsync(id);
            db.VehicleModels.Remove(vehicleModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
