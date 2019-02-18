using BGModern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BGO.OwnerWS;

namespace BGModern.Controllers
{
    public class OwnerPointsAndSignOffController :  Umbraco.Web.Mvc.SurfaceController
    {
        // GET: Owner
        public ActionResult OwnerPointsAndSignOff()
        {
            // TODO : build the code to populate the model with the signed-in owner's data
            BGO.OwnerWS.Owner model = (BGO.OwnerWS.Owner)Session["BXGOwner"] ?? new BGO.OwnerWS.Owner();

            return PartialView(model);
        }
    }
}