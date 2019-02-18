using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using BGO;//.availabilityservice;

namespace BGModern.Controllers
{
    // TODO: [Authorize]
    public class DailyRateController : Umbraco.Web.Mvc.SurfaceController
    {
        public JsonResult RetrieveAvailabilitySample()
        {
            var availabilityServiceClient = new BGO.availabilityservice.AvailabilityServiceSoapClient();

            var resortInfo = new BGO.availabilityservice.ResortCriteria()
            {
                WebUnitTypes = "ANY",
                ResortId = "10"
            };

            var resortInfoArray = new BGO.availabilityservice.ResortCriteria[] { resortInfo };

            var criteria = new BGO.availabilityservice.AvailabilitySearchCriteria()
            {
                CheckinDate = "1/2/2016",
                CheckoutDate = "1/8/2016",
                LOS = 7,
                ResortInfo = resortInfoArray,
                Accomodates = 2,
                ExtendResortList = "",
                Segments = "",
                SiteId = "7",
                SiteName = "Online Points",
                ReservationType = "P",
                HandicapAccessible = false,
            };

            var results = availabilityServiceClient.GetBGOInventoryAvailability(criteria);
            return Json(results.mylist, JsonRequestBehavior.AllowGet);
        }
    }
}