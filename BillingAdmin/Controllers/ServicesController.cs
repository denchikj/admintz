﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillingAdmin.Models;
using System.Text.RegularExpressions;

namespace BillingAdmin.Controllers
{
    public class ServicesController : Controller
    {
        private Entities db = new Entities();

        // GET: Services
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,description")] Services services)
        {
            if (ModelState.IsValid)
            {
                services.description = HttpUtility.UrlDecode(services.description);

                db.Services.Add(services);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(services);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        // POST: Services/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description")] Services services)
        {
            if (ModelState.IsValid)
            {
                services.description = HttpUtility.UrlDecode(services.description);
                db.Entry(services).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Services");
            }
            return View(services);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Services services = db.Services.Find(id);
            db.Services.Remove(services);
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
