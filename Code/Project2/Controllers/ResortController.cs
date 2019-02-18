using BGModern.Mappers;
using BGModern.Models;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class ResortController : RenderMvcController
    {
        public ActionResult Resort()
        {
            ResortModel model = ResortMapper.Map(CurrentPage);

            model = MasterMapper.Map(model, CurrentPage);

            return View(model);
        }
    }
}