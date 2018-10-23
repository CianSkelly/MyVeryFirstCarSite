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
    public class VehicleLinkTextController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/VehicleLinkText
        public async Task<ActionResult> Index()
        {
            return View(await db.VehicleLinkTexts.ToListAsync());
        }

        // GET: Admin/VehicleLinkText/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleLinkText vehicleLinkText = await db.VehicleLinkTexts.FindAsync(id);
            if (vehicleLinkText == null)
            {
                return HttpNotFound();
            }
            return View(vehicleLinkText);
        }

        // GET: Admin/VehicleLinkText/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/VehicleLinkText/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title")] VehicleLinkText vehicleLinkText)
        {
            if (ModelState.IsValid)
            {
                db.VehicleLinkTexts.Add(vehicleLinkText);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vehicleLinkText);
        }

        // GET: Admin/VehicleLinkText/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleLinkText vehicleLinkText = await db.VehicleLinkTexts.FindAsync(id);
            if (vehicleLinkText == null)
            {
                return HttpNotFound();
            }
            return View(vehicleLinkText);
        }

        // POST: Admin/VehicleLinkText/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title")] VehicleLinkText vehicleLinkText)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleLinkText).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vehicleLinkText);
        }

        // GET: Admin/VehicleLinkText/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleLinkText vehicleLinkText = await db.VehicleLinkTexts.FindAsync(id);
            if (vehicleLinkText == null)
            {
                return HttpNotFound();
            }
            return View(vehicleLinkText);
        }

        // POST: Admin/VehicleLinkText/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VehicleLinkText vehicleLinkText = await db.VehicleLinkTexts.FindAsync(id);
            db.VehicleLinkTexts.Remove(vehicleLinkText);
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
