using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MVCEmployeeScheduler.Contexts;
using MVCEmployeeScheduler.Models;

namespace MVCEmployeeScheduler.Controllers
{
    public class EmployeesController : Controller
    {
        private CRMDBContext db = new CRMDBContext();

        EmployeeQualifications employeeQualifications = new EmployeeQualifications();

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            List<SelectListItem> position = new List<SelectListItem>();
            List<SelectListItem> branchName = new List<SelectListItem>();
            List<SelectListItem> Qualifications = new List<SelectListItem>();

            position.Add(new SelectListItem { Text = "Admin", Value = "Admin" });
            position.Add(new SelectListItem { Text = "Manager", Value = "Manager" });
            position.Add(new SelectListItem { Text = "Staff", Value = "Staff" });

            ViewData["Position"] = position;

            //Add branch names to branchName list
            branchName.Add(new SelectListItem { Text = "Cloverfield", Value = "Cloverfield" });
            branchName.Add(new SelectListItem { Text = "Serentia", Value = "Serentia" });
            branchName.Add(new SelectListItem { Text = "West Wind", Value = "West Wind" });
            branchName.Add(new SelectListItem { Text = "Highland Way", Value = "Highland Way" });
            branchName.Add(new SelectListItem { Text = "Northern Emissaries", Value = "Northern Emissaries" });
            branchName.Add(new SelectListItem { Text = "Sunny Meadows", Value = "Sunny Meadows" });

            ViewData["BranchName"] = branchName;

            //Adding qualifications to the list
            Qualifications.Add(new SelectListItem { Value = "None", Text = "None" });
            Qualifications.Add(new SelectListItem { Value = "High School", Text = "High School" });
            Qualifications.Add(new SelectListItem { Value = "Certificate", Text = "Certificate" });
            Qualifications.Add(new SelectListItem { Value = "Diploma", Text = "Diploma" });
            Qualifications.Add(new SelectListItem { Value = "Degree", Text = "Degree" });
            Qualifications.Add(new SelectListItem { Value = "Masters", Text = "Masters" });
            Qualifications.Add(new SelectListItem { Value = "PhD", Text = "PhD" });

            ViewBag.Qualifications = new MultiSelectList(Qualifications, "Text", "Value");

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ContactID,EmployeeID,QualificationsID,Name,Password,Position,StaffType,EmailAddress,BranchName,ContactInformation,EmergencyContact")] Employee employee,
                                                   EmployeeContacts employeeContact, EmergencyContact emergencyContact, EmployeeQualifications qualify, Schedules schedule, string[] Qualifications, string ID, string Name)
        {
            if (ModelState.IsValid)
            {
                for(int i = 0; i < Qualifications.Length; i++)
                {
                    string results = Qualifications[i];
                    qualify.Qualifications = results;
                    qualify.EmployeeID = ID;
                    db.EmployeeQualifications.Add(qualify);
                    db.SaveChanges();
                }

                employeeContact.EmployeeID = ID;
                emergencyContact.EmployeeID = ID;
                //schedule.EmpId = Convert.ToInt32(ID); 

                db.SaveChanges();

                db.Employees.Add(employee);
                db.EmployeeContacts.Add(employeeContact);
                db.EmergencyContact.Add(emergencyContact);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(id);

            List<SelectListItem> position = new List<SelectListItem>();
            List<SelectListItem> branchName = new List<SelectListItem>();
            List<SelectListItem> Qualifications = new List<SelectListItem>();

            position.Add(new SelectListItem { Text = "Admin", Value = "Admin" });
            position.Add(new SelectListItem { Text = "Manager", Value = "Manager" });
            position.Add(new SelectListItem { Text = "Staff", Value = "Staff" });

            ViewData["Position"] = position;

            //Add branch names to branchName list
            branchName.Add(new SelectListItem { Text = "Cloverfield", Value = "Cloverfield" });
            branchName.Add(new SelectListItem { Text = "Serentia", Value = "Serentia" });
            branchName.Add(new SelectListItem { Text = "West Wind", Value = "West Wind" });
            branchName.Add(new SelectListItem { Text = "Highland Way", Value = "Highland Way" });
            branchName.Add(new SelectListItem { Text = "Northern Emissaries", Value = "Northern Emissaries" });
            branchName.Add(new SelectListItem { Text = "Sunny Meadows", Value = "Sunny Meadows" });

            ViewData["BranchName"] = branchName;

            //Adding qualifications to the list
            Qualifications.Add(new SelectListItem { Value = "None", Text = "None" });
            Qualifications.Add(new SelectListItem { Value = "High School", Text = "High School" });
            Qualifications.Add(new SelectListItem { Value = "Certificate", Text = "Certificate" });
            Qualifications.Add(new SelectListItem { Value = "Diploma", Text = "Diploma" });
            Qualifications.Add(new SelectListItem { Value = "Degree", Text = "Degree" });
            Qualifications.Add(new SelectListItem { Value = "Masters", Text = "Masters" });
            Qualifications.Add(new SelectListItem { Value = "PhD", Text = "PhD" });

            ViewBag.Qualifications = new MultiSelectList(Qualifications, "Text", "Value");

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ContactID,EmployeeID,QualificationsID,Name,Password,Position,StaffType,EmailAddress,BranchName,Qualifications")] Employee employee,
                                                   EmployeeContacts employeeContact, EmergencyContact emergencyContact, EmployeeQualifications qualify, string[] Qualifications, string id)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < Qualifications.Length; i++)
                {
                    string results = Qualifications[i];
                    qualify.Qualifications = results;
                    qualify.EmployeeID = id;
                    db.EmployeeQualifications.Add(qualify);
                    db.SaveChanges();
                }

                employeeContact.EmployeeID = id;
                emergencyContact.EmployeeID = id;

                db.Entry(employee).State = EntityState.Modified;
                db.Entry(employeeContact).State = EntityState.Modified;
                db.Entry(emergencyContact).State = EntityState.Modified;
                db.Entry(qualify).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //Safely ignore this exception
                }
                catch (Exception e)
                {
                    //Something else has occurred
                }

                return RedirectToAction("Index");
            }
            //return View(employee);
            return View(employee);
        
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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

        private IEnumerable<SelectListItem> GetAllQualifications()
        {
            List<SelectListItem> allQualifications = new List<SelectListItem>();
            //Add a few cars to make a list of cars
            allQualifications.Add(new SelectListItem { Value = "1", Text = "None" });
            allQualifications.Add(new SelectListItem { Value = "2", Text = "Little Bit" });
            allQualifications.Add(new SelectListItem { Value = "3", Text = "Slightly More" });
            allQualifications.Add(new SelectListItem { Value = "4", Text = "Some" });
            allQualifications.Add(new SelectListItem { Value = "5", Text = "Some more" });
            allQualifications.Add(new SelectListItem { Value = "6", Text = "Quite a bit" });
            allQualifications.Add(new SelectListItem { Value = "7", Text = "Lots" });
            allQualifications.Add(new SelectListItem { Value = "8", Text = "All" });

            return allQualifications.AsEnumerable();
        }

        /*
        public ActionResult BackupDatabaseTrigger()
        {

            //var database = new MVCEmployeeScheduler.Models.EmployeeDBContext();

            var dbPath = Server.MapPath("~/App_Data/DatabaseBackup.bak");
            using (var database = new MVCEmployeeScheduler.Models.EmployeeDBContext())
            {
                var cmd = String.Format("CREATE TRIGGER trg_Employee_Insert " +
                                        "ON dbo.database " +
                                        "AFTER INSERT " +
                                        "AS " +
                                        "INSERT INTO dbo.databaseBackup(EmployeeID, Name) " +
                                        "SELECT " +
                                        "EmployeeID, Name " +
                                        "FROM " +
                                        "Employees "
                                       );


                return RedirectToAction("Index");
                //return new FilePathResult(dbPath, "application/octet-stream");

            }
        }

        public ActionResult BackupDatabaseProcedure()
        {

            //var database = new MVCEmployeeScheduler.Models.EmployeeDBContext();
            var dbPath = Server.MapPath("~/App_Data/DatabaseBackup.bak");
            using (var database = new MVCEmployeeScheduler.Models.EmployeeDBContext())
            {
                SqlCommand sql = new SqlCommand("BACKUP DATABASE @database TO DISK = @dbPath ;WITH FORMAT, MEDIANAME = 'Backup', NAME = 'Full Backup'");
                sql.Parameters.AddWithValue("@database", database);
                sql.Parameters.AddWithValue("@dbPath", dbPath);
            }

            //return RedirectToAction("Index");
            return new FilePathResult(dbPath, "application/octet-stream");

        }
        */
        /*
        public ActionResult BackupDatabase()
        {
            var dbPath = Server.MapPath("~/App_Data/DatabaseBackup.bak");
            using (var database = new EmployeesContext())
            {
                var cmd = String.Format("BACKUP DATABASE {0} TO DISK='{1}' ;WITH FORMAT, MEDIANAME='DbBackups', MEDIADESCRIPTION='Media set for {0} database';", database, dbPath);
                database.Database.ExecuteSqlCommand(cmd);
            }

            //return RedirectToAction("Index");
            return new FilePathResult(dbPath, "application/octet-stream");
        }
         */
        /*
        //
        // GET: RestoreDatabase
        //[Authorize(Roles = "Admin")]
        public ActionResult RestoreDatabase()
        {
            //return View(new RestoreDatabase());
            return RedirectToAction("Index");
        }

        //
        // POST: RestoreDatabase
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RestoreDatabase(RestoreDatabase model)
        {
            const string appName = "Employee";
            var dbPath = Server.MapPath(String.Format("~/App_Data/Restore_{0}_DB_{1:yyyy-MM-dd-HH-mm-ss}.bak", appName, DateTime.UtcNow));

            try
            {
                model.File.SaveAs(dbPath);

                using (var database = new EmployeeDBContext())
                {
                    var cmd = String.Format(@"
                                              USE [Master]; 
                                              ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                                              RESTORE DATABASE {0} FROM DISK='{1}' WITH REPLACE;
                                              ALTER DATABASE {0} SET MULTI_USER;"
                                              , appName, dbPath);

                    db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd);
                }

                ModelState.AddModelError("", "Restored!");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }

            return RedirectToAction("Index");
            //return View(model);
        }
        */
    }
}
        