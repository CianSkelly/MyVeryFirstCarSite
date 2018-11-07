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
    [Authorize(Roles = "Admin")]
    public class SectionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Section
        public async Task<ActionResult> Index()
        {
            return View(await db.Sections.ToListAsync());
        }

        // GET: Admin/Section/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: Admin/Section/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Section/Create - used when we fill in data in the box on the create view, 
        //and click the create button. The data is sent back to the server from the clients' input
        [HttpPost]
        [ValidateAntiForgeryToken]
        //the "Bind" below mean that only ID & Title are allowed to be sent in to this action. 
        //Prevents malicous code entering.
        public async Task<ActionResult> Create([Bind(Include = "Id,Title")] Section section)
        {
            if (ModelState.IsValid)
            {
                //1st - section object added to the section table
                db.Sections.Add(section);
                //then await the result from the SaveChangesAsync method which adds the data to the DB 
                //once no exception thrown
                await db.SaveChangesAsync();
                //then redirect to the index action so the data that has been added can be viewed
                return RedirectToAction("Index");
            }
            //else re-render the view with the error data so we can try re-create
            return View(section);
        }

        // GET: Admin/Section/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Admin/Section/Edit/5 - use this view to add data to the DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        //again, action object sent in and only allow Id & title
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title")] Section section)
        {
            if (ModelState.IsValid)
            {
                //telling the DB that it ahs beeen edited...
                db.Entry(section).State = EntityState.Modified;
                //as above
                await db.SaveChangesAsync();
                //see the changes
                return RedirectToAction("Index");
            }
            //else re-render the view
            return View(section);
        }

        // GET: Admin/Section/Delete/5 - delete the item with only the Id sent in
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Admin/Section/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Section section = await db.Sections.FindAsync(id);
            db.Sections.Remove(section);
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
