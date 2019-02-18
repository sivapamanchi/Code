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
    public class OurResortsController : RenderMvcController
    {
        public ActionResult OurResorts(OurResortsModel resorts)
        {
            ValidateSession();
            if (Request.QueryString["ResortID"] != null)
            {
                dynamic resortsContent = null;
                int ourResortsContentId;
                string ourResortsContentIdString = ConfigurationManager.AppSettings["ourResortsContentId"];
                if (!string.IsNullOrWhiteSpace(ourResortsContentIdString) && Int32.TryParse(ourResortsContentIdString, out ourResortsContentId))
                {
                    resortsContent = Umbraco.TypedContent(ourResortsContentId);
                }

                //Populate the image based on the resorts.
                if (resortsContent != null)
                {
                    string resortId = Request.QueryString["ResortId"];
                    foreach (IPublishedContent content in resortsContent.Children)
                    {
                        if (content.DocumentTypeAlias.Equals("Resort"))
                        {
                            if (resortId == content.GetPropertyValue<string>("DatabaseId"))
                            {
                                string path = BGModern.HtmlExtensions.CustomHtmlHelpers.GetParentSitePath(null).ToString();
                                 return Redirect(path + content.Url);
                            }
                        }
                    }
                }
            }

            //Logic for checking side navigation visibility
            BGO.OwnerWS.Owner BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            bool vacationClubOwner = false;
            bool vacationClubOrSampler = false;
            if (Session["OwnerContractType"] != null && (Session["OwnerContractType"].ToString() == "Vacation Club" ||
                Session["OwnerContractType"].ToString() == "Sampler"))
            {
                vacationClubOrSampler = true;
                if (BXGOwner != null && BXGOwner.User != null && !BXGOwner.User[0].isSampler && Session["OwnerContractType"].ToString() != "Sampler")
                {
                    vacationClubOwner = true;
            }
            }

            Boolean showAll = false;
            int pageNumber = 1;
            OurResortsModel ourResorts = null;
            if (resorts == null || resorts.Page == 0)
            {
                pageNumber = 1;
            }
            else 
            {
                if (resorts.Page == -1)
                {
                    showAll = true;
                }
                else
                {
                    pageNumber = resorts.Page;
                }
            }

            if (showAll)
            {
                ourResorts = OurResortsMapper.Map(CurrentPage);
                ourResorts.VacationClubOwner = vacationClubOwner;
                ourResorts.VacationClubOrSamplerOwner = vacationClubOrSampler;
                ourResorts.PagerModel = null;
                

                var children = CurrentPage.Children;

                if (!string.IsNullOrWhiteSpace(resorts.FilterCity) && !string.IsNullOrWhiteSpace(resorts.FilterState))
                {
                    resorts.FilterCity = resorts.FilterCity.ToUpper();
                    resorts.FilterState = resorts.FilterState.ToUpper();
                    children = children.Where(x => x.GetPropertyValue<string>("city").Trim().ToUpper() == resorts.FilterCity && x.GetPropertyValue<string>("state").Trim().ToUpper() == resorts.FilterState);
                    ourResorts.FilterCity = resorts.FilterCity;
                    ourResorts.FilterState = resorts.FilterState;
                }

                if (!string.IsNullOrWhiteSpace(resorts.FilterExperience))
                {
                    var experienceList = resorts.FilterExperience.Split(',');
                    children = children.Where(x => x.GetPropertyValue<string>("experience") != null && x.GetPropertyValue<string>("experience")
                        .Split(',')
                        .Intersect(experienceList)
                        .Any());
                    ourResorts.FilterExperience = resorts.FilterExperience;
                }

                foreach (IPublishedContent child in children)
                {
                    ResortModel resort = ResortMapper.Map(child);

                    if (resort != null)
                    {
                        ourResorts.OurResorts.Add(resort);
                    }
                }
            }
            else
            {
                ourResorts = OurResortsMapper.MapWithPaging(CurrentPage, pageNumber, resorts.FilterCity, resorts.FilterState, resorts.FilterExperience);
                ourResorts.VacationClubOwner = vacationClubOwner;
                ourResorts.VacationClubOrSamplerOwner = vacationClubOrSampler;

                if (ourResorts.ResortCount == 1 && ((resorts.FilterCity != null && resorts.FilterState != null) || resorts.FilterExperience != null))
                {
                    string url = ourResorts.OurResorts[0].Url;
                    string path = BGModern.HtmlExtensions.CustomHtmlHelpers.GetParentSitePath(null).ToString();
                    return Redirect(path + url);
                }
            }
            string CurrentURL = Request.CurrentExecutionFilePath;
            if (CurrentURL == "/BGModern/our-resorts")
            {
                Response.Redirect(ConfigurationManager.AppSettings["OurResortsToSiteCore"], true);
            }
            // return a view
            return View("OurResorts", ourResorts);
        }

        public ActionResult OurPagedResorts(int pageNumber)
        {
            ValidateSession();
            OurResortsModel ourResorts = OurResortsMapper.Map(CurrentPage);

            dynamic resorts = null;
            int resortContentId;
            string resortContentIdString = System.Configuration.ConfigurationManager.AppSettings["ourResortsContentId"];
            if (!string.IsNullOrWhiteSpace(resortContentIdString) && Int32.TryParse(resortContentIdString, out resortContentId))
            {
                resorts = Umbraco.Content(resortContentId);
            }

            int resortCount = 0;
            int pageSize = String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["ourResortsListPageSize"]) == true ? 6 : Convert.ToInt32(ConfigurationManager.AppSettings["ourResortsListPageSize"]);
            int resortNumberStart = (pageNumber * pageSize);
            int resortNumberStop = resortNumberStart + pageSize - 1;

            if (resorts != null)
            {
                // loop through the children increment the resort count
                foreach (IPublishedContent child in resorts)
                {
                    // we will increment the counter for every resort (for the sake of the resort pager), but will only create and add ResortModels to OurResortModel for the page to be displayed.
                    resortCount++;

                    if (resortCount >= resortNumberStart)
                    {
                        if (resortCount <= resortNumberStop)
                        {
                            ResortModel resort = ResortMapper.Map(child);

                            if (resort != null)
                            {
                                ourResorts.OurResorts.Add(resort);
                            }
                        }
                    }
                }
            }

            if (ourResorts.ResortCount > 0)
            {
                ourResorts.PagerModel = new OurResortsPagerModel();
                ourResorts.PagerModel.CurrentPage = pageNumber;
                ourResorts.PagerModel.PageCount = ourResorts.ResortCount / pageSize;
                if (ourResorts.ResortCount % pageSize > 0)
                {
                    ourResorts.PagerModel.PageCount++;
                }
            }

            return View("OurResorts", ourResorts);
        }

        private void ValidateSession()
        {
            if (Session["BXGOwner"] == null)
            {
                if (Session["_path_info"] != null)
                    Session["_path_info"] = Request.RawUrl;

                Response.Redirect(ConfigurationManager.AppSettings["bxgwebUnsecureURL"] + "default.aspx?sess=timeout", true);
            }
        }
    }
}