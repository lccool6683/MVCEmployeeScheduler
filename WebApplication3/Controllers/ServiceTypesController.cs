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
    public class ServiceTypesController : Controller
    {
        private CRMDBContext db = new CRMDBContext();

        // GET: ServiceTypes
        public ActionResult Index()
        {
            return View(db.ServiceTypes.ToList());
        }

        // GET: ServiceTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = db.ServiceTypes.Find(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            return View(serviceType);
        }

        // GET: ServiceTypes/Create
        public ActionResult Create()
        {
            BranchNameDropDownList();
            CurrencyDropDownList();
            return View();
        }

        // POST: ServiceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BranchName,Category,SubCategory,Description,MinutesPerUnit,CostPerUnit,Currency")] ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                db.ServiceTypes.Add(serviceType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceType);
        }

        // GET: ServiceTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = db.ServiceTypes.Find(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            return View(serviceType);
        }

        // POST: ServiceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BranchName,Category,SubCategory,Description,MinutesPerUnit,CostPerUnit,Currency")] ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceType);
        }

        // GET: ServiceTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = db.ServiceTypes.Find(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            return View(serviceType);
        }

        // POST: ServiceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceType serviceType = db.ServiceTypes.Find(id);
            db.ServiceTypes.Remove(serviceType);
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

        private void BranchNameDropDownList()
        {
            var NameList = new List<string>();
            var NameQry = from d in db.Branches
                          orderby d.Name
                          select d.Name;


            List<string> parentbranches = NameQry.ToList();
            var ServiceTypeList = new List<SelectListItem>();
            foreach (string branch in parentbranches)
            {
                ServiceTypeList.Add(new SelectListItem { Text = branch.ToString(), Value = branch.ToString() });
            }
            ViewBag.ParentBranch = ServiceTypeList;
        }

        private void CurrencyDropDownList()
        {
    var ServiceTypeList = new List<SelectListItem>
{
	new SelectListItem{Text = "Candian Dollar", Value = "Candian Dollar"},
	new SelectListItem{Text = "Amercian Dollar", Value = "American Dollar"},
	new SelectListItem{Text = "Rupees", Value = "Rupees"}
};
	ViewBag.Service = ServiceTypeList;
        }
    }
}
