using BGModern.Classes.Utilities;
using BGModern.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace BGModern.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    public class CreditCardInfoController : Umbraco.Web.Mvc.SurfaceController
    {
        private bool cardSuccess = false;
        private CreditCardInfoModel mCreditCardModel;

        private BGO.OwnerWS.Owner BXGOwner = new BGO.OwnerWS.Owner();
        private BGO.OwnerWS.OwnerWS1SoapClient OwnerService = new BGO.OwnerWS.OwnerWS1SoapClient();
        private enum CardTypes
        {
            V,
            M,
            D,
            A
        }
       
   
        //private List<RuleViolation> mRuleViolations;

        [HttpGet]
        public ActionResult CreditCardInfo()
        {
            throw new NotImplementedException("Index is not implemented for CreditCardInfoController");
        }

        public ActionResult GetPartialView()
        {
            //mCreditCardModel = new CreditCardInfoModel()
            //TODO:  Web Service populate of Owner
            BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            CreditCardInfoModel model = new CreditCardInfoModel();
            HydrateModel(model);
            TempData["CCFormRequestFromMyPoints"] = CurrentPage.Id == Convert.ToInt32(ConfigurationManager.AppSettings["myPointsContentId"]);
            return PartialView("CreditCardInfo", model);
        }

        [HttpPost]
        public ActionResult SubmitForm(CreditCardInfoModel model)
        {
            BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            mCreditCardModel = model;
            mCreditCardModel.CreditCardInfoErrors = new List<String>();
            ValidateForm(mCreditCardModel);
            if(ModelState.IsValid)
            {
                //redirect to current page to clear the form
                SavePoints();
                return RedirectToCurrentUmbracoPage();
            }
            else
            {
                TempData.Add("DisplaySaveMyPoints", true);
                //Add Error processing here
                return CurrentUmbracoPage();
            }
            
        }

        // Same as SubmitForm, but with the following changes so that we can use it with Ajax.BeginForm:
        //  * Returns the partial view with the model instead of redirecting to the current page
        //  * Populates the dropdown model properties so the rendere doesn't yell at us
        //  * Deletes the DisplaySaveMyPoints TempData key if it exists, since SubmitForm relies on the My Points page deleting it
        public ActionResult SubmitAjaxForm(CreditCardInfoModel model)
        {
            if (TempData.ContainsKey("DisplaySaveMyPoints"))
            {
                TempData.Remove("DisplaySaveMyPoints");
            }
            BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            mCreditCardModel = model;
            PopulateMonths();
            PopulateYears();
            PopulateCreditCardType();
            PopulatePrice();
            mCreditCardModel.CreditCardInfoErrors = new List<String>();

            ValidateForm(mCreditCardModel);

            if (ModelState.IsValid)
            {
                //redirect to current page to clear the form
                SavePoints();
            }
            else
            {
                TempData.Add("DisplaySaveMyPoints", true);
                //Add Error processing here
            }

            if (cardSuccess)
            {
                if (Session["CcformRedirectUrl"] != null)
                    ClearMessages();

                string path = (string)Session["CcformRedirectUrl"] ?? BGModern.HtmlExtensions.CustomHtmlHelpers.GetFullSitePath(null).ToString() + "/my-points";
                Session["CcformRedirectUrl"] = null;
                return Json(new{ url = path });
            }
            else
                return PartialView("CreditCardInfo", model);
        }


        private void HydrateModel(CreditCardInfoModel model)
        {
            // Month logic
            mCreditCardModel = model;

            PopulatePrice();
            PopulateMonths();
            PopulateYears();
            PopulateCreditCardType();
        }

        private void PopulateCreditCardType()
        {
            var selectItems = new Dictionary<string, string> { { "0", "Visa" }, { "1", "MasterCard" }, { "2", "Discover" }, { "3", "American Express"} };
            mCreditCardModel.lstCreditCardTypes = new SelectList(selectItems,"Key", "Value");
            
        }

        private void ClearMessages()
        { 
            if(TempData.ContainsKey("CCFormRequestFromMyPoints"))
                TempData.Remove("CCFormRequestFromMyPoints");

            if(TempData.ContainsKey("CreditCardStatusMessage"))
                TempData.Remove("CreditCardStatusMessage");
        }

        private void PopulateMonths()
        {
            mCreditCardModel.lstExpDateMonth = new List<SelectListItem>();
            for (int x = 1; x <= 12; x++)
            {
                SelectListItem item = new SelectListItem() { Text = x.ToString(), Value = x.ToString() };
                mCreditCardModel.lstExpDateMonth.Add(item);
            }
        }

        private void PopulateYears()
        {
            mCreditCardModel.lstExpDateYears = new List<SelectListItem>();
            for (int x = DateTime.Now.Year; x <= DateTime.Now.Year + 12; x++)
            {   
                SelectListItem item = new SelectListItem() {Text = x.ToString(), Value = x.ToString()};
                mCreditCardModel.lstExpDateYears.Add(item);
            }

        }

        private void PopulatePrice()
        {
           if(BXGOwner.AnnualPointsExpiration != null && BXGOwner.AnnualPointsExpiration.SavePointsFee != null)
                if (BXGOwner.AnnualPointsExpiration.SavePointsEligible == true && BXGOwner.AnnualPointsExpiration.SavePointsFee.Trim().Length > 1)
                {
                    mCreditCardModel.Payment = "$" + string.Format("{0:C}", BXGOwner.AnnualPointsExpiration.SavePointsFee) + " administrative fee.";
                }
        }

        private void SavePoints()
        {
            //FORM has already been validated
            ProcessSettlement();

            //LblError.Visible = false;
            //LblError.Text = "";
            //string cctype = "0";

            //cctype = model.CardType;
            //try
            //{

            //if (ValidateForm())
            //{
            //    ProcessSettlement();
            //}
            //   else
            //{
            //        savePointsSubmit.Enabled = true;
            //        LblError.Visible = true;
             //}
            //}
            //catch (Exception ex)
            //{
            //    savePointsSubmit.Enabled = true;
            //    LblError.Visible = true;
            //}
        }

        private void ProcessSettlement()
        {

            string expMonth = null;
            String ccStatusMessage;
            try
            {
                //BXGOwner = Session["BXGOwner"];
                expMonth = String.Format("{0:00}", mCreditCardModel.CreditCardExpDateMonth);

                BGO.savePointsWS.MDPNTWSPortTypeClient theSavingPoints = new BGO.savePointsWS.MDPNTWSPortTypeClient();
                BGO.savePointsWS.PNTSVELECTWSInput _elect = new BGO.savePointsWS.PNTSVELECTWSInput();
                BGO.savePointsWS.PNTSVELECTWSResult _electResult = new BGO.savePointsWS.PNTSVELECTWSResult();

                _elect._PNTSVELECTPR = new BGO.savePointsWS.PNTSVELECTDS();

                _elect._PNTSVELECTPR._AGENTID = BGModern.Classes.Utilities.Constants.WebAgentId;
                _elect._PNTSVELECTPR._NAMEONCARD = mCreditCardModel.CreditCardName;//txtbxCcname.Text;
                _elect._PNTSVELECTPR._CCEXP = expMonth.Trim() + mCreditCardModel.CreditCardExpDateYear.ToString().Substring(2, 2);//Strings.Trim(ddlCcyear.SelectedValue);
                _elect._PNTSVELECTPR._CCNUM = mCreditCardModel.CreditCardNumber;//txtbxCcnum.Text;
                //if (string.IsNullOrEmpty(mCreditCardModel.PaymentFromServiceCall))
                //{
                    _elect._PNTSVELECTPR._FEE = BXGOwner.AnnualPointsExpiration.SavePointsFee;
                //}
                //else
                //{
                //    _elect._PNTSVELECTPR._FEE = mCreditCardModel.PaymentFromServiceCall;
                //}
                _elect._PNTSVELECTPR._OWNERNUMBER = BXGOwner.Arvact;
                _electResult = theSavingPoints.pntsvelectws(_elect);


                if (_electResult._PNTSVELECTPR._RETCD == "0")
                {
                    //update owner object save points eligibility 
                    BXGOwner.AnnualPointsExpiration.SavePointsEligible = false;
                    BXGOwner.AnnualPointsExpiration.SavePointsFee = "0.00";
                    BXGOwner.AnnualPointsExpiration.SavePointsMessage = "";

                    //save owner object with updated annualpoints expiration
                    Session["BXGOwner"] = BXGOwner;

                    //TODO
                    //GetOwnerPoints();
                    string path = BGModern.HtmlExtensions.CustomHtmlHelpers.GetFullSitePath(null).ToString();
                    //ccStatusMessage = "<p style=\"line-height:25px; color:#090; font-size:14px;\"><img src=\"" + path + "/Content/Images/icon_statusSuccess.png\" width=\"25\" height=\"25\" alt=\"error\" style=\"float:left; margin-right:10px; \" /><strong>Your Payment Was Successful!</strong> Confirmation: " + _electResult._PNTSVELECTPR._AUTH + "</p>";
                    //ccStatusMessage = ccStatusMessage + "<p>The eligible Points in your account(s) will be saved for an additional year of use in Red, White and Blue seasons. Thank you for electing to save your Points. </p>";
                    ccStatusMessage = "<p>The eligible Points in your account(s) will be saved for an additional year of use in Red, White and Blue seasons. Thank you for electing to save your Points. </p>";
                    TempData.Add("CreditCardStatusMessage", ccStatusMessage);
                    
                    //spmessage.Text = "<p style=\"line-height:25px; color:#090; font-size:14px;\"><img src=\"images/icon_statusSuccess.png\" width=\"25\" height=\"25\" alt=\"error\" style=\"float:left; margin-right:10px; \" /><strong>Your Payment Was Successful!</strong> Confirmation: " + _electResult._PNTSVELECTPR._AUTH + "</p>";
                    //spmessage.Text = spmessage.Text + "<p>The eligible Points in your account(s) will be saved for an additional year of use in Red, White and Blue seasons. Thank you for electing to save your Points. </p>";

                    //reset points grid

                    //hide payment information
                    //TempData.Remove("DisplaySaveMyPoints");
                    TempData.Add("DisplaySaveMyPoints", false);
                    cardSuccess = true;
                    //PaymentInformation.Visible = false;
                    //tpRenewSaveMyPoints.Visible = false;
                    //Panel1.Visible = false;
                    //ccform.Visible = false;
                    //pnlRemind.Visible = false;
                }
                else
                {
                    //Panel1.Visible = true;
                    //LblError.Visible = true;
                    ccStatusMessage = "There was an error processing your request. Please contact Customer Service at 1.800.459.1597.";
                    TempData.Add("CreditCardErrorMessage", ccStatusMessage);
                    TempData.Add("DisplaySaveMyPoints", true);                    
                    //ModelState.AddModelError("Error processing your request", "There was an error processing your request. Please contact Customer Service at 1.800.459.1597.");
                    //mCreditCardModel.ccError = "There was an error processing your request. Please contact Customer Service at 1.800.459.1597.";
                    //LblError.Text = "There was an error processing your request. Please contact Customer Service at 1.800.459.1597.";
                    //savePointsSubmit.Enabled = true;
                    //pnlRemind.Visible = false;
                }
            }
            catch (Exception ex)
            {
                //Panel1.Visible = true;
                //LblError.Visible = true;
                ccStatusMessage = "There was an error processing your request. Please contact Customer Service at 1.800.459.1597.";
                TempData.Add("CreditCardErrorMessage", ccStatusMessage);
                TempData.Add("DisplaySaveMyPoints", true);  
                //mCreditCardModel.ccError = "There was an error processing your request. Please contact Customer Service at 1.800.459.1597.";
                //LblError.Text = "There was an error processing your request. Please contact Customer Service at 1.800.459.1597.";
                //savePointsSubmit.Enabled = true;
            }

        }

        private Boolean ValidateForm(CreditCardInfoModel model)
        {
            bool bValid = true;
            
            List<RuleViolation> Violations = GetCreditCardViolations();

            if (Violations.Count != 0)
                bValid = false;

            //Add violations to model state here...   this might need to be moved to better location
            foreach (var vi in Violations)
            {
                ModelState.AddModelError(vi.PropertyName,
                                         vi.ErrorMessage);

                model.CreditCardInfoErrors.Add(vi.ErrorMessage);
            }
           
            return bValid;
        }

        private List<RuleViolation> GetCreditCardViolations()
        {

            List<RuleViolation> validationIssues = new List<RuleViolation>();
        //this.LblError.Text = "";
            
        //TODO
        //CHECK FOR NULLS

            if (mCreditCardModel.CreditCardName == null)
            {
                validationIssues.Add(new RuleViolation("Credit Card Name", "",
                                                            "Please enter the name on the credit card."));
            }
            else if (mCreditCardModel.CreditCardName.ToString().Length <= 0)
            {
                validationIssues.Add(new RuleViolation("Credit Card Name",
                                                        mCreditCardModel.CreditCardName.ToString(),
                                                        "Please enter the name on the credit card."));
            }

            if(mCreditCardModel.CreditCardNumber == null)
            {
                validationIssues.Add(new RuleViolation("Credit Card Number",
                                                            "",
                                                            "Please enter a valid credit card number."));

                validationIssues.Add(new RuleViolation("Credit Card Type",
                                                        mCreditCardModel.CreditCardType.ToString(),
                                                        "Please check credit card type and credit card number."));
            }
            else
            {
                if (!CheckCC(mCreditCardModel.CreditCardNumber.ToString()))
                {

                    validationIssues.Add(new RuleViolation("Credit Card Number",
                                                            mCreditCardModel.CreditCardNumber.ToString(),
                                                            "Please enter a valid credit card number."));

                }

                if (mCreditCardModel.CreditCardNumber == null || !IsValidForCardType(mCreditCardModel.CreditCardNumber.ToString(), (CardTypes)Enum.Parse(typeof(CardTypes), mCreditCardModel.CreditCardType)))
                {
                    validationIssues.Add(new RuleViolation("Credit Card Type",
                                                        mCreditCardModel.CreditCardType.ToString(),
                                                        "Please check credit card type and credit card number."));
                }
            }


            DateTime dtCreditCardDate = default(DateTime);

            dtCreditCardDate = DateTime.Parse(mCreditCardModel.CreditCardExpDateMonth + "/1/" + mCreditCardModel.CreditCardExpDateYear.ToString().Substring(2,2));
            dtCreditCardDate = dtCreditCardDate.AddMonths(1);

            if (dtCreditCardDate.CompareTo(DateTime.Now) < 0)
            {
                validationIssues.Add(new RuleViolation("Credit Card Expiration Date",
                                                    mCreditCardModel.CreditCardType.ToString(),
                                                    "Your credit card has expired. Please check the date again or try another card."));

            }


            if (!mCreditCardModel.InternationalZipCode)
            {
                if (mCreditCardModel.CreditCardZipCode == null)
                {
                    validationIssues.Add(new RuleViolation("Credit Card Zip Code",
                                                        mCreditCardModel.CreditCardType.ToString(),
                                                        "Please enter a billing zip code."));


                }
                else if (mCreditCardModel.CreditCardZipCode.Length < 2)
                {
                        validationIssues.Add(new RuleViolation("Credit Card Zip Code",
                                                            mCreditCardModel.CreditCardType.ToString(),
                                                            "Please enter a valid billing zip code."));
                }
                else
                {
                    int value;
                    if (!int.TryParse(mCreditCardModel.CreditCardZipCode, out value))
                    {
                        validationIssues.Add(new RuleViolation("Credit Card Zip Code",
                                                            mCreditCardModel.CreditCardType.ToString(),
                                                            "Please enter a valid billing zip code."));
                    }
                }
                //else if (!mCreditCardModel.InternationalZipCode)
                //{
                //    // TODO: remove the following "if"; it is testing an int to see if it is numeric, which it always will be, since it is not a nullable int
                //    //if (!Information.IsNumeric(mCreditCardModel.CreditCardZipCode))
                //    //{
                //    validationIssues.Add(new RuleViolation("Credit Card Zip Code",
                //                                    mCreditCardModel.CreditCardType.ToString(),
                //                                    "Please enter a valid billing zip code."));
                //    //}

                //}
            }

            /*TODO CODE Visibility*/
             //if (bValid)
             //{
             //    Panel1.Visible = false;
             //    LblError.Visible = false;
             //    LblError.Text = "";
             //}
             //else
             //{
             //    Panel1.Visible = true;
             //    LblError.Visible = true;
             //}

            return validationIssues;

        }

        private bool CheckCC(string strCCNo)
        {
            bool functionReturnValue = false;
            try
            {
                int i = 0;
                int w = 0;
                int x = 0;
                int y = 0;
                string CCNo = null;

                y = 0;
                CCNo = Convert.ToString(strCCNo).Replace("-", "").Replace(" ", "").Replace(".", "");
                //Ensure proper format of the input
                if (strCCNo != CCNo)
                {
                    //Whoah there, spaces and dashes and periods oh my!
                    functionReturnValue = false;
                }
                else
                {
                    //Process digits from right to left, drop
                    //     last digit if total length is even
                    w = 2 * (CCNo.Length % 2);
                    for (i = CCNo.Length - 1; i >= 1; i += -1)
                    {
                        x = Convert.ToInt32(CCNo.Substring(i-1, 1));
                        // TODO: remove the following "if"; the prior line will have thrown an exception if the substring is not numeric
                        //if (Information.IsNumeric(x))
                        //{
                            switch ((i % 2) + w)
                            {
                                case 0:
                                case 3:
                                    //Even Digit - Odd where total length is odd (eg. Visa vs. Amx)
                                    y = y + Convert.ToInt32(x);
                                    break;
                                case 1:
                                case 2:
                                    //Odd Digit - Even where total length is odd (eg. Visa vs. Amx)
                                    x = Convert.ToInt32(x) * 2;
                                    if (x > 9)
                                    {
                                        //Break the digits (eg. 19 becomes 1 + 9)
                                        //     
                                        y = y + (x / 10) + (x - 10);
                                    }
                                    else
                                    {
                                        y = y + x;
                                    }
                                    break;
                            }
                        //}
                    }
                    //Return the 10's complement of the total
                    //     
                    y = 10 - (y % 10);
                    if (y > 9)
                        y = 0;
                    functionReturnValue = (Convert.ToString(y) == CCNo.Substring(CCNo.Length - 1));
                }
            }
            catch (Exception ex)
            {
                //Whoops, that wasn't a number!
                functionReturnValue = false;
            }
            return functionReturnValue;
        }


        private bool IsValidForCardType(string cardnumber, CardTypes cardtyp)
        {

            bool validType = false;
            switch (cardtyp)
            {
                case CardTypes.V:
                    if ((Regex.IsMatch(cardnumber, "^(4)")) & (cardnumber.Length == 16 | cardnumber.Length == 13))
                    {
                        validType = true;
                    }
                    break;
                case CardTypes.M:
                    if ((Regex.IsMatch(cardnumber, @"^5[1-5]\d{14}$|^2(?:2(?:2[1-9]|[3-9]\d)|[3-6]\d\d|7(?:[01]\d|20))\d{12}$")) & cardnumber.Length == 16)
                    {
                        validType = true;
                    }
                    break;
                case CardTypes.A:
                    if ((Regex.IsMatch(cardnumber, "^3(4|7)")) & cardnumber.Length == 15)
                    {
                        validType = true;
                    }
                    break;
                case CardTypes.D:
                    if ((Regex.IsMatch(cardnumber, "^(6011)")) & cardnumber.Length == 16)
                    {
                        validType = true;
                    }
                    break;
                default:
                    validType = false;
                    break;
            }
            return validType;
        }

    }
}