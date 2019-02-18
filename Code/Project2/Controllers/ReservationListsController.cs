using BGModern.Models;
using System;
using ReservationLibrary;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
using Umbraco.Core.Models;
using Umbraco.Web;
using BGModern.Mappers;

namespace BGModern.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    public class ReservationListsController : Umbraco.Web.Mvc.SurfaceController
    {

        private BGO.OwnerWS.Owner BXGOwner = new BGO.OwnerWS.Owner();
        private BGO.OwnerWS.OwnerWS1SoapClient OwnerService = new BGO.OwnerWS.OwnerWS1SoapClient();
        private ResortInfo instanceResort = new ResortInfo();

        private bool? _PolicyEligible;
        private String _PolicyStatus;
        private String _Status;
        private Boolean? _PromoCodeActived = null;
        private BGO.ResortsService.BookingScreeningResponse bookingEligibility = new BGO.ResortsService.BookingScreeningResponse();
        private BGO.ResortsService.ReservationRequest reservationRequest = new BGO.ResortsService.ReservationRequest();
      
        [HttpGet]
        public ActionResult ReservationLists ()
        {
            throw new NotImplementedException("Index is not implemented for ReservationController");
        }

        public ActionResult GetPartialView(ReservationListModel model)
        {
            return PartialView("ReservationLists", model);
        }

        public ActionResult ReservationsLists()
        {
            BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            ReservationListModel myReservations = ReservationListMapper.Map(CurrentPage);
            myReservations = MasterMapper.Map(myReservations, CurrentPage);


            string reservationNo = String.Empty;
            string resortNo = String.Empty;

            if (TempData["ReservationNo"] != null && TempData["ResortNo"] != null)
            {
                reservationNo = TempData["ReservationNo"].ToString();
                resortNo = TempData["ResortNo"].ToString();
            }

            if (reservationNo != String.Empty && resortNo != String.Empty)
            {
                ReservationDetailModel detailsModel = new ReservationDetailModel();
                detailsModel.ReservationNo = reservationNo;
                detailsModel.ResortNo = resortNo;
                myReservations.DetailModel = detailsModel;
            }

            ReservationListModel myreservation = new ReservationListModel();
            PopulateAllReservations(myreservation);
            Response.Redirect(ConfigurationManager.AppSettings["bxgwebSitecoremyReservationRedirectUrl"], true);
            return PartialView("ReservationsList", myreservation);
        }

        public ActionResult GetMyReservationsDetails(string reservationNo, string resortNo)
        {
                TempData["ReservationNo"] = reservationNo;
                TempData["ResortNo"] = resortNo;
                return CurrentUmbracoPage();
                //return Redirect("~/reservation-detail");
        }

        public ActionResult ViewPPP(string reservationNo, string resortNo)
        {
            BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            BGO.ResortsService.OwnerID owner = new BGO.ResortsService.OwnerID();

            //searching for the clicked reservation item so that the session can be set for the next page.
            BGO.ResortsService.ReservationHistory history = new BGO.ResortsService.ReservationHistory();
            BGO.ResortsService.ReservationHistoryList historyResult = new BGO.ResortsService.ReservationHistoryList();
            BGO.ResortsService.ResortsServiceClient reservationAS400 = new BGO.ResortsService.ResortsServiceClient();
            BGO.ResortsService.ReservationHistoryItem[] histories = null;

            history.OwnerID = null;
            owner.OwnerVacationNumber = BXGOwner.Arvact;

            history.OwnerID = owner;
            history.SiteName = BGO.ResortsService.Sites.OnlinePoints;
            history.EffectiveDate = DateTime.Now;

            history.SearchHistoryBy = BGO.ResortsService.ReservationHistoryType.Future;
            historyResult = reservationAS400.GetReservationsHistory(history);
            if (historyResult.Success)
            {
                histories = historyResult.ReservationHistoryItem;
                foreach (BGO.ResortsService.ReservationHistoryItem reservationItem in histories)
                {
                    if (reservationItem._ReservationNumber == reservationNo)
                        Session["ReservationSelected4Details"] = reservationItem;
                }
            }
            ResortInfo resortInfo = Utilities.getResortInfo(Convert.ToInt32(resortNo));
            dynamic ourResorts = null;
            int ourResortsContentId;
            string ourResortsContentIdString = ConfigurationManager.AppSettings["ourResortsContentId"];
            if (!string.IsNullOrWhiteSpace(ourResortsContentIdString) && Int32.TryParse(ourResortsContentIdString, out ourResortsContentId))
            {
                ourResorts = Umbraco.TypedContent(ourResortsContentId);
            }

            //Populate the image based on the resorts.
            if (ourResorts != null)
            {
                foreach (IPublishedContent content in ourResorts.Children)
                {
                    if (content.DocumentTypeAlias.Equals("Resort"))
                    {
                        if (resortInfo.ResortID == content.GetPropertyValue<string>("DatabaseId"))
                        {
                            Session["ReservResortImage"] = content.GetPropertyValue<string>("ResortImage");
                            Session["ReservResortUrl"] = content.Url;

                        }
                    }
                }
            }
            Session["BuyPPPResortNo"] = resortNo;
            Session["resortcity"] = String.Concat(resortInfo.City, ", ", resortInfo.State, "    ", resortInfo.PostalCode);
            string path = BGModern.HtmlExtensions.CustomHtmlHelpers.GetParentSitePath(null).ToString();
            string pppUrl = string.Concat(path, "/owner/ptsPurchaseppp.aspx?Origin=myreservation&resno=", reservationNo + "&ResortNo=" + Session["BuyPPPResortNo"]);
            return Redirect(pppUrl);
        }

        public void getRelationTypes()
        {
            //TODO
            //ddlGuessRelationshipType.Items.Clear();
            //ddlGuessRelationshipType.Items.Add("Select Guest Relationship Type");

            foreach (BGO.ResortsService.RelationshipType relationshipType in System.Enum.GetValues(typeof(BGO.ResortsService.RelationshipType)))
            {
                try
                {
                    if (relationshipType.ToString() != "None")
                    {
                        //ddlGuessRelationshipType.Items.Add(relationshipType.ToString);
                    }

                }
                catch (Exception ex)
                {
                }
            }

        }

        private void PopulateAllReservations(ReservationListModel model)
        {
            BGO.ResortsService.ReservationHistory history = new BGO.ResortsService.ReservationHistory();
            BGO.ResortsService.ReservationHistoryList historyResult = new BGO.ResortsService.ReservationHistoryList();
            BGO.ResortsService.ResortsServiceClient reservationAS400 = new BGO.ResortsService.ResortsServiceClient();
            BGO.ResortsService.ReservationHistoryItem[] histories = null;
            BGO.ResortsService.OwnerID owner = new BGO.ResortsService.OwnerID();

            try
            {
                history.OwnerID = null;
                owner.OwnerVacationNumber = BXGOwner.Arvact;
                history.OwnerID = owner;
                history.SiteName = BGO.ResortsService.Sites.OnlinePoints;
                history.EffectiveDate = DateTime.Now;

                history.SearchHistoryBy = BGO.ResortsService.ReservationHistoryType.Future;

                if (ViewData["myPendingHistoryBind"] == null)
                {
                    historyResult = reservationAS400.GetReservationsHistory(history);

                    if (historyResult.Success)
                    {
                        histories = historyResult.ReservationHistoryItem;
                        model.PendingReservations = PopulateReservationsList(histories,BGO.ResortsService.ReservationHistoryType.Future);
                        //ViewData["myPendingHistoryBind"] = histories;
                    }
                }

                if (ViewData["myPastHistoryBind"] == null)
                {
                    history.SearchHistoryBy = BGO.ResortsService.ReservationHistoryType.Past;
                    historyResult = reservationAS400.GetReservationsHistory(history);

                    if (historyResult.Success)
                    {
                        histories = historyResult.ReservationHistoryItem;
                        model.PastReservations = PopulateReservationsList(histories, BGO.ResortsService.ReservationHistoryType.Past);
                        //ViewData["myPastHistoryBind"] = histories;
                    }

                }

                if (reservationAS400.State != System.ServiceModel.CommunicationState.Faulted)
                {
                    reservationAS400.Close();
                }

            }
            catch (Exception ex)
            {
                if (reservationAS400.State != System.ServiceModel.CommunicationState.Faulted)
                {
                    reservationAS400.Close();
                }

                StringBuilder errMsg = new StringBuilder();
                errMsg.Append("Error happened fetching Reservations List");errMsg.Append(ex.Message);
                sendMail.sendMessage("OLPSupport@bxgcorp.com", "", "Exception Error - Getting Reservation List", errMsg);
            }

        }

        private List<ReservationModel> PopulateReservationsList(BGO.ResortsService.ReservationHistoryItem[] reservations, BGO.ResortsService.ReservationHistoryType enmResType)
        {
            List<ReservationModel> reservationList = new List<ReservationModel>();

            if (enmResType == BGO.ResortsService.ReservationHistoryType.Past)
            {
                for (int x = 0; x <= reservations.Length - 1; x++)
                {
                    ReservationModel rsdtl = new ReservationModel();
                    rsdtl.ReservationNumber = reservations[x]._ReservationNumber;
                    rsdtl.NumberOfAdults = reservations[x]._NumberOfAdults;
                    rsdtl.NumberOfNights = Convert.ToInt32(reservations[x]._NumberOfNights);
                    rsdtl.AmountDue = reservations[x]._AmountDue;
                    rsdtl.PolicyPrice = reservations[x]._PolicyPrice;
                    rsdtl.AS400UnitType = reservations[x]._AS400UnitType;
                    rsdtl.ProjectStay = Convert.ToInt32(reservations[x]._ProjectStay);
                    rsdtl.ReservationType = reservations[x]._ReservationType;

                    rsdtl.ResortNo = Convert.ToInt32(reservations[x]._ProjectStay);
                    rsdtl.ResortName = getResortName(rsdtl.ProjectStay, rsdtl.ReservationType);
                    rsdtl.CheckInDate = NumericToDate(reservations[x]._CheckInDate, "yyMMdd");
                    rsdtl.CheckOutDate = CalcCheckOutDate(rsdtl.NumberOfNights, rsdtl.CheckInDate);
                    rsdtl.VillaType = getVillaDescription(rsdtl.ProjectStay, rsdtl.AS400UnitType);
                    rsdtl.ReservationType = ReservationType(reservations[x]._ReservationType, reservations[x]._ExchangeCode);
                    rsdtl.Points = convertPoints(reservations[x]._Points, reservations[x]._ReservationType);
                    rsdtl.PPPStatus = PolicyStatus(reservations[x]._PolicyStatus, reservations[x]._EligibleDate, "Future", reservations[x]._ReservationNumber, reservations[x]._ExchangeCode, reservations[x]._ReservationType);
                    reservationList.Add(rsdtl);
                }
            }
            else
            {
                for (int x = reservations.Length - 1; x >= 0; x--)
                {
                    ReservationModel rsdtl = new ReservationModel();
                    rsdtl.ReservationNumber = reservations[x]._ReservationNumber;
                    rsdtl.NumberOfAdults = reservations[x]._NumberOfAdults;
                    rsdtl.NumberOfNights = Convert.ToInt32(reservations[x]._NumberOfNights);
                    rsdtl.AmountDue = reservations[x]._AmountDue;
                    rsdtl.PolicyPrice = reservations[x]._PolicyPrice;
                    rsdtl.AS400UnitType = reservations[x]._AS400UnitType;
                    rsdtl.ProjectStay = Convert.ToInt32(reservations[x]._ProjectStay);
                    rsdtl.ReservationType = reservations[x]._ReservationType;

                    rsdtl.ResortNo = Convert.ToInt32(reservations[x]._ProjectStay);
                    rsdtl.ResortName = getResortName(rsdtl.ProjectStay, rsdtl.ReservationType);
                    rsdtl.CheckInDate = NumericToDate(reservations[x]._CheckInDate, "yyMMdd");
                    rsdtl.CheckOutDate = CalcCheckOutDate(rsdtl.NumberOfNights, rsdtl.CheckInDate);
                    rsdtl.VillaType = getVillaDescription(rsdtl.ProjectStay, rsdtl.AS400UnitType);
                    rsdtl.ReservationType = ReservationType(reservations[x]._ReservationType, reservations[x]._ExchangeCode);
                    rsdtl.Points = convertPoints(reservations[x]._Points, reservations[x]._ReservationType);
                    rsdtl.PPPStatus = PolicyStatus(reservations[x]._PolicyStatus, reservations[x]._EligibleDate, "Future", reservations[x]._ReservationNumber, reservations[x]._ExchangeCode, reservations[x]._ReservationType);
                    reservationList.Add(rsdtl);
                }
            }

            return reservationList;
        }

        //TODO Currently Working
        private void PendingReservationListItemSelect(ReservationModel model)
        {

                try {
          //          instanceResort = Utilities.getResortInfo(Convert.ToInt32(lblResortNo.Text));
                } catch (Exception ex) {
                    instanceResort = new ResortInfo();
                }

                Session["instanceResort"] = instanceResort;


        }

        private void loadGrids()
        {
            //Dim conn As IDbConnection = New iDB2Connection
            //conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("BxgCorp.Owner.Reservation.ConnectionString")

            BGO.OwnerWS.Owner BXGOwner = new BGO.OwnerWS.Owner();
            BGO.OwnerWS.OwnerWS1SoapClient OwnerService = new BGO.OwnerWS.OwnerWS1SoapClient();

            System.DateTime dtm = DateTime.Now.Date;
            string _d = dtm.Day.ToString();
            string _m = dtm.Month.ToString();

            if (_d.Length < 2)
            {
                _d = "0" + _d;
            }

            if (_m.Length < 2)
            {
                _m = "0" + _m;
            }

            string s = dtm.Year.ToString().Substring(2, 2) + _m + _d;

            try
            {
                if (ViewData["myPendingHistoryBind"] == null)
                {
                    //TODO
                    //this.lblMessages.Text = "There are no current reservations to display.<br />";
                    //this.rptPendingReservations.Visible = false;
                }
                else
                {
                    //PopulateModelPendingHistory(ViewData["myPendingHistoryBind"]);

                    //this.rptPendingReservations.DataSource = GetPendingHistoryViewState();
                    //this.rptPendingReservations.DataBind();
                }

                //if (ViewState("myPastHistoryBind") == null)
                //{
                //    //this.lblMessages.Text = "There are no past reservations to display.";
                //    //this.rptPastReservations.Visible = false;
                //}
                //else
                //{
                //    //this.rptPastReservations.DataSource = GetPastHistoryViewState();
                //    //this.rptPastReservations.DataBind();
                //}
            }
            catch (Exception ex)
            {
                Response.Write("error loading grids: " + ex.Message + "<br />");

            }
            finally
            {
            }

        }

        private object GetPendingHistoryViewData()
        {
            //Gets the ViewState
            return ViewData["myPendingHistoryBind"];
        }

        public string getResortName(int _projNum, string _reservationType)
        {
            try
            {

                //ResortInfo resort = new ResortInfo();
                //gets the correct resort name for resorts with multiple names.  For example,
                //oasis lakes is also the fountains. This function will return the fountains
                // as a display name for any reservation that is set for oasis lakes.

                ResortInfo resort = Utilities.getResortInfo(_projNum);

                //This modification added a logical to identify PNE - Preferred Status Exchange Resorts,  as the Resort Name. 
                //BITS 7748. Luiz - 04/26/2010
                if (_reservationType.Trim() == "K" | _reservationType.Trim() == "L")
                {
                    resort.ResortName = "Preferred Status Exchange Resorts";
                }

                return resort.ResortName;
            }
            catch (Exception ex)
            {
            }

            return "";
        }

        public string CalcCheckOutDate(int days, string checkIn)
        {

            try
            {
                DateTime dtm = Convert.ToDateTime(checkIn);
                dtm = dtm.AddDays(days);
                return dtm.ToString("d");
            }
            catch (Exception ex)
            {
                return "00/00/00";
            }

        }

        public string NumericToDate(string source, string format)
        {
            string temp = "";

            if (source.Length == 0)
            {
                return "00/00/0000";
            }
            else if (source.Length == 3)
            {
                source = "000" + source;
            }
            else if (source.Length == 4)
            {
                source = "00" + source;
            }

            int parsedInt = 0;
            if (source.Length >= 5 & int.TryParse(source, out parsedInt))
            {
                if (source.Length == 5)
                {
                    source = "0" + source;
                }

                if (format == "yyMMdd")
                {
                    temp = source.Substring(2, 2) + "/" + source.Substring(4, 2) + "/" + source.Substring(0, 2);
                }
                else if (format == "MMddyy")
                {
                    temp = source.Substring(0, 2) + "/" + source.Substring(2, 2) + "/" + source.Substring(4, 2);
                }

                return Convert.ToDateTime(temp).ToString("d");

            }
            else
            {
                return "00/00/0000";

            }

        }

        public string getVillaDescription(int pn, string ut)
        {

            string xx = null;

            try
            {
                xx = Utilities.getUnitDescription(pn, ut);
                if (string.IsNullOrEmpty(xx))
                {
                    xx = null;
                }

            }
            catch (Exception ex)
            {
                string Path = null;
                StringBuilder errMsg = new StringBuilder();

                string sHost = (Request.ServerVariables["HTTP_HOST"] + Request.ServerVariables["PATH_INFO"]).ToString().ToLower().Replace("www.", "");
                // TODO: see if C# version of VB code works.
                Path = "http://" + sHost.Substring(0, sHost.IndexOf("/")) + System.Configuration.ConfigurationManager.AppSettings["bxgwebImgPath"];

                errMsg.Append("Machine: " + Path);
                errMsg.Append(ex.Message);
                sendMail.sendMessage("OLPSupport@bluegreenvacations.com", "", " verifyScenary procedure ", errMsg);

                retrievalError(errMsg.ToString());

            }

            return xx;

        }

        public static string ReservationType(string rt, string exch)
        {

            rt = Utilities.GetReservationDescription(rt.Trim());

            //Determine if this is an exchange reservation.  If it is, then exch will be populated.
            //if it is, then rt needs to be "exch*"  
            //This was added after a column labelled Exchange was removed from the reservation
            //repeater as a way to display an exchange reservation type.
            String trimmedExch = exch.Trim();

            if (trimmedExch == "#")
            {
                rt = "Exch*";
            }
            else if (trimmedExch == "@")
            {
                rt = "Exch*";

            }
            else if (trimmedExch == "*")
            {
                rt = "Exch*";

            }
            else if (trimmedExch == "&")
            {
                rt = "Exch*";
            }

            return rt;
        }

        public string convertPoints(string s, string t)
        {

            if (t == "P")
            {
                s = String.Format("{0:#,##0}", Convert.ToInt32(s));
                return s;
            }
            else
            {
                string blank = "";
                return blank;
            }
        }

        public string PolicyStatus(string Policy, string EligibilityDate, string ReservationCondition, string ReservationNbr, string ExchangeType, string ReservationType)
        {
            DateTime? _PolicyISODate = null;
            // TODO: see if C# version of VB code works.
            System.DateTime _dateTimeInfo = Convert.ToDateTime(DateTime.Now.ToString("s").Substring(0, 10));// Convert.ToDateTime(Strings.Left(DateTime.Now.ToString("s"), 10));
            int? RowNo = null;
            string _lnkClientID = null;
            string _ClientID = null;

            _PolicyEligible = null;
            _PolicyStatus = null;
            _Status = null;


            if (ReservationCondition == "Future")
            {
                //--------------------------------------------------------------------------
                //PROMOCODE Program - There is a decision to open PPP out to all Future reservation without PPP.
                //Concern on this moment, on scenario, is 24 hours or today = check-in date to allow to buy PPP.
                //According to Chad today = check-in on his book the reservation will be allow to buy PPP
                //--------------------------------------------------------------------------

                if (_PromoCodeActived == true)
                {
                    switch (Policy)
                    {
                        case "A":
                            _PolicyStatus = "Protected";
                            //Active
                            break;
                        default:
                            _PolicyStatus = "Buy Now!";
                            break;
                    }

                    //if reservation type is not <Points> do not allow it to buy PPP. Some type: Exchange, Flex or Bonus type, PM Unit Upgrade as <G>  and New Onwer/VIP as <Q>". Luiz 08/20/2010
                    //If Trim(ReservationType) = "F" Or Trim(ReservationType) = "B" Or Trim(ExchangeType) = "#" Or Trim(ExchangeType) = "@" Or Trim(ExchangeType) = "*" Or Trim(ExchangeType) = "&" Or Trim(ExchangeType) = "G" Or Trim(ExchangeType) = "Q" Then
                    // TODO: see if C# version of VB code works.
                    if (ReservationType.Trim() != "P")
                    {
                        _PolicyStatus = "";
                    }
                    else if (ExchangeType.Trim() == "#" | ExchangeType.Trim() == "@" | ExchangeType.Trim() == "*" | ExchangeType.Trim() == "&" | ExchangeType.Trim() == "G" | ExchangeType.Trim() == "Q")
                    {
                        _PolicyStatus = "";
                    }
                }
                else
                {
                    //It's not suppose EligibilityDate be zero out, meaning, something got error or it can be an Exchange.
                    //Though Cancellation will procede without see the PPP status in Blank.

                    if (EligibilityDate != "0")
                    {
                        _PolicyISODate = Convert.ToDateTime(EligibilityDate.ToString().Substring(4, 2) + "/" + EligibilityDate.ToString().Substring(6, 2) + "/" + EligibilityDate.ToString().Substring(0, 4));
                        _PolicyISODate = Convert.ToDateTime(Convert.ToDateTime(_PolicyISODate).ToString("d"));
                        if (_dateTimeInfo <= _PolicyISODate)
                        {
                            _PolicyEligible = true;
                        }
                        else
                        {
                            _PolicyEligible = false;
                        }

                        switch (Policy)
                        {
                            case "A":
                                _PolicyStatus = "Protected";
                                //Active
                                break;
                            case "D":
                                if (_PolicyEligible == true & ReservationType.Trim() == "P")
                                {
                                    _PolicyStatus = "Buy Now!";
                                }
                                else
                                {
                                    _PolicyStatus = "Not Protected";
                                }
                                break;
                            default:
                                ErrorHappened(ReservationNbr, Policy);
                                break;
                        }

                        if (ExchangeType.Trim() == "#" | ExchangeType.Trim() == "@" | ExchangeType.Trim() == "*" | ExchangeType.Trim() == "&" | ExchangeType.Trim() == "G" | ExchangeType.Trim() == "Q")
                        {
                            _PolicyStatus = "";
                        }

                        //Response.Write("Now: '" & _dateTimeInfo & "'<br />")
                        //Response.Write("Policy Date: '" & _PolicyISODate & "'<br />")
                        //Response.Write("Policy Type: '" & Policy & "'<br />")
                        //Response.Write("Eligible: '" & _PolicyEligible & "'<br />")
                        //Response.Write("Eligible Status begin : '" & _PolicyStatus & "'<br />")
                        //Response.Write("Eligible Status End: '" & _PolicyStatus & "'<br />")
                        //Response.Write("--------------------------------" & "<br />")
                        //Response.Write("Eligible Status begin fase2: '" & _PolicyStatus & "'<br />")

                    }
                    else
                    {
                        //if reservation type is not <Points> do not allow it to buy PPP. Some type: Exchange, Flex or Bonus type, PM Unit Upgrade as <G>  and New Onwer/VIP as <Q>". Luiz 08/20/2010
                        //If Trim(ReservationType) = "F" Or Trim(ReservationType) = "B" Or Trim(ExchangeType) = "#" Or Trim(ExchangeType) = "@" Or Trim(ExchangeType) = "*" Or Trim(ExchangeType) = "&" Or Trim(ExchangeType) = "G" Or Trim(ExchangeType) = "Q" Then
                        if (ReservationType.Trim() != "P")
                        {
                            _PolicyStatus = "";
                        }
                        else if (ExchangeType.Trim() == "#" | ExchangeType.Trim() == "@" | ExchangeType.Trim() == "*" | ExchangeType.Trim() == "&" | ExchangeType.Trim() == "G" | ExchangeType.Trim() == "Q")
                        {
                            _PolicyStatus = "";
                        }
                    }

                }


                //<---- Past Reservations
            }
            else
            {

                switch (Policy)
                {
                    case "A":
                        _PolicyStatus = "Protected";
                        break;
                    case "S":
                        _PolicyStatus = "Consumed";
                        break;
                    case "D":
                    case "":
                    case " ":
                        _PolicyStatus = "Not Protected";
                        break;
                    case "E":
                        _PolicyStatus = "Expired";
                        break;
                    case "T":
                        _PolicyStatus = "Transfered";
                        break;
                    case "X":
                        _PolicyStatus = "Refunded";
                        break;
                    default:
                        _PolicyStatus = "-";
                        break;
                }

            }

            if (_PolicyStatus == null || _PolicyStatus == "")
                _PolicyStatus = "-";

            return _PolicyStatus;
        }

        public void retrievalError(string location)
        {
            //TODO HIDE/SHOW
            //this.imgAlert.Visible = true;
            ViewBag.Message = "We're sorry but we are unable to process your request at this time.  Please try again later.";

            if (location == "list")
            {
                ViewBag.DisplayMessagesBackLink = false;
            }
            else
            {
                ViewBag.DisplayMessagesBackLink = true;
            }

        }

        protected void ErrorHappened(string ReservationNbr, string PPPStatus)
        {
            StringBuilder errMsg = new StringBuilder();
            errMsg.Append("Reservation #: " + ReservationNbr + System.Environment.NewLine);
            errMsg.Append("PPP Status: " + PPPStatus + System.Environment.NewLine);
            sendMail.sendMessage("olpsupport@bluegreencorp.com", "", "PPP status irregular ", errMsg);

        }

    }

}