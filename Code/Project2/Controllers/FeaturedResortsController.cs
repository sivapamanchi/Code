using BGModern.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace BGModern.Controllers
{
    public class FeaturedResortsController : Umbraco.Web.Mvc.SurfaceController
    {
        // GET: FeaturedResorts
        public ActionResult GetFeaturedResorts()
        {
            FeaturedResortsListModel model = new FeaturedResortsListModel();
            model.FeaturedResortsList = new List<FeaturedResortsModel>();


            IPublishedContent ourResorts = null;
            int ourResortsContentId;
            string ourResortsContentIdString = ConfigurationManager.AppSettings["ourResortsContentId"];
            if (!string.IsNullOrWhiteSpace(ourResortsContentIdString) && Int32.TryParse(ourResortsContentIdString, out ourResortsContentId))
            {
                ourResorts = Umbraco.TypedContent(ourResortsContentId);

                if (ourResorts != null)
                {
                    var resorts = ourResorts.Children;
                    resorts = resorts.Where(x => x.GetPropertyValue<int>("IsFeatured") == 1);

                    //Populate the image based on the resorts.
                    if (resorts != null)
                    {
                        foreach (IPublishedContent content in resorts)
                        {
                            if (content.DocumentTypeAlias.Equals("Resort"))
                            {
                                FeaturedResortsModel resort = new FeaturedResortsModel();

                                resort.ResortId = content.GetPropertyValue<string>("DatabaseId");
                                resort.ResortName = content.GetPropertyValue<string>("ResortName");
                                resort.ResortDescShort = content.GetPropertyValue<string>("ResortSummary");

                                resort.ImageName = content.GetPropertyValue<string>("ResortImage");
                                resort.ResortLink = content.Url;

                                if (resort.ResortLink == null || resort.ResortLink == "")
                                {
                                    resort.ResortLink = BGModern.HtmlExtensions.CustomHtmlHelpers.GetFullSitePath(null).ToString() + "/our-resorts";
                                }

                                model.FeaturedResortsList.Add(resort); 
                            }
                        }
                    }
                }
            }

            return PartialView("FeaturedResorts", model);
        }
    }
}