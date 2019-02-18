using BGModern.Models;
using BGO;
using BGO.OwnerWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace BGModern.Controllers
{
    public class DynamicSpecialsController : Umbraco.Web.Mvc.SurfaceController
    {

        public Owner BXGOwner;

        // GET: DynamicSpecials
        public ActionResult GetDynamicSpecials()
        {
            DynamicSpecialsModel model = new DynamicSpecialsModel();
            BXGOwner = (Owner)Session["BXGOwner"];

            if (Session["BonusTimeEnabled"] != null)
                model.BonusTimeEnabled = Session["BonusTimeEnabled"].ToString();
            else
                model.BonusTimeEnabled = "FALSE";

            if (Session["IsTravelerPlusEligible"] != null)
                model.TravelerPlusEligible = Session["IsTravelerPlusEligible"].ToString();
            else
                model.TravelerPlusEligible = "FALSE";

            if (Session["siteNavjs"] != null)
                model.SiteNavJS = Session["siteNavjs"].ToString();
            else
                model.SiteNavJS = "";

            if (Session["OwnerContractdate"] != null)
                model.OwnerContractDate = Session["OwnerContractdate"].ToString();
            else
                model.OwnerContractDate = "";

            model.OwnerContractType = Session["OwnerContractType"].ToString();

            model.TPLevel = BXGOwner.TravelerPlusMembership.TPLevel;
            bool eligible4K = BXGOwner.Eligible4kFlagged;

            if (BXGOwner.Eligible4kFlagged && BXGOwner.Eligible4kOnAnniversaryDateWindow && !BXGOwner.Eligible4kRequested)
                model.Show4K = true;
            else
                model.Show4K = false;

            try
            {
                string pageName = String.Empty;
                string url = Request.ServerVariables["URL"].ToString();

                for (int x = 0; x < url.Length; x++)
                {
                    pageName += url.Substring(x, 1);
                    if (url.Substring(x, 1) == "/")
                        pageName = String.Empty;
                }

                if ("home|homefixed|calcPoints.aspx|reservRequestSent.aspx|payAcctBal.aspx|payConfirm.aspx|payInfo.aspx|payNotConfirm.aspx|gyc.aspx|ownerAccount.aspx|ownerAcctConfirm.aspx|points4k.aspx|points4kConfirm.aspx".ToLower().IndexOf(pageName.ToLower()) >= 0)
                {
                    if (model.OwnerContractType == "Vacation Club")
                        model.VacationGuardPath = "Vacation_Guard_banner2.gif";
                    else
                        model.VacationGuardPath = "Vacation_Guard_banner.gif";

                    model.ShowVG = true;
                }
                else
                    model.ShowVG = false;
            }
            catch (Exception ex)
            {
                //No handling, just don't show vacation guard banner if there was a problem
                model.ShowVG = false;
            }

            string protocol = "";

            if (Request.ServerVariables["SERVER_PORT_SECURE"] != null)
                protocol = Request.ServerVariables["SERVER_PORT_SECURE"].ToString();

            if (protocol != String.Empty && protocol != "1")
                protocol = "http://";
            else
                protocol = "https://";

            model.ServiceRoot = protocol + Request.ServerVariables["SERVER_NAME"].ToString();

            return PartialView("DynamicSpecials", model);
        }
    }
}