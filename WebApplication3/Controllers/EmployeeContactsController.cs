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
    public class EmployeeContactsController : Controller
    {
        private CRMDBContext db = new CRMDBContext();

        public ActionResult Index(string id)
        {
            List<EmployeeContacts> employees = db.EmployeeContacts.Where(x => x.EmployeeID == id).ToList();

            return View(employees);
        }

        // GET: EmployeeContacts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeContacts employeeContacts = db.EmployeeContacts.Find(id);
            if (employeeContacts == null)
            {
                return HttpNotFound();
            }
            return View(employeeContacts);
        }

        // GET: EmployeeContacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,Name,PhoneNumber,City,PostalCode,StreetAddress")] EmployeeContacts employeeContacts)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeContacts.Add(employeeContacts);
                db.SaveChanges();
                return RedirectToAction("Index/" + employeeContacts.EmployeeID);
            }

            return View(employeeContacts);
        }

        // GET: EmployeeContacts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeContacts employeeContacts = db.EmployeeContacts.Find(id);
            if (employeeContacts == null)
            {
                return HttpNotFound();
            }
            return View(employeeContacts);
        }

        // POST: EmployeeContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,Name,PhoneNumber,City,PostalCode,StreetAddress")] EmployeeContacts employeeContacts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeContacts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/" + employeeContacts.EmployeeID);
            }
            return View(employeeContacts);
        }

        // GET: EmployeeContacts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeContacts employeeContacts = db.EmployeeContacts.Find(id);
            if (employeeContacts == null)
            {
                return HttpNotFound();
            }
            return View(employeeContacts);
        }

        // POST: EmployeeContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            EmployeeContacts employeeContacts = db.EmployeeContacts.Find(id);
            db.EmployeeContacts.Remove(employeeContacts);
            db.SaveChanges();
            return RedirectToAction("Index" + employeeContacts.EmployeeID);
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
