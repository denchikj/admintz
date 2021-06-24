using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BillingAdmin.Models;
using BillingAdmin.Models.Charts;
using Newtonsoft.Json;

namespace BillingAdmin.Controllers
{
    [CustomAuthorizeAttribute]
    public class Statistic_pbxController : Controller
    {
        private Entities db = new Entities();
        // GET: Statistic_pbx
        public async Task<ActionResult> Index()
        {
            List<Statistic_pbx> Statistics = await db.Statistic_pbx.OrderByDescending(c => c.id).Where(c => c.billsec > 0).AsNoTracking().ToListAsync();
            return View(Statistics);
        }
        //Statistic_pbx/Charts
        // GET: Statistic_pbx
        public ActionResult Charts()
        {
            ViewBag.ProjectName = "Все проекты";
            var q = (from x in db.Statistic_pbx
                     let dt = DbFunctions.TruncateTime(x.start)
                     group x by dt into g
                     select new
                     {
                         start = g.Count(),
                         billsec = g.Key
                     }).ToLookup(x => x.start, x => x.billsec)
                     .Select(x => x.Key);

            return View("Charts", q);
        }

        [Route("/getByPrefix/{prefix}")]
        public ActionResult getByPrefix(string prefix)
        {
            return View("Index", db.Statistic_pbx.Where(c => c.dst_num.StartsWith(prefix)).ToList());
        }

        [Route("/getByCode/{code}")]
        public ActionResult getByCode(string code)
        {
            return View("Index", db.Statistic_pbx.Where(c => c.did.Equals(code)).ToList());
        }

        [Route("/getByProject/{proj}")]
        public ActionResult getByProject(string proj)
        {
            ViewBag.ProjectName = db.Projects.Where(c => c.oktellCode == proj).FirstOrDefault().name;


            if (proj.Contains('-'))
            {
                string code = proj.Split('-')[0];
                string prefix = proj.Split('-')[1];

                var st = db.Statistic_pbx.Where(c => c.did.Equals(code) || c.dst_num.StartsWith(prefix)).OrderByDescending(c => c.id).Where(c => c.billsec > 0);
                //(st as ObjectQuery<Statistic_pbx>).MergeOption =  MergeOption.OverwriteChanges;
                //db.Entry(st).State = EntityState.Detached;
                //st = db.Statistic_pbx.Where(c => c.did.Equals(code) || c.dst_num.StartsWith(prefix));
                return View("Index", st.AsNoTracking().ToList());
            }
            else
            {
                var st = db.Statistic_pbx.Where(c => c.did.Equals(proj));
                //(st as ObjectQuery<Statistic_pbx>).MergeOption = MergeOption.OverwriteChanges;
                //db.Entry(st).State = EntityState.Detached;
                st = db.Statistic_pbx.Where(c => c.did.Equals(proj));

                return View("Index", st.AsNoTracking().ToList());
            }
        }

        [Route("/getChartByProject/{proj}")]
        public ActionResult getChartByProject(string proj)
        {
            ViewBag.ProjectName = db.Projects.Where(c => c.oktellCode == proj).FirstOrDefault().name;
            if (proj.Contains('-'))
            {
                string code = proj.Split('-')[0];
                string prefix = proj.Split('-')[1];

                var st = db.Statistic_pbx.Where(c => c.did.Equals(code) || c.dst_num.StartsWith(prefix));
                //(st as ObjectQuery<Statistic_pbx>).MergeOption =  MergeOption.OverwriteChanges;
                //db.Entry(st).State = EntityState.Detached;
                //st = db.Statistic_pbx.Where(c => c.did.Equals(code) || c.dst_num.StartsWith(prefix));
                // var l = st.AsNoTracking().GroupBy(m=>m.start).Sum(c => c.billsec);
                var q = (from x in st.AsNoTracking()
                         let dt = DbFunctions.TruncateTime(x.start)
                         group x by dt into g
                         select new
                         {
                             start = g.Count(),
                             billsec = g.Key
                         }).ToLookup(x => x.start, x => x.billsec)
                         .Select(x => x.Key);

                return View("Charts", q);
            }
            else
            {
                var st = db.Statistic_pbx.Where(c => c.did.Equals(proj)).Select(x => x.start);
                //(st as ObjectQuery<Statistic_pbx>).MergeOption = MergeOption.OverwriteChanges;
                //db.Entry(st).State = EntityState.Detached;
                //st = db.Statistic_pbx.Where(c => c.did.Equals(proj));

                return View("Index", st.AsNoTracking());
            }
        }

        // GET: Statistic_pbx/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic_pbx statistic_pbx = db.Statistic_pbx.Find(id);
            if (statistic_pbx == null)
            {
                return HttpNotFound();
            }
            return View(statistic_pbx);
        }

        // GET: Statistic_pbx/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Statistic_pbx/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,UNIQUEID,start,answer,endtime,src_chan,src_num,dst_chan,dst_num,linkedid,did,disposition,recordingfile,from_account,to_account,dialstatus,appname,transfer,is_app,duration,billsec,work_completed")] Statistic_pbx statistic_pbx)
        {
            if (ModelState.IsValid)
            {
                db.Statistic_pbx.Add(statistic_pbx);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statistic_pbx);
        }

        // GET: Statistic_pbx/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic_pbx statistic_pbx = db.Statistic_pbx.Find(id);
            if (statistic_pbx == null)
            {
                return HttpNotFound();
            }
            return View(statistic_pbx);
        }

        // POST: Statistic_pbx/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,UNIQUEID,start,answer,endtime,src_chan,src_num,dst_chan,dst_num,linkedid,did,disposition,recordingfile,from_account,to_account,dialstatus,appname,transfer,is_app,duration,billsec,work_completed")] Statistic_pbx statistic_pbx)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statistic_pbx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statistic_pbx);
        }

        // GET: Statistic_pbx/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic_pbx statistic_pbx = db.Statistic_pbx.Find(id);
            if (statistic_pbx == null)
            {
                return HttpNotFound();
            }
            return View(statistic_pbx);
        }

        // POST: Statistic_pbx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Statistic_pbx statistic_pbx = db.Statistic_pbx.Find(id);
            db.Statistic_pbx.Remove(statistic_pbx);
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

        [HttpPost]
        public string GetStatistics(int? draw, int? start, int? length)
        {
            var search = Request["search[value]"];
            var totalRecords = 0;
            var recordsFiltered = 0;
            start = start.HasValue ? start / length : 0;
            var catalog = new Statistic_pbxRepository().GetPaginated(ControllerContext, search, start ?? 0, length ?? 25, out totalRecords, out recordsFiltered);
            string json = JsonConvert.SerializeObject(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = totalRecords, data = catalog });
            return json;
        }
    }
}
