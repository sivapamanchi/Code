using BGModern.Mappers;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class AreaInformationController : RenderMvcController
    {
        // GET: AreaInformation
        public ActionResult AreaInformation()
        {
            var model = ResortMapper.MapForAreaInformation(CurrentPage.Parent);

            model = MasterMapper.Map(model, CurrentPage.Parent);

            return View(model);
        }
    }
}