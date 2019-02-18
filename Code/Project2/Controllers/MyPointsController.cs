using BGModern.Classes;
using BGModern.Classes.Utilities;
using BGModern.Mappers;
using BGModern.Models;
using System.Configuration;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using BGO.OwnerWS;


namespace BGModern.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    public class MyPointsController : RenderMvcController
    {
        private BGO.OwnerWS.Owner BXGOwner = new BGO.OwnerWS.Owner();
        private BGO.OwnerWS.OwnerWS1SoapClient OwnerService = new BGO.OwnerWS.OwnerWS1SoapClient();
        

        public ActionResult MyPoints()
        {
            ValidateSession();

            //TODO:  Web Service populate of Owner
            BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            MyPointsModel myPoints = MyPointsMapper.Map(CurrentPage);
            myPoints = MasterMapper.Map(myPoints, CurrentPage);
            
            InitializePageView(myPoints);
            HydrateModel(myPoints);

            //return a view       

            if (mdlCheckAS400.IsAS400Available())
            return View(myPoints);
            else
                return Redirect("../siteMaintenance.aspx");
        }

        private void InitializePageView(MyPointsModel model)
        {
            //ValidateSession();

            if ((BXGOwner.User[0] != null))
            {
                model.IsSamplerOwner = BXGOwner.User[0].isSampler;
                model.HomeProject = BXGOwner.User[0].HomeProject;
            }
            else
            {
                model.IsSamplerOwner = false;
                model.HomeProject = "0";
            }

            if (model.IsSamplerOwner == true)
            {
                model.HideAccountInfo = true;
                model.HidePanelChoice = true;
                model.HideLinkChoice = true;
                model.HideLinkGoGreen = true;
                model.HideSavePointsButton = true;
            }
            else
            {
                model.HideLinkGoGreen = false;
                model.HideLinkChoice = false;
                model.HideSavePointsButton = false;
            }

            model.HidePaymentInfo = true;

            if (Session["OwnerContractType"].ToString() != "Vacation Club" || (Session["OwnerContractType"].ToString() == "Vacation Club" && model.IsSamplerOwner))
            {
                model.HidePageDisclaimer = true;
                model.HideLinkMyPoints = true;
            }
            else
            {
                model.HidePageDisclaimer = false;
                model.HideLinkMyPoints = false;
            }

            if (BXGOwner.membershipLevel != "V" && Session["OwnerContractType"].ToString() == "Vacation Club" && !model.IsSamplerOwner)
                model.HidePremier = false;
            else
                model.HidePremier = true;

            if (BXGOwner.membershipLevel == "P" || BXGOwner.membershipLevel == "G")
                model.HideFreeStay = false;
            else
                model.HideFreeStay = true;

            if (BXGOwner.User[0].HomeProject == "51" || BXGOwner.User[0].HomeProject == "52")
                model.HideRestricted = true;
            else
                model.HideRestricted = false;

            if (Request.QueryString["display"] == null)
                model.HidePanelReminder = true;

            if (BXGOwner.Eligible4kFlagged && BXGOwner.Eligible4kOnAnniversaryDateWindow && !BXGOwner.Eligible4kRequested && !BXGOwner.User[0].isSampler)
                model.Show4K = true;
            else
                model.Show4K = false;
               
        }

        private void HydrateModel(MyPointsModel model)
        {
            //ValidateSession();
            PopulateAcctContractInfo(model);
        }

        private void PopulateAcctContractInfo(MyPointsModel model)
        {
            BGO.savePointsWS.MDPNTWSPortTypeClient theSavingPoints = new BGO.savePointsWS.MDPNTWSPortTypeClient();
            BGO.savePointsWS.PNTBALLISTWSInput _elect = new BGO.savePointsWS.PNTBALLISTWSInput();
            BGO.savePointsWS.PNTBALLISTWSResult _electResult = new BGO.savePointsWS.PNTBALLISTWSResult();
            bool needtosave = false;
            bool willbesaved = false;

            _elect._PNTBALLISTPR = new BGO.savePointsWS.PNTBALLISTDS();
            _elect._PNTBALLISTPR._LISTTYPE = Constants.SavePointsListType;
            _elect._PNTBALLISTPR._OWNERNUMBER = BXGOwner.Arvact;
            _elect._PNTBALLISTPR._POINTTYPES = Constants.SavePointsBuckets;
            _electResult = theSavingPoints.pntballistws(_elect);

            if (_electResult._PNTBALLISTWSDI != null)
            {
                for (int x = 0; x <= _electResult._PNTBALLISTWSDI.Length - 1; x++)
                {
                    string strExpireDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(_electResult._PNTBALLISTWSDI[x]._ENDDATE));

                    if (!needtosave && Convert.ToDateTime(strExpireDate) >= System.DateTime.Today && _electResult._PNTBALLISTWSDI[x]._OPTIONPAID == "0"
                        && Convert.ToInt32(_electResult._PNTBALLISTWSDI[x]._POINTBAL) > 0)
                    {
                        needtosave = true;
                    }
                    else if (!needtosave && !willbesaved && Convert.ToDateTime(strExpireDate) >= System.DateTime.Today && _electResult._PNTBALLISTWSDI[x]._OPTIONPAID == "1"
                        && Convert.ToInt32(_electResult._PNTBALLISTWSDI[x]._POINTBAL) > 0)
                    {
                        willbesaved = true;
                    }
                }
            }

            if (!needtosave && willbesaved)
            {
                model.Message = "<p> The eligible Points in your account(s) will be automatically saved at the end of their expiration date for an additional year of use in Red, White and Blue seasons. Thank you for electing to save your Points.</p>";
                model.HideSavePointsButton = true;
            }
            else if (needtosave)
                model.HideSavePointsButton = false;
            else if (!needtosave && !willbesaved)
            {
                model.Message = "<p>You do not currently have any Points in your account that are eligible to be saved.</p>";
                model.HideSavePointsButton = true;
            }

            try
            {
                //Final check for hidesavepoints button
                if (model.HomeProject == "52")
                {
                    model.HideSavePointsButton = false;
                    model.Message = String.Empty;
                    model.HidePanelReminder = true;
                    MyPointsModel myPoints = new MyPointsModel();
                    myPoints.Message = String.Empty;
                    myPoints.ConvertYourPoints = String.Empty;
                    if (BXGOwner.AnnualPointsExpiration.SavePointsEligible && BXGOwner.AnnualPointsExpiration.SavePointsEligible.ToString().Trim().Length > 1)
                        model.HideSavePointsButton = true;
                }
                else
                {
                    if (BXGOwner.AnnualPointsExpiration.SavePointsEligible && BXGOwner.AnnualPointsExpiration.SavePointsEligible.ToString().Trim().Length > 1)
                        model.HideSavePointsButton = false;
                }
            }
            catch (Exception ex)
            {
                model.HideSavePointsButton = true;
            }
            if (model.IsSamplerOwner)
            {
                model.HideSavePointsButton = true;
            }
            
            //------------------------------------------------------
            //Fetching Owner Information from WebService OwnerWS1
            //------------------------------------------------------
            BGO.OwnerWS.AccountInfo[] accountList = null;

            model.AccountInfo = new List<AccountModel>();

            //Call to Web Service Returning AccountInfo
            accountList = OwnerService.fetchOwnerAccounts(BXGOwner.Arvact);
            //TODO: Replace this with line above when actually connecting to WebService For now Just clone the values in StubMe out
            //accountList = (OwnerWS.AccountInfo[])BXGOwner.Accounts.Clone();

            for (int x = 0; x <= accountList.Length - 1; x++)
            {
                if (accountList[x].Proj != "51" & accountList[x].Proj != "52")
                {
                    DateTime strExpireDate = default(DateTime);
                    if (accountList[x].Proj == "50")
                    {
                        strExpireDate = DateTime.ParseExact(accountList[x].Expiration, "yyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        accountList[x].Expiration = string.Format("{0:MM/dd/yyyy}", strExpireDate);
                    }

                    //Populate Results to Model
                    AccountModel acct = new AccountModel();

                    acct.AccNo = accountList[x].index;
                    acct.AccountNumber = accountList[x].AcctNum;
                    acct.Description = accountList[x].projNM;
                    if (accountList[x].Expiration == "0")
                        acct.NextEarnDate = "";
                    else
                        acct.NextEarnDate = accountList[x].Expiration;

                    if (accountList[x].NextEarnAmount == "0")
                        acct.NextEarnAmount = "";
                    else
                        acct.NextEarnAmount = accountList[x].NextEarnAmount;

                    model.AccountInfo.Add(acct);
                }
            }

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