using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCEmployeeScheduler.Contexts;
using MVCEmployeeScheduler.Models;

namespace MVCEmployeeScheduler.Controllers
{
    public class EmergencyContactController : Controller
    {
        private CRMDBContext db = new CRMDBContext();

        // GET: EmergencyContact
        public ActionResult Index(string id)
        {
            List<EmergencyContact> employees = db.EmergencyContact.Where(x => x.EmployeeID == id).ToList();

            return View(employees);

        }

        // GET: EmergencyContact/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } 
            EmergencyContact emergencyContact = db.EmergencyContact.Where(x => x.EmployeeID == id).FirstOrDefault();
            if (emergencyContact == null)
            {
                return HttpNotFound();
            }
            return View(emergencyContact);
        }

        // GET: EmergencyContact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmergencyContact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,Name,ContactName,ContactEmail,ContactPhone,ContactCity,ContactAddress")] EmergencyContact emergencyContact)
        {
            if (ModelState.IsValid)
            {
                db.EmergencyContact.Add(emergencyContact);
                db.SaveChanges();
                return RedirectToAction("Index/" + emergencyContact.EmployeeID);
            }

            return View(emergencyContact);
        }

        // GET: EmergencyContact/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmergencyContact emergencyContact = db.EmergencyContact.Where(x => x.EmployeeID == id).FirstOrDefault();

            if (emergencyContact == null)
            {
                return HttpNotFound();
            }
            return View(emergencyContact);
        }

        // POST: EmergencyContact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactName,ContactEmail,ContactPhone,ContactCity,ContactAddress")] EmergencyContact emergencyContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emergencyContact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/" + emergencyContact.EmployeeID);
            }

            return View(emergencyContact);
        }

        // GET: EmergencyContact/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmergencyContact emergencyContact = db.EmergencyContact.Where(x => x.EmployeeID == id).FirstOrDefault();
            if (emergencyContact == null)
            {
                return HttpNotFound();
            }
            return View(emergencyContact);
        }

        // POST: EmergencyContact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            EmergencyContact emergencyContact = db.EmergencyContact.Where(x => x.EmployeeID == id).FirstOrDefault();
            db.EmergencyContact.Remove(emergencyContact);
            db.SaveChanges();
            return RedirectToAction("Index/" + emergencyContact.EmployeeID);
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
