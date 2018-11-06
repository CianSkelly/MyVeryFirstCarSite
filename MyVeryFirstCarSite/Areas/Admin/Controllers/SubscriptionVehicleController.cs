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
    public class SubscriptionVehicleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SubscriptionVehicle
        public async Task<ActionResult> Index()
        {
            return View(await db.SubscriptionVehicles.Convert(db));
        }

        // GET: Admin/SubscriptionVehicle/Details/5
        public async Task<ActionResult> Details(int? subscriptionId, int? vehicleId)
        {
            if (subscriptionId == null || vehicleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionVehicle subscriptionVehicle = 
                await GetSubscriptionVehicle(subscriptionId, vehicleId);
            if (subscriptionVehicle == null)
            {
                return HttpNotFound();
            }
            return View(await subscriptionVehicle.Convert(db));
        }

        // GET: Admin/SubscriptionVehicle/Create
        public async Task<ActionResult> Create()
        {
            var model = new SubscriptionVehicleModel
            {
                Subscriptions = await db.Subscriptions.ToListAsync(),
                Vehicles = await db.Vehicles.ToListAsync(),
            };
            return View(model);
        }

        // POST: Admin/SubscriptionVehicle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VehicleId,SubscriptionId")] SubscriptionVehicle subscriptionVehicle)
        {
            if (ModelState.IsValid)
            {
                db.SubscriptionVehicles.Add(subscriptionVehicle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(subscriptionVehicle);
        }

        // GET: Admin/SubscriptionVehicle/Edit/5
        public async Task<ActionResult> Edit(
            int? subscriptionId, int? vehicleId)
        {
            if (subscriptionId == null || vehicleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionVehicle subscriptionVehicle = 
                await GetSubscriptionVehicle(subscriptionId, vehicleId);

            if (subscriptionVehicle == null)
            {
                return HttpNotFound();
            }
            return View(await subscriptionVehicle.Convert(db));
        }

        // POST: Admin/SubscriptionVehicle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(
            Include = "VehicleId,SubscriptionId,OldVehicleId,OldSubscriptionId")]
        SubscriptionVehicle subscriptionVehicle)
        {
            if (ModelState.IsValid)
            {
                var canChange = await subscriptionVehicle.CanChange(db);
                if (canChange)
                    await subscriptionVehicle.Change(db);

                return RedirectToAction("Index");
            }
            return View(subscriptionVehicle);
        }

        // GET: Admin/SubscriptionVehicle/Delete/5
        public async Task<ActionResult> Delete(int? subscriptionId, int? vehicleId )
        {
            if (subscriptionId == null || vehicleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionVehicle subscriptionVehicle = 
                await GetSubscriptionVehicle(subscriptionId, vehicleId);

            if (subscriptionVehicle == null)
            {
                return HttpNotFound();
            }
            return View(await subscriptionVehicle.Convert(db));
        }

        // POST: Admin/SubscriptionVehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(
            int subscriptionId, int vehicleId)
        {
            SubscriptionVehicle subscriptionVehicle = 
                await GetSubscriptionVehicle(subscriptionId, vehicleId);
            db.SubscriptionVehicles.Remove(subscriptionVehicle);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<SubscriptionVehicle> GetSubscriptionVehicle(int? subscriptionId, int? vehicleId)
        {
            try
            {
                int subscId = 0, vehId = 0;
                int.TryParse(subscriptionId.ToString(), out subscId);
                int.TryParse(vehicleId.ToString(), out vehId);
                var subscriptionVehicle = await db.SubscriptionVehicles.FirstOrDefaultAsync
                    (vi => vi.VehicleId.Equals(vehId) && vi.SubscriptionId.Equals(subscId));
                return subscriptionVehicle;
            }
            catch { return null; }
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
