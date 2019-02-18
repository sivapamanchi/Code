using BGSitecore.Models;
using Glass.Mapper.Sc;
using System;
using System.Web.Mvc;
using BGSitecore.Components;
using BGSitecore.Utils;
using BGSitecore.Services;
using Glass.Mapper.Sc.Web.Mvc;
using System.Globalization;
using Sitecore;
using System.Web.Security;
using System.Data.SqlClient;
using BGSitecore.Attributes;
using Sitecore.Web;
using System.Linq;
using System.Collections.Generic;

namespace BGSitecore.Controllers
{
    public class ProfileController : GlassController
    {

        public string sEmail;
        public string sPass;

        // Hidden fields

        public string path_info;

        public string sReferrer;

        public string OwnerId { get; set; }
        public string OwnerType { get; set; }
        public string TPStatus { get; set; }
        public BGO.OwnerWS.Owner bxgOwner = new BGO.OwnerWS.Owner();
        public bool HasOwnerInformation { get; set; }

        private bool IsPostBack = false;

        // GET: Authentication
        public ActionResult SignIn()
        {


            var context = new SitecoreContext();
            var model = context.GetCurrentItem<SignIn>();
            model.noAckError = (Request.QueryString["error"] == "NoACK");
            model.SurveyPercent = int.Parse(Sitecore.Configuration.Settings.GetSetting("SurveyPercent"));
            if (Session["SignInUiError"] != null)
            {
                ModelState.AddModelError("", Session["SignInUiError"].ToString());
                Session["SignInUiError"] = null;
            }
            OriginalCall(model);

            return View(model);
        }


        public ActionResult ChangePassword()
        {
            var context = new SitecoreContext();
            var model = context.GetCurrentItem<ChangePassword>();
            model.isPasswordPolicyFail = false;
            model.isShowPasswordUpdated = false;
            SitecoreProfileService scProfileService = new SitecoreProfileService();
            if (Session["isShowPasswordUpdated"] != null)
            {
                model.isShowPasswordUpdated = (bool)Session["isShowPasswordUpdated"];
                Session["isShowPasswordUpdated"] = null;
            }
            if (Session["ChangePasswordError"] != null)
            {
                List<ModelErrorCollection> allerror = (List<ModelErrorCollection>)Session["ChangePasswordError"];
                foreach (var item in allerror)
                {
                    foreach (var subItem in item)
                    {
                        ModelState.AddModelError("", subItem.ErrorMessage.ToString());

                    }

                }

                Session["ChangePasswordError"] = null;
            }

            if (scProfileService.CheckForDisableAccountUpdates())
            {
                model.isAccountLocked = true;
                ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("Profile_AccountNoUpdateAllow"));
            }
            else
            {
                model.isAccountLocked = false;
                if (Request.QueryString["PasswordRuleFail"] != "" && Request.QueryString["PasswordRuleFail"] == "true")
                {
                    var membershipUser = scProfileService.GetCurrentMembershipUser();
                    ProfileService profileService = new ProfileService();

                    profileService.SetLoginWaitContext(membershipUser.Email, scProfileService.RemoveDomainToUserName(membershipUser.UserName), null, null);

                    model.isPasswordPolicyFail = true;

                }
                else
                {
                    //If the user acecss this page using Self service they MUST be authenticated If not we send them back to the Login page
                    BlueGreenContext bgContext = new BlueGreenContext();
                    if (!bgContext.IsAuthenticated)
                    {
                        Response.Redirect(UrlMapper.Map(model.SiteSettings.SignInPage.Url));
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// Change password POST action
        /// </summary>
        [HttpPost]
        public ActionResult ChangePasswordProcess(ChangePassword changePassword)
        {

            var context = new SitecoreContext();
            ChangePassword model = context.GetCurrentItem<ChangePassword>();

            if (!changePassword.isPasswordPolicyFail && !Context.User.IsAuthenticated)
            {
                Response.Redirect(UrlMapper.Map(model.SiteSettings.SignInPage.Url));
                return null;
            }

            model.isPasswordPolicyFail = changePassword.isPasswordPolicyFail;  //Reset the default value
            if (ModelState.IsValid)
            {
                if (changePassword.txtNewPassword.Contains(" "))
                {
                    ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("Profile_PasswordInvalid"));
                }
                else
                {
                    SitecoreProfileService scProfileService = new SitecoreProfileService();

                    var membershipUser = scProfileService.GetCurrentMembershipUser();
                    if (membershipUser.ChangePassword(changePassword.txtCurrentPassword, changePassword.txtNewPassword))
                    {
                        EmailManager.UpdatePassword(membershipUser.UserName, membershipUser.Email);
                        if (changePassword.isPasswordPolicyFail)// Need to Complete the login Proces
                        {
                            Response.Redirect(UrlMapper.Map(model.SiteSettings.SignInWaitPage.Url));
                            return null;
                        }
                        else
                        {


                            Session["isShowPasswordUpdated"] = true;
                            //  model.isShowPasswordUpdated = true;
                            return Redirect(UrlMapper.Map(SitecoreUtils.GetPageUrl(SitecoreItemReferences.ChangePasswordPageId)));

                        }
                    }
                    else
                    {
                        MembershipUser user = Membership.GetUser(Context.User.Name, false);
                        if (user != null)
                        {

                            if (user.IsLockedOut)
                            {

                                var scUser = scProfileService.GetUser(Context.User.Name);
                                Components.EmailManager.ResetEmail(Context.User.Name, scUser.Profile.Email);
                                if (scUser != null && !scProfileService.CheckForPasswordLockedEmail(scUser))
                                {
                                    scUser.Profile.SetCustomProperty(SitecoreProfileService.PasswordLockedEmailId, "1");
                                    scUser.Profile.Save();
                                }
                                Session["SignInUiError"] = Sitecore.Globalization.Translate.Text("Profile_AccountLocked");
                                return Redirect(UrlMapper.Map(model.SiteSettings.SignInPage.Url));

                            }
                        }
                        ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("CurrentPassword_Current_NotCorrect"));
                    }
                }
            }


            var errors = ModelState.Select(x => x.Value.Errors)
            .Where(y => y.Count > 0)
            .ToList();
            if (errors != null && errors.Count > 0)
            {
                Session["ChangePasswordError"] = errors;
            }
            if (changePassword.isPasswordPolicyFail)
            {
                return Redirect(UrlMapper.Map(SitecoreUtils.GetPageUrl(SitecoreItemReferences.ChangePasswordPageId)) + "?PasswordRuleFail=true");
            }
            else
            {
                return Redirect(UrlMapper.Map(SitecoreUtils.GetPageUrl(SitecoreItemReferences.ChangePasswordPageId)));
            }
            //return View(model);
        }



        /// <summary>
        /// Sign in user from external system like VSSA
        /// </summary>
        public void SignInProcess()
        {
            var email = Request.Form["txtEmail"].Trim();
            var ownerArvact = Request.Form["ownerARVACT"].Trim();
            var AgentLoginID = Request.Form["AgentLoginID"].Trim();
            var fromVSSA = Request.Form["fromVSSA"];
            var ownerACCT = Request.Form["ownerACCT"];

            var profileservice = new ProfileService();
            var response = profileservice.LoginUser(email, null, ownerArvact, fromVSSA, ownerACCT, AgentLoginID);
            if (response.IsSuccessfull)
            {
                var context = new SitecoreContext();
                var model = context.GetCurrentItem<SignIn>();
                Response.Redirect(UrlMapper.Map(model.SiteSettings.SignInWaitPage.Url));

            }
            else
            {
                var context = new SitecoreContext();
                var model = context.GetCurrentItem<SignIn>();
                Response.Redirect(UrlMapper.Map(model.SiteSettings.SignInHelpPage.Url));
            }
        }

        [HttpPost]
        public ActionResult SignInSitecore(SignIn signIn)
        {
            //Validate if the fields are populated
            if (string.IsNullOrEmpty(signIn.txtEmail) || string.IsNullOrEmpty(signIn.txtPassword))
            {
                var context = new SitecoreContext();
                var model = context.GetCurrentItem<SignIn>();
                Response.Redirect(UrlMapper.Map(model.SiteSettings.SignInHelpPage.Url));
            }

            //Remove invalid characters from the Email
            if (signIn.txtEmail.Contains(","))
            {
                Session["SignInUiError"] = Sitecore.Globalization.Translate.Text("Profile_AccountNotFound");
                return base.Index();

            }

            ActionResult result = null;
            var profileservice = new ProfileService();

            var loginResponse = profileservice.LoginUser(signIn.txtEmail, signIn.txtPassword, null, null, null, null);
            if (loginResponse.IsSuccessfull)
            {
                var context = new SitecoreContext();
                var model = context.GetCurrentItem<SignIn>();
                //TODO: Enable Below code once owner object is completely ready
                //RedirectRegistrationConfirmation(UrlMapper.Map(registrationInfo.PostbackSuccessPageUrl));
                Response.Redirect(UrlMapper.Map(model.SiteSettings.SignInWaitPage.Url));
            }
            else
            {
                var context = new SitecoreContext();
                var model = context.GetCurrentItem<SignIn>();
                if (loginResponse.errorCode == SignInResponse.errors.InvalidPassword)
                {
                    //Session["PasswordRuleFail"] = "true";  //TODO find another way to get this working
                    Response.Redirect("/mybluegreen/my-account/Change-Password?PasswordRuleFail=true", true);  //TODO define const for URL
                }
                SitecoreProfileService scProfileService = new SitecoreProfileService();
                var scUserName = scProfileService.GetUserByEmail(signIn.txtEmail);
                if (loginResponse.errorCode == SignInResponse.errors.LockedAccount && !scProfileService.CheckForDisableAccountUpdates(scUserName))
                {
                    Session["SignInUiError"] = Sitecore.Globalization.Translate.Text("Profile_AccountLocked");

                }
                else
                {
                    Session["SignInUiError"] = Sitecore.Globalization.Translate.Text("Profile_AccountNotFound");
                }
                result = base.Index();
            }

            return result;
        }

        public ActionResult ResetPassword()
        {
            var context = new SitecoreContext();
            var model = context.GetCurrentItem<ResetPassword>();
            if (Request.QueryString["email"] == null || Request.QueryString["resetid"] == null)
            {
                WebUtil.Redirect("/Home");
            }

            model.email = Request.QueryString["email"];
            model.resetid = FormatUtils.ConvertToGuid(Request.QueryString["resetid"]);

            SitecoreProfileService scProfileService = new SitecoreProfileService();

            var scUserName = scProfileService.GetUserByEmail(model.email);
            ProfileService profileService = new ProfileService();

            var user = scProfileService.GetUser(scUserName);
            if (Session["ResetPasswordError"] != null)
            {
                List<ModelErrorCollection> allerror = (List<ModelErrorCollection>)Session["ResetPasswordError"];
                foreach (var item in allerror)
                {
                    foreach (var subItem in item)
                    {
                        ModelState.AddModelError("", subItem.ErrorMessage.ToString());

                    }

                }

                Session["ResetPasswordError"] = null;
            }
            if (user != null && user.Profile != null)
            {
                Guid userResetId = FormatUtils.ConvertToGuid(user.Profile.GetCustomProperty("Forgot Password Unique Id"));
                var forgotPasswordTimestamp = user.Profile.GetCustomProperty("Forgot Password Timestamp");

                if (Guid.Equals(userResetId, model.resetid))
                {
                    DateTime expireDate = DateUtil.ParseDateTime(forgotPasswordTimestamp, DateTime.MinValue);
                    if (expireDate != DateTime.MinValue)
                    {
                        int forgotPasswordExpirationTime = Int32.Parse(Sitecore.Configuration.Settings.GetSetting("ForgotPasswordExpirationTime"));

                        if (DateTime.Now > expireDate.AddHours(forgotPasswordExpirationTime))
                        {
                            model.hideUIElement = true;
                            ViewData["message"] = model.MessageLinkExpired;

                            // WebUtil.Redirect("/Home");
                        }
                    }
                }
                else
                {
                    model.hideUIElement = true;
                    ViewData["message"] = model.MessageLinkInvalid;
                    //WebUtil.Redirect("/Home");
                }
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult ResetPasswordProcess(ResetPassword resetPassword)
        {
            bool isValid = true;

            if (resetPassword.txtNewPassword.Contains(" "))
            {
                if (ModelState.IsValid)
                {
                    ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("Profile_PasswordInvalid"));
                }
                isValid = false;
            }

            if (ModelState.IsValid && isValid)
            {


                SitecoreProfileService scProfileService = new SitecoreProfileService();
                var scUserName = scProfileService.GetUserByEmail(resetPassword.email);
                MembershipUser user = scProfileService.GetMembershipUser(scUserName);
                if (user != null)
                {

                    user.UnlockUser();

                    scProfileService.ResetFlagPasswordLockedEmail(scUserName);
                    ProfileService profileService = new ProfileService();
                    if (!user.ChangePassword(user.ResetPassword(), resetPassword.txtNewPassword))
                    {
                        ModelState.AddModelError("", "ERROR");  //todo add error
                        return base.Index();
                    }
                    else
                    {
                        EmailManager.UpdatePassword(scUserName, resetPassword.email);

                        var response = profileService.LoginUser(resetPassword.email, resetPassword.txtNewPassword, null, null, null, null);
                        if (response.IsSuccessfull)
                        {
                            var context = new SitecoreContext();
                            var model = context.GetCurrentItem<SignIn>();
                            profileService.SetLoginWaitContext(resetPassword.email, null, null, null);
                            Response.Redirect(UrlMapper.Map(model.SiteSettings.SignInWaitPage.Url));
                        }
                        else
                        {

                            ModelState.AddModelError("", "ERROR");  //todo add error
                            return base.Index();

                        }
                    }
                }

            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                Session["ResetPasswordError"] = errors;

                Response.Redirect(string.Format("/reset-password?email={0}&resetid={1}", resetPassword.email, resetPassword.resetid));
            }
            return null;
        }

        [HttpPost]
        public ActionResult Register(Registration registrationInfo)
        {

            var profileservice = new ProfileService();

            if (registrationInfo.btnSubmit == "action:lookup")
            {
                foreach (var modelValue in ModelState.Values)
                {
                    modelValue.Errors.Clear();
                }
                if (string.IsNullOrEmpty(registrationInfo.txtLookupSSN) || string.IsNullOrEmpty(registrationInfo.txtLookupPhone))
                {
                    ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("Register_AccountNotFound"));
                }
                else
                {
                    var ownerId = profileservice.GetOwnerNumber(registrationInfo.txtLookupSSN, registrationInfo.txtLookupPhone);

                    if (string.IsNullOrEmpty(ownerId))
                    {
                        ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("Register_AccountNotFound"));
                    }
                    else
                    {
                        ValueProviderResult temp = new ValueProviderResult(ownerId, ownerId, CultureInfo.InvariantCulture);
                        ModelState["txtOwnerId"].Value = temp;
                    }
                }
            }
            else if (registrationInfo.btnSubmit == "action:register")
            {
                if (ModelState.IsValid)
                {
                    if (registrationInfo.txtPassword.Contains(" "))
                    {
                        ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("Profile_PasswordInvalid"));

                    }
                    else
                    {
                        RegisterServiceModel serviceModel = new Services.RegisterServiceModel();
                        SitecoreProfileService scProfileService = new SitecoreProfileService();
                        ProfileService profileService = new ProfileService();

                        var scUserName = scProfileService.AddDomainToUserName(registrationInfo.txtOwnerId);

                        if (scProfileService.SitecoreExists(scUserName))
                        {
                            var user = scProfileService.GetUser(scUserName);


                            if (user.Profile.Email == registrationInfo.txtAcctEmail)
                            {
                                ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("Register_EmailAlreadyRestierToSameOwner"));
                            }
                            else
                            {
                                ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("Register_OwnerAlreadyRegister"));
                            }
                        }
                        else
                        {

                            bool allowRegistration = false;
                            var username = scProfileService.GetUserByEmail(registrationInfo.txtAcctEmail);
                            if (string.IsNullOrEmpty(username))
                            {
                                var legacyUser = profileService.GetOwnerDemographic(registrationInfo.txtAcctEmail, null);
                                if (legacyUser == null)
                                {
                                    allowRegistration = true;
                                }
                                else if (legacyUser.OwnerId == registrationInfo.txtOwnerId)
                                {
                                    allowRegistration = true;


                                }
                            }
                            //Verify if the email already exists

                            if (allowRegistration)
                            {

                                serviceModel.Email = registrationInfo.txtAcctEmail;
                                serviceModel.OwnerId = registrationInfo.txtOwnerId;
                                serviceModel.Password = registrationInfo.txtPassword;
                                serviceModel.Phone = registrationInfo.txtPhone;
                                serviceModel.SSN = registrationInfo.txtSSN;

                                if (profileservice.Register(serviceModel))
                                {
                                    var loginResponse = profileservice.LoginUser(registrationInfo.txtAcctEmail, registrationInfo.txtPassword, null, null, null, null);

                                    Session["LoginEmail"] = registrationInfo.txtAcctEmail;
                                    Session["LoginPassword"] = registrationInfo.txtPassword;
                                    Session["EnrollAcctNo"] = registrationInfo.txtOwnerId;
                                    Session["ownerACCT"] = registrationInfo.txtOwnerId;
                                    Session["ownerRegisterReferrer"] = "Register";
                                    Response.Redirect(UrlMapper.Map(registrationInfo.PostbackSuccessPageUrl), false);
                                    return null;
                                    //RedirectRegistrationConfirmation(UrlMapper.Map(registrationInfo.PostbackSuccessPageUrl));
                                }
                                else
                                {
                                    ViewData["ShowUnsuccessMessage"] = "true";
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("Register_EmailMustBeUnique"));

                            }
                        }
                    }
                }
            }
            return base.Index();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword form)
        {

            if (ModelState.IsValid)
            {
                SitecoreProfileService scProfileService = new SitecoreProfileService();
                ProfileService profileService = new ProfileService();

                var scUser = scProfileService.GetUserByEmail(form.txtEmail);

                if (scProfileService.SitecoreExists(scUser))
                {
                    if (scProfileService.CheckForDisableAccountUpdates(scUser))
                    {
                        form.isAccountLocked = true;
                        ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("Profile_AccountNoUpdateAllow"));
                    }
                    else
                    {
                        if (EmailManager.ResetEmail(scUser, form.txtEmail))
                        {
                            // ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("Profile_forgotPassword_emailsend"));
                            ViewData["message"] = Sitecore.Globalization.Translate.Text("Profile_forgotPassword_emailsend");

                        }
                        else
                        {
                            ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("profile_forgotPassword_emailfail"));
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", Sitecore.Globalization.Translate.Text("Profile_ForgotPassword_EmailDonotExist"));
                }
            }

            return base.Index();
        }


        private void OriginalCall(SignIn model)
        {
            /**************************************************************
             * LoginUtils.VacationsRedirect(Request.Url.Host.ToLower());
             *************************************************************/
            if (Request.Url.Host.ToLower() == "dev.bluegreenonline.com")
            {
                Response.Redirect("http://mdev.bluegreenvacations.com/");
            }
            else if (Request.Url.Host.ToLower() == "stg.bluegreenonline.com")
            {
                Response.Redirect("http://stg.bluegreenvacations.com/");
            }
            else if (Request.Url.Host.ToLower() == "bluegreenonline.com" | Request.Url.Host.ToLower() == "www.bluegreenonline.com")
            {
                Response.Redirect("http://www.bluegreenvacations.com/");
            }

            // (LoginUtils.IsBgvfsReferrer(referrer)
            if (Request.Url.ToString().ToLower().Contains("signoff=true"))
            {
                // BlueGreenContext.DigestRequest()
                if (Request.Cookies["OwnerInfo"] != null)
                {
                    model.OwnerId = Request.Cookies["OwnerInfo"]["OwnerId"];
                    model.OwnerType = Request.Cookies["OwnerInfo"]["OwnerType"];
                    model.TPStatus = Request.Cookies["OwnerInfo"]["TPStatus"];
                }
                // BlueGreenContext.DigestRequest();
                System.Web.Security.FormsAuthentication.SignOut();
                foreach (string cookie in Request.Cookies.AllKeys)
                {
                    Request.Cookies.Remove(cookie);
                }

                bxgOwner = (BGO.OwnerWS.Owner)Session["BXGOwnerMedallia"];

                // BlueGreenContext.DigestRequest()
                if ((bxgOwner != null))
                {
                    HasOwnerInformation = true;
                    model.OwnerId = bxgOwner.Arvact;

                    if (bxgOwner.User[0].isSampler)
                    {
                        model.OwnerType = "SAMPLER";
                    }
                    else
                    {
                        if ((string)Session["OwnerContractType"] == "Vacation Club")
                        {
                            model.OwnerType = "VACCLUB";
                        }
                        else
                        {
                            model.OwnerType = "TRADITIONAL";
                        }
                    }

                    if (bxgOwner.TravelerPlusMembership.IsTravelerPlusEligible)
                    {
                        if (bxgOwner.TravelerPlusMembership.AccountExpired)
                        {
                            model.TPStatus = "EXPIRED";
                        }
                        else
                        {
                            model.TPStatus = "ACTIVE";
                        }
                    }
                    else
                    {
                        model.TPStatus = "NOTELIGIBLE";
                    }
                }
            }

            // this.SetPostData()
            string tutorialRedirect = (string)Session["ReferrerURL"];
            if (tutorialRedirect != null)
            {
                if (tutorialRedirect.Contains("/tutorials/default.aspx"))
                {
                    model.IsTutorialTransfer = "True";
                }
            }

            //Check for user redirected for login. Assign the path info to  
            //a variable to re-assign to session variable in case of session values cleared

            //If Session("_path_info") <> "" Then
            //    path_info = Session("_path_info")
            //End If


            if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
            {
                path_info = Request.QueryString["ReturnUrl"];
                path_info = path_info.Replace("%2f", "/");
            }

            model.sMessage = "";
            model.IsTravelerPlusLogin = "";
            model.IsEncoreRewardsLogin = "";

            //Sales users that are logged in go to the sales login page
            if ((string)Session["SalesUser"] == "BLUEGREEN")
            {
                Response.Redirect("SalesLogin.aspx", true);
            }

            HandleRedirects(model);

            string email_id = null;
            string pass = null;
            email_id = (string)Session["email_id"];
            pass = (string)Session["password"];

            //Allow Owner support agents to login as the owner from their application
            if (!string.IsNullOrEmpty(Request.Form["AgentID"]))
            {
                string sURL = Request.ServerVariables["HTTP_REFERER"] + "";
                if (Request.Form["AgentID"] == "SALESKIOSK" & sURL.ToLower().IndexOf("saleskiosk") > -1)
                {
                    email_id = "travelerplus@bxgcorp.com";
                    pass = "hut44#";
                }
                else if (Request.Form["AgentID"] == "SALESKIOSKVC" & sURL.ToLower().IndexOf("saleskioskvc") > -1)
                {
                    email_id = "vacationclub@bxgcorp.com";
                    pass = "hut44#";
                }
                else
                {
                    email_id = Request.Form["AgentLoginEmail"];
                    pass = Request.Form["AgentLoginPassword"];
                }
                model.AgentLoginID = Request.Form["AgentID"];
            }

            sEmail = "";
            sPass = "";
            bxgOwner = (BGO.OwnerWS.Owner)Session["BXGOwnerMedallia"];
            string ownerContractType = (string)Session["OwnerContractType"];

            Session.Clear();
            Session["BXGOwnerMedallia"] = bxgOwner;
            Session["OwnerContractType"] = ownerContractType;

            //re-assign path info to session variable
            Session["_path_info"] = path_info;
            if (!IsPostBack)
            {
                try
                {
                    //Allow logins from elsewhere to feed into this page
                    if (!string.IsNullOrEmpty(email_id) & !string.IsNullOrEmpty(pass))
                    {
                        sEmail = email_id;
                        sPass = pass;
                    }
                }
                catch
                {
                }
            }

            //Display message for session timeouts
            if (Request.QueryString["sess"] == "timeout")
            {
                model.sMessage = UiUtils.BuildMessage("Either you are requesting a page that requires sign in for access, or your previous Bluegreen Online session timed out after 20 minutes of inactivity. Please sign in below.");
            }
            //Display message for blocked accounts
            if (Request.QueryString["acctstat"] == "block")
            {
                model.sMessage = UiUtils.BuildMessage("Your account does not qualify for online access at this time.  <BR>Please contact us at 800.456.CLUB(2582) and select option 2 to learn how to enable your account.");

            }
            if (!string.IsNullOrEmpty(Request.QueryString["ErrMessage"]))
            {
                model.sMessage = UiUtils.BuildMessage("Oasis Lakes owners: <font color='#000000'>The association news page has moved. To access it, please sign in below and click on the picture of The Fountains/Oasis Lakes clubhouse at the top of the home page. Then click on the <strong style='font-size:8pt'>Association Owners</strong> link on the left side of the Resort Detail page. If you have not yet enrolled in Bluegreen Online, click on the <strong style='font-size:8pt'>Not registered?</strong> link to do so.</font>");

            }

            //Display an error at the top of the login page if the Login was unsuccessful
            if (Request.QueryString["error"] == "NoConn")
            {
                model.sMessage = UiUtils.BuildMessage("We have encountered an unexpected error. Please wait a few minutes and try to log in again. We apologize for the inconvenience.");
            }

            if (Request.QueryString["lo"] == "1" | Request.QueryString["error"] == "NoACK")
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();

                if (Request.Cookies["ASP.NET_SessionId"] != null)
                {
                    Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                    Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                }
            }
        }

        private void HandleRedirects(SignIn model)
        {
            //Get the HTTP_HOST AND HTTP_REFERER
            string sHost = Request.ServerVariables["HTTP_HOST"].ToLower().Replace("www.", "");
            string sHostTP = (Request.ServerVariables["HTTP_HOST"] + Request.ServerVariables["PATH_INFO"]).ToLower().Replace("www.", "");
            sReferrer = Request.ServerVariables["HTTP_REFERER"] + "";
            string sRedirectURL = null;

            bxgOwner = (BGO.OwnerWS.Owner)Session["BXGOwnerMedallia"];
            string ownerContractType = (string)Session["OwnerContractType"];
            Session.Clear();

            Session["BXGOwnerMedallia"] = bxgOwner;
            Session["OwnerContractType"] = ownerContractType;


            if (ConfigurationFactory.LogLoginProcess)
            {
                BXGDiagnostics.EventLogging objLogging = new BXGDiagnostics.EventLogging("BlueGreenOnline", "LoginProcess");
                objLogging.LogEvent("Host: " + sHost + "HostTP: " + sHostTP + " Refferrer: " + sReferrer, System.Diagnostics.EventLogEntryType.Information, true);
                objLogging.LogEvent("sHostTP.ToLower().IndexOf(encorerewards): " + sHostTP.ToLower().IndexOf("encorerewards"), System.Diagnostics.EventLogEntryType.Information, true);
                objLogging = null;
            }

            if (sReferrer.IndexOf(sHost) == -1)
            {
                //Are we a traveler plus login?
                if ((sHostTP.ToLower().IndexOf("travelerplus") > 0))
                {
                    model.IsTravelerPlusLogin = "TRUE";
                    //imgLogo.ImageUrl = "TravelerPlus/owner/images/bgtp_logo_lrg.gif"

                    //Are we a encore rewards login?
                }
                else if ((sHostTP.ToLower().IndexOf("encorerewards") >= 0))
                {
                    model.IsEncoreRewardsLogin = "TRUE";
                    //imgLogo.ImageUrl = "images/e_logo.gif"
                }
                else
                {
                    //If we came from somewhere else then check our redirect list
                    clsDBConnectivity dbCon = new clsDBConnectivity();
                    if (dbCon.dbCmnd != null)
                    {
                        dbCon.dbCmnd.CommandText = "uspCheckForRedirection";
                        dbCon.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure;
                        dbCon.dbCmnd.Parameters.Clear();
                        //dbCon.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@URL", System.Data.SqlDbType.VarChar, 50)).Value = sHost;
                        //sRedirectURL = dbCon.dbCmnd.ExecuteScalar();
                        //dbCon.Close();
                        dbCon = null;
                    }

                    //We found a redirect page, so lets go there
                    if (sRedirectURL != null)
                    {
                        Response.Redirect(sRedirectURL, true);
                    }
                }
            }
        }


        #region sd-attempt

        private void ProcessRequest(SignIn model)
        {
            BlueGreenContext bgContext;
            string redirect = LoginUtils.VacationsRedirect(Request.Url.Host.ToLower());
            if (String.IsNullOrEmpty(redirect))
            {
                Response.Redirect(redirect);
            }


            string referrer = Request.ServerVariables["HTTP_REFERER"];

            if (LoginUtils.IsBgvfsReferrer(referrer))
            {
                bgContext = new BlueGreenContext();
                //bgContext.DigestRequest();

            }

            if (SetPostData(model))
            {
                HandleRedirects(model);
            }


            bgContext = new BlueGreenContext();
            //bgContext.CleanSession();

        }

        /// <summary>
        /// Sets the post data hidden fields 
        /// </summary>
        private bool SetPostData(SignIn model)
        {
            bool result = false;
            string tutorialRedirect = (string)Session["ReferrerURL"];
            if (tutorialRedirect != null)
            {
                if (tutorialRedirect.Contains("/tutorials/default.aspx"))
                {
                    model.IsTutorialTransfer = "True";
                }
            }

            string sHost = Request.ServerVariables["HTTP_HOST"].ToLower().Replace("www.", "");
            string sHostTP = (Request.ServerVariables["HTTP_HOST"] + Request.ServerVariables["PATH_INFO"]).ToLower().Replace("www.", "");
            sReferrer = Request.ServerVariables["HTTP_REFERER"] + "";
            if (sReferrer.IndexOf(sHost) == -1)
            {
                //Are we a traveler plus login?
                if ((sHostTP.ToLower().IndexOf("travelerplus") > 0))
                {
                    model.IsTravelerPlusLogin = "TRUE";
                    //imgLogo.ImageUrl = "TravelerPlus/owner/images/bgtp_logo_lrg.gif"

                    //Are we a encore rewards login?
                }
                else if ((sHostTP.ToLower().IndexOf("encorerewards") >= 0))
                {
                    model.IsEncoreRewardsLogin = "TRUE";
                    //imgLogo.ImageUrl = "images/e_logo.gif"
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }


        #endregion

        #region Profile 

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult MyContactUpdate(MyProfileResponseParameters model)
        {
            Session["ChangedCountry"] = null;
            ViewData["SuggestedAddress"] = null;
            MyProfileResponseParameters Suggestion = new MyProfileResponseParameters();
            ProfileService psService = new ProfileService();
            if (ModelState.IsValid)
            {
                if (!model.Address_Country.Equals("us", StringComparison.CurrentCultureIgnoreCase))
                {
                    model.Address_City = "";
                    model.Address_State = "";
                    model.Address_ZipCode = "";
                }

                if (model.correct_address != null && Session["suggestion"] != null)
                {
                    var objectToConsider = model.correct_address == "no" ? (MyProfileResponseParameters)Session["suggestion"] : model;
                    objectToConsider.isPaperLessSelected = model.isPaperLessSelected;
                    var listOfServiceErrors = psService.UpdateProfileEmailAndDemoGraphics(objectToConsider);
                    if (listOfServiceErrors.Count > 0)
                    {
                        if (listOfServiceErrors.Count == 1 && listOfServiceErrors.FirstOrDefault().ErrorMessage == "ShowUnsuccessMessage")
                        {
                            //Generic message Caused due to network issues
                            ViewData["ShowUnsuccessMessage"] = "true";
                        }
                        else
                        {
                            foreach (var vi in listOfServiceErrors)
                            {
                                ModelState.AddModelError("", vi.ErrorMessage);
                            }
                        }
                    }
                    else
                    {
                        ViewData["ShowSuccessMessage"] = "true";
                        ModelState.Clear();
                    }
                    ViewData["SuggestedAddress"] = null;

                    if (!string.IsNullOrEmpty(model.successPageUrl) && (string)ViewData["ShowSuccessMessage"] == "true")
                    {
                        RedirectToRegistrationSuccess(UrlMapper.Map(model.successPageUrl));
                        return null;
                    }
                    else
                    {
                        return base.Index();
                    }
                }

                var listOfError = ValidationUtils.GetMyProfileViolations(model);
                if (listOfError.Count == 0 && model.Address_Country.Equals("us", StringComparison.CurrentCultureIgnoreCase))
                {
                    listOfError = ValidationUtils.GetMelissaLookUpViolations(model, ref Suggestion);
                    Session["suggestion"] = Suggestion;
                    if (!string.IsNullOrEmpty(Suggestion.Address_AddressLine1))
                    {
                        ViewData["SuggestedAddress"] = "true";
                        ViewData["SuggestedAddressDetail"] = ValidationUtils.AddressSuggestionFormat(Suggestion);
                        ViewData["ActualAddressDetail"] = ValidationUtils.AddressSuggestionFormat(model);
                    }
                    else
                    {
                        if (listOfError.Count == 0)
                        {
                            ViewData["SuggestedAddress"] = null;
                        }
                        else
                        {
                            ViewData["SuggestedAddress"] = "false";
                            ViewData["ActualAddressDetail"] = ValidationUtils.AddressSuggestionFormat(model);
                        }
                    }
                }
                if (listOfError.Count > 0)
                {

                    Session["ChangedCountry"] = model.Address_Country;
                    foreach (var vi in listOfError)
                    {
                        ModelState.AddModelError("", vi.ErrorMessage);
                    }
                }
                else
                {
                    ViewData["SuggestedAddress"] = ViewData["SuggestedAddress"] == null ? null : ViewData["SuggestedAddress"];
                    if (ViewData["SuggestedAddress"] == null || ViewData["SuggestedAddress"].ToString() == "false")
                    {
                        var listOfServiceErrors = psService.UpdateProfileEmailAndDemoGraphics(model);
                        if (listOfServiceErrors.Count > 0)
                        {
                            if (listOfServiceErrors.Count == 1 && listOfServiceErrors.FirstOrDefault().ErrorMessage == "ShowUnsuccessMessage")
                            {
                                //Generic message Caused due to network issues
                                ViewData["ShowUnsuccessMessage"] = "true";
                            }
                            else
                            {
                                foreach (var vi in listOfServiceErrors)
                                {
                                    ModelState.AddModelError("", vi.ErrorMessage);
                                }
                            }
                        }
                        else
                        {
                            ViewData["ShowSuccessMessage"] = "true";
                            ModelState.Clear();
                        }

                    }
                }
            }
            if (!string.IsNullOrEmpty(model.successPageUrl) && (string)ViewData["ShowSuccessMessage"] == "true")
            {
                RedirectToRegistrationSuccess(UrlMapper.Map(model.successPageUrl));
                return null;
            }
            else
            {
                return base.Index();
            }
        }
        private void RedirectRegistrationConfirmation(string successPageUrl)
        {
            Session["registeredSuccessPageUrl"] = successPageUrl;
            Response.Redirect("/Login Wait");
        }

        private void RedirectToRegistrationSuccess(string successPageUrl)
        {
            EmailManager.SendRegistrationConfirmationEmail();
            Session["ownerRegisterReferrer"] = null;
            Response.Redirect(successPageUrl, false);
            return;
        }

        #endregion

    }
}