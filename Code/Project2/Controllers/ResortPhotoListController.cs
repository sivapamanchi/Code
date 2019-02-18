using BGModern.Mappers;
using BGModern.Models;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class ResortPhotoListController : RenderMvcController
    {
        /// <summary>
        /// This action will use the current page's parent (the parent is the resort and we need the resort model...it includes the child photo list)
        /// </summary>
        /// <returns></returns>
        public ActionResult ResortPhotoList()
        {
            var model = ResortMapper.MapForPhotos(CurrentPage.Parent);

            model = MasterMapper.Map(model, CurrentPage.Parent);

            return View(model);
        }
    }
}