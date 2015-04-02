using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;
using System.Collections.Generic;
using System.Web.Mvc;
using MVCEmployeeScheduler.Models;
using System.Linq;
using System;
using MVCEmployeeScheduler.Logic;

namespace MVCEmployeeScheduler.Controllers
{
    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            //Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes
            var scheduler = new DHXScheduler();
            scheduler.DataAction = Url.Action("Data");
            scheduler.SaveAction = Url.Action("Save");

            /*
             * It's possible to use different actions of the current controller
             *      var scheduler = new DHXScheduler(this);     
             *      scheduler.DataAction = "ActionName1";
             *      scheduler.SaveAction = "ActionName2";
             * 
             * Or to specify full paths
             *      var scheduler = new DHXScheduler();
             *      scheduler.DataAction = Url.Action("Data", "Calendar");
             *      scheduler.SaveAction = Url.Action("Save", "Calendar");
             */

            /*
             * The default codebase folder is ~/Scripts/dhtmlxScheduler. It can be overriden:
             *      scheduler.Codebase = Url.Content("~/customCodebaseFolder");
             */


            //scheduler.InitialDate = new DateTime(2012, 09, 03);

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            var context = new CRMDBContext();

            scheduler.Lightbox.Add(new LightboxText("text", "Title"));

            var multiselect = new LightboxSelect("staff", "Staff");

            multiselect.AddOptions(context.Employees.Select(t => new { key = t.ID, label = t.Name }));
            scheduler.Lightbox.Add(multiselect);

            var selectcategory = new LightboxSelect("category", "Category");
            var items = new List<object>(){
                new { key = "employee", label = "Employee" },
                new { key = "manager", label = "Management" },
                new { key = "visitor", label = "Visitor" }
            };
            selectcategory.AddOptions(items);
            scheduler.Lightbox.Add(selectcategory);

            scheduler.Lightbox.Add(new LightboxMiniCalendar("time", "Time"));

            return View(scheduler);
        }

        public ContentResult Data()
        {
            //var data = new SchedulerAjaxData(new DataClasses1DataContext().Events);
            var data = new SchedulerAjaxData(
                new CRMDBContext().Shift.Select(e => new { id = e.ID, start_date = e.Start_date, end_date = e.End_date, text = e.Description })
            );

            return (ContentResult)data;
        }

        public ContentResult Save(int? id, FormCollection actionValues, string text)
        {
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = (Shift)DHXEventsHelper.Bind(typeof(Shift), actionValues);
                var data = new CRMDBContext();

                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        data.Shift.Add(changedEvent);
                        changedEvent.Description = text;
                        action.TargetId = changedEvent.ID;//assign postoperational id
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = data.Shift.SingleOrDefault(ev => ev.ID == action.SourceId);
                        data.Shift.Remove(changedEvent);
                        break;
                    default:// "update"                          
                        var eventToUpdate = data.Shift.SingleOrDefault(ev => ev.ID == action.SourceId);
                        changedEvent.Description = text;
                        DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });
                        break;
                }
                data.SaveChanges();
                action.TargetId = changedEvent.ID;
            }
			catch (Exception e)
            {
                action.Type = DataActionTypes.Error;
				// Log the exception.
				ExceptionUtility.LogException(e, action.Type.ToString());
            }
            return (ContentResult)new AjaxSaveResponse(action);
        }
    }
}

