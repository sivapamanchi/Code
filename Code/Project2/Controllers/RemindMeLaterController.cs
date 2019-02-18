using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BGModern.Classes.Utilities;
using BGModern.Models;
using BGO.OwnerWS;
using BGModern.Classes.TravelerPlus.owner;

namespace BGModern.Controllers
{
    public class RemindMeLaterController : Umbraco.Web.Mvc.SurfaceController
    {
        //private CreditCardInfoModel mCreditCardModel;

        private BGO.OwnerWS.Owner BXGOwner = new BGO.OwnerWS.Owner();
        private BGO.OwnerWS.OwnerWS1SoapClient OwnerService = new BGO.OwnerWS.OwnerWS1SoapClient();
              

        [HttpGet]
        public ActionResult RemindMeLater()
        {
            throw new NotImplementedException("Index is not implemented for RemindMeLaterController");
        }

        public ActionResult GetPartialView()
        {
            //mCreditCardModel = new CreditCardInfoModel()
            //TODO:  Web Service populate of Owner
            BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
  
            return PartialView("RemindMeLater");
        }

        public ActionResult RemindMeLaterClick(string Command)
        {
            BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            if(ModelState.IsValid)
            {
                //redirect to current page to clear the form
                TempData.Add("DisplayReminderButtons", false);
                RenewalMsgPopup(Constants.SavePointsRemindme);
                string path = BGModern.HtmlExtensions.CustomHtmlHelpers.GetFullSitePath(null).ToString();
                return Redirect(path + "/Home");
            }
            else
            {
                //Add Error processing here
                return CurrentUmbracoPage();
            }
        }

        public ActionResult DontRemindMeClick()
        {
            BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            if (ModelState.IsValid)
            {
                //redirect to current page to clear the form
                RenewalMsgPopup(Constants.SavePointsDTRemindme);
                string path = BGModern.HtmlExtensions.CustomHtmlHelpers.GetFullSitePath(null).ToString();
                return Redirect(path + "/Home");
            }
            else
            {
                //Add Error processing here
                return CurrentUmbracoPage();
            }

        }

        private void RenewalMsgPopup(int opt)
        {
            try
            {
                BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
                string MSGRDATE = BXGOwner.AnnualPointsExpiration.SavepointsPopNextDate.ToString();

                TravelerPlusEligibility tp = new TravelerPlusEligibility();
                tp.TPARVACT = BXGOwner.Arvact;
                tp.TPMsgId = " ";
                tp.TPNextPopDate = MSGRDATE;
                tp.TPMsgId = BXGOwner.AnnualPointsExpiration.SavepointsMessageId;
                TravelerPlusRenewDB tprenewdb = new TravelerPlusRenewDB();
                bool processed = tprenewdb.UpdatePopUpMenuOption(tp, opt);

            }
            catch (Exception ex)
            {
            }
        }
    }
}