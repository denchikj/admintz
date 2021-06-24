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
    public class Services_Preferences_HBController : Controller
    {
        private Entities db = new Entities();

        // GET: Services_Preferences_HB
        public ActionResult Index()
        {
            var services_Preferences_HB = db.Services_Preferences_HB.Include(s => s.Services_Preferences_Types);
            return View(services_Preferences_HB.ToList());
        }

        // GET: Services_Preferences_HB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services_Preferences_HB services_Preferences_HB = db.Services_Preferences_HB.Find(id);
            if (services_Preferences_HB == null)
            {
                return HttpNotFound();
            }
            return View(services_Preferences_HB);
        }

        // GET: Services_Preferences_HB/Create
        public ActionResult Create()
        {
            ViewBag.typeId = new SelectList(db.Services_Preferences_Types, "id", "name");
            return View();
        }

        // POST: Services_Preferences_HB/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,typeId")] Services_Preferences_HB services_Preferences_HB)
        {
            if (ModelState.IsValid)
            {
                db.Services_Preferences_HB.Add(services_Preferences_HB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.typeId = new SelectList(db.Services_Preferences_Types, "id", "name", services_Preferences_HB.typeId);
            return View(services_Preferences_HB);
        }

        // GET: Services_Preferences_HB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services_Preferences_HB services_Preferences_HB = db.Services_Preferences_HB.Find(id);
            if (services_Preferences_HB == null)
            {
                return HttpNotFound();
            }
            ViewBag.typeId = new SelectList(db.Services_Preferences_Types, "id", "name", services_Preferences_HB.typeId);
            return View(services_Preferences_HB);
        }

        // POST: Services_Preferences_HB/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,typeId")] Services_Preferences_HB services_Preferences_HB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(services_Preferences_HB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.typeId = new SelectList(db.Services_Preferences_Types, "id", "name", services_Preferences_HB.typeId);
            return View(services_Preferences_HB);
        }

        // GET: Services_Preferences_HB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services_Preferences_HB services_Preferences_HB = db.Services_Preferences_HB.Find(id);
            if (services_Preferences_HB == null)
            {
                return HttpNotFound();
            }
            return View(services_Preferences_HB);
        }

        // POST: Services_Preferences_HB/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Services_Preferences_HB services_Preferences_HB = db.Services_Preferences_HB.Find(id);
            db.Services_Preferences_HB.Remove(services_Preferences_HB);
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
