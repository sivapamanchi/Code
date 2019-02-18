using BGSitecore.Components;
using BGSitecore.Models;
using BGSitecore.Utils;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BGSitecore.Controllers
{
    public class TopMenuController : GlassController
    {
      
        public ActionResult BuildMenu()
        {
            DebugUtils.StartLogEvent("TopMenuController.BuildMenu");

            OwnerUtils.ReloadPallet();

            //return View();
            var context = new SitecoreContext();
            var model = context.GetHomeItem<MenuItem>();

            var currentPage = context.GetCurrentItem<BasePage>();
            BlueGreenContext bgContext = new BlueGreenContext();
            model.ShowMenu = bgContext.IsAuthenticated;

            //model.ShowMenu = currentPage.PageRequireAuth;

            var datasourceModel = GetDataSourceItem<Header>();
            var homePage = context.GetHomeItem<BasePage>();
            if (datasourceModel == null)
            {
                model.Logo = homePage.HeaderConfiguration.HeaderImage;
            }
            else
            {
                model.Logo = datasourceModel.HeaderImage;
            }

            model.RemoteImageUrl = homePage.SiteSettings.RemoteImageUrl;
            DebugUtils.EndLogEvent("TopMenuController.BuildMenu");

            return View(model);
        }
    }
}