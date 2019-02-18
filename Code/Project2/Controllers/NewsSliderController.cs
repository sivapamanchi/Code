using BGModern.Mappers;
using BGModern.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Dynamics;
using Umbraco.Core.Models;

namespace BGModern.Controllers
{
    public class NewsSliderController : Umbraco.Web.Mvc.SurfaceController
    {
        public ActionResult Index()
        {
            throw new NotImplementedException("Index is not implemented for ResortLocatorController");
        }

        // GET: NewsSlider
        [HttpGet]
        public ActionResult GetPartialView()
        {
            var salesTypeCode = GetSalesTypeCode((BGO.OwnerWS.Owner)Session["BXGOwner"]);

            NewsListModel newsList = new NewsListModel();
            int newsListContentId;
            string newsListContentIdString = ConfigurationManager.AppSettings["newsListContentId"];
            if (!string.IsNullOrWhiteSpace(newsListContentIdString) && Int32.TryParse(newsListContentIdString, out newsListContentId))
            {
                var content = Umbraco.Content(newsListContentId);
                if (content.GetType() != typeof(DynamicNull))
                {
                    newsList = NewsListMapper.MapForHomePage(content, salesTypeCode);
                }
            }

            PromoListModel promoList = new PromoListModel();
            int promoListContentId;
            string promoListContentIdString = ConfigurationManager.AppSettings["promoListContentId"];
            if (!string.IsNullOrWhiteSpace(promoListContentIdString) && Int32.TryParse(promoListContentIdString, out promoListContentId))
            {
                var content = Umbraco.Content(promoListContentId);
                if (content.GetType() != typeof(DynamicNull))
                {
                    promoList = PromoListMapper.MapForHomePage(content, salesTypeCode);
                }
            }

            var newsItemCount = newsList.NewsItems.Count;
            var promoItemCount = promoList.PromoItems.Count;
            var numTotalItems = newsItemCount + promoItemCount;

            var model = new NewsSliderModel();
            int newsIndex = 0;
            int promoIndex = 0;

            while (newsIndex < newsItemCount || promoIndex < promoItemCount)
            {
                if (newsIndex < newsItemCount)
                {
                    model.NewsAndPromoList.Add(newsList.NewsItems[newsIndex]);
                    newsIndex++;
                }

                if (promoIndex < promoItemCount)
                {
                    model.NewsAndPromoList.Add(promoList.PromoItems[promoIndex]);
                    promoIndex++;
                }
            }

            return PartialView("NewsSlider", model);
        }

        private string GetSalesTypeCode(BGO.OwnerWS.Owner bxgOwner)
        {
            var contractType = (string)Session["OwnerContractType"];
            bool isTravelerPlusEmployee = (string)Session["IsTravelerPlusEmployee"] == "TRUE";
            bool isSamplerOwner = bxgOwner.User[0].isSampler;
            string homeProject = bxgOwner.User[0].HomeProject;
            bool NVC = false;
            string searchParameter = string.Empty;

            if (contractType == "Vacation Club" || contractType == "Sampler")
            {
                NVC = false;
            }
            else
            {
                NVC = true;
            }

            if (isTravelerPlusEmployee)
            {
                searchParameter = "TP";
            }
            else if (NVC)
            {
                searchParameter = "NVC";
            }
            else if (contractType == "Vacation Club")
            {
                if (HasOwnerAccountTypeAU(bxgOwner))
                {
                    searchParameter = "O1";
                }
                else
                {
                    searchParameter = "VC";
                }
            }
            else if (isSamplerOwner)
            {
                if (homeProject == "52")
                {
                    searchParameter = "SMP24";
                }
                else if (homeProject == "51")
                {
                    searchParameter = "VSMP";
                }
            }

            return searchParameter;
        }

        private bool HasOwnerAccountTypeAU(BGO.OwnerWS.Owner bxgOwner)
        {
            foreach (var maintenanceFee in bxgOwner.maintFees)
            {
                if (maintenanceFee.saleType == "A" || maintenanceFee.saleType == "U")
                {
                    return true;
                }
            }

            return false;
        }
    }
}