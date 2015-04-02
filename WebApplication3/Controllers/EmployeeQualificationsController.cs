using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEmployeeScheduler.Contexts;
using MVCEmployeeScheduler.Models;

namespace MVCEmployeeScheduler.Controllers
{
    public class EmployeeQualificationsController : Controller
    {
        private CRMDBContext db = new CRMDBContext();
        // GET: EmployeeQualifications
        public ActionResult Index(string id)
        {
            List<EmployeeQualifications> employees = db.EmployeeQualifications.Where(x => x.EmployeeID == id).ToList();
            return View(employees);
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