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
    public class Tarifs_DataController : Controller
    {
        private Entities db = new Entities();

        // GET: Tarifs_Data
        public ActionResult Index()
        {
            var tarifs_Data = db.Tarifs_Data.Include(t => t.Tarifs);
            return View(tarifs_Data.ToList());
        }

        // GET: Tarifs_Data/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarifs_Data tarifs_Data = db.Tarifs_Data.Find(id);
            if (tarifs_Data == null)
            {
                return HttpNotFound();
            }
            return View(tarifs_Data);
        }

        // GET: Tarifs_Data/Create
        public ActionResult Create()
        {
            ViewBag.tarifId = new SelectList(db.Tarifs, "id", "name");
            return View();
        }

        // POST: Tarifs_Data/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,value,count,tarifId")] Tarifs_Data tarifs_Data)
        {
            if (ModelState.IsValid)
            {
                db.Tarifs_Data.Add(tarifs_Data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tarifId = new SelectList(db.Tarifs, "id", "name", tarifs_Data.tarifId);
            return View(tarifs_Data);
        }

        // GET: Tarifs_Data/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarifs_Data tarifs_Data = db.Tarifs_Data.Find(id);
            if (tarifs_Data == null)
            {
                return HttpNotFound();
            }
            ViewBag.tarifId = new SelectList(db.Tarifs, "id", "name", tarifs_Data.tarifId);
            return View(tarifs_Data);
        }

        // POST: Tarifs_Data/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,value,count,tarifId")] Tarifs_Data tarifs_Data)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarifs_Data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tarifId = new SelectList(db.Tarifs, "id", "name", tarifs_Data.tarifId);
            return View(tarifs_Data);
        }

        // GET: Tarifs_Data/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarifs_Data tarifs_Data = db.Tarifs_Data.Find(id);
            if (tarifs_Data == null)
            {
                return HttpNotFound();
            }
            return View(tarifs_Data);
        }

        // POST: Tarifs_Data/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tarifs_Data tarifs_Data = db.Tarifs_Data.Find(id);
            db.Tarifs_Data.Remove(tarifs_Data);
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
