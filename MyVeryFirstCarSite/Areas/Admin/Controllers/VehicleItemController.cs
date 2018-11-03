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

namespace MyVeryFirstCarSite.Areas.Admin.Controllers
{
    public class VehicleItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/VehicleItem
        public async Task<ActionResult> Index()
        {
            return View(await db.VehicleItems.ToListAsync());
        }

        // GET: Admin/VehicleItem/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleItem vehicleItem = await db.VehicleItems.FindAsync(id);
            if (vehicleItem == null)
            {
                return HttpNotFound();
            }
            return View(vehicleItem);
        }

        // GET: Admin/VehicleItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/VehicleItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VehicleId,ItemId")] VehicleItem vehicleItem)
        {
            if (ModelState.IsValid)
            {
                db.VehicleItems.Add(vehicleItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vehicleItem);
        }

        // GET: Admin/VehicleItem/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleItem vehicleItem = await db.VehicleItems.FindAsync(id);
            if (vehicleItem == null)
            {
                return HttpNotFound();
            }
            return View(vehicleItem);
        }

        // POST: Admin/VehicleItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VehicleId,ItemId")] VehicleItem vehicleItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vehicleItem);
        }

        // GET: Admin/VehicleItem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleItem vehicleItem = await db.VehicleItems.FindAsync(id);
            if (vehicleItem == null)
            {
                return HttpNotFound();
            }
            return View(vehicleItem);
        }

        // POST: Admin/VehicleItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VehicleItem vehicleItem = await db.VehicleItems.FindAsync(id);
            db.VehicleItems.Remove(vehicleItem);
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
