using BGModern.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class ResortFoodAndBeverageController : RenderMvcController
    {
        // GET: ResortFoodAndBeverage
        public ActionResult ResortFoodAndBeverage()
        {
            var model = ResortMapper.MapForFoodAndBeverages(CurrentPage.Parent);

            model = MasterMapper.Map(model, CurrentPage.Parent);

            return View(model);
        }
    }
}