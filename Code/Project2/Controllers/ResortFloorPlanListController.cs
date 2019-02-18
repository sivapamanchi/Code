using BGModern.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class ResortFloorPlanListController : RenderMvcController
    {
        // GET: ResortFloorPlanList
        public ActionResult ResortFloorPlanList()
        {
            var model = ResortMapper.MapForFloorPlans(CurrentPage.Parent);

            model = MasterMapper.Map(model, CurrentPage.Parent);

            return View("ResortFloorPlanList", model);
        }
    }
}