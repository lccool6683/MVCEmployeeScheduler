using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCEmployeeScheduler.Controllers;

namespace MVCEmployeeScheduler.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(loginModel model)
        {
            if (ModelState.IsValid)
            {
                if (DataAccess.DAL.UserIsValid(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                       if (DataAccess.DAL.Position("manager", model.Username))
                       {
                           return RedirectToAction("index", "Calendar");
                       }
                       else { 
                         return RedirectToAction("index", "User");
                       }
                }
                {
                    ModelState.AddModelError("", "invalid username or password");
                }
            }
            return View();
        }
    }
}