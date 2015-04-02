using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCEmployeeScheduler.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Transactions;


namespace MVCEmployeeScheduler.Controllers
{
    public class ScheduleController : Controller
    {
        private CRMDBContext db = new CRMDBContext();

        // GET: Schedule
        public ActionResult Index()
        {
            return View(db.ScheduleModels.ToList());  
            //List<ScheduleModel> schedule = db.Schedules.Where(x => x.EmpId == id).ToList();

            //return View(schedule);
            //return View(db.Schedules.ToList());
        }
        [HttpPost]
        public String SubmitSchedule(Schedules s)
        {
                if (!string.IsNullOrEmpty(s.DateFrom) && !string.IsNullOrEmpty(s.DateTo) && !string.IsNullOrEmpty(s.TimeFrom) && !string.IsNullOrEmpty(s.TimeTo))
                {
                    string datetimefrom = s.DateFrom + " " + s.TimeFrom;
                    string datetimeto = s.DateTo + " " + s.TimeTo;
                    string f = datetimefrom.ToString();
                    string t = datetimeto.ToString();
                    DateTime dateValueFrom;
                    DateTime dateValueTo;
                    string format = "MM/dd/yyyy h:mm tt";

                    DateTime.TryParseExact(f, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValueFrom);


                    DateTime.TryParseExact(t, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValueTo);

                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMDBContext"].ToString());
                    string query = "INSERT INTO [Schedules]([DateFrom], [DateTo], [TimeFrom], [TimeTo], [EmpId], [DateTimeFrom], [DateTimeTo]) VALUES('" + s.DateFrom + "', '" + s.DateTo
                    + "', '" + s.TimeFrom + "', '" + s.TimeTo + "', '" + s.EmpId + "', '" + dateValueFrom.ToString() + "', '" + dateValueTo.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    conn.Close();
                    return "Schedule Saved";
                }
                else
                {
                    return "Please complete the form";
                }
            
        }

        // GET: Schedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedules scheduleModel = db.ScheduleModels.Where(x => x.EmpId == id).FirstOrDefault();
            if (scheduleModel == null)
            {
                return HttpNotFound();
            }
            return View(scheduleModel);
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpId,ID,UserId,DateFrom,DateTo,TimeFrom,TimeTo")] Schedules scheduleModel, OffDay offday)
        {
            if (ModelState.IsValid)
            {
                db.ScheduleModels.Add(scheduleModel);
                db.OffDaysSchedule.Add(offday);
                db.SaveChanges();
                return RedirectToAction("Index/" + scheduleModel.EmpId);
            }

            return View(scheduleModel);
        }

        // GET: Schedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedules scheduleModel = db.ScheduleModels.Where(x => x.EmpId == id).FirstOrDefault();
            if (scheduleModel == null)
            {
                return HttpNotFound();
            }
            return View(scheduleModel);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmpId,DateTimeFrom,DateTimeTo")] Schedules scheduleModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduleModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/" + scheduleModel.EmpId);
            }
            return View(scheduleModel);
        }

        // GET: Schedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedules scheduleModel = db.ScheduleModels.Where(x => x.EmpId == id).FirstOrDefault();
            if (scheduleModel == null)
            {
                return HttpNotFound();
            }
            return View(scheduleModel);
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedules scheduleModel = db.ScheduleModels.Where(x => x.EmpId == id).FirstOrDefault();
            db.ScheduleModels.Remove(scheduleModel);
            db.SaveChanges();
            return RedirectToAction("Index/" + scheduleModel.id);
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
