using BGSitecore.Components;
using BGSitecore.Models;
using BGSitecore.Services;
using BGSitecore.Utils;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BGSitecore.Controllers
{
    public class CalendarController : GlassController
    {
        /// <summary>
        /// Generates an iCalendar .ics link and returns it to the user.
        /// </summary>
        [System.Web.Mvc.HttpPost]
        public ActionResult GetCalendar(string resNo)
        {
            BlueGreenContext bgcontext = new BlueGreenContext();
            var reservation = bgcontext.GetActiveReservation(resNo, ResortService.RESERVATION_TYPE_FUTURE);
            var bytes = CalendarUtils.GenerateEvent(reservation);
            return this.File(bytes, "text/calendar", "BlueGreenVacations.ics");
        }
    }
}