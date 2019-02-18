
using BGSitecore.Components;
using BGSitecore.Models;
using BGSitecore.Models.Resort;
using BGSitecore.Utils;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Device.Location;
using Newtonsoft.Json;
using System.Text;

namespace BGSitecore.Controllers
{
    public class ResortController : GlassController
    {


        // GET: ResortLocator
        public ActionResult Resort()
        {
            DebugUtils.StartLogEvent("ResortController.Resort");

            var model = GetLayoutItem<ResortDetails>();

            //Save background image URL to be used on the home page
            if (model.HomePageBackgroundImage != null)
            {
                ResortUtils.SaveResortImageBackground(model.HomePageBackgroundImage.ImageFullUrl());
                ResortUtils.SaveResortBackgroundCaption(model.HomePageBackgroundImage.BuildResortCaption());
            }
            DebugUtils.EndLogEvent("ResortController.Resort");
            return View(model);
        }

        // GET: ResortLocator
        public ActionResult ResortSection()
        {
            DebugUtils.StartLogEvent("ResortController.ResortSection");
            var model = GetLayoutItem<ResortSections>();

            foreach (Models.Resort.Section section in model.AllSections)
            {

                bool showUsingRules = true;

               
                if (section.InnerItem.Fields["{85B5A4F7-0974-4455-85FA-B33FFAF397E0}"] != null)
                {
                    String rule = section.InnerItem.Fields["{85B5A4F7-0974-4455-85FA-B33FFAF397E0}"].Value;
                    if (!string.IsNullOrEmpty(rule))
                    {
                        var rules = RuleFactory.ParseRules<RuleContext>(section.InnerItem.Database, XElement.Parse(rule));
                        var ruleContext = new RuleContext()
                        {
                            Item = section.InnerItem
                        };

                        if (rule.Any() && rules.Rules.Count() > 0)
                        {
                            showUsingRules = rules.Rules.First().Evaluate(ruleContext);
                        }
                    }
                }
                section.HideSection = !showUsingRules;
                DebugUtils.EndLogEvent("ResortController.ResortSection");


            }

            return View(model);
        }
        public ActionResult NearbyResort()
        {
            DebugUtils.StartLogEvent("ResortController.NearbyResort");
            var soureceResort = GetContextItem<ResortDetails>();

            var allREsorts = ResortManager.GetAllResorts();

            var coord = soureceResort.resortGeoCoordinate;

            var nearest = allREsorts.OrderBy(x => x.resortGeoCoordinate.GetDistanceTo(coord));

            var model = GetLayoutItem<NearbyResortList>();
            if (model == null)
            {
                model = new NearbyResortList();
            }
            model.Resort = new List<ResortDetails>();

            
            foreach (var item in nearest)
            {
                if (item.ResortId != soureceResort.ResortId)
                {
                    item.Distance = Convert.ToInt32(coord.GetDistanceTo(item.resortGeoCoordinate)/1000);  //convert meter to KM
                    ((List<ResortDetails>)model.Resort).Add(item);

                    if (model.Resort.Count() >= model.NumberToDisplay)
                    {
                        break;
                    }
                }
            }
            DebugUtils.EndLogEvent("ResortController.NearbyResort");

            return View(model);
        }

        
        public ActionResult Explore()
        {
            DebugUtils.StartLogEvent("ResortController.Explore");

            var model = GetLayoutItem<ExploreBluegreen>();
            model.AllResorts = ResortManager.GetAllResorts();
            model.AllExperience = ResortManager.GetAllExperience();
            DebugUtils.EndLogEvent("ResortController.Explore");

            return View(model);
        }

        public ActionResult Book()
        {
            DebugUtils.StartLogEvent("ResortController.Book");

            var model = GetLayoutItem<ResortDetails>();

            DebugUtils.EndLogEvent("ResortController.Book");

            return View(model);
        }

        public ActionResult Reviews()
        {
            DebugUtils.StartLogEvent("ResortController.Reviews");

            var model = GetLayoutItem<ResortDetails>();
            DebugUtils.EndLogEvent("ResortController.Reviews");

            return View(model);
        }

        public ActionResult IdealGetaway()
        {
            DebugUtils.StartLogEvent("ResortController.IdealGetaway");

            var soureceResort = GetContextItem<ResortDetails>();

            if (Request.RawUrl.ToLower() == "/home" && soureceResort.ShowGatewayWidget)  //TODO find a better way to know if the user is looking at resort detail from the home page
            {
                var model = GetLayoutItem<Wysiwyg>();
                DebugUtils.EndLogEvent("ResortController.IdealGetaway");

                return View(model);
            }
            else
            {
                DebugUtils.EndLogEvent("ResortController.IdealGetaway");

                return new EmptyResult();
            }
        }

        public ActionResult Search()
        {
            DebugUtils.StartLogEvent("ResortController.Search");

            if (Request.RawUrl.ToLower() != "/home")
            {
                SearchParametersManager searchParametersManager = new SearchParametersManager();

                var model = GetLayoutItem<SearchResult>();
                SearchUtils.ValidateSearchOptions(model);  //Removes any Resorts the user doesn't have access
                model.SavedSearches = SearchUtils.GetSavedSearches();

                var resortInfo = GetLayoutItem<ResortDetails>();
                if ((model.AllResorts.FindIndex(m => m.ResortId == resortInfo.ResortId) != -1))
                    {
                    model.searchParameters.ResortId = resortInfo.ResortId.ToString();
                    model.CurrentResortName = resortInfo.ResortName;
                    searchParametersManager.parameter.Destination = "city-" + resortInfo.City;
                }
                DebugUtils.EndLogEvent("ResortController.Search");
                return View(model);
            }
            DebugUtils.EndLogEvent("ResortController.Search");

            return new EmptyResult();
        }


        /// <summary>
        /// This method returns a JSON representing the Resort based on supplied project number and unit type
        /// Sample Call: http://sc.bluegreenowner.com/api/sitecore/resort/GetResortInfo?pn=87&ut=2
        /// </summary>
        /// <returns></returns>
        /// 
        public string GetResortInfo()
        {
            DebugUtils.StartLogEvent("ResortController.GetResortInfo");

            StringBuilder result = new StringBuilder();
            string pn = Request.QueryString["pn"];
            string ut = Request.QueryString["ut"];

            result.Append("{");
            var resort = ResortManager.GetResortByProject(pn);
            if (resort != null)
            {
                string image = (resort.MainResortImage != null) ? resort.MainResortImage.ImageFullUrl() : "";
                result.AppendFormat("\"resortId\":\"{0}\"", resort.ResortId);
                result.AppendFormat(",\"resortName\":\"{0}\"", StringUtils.ClearDoubleQuotes(resort.ResortName));
                result.AppendFormat(",\"resortDetails\":\"{0}\"", StringUtils.ClearDoubleQuotes(resort.ResortSummary));
                result.AppendFormat(",\"address\":\"{0} {1}\"", resort.AddressLine1, (!string.IsNullOrEmpty(resort.AddressLine2)) ? "," + resort.AddressLine2 : "");
                result.AppendFormat(",\"city\":\"{0}\"", resort.City);
                result.AppendFormat(",\"state\":\"{0}\"", resort.State.DisplayName);
                result.AppendFormat(",\"zip\":\"{0}\"", resort.ZipCode);
                result.AppendFormat(",\"phone\":\"{0}\"", resort.PhoneNumber);
                result.AppendFormat(",\"imagePath\":\"{0}\"", image);
                result.AppendFormat(",\"resortDetailsUrl\":\"http://{0}{1}\"", Request.Url.Host, resort.Url);
                result.AppendFormat(",\"taxRate\":\"{0}\"", resort.TaxRate);

                if (resort.ImportantNoteList != null && resort.ImportantNoteList.Count() > 0)
                {
                    foreach (ImportantNote item in resort.ImportantNoteList)
                    {
                        result.AppendFormat(",\"{0}\":\"{1}\"", StringUtils.JsonAttribute(item.InnerItem.Name), StringUtils.JsonContent(item.NotesContent));
                    }
                }
            }

            var room = ResortManager.GetRoom(pn, ut);
            if (room != null)
            {
                result.AppendFormat(",\"vilaSize\":\"{0}\"", StringUtils.ClearDoubleQuotes(room.RoomDescription));
                result.AppendFormat(",\"ROOM_TYPE\":\"{0}\"", room.ViewTitle);
                //result.AppendFormat(",\"cancelPolicy\":\"{0}\",", room.C);
                //result.AppendFormat(",\"checkInTime\":\"{0}\",", resort.C);
                //result.AppendFormat(",\"checkOutTime\":\"{0}\",", resort.C);
                //result.AppendFormat(",\"officeHours\":\"{0}\",", resort.C);
                //result.AppendFormat(",\"lateCheckInProcedure\":\"{0}\",", resort.C);
                //result.AppendFormat(",\"feeDetails\":\"{0}\",", resort.C);
                //result.AppendFormat(",\"constructionInfo\":\"{0}\",", resort.C);
                //result.AppendFormat(",\"creditCard\":\"{0}\",", resort.C);
            }

            result.Append("}");
            DebugUtils.EndLogEvent("ResortController.GetResortInfo");

            return result.ToString();
        }
    }
}