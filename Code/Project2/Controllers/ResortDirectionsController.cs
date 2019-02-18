using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;

namespace BGModern.Controllers
{
    public class ResortDirectionsController : Umbraco.Web.Mvc.SurfaceController
    {
        // GET: ResortDirections
        public ActionResult Geolocate(int nodeID)
        {
            IPublishedContent page = Umbraco.TypedContent(nodeID);

            return View();
        }
    }
}