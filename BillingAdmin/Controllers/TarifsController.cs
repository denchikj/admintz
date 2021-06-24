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
    public class TarifsController : Controller
    {
        private Entities db = new Entities();

        // GET: Tarifs
        public ActionResult Index()
        {
            return View(db.Tarifs.ToList());
        }

        // GET: Tarifs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarifs tarifs = db.Tarifs.Find(id);
            if (tarifs == null)
            {
                return HttpNotFound();
            }
            return View(tarifs);
        }

        // GET: Tarifs/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Tarifs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,date_create")] Tarifs tarifs)
        {
            if (ModelState.IsValid)
            {
                db.Tarifs.Add(tarifs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tarifs);
        }

        // GET: Tarifs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarifs tarifs = db.Tarifs.Find(id);
            if (tarifs == null)
            {
                return HttpNotFound();
            }
            return View(tarifs);
        }

        // POST: Tarifs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,date_create")] Tarifs tarifs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarifs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tarifs);
        }

        // GET: Tarifs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarifs tarifs = db.Tarifs.Find(id);
            if (tarifs == null)
            {
                return HttpNotFound();
            }
            return View(tarifs);
        }

        // POST: Tarifs/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tarifs tarifs = db.Tarifs.Find(id);
            db.Tarifs.Remove(tarifs);
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
