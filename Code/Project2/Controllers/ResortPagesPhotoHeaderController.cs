using BGModern.Mappers;
using BGModern.Models;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class ResortPagesPhotoHeaderController : SurfaceController
    {
        /// <summary>
        /// This action is used to return a basic photo carousel to be used as a header on various Resort pages. This does not include a thumbnail scroller.
        /// </summary>
        /// <param name="id">the id of the resort Content for which photos are to be retrieved</param>
        /// <returns></returns>
        public ActionResult ResortPagesPhotoHeader(int id)
        {
            IPublishedContent page = Umbraco.TypedContent(id);

            ResortModel resortModel = new ResortModel();

            CaptionedPhotoListModel model = new CaptionedPhotoListModel();

            foreach (IPublishedContent child in page.Children)
            {
                if (child.DocumentTypeAlias == "Resortphotolist")
                {
                    model = CaptionedPhotoListMapper.Map(child);
                    break;
                }
            }

            return PartialView("ResortPagesPhotosHeader", model);
        }
    }
}