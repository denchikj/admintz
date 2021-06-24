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
    public class Services_Preferences_TypesController : Controller
    {
        private Entities db = new Entities();

        // GET: Services_Preferences_Types
        public ActionResult Index()
        {
            return View(db.Services_Preferences_Types.ToList());
        }

        // GET: Services_Preferences_Types/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services_Preferences_Types services_Preferences_Types = db.Services_Preferences_Types.Find(id);
            if (services_Preferences_Types == null)
            {
                return HttpNotFound();
            }
            return View(services_Preferences_Types);
        }

        // GET: Services_Preferences_Types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services_Preferences_Types/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Services_Preferences_Types services_Preferences_Types)
        {
            if (ModelState.IsValid)
            {
                db.Services_Preferences_Types.Add(services_Preferences_Types);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(services_Preferences_Types);
        }

        // GET: Services_Preferences_Types/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services_Preferences_Types services_Preferences_Types = db.Services_Preferences_Types.Find(id);
            if (services_Preferences_Types == null)
            {
                return HttpNotFound();
            }
            return View(services_Preferences_Types);
        }

        // POST: Services_Preferences_Types/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Services_Preferences_Types services_Preferences_Types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(services_Preferences_Types).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(services_Preferences_Types);
        }

        // GET: Services_Preferences_Types/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services_Preferences_Types services_Preferences_Types = db.Services_Preferences_Types.Find(id);
            if (services_Preferences_Types == null)
            {
                return HttpNotFound();
            }
            return View(services_Preferences_Types);
        }

        // POST: Services_Preferences_Types/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Services_Preferences_Types services_Preferences_Types = db.Services_Preferences_Types.Find(id);
            db.Services_Preferences_Types.Remove(services_Preferences_Types);
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
