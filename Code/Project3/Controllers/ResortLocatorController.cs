using BGSitecore.Components;
using BGSitecore.Models.Resort;
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
    public class ResortLocatorController : GlassController
    {
        // GET: ResortLocator
        public ActionResult Find()
        {
            DebugUtils.StartLogEvent("ResortLocatorController.Find");
            var context = new SitecoreContext();
            var model = context.GetCurrentItem<ResortLocator>(false, true);
            model.AllResorts = ResortManager.GetAllResorts();
            model.AllExperience = ResortManager.GetAllExperience();
            model.ClubAffiliation = ResortManager.GetAllClubAffiliation();

            DebugUtils.EndLogEvent("ResortLocatorController.Find");
            return View(model);
        }

    }
}