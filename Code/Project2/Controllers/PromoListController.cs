using BGModern.Mappers;
using BGModern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class PromoListController : SurfaceController
    {
        //
        // GET: /NewsList/

        public ActionResult PromoList(PromoListModel model)
        {
            return View(model);
        }

        //
        // ChildAction
        [ChildActionOnly]
        public ActionResult DisplayPromoList(List<PromoListModel> newsItems)
        {
            return View();
        }

        //
        // Build list for home page
        public ActionResult BuildHomePageList()
        {
            PromoListModel model = null;

            IPublishedContent list = null;
            int promoListContentId;
            string promoListContentIdString = System.Configuration.ConfigurationManager.AppSettings["promoListContentId"];
            if (!string.IsNullOrWhiteSpace(promoListContentIdString) && Int32.TryParse(promoListContentIdString, out promoListContentId))
            {
                list = Umbraco.TypedContent(promoListContentId);
            }

            if (list != null)
            {
                model = PromoListMapper.MapForHomePage(list);// we only want to pull the promo items that are flagged for homepage inclusion

                if (String.IsNullOrEmpty(model.ListTitle))
                {
                    model.ListTitle = "Bluegreen Promotions";
                }
            }

            return PartialView("PromoColumn", model);
        }
    }
}