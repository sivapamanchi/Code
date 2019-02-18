using System;
using BGSitecore.Models;
using Glass.Mapper.Sc;
using System.Web.Mvc;
using BGSitecore.Components;
using BGSitecore.Services;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Data.Fields;
using Sitecore.Data;

namespace BGSitecore.Controllers
{
    public class ArticleFeedbackController : GlassController
    {
        // GET: ArticleFeedback

        public ActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Feedback(ArticleRating Articlerating)
        {
            JsonResponse jsonResponse = new JsonResponse();
            var context = new SitecoreContext();
            
          
            bool success = false;
            try
            {
                Articlerating.FeedbackSaved = false;
                Sitecore.Data.Items.Item FAQItem = context.Database.GetItem((new ID(new Guid(Articlerating.FeedbackReference))));
                BlueGreenContext bgContext = new BlueGreenContext();
                if (bgContext != null && bgContext.bxgOwner != null)
                {
                    Articlerating.UserReference = bgContext.bxgOwner.Email;
                    Articlerating.UserName = bgContext.bxgOwner.fullName;
                    Articlerating.UserType = bgContext.GetOwnerType();
                }

                Articlerating.ArticleTitle = FAQItem["Title"];
                Articlerating.ArticlePath = FAQItem.Paths.GetPath(Sitecore.Data.Items.ItemPathType.DisplayName);

                success = DBManager.InsertArticleFeedback(Articlerating);
                
                if (success)
                {
                    jsonResponse.RetCode = "0";
                }
            }
            catch (Exception Ex)
            {
                jsonResponse.RetCode = "1";
                jsonResponse.errors.Add("Error while saving the data, please Try later");
                return Json(jsonResponse, JsonRequestBehavior.AllowGet);
            }
            return Json(jsonResponse, JsonRequestBehavior.AllowGet);
        }
        
    }
}