using BGModern.Mappers;
using BGModern.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    public class HomeController : RenderMvcController
    {
        string HomePageRedirect = ConfigurationManager.AppSettings["LinkForHomePage"];
        string SearchResultsRedirect = ConfigurationManager.AppSettings["LinkForSearchResultsPage"];
        string TryNewSearchRedirect = ConfigurationManager.AppSettings["LinkForTryNewSearchRedirectPage"];
        //
        // GET: /Home/

        public ActionResult Home()
        {
            if (Session["BXGOwner"] == null)
            {
                if (Session["_path_info"] != null)
                    Session["_path_info"] = Request.RawUrl;

                Response.Redirect(ConfigurationManager.AppSettings["bxgwebUnsecureURL"] + "default.aspx?sess=timeout", true);
            }

            // Fixed/Flex users have a different home page
            var isFixedFlexOrTraditionalOwner = Session["IsFixedFlexOrTraditionalOwner"];
            
            string referrer = string.Empty;
            if (System.Web.HttpContext.Current.Request.UrlReferrer != null)
            {
                if (System.Web.HttpContext.Current.Request.UrlReferrer.AbsoluteUri != null)
                {
                    referrer = System.Web.HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
                }
            }
            if (isFixedFlexOrTraditionalOwner is bool && (bool)isFixedFlexOrTraditionalOwner)
            {
                OurResortsModel ourResorts = null;
                bool vacationClubOwner = false;
                bool vacationClubOrSampler = false;
                int ourResortsContentId;
                IPublishedContent resortContent = null;
                string ourResortsContentIdString = ConfigurationManager.AppSettings["ourResortsContentId"];
                if (!string.IsNullOrWhiteSpace(ourResortsContentIdString) && Int32.TryParse(ourResortsContentIdString, out ourResortsContentId))
                {
                    resortContent = Umbraco.TypedContent(ourResortsContentId);
                    ourResorts = OurResortsMapper.Map(resortContent);
                    ourResorts.VacationClubOwner = vacationClubOwner;
                    ourResorts.VacationClubOrSamplerOwner = vacationClubOrSampler;
                    ourResorts.PagerModel = null;
                }

                var children = resortContent.Children;
                var homeResortId = Session["OwnerHomeResort"];
                if (homeResortId != null)
                {
                    children = children.Where(x => x.GetPropertyValue<string>("databaseid") == (string)homeResortId);
                }

                foreach (IPublishedContent child in children)
                {
                    ResortModel resort = ResortMapper.Map(child);
                    resort.IncludeBorderLine = true;
                    resort.IncludePhone = true;

                    if (resort != null)
                    {
                        if (resort.Name != null)
                            resort.Name = resort.Name.ToUpper();

                        ourResorts.OurResorts.Add(resort);
                    }
                }
                if (AllowLegacySearch(referrer))
                {
                    Response.Redirect(HomePageRedirect, true);
                }
                return View("HomeFixed", ourResorts);
            }
            else
            {
                HomeModel model = new HomeModel();

                //Logic for checking side navigation visibility
                BGO.OwnerWS.Owner BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
                model.VacationClubOwner = false;
                if (Session["OwnerContractType"] != null && (Session["OwnerContractType"].ToString() == "Vacation Club" ||
                    Session["OwnerContractType"].ToString() == "Sampler"))
                {
                    if (BXGOwner != null && BXGOwner.User != null && !BXGOwner.User[0].isSampler && Session["OwnerContractType"].ToString() != "Sampler")
                        model.VacationClubOwner = true;
                }

                if (Session["BGModernSessionVariablesAreSet"] == null)
                {
                    Global.SetSessionVariablesForBGModern();
                }
                if (AllowLegacySearch(referrer))
                {
                    Response.Redirect(HomePageRedirect, true);
                }
                return View(model);
            }
        }

        public bool AllowLegacySearch(string referrer)
        {
            if (string.IsNullOrEmpty(referrer)
                || (!string.IsNullOrEmpty(referrer) && !(referrer.ToString().ToLower().Equals(HomePageRedirect.ToLower())
                || referrer.ToString().ToLower().Equals(SearchResultsRedirect.ToLower())
                || referrer.ToString().ToLower().Equals(TryNewSearchRedirect.ToLower())
                )))
            {
                return true;
            }
            return false;
        }
    }
}
