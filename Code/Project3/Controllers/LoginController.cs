using BGSitecore.Services;
using Glass.Mapper.Sc.Web.Mvc;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using BGSitecore.Models;
using System.Linq;
using BGSitecore.Components;
using BGSitecore.Models.Resort;
using System;
using BGSitecore.Utils;

namespace BGSitecore.Controllers
{
    public class LoginController : GlassController
    {
        string theRedirectSet = "/home";
        bool bSuccess = false;
        bool isSamplerOwner = false;
        string homeProject = "0";

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult LoginWait()
        {

            ViewBag.MetaLoginRefresh = "<meta http-equiv='refresh' content='0;url=" + Url.Action("ProcessLogin", "Login") + "' />";

            LoginWaitProcessing();
            return View();
        }

        private void LoginWaitProcessing()
        {
            try
            {
                string path_info = "";
                bool Sitecore_Login = true;
                bool VSSA_Login = false;
                var sReferer = Request.ServerVariables.Get("HTTP_REFERER");
                if (!string.IsNullOrEmpty(Convert.ToString(Session["_path_info"])) && Convert.ToString(Session["_path_info"]) != "/")
                {
                    path_info = Convert.ToString(Session["_path_info"]);
                    path_info = HttpUtility.HtmlDecode(path_info);
                    if (path_info.StartsWith("/") && path_info.EndsWith(".aspx"))
                    {
                        path_info = UrlMapper.Map(Sitecore.Configuration.Settings.GetSetting("BGOLegacyHost")) + path_info;
                    }
                   
                }

                if (Convert.ToString(Session["SalesUser"]) == "BLUEGREEN")
                {
                    Session.Clear();
                    Session["SalesUser"] = "BLUEGREEN";
                    Session["_path_info"] = path_info;
                }
                else
                {
                    if (!Convert.ToBoolean(Session["newUser"]))
                    {
                        Session.Clear();
                    }
                    Session["_path_info"] = path_info;
                }

                if ((!string.IsNullOrEmpty(sReferer) && sReferer.ToLower().Contains("bluegreenowner.com")))
                {
                    Sitecore_Login = true;
                }
                else if ((!string.IsNullOrEmpty(sReferer) && sReferer.ToLower().Contains("vssa.bxgcorp.com")))
                {
                    VSSA_Login = true;
                    Session.Clear();
                    Sitecore_Login = false;
                }
                else
                {
                    Sitecore_Login = false;
                }

                if (!Convert.ToBoolean(Session["newUser"]))
                {
                    // Try
                    // Collect information from when a rep logs in as an owner
                    Session["LoginEmail"] = Request.Form["txtEmail"].Trim();
                    Session["LoginPassword"] = Request.Form["txtPassword"].Trim();
                    Session["IsTravelerPlusOwner"] = Request.Form["IsTravelerPlusOwner"];
                    Session["AgentLoginID"] = Request.Form["AgentLoginID"];
                    Session["IsTravelerPlusEmployee"] = Request.Form["IsTravelerPlusEmployee"];
                    Session["TPEmployeeEmail"] = Request.Form["txtEmail"].Trim();
                    Session["TPEmployeePassword"] = Request.Form["txtPassword"].Trim();
                    Session["ownerACCT"] = Request.Form["ownerACCT"];
                    Session["ownerARVACT"] = Request.Form["ownerARVACT"];
                    Session["fromVSSA"] = Request.Form["fromVSSA"];
                    Session["IsTutorialTransfer"] = Request.Form["IsTutorialTransfer"];
                    Session["IsEncoreRewardsOwner"] = Request.Form["IsEncoreRewardsOwner"];
                    Session["sLOCATION"] = Request.Form["Location"];
                    Session["redirect_LCD"] = Request.Form["redirect_LCD"];
                    

                    if (VSSA_Login)
                    {
                        Session["fromVSSA"] = "TRUE";
                        // LoginUtils.loginAsUser();
                        var profilecontroller = new ProfileController();
                        profilecontroller.SignInProcess();
                    }
                    else if ((!Sitecore_Login && Convert.ToString(Session["LoginEmail"]).ToLower().Equals("travelerplus@bxgcorp.com")))
                    {
                        //  If Patrick Jones account and not from VSSA or Sitecore login, assume this is a Sales Kiosk
                        if ((Request.Form["AgentLoginID"] == "Sales_LCD"))
                        {
                            Session["_path_info"] = Session["redirect_LCD"];
                            Session["fromVSSA"] = "FALSE";
                            Response.Cookies.Add(LoginUtils.createFormsAuthCookie(Request.Form["txtEmail"].Trim(), DateTime.Now.AddMinutes(30)));
                        }
                        var profilecontroller = new ProfileController();
                        profilecontroller.SignInProcess();
                    }
                    else if (!Sitecore_Login)
                    {
                        //Write code to sign out unauthorized access 
                        string loginPage = ""; // Get The Login Page from Configuration 
                        foreach (string cookie in Request.Cookies.AllKeys)
                        {
                            Request.Cookies.Remove(cookie);
                        }

                        Response.Redirect(loginPage, true);
                    }

                }
                //TODO : Remove once pages converted - Being used in many Pages in legacy ()
                Session["UnsecuredURL"] = UrlMapper.Map(Sitecore.Configuration.Settings.GetSetting("BGOLegacyHost"));

                // Login hijack for Sales users
                try
                {
                    if (((Convert.ToString(Request.Form["txtEmail"]).ToLower() == "sales") && (Convert.ToString(Request.Form["txtPassword"]).ToLower() == "bluegreen")))
                    {
                        Session["SalesUser"] = "BLUEGREEN";
                        string redirectUrl = UrlMapper.Map(Sitecore.Configuration.Settings.GetSetting("BGOLegacyHost") + "SalesLogin.aspx");
                        Response.Redirect(redirectUrl, true);
                    }
                }
                catch (Exception Ex)
                {
                }

            }
            catch (Exception Ex)
            {
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public void ProcessLogin()
        {
            try
            {
                string loginEmail = Convert.ToString(Session["LoginEmail"]);
                string LoginPassword = Convert.ToString(Session["LoginPassword"]);
                if (ProcessOwnerDetails(loginEmail))
                {
                    DoLoginCheck(loginEmail, LoginPassword);
                }
                else
                {
                    Response.Redirect("/?error=NoConn", true);
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error in Processlogin", ex.StackTrace);
                Response.Redirect("/?error=NoConn", true);
            }
        }

        private bool ProcessOwnerDetails(string email = "")
        {
            // All Logic from legacy loginprocess should go here
            ProfileService pService = new ProfileService();
            bool isSuccess = false;
            var request = new Models.OwnerService.OwnerReq()
            {
                //OwnerID = arvact, // populates during Registration process
                Email = email,// populates during Login process
                SiteName = "BGO"
            };
            var response = pService.GetOwnerDetails(request);
            if (response.Success)
            {
                var legacyObject = OwnerUtils.MapNewOwnerObjectTolegacy(response.Payload);
                Session["BXGOwner"] = legacyObject;
                isSuccess = response.Success;
                //TODO: Turned off analytics as leadership decided not take risk. Please turn it on when ready
                //AnalyticsUtils.IdentifyContact();
            }
            else {
                Sitecore.Diagnostics.Log.Error(string.Concat("FetchOwner", ",Request Email-" ,request.Email), "ProcessOwnerDetails");
            }
            return isSuccess;
        }

        public void checkExposeBonusTime()
        {
            string sAgent = Session["AgentLoginID"] == null ? "" : Convert.ToString(Session["AgentLoginID"]);
            string sBTBlockFixedOwners = "FALSE";   // <add key="BonusTimeBlockFixedOwners" value="FALSE"/> Read From Sitecore instead of Web Config 
            string sBTEnabled = "TRUE"; //<add key="BonusTimeEnabled" value="TRUE"/>

            if (((sAgent.Length > 0) || (sBTEnabled == "TRUE")))
            {
                Session["BonusTimeEnabled"] = true;
            }
            else
            {
                Session["BonusTimeEnabled"] = false;
            }

            if ((sBTBlockFixedOwners == "TRUE"))
            {

                Session["BonusTimeBlockFixedOwners"] = true;
            }
            else
            {
                Session["BonusTimeBlockFixedOwners"] = false;
            }
        }
        //Login Redirect
        private void LoginEgress(string sRedirectTo = null, bool bStop = true)
        {
            BlueGreenContext bgContext = new BlueGreenContext();
            if ((sRedirectTo != null))
            {
                if ((sRedirectTo.Contains("loginUnsuccessful") || sRedirectTo.Contains("error") || bgContext?.bxgOwner == null))
                {
                    Response.Redirect("/", bStop);
                    Session.Remove("BXGOwner");
                    return;
                }

            }

            // Redirect to the page, where user redirected for sign on.
            if ((Convert.ToString(Session["_path_info"]) != string.Empty))
            {
                string strPathInfo = Convert.ToString(Session["_path_info"]);
                Session["_path_info"] = null;
                Response.Redirect(strPathInfo, bStop);
                return;
            }

            if ((sRedirectTo != null))
            {
                // 9/2/11 KO - added to redirect TPEmployee to the right page.  
                if (((Convert.ToString(Session["IsTravelerPlusEmployee"]) == "TRUE") && (sRedirectTo.IndexOf("error") > 0)))
                {
                    LoginEgress("TravelerPlus/loginUnsuccessful");
                }

                //  use Visual Studio's conditional compilation functionality to control if trying to authenticate through ADFS. Doing this so BG does not have to set up a Relying Party definition for 
                //  every workstation being used for development (ADFS requires an RP def for each machine trying to authenticate). Changing the type of build being done determines which of the branches 
                //  below is included in the binary.
                Response.Redirect(sRedirectTo, bStop);
                return;
            }

        }

        //Update Session below 

        public void populateOwnerLegacyObject(BGO.OwnerWS.Owner ownerObject, BGO.BluegreenOnline.Bluegreenowner bgModernLegacyObject)
        {
            if (bgModernLegacyObject != null && ownerObject != null)
            {
                bgModernLegacyObject.OwnerVCNumber = ownerObject.Arvact;
                bgModernLegacyObject.OwnerId = string.IsNullOrEmpty(ownerObject.ownerID) ? 0 : long.Parse(ownerObject.ownerID);
                bgModernLegacyObject.OwnerNamePrefix = ownerObject.namePrefix;
                bgModernLegacyObject.OwnerFirstName = ownerObject.firstName;
                bgModernLegacyObject.OwnerMiddleName = ownerObject.middleName;
                bgModernLegacyObject.OwnerLastName = ownerObject.lastName;
                bgModernLegacyObject.OwnerNameSuffix = ownerObject.nameSuffix;
                bgModernLegacyObject.OwnerEmailAddress = ownerObject.Email;
                bgModernLegacyObject.OwnerAddress1 = ownerObject.Address1;
                bgModernLegacyObject.OwnerAddress2 = ownerObject.Address2;
                bgModernLegacyObject.OwnerCity = ownerObject.City;
                bgModernLegacyObject.OwnerPostalCode = ownerObject.PostalCode;
                bgModernLegacyObject.OwnerSubdivision = ownerObject.Subdivision;
                bgModernLegacyObject.OwnerStateAbr = ownerObject.StateAbr;
                bgModernLegacyObject.OwnerCountryCode = ownerObject.CountryCode;
                bgModernLegacyObject.OwnerHomePhone = ownerObject.HomePhone;
                bgModernLegacyObject.OwnerAlternatePhone = ownerObject.AlternatePhone;
                bgModernLegacyObject.OwnerLast4SSN = ownerObject.last4SSN;
                bgModernLegacyObject.OwnerPaymentBalance = Convert.ToString(ownerObject.PaymentBalance);
                bgModernLegacyObject.OwnerEncoreDividends = ownerObject.encoreDividends;
                bgModernLegacyObject.OwnerAnnualPoints = Convert.ToString(ownerObject.PointsTotalAnnual);
                bgModernLegacyObject.OwnerSavedPoints = Convert.ToString(ownerObject.PointsTotalSaved);
                bgModernLegacyObject.OwnerRestrictedPoints = Convert.ToString(ownerObject.PointsTotalRestricted);
                bgModernLegacyObject.OwnerTotalPoints = Convert.ToString(ownerObject.PointsTotal);
                bgModernLegacyObject.OwnerPaymentBalance = Convert.ToString(ownerObject.PaymentBalance);
                bgModernLegacyObject.OwnerClubLevelDescription = ownerObject.membershipLevelDesc;
                bgModernLegacyObject.OwnerMembershipType = ownerObject.membershipLevel;
                bgModernLegacyObject.OwnerAccountNumber = ownerObject.accountNumber;
                bgModernLegacyObject.OwnerTPLevel = ownerObject.TravelerPlusMembership.TPLevel;

            }

        }

        public void populateOwnerLegacySessionVars(BGO.OwnerWS.Owner ownerObject)
        {

            Session["OwnerNumber"] = Convert.ToString(ownerObject.Arvact);
            Session["OwnerIdentity"] = ownerObject.ownerID;
            Session["OwnerNamePrefix"] = ownerObject.namePrefix;
            Session["OwnerNameFirst"] = ownerObject.firstName;
            Session["OwnerNameMiddle"] = ownerObject.middleName;
            Session["OwnerNameLast"] = ownerObject.lastName;
            Session["OwnerNameSuffix"] = ownerObject.nameSuffix;
            Session["OwnerEmailAddress"] = Convert.ToString(ownerObject.Email);
            Session["OwnerAddress1"] = ownerObject.Address1;
            Session["OwnerAddress2"] = ownerObject.Address2;
            Session["OwnerCity"] = ownerObject.City;
            Session["OwnerPostalCode"] = ownerObject.PostalCode;
            Session["OwnerSubdivision"] = ownerObject.Subdivision;
            Session["OwnerStateAbr"] = ownerObject.StateAbr;
            Session["OwnerCountryCode"] = ownerObject.CountryCode;
            Session["OwnerHomePhone"] = ownerObject.HomePhone;
            Session["OwnerAlternatePhone"] = ownerObject.AlternatePhone;
            Session["OwnerLast4SSN"] = ("    " + ownerObject.last4SSN).Substring((("    " + ownerObject.last4SSN).Length - 4)).Trim();
            Session["OwnerEncoreDividends"] = ownerObject.encoreDividends;
            Session["OwnerPoints"] = ownerObject.PointsTotal;
            Session["OwnerOriginalPoints"] = ownerObject.PointsTotalAnnual;
            Session["OwnerSavedPoints"] = ownerObject.PointsTotalSaved;
            Session["AccountNumber"] = ownerObject.accountNumber;
            Session["Expires"] = ownerObject.OwnerExpiration;
            if (Convert.ToString(ownerObject.User[0]?.AllAccountsComesFromSecondaryMarketing).ToLower().Equals("false"))
            {
                Session["secIdentity"] = ownerObject.User[0].AllAccountsComesFromSecondaryMarketing;
            }

        }

        private void LoadPaymentBalance(BGO.OwnerWS.Owner ownerObject, BGO.BluegreenOnline.Bluegreenowner bgModernlegacyObject)
        {
            BGO.BluegreenOnline.Bluegreenowner owner = bgModernlegacyObject;
            try
            {
                double dTotal = 0;
                string sCurProj = "";
                List<BGO.BluegreenOnline.clsLoginAccount> alAccounts = new List<BGO.BluegreenOnline.clsLoginAccount>();
                bool bSamplerHolder = false;

                for (int counter = 0; counter < ownerObject?.maintFees?.Count(); counter++)
                {
                    BGO.BluegreenOnline.clsLoginAccount AccountInfo = new BGO.BluegreenOnline.clsLoginAccount();

                    AccountInfo.VIP = ownerObject?.maintFees[counter]?.vip == null ? false : true;
                    AccountInfo.ProjNum = ownerObject?.maintFees[counter]?.projnum;
                    // AccountInfo.ContractID = .acctNum
                    if (((AccountInfo.ProjNum != "51") && (AccountInfo.ProjNum != "52")))
                    {
                        if ((ownerObject.maintFees[counter].projnum == "50"))
                        {
                            AccountInfo.ContractType = "Vacation Club";
                            AccountInfo.ResortName = "Vacation Club";
                            AccountInfo.ResortID = "0";
                            Session["ProjNum"] = ownerObject.maintFees[counter].projnum;
                            Session["ContractType"] = AccountInfo.ContractType;
                            Session["OwnerContractType"] = AccountInfo.ContractType;
                        }
                        else
                        {
                            AccountInfo.ContractType = ownerObject.maintFees[counter].proname.Trim();
                            ResortDetails sTmpRes = new ResortDetails();
                            sTmpRes = ResortManager.GetResortByProject(ownerObject.maintFees[counter].projnum);
                            AccountInfo.ResortID = sTmpRes?.ResortId != null ? Convert.ToString(sTmpRes.ResortId) : " ";
                            AccountInfo.ResortName = sTmpRes?.ResortName != null ? Convert.ToString(sTmpRes.ResortName): " " ;
                        }

                        AccountInfo.Weeks = ownerObject.maintFees[counter].weeks;
                        // Accumulate the same project, otherwise add new ones
                        if ((sCurProj == AccountInfo.ResortID))
                        {
                            // need to do this if owner was a sampler prior to becoming an owner; owner holds multiple account
                            if ((alAccounts.Count == 0))
                            {
                                if ((AccountInfo.ProjNum == "32"))
                                {
                                    Session["Players"] = "YES";
                                }
                                else if ((AccountInfo.ProjNum == "79"))
                                {
                                    Session["Pono"] = "YES";
                                }
                                else
                                {
                                    alAccounts.Add(AccountInfo);
                                }

                            }

                            alAccounts[alAccounts.Count - 1].Weeks += ("," + AccountInfo.Weeks);
                            double cBal = Convert.ToDouble(alAccounts[alAccounts.Count - 1].PaymentBalance);
                            cBal = (cBal + Convert.ToDouble(AccountInfo.PaymentBalance));
                            alAccounts[alAccounts.Count - 1].PaymentBalance = Convert.ToString(cBal);
                        }
                        else
                        {
                            // Filter Out Sampler Owners
                            if ((AccountInfo.ProjNum == "32"))
                            {
                                // Onwer Project is 50 and home project is 51 allow to access the web site.
                                bSamplerHolder = true;
                                Session["Players"] = "YES";
                            }
                            else if ((AccountInfo.ProjNum == "79"))
                            {
                                bSamplerHolder = true;
                                Session["Pono"] = "YES";
                            }
                            else
                            {
                                alAccounts.Add(AccountInfo);
                            }

                        }

                        sCurProj = AccountInfo.ResortID;
                        // Add up a total balance
                        dTotal = (dTotal + Convert.ToDouble(ownerObject.maintFees[counter].amt));
                        if (((AccountInfo.ProjNum == "51") || (AccountInfo.ProjNum == "52")))
                        {
                            alAccounts.Remove(AccountInfo);
                        }

                    }

                }

                Session["OwnerPaymentBalance"] = dTotal;
                Session["EncoreMaintenanceFeeBalance"] = Session["OwnerPaymentBalance"];
                Session["LoginAccountList"] = alAccounts;
                // If user only holds a sampler account
                if ((bSamplerHolder && (alAccounts.Count == 0)))
                {
                    // Session("SamplerOnlyUser") = "SAMPLER"
                    if (Convert.ToString(Session["Players"]) == "YES")
                    {
                        Session["SamplerOnlyUser"] = "Players";
                    }
                    else if (Convert.ToString(Session["Pono"]) == "YES")
                    {
                        Session["SamplerOnlyUser"] = "Pono";
                    }

                }

                // If there was only one account then let's get it set up
                if (((alAccounts.Count == 1) && (ownerObject.User[0].isSampler == true)))
                {
                    owner.OwnerContractType = "Sampler";
                    Session["OwnerContractType"] = "Sampler";
                    Session["OwnerHomeResort"] = alAccounts[0].ResortID;
                    Session["OwnerHomeProjectNumber"] = alAccounts[0].ProjNum;
                    Session["OwnerHomeResortWeeks"] = alAccounts[0].Weeks;
                    Session["OwnerVIP"] = alAccounts[0].VIP;
                }
                else if (((alAccounts.Count == 1)
                            && (ownerObject.User[0].isSampler == false)))
                {
                    owner.OwnerContractType = alAccounts[0].ContractType;
                    Session["OwnerContractType"] = alAccounts[0].ContractType;
                    Session["OwnerHomeResort"] = alAccounts[0].ResortID;
                    Session["OwnerHomeProjectNumber"] = alAccounts[0].ProjNum;
                    Session["OwnerHomeResortWeeks"] = alAccounts[0].Weeks;
                    Session["OwnerVIP"] = alAccounts[0].VIP;
                }

                alAccounts = null;
            }
            catch (Exception ex)
            {
                //Response.Redirect(ex.Message);
                string sErr = ex.Message;
            }

        }

        void CheckNonTravelerPlusRedirect(BGO.OwnerWS.Owner bxgOwner, BGO.BluegreenOnline.Bluegreenowner bgModernOwnerObject)
        {
            bool isPopuupRedirect = false;
            BGO.BluegreenOnline.Bluegreenowner owner = bgModernOwnerObject;


            bxgOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            if ((((List<BGO.BluegreenOnline.clsLoginAccount>)Session["LoginAccountList"]).Count == 1))
            {
                // Use the default account info if there was only one account
                // Set logo information for user session based on club membership
                if (Session["OwnerContractType"] != null && (Convert.ToString(Session["OwnerContractType"]) == "Vacation Club") || Convert.ToString(Session["OwnerContractType"]) == "Sampler")
                {
                    // Vacation Club Member
                    Session["siteLogo"] = "bgvc_logo2.gif";
                    Session["sitePhoneNumberImg"] = "hdrTag_x.gif";
                    Session["sitePhoneNumberMouseover"] = "hdrTag_o.gif";
                    if ((Convert.ToString(Session["OwnerContractType"]) == "Sampler"))
                    {
                        Session["sitePhoneNumber"] = "800.459.1597";
                    }
                    else
                    {
                        Session["sitePhoneNumber"] = "800.456.CLUB (2582)";
                    }

                    Session["siteLogoAlt"] = "Bluegreen Vacation Club";
                    Session["siteLogoHeight"] = "63";
                    if ((Convert.ToString(Session["IsTravelerPlusEligible"]) != "TRUE"))
                    {
                        Session["siteNavjs"] = "owner_data";
                    }

                    Session["siteTemplateHeight"] = "253";
                    Session["siteTemplateImage"] = "ownerBkgd1b_lft.gif";
                    if ((Convert.ToString(Session["IsEncoreRewardsOwner"]) == "TRUE"))
                    {
                        owner.OwnerType = "EncoreRewards";
                        theRedirectSet = "/rewards/bluegreen-rewards";

                    }
                    else
                    {
                        Session["BXGOwner"] = bxgOwner;
                        if ((Convert.ToString(Session["IsTutorialTransfer"]) == "True"))
                        {
                            theRedirectSet = UrlMapper.Map(Sitecore.Configuration.Settings.GetSetting("BGOLegacyHost") + "owner/OwnerTutorial.aspx");
                        }
                        else if (Convert.ToString(Session["sLOCATION"]) == "TravelerPlus")
                        {
                            theRedirectSet = UrlMapper.Map(Sitecore.Configuration.Settings.GetSetting("BGOLegacyHost") + "owner/vcTravelerplus.aspx");
                        }
                        else
                        {
                            // if the account is overdue we need to redirect. However is the owner is under Installment Payment (IP) just go to home page.
                            if ((bxgOwner?.InstallmentPlan != null && bxgOwner.InstallmentPlan[0].InstallmentStatus != null) && ((bxgOwner.InstallmentPlan[0].InstallmentStatus == "IS") || (bxgOwner.InstallmentPlan[0].InstallmentStatus == "ID")))
                            {
                                theRedirectSet = "/payments/installment-status";
                                //theRedirectSet = UrlMapper.Map(Sitecore.Configuration.Settings.GetSetting("BGOLegacyHost") + "owner/acctStatus.aspx");
                                isPopuupRedirect = true;
                            }
                            else if (((bxgOwner.ReservationDuePaymentBalance > 0) && (bxgOwner?.InstallmentPlan != null && bxgOwner.InstallmentPlan[0].InstallmentStatus != null && bxgOwner.InstallmentPlan[0].InstallmentStatus != "IP")))
                            {
                                theRedirectSet = "/maintenance/reminder";
                               // theRedirectSet = UrlMapper.Map(Sitecore.Configuration.Settings.GetSetting("BGOLegacyHost") + "owner/paymentReminder.aspx");
                                isPopuupRedirect = true;
                            }
                            else if (((bxgOwner.AnnualPointsExpiration.SavePointsEligible == true)
                                        && (bxgOwner.AnnualPointsExpiration.SavePointsPopup == true)))
                            {
                                if ((bxgOwner.User[0].HomeProject == "52"))
                                {
                                    if (string.IsNullOrWhiteSpace(Convert.ToString(Session["LinkForHomePage"])))
                                    {
                                        theRedirectSet = "/home";
                                    }

                                }
                                else
                                {
                                    theRedirectSet = "/mybluegreen/my-points";
                                    isPopuupRedirect = true;
                                }

                            }
                            else if (((bxgOwner.TravelerPlusMembership.TPRenewPopup == true) && (isPopuupRedirect == false)))
                            {
                                try
                                {
                                    // Remove Traveler Plus reminder page from auto renewal TP owners
                                    if ((bxgOwner.TravelerPlusMembership.IsTravelerPlusAutoRenew == true))
                                    {
                                        // TODO: once BGO has been deployed to production with the inclusion of the new BGModern site, the "theRedirectSet" var can be set to the home route in BGModern
                                        if (string.IsNullOrWhiteSpace(Convert.ToString(Session["LinkForHomePage"])))
                                        {
                                            theRedirectSet = "/home";
                                        }
                                        else
                                        {
                                            theRedirectSet = Convert.ToString(Session["LinkForHomePage"]);
                                        }

                                    }
                                    else if (((bxgOwner.User[0].HasAccountInSecondaryMarket == false)
                                                && (bxgOwner.PointsTotalSecondaryMarket <= 0)))
                                    {
                                        theRedirectSet = UrlMapper.Map(Sitecore.Configuration.Settings.GetSetting("BGOLegacyHost") + "TravelerPlus/owner/ownerrenewal.aspx?display=1");
                                    }

                                    isPopuupRedirect = true;
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                            else
                            {
                                // This new logical address the new Marketing application "LCD".
                                // Any modification MUST be discussed to Creative Dept.
                                // The LCD application hardcode hidden info based on "travelerplus@bxgcorp.com" to log in on BGO without prompt and
                                // redirect to whatever page.
                                if ((Convert.ToString(Session["AgentLoginID"]) == "Sales_LCD"))
                                {
                                    theRedirectSet = Convert.ToString(Session["redirect_LCD"]);
                                }
                                else
                                {
                                    // otherwise just go to home page
                                    // TODO: once BGO has been deployed to production with the inclusion of the new BGModern site, the "theRedirectSet" var can be set to the home route in BGModern
                                    if (string.IsNullOrWhiteSpace(Convert.ToString(Session["LinkForHomePage"])))
                                    {
                                        theRedirectSet = "/home";
                                    }
                                    else
                                    {
                                        theRedirectSet = Convert.ToString(Session["LinkForHomePage"]);
                                    }

                                }

                            }

                        }

                    }

                }
                else if (Session["OwnerContractType"] != null && (Convert.ToString(Session["OwnerContractType"]) == "Sampler"))
                {
                    Session["siteLogo"] = "bgvc_logo2.gif";
                    Session["sitePhoneNumberImg"] = "hdrTag_x.gif";
                    Session["sitePhoneNumberMouseover"] = "hdrTag_o.gif";
                    if ((Convert.ToString(Session["OwnerContractType"]) == "Sampler"))
                    {
                        Session["sitePhoneNumber"] = "800.459.1597";
                    }
                    else
                    {
                        Session["sitePhoneNumber"] = "800.456.CLUB (2582)";
                    }

                    Session["siteLogoAlt"] = "Bluegreen Vacation Club";
                    Session["siteLogoHeight"] = "63";
                    if ((Convert.ToString(Session["IsTravelerPlusEligible"]) != "TRUE"))
                    {
                        Session["siteNavjs"] = "owner_data";
                    }

                    Session["siteTemplateHeight"] = "253";
                    Session["siteTemplateImage"] = "ownerBkgd1b_lft.gif";
                    if (string.IsNullOrWhiteSpace(Convert.ToString(Session["LinkForHomePage"])))
                    {
                        theRedirectSet = "/home";
                    }
                    else
                    {
                        theRedirectSet = Convert.ToString(Session["LinkForHomePage"]);
                    }

                }
                else
                {
                    // Some other club - Fixed, etc.
                    Session["siteLogo"] = "bgvc_logo2.gif";
                    Session["sitePhoneNumberImg"] = "nonclubowner_phone.gif";
                    Session["sitePhoneNumberMouseover"] = "nonclubowner_phone.gif";
                    Session["sitePhoneNumber"] = "877.688.9889";
                    Session["siteLogoAlt"] = "Bluegreen Resort Management";
                    Session["siteLogoHeight"] = "63";
                    Session["siteNavjs"] = "ownerNVC_data";
                    Session["siteTemplateHeight"] = "223";
                    Session["siteTemplateImage"] = "ownerBkgd1_lft.gif";
                    if ((Convert.ToString(Session["IsEncoreRewardsOwner"]) == "TRUE"))
                    {
                        owner.OwnerType = "EncoreRewards";
                        theRedirectSet = "/rewards/bluegreen-rewards";
                    }
                    else
                    {
                        theRedirectSet = "/home";
                    }

                }

            }
            else if (Session["LoginAccountList"] != null && (((List<BGO.BluegreenOnline.clsLoginAccount>)Session["LoginAccountList"]).Count > 1))
            {
                // save number of accounts an owner have in a cookie
                HttpCookie accCookie = new HttpCookie("accountListCookie");
                accCookie.Value = Convert.ToString(((List<BGO.BluegreenOnline.clsLoginAccount>)Session["LoginAccountList"]).Count);
                accCookie.Secure = true;
                Response.Cookies.Add(accCookie);
                //RegenerateSessionID();
                // Go to an account selection screen and let the user pick
                theRedirectSet = "/select-account";
            }
            else
            {
                bSuccess = false;
            }

        }

        void checkTravelerplusRedirect(BGO.BluegreenOnline.Bluegreenowner ownerContext)
        {
            BGO.BluegreenOnline.Bluegreenowner owner = ownerContext;
            string sIsTPEligible = "NO";

            if (((owner.OwnerTPLevel == BGO.Travelerpluslevel.TravelerPlusPlusLevel)
                        || ((owner.OwnerTPLevel == BGO.Travelerpluslevel.TravelerPlusLevel)
                        || (owner.OwnerTPLevel == BGO.Travelerpluslevel.TravelerPlus3))))
            {
                sIsTPEligible = "YES";
                Session["IsTravelerPlusEligible"] = "TRUE";
                Session["siteNavjs"] = "ownerTP_data";
                owner.OwnerIsTravelerPlusEligible = "true";
            }
            else
            {
                sIsTPEligible = "NO";
                Session["IsTravelerPlusEligible"] = "FALSE";
                owner.OwnerIsTravelerPlusEligible = "false";
                Session["IsTravelerPlusEligible"] = false;
            }

            // Redirect for traveler plus logins if qualified
            if (Session["IsTravelerPlusOwner"] != null && (Convert.ToString(Session["IsTravelerPlusOwner"]) == "TRUE"))
            {
                owner.OwnerType = "TravelerPlus";
                if ((sIsTPEligible == "YES"))
                {
                    // Fill in our template as a vacation club user
                    Session["siteLogo"] = "bgvc_logo2.gif";
                    Session["sitePhoneNumberImg"] = "hdrTag_x.gif";
                    Session["sitePhoneNumberMouseover"] = "hdrTag_o.gif";
                    Session["sitePhoneNumber"] = "800.456.CLUB (2582)";
                    Session["siteLogoAlt"] = "Bluegreen Vacation Club";
                    Session["siteLogoHeight"] = "63";
                    Session["siteTemplateHeight"] = "253";
                    Session["siteTemplateImage"] = "ownerBkgd1b_lft.gif";
                }

            }

        }

        private void SetMedalliaCookie(BGO.OwnerWS.Owner bxgOwner)
        {
            HttpCookie _userInfoCookies = new HttpCookie("OwnerInfo");
            string TPStatus = string.Empty;
            if (bxgOwner?.TravelerPlusMembership?.IsTravelerPlusEligible != null && bxgOwner.TravelerPlusMembership.IsTravelerPlusEligible)
            {
                if (bxgOwner.TravelerPlusMembership.AccountExpired)
                {
                    TPStatus = "EXPIRED";
                }
                else
                {
                    TPStatus = "ACTIVE";
                }
            }
            else
            {
                TPStatus = "NOTELIGIBLE";
            }

            string OwnerType = string.Empty;
            if (bxgOwner.User[0].isSampler)
            {
                OwnerType = "SAMPLER";
            }
            else
            {
                if (Session["OwnerContractType"] != null && Convert.ToString(Session["OwnerContractType"]) == "Vacation Club")
                {
                    OwnerType = "VACCLUB";
                }
                else
                {
                    OwnerType = "TRADITIONAL";
                }
            }

            _userInfoCookies["OwnerId"] = bxgOwner.Arvact;
            _userInfoCookies["OwnerType"] = OwnerType.ToUpper();
            _userInfoCookies["TPStatus"] = TPStatus;
            _userInfoCookies.Expires = DateTime.Now.AddDays(1);
            _userInfoCookies.Secure = true;
            Response.Cookies.Add(_userInfoCookies);
        }


        public ActionResult SelectAccount()
        {
            var accountsModel = GetLayoutItem<SelectAccountViewModel>();
            var BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            if (BXGOwner != null)
            {
                if (BXGOwner?.User != null && BXGOwner.User.Any())
                {
                    isSamplerOwner = BXGOwner.User[0].isSampler;
                    homeProject = BXGOwner.User[0].HomeProject;
                }

                if (accountsModel.RandomImageLocation != null && accountsModel.RandomImageLocation.Count() > 0)
                {
                    Random rnd = new Random();
                    int imageCount = rnd.Next(accountsModel.RandomImageLocation.Count());
                    accountsModel.BackGroundImage = accountsModel.RandomImageLocation.ElementAt(imageCount);
                }


                //Logic to map accountlist with model
                // Loop through all the stored accounts and add them to the radio button list
                List<BGO.BluegreenOnline.clsLoginAccount> accountList = Session["LoginAccountList"] != null ? (List<BGO.BluegreenOnline.clsLoginAccount>)Session["LoginAccountList"] : new List<BGO.BluegreenOnline.clsLoginAccount>();
                foreach (BGO.BluegreenOnline.clsLoginAccount account in accountList)
                {

                    if (((account.ProjNum == "50") && ((isSamplerOwner) && (homeProject == "52"))))
                    {
                        accountsModel.AccountList.Add(accountList.IndexOf(account), "Sampler 24");
                    }
                    else if (((account.ProjNum == "50") && ((isSamplerOwner) && (homeProject == "51"))))
                    {
                        accountsModel.AccountList.Add(accountList.IndexOf(account), "Sampler");
                    }
                    else
                    {
                        accountsModel.AccountList.Add(accountList.IndexOf(account), account.ResortName);
                    }
                }
            }
            else
            {
                Response.Redirect("/", true);
                Session.Remove("BXGOwner");
            }
            return View(accountsModel);
        }

        [HttpPost]
        public ActionResult SelectAccount(SelectAccountViewModel SelectAccount)
        {
            ActionResult result = null;
            var bxgOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            if (ModelState.IsValid && bxgOwner != null)
            {
                if (bxgOwner?.User != null && bxgOwner.User.Any())
                {
                    isSamplerOwner = bxgOwner.User[0].isSampler;
                    homeProject = bxgOwner.User[0].HomeProject;
                }

                List<BGO.BluegreenOnline.clsLoginAccount> accountList = (List<BGO.BluegreenOnline.clsLoginAccount>)Session["LoginAccountList"];
                var account = accountList?.Where(x => accountList?.IndexOf(x) == Convert.ToInt32(SelectAccount?.selectedAccount))?.FirstOrDefault();

                if (account != null)
                {
                    if (((account.ProjNum == "50") && (isSamplerOwner)))
                    {
                        Session["OwnerContractType"] = "Sampler";
                    }
                    else
                    {
                        Session["OwnerContractType"] = account.ContractType;
                    }
                    Session["OwnerHomeResort"] = account.ResortID;
                    Session["OwnerHomeProjectNumber"] = account.ProjNum;
                    Session["OwnerContractNumber"] = account.ContractID;
                    Session["OwnerHomeResortWeeks"] = account.Weeks;
                    Session["OwnerVIP"] = account.VIP;
                }

                if ((Convert.ToString(Session["OwnerContractType"]) == "Vacation Club"))
                {
                    // Vacation Club Member
                    Session["siteLogo"] = "bgvc_logo2.gif";
                    Session["sitePhoneNumberImg"] = "hdrTag_x.gif";
                    Session["sitePhoneNumberMouseover"] = "hdrTag_o.gif";
                    Session["sitePhoneNumber"] = "800.456.CLUB (2582)";
                    Session["siteLogoAlt"] = "Bluegreen Vacation Club";
                    Session["siteLogoHeight"] = "63";
                    Session["siteNavjs"] = "owner_data";
                    Session["siteTemplateHeight"] = "253";
                    Session["siteTemplateImage"] = "ownerBkgd1b_lft.gif";

                    var installMentStatus = (bxgOwner?.InstallmentPlan != null && bxgOwner?.InstallmentPlan[0]?.InstallmentStatus != null) ? bxgOwner?.InstallmentPlan[0]?.InstallmentStatus : string.Empty;

                    if (installMentStatus == "IS" || installMentStatus == "ID")
                    {
                       // string redirectUrl = UrlMapper.Map(Sitecore.Configuration.Settings.GetSetting("BGOLegacyHost") + "owner/acctStatus.aspx");
                        string redirectUrl  = "/payments/installment-status";
                        Response.Redirect(redirectUrl, true);
                    }
                    else if (bxgOwner?.ReservationDuePaymentBalance > 0 && installMentStatus != "IP")
                    {
                        //string redirectUrl  = UrlMapper.Map(Sitecore.Configuration.Settings.GetSetting("BGOLegacyHost") + "owner/paymentReminder.aspx");
                        //Response.Redirect(redirectUrl, true);
                        Response.Redirect("/maintenance/reminder", true);
                    }
                    else if (((bxgOwner.AnnualPointsExpiration.SavePointsEligible == true) && (bxgOwner.AnnualPointsExpiration.SavePointsPopup == true)))
                    {
                        Response.Redirect("/mybluegreen/my-points", true);
                    }
                    else if ((bxgOwner.TravelerPlusMembership.TPRenewPopup == true))
                    {
                        string redirectUrl = UrlMapper.Map(Sitecore.Configuration.Settings.GetSetting("BGOLegacyHost") + "TravelerPlus/owner/ownerrenewal.aspx?display=1");
                        Response.Redirect(redirectUrl, true);
                    }
                    else
                    {
                        Response.Redirect("/home", true);
                    }

                }
                else if ((Convert.ToString(Session["OwnerContractType"]) == "Sampler"))
                {
                    Session["siteLogo"] = "bgvc_logo2.gif";
                    Session["sitePhoneNumberImg"] = "hdrTag_x.gif";
                    Session["sitePhoneNumberMouseover"] = "hdrTag_o.gif";
                    if ((Convert.ToString(Session["OwnerContractType"]) == "Sampler"))
                    {
                        Session["sitePhoneNumber"] = "800.459.1597";
                    }
                    else
                    {
                        Session["sitePhoneNumber"] = "800.456.CLUB (2582)";
                    }

                    Session["siteLogoAlt"] = "Bluegreen Vacation Club";
                    Session["siteLogoHeight"] = "63";
                    if ((Convert.ToString(Session["IsTravelerPlusEligible"]) != "TRUE"))
                    {
                        Session["siteNavjs"] = "owner_data";
                    }

                    Session["siteTemplateHeight"] = "253";
                    Session["siteTemplateImage"] = "ownerBkgd1b_lft.gif";
                    Response.Redirect("/home", true);
                }
                else
                {
                    // Some other club - Fixed, etc.
                    Session["siteLogo"] = "bgvc_logo2.gif";
                    Session["sitePhoneNumberImg"] = "nonclubowner_phone.gif";
                    Session["sitePhoneNumberMouseover"] = "nonclubowner_phone.gif";
                    Session["sitePhoneNumber"] = "877.688.9889";
                    Session["siteLogoAlt"] = "Bluegreen Resort Management";
                    Session["siteLogoHeight"] = "63";
                    Session["siteNavjs"] = "ownerNVC_data";
                    Session["siteTemplateHeight"] = "223";
                    Session["siteTemplateImage"] = "ownerBkgd1_lft.gif";
                    Response.Redirect("/home", true);
                }
            }
            else
            {
                result = base.Index();
            }
            return result;
        }


        private void DoLoginCheck(string Username, string UserPassword)
        {
            BGO.BluegreenOnline.Bluegreenowner owner = new BGO.BluegreenOnline.Bluegreenowner();
            BGO.OwnerWS.Owner bxgOwner = new BGO.OwnerWS.Owner();
            bxgOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];

            if (bxgOwner != null && !string.IsNullOrEmpty(Username))
            {
                 // save user name for an owner in a cookie
                HttpCookie userIdCookie = new HttpCookie("userIdCookie");
                userIdCookie.Value = Convert.ToString(Session["LoginEmail"]);
                userIdCookie.Expires = DateTime.Now.AddDays(1);
                userIdCookie.Secure = true;
                Response.Cookies.Add(userIdCookie);
                //RegenerateSessionID(); **
                // Set flag to hide submit buttons for bookings if vacationclub or saleskiosk
                if (Session["AgentLoginID"] != null && Convert.ToString(Session["AgentLoginID"]) == "SALESKIOSK")
                {
                    Session["DEMOMODE"] = "TRUE";
                    owner.OwnerType = "TravelerPlus";
                    Session["IsTravelerPlusOwner"] = "TravelerPlus";
                }

                // Loop through configured demo account list
                string[] sOmitItem;
                sOmitItem = Sitecore.Globalization.Translate.Text("SelectAccount_DemoAccountList1").Split('|');
                for (int x = 0; x < sOmitItem.Length; x++)
                {
                    if ((Username.ToLower().IndexOf(sOmitItem[x].ToLower()) > -1))
                    {
                        Session["DEMOMODE"] = "TRUE";
                    }

                }

                // If an agent logs in or it is set to enabled then expose the bonus time pages
                checkExposeBonusTime();

                if (bxgOwner.Authenticated)
                {

                    // Assign Session to recognise premier Owner
                    string premierOwnerLevels = "PLATINUM,GOLD,SILVER,BRONZE";
                    if (!string.IsNullOrEmpty(bxgOwner?.membershipLevelDesc))
                    {
                        if (premierOwnerLevels.Contains(bxgOwner.membershipLevelDesc?.ToUpper()))
                        {
                            Session["ispremierOwner"] = ((bxgOwner.membershipLevelDesc == "") ? false : true);
                            Session["premierOwnerLevel"] = ((bxgOwner.membershipLevelDesc == "") ? string.Empty : bxgOwner.membershipLevelDesc);
                        }

                    }

                    //Validate sampler plus accounts **
                    try
                    {
                        if (bxgOwner.User[0].isSampler)
                        {
                            DateTime expirationdate = Convert.ToDateTime(bxgOwner.User[0].samplerExpiration);
                            if ((DateTime.Today > expirationdate))
                            {
                                Session["SamplerOnlyUser"] = "SAMPLER";
                                Session["ownernumber"] = bxgOwner.Arvact;
                                LoginEgress("loginUnsuccessful");
                                Session["SignInUiError"] = SitecoreUtils.getLocalString("Profile_AccountExpired");
                                return;
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        Session["SamplerOnlyUser"] = "SAMPLER";
                    }
                    populateOwnerLegacyObject(bxgOwner, owner);
                    populateOwnerLegacySessionVars(bxgOwner);
                }
                else if (Session["BXGOwner"] != null)
                {
                    if (((bxgOwner.User[0].project == "51") || (bxgOwner.User[0].project == "52")))
                    {
                        if ((Session["_path_info"] != null))
                        {
                            Session["_path_info"] = "";
                        }
                        LoginEgress("loginUnsuccessful");
                        return;
                    }
                }
                else
                {
                    LoginEgress("default?error=NoACK");
                    return;
                }
                Session["IsPendingOwner"] = "FALSE";
                try
                {
                    if ((Sitecore.Configuration.Settings.GetSetting("AllowBooking").Equals("true", StringComparison.CurrentCultureIgnoreCase)))
                    {
                        LoadPaymentBalance(bxgOwner, owner);
                    }
                    BGO.BluegreenOnline.Bluegreenowner.CurrentOwner = owner;
                    // the above must be saved to the session for the BGModern site.
                    Session["BluegreenownerForUmbraco"] = BGO.BluegreenOnline.Bluegreenowner.CurrentOwner;
                    checkTravelerplusRedirect(owner);
                    CheckNonTravelerPlusRedirect(bxgOwner, owner);
                    bSuccess = true;

                }
                catch (Exception ex)
                {
                    bSuccess = false;
                }
            }
            else
            {
                LoginEgress("/SignInHelp", true);
                Session.Remove("BXGOwner");
                return;
            }

            SetMedalliaCookie(bxgOwner);            
            // Failed to log in
            if ((bSuccess == false))
            {
                LoginEgress("loginUnsuccessful", true);
                Session.Remove("BXGOwner");
                return;
            }
            else if (((Convert.ToString(Session["SamplerOnlyUser"]) == "Players") && (Convert.ToString(Session["Players"]) == "YES")))
            {
                LoginEgress("loginUnsuccessful", true);
                Session.Remove("BXGOwner");
                return;
            }
            else if (((Convert.ToString(Session["SamplerOnlyUser"]) == "Pono") && (Convert.ToString(Session["Pono"]) == "YES")))
            {
                LoginEgress("loginUnsuccessful", true);
                Session.Remove("BXGOwner");
                return;
            }
            else
            {
                // 'Force User to Registration Confirmation irrespective of any condition during registration
                if (Session["ownerACCT"] != null && Session["ownerRegisterReferrer"] != null)
                {
                    if (Convert.ToString(Session["ownerRegisterReferrer"]).Contains("Register"))
                    {
                        theRedirectSet = "/confirm-registration";
                    }

                }

                LoginEgress(theRedirectSet, true);
                return;
            }

        }


        public void Signoff()
        {
            try {
                Session.Abandon();
                FormsAuthentication.SignOut();
                foreach (var cookieItem in Request.Cookies.AllKeys)
                {

                    var httpCookieItem = Request.Cookies[cookieItem];
                    if (httpCookieItem.Name.ToLower() != "ownerinfo")
                    {
                        httpCookieItem.Value = string.Empty;
                        httpCookieItem.Expires = DateTime.Now.AddMonths(-20);
                        Response.Cookies.Add(httpCookieItem);
                        Request.Cookies.Remove(cookieItem);
                    }
                }
            }
            catch (Exception ex) {
                Sitecore.Diagnostics.Log.Error("Error While Signing off :", ex);
            }
            finally {
                Response.Redirect("/?signoff=true");
            }
        }
    }
}
