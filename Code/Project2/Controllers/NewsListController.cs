using BGModern.Mappers;
using BGModern.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class NewsListController : SurfaceController
    {
        //
        // GET: /NewsList/

        public ActionResult NewsList(NewsListModel model)
        {
            return View(model);
        }

        //
        // ChildAction
        [ChildActionOnly]
        public ActionResult DisplayNewsList(List<NewsItemModel> newsItems)
        {
            return View();
        }

        //
        // Build list for home page
        public ActionResult BuildHomePageList()
        {
            NewsListModel model = null;

            dynamic list = null;
            int newsListContentId;
            string newsListContentIdString = System.Configuration.ConfigurationManager.AppSettings["newsListContentId"];
            if (!string.IsNullOrWhiteSpace(newsListContentIdString) && Int32.TryParse(newsListContentIdString, out newsListContentId))
            {
                list = Umbraco.TypedContent(newsListContentId);
            }

            if (list != null)
            {
                model = NewsListMapper.MapForHomePage(list);// we only want to pull the news items that are flagged for homepage inclusion
                
                if (String.IsNullOrEmpty(model.ListTitle))
                {
                    model.ListTitle = "Bluegreen News"; 
                } 
            }

            return PartialView("NewsColumn", model);
        }
    }
}
