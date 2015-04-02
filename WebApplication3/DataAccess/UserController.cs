using DHTMLX.Common;
using DHTMLX.Helpers;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEmployeeScheduler.Models;
using MVCEmployeeScheduler.Logic;

namespace MVCEmployeeScheduler.Controllers
{
	public class UserController : Controller
	{
		public ActionResult Index()
		{
			//Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes
			var scheduler = new DHXScheduler();
			scheduler.Config.isReadonly = true;
			scheduler.DataAction = Url.Action("Data");
			scheduler.SaveAction = Url.Action("Save");
			scheduler.LoadData = true;
			scheduler.EnableDataprocessor = true;

			return View(scheduler);
		}

		public ContentResult Data()
		{
            var data = new SchedulerAjaxData(new CRMDBContext().Shift);

			return (ContentResult)data;
		}

		public ContentResult Save(int? id, FormCollection actionValues)
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
						action.TargetId = changedEvent.ID;//assign postoperational id
						break;
					case DataActionTypes.Delete:
						changedEvent = data.Shift.SingleOrDefault(ev => ev.ID == action.SourceId);
						data.Shift.Remove(changedEvent);
						break;
					default:// "update"                          
						var eventToUpdate = data.Shift.SingleOrDefault(ev => ev.ID == action.SourceId);
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




