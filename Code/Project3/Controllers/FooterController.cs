using BGSitecore.Components;
using BGSitecore.Models;
using Glass.Mapper.Sc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BGSitecore.Controllers
{
    public class FooterController : Controller
    {
        public ActionResult Index()
        {
            var context = new SitecoreContext();
            Footer model = null;
            var bgContext = new BlueGreenContext();

            if (bgContext.IsAuthenticated && !Sitecore.Context.Item.ID.ToString().Equals("{D7551BEB-7866-4FF6-B238-94F2A3AE87C8}", StringComparison.CurrentCultureIgnoreCase))
            {
                model = context.GetItem<Footer>(Footer.AuthenticatedFooterId);
            }
            else
            {
                model = context.GetItem<Footer>(Footer.AnonymousFooterId);
            }

            return View("Footer", model);
        }

    }
}