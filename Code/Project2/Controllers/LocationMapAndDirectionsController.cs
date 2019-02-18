using BGModern.Mappers;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class LocationMapAndDirectionsController : RenderMvcController
    {
        // GET: LocationMapAndDirections
        public ActionResult LocationMapAndDirections()
        {
            var model = ResortMapper.Map(CurrentPage.Parent);

            model = MasterMapper.Map(model, CurrentPage.Parent);

            return View(model);
        }
    }
}