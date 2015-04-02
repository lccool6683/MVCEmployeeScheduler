using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DHTMLX.Scheduler;
using DHTMLX.Common;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;
using MVCEmployeeScheduler.Models;
using MVCEmployeeScheduler.Logic;

namespace MVCEmployeeScheduler.Controllers
{
	public class OffDaysController : Controller
	{
		// GET: MultiScheduler
		public class Mod
		{
			public DHXScheduler Sh1 { get; set; }
			public DHXScheduler Sh2 { get; set; }
		}

		public ActionResult Index()
		{
			//each scheduler must have unique name
			var scheduler = new DHXScheduler("sched1");
			var scheduler2 = new DHXScheduler("sched2");

			scheduler.DataAction = Url.Action("Data", "OffDays");
			scheduler.SaveAction = Url.Action("Save", "OffDays");
			scheduler2.DataAction = Url.Action("Data1", "OffDays");
			scheduler2.SaveAction = Url.Action("Save1", "OffDays");

			scheduler.LoadData = true;
			scheduler.EnableDataprocessor = true;
			scheduler2.LoadData = true;
			scheduler2.EnableDataprocessor = true;

            var context = new CRMDBContext();
			var multiselect = new LightboxSelect("employee", "Employee");
			multiselect.AddOptions(context.Employees.Select(t => new { key = t.ID, label = t.Name }));

			scheduler.Lightbox.Add(new LightboxText("text", "Title"));
			scheduler.Lightbox.Add(multiselect);
			scheduler.Lightbox.Add(new LightboxMiniCalendar("time", "Time"));

			scheduler2.Lightbox.Add(new LightboxText("text", "Title"));
			scheduler2.Lightbox.Add(multiselect);
			scheduler2.Lightbox.Add(new LightboxMiniCalendar("time", "Time"));

			return View(new Mod { Sh1 = scheduler, Sh2 = scheduler2 });
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
						action.TargetId = changedEvent.ID;//assign postoperational id
						changedEvent.Description = text;
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
			catch //(Exception e)
			{
				action.Type = DataActionTypes.Error;

				// Log the exception.
                //ExceptionUtility.LogException(e, action.Type.ToString());
			}
			return (ContentResult)new AjaxSaveResponse(action);
		}

		public ContentResult Data1()
		{
			//var data = new SchedulerAjaxData(new DataClasses1DataContext().Events);
			var data = new SchedulerAjaxData(
                new CRMDBContext().OffDaysCalendar.Select(e => new { id = e.ID, start_date = e.Start_date, end_date = e.End_date, text = e.Description })
			);

			return (ContentResult)data;
		}

		public ContentResult Save1(int? id, FormCollection actionValues, string text)
		{
			var action = new DataAction(actionValues);

			try
			{
                var changedEvent = (OffDays)DHXEventsHelper.Bind(typeof(OffDays), actionValues);
                var data = new CRMDBContext();

				switch (action.Type)
				{
					case DataActionTypes.Insert:
                        data.OffDaysCalendar.Add(changedEvent);
						action.TargetId = changedEvent.ID;//assign postoperational id
						//changedEvent.Description = text;
						break;
					case DataActionTypes.Delete:
                        changedEvent = data.OffDaysCalendar.SingleOrDefault(ev => ev.ID == action.SourceId);
                        data.OffDaysCalendar.Remove(changedEvent);
						break;
					default:// "update"
                        var eventToUpdate = data.OffDaysCalendar.SingleOrDefault(ev => ev.ID == action.SourceId);
						changedEvent.Description = text;
						DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });
						break;
                }
                data.SaveChanges();
				action.TargetId = changedEvent.ID;
			}
			catch//(Exception e)
			{
				action.Type = DataActionTypes.Error;
				// Log the exception.
                //ExceptionUtility.LogException(e, action.Type.ToString());
			}
			return (ContentResult)new AjaxSaveResponse(action);
		}
	}
}
