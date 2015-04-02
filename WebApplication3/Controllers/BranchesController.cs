using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MVCEmployeeScheduler.Models;

namespace MVCEmployeeScheduler.Controllers
{
    public class BranchesController : Controller
    {
        private CRMDBContext db = new CRMDBContext();

        // GET: Branches
        public ActionResult Index(string ParentBranch)
        {
            //var NameList = new List<string>();
            //var NameQry = from d in db.Branchs
            //              orderby d.ParentBranch
            //              select d.ParentBranch;

            //ViewBag.ParentBranch = new SelectList(NameList);
            CreateDropDownList();
            var tasks = from t in db.Branches
                        select t;

            //NameList.AddRange(NameQry.Distinct());

            if (!string.IsNullOrEmpty(ParentBranch))
            {
                tasks = tasks.Where(x => x.ParentBranch == ParentBranch);
            }
            //List<Branches> brancheslist = new List<Branches>();

            return View(tasks);
        }

        // GET: Branches/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branches branch = db.Branches.Where(x => x.Name == id).FirstOrDefault();
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        private void CreateDropDownList(){
            //var NameList = new List<string>();
            var NameQry = from d in db.Branches
                          orderby d.Name
                          select d.Name;


            List<string> parentbranches = NameQry.ToList();
            var ServiceTypeList = new List<SelectListItem>();
            foreach (Branches branch in db.Branches.ToList())
            {
               ServiceTypeList.Add(new SelectListItem { Text = branch.Name, Value = branch.Name });
            }
            ViewBag.ParentBranch = ServiceTypeList;
        }

        public ActionResult CreateService(string searchString)
        {
            var Branches = from m in db.Branches
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Branches = Branches.Where(s => s.Name.Contains(searchString));
            }

            return View(Branches);
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public void Upload()
        //{
        //    foreach(string file in Request.Files)
        //    {
        //        var postedFile = Request.Files[file];
        //        postedFile.SaveAs(Server.MapPath("../Pictures") + Path.GetFileName(postedFile.FileName));
        //    }
        //}

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("../Pictures"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Index");
        }

        // GET: Branches/Create
        public ActionResult Create()
        {
            CreateDropDownList();
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,ParentBranch,Address")] Branches branch)
        {
            //if (file != null)
            //{
            //    string pic = System.IO.Path.GetFileName(file.FileName);
            //    string path = System.IO.Path.Combine(
            //                           Server.MapPath("../Pictures"), pic);
            //    // file is uploaded
            //    file.SaveAs(path);

            //    // save the image path path to the database or you can send image 
            //    // directly to database
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        file.InputStream.CopyTo(ms);
            //        byte[] array = ms.GetBuffer();
            //    }

            //}
            if (ModelState.IsValid)
            {
                try
                {
                    db.Branches.Add(branch);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    //ViewBag.errorMessage = "Name Already Exits in database";
                    //CreateDropDownList();
                    return View(branch);
                }
                return RedirectToAction("Index");
            }
           
 
          return View(branch);
        }

        // GET: Branches/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branches branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,ParentBranch,Address")] Branches branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        // GET: Branches/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branches branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Branches branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
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
