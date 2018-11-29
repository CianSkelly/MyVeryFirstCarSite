using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyVeryFirstCarSite.Entities;
using MyVeryFirstCarSite.Models;
using MyVeryFirstCarSite.Areas.Admin.Extensions;
using MyVeryFirstCarSite.Areas.Admin.Models;

namespace MyVeryFirstCarSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        //modified the line above (to below) for unit testing
        private IApplicationDbContext db = new ApplicationDbContext();

        //added for unit testing
        public VehicleController() { }

        //added for unit testing
        public VehicleController(IApplicationDbContext context)
        {
            db = context;
        }

        // GET: Admin/Vehicle
        public async Task<ActionResult> Index()
        {
            var vehicles = await db.Vehicles.ToListAsync();
            var model = await vehicles.Convert(db);
            return View(model);
        }

        // GET: Admin/Vehicle/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            var model = await vehicle.Convert(db);
            return View(model);
        }

        // GET: Admin/Vehicle/Create
        public async Task<ActionResult> Create()
        {
            var model = new VehicleModel
            {
                VehicleLinkTexts = await db.VehicleLinkTexts.ToListAsync(),
                VehicleTypes = await db.VehicleTypes.ToListAsync()
            };
            return View(model);
        }

        // POST: Admin/Vehicle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,ImageUrl,CubicCapicity," +
        //    "FuelType,Colour,CountySoldFrom,ManufacturerYear,NumberOfPreviousOwners,VehicleLinkTextId,VehicleTypeId")]

        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,ImageUrl,CubicCapicity,FuelType,Price,Colour,CountySoldFrom,ManufacturerYear,NumberOfPreviousOwners,VehicleLinkTextId,VehicleTypeId")]
        Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var veh = new List<Vehicle>();
            veh.Add(vehicle);
            var VehicleModel = await veh.Convert(db);
            return View(VehicleModel.First());
            //return View(vehicle);
        }

        // GET: Admin/Vehicle/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            var veh = new List<Vehicle>();
            veh.Add(vehicle);
            var VehicleModel = await veh.Convert(db);
            return View(VehicleModel.First());
        }

        // POST: Admin/Vehicle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //"Id,Title,Description,ImageUrl,CubicCapicity,
        //FuelType,Price,Colour,CountySoldFrom,ManufacturerYear,
        //NumberOfPreviousOwners,VehicleLinkTextId,VehicleTypeId"
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,ImageUrl,CubicCapicity,FuelType,Price,Colour,CountySoldFrom,ManufacturerYear,NumberOfPreviousOwners,VehicleLinkTextId,VehicleTypeId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(vehicle).State = EntityState.Modified;
                db.MarkAsModified(vehicle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var veh = new List<Vehicle>();
            veh.Add(vehicle);
            var VehicleModel = await veh.Convert(db);
            return View(VehicleModel.First());
           // return View(vehicle);
        }

        

        // GET: Admin/Vehicle/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            var model = await vehicle.Convert(db);
            return View(model);
        }

        // POST: Admin/Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Vehicle vehicle = await db.Vehicles.FindAsync(id);
            db.Vehicles.Remove(vehicle);
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
