using BGSitecore.Components;
using BGSitecore.Models.MortgageService;
using BGSitecore.Models.Payments;
using BGSitecore.Services;
using BGSitecore.Utils;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BGSitecore.Models;
using BGSitecore.Models.Common;
using Newtonsoft.Json;
using Sitecore.Mvc.Configuration;
using BGSitecore.Helper.Payments;

namespace BGSitecore.Controllers
{
    public class PaymentsController : GlassController
    {
        #region properties

        private SiteSettings SiteSettings
        {
            get
            {
                return new SiteSettings();
            }
        }
        #endregion
        public PaymentsController()
        {

        }

        /// <summary>
        /// Payment Reminder Message
        /// </summary>
        /// <returns>View from </returns>
        // GET: Payments
        public ActionResult PaymentReminder()
        {
            AccountsBalanceHelper accountsBalanceHelper = new AccountsBalanceHelper();
            BlueGreenContext bg = new BlueGreenContext();
            if (bg?.bxgOwner == null)
                return Redirect(UrlMapper.Map(SiteSettings.SignInPage.Url));

            if (bg.IsInstallmentPlan())
                return RedirectToActionPermanent("AccountsBalance");

            var model = accountsBalanceHelper.GetPaymentContentFromPaymentConfig(bg);
            return View(model);
        }

        public ActionResult PaymentOptions()
        {
            BlueGreenContext bg = new BlueGreenContext();
            if (bg?.bxgOwner == null)
                return Redirect(UrlMapper.Map(SiteSettings.SignInPage.Url));
            var accountInfo = Session["AccountDetails"] as AccountViewModel;
            var paymentOptions = GetDataSourceItem<PaymentOptions>();
            paymentOptions.AccountInfo = accountInfo;
            ViewBag.TotalDue = Session["TotalDue"];
            return View(paymentOptions);

        }

        public ActionResult AccountsBalance()
        {
            //Redirect Based on Business Rules
            PaymentUtils.PrePageLoadRedirectUrlsAccountsBalance();

            AccountsBalanceHelper accountsBalanceRepo = new AccountsBalanceHelper();
            var model = accountsBalanceRepo.GetOwnerAssociationBalance();
            Session["AccountPaymentData"] = model.AccountList;
            return View(model);
        }

        #region Helper Methods
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AccountsBalance(AccountViewModel account)
        {
            AccountsBalanceHelper accountsBalanceHelper = new AccountsBalanceHelper();
            try
            {
                if (!ModelState.IsValid || !accountsBalanceHelper.ValidateAccountData(account))
                    throw new Exception();

                Session.Add("AccountDetails", account);
                //Session["PaymentProjNo"] = account.GroupID;
                //Session["ProjectName"] = account.GroupTitle;
                //Session["TotalDue"] = account.UserPayInfoList.Select(x => x.PaymentAmount).Sum();

                //var accountData = accountsBalanceHelper.GetAccountInfoForProject(account.GroupID);
                //var accList = account.UserPayInfoList.Select<AccountBalanceInfo, AccountDetailsForACH>(x => GetAccountDetailsForACH(x, account.GroupID)).ToList();
                //Session["AccountList"] = accList;
                //Session["accNumList"] = accList.Select(x => x.accountNumber).ToList();
                //Session["PaymentPayments"] = accList.Select(x => x.paymentamount).ToList();
                //Session["ArdaCollections"] = account.UserPayInfoList.Select(x => !x.IsARDAAmount && x.ARDAAmount > 0 ? "Y" : "").ToList();

                var context = new SitecoreContext();
                PaymentsConfiguration getContextItem = context.GetItem<PaymentsConfiguration>(PaymentsConfiguration.PaymentsConfigurationItem);

                var pathInfo = getContextItem?.PaymentsOptionPage?.Url;
                PaymentUtils.RedirectToPage(pathInfo);
                ModelState.Clear();
                return null;
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                return AccountsBalance();
            }

        }

        private AccountDetailsForACH GetAccountDetailsForACH(AccountBalanceInfo x, int projNumber)
        {
            return x != null ? new AccountDetailsForACH() { accountNumber = x.AccountNunmber, paymentamount = x.PaymentAmount, projNumber = projNumber } : new AccountDetailsForACH();
        }

        private PaymentOptions FilterPaymentOptions(PaymentOptions paymentOptions, string ProjectID, bool isIP)
        {
            var options = paymentOptions.AllPaymentOptions.ToList();
            paymentOptions.AllPaymentOptions = paymentOptions.AllPaymentOptions.Where(x => x.Code == "CC" || (x.Code == "SA" && ProjectID == "50") || (x.Code == "IP" && isIP));
            return paymentOptions;
        }

        [HttpPost]
        public ActionResult PaymentOptions(PaymentOptionSubmit value)
        {
            Session["TotalDue"] = value.Amount.ToString();

            switch (value.Code)
            {
                case "CC":
                    Session["PaymentType"] = "creditcard";
                    return Redirect("payinfo.aspx");
                case "SA":
                    Session["PaymentType"] = "installmentnew";
                    return Redirect("payInstallPlan.aspx");

                case "IP":
                    Session["PaymentType"] = "installmentnew";
                    return Redirect("payInstallPlan.aspx");

                default: return Json(new CustomAlertMessage() { Text = "No Options is select. There is might be some technical issue. please refresh or contact.", Type = AlertMessageTypes.danger });
            }

        }
        #endregion
        
        public ActionResult CardPayments()
        {            
            var model = GetLayoutItem<CardPayment>();
            return View(model);
        }

        [HttpPost]
        public ActionResult CardPayments(CardPayment paymentDetails)
        {
            //BlueGreenContext bgContext = new BlueGreenContext();
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            Response.Expires = 0;

            CardPaymentHelper cardPaymentRepo = new CardPaymentHelper();
            var model = cardPaymentRepo.SubmitCardPayment(paymentDetails);

            return View(paymentDetails);
        }

        public ActionResult ProcessCardPayment(CardPayment paymentDetail)
        {
            //Redirect Based on Business Rules
            //PaymentUtils.PrePageLoadRedirectUrlsAccountsBalance();

            CardPaymentHelper cardPaymentRepo = new CardPaymentHelper();
            var model = cardPaymentRepo.SubmitCardPayment(paymentDetail);

            return View(model);
        }
    }
}