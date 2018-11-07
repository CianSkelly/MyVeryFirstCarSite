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
using MyVeryFirstCarSite.Areas.Admin.Models;
using MyVeryFirstCarSite.Areas.Admin.Extensions;

namespace MyVeryFirstCarSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/VehicleItem
        public async Task<ActionResult> Index()
        {
            return View(await db.VehicleItems.Convert(db));
        }

        // GET: Admin/VehicleItem/Details/5
        public async Task<ActionResult> Details(int? itemId, int? vehicleId)
        {
            if (itemId == null || vehicleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleItem vehicleItem = await GetVehicleItem(itemId, vehicleId);
            if (vehicleItem == null)
            {
                return HttpNotFound();
            }

            return View(await vehicleItem.Convert(db));
        }

        // GET: Admin/VehicleItem/Create
        public async Task<ActionResult> Create()
        {
            var model = new VehicleItemModel
            {
                Items = await db.Items.ToListAsync(),
                Vehicles = await db.Vehicles.ToListAsync(),
            };
            return View(model);
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
        public async Task<ActionResult> Edit(int? itemId, int? vehicleId)
        {
            if (itemId == null || vehicleId == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleItem vehicleItem = await GetVehicleItem(itemId, vehicleId);
            if (vehicleItem == null)
            {
                return HttpNotFound();
            }
            return View(await vehicleItem.Convert(db));
        }

        // POST: Admin/VehicleItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VehicleId,ItemId,OldVehicleId,OldItemId")] VehicleItem vehicleItem)
        {
            if (ModelState.IsValid)
            {
                var canChange = await vehicleItem.CanChange(db);
                if (canChange)
                    await vehicleItem.Change(db);

                return RedirectToAction("Index");
            }
            return View(vehicleItem);
        }

        // GET: Admin/VehicleItem/Delete/5
        public async Task<ActionResult> Delete(int? itemId, int? vehicleId)
        {
            if (itemId == null || vehicleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleItem vehicleItem = await GetVehicleItem(itemId, vehicleId);
            if (vehicleItem == null)
            {
                return HttpNotFound();
            }

            return View(await vehicleItem.Convert(db));
        }

        // POST: Admin/VehicleItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int itemId, int vehicleId)
        {
            VehicleItem vehicleItem = await GetVehicleItem(itemId, vehicleId);
            db.VehicleItems.Remove(vehicleItem);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //below I'm getting a vehicle item and use the GetVehicleItem method and pass in 2 variables from the edit action
        //then parse the values and store the result in 2 variables called itmId and vehId, then return the result from
        //the method
        private async Task<VehicleItem> GetVehicleItem(int? itemId, int? vehicleId)
        {
            try
            {
                int itmId = 0, vehId = 0;
                int.TryParse(itemId.ToString(), out itmId);
                int.TryParse(vehicleId.ToString(), out vehId);
                var vehicleItem = await db.VehicleItems.FirstOrDefaultAsync
                    (vi => vi.VehicleId.Equals(vehId) && vi.ItemId.Equals(itmId));
                return vehicleItem;
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
