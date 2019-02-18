using BGModern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class SiteHeaderController : SurfaceController
    {
        private SiteHeaderModel model;
        private BGO.OwnerWS.Owner bxgOwner;

        // GET: SiteHeader
        public ActionResult Index()
        {
            throw new NotImplementedException("Index is not implemented for SiteHeaderController");
        }

        public ActionResult GetPartialView()
        {
            model = new SiteHeaderModel();
            HydrateModel(model);
            return PartialView("SiteHeader", model);
        }

        private void HydrateModel(SiteHeaderModel model)
        {
            if (Session["IsFixedFlexOrTraditionalOwner"] is bool)
            {
                model.IsFixedFlexOrTraditionalOwner = (bool)Session["IsFixedFlexOrTraditionalOwner"];
            }
            else
            {
                model.IsFixedFlexOrTraditionalOwner = false;
            }

            IdentifyNavigatorType();

            string phoneURL = "https://bluegreenvacations.force.com/CustomerCommunity/s/";
            if (System.Configuration.ConfigurationManager.AppSettings["AskQuestionURL"] != null)
                phoneURL = System.Configuration.ConfigurationManager.AppSettings["AskQuestionURL"];

            switch (model.NavigatorType)
            {
                case "owner":
                    model.PhoneNumberImage = "bgo-ask-question-animation.gif";
                    model.PhoneNumberAltText = "Phone us at 800.456.2582";
                    model.PhoneNumberURL = phoneURL;
                    break;

                case "Sampler":
                    model.PhoneNumberImage = "tpphone.gif";
                    model.PhoneNumberAltText = "Phone us at 800.459.1597";
                    model.PhoneNumberURL = "";
                    break;

                case "Travelerplus":
                    model.PhoneNumberImage = "bgo-ask-question-animation.gif";
                    model.PhoneNumberAltText = "Phone us at 800.456.2582";
                    model.PhoneNumberURL = phoneURL;
                    break;

                case "Fixed":
                    model.PhoneNumberImage = "phoneFixed.gif";
                    model.PhoneNumberAltText = "Phone us at 800.688.9889";
                    model.PhoneNumberURL = "";
                    break;

                case "Pending":
                    model.PhoneNumberImage = "bgo-ask-question-animation.gif";
                    model.PhoneNumberAltText = "Phone us at 800.456.2582";
                    model.PhoneNumberURL = phoneURL;
                    break;

                default:
                    model.PhoneNumberImage = "phone.gif";
                    model.PhoneNumberAltText = "Phone us at 800.456.2582";
                    model.PhoneNumberURL = "";
                    break;
            }
        }

        private void IdentifyNavigatorType()
        {
            bxgOwner = (BGO.OwnerWS.Owner)HttpContext.Session["BXGOwner"];
            
            if (bxgOwner.User[0].HomeProject !=null)
            {
                if (bxgOwner.User[0].HomeProject == "52")
                {
                    model.NavigatorType = "Sampler24";
                }
            }
            model.ContractType = Session["OwnerContractType"].ToString();

            if (Server.MapPath("").ToLower().EndsWith("owner"))
            {
                model.NavigatorType = "owner";
            }

            if (model.ContractType == "Vacation Club")
            {
                model.NavigatorType = "owner";
            }

            if (model.ContractType == "Sampler")
            {
                model.NavigatorType = "Sampler";
            }

            if (model.IsFixedFlexOrTraditionalOwner || Session["siteNavjs"] != null && Session["siteNavjs"].ToString() == "ownerNVC_data")
            {
                model.NavigatorType = "Fixed";
            }

            if ((Session["IsTravelerPlusEmployee"] != null && (Session["IsTravelerPlusEmployee"].ToString() == "TRUE") ||
                (HttpContext.Session["IsTravelerPlusOwner"] != null && HttpContext.Session["IsTravelerPlusOwner"].ToString() == "TRUE") ||
                (HttpContext.Session["IsTravelerPlusEligible"] != null && HttpContext.Session["IsTravelerPlusEligible"].ToString() == "TRUE")) && model.ContractType == "Vacation Club")
            {
                model.NavigatorType = "Travelerplus";
            }

            if (Session["PendingOwner"] != null && Session["PendingOwner"].ToString() == "TRUE")
            {
                model.NavigatorType = "Pending";
            }
        }
    }
}