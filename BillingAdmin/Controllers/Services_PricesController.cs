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
    public class Services_PricesController : Controller
    {
        private Entities db = new Entities();

        // GET: Services_Prices
        public ActionResult Index()
        {
            var services_Prices = db.Services_Prices.Include(s => s.Price_TypesHB).Include(s => s.Services).Include(s => s.Tarifs).Include(s => s.Services_Preferences_HB);
            return View(services_Prices.ToList());
        }

        // GET: Services_Prices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services_Prices services_Prices = db.Services_Prices.Find(id);
            if (services_Prices == null)
            {
                return HttpNotFound();
            }
            return View(services_Prices);
        }

        // GET: Services_Prices/Create
        public ActionResult Create()
        {
            ViewBag.priceTypesId = new SelectList(db.Price_TypesHB, "id", "name");
            ViewBag.serviceId = new SelectList(db.Services, "id", "name");
            ViewBag.tarifId = new SelectList(db.Tarifs, "id", "name");
            ViewBag.preferenceId = new SelectList(db.Services_Preferences_HB, "id", "name");
            return View();
        }

        // POST: Services_Prices/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,serviceId,priceTypesId,tarifId,value,date_create,date_start,date_stop,isActive,preferenceId")] Services_Prices services_Prices)
        {
            if (ModelState.IsValid)
            {
                db.Services_Prices.Add(services_Prices);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.priceTypesId = new SelectList(db.Price_TypesHB, "id", "name", services_Prices.priceTypesId);
            ViewBag.serviceId = new SelectList(db.Services, "id", "name", services_Prices.serviceId);
            ViewBag.tarifId = new SelectList(db.Tarifs, "id", "name", services_Prices.tarifId);
            ViewBag.preferenceId = new SelectList(db.Services_Preferences_HB, "id", "name", services_Prices.preferenceId);
            return View(services_Prices);
        }

        // GET: Services_Prices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services_Prices services_Prices = db.Services_Prices.Find(id);
            if (services_Prices == null)
            {
                return HttpNotFound();
            }
            ViewBag.priceTypesId = new SelectList(db.Price_TypesHB, "id", "name", services_Prices.priceTypesId);
            ViewBag.serviceId = new SelectList(db.Services, "id", "name", services_Prices.serviceId);
            ViewBag.tarifId = new SelectList(db.Tarifs, "id", "name", services_Prices.tarifId);
            ViewBag.preferenceId = new SelectList(db.Services_Preferences_HB, "id", "name", services_Prices.preferenceId);
            return View(services_Prices);
        }

        // POST: Services_Prices/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,serviceId,priceTypesId,tarifId,value,date_create,date_start,date_stop,isActive,preferenceId")] Services_Prices services_Prices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(services_Prices).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.priceTypesId = new SelectList(db.Price_TypesHB, "id", "name", services_Prices.priceTypesId);
            ViewBag.serviceId = new SelectList(db.Services, "id", "name", services_Prices.serviceId);
            ViewBag.tarifId = new SelectList(db.Tarifs, "id", "name", services_Prices.tarifId);
            ViewBag.preferenceId = new SelectList(db.Services_Preferences_HB, "id", "name", services_Prices.preferenceId);
            return View(services_Prices);
        }

        // GET: Services_Prices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services_Prices services_Prices = db.Services_Prices.Find(id);
            if (services_Prices == null)
            {
                return HttpNotFound();
            }
            return View(services_Prices);
        }

        // POST: Services_Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Services_Prices services_Prices = db.Services_Prices.Find(id);
            db.Services_Prices.Remove(services_Prices);
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
