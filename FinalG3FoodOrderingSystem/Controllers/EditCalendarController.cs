using FinalG3FoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalG3FoodOrderingSystem.Controllers
{
    public class EditCalendarController : Controller
    {
        // GET: EditCalendar
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            using (FoodOrderingSystemDatabaseEntities7 dc = new FoodOrderingSystemDatabaseEntities7())
            {
                var events = dc.Tables.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(Table e)
        {
            var status = false;
            using (FoodOrderingSystemDatabaseEntities7 dc = new FoodOrderingSystemDatabaseEntities7())
            {
                if (e.EventID > 0)
                {
                    //Update the event
                    var v = dc.Tables.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.Tables.Add(e);
                }
                dc.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (FoodOrderingSystemDatabaseEntities7 dc = new FoodOrderingSystemDatabaseEntities7())
            {
                var v = dc.Tables.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Tables.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}
