using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCEmployeeScheduler.Models;

namespace MVCEmployeeScheduler.Controllers
{
    public class ServiceController : Controller
    {
        private CRMDBContext db = new CRMDBContext();

        // GET: /Service/
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        // GET: /Service/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: /Service/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Service/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,ServiceTypeID,ClientID,StartDate,EndDate,RepeatOption,RepeatUntil,ShiftID")] Service service)
        {
            if (ModelState.IsValid)
            {
                service.ID = Guid.NewGuid();
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }

        // GET: /Service/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: /Service/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,ServiceTypeID,ClientID")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: /Service/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: /Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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

        public ActionResult CreateInvoice()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateInvoice(string guid, DateTime startDate, DateTime endDate)
        {
            var model = new Service();
            model.ClientID = new Guid(guid);

            //var thisClient = from m in db.Services
            //                 where (m.ClientID == model.ClientID) && (m.DateReq >= startDate) && (m.DateReq <= endDate)
            //                 select m;

            return View("../Client/Invoice");
        }


    }
}
