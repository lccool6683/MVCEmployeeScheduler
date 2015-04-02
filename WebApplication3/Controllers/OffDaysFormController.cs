using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCEmployeeScheduler.Models;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;

namespace MVCEmployeeScheduler.Controllers
{
    public class OffDaysFormController : Controller
    {
        private CRMDBContext db = new CRMDBContext();

        // GET: OffDays
        public ActionResult Index()
        {
            //Database.SetInitializer<OffDaysDBContext>(null);
            return View(db.OffDaysSchedule.ToList());
        }
        [HttpPost]
        public String SubmitOffDays(OffDay s)
        {
            if (!string.IsNullOrEmpty(s.DateFrom) && !string.IsNullOrEmpty(s.DateTo) && !string.IsNullOrEmpty(s.TimeFrom) && !string.IsNullOrEmpty(s.TimeTo) && !string.IsNullOrEmpty(s.Description))
            {
                string datetimefrom = s.DateFrom + " " + s.TimeFrom;
                string datetimeto = s.DateTo + " " + s.TimeTo;
                string f = datetimefrom.ToString();
                string t = datetimeto.ToString();
                DateTime dateValueFrom;
                DateTime dateValueTo;

                string format = "MM/dd/yyyy hh:mm tt";

                DateTime.TryParseExact(f, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValueFrom);


                DateTime.TryParseExact(t, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValueTo);
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMDBContext"].ToString());
                string query = "INSERT INTO [OffDays]([EmpId],[DateFrom], [DateTo], [TimeFrom], [TimeTo], [DateTimeFrom], [DateTimeTo], [Description]) VALUES('" + s.DateFrom + "', '" + s.DateTo
                + "', '" + s.TimeFrom + "', '" + s.TimeTo + "', '" + s.EmpId + "', '" + dateValueFrom.ToString() + "', '" + dateValueTo.ToString() + "', '" + s.Description + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                conn.Close();
                return "Days off Saved";
            }
            else
            {
                return "Please complete the form";
            }
        }

        // GET: OffDays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OffDay DBoffDays = db.OffDaysSchedule.SingleOrDefault(ev => ev.id == id);
            if (DBoffDays == null)
            {
                return HttpNotFound();
            }
            return View(DBoffDays);
        }

        // GET: OffDays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OffDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,DateTimeFrom,DateTimeTo,EmpId")] OffDay offDays)
        {
            if (ModelState.IsValid)
            {
                db.OffDaysSchedule.Add(offDays);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offDays);
        }

        // GET: OffDays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OffDay DBoffDays = db.OffDaysSchedule.Find(id);
            if (DBoffDays == null)
            {
                return HttpNotFound();
            }
            return View(DBoffDays);
        }

        // POST: OffDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,DateTimeFrom,DateTimeTo,EmpId,Description")] OffDay offDays)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offDays).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offDays);
        }

        // GET: OffDays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OffDay DBoffDays = db.OffDaysSchedule.Find(id);
            if (DBoffDays == null)
            {
                return HttpNotFound();
            }
            return View(DBoffDays);
        }

        // POST: OffDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OffDay DBoffDays = db.OffDaysSchedule.Find(id);
            db.OffDaysSchedule.Remove(DBoffDays);
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
