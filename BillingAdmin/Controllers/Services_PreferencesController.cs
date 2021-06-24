using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillingAdmin.Models;

namespace BillingAdmin.Controllers
{
    public class Services_PreferencesController : Controller
    {
        private Entities db = new Entities();

        // GET: Services_Preferences
        public ActionResult Index()
        {
            var services_Preferences = db.Services_Preferences.Include(s => s.Services).Include(s => s.Services_Preferences_HB);
            return View(services_Preferences.ToList());
        }

        // GET: Services_Preferences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services_Preferences services_Preferences = db.Services_Preferences.Find(id);
            if (services_Preferences == null)
            {
                return HttpNotFound();
            }
            return View(services_Preferences);
        }

        // GET: Services_Preferences/Create
        public ActionResult Create()
        {
            ViewBag.serviceid = new SelectList(db.Services, "id", "name");
            ViewBag.preferenceId = new SelectList(db.Services_Preferences_HB, "id", "name");
            return View();
        }

        // POST: Services_Preferences/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,serviceid,preferenceId")] Services_Preferences services_Preferences)
        {
            if (ModelState.IsValid)
            {
                db.Services_Preferences.Add(services_Preferences);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.serviceid = new SelectList(db.Services, "id", "name", services_Preferences.serviceid);
            ViewBag.preferenceId = new SelectList(db.Services_Preferences_HB, "id", "name", services_Preferences.preferenceId);
            return View(services_Preferences);
        }

        // GET: Services_Preferences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services_Preferences services_Preferences = db.Services_Preferences.Find(id);
            if (services_Preferences == null)
            {
                return HttpNotFound();
            }
            ViewBag.serviceid = new SelectList(db.Services, "id", "name", services_Preferences.serviceid);
            ViewBag.preferenceId = new SelectList(db.Services_Preferences_HB, "id", "name", services_Preferences.preferenceId);
            return View(services_Preferences);
        }

        // POST: Services_Preferences/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,serviceid,preferenceId")] Services_Preferences services_Preferences)
        {
            if (ModelState.IsValid)
            {
                db.Entry(services_Preferences).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.serviceid = new SelectList(db.Services, "id", "name", services_Preferences.serviceid);
            ViewBag.preferenceId = new SelectList(db.Services_Preferences_HB, "id", "name", services_Preferences.preferenceId);
            return View(services_Preferences);
        }

        // GET: Services_Preferences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services_Preferences services_Preferences = db.Services_Preferences.Find(id);
            if (services_Preferences == null)
            {
                return HttpNotFound();
            }
            return View(services_Preferences);
        }

        // POST: Services_Preferences/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Services_Preferences services_Preferences = db.Services_Preferences.Find(id);
            db.Services_Preferences.Remove(services_Preferences);
            db.SaveChanges();
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
