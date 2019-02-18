using BGSitecore.Components;
using BGSitecore.Models;
using BGSitecore.Models.OwnerService.OwnerPointsResponse;
using BGSitecore.OwnerService;
using BGSitecore.Services;
using BGSitecore.Utils;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;


namespace BGSitecore.Controllers
{
 
    public class OwnerController : GlassController
    {
        public ActionResult Wysiwyg()
        {
            var model = GetLayoutItem<Wysiwyg>();
            if (SitecoreUtils.EvaluateRules(model.RestrictionRule, model.InnerItem))
            {
                return View(model);
            }
            else
            {
                return new EmptyResult();
            }
        }

        public ActionResult WysiwygWithForm()
        {
            var model = GetLayoutItem<Wysiwyg>();
            if (SitecoreUtils.EvaluateRules(model.RestrictionRule, model.InnerItem))
            {
                model.ControllerName = RenderingContext.Current.Rendering.Parameters["Controller"];
                model.ActionName = RenderingContext.Current.Rendering.Parameters["Action"];
                return View(model);
            }
            else
            {
                return new EmptyResult();
            }
        }



        public ActionResult Section()
        {
            DebugUtils.StartLogEvent("OwnerController.Section");

            var model = new Section();
            OwnerSectionCache ownerSectionCache = new OwnerSectionCache();

            if (Request.QueryString["SectionType"] != null)
            {
                var resultViews = new List<KeyValuePair<string, string>>();
                var lazyLoadSectionId = Request.QueryString["SectionType"].ToString();
                var sectionIds = lazyLoadSectionId.Split(',');
                foreach (var id in sectionIds)
                {
                    var context = new SitecoreContext();
                    model = context.GetItem<Section>(id);
                    if (model != null)
                    {
                        model.LazyLoadContent = false;
                        model.showSectionTitle = false;
                        model = LoadSubSection(model, ownerSectionCache);

                        if (!model.HideSection)

                        {
                            var totalValuesPartialView = RenderRazorViewToString(this.ControllerContext, "section", model);
                            resultViews.Add(new KeyValuePair<string, string>(id, totalValuesPartialView));
                        }
                    }
                }
                DebugUtils.EndLogEvent("OwnerController.Section");
                return Json(resultViews, JsonRequestBehavior.AllowGet);
            }
            else
            {
                model = GetLayoutItem<Section>();
                model.showSectionTitle = true;
                model = LoadSubSection(model, ownerSectionCache);

                DebugUtils.EndLogEvent("OwnerController.Section");
                if (model.HideSection)
                {
                    return new EmptyResult();
                }
                else
                {
                    return View(model);
                }
            }

        }

        private Section LoadSubSection(Section model, OwnerSectionCache ownerSectionCache)
        {
            //Evaluate if this section should be displayed
            if (SitecoreUtils.EvaluateRules(model.RestrictionRule, model.InnerItem))
            {
                model.HideSection = false;

                var context = new SitecoreContext();
                var homePage = context.GetHomeItem<BasePage>();

                BlueGreenContext bgContext = new BlueGreenContext();
                string ownerId = bgContext.OwnerId;
                if (model.LazyLoadContent)
                {

                }
                else
                {
                    OwnerUtils.GetSectionTableData(ref model, ownerId, homePage.SiteSettings, ownerSectionCache);
                }
            }
            else
            {
                model.HideSection = true; 
            }

            return model;
        }

        public static string RenderRazorViewToString(ControllerContext controllerContext,    string viewName, object model)
        {
            controllerContext.Controller.ViewData.Model = model;

            using (var stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var viewContext = new ViewContext(controllerContext, viewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        public ActionResult Links()
        {
            var model = GetLayoutItem<LinkCollection>();
            return View(model);
        }

        public ActionResult Points()
        {
            DebugUtils.StartLogEvent("OwnerController.Points");

            var model = GetLayoutItem<PointsSummary>();
            model = GetPointsSummaryData(model);
            DebugUtils.EndLogEvent("OwnerController.Points");
            return View(model);
        }

        public ActionResult PaymentInfo()
        {
            return View();
        }


        private PointsSummary GetPointsSummaryData(PointsSummary model) {

            DebugUtils.StartLogEvent("OwnerController.GetPointsSummaryData");
            BlueGreenContext bgContext = new BlueGreenContext();
            string ownerId = bgContext.OwnerId; 

            model.AnnualPoints = "0";
            model.SavedPoints = "0";
            model.RestrictedPoints = "0";
            model.AvailablePoints = "0";
            try
            {
                ProfileService service = new ProfileService();
                RestOwnerPointsResponse result = service.GetOwnerPoints(ownerId, null);

                if (result != null && result.Account != null && result.Account.Memberships != null && result.Account.Memberships.VacationClubMembership != null && result.Account.Memberships.VacationClubMembership.Points != null)
                {
                    model.AnnualPoints = FormatUtils.FormatPoints(result.Account.Memberships.VacationClubMembership.Points.TotalAnnualPoints);
                    model.SavedPoints = FormatUtils.FormatPoints(result.Account.Memberships.VacationClubMembership.Points.TotalSavedPoints);
                    model.RestrictedPoints = FormatUtils.FormatPoints(result.Account.Memberships.VacationClubMembership.Points.TotalRestrictedPoints);
                    model.AvailablePoints = FormatUtils.FormatPoints(result.Account.Memberships.VacationClubMembership.Points.TotalPoints);
                }

            } catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Unexpected exception retreiving points for "+ownerId, ex, bgContext);
            }
            DebugUtils.EndLogEvent("OwnerController.GetPointsSummaryData");


            return model;
        }

    }
}