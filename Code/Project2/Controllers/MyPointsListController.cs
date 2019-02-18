using BGModern.Classes.Utilities;
using BGModern.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;


namespace BGModern.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    public class MyPointsListController : Umbraco.Web.Mvc.SurfaceController
    {
        //private PointsListModel mPointsListModel;

        private BGO.OwnerWS.Owner BXGOwner = new BGO.OwnerWS.Owner();
        private BGO.OwnerWS.OwnerWS1SoapClient OwnerService = new BGO.OwnerWS.OwnerWS1SoapClient();
       
   
        [HttpGet]
        public ActionResult MyPointsList()
        {
            throw new NotImplementedException("Index is not implemented for MyPointsListController");
        }

        public ActionResult GetPartialView(MyPointsListModel model)
        {
            return PartialView("MyPointsList", model);
        }

        public ActionResult GetMyPointsList()
        {
            BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            MyPointsListModel mypoints = new MyPointsListModel();

            if (BXGOwner.User[0].HomeProject == "51" || BXGOwner.User[0].HomeProject == "52")
                mypoints.HideFuturePoints = true;

            PopulateOwnerPointDetails(mypoints);
            PopulateOwnerPointTypes(mypoints);
            PopulateOwnerPointSummary(mypoints);

            //Default Sort
            mypoints.CurrentDetailPoints.OrderBy(x => x.PointTypeDesc);
            Response.Redirect(ConfigurationManager.AppSettings["bxgwebSitecoremyPointsRedirectUrl"], true);

            return PartialView("PointsList", mypoints);
        }

        //public ActionResult SortMyPointsList()
        //{

        //    MyPointsListModel mypoints = new MyPointsListModel();
        //    if (Session["AllDetailPoints"] != null)
        //    {
        //        mypoints = (MyPointsListModel)Session["AllDetailPoints"];
        //    }
        //    else
        //    {
        //        PopulateOwnerPointDetails(mypoints);
        //        PopulateOwnerPointTypes(mypoints);
        //        PopulateOwnerPointSummary(mypoints);
        //        Session["AllDetailPoints"] = mypoints;
        //    }

        //    return PartialView("PointsList", mypoints);
        //}

        private void PopulateOwnerPointDetails(MyPointsListModel model)
        {

            //Global Detail Points listing
            model.AllDetailPoints = new List<PointModel>();

            List<PointModel> pttbl = new List<PointModel>();

            BGO.savePointsWS.MDPNTWSPortTypeClient theSavingPoints = new BGO.savePointsWS.MDPNTWSPortTypeClient();
            BGO.savePointsWS.PNTBALLISTWSInput _elect = new BGO.savePointsWS.PNTBALLISTWSInput();
            BGO.savePointsWS.PNTBALLISTWSResult _electResult = new BGO.savePointsWS.PNTBALLISTWSResult();

            bool willbesaved = false;
            bool needtosave = false;
            string strExpireDate = null;
            DateTime thisDay = DateTime.Today;

            int lastDayOfMonth = 0;
            DateTime dteFirstDayNextMonth = default(DateTime);
            DateTime messagedate = DateTime.Now;

            dteFirstDayNextMonth = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, 1);
            lastDayOfMonth = dteFirstDayNextMonth.AddDays(-1).Day;
            messagedate = DateTime.Parse(DateTime.Now.Month.ToString() + "/" + lastDayOfMonth.ToString() + "/" + DateTime.Now.Year.ToString());

            DateTime AnnualExpEligDate = messagedate.AddMonths(2);//DateAdd(DateInterval.Month, 2, messagedate);

            //call as400 ws for savepoints accunt info
            _elect._PNTBALLISTPR = new BGO.savePointsWS.PNTBALLISTDS();
            _elect._PNTBALLISTPR._LISTTYPE = Constants.SavePointsListType;
            _elect._PNTBALLISTPR._OWNERNUMBER = BXGOwner.Arvact;
            _elect._PNTBALLISTPR._POINTTYPES = Constants.SavePointsBuckets;
            _electResult = theSavingPoints.pntballistws(_elect);

            if ((_electResult._PNTBALLISTWSDI != null))
            {
                for (int x = 0; x <= (_electResult._PNTBALLISTWSDI.Length - 1); x++)
                {
                    //grid will show points details for all owners except for Expired Value sampler and sampler 24
                    if (_electResult._PNTBALLISTWSDI[x]._PROJ_ != "51" && _electResult._PNTBALLISTWSDI[x]._PROJ_ != "52")
                    {

                        PointModel ptsdtl = new PointModel();
                        strExpireDate = string.Format("{0:MM/dd/yyyy}", DateTime.Parse(_electResult._PNTBALLISTWSDI[x]._ENDDATE));

                        //Populate Results to Model
                        ptsdtl.AcctNum = _electResult._PNTBALLISTWSDI[x]._ACCT_.Trim();
                        ptsdtl.ExpireDate = Convert.ToDateTime(strExpireDate);
                        ptsdtl.PointBal = Convert.ToInt32(_electResult._PNTBALLISTWSDI[x]._POINTBAL);
                        ptsdtl.Action = _electResult._PNTBALLISTWSDI[x]._OPTIONPAID;
                        ptsdtl.PointTypeDesc = _electResult._PNTBALLISTWSDI[x]._REFCODE;

                        //TODO: Validate this logic
                        //validate owner has more the save points set
                        if (needtosave == false & DateTime.Parse(strExpireDate) >= thisDay & _electResult._PNTBALLISTWSDI[x]._OPTIONPAID == "0" & Convert.ToInt32(_electResult._PNTBALLISTWSDI[x]._POINTBAL) > 0)
                        {
                            needtosave = true;
                        }
                        else if (needtosave == false & willbesaved == false & DateTime.Parse(strExpireDate) >= thisDay & _electResult._PNTBALLISTWSDI[x]._OPTIONPAID == "1" & Convert.ToInt32(_electResult._PNTBALLISTWSDI[x]._POINTBAL) > 0)
                        {
                            willbesaved = true;
                        }

                        //pttbl.Rows.Add(tbl_row);
                        //Add to points
                        pttbl.Add(ptsdtl);
                        //model.AllPoints.AllDetailPoints.Add(ptsdtl);
                    }
                }
            }

            //override as400 message
            if (needtosave == false & willbesaved == true)
            {
                ViewBag.Message = "<p> The eligible Points in your account(s) will be automatically saved at the end of their expiration date for an additional year of use in Red, White and Blue seasons. Thank you for electing to save your Points.</p>";
                //model.Message = "<p> The eligible Points in your account(s) will be automatically saved at the end of their expiration date for an additional year of use in Red, White and Blue seasons. Thank you for electing to save your Points.</p>";

            }
            else if (needtosave == true)
            {
                ViewBag.HidePaymentInfo = false;
                //model.PaymentInformationVisible = true;
                //ViewBag.Message = BXGOwner.AnnualPointsExpiration.SavePointsMessage;
                ViewBag.Message = "<p><strong>Expiring Points Alert:</strong> You have Points in your account(s) that will expire unless you elect to save them before their expiration date. Click the Save My Points button below in order to have the eligible Points automatically save on their expiration date.</p>";
            }
            else if (needtosave == false & willbesaved == false)
            {
                ViewBag.Message = "<p>You do not currently have any Points in your account that are eligible to be saved.</p>";
                //ViewBag.PaymentInformationVisible = false;
                ViewBag.HidePaymentInfo = true;
            }

            string pointsdescval = null;


            //------------------------------------------------------
            //Fetching Owner Information from WebService OwnerWS1
            //------------------------------------------------------

            BGO.OwnerWS.AccountInfo[] accountList = null;
            List<string> accList = new List<string>();

            //Call to Web Service Returning AccountInfo
            accountList = OwnerService.fetchOwnerAccounts(BXGOwner.Arvact);

            //TODO: Remove this when actually connecting to WebService For now Just clone the values in StubMe out
            //accountList = (OwnerWS.AccountInfo[])BXGOwner.Accounts.Clone();

            for (int count = 0; count <= accountList.Length - 1; count++)
            {
                if (accountList[count].Proj == "51" || accountList[count].Proj == "52")
                {
                    accList.Add(accountList[count].AcctNum);
                }
            }

            foreach (BGO.OwnerWS.Point point_row in OwnerService.fetchOwnerPointsBuckets(BXGOwner.Arvact))
            {
                //remove point details for expired Value sampler and Sampler 24 owners
                bool valid = true;
                if (!object.Equals(null, accList) & accList.Count > 0)
                {
                    if (accList.Contains(point_row.AcctNum))
                    {
                        valid = false;
                    }
                }
                if (valid)
                {
                    pointsdescval = string.Empty;
                    strExpireDate = point_row.expireDate.ToString();

                    //DateTime as400ExpireDate;
                    string acct = point_row.AcctNum;
                    int tpoints = point_row.pointBal;

                    PointModel ptsdtl = new PointModel();

                    ptsdtl.AcctNum = point_row.AcctNum.Trim();
                    ptsdtl.PointBal = point_row.pointBal;
                    ptsdtl.ExpireDate = point_row.expireDate;//DateTime.Parse(strExpireDate);


                    //It’s only apply for Sampler Owners (VS and S24).
                    if ((BXGOwner.User[0].isSampler))
                    {
                        //It’s only apply for Sampler 24 Owners.
                        if ((BXGOwner.User[0].HomeProject) == "52")
                        {
                            if (point_row.pointTypeDesc == "Additional Points")
                            {
                                point_row.pointTypeDesc = "Annual";
                            }
                        }
                    }
                    ptsdtl.PointTypeDesc = point_row.pointTypeDesc;
                    ptsdtl.PointType = point_row.pointsType;
                    ptsdtl.NextEarnDate = point_row.nextEarnDate;
                    ptsdtl.NextEarnAmount = point_row.nextEarnAmount;
                    ptsdtl.EarnedDate = point_row.beginDate;


                    if (point_row.pointTypeDesc == "Annual" || point_row.pointTypeDesc == "Borrowed")
                    {

                        var foundRows = from x in pttbl.ToList()
                                        where x.PointBal == point_row.pointBal && x.ExpireDate == Convert.ToDateTime(strExpireDate) && x.AcctNum == point_row.AcctNum.Trim()
                                        select x;


                        //int i = 0;
                        // Print column 0 of each returned row.
                        foreach (var row in foundRows)//(i = 0; i < foundRows.Count(); i++)
                        {
                            if (Convert.ToDateTime(strExpireDate) >= thisDay & row.Action == "1" & point_row.pointBal <= 0)
                                ptsdtl.Action = "N/A";
                            else if ((Convert.ToDateTime(strExpireDate) >= thisDay & row.Action == "1" & point_row.pointBal > 0))
                                ptsdtl.Action = "Will be Saved";
                            else if ((Convert.ToDateTime(strExpireDate) >= thisDay & row.Action == "0" & point_row.pointBal > 0))
                                ptsdtl.Action = "Need to Save";
                            else
                                ptsdtl.Action = "N/A";
                        }
                    }
                    else
                    {
                        ptsdtl.Action = "N/A";
                    }

                    //TODO:
                    if (BXGOwner.User[0].isSampler == true)
                    //if (isSamplerOwner == true)
                    {
                        ptsdtl.Action = "N/A";
                        ptsdtl.NextEarnDate = "N/A";
                        ptsdtl.NextEarnAmount = "N/A";
                    }

                    model.AllDetailPoints.Add(ptsdtl);

                }
            }
        }

        private void PopulateOwnerPointTypes(MyPointsListModel model)
        {

            //Populate model Future Detail Points from subset of AllDetailPoints 
            model.FutureDetailPoints = (List<PointModel>)model.AllDetailPoints.Where(x => x.EarnedDate > DateTime.Now).ToList();

            //Populate model Current Detail Points from subset of AllDetailPoints             
            model.CurrentDetailPoints = (List<PointModel>)model.AllDetailPoints.Where(x => x.EarnedDate <= DateTime.Now).ToList();
            model.CurrentDetailPoints.RemoveAll(x => x.PointType == "S" && x.PointBal == 0);

        }

        private void PopulateOwnerPointSummary(MyPointsListModel model)
        {
            //Deep copy
            List<PointModel> currPoints = new List<PointModel>();
            foreach (PointModel original in model.CurrentDetailPoints)
            {
                PointModel copy = new PointModel();
                copy.PointTypeDesc = original.PointTypeDesc;
                copy.ExpireDate = original.ExpireDate;
                copy.PointBal = original.PointBal;
                copy.Action = original.Action;
                currPoints.Add(copy);
            }

            var currentresults = (from list in currPoints select list).OrderByDescending(x => x.PointBal);

            List<PointModel> cPointSummary = new List<PointModel>();
            foreach (PointModel pointModel in currentresults.ToList())
            {
                string pointType = pointModel.PointTypeDesc;
                DateTime expireDate = pointModel.ExpireDate;
                var pointRow = (from myRow in cPointSummary where myRow.PointTypeDesc == pointType 
                    && myRow.ExpireDate == expireDate select myRow).SingleOrDefault();
                if (pointRow == null)
                    cPointSummary.Add(pointModel);
                else
                    pointRow.PointBal += pointModel.PointBal;
            }

            model.CurrentSummaryPoints = cPointSummary;

            //Deep copy
            List<PointModel> futurePoints = new List<PointModel>();
            foreach (PointModel original in model.FutureDetailPoints)
            {
                PointModel copy = new PointModel();
                copy.PointTypeDesc = original.PointTypeDesc;
                copy.ExpireDate = original.ExpireDate;
                copy.EarnedDate = original.EarnedDate;
                copy.PointBal = original.PointBal;
                copy.Action = original.Action;
                futurePoints.Add(copy);
            }

            var futureresults = (from list in futurePoints select list).OrderByDescending(x => x.PointBal);

            List<PointModel> fPointSummary = new List<PointModel>();
            foreach (PointModel pointModel in futureresults.ToList())
            {
                string pointType = pointModel.PointTypeDesc;
                DateTime expireDate = pointModel.ExpireDate;
                var pointRow = (from myRow in fPointSummary
                                where myRow.PointTypeDesc == pointType
                                    && myRow.ExpireDate == expireDate
                                select myRow).SingleOrDefault();
                if (pointRow == null)
                    fPointSummary.Add(pointModel);
                else
                    pointRow.PointBal += pointModel.PointBal;
            }

            model.FutureSummaryPoints = fPointSummary;
        }

        //[HttpPost]
        //public ActionResult SubmitForm(CreditCardInfoModel model)
        //{

        //    mCreditCardModel = model;
        //    ValidateForm();
        //    if(ModelState.IsValid)
        //    {
        //        //redirect to current page to clear the form
        //        SavePoints();
        //        return RedirectToCurrentUmbracoPage();
        //    }
        //    else
        //    {
        //        TempData.Add("DisplaySaveMyPoints", true);
        //        //Add Error processing here
        //        return CurrentUmbracoPage();
        //    }
            
        //    //redirect to current page to clear the form
        //    //return RedirectToCurrentUmbracoPage();

        //    //or redirect to specific page
        //    //return RedirectToUmbracoPage(12345);

        //}

        //private void HydrateModel(MyPointsListModel model)
        //{
        //    //mPointsListModel = model;
        //    //PopulateOwnerPointDetails(model);
        //    PopulateOwnerPointTypes(model);
        //    PopulateOwnerPointSummary(model);
        //}

    }

}