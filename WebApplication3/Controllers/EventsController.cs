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
	public class EventsController : Controller
	{
		private CRMDBContext db = new CRMDBContext();

		// GET: DBEvents
		public ActionResult Index()
		{
			return View(db.Events.ToList());
		}

		// GET: DBEvents/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Events DBEvents = db.Events.Find(id);
			if (DBEvents == null)
			{
				return HttpNotFound();
			}
			return View(DBEvents);
		}

		// GET: DBEvents/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: DBEvents/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Title,Description,StartDate,EndDate,Repeated,RepeatedOption,RepeatedEvery,RepeatedTimes,RepeatedEnd,RepeatedMonday,RepeatedTuesday,RepeatedWednesday,RepeatedThursday,RepeatedFriday,RepeatedSaturday,RepeatedSunday")] Events events, string services)
		{
			if (events.Repeated == true)
			{
				if (events.RepeatedOption == "Hourly")
				{
					//db.Events.Add(events);
					//db.SaveChanges();
					for (int i = 0; i < (events.RepeatedTimes); i++)
					{
						for (int j = 0; j < events.RepeatedEvery; j++)
						{
                            events.StartDate = events.StartDate.AddHours(1);
							events.EndDate = events.EndDate.AddHours(1);
						}
						db.Events.Add(events);
						db.SaveChanges();
					}
					return RedirectToAction("Index");
				}
				if (events.RepeatedOption == "Daily")
				{
					//db.Events.Add(events);
					//db.SaveChanges();
					for (int i = 0; i < (events.RepeatedTimes); i++)
					{
						for (int j = 0; j < events.RepeatedEvery; j++)
						{
							events.StartDate = events.StartDate.AddDays(1);
							events.EndDate = events.EndDate.AddDays(1);
						}
						db.Events.Add(events);
						db.SaveChanges();
					}
					return RedirectToAction("Index");
				}
				else if (events.RepeatedOption == "Weekly")
				{
					if (events.RepeatedMonday == true)
					{
						//db.Events.Add(events);
						//db.SaveChanges();
						for (int i = 0; i < (events.RepeatedTimes); i++)
						{
							for (int j = 0; j < events.RepeatedEvery; j++)
							{
                                events.StartDate = events.StartDate.AddDays(1);
								events.EndDate = events.EndDate.AddDays(1);
								while (events.StartDate.DayOfWeek != DayOfWeek.Monday)
								{
									events.StartDate = events.StartDate.AddDays(1);
									events.EndDate = events.EndDate.AddDays(1);
								}
							}
							db.Events.Add(events);
							db.SaveChanges();
						}
					}

					if (events.RepeatedTuesday == true)
					{
						//db.Events.Add(events);
						//db.SaveChanges();
						for (int i = 0; i < (events.RepeatedTimes); i++)
						{
							for (int j = 0; j < events.RepeatedEvery; j++)
							{
								events.StartDate = events.StartDate.AddDays(1);
								events.EndDate = events.EndDate.AddDays(1);
								while (events.StartDate.DayOfWeek != DayOfWeek.Tuesday)
								{
									events.StartDate = events.StartDate.AddDays(1);
									events.EndDate = events.EndDate.AddDays(1);
								}
							}
							db.Events.Add(events);
							db.SaveChanges();
						}
					}

					if (events.RepeatedWednesday == true)
					{
						//db.Events.Add(events);
						//db.SaveChanges();
						for (int i = 0; i < (events.RepeatedTimes); i++)
						{
							for (int j = 0; j < events.RepeatedEvery; j++)
							{
								events.StartDate = events.StartDate.AddDays(1);
								events.EndDate = events.EndDate.AddDays(1);
								while (events.StartDate.DayOfWeek != DayOfWeek.Wednesday)
								{
									events.StartDate = events.StartDate.AddDays(1);
									events.EndDate = events.EndDate.AddDays(1);
								}
							}
							db.Events.Add(events);
							db.SaveChanges();
						}
					}

					if (events.RepeatedThursday == true)
					{
						//db.Events.Add(events);
						//db.SaveChanges();
						for (int i = 0; i < (events.RepeatedTimes); i++)
						{
							for (int j = 0; j < events.RepeatedEvery; j++)
							{
								events.StartDate = events.StartDate.AddDays(1);
								events.EndDate = events.EndDate.AddDays(1);
								while (events.StartDate.DayOfWeek != DayOfWeek.Thursday)
								{
									events.StartDate = events.StartDate.AddDays(1);
									events.EndDate = events.EndDate.AddDays(1);
								}
							}
							db.Events.Add(events);
							db.SaveChanges();
						}
					}

					if (events.RepeatedFriday == true)
					{
						//db.Events.Add(events);
						//db.SaveChanges();
						for (int i = 0; i < (events.RepeatedTimes); i++)
						{
							for (int j = 0; j < events.RepeatedEvery; j++)
							{
								events.StartDate = events.StartDate.AddDays(1);
								events.EndDate = events.EndDate.AddDays(1);
								while (events.StartDate.DayOfWeek != DayOfWeek.Friday)
								{
									events.StartDate = events.StartDate.AddDays(1);
									events.EndDate = events.EndDate.AddDays(1);
								}
							}
							db.Events.Add(events);
							db.SaveChanges();
						}
					}

					if (events.RepeatedSaturday == true)
					{
						//db.Events.Add(events);
						//db.SaveChanges();
						for (int i = 0; i < (events.RepeatedTimes); i++)
						{
							for (int j = 0; j < events.RepeatedEvery; j++)
							{
								events.StartDate = events.StartDate.AddDays(1);
								events.EndDate = events.EndDate.AddDays(1);
								while (events.StartDate.DayOfWeek != DayOfWeek.Saturday)
								{
									events.StartDate = events.StartDate.AddDays(1);
									events.EndDate = events.EndDate.AddDays(1);
								}
							}
							db.Events.Add(events);
							db.SaveChanges();
						}
					}

					if (events.RepeatedSunday == true)
					{
						//db.Events.Add(events);
						//db.SaveChanges();
						for (int i = 0; i < (events.RepeatedTimes); i++)
						{
							for (int j = 0; j < events.RepeatedEvery; j++)
							{
								events.StartDate = events.StartDate.AddDays(1);
								events.EndDate = events.EndDate.AddDays(1);
								while (events.StartDate.DayOfWeek != DayOfWeek.Sunday)
								{
									events.StartDate = events.StartDate.AddDays(1);
									events.EndDate = events.EndDate.AddDays(1);
								}
							}
							db.Events.Add(events);
							db.SaveChanges();
						}
					}
					return RedirectToAction("Index");
				}
				else if (events.RepeatedOption == "Monthly")
				{
					//db.Events.Add(events);
					//db.SaveChanges();
					for (int i = 0; i < (events.RepeatedTimes); i++)
					{
						for (int j = 0; j < events.RepeatedEvery; j++)
						{
							events.StartDate = events.StartDate.AddMonths(1);
							events.EndDate = events.EndDate.AddMonths(1);
						}
						db.Events.Add(events);
						db.SaveChanges();
					}
					return RedirectToAction("Index");
				}
				else if (events.RepeatedOption == "Yearly")
				{
					//db.Events.Add(events);
					//db.SaveChanges();
					for (int i = 0; i < (events.RepeatedTimes); i++)
					{
						for (int j = 0; j < events.RepeatedEvery; j++)
						{
							events.StartDate = events.StartDate.AddYears(1);
							events.EndDate = events.EndDate.AddYears(1);
						}
						db.Events.Add(events);
						db.SaveChanges();
					}
					return RedirectToAction("Index");
				}
			}
			return RedirectToAction("Index");
		}

		// GET: DBEvents/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Events DBEvents = db.Events.SingleOrDefault(ev => ev.ID == id);
			if (DBEvents == null)
			{
				return HttpNotFound();
			}
			return View(DBEvents);
		}

		// POST: DBEvents/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ID,EmployeeID,ServiceID,Title,Description,StartDate,EndDate,Repeated,RepeatedOption,RepeatedEvery,RepeatedTimes,RepeatedMonday,RepeatedTuesday,RepeatedWednesday,RepeatedThursday,RepeatedFriday,RepeatedSaturday,RepeatedSunday,Status,ActualDuration,CompleteDate")] Events events)
		{
			if (ModelState.IsValid)
			{
				//db.Entry(DBEvents).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(events);
		}

		// GET: DBEvents/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Events DBEvents = db.Events.SingleOrDefault(ev => ev.ID == id);
			if (DBEvents == null)
			{
				return HttpNotFound();
			}
			return View(DBEvents);
		}

		// POST: DBEvents/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Events DBEvents = db.Events.Find(id);
			db.Events.Remove(DBEvents);
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
