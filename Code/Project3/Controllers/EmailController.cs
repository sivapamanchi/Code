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
using GoogleMapsApi;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.StaticMaps;
using GoogleMapsApi.StaticMaps.Entities;
using BGSitecore.Models.ResortService.ReservationsList;
using BGSitecore.Models.Resort;

namespace BGSitecore.Controllers
{
    public class EmailController : GlassController
    {

        [HttpPost]
        public string SendItinerary()
        {
            string result = "";
            string resNo = HttpContext.Server.HtmlEncode(Request.Form["resNo"]);
            string to = HttpContext.Server.HtmlEncode(Request.Form["to"]).Trim();
            string subject = HttpContext.Server.HtmlEncode(Request.Form["subject"]);
            string message = HttpContext.Server.HtmlEncode(Request.Form["message"]);

            BlueGreenContext bgcontext = new BlueGreenContext();
            Reservation reservation = bgcontext.GetActiveReservation(resNo);
            string projectNumber = (reservation != null) ? reservation.ProjectStay : null;
            if (projectNumber != null)
            {
                var splitToEmail = to.Split(';');
                foreach (string toEmail in splitToEmail)
                {
                    if (!string.IsNullOrEmpty(toEmail))
                    {
                        result = EmailUtils.SendItineraryEmail(toEmail, subject, message, reservation, bgcontext);
                    }
                }
            } else
            {
                result = EmailUtils.BuildJsonResponse(false, "project number not found");
            }

            return result;
        }
    }
}