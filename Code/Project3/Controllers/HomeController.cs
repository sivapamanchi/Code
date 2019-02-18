using BGSitecore.Components;
using BGSitecore.Models;
using BGSitecore.Models.Resort;
using BGSitecore.Utils;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BGSitecore.Controllers
{
    public class HomeController : GlassController
    {

        public ActionResult Featured()
        {
            DebugUtils.StartLogEvent("HomeController.Featured");
            var model = new FeaturedItemsCache();

            if (Session["AllFeaturedItems"] != null)
            {
                model = (FeaturedItemsCache)Session["AllFeaturedItems"];
            }
            else
            {
                var allFeatures = GetLayoutItem<FeaturedItems>();
                FeaturedItemsCache tmpList = new FeaturedItemsCache();
                tmpList.AllFeaturedItems = new List<FeaturedItemCache>();
                tmpList.PageTitle = allFeatures.PageTitle;
                allFeatures.AllFeaturedItems = allFeatures.AllFeaturedItems.Where(x => ((x.StartDate == DateTime.MinValue || x.StartDate <= DateTime.Now) &&
        (x.EndDate == DateTime.MinValue || x.EndDate >= DateTime.Now))).Select(featuredItem => featuredItem);

                foreach (FeaturedItem item in allFeatures.AllFeaturedItems)
                {
                    if (item.Active && item.AccessibleHomePage)
                    {
                        if (item.InnerItem.Fields[BaseComponent.RestrictionRuleId] != null)
                        {
                            String rule = item.InnerItem.Fields[BaseComponent.RestrictionRuleId].Value;
                            if (!string.IsNullOrEmpty(rule))
                            {
                                if (SitecoreUtils.EvaluateRules(rule, item.InnerItem))
                                {
                                    FeaturedItemCache newItem = new Models.FeaturedItemCache();
                                    newItem.PageTitle = item.PageTitle;
                                    newItem.ImageSrc = ImageUtils.BuildImageUrl(item.FeaturedImage, allFeatures.SiteSettings.RemoteImageUrl, "/images/white.png");
                                    newItem.ImageCaption = item.Caption;
                                    newItem.ImageHref = item.RedirectLink == null ? item.Url : item.RedirectLink.Url;
                                    newItem.Category = item.Category;

                                    tmpList.AllFeaturedItems.Add(newItem);
                                }

                            }
                        }
                    }


                }
                if (tmpList.AllFeaturedItems.Count > 0)
                {
                    tmpList.AllFeaturedItems = tmpList.AllFeaturedItems.OrderBy(o => o.Category).ToList();
                }
                    model = tmpList;
                    Session["AllFeaturedItems"] = tmpList;
                
            }
            DebugUtils.EndLogEvent("HomeController.Featured");

            return View(model);
        }

        public ActionResult Search()
        {
            DebugUtils.StartLogEvent("HomeController.Search");
            var model = GetLayoutItem<IndexSearch>();
            model.searchOptions = new SearchResult();
            model.SavedSearches = SearchUtils.GetSavedSearches();

            SearchUtils.ValidateSearchOptions(model.searchOptions, true);  //Removes any Resorts the user doesn't have access
            Session["SearchType"] = null;
            Session["SearchResortId"] = null;

            if (Session["SearchCity"] != null)
            {
                var city = Session["SearchCity"].ToString();
                if (!string.IsNullOrEmpty(city))
                {
                    var spit = city.Split(',');
                    model.searchOptions.searchParameters.Destination = "city-" + spit[0];
                }
                Session["SearchCity"] = null;
            }
            if (Session["SearchLOS"] != null)
            {
                var los = Convert.ToInt16(Session["SearchLOS"]);
                model.searchOptions.searchParameters.CheckOutDate = model.searchOptions.searchParameters.CheckInDate.AddDays(los);
                Session["SearchLOS"] = null;
            }

            AddBackgroundImage(model);
            DebugUtils.EndLogEvent("HomeController.Search");

            return View(model);
        }

        public ActionResult ShowFlexResort()
        {
            DebugUtils.StartLogEvent("HomeController.ShowFlexResort");
            if (Session["OwnerHomeResort"] != null)
            {
                string resortId = Session["OwnerHomeResort"].ToString();
                var resort = ResortManager.FindResort(FormatUtils.ConvertStringToInt(resortId));
                if (resort != null)
                {
                    Server.TransferRequest(SitecoreUtils.GetPageUrl(resort.InnerItem));
                }
                DebugUtils.EndLogEvent("HomeController.ShowFlexResort");
            }
            return null;
        }

        [HttpPost]
        public ActionResult DoSearch(SearchParameters tmpSearch)
        {
            DebugUtils.StartLogEvent("HomeController.DoSearch");


            if (string.IsNullOrEmpty(tmpSearch.ReservationType))
            {
                tmpSearch.ReservationType = "1";
            }
            if (tmpSearch.ReservationType == "2" && !string.IsNullOrEmpty(tmpSearch.DestinationBonusTime))
            {
                tmpSearch.Destination = tmpSearch.DestinationBonusTime;
            }

            if (tmpSearch.Destination != null && !tmpSearch.Destination.ToLower().Contains("all-"))
            {
                tmpSearch.CheckInDate = DateTime.MinValue;
                tmpSearch.CheckOutDate = DateTime.MinValue;
            }
            SearchParametersManager searchParametersManager = new SearchParametersManager(false);

            searchParametersManager.parameter = tmpSearch;

            searchParametersManager.SaveParameters();

            DebugUtils.EndLogEvent("HomeController.DoSearch");
            if (tmpSearch.Destination == null)
            {
                return null;
            }
            else
            {
                return Redirect(SitecoreUtils.GetPageUrl(SitecoreItemReferences.SearchResultPage));
            }

        }

        private void AddBackgroundImage(IndexSearch model)
        {
            model.UseBackgroundImageUrl = ResortUtils.GetResortImageBackground();
            model.BackgroundImageCaption = ResortUtils.GetResortBackgroundCaption();

            if (string.IsNullOrEmpty(model.UseBackgroundImageUrl))
            {
                if (model.RandomBackgroundImageList != null && model.RandomBackgroundImageList.Count() > 0)
                {
                    Random rnd = new Random();
                    int imageCount = rnd.Next(model.RandomBackgroundImageList.Count());
                    var bgImage = model.RandomBackgroundImageList.ElementAt(imageCount);
                    model.UseBackgroundImageUrl = bgImage.ImageFullUrl();
                    model.BackgroundImageCaption = bgImage.BuildResortCaption();
                }
            }
        }
       
    }
}