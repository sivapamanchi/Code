using BGModern.Mappers;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class ResortPointsTableController : RenderMvcController
    {
        // GET: ResortPointsTable
        public ActionResult ResortPointsTable()
        {
            var model = ResortMapper.MapForPointsTable(CurrentPage.Parent);

            model = MasterMapper.Map(model, CurrentPage.Parent);

            return View(model);
        }
    }
}