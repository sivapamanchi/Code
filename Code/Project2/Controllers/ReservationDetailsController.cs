using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BGModern.Models;
using OptionDropDownList;
using ReservationLibrary;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Text.RegularExpressions;

namespace BluegreenOnline.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    public class ReservationDetailsController : Umbraco.Web.Mvc.SurfaceController
    {
        private BGO.OwnerWS.Owner BXGOwner = new BGO.OwnerWS.Owner();

        public ActionResult GetReservationDetails(ReservationDetailModel detailModel)
        {
            if (TempData["DetailGuestData"] == null)
            {
                detailModel.ExchangeVisible = false;

                //if (TempData["DetailModelData"] == null)
                //{
                PopulateResortInfo(detailModel);
                PopulateReservationInfo(detailModel);
                PopulateRelationshipTypes(detailModel);
                PopulateStates(detailModel);
                populateOccupancy(detailModel);
                //}

                //TempData["DetailModelData"] = detailModel;

                //If there was a failure message for the submission
                if (detailModel.MessageText != null && detailModel.MessageText != "")
                {
                    detailModel.Updating = true;
                    detailModel.GuestFormVisible = true;
                    PopulateGuestList(detailModel);
                    detailModel.ButtonUpdateText = "Save";
                }
                else
                {
                    detailModel.SelectedGuest = detailModel.GuestName;
                    PopulateGuestList(detailModel);
                    detailModel.ButtonUpdateText = "Edit";
                    detailModel.Updating = false;
                }

            }

            return PartialView("ReservationDetails", detailModel);
        }

        [HttpPost]
        public ActionResult EditOrSubmit(ReservationDetailModel detailModel)
        {
            if (detailModel.PostType == "Edit")
            {
                //if (TempData["DetailModelData"] == null)
                //{
                PopulateResortInfo(detailModel);
                PopulateReservationInfo(detailModel);
                PopulateRelationshipTypes(detailModel);
                PopulateStates(detailModel);
                populateOccupancy(detailModel);
                //}
                //else
                //{
                //    detailModel = ((ReservationDetailModel)TempData["DetailModelData"]);
                //    TempData["DetailModelData"] = detailModel;
                //}

                detailModel.ExchangeVisible = false;

                //Updating Form Controls
                PopulateGuestList(detailModel);
                PopulateGuestForm(detailModel);
                detailModel.Updating = true;
                detailModel.ButtonUpdateText = "Save";

                TempData["DetailGuestData"] = detailModel;
            }
            else if (detailModel.PostType == "Submit")
            {
                ValidateAndSubmit(detailModel);
            }

            return CurrentUmbracoPage();
        }
        public bool IsValidPhone(string Phone, string formattedPhoneNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(Phone))
                    return false;

                Regex regexPhoneNumber = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");

                if (regexPhoneNumber.IsMatch(Phone))
                {
                    formattedPhoneNumber = regexPhoneNumber.Replace(Phone, "($1) $2-$3");
                }
                else
                {
                    // Invalid phone number
                }
                return regexPhoneNumber.IsMatch(formattedPhoneNumber);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ValidateAndSubmit(ReservationDetailModel detailModel)
        {
            string messageText = "";

            if (Session["garbageInFound"] != null)
            {
                if (detailModel.SelectedGuestGroup == "Owner" && (bool)Session["garbageInFound"] == true)
                {
                    messageText += "<FONT color=red><b>Please select a valid owner name (Last Name, First Name).</b></font><br>";
                }
            }

            if (detailModel.SelectedGuestGroup == "New" || detailModel.SelectedGuestGroup == "Guests")
            {
                if (detailModel.GuestFormFirstName == null || detailModel.GuestFormFirstName.Trim() == "")
                    messageText += "<FONT color=red><b>Guest first name is required to continue.</b></font><br>";

                if (detailModel.GuestFormLastName == null || detailModel.GuestFormLastName.Trim() == "")
                    messageText += "<FONT color=red><b>Guest last name is required to continue.</b></font><br>";

                if (detailModel.GuestFormRelationship == "Select Guest Relationship Type")
                    messageText += "<FONT color=red><b>Guest relationship is required to continue.</b></font><br>";

                if (detailModel.GuestFormEmail != null && detailModel.GuestFormEmail.Trim().Length > 0)
                {
                    bool isValidEmail = BXG.WebTeam.Validation.EmailExtensions.IsValidEmail(detailModel.GuestFormEmail);
                    if (!isValidEmail)
                        messageText += "<FONT color=red><b>Guest email address is invalid.</b></font><br>";
                }

                if (detailModel.GuestFormPhone != null && detailModel.GuestFormPhone.Trim().Length > 0)
                {
                    if (!IsValidPhone(detailModel.GuestFormPhone, ""))
                        messageText += "<FONT color=red><b>Guest phone number is invalid.</b></font><br>";
                }
            }

            if (messageText != "")
            {
                detailModel.MessageText = messageText;
                TempData["SubmittedForm"] = detailModel;
            }
            else
            {
                BGO.ResortsService.Guest _Guest = new BGO.ResortsService.Guest();
                List<BGO.ResortsService.Address> _GuestAddressList = new List<BGO.ResortsService.Address>();
                BGO.ResortsService.Address _GuestAddress = new BGO.ResortsService.Address();
                BGO.ResortsService.Phone _GuestPhone = new BGO.ResortsService.Phone();
                List<BGO.ResortsService.Phone> _GuestPhoneList = new List<BGO.ResortsService.Phone>();
                BGO.ResortsService.Email _GuestEmail = new BGO.ResortsService.Email();
                List<BGO.ResortsService.Email> _GuestEmailList = new List<BGO.ResortsService.Email>();
                BGO.ResortsService.ReservationHistoryItem ReservationSelected4Details = new BGO.ResortsService.ReservationHistoryItem();
                BGO.ResortsService.ConfirmationNumber ReservationNumber = new BGO.ResortsService.ConfirmationNumber();
                BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];

                BGO.ResortsService.ModifyGOOReservationRequest gooReservationRequest = new BGO.ResortsService.ModifyGOOReservationRequest();
                BGO.ResortsService.ModifyGOOReservationRequest gooReservationResponse = new BGO.ResortsService.ModifyGOOReservationRequest();

                ReservationNumber.ID = detailModel.ReservationNo;
                BGO.ResortsService.ConfirmationNumber reservationNo = new BGO.ResortsService.ConfirmationNumber();
                reservationNo.ID = detailModel.ReservationNo;
                gooReservationRequest.ReservationNumber = reservationNo;
                gooReservationRequest.NumberOfPeople = Convert.ToInt32(detailModel.NumberOfAdults);

                if (detailModel.SelectedGuest == "Please, select a Guest,")
                {
                    BGO.ResortsService.ReservationHistoryItem historyItem = (BGO.ResortsService.ReservationHistoryItem)Session["ReservationSelected4Details"];
                    string guestGroup = "Guests";

                    foreach (string strOwnerCheckInName in BXGOwner.AccountHolders)
                    {
                        if (historyItem._GuestFullName.Trim().ToUpper() == strOwnerCheckInName.Trim().ToUpper())
                            guestGroup = "Owner";
                    }

                    if (guestGroup == "Guests")
                    {
                        _Guest = BGO.OwnerGuest.GuestSelected(Convert.ToInt32(historyItem._GuestID), BGO.OwnerGuest.GetGuests(BXGOwner.Arvact));
                        var guestLastName = (!string.IsNullOrEmpty(_Guest.LastName) ? _Guest.LastName.Trim().ToUpper() : "");
                        var guestFirstName = (!string.IsNullOrEmpty(_Guest.FirstName) ? _Guest.FirstName.Trim().ToUpper() : "");
                        if (!string.IsNullOrEmpty(guestLastName) && !string.IsNullOrEmpty(guestFirstName))
                            gooReservationRequest.ReservationName = guestLastName + ", " + guestFirstName;
                        else
                            gooReservationRequest.ReservationName = historyItem._GuestFullName;
                    }
                    else
                        gooReservationRequest.ReservationName = historyItem._GuestFullName;

                    detailModel.SelectedGuest = gooReservationRequest.ReservationName;
                }
                else
                {
                    if (detailModel.SelectedGuestGroup == "New" || detailModel.SelectedGuestGroup == "Guests")
                    {
                        if (detailModel.SelectedGuestGroup == "Guests")
                            _Guest = BGO.OwnerGuest.GuestSelected(detailModel.SelectedGuestId, BGO.OwnerGuest.GetGuests(BXGOwner.Arvact));

                        gooReservationRequest.ReservationName = detailModel.GuestFormLastName.Trim().ToUpper() + ", " + detailModel.GuestFormFirstName.Trim().ToUpper();
                        _Guest.FirstName = detailModel.GuestFormFirstName.Trim().ToUpper();
                        _Guest.LastName = detailModel.GuestFormLastName.Trim().ToUpper();

                        //BGO.ResortsService.RelationshipType relationshipType = new BGO.ResortsService.RelationshipType();
                        //relationshipType.

                        Enum.Parse(typeof(BGO.ResortsService.RelationshipType), detailModel.GuestFormRelationship);

                        _Guest.Relationship = ((BGO.ResortsService.RelationshipType)Enum.Parse(typeof(BGO.ResortsService.RelationshipType), detailModel.GuestFormRelationship));
                        _GuestAddress.AddressLine1 = " ";
                        _GuestAddress.AddressLine2 = " ";
                        _GuestAddress.AddressType = BGO.ResortsService.AddressType.Unknown;
                        _GuestAddress.City = detailModel.GuestFormCity;
                        _GuestAddress.CountryCode = " ";
                        _GuestAddress.CountryCode = " ";
                        _GuestAddress.StateCode = detailModel.GuestFormState;
                        _GuestAddress.ZipCode = " ";
                        _GuestAddressList.Add(_GuestAddress);
                        _Guest.Addresses = _GuestAddressList.ToArray();

                        _GuestEmail.EmailAddress = detailModel.GuestFormEmail;
                        _GuestEmail.EmailType = BGO.ResortsService.EmailType.Primary;
                        _GuestEmailList.Add(_GuestEmail);
                        _Guest.Emails = _GuestEmailList.ToArray();

                        _GuestPhone.PhoneNumber = detailModel.GuestFormPhone;
                        _GuestPhone.LocationType = BGO.ResortsService.PhoneType.Unknown;
                        _GuestPhoneList.Add(_GuestPhone);
                        _Guest.Phones = _GuestPhoneList.ToArray();
                    }
                    else if (detailModel.SelectedGuestGroup == "Owner")
                        gooReservationRequest.ReservationName = detailModel.SelectedGuest;
                }

                gooReservationRequest.Guest = _Guest;

                ExecuteBook(gooReservationRequest, detailModel);
            }
        }

        public void ExecuteBook(BGO.ResortsService.ModifyGOOReservationRequest gooReservationRequest, ReservationDetailModel detailModel)
        {
            BGO.ResortsService.ModifyGOOReservationResponse modifyGOOResponse = new BGO.ResortsService.ModifyGOOReservationResponse();
            BGO.ResortsService.ResortsServiceClient client = new BGO.ResortsService.ResortsServiceClient();

            try
            {
                modifyGOOResponse = client.ModifyGOOReservation(gooReservationRequest);

                if (modifyGOOResponse.Success == 1)
                {
                    if (Session["AgentLoginID"] != null && Session["AgentLoginID"].ToString() != "")
                        BGO.VSSALog.log(detailModel.ReservationNo, "Modify Guest reservation ", BXGOwner.Arvact, Request.UserHostAddress);

                    detailModel.UpdateText = "Reservation successfully changed!";
                }
                else
                {
                    detailModel.UpdateText = "We're sorry, but found some issues updating the reservation.";
                }

                TempData["UpdateText"] = detailModel.UpdateText;

                if (client.State != System.ServiceModel.CommunicationState.Faulted)
                    client.Close();
            }
            catch (Exception ex)
            {
                StringBuilder errMsg = new StringBuilder();
                errMsg.Append("Error happened, on changing Guest on Reservation - myreservation");
                errMsg.Append(ex.Message);
                sendMail.sendMessage("OLPSupport@bxgcorp.com", "", "Exception Error during updating guest information to existent Reservation ", errMsg);
            }
        }
        public void PopulateResortInfo(ReservationDetailModel detailModel)
        {
            ResortInfo resortInfo = Utilities.getResortInfo(Convert.ToInt32(detailModel.ResortNo));
            Session["instanceResort"] = resortInfo;
            detailModel.ResortName = resortInfo.ResortName;
            detailModel.ResortAddress = resortInfo.Address;
            detailModel.ResortCityState = String.Concat(resortInfo.City, ", ", resortInfo.State, "    ", resortInfo.PostalCode);
            detailModel.ResortPhone = resortInfo.Phone;
            detailModel.Description = resortInfo.ShortDesc;
            detailModel.ResortImageLink = ""; //start with blank url incase the content does not exist in umbraco
            detailModel.ResortLink = "";

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
                            detailModel.ResortImageLink = content.GetPropertyValue<string>("ResortImage");
                            detailModel.ResortLink = content.Url;
                        }
                    }
                }
            }

            Session["resortname"] = detailModel.ResortName;
            Session["resortimage"] = detailModel.ResortImageLink;
            Session["resortAddress"] = detailModel.ResortAddress;
            Session["resortcity"] = detailModel.ResortCityState;
            Session["resortphone"] = detailModel.ResortPhone;
            Session["ResortNotes"] = detailModel.Description;


            //detailModel.ResortImageLink = "~/Content/Images/" + resortInfo.ImageName;
        }

        public void PopulateReservationInfo(ReservationDetailModel detailModel)
        {
            BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];

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

                if (detailModel.ReservationCondition == null || detailModel.ReservationCondition == "")
                    detailModel.ReservationCondition = "Future";

                if (detailModel.ReservationCondition == "Future")
                    history.SearchHistoryBy = BGO.ResortsService.ReservationHistoryType.Future;
                else
                    history.SearchHistoryBy = BGO.ResortsService.ReservationHistoryType.Past;

                if (detailModel.ReservationCondition == "")
                    history.SearchHistoryBy = BGO.ResortsService.ReservationHistoryType.Both;

                historyResult = reservationAS400.GetReservationsHistory(history);
                if (historyResult.Success)
                {
                    histories = historyResult.ReservationHistoryItem;
                    foreach (BGO.ResortsService.ReservationHistoryItem reservationItem in histories)
                    {
                        if (reservationItem._ReservationNumber == detailModel.ReservationNo)
                        {
                            //default settings
                            detailModel.CanCancel = true;
                            detailModel.CanUpdate = true;

                            detailModel.GuestName = reservationItem._GuestFullName;
                            detailModel.GuestId = reservationItem._GuestID;
                            detailModel.MaxOccupancy = reservationItem._MaximumOccupancy;
                            detailModel.ReservationType = ReservationType(reservationItem._ReservationType, reservationItem._ExchangeCode);
                            detailModel.NumberOfNights = Convert.ToInt32(reservationItem._NumberOfNights);
                            detailModel.NumberOfAdults = reservationItem._NumberOfAdults;
                            detailModel.PolicyStatus = PolicyStatus(reservationItem._PolicyStatus, reservationItem._EligibleDate, reservationItem._HistoryType.ToString(), reservationItem._ReservationNumber, reservationItem._ExchangeCode, reservationItem._ReservationType);
                            detailModel.PolicyPrice = reservationItem._PolicyPrice;
                            detailModel.Amount = reservationItem._AmountDue;
                            detailModel.CheckIn = NumericToDate(reservationItem._CheckInDate, "yyMMdd");
                            detailModel.CheckOut = CalcCheckOutDate(detailModel.NumberOfNights, reservationItem._CheckInDate);
                            detailModel.VillaSize = getVillaDescription(Convert.ToInt32(reservationItem._ProjectStay), reservationItem._AS400UnitType, detailModel);
                            detailModel.VillaDescription = GetBGOVillaDescription(detailModel.ResortNo, reservationItem._AS400UnitType, detailModel);


                            if (HandiCapAccessible(detailModel.ResortNo, reservationItem._AS400UnitType, reservationItem._UnitNumber, true, detailModel))
                            {
                                string path = BGModern.HtmlExtensions.CustomHtmlHelpers.GetFullSitePath(null).ToString();
                                detailModel.VillaSize = detailModel.VillaSize + "&nbsp;&nbsp;&nbsp;&nbsp;<img src=\"" + path + "/Content/Images/icn_wheelchair_accessible.gif\">" + "  (Wheelchair Accessible)";
                            }

                            Session["unitdecscription"] = detailModel.VillaSize;
                            Session["roomdecscription"] = detailModel.VillaDescription;

                            // save the villa size in the reservationItem._VillaDescription member, as that is what is used to display the villa size on the cancelation confirmation page
                            reservationItem._VillaDescription = detailModel.VillaSize;

                            if (detailModel.ReservationType == "PSE")
                                detailModel.ResortNo = "85";

                            if (detailModel.ReservationType == "Points")
                                detailModel.CanUpdate = true;
                            else
                                detailModel.CanUpdate = false;

                            string parentPath = BGModern.HtmlExtensions.CustomHtmlHelpers.GetParentSitePath(null).ToString();

                            if (detailModel.ReservationType == "PSE" && detailModel.ReservationType == "Freedom Plus")
                                detailModel.ResortLink = "";

                            if (reservationItem._Points == "")
                                detailModel.Points = "0";
                            else
                                detailModel.Points = String.Format("{0:#,###}", reservationItem._Points.Trim());

                            if (detailModel.ReservationType == "Bonus Time")
                            {
                                //Tax calculations are used for points
                                double tax = Convert.ToDouble(Utilities.getResortInfo(Convert.ToInt32(reservationItem._ProjectStay)).TaxRate);
                                if (detailModel.NumberOfNights == 1)
                                    detailModel.NumberOfNights = 2;

                                tax = tax * Convert.ToDouble(Convert.ToDouble(detailModel.Amount) / detailModel.NumberOfNights);  //The tax charge daily
                                tax = tax / 100;
                                tax = Math.Round(tax, 2, MidpointRounding.AwayFromZero);  //Round up tax charge to a penny.
                                double total = tax * detailModel.NumberOfNights + Convert.ToDouble(detailModel.Amount);

                                detailModel.PayType = "Amount:";
                                detailModel.Amount = String.Format("{0:c}", total);

                                if (Session["taxFeeForTheResort"] != null)
                                    Session["taxFeeForTheResort"] = null;
                                Session["taxFeeForTheResort"] = tax;
                            }
                            else if (detailModel.ReservationType == "Flex")
                            {
                                detailModel.PayType = "Nights:";
                                detailModel.Points = detailModel.NumberOfNights.ToString();
                            }
                            else
                                detailModel.PayType = "Points:";

                            Session["resConfNum"] = detailModel.ReservationNo;
                            Session["maxoccupancy"] = detailModel.MaxOccupancy;
                            Session["ResortNumber"] = detailModel.ResortNo;
                            //The following will be used for the PPP Cancellation Process
                            Session["PPPStatus"] = "";
                            Session["PPPFee"] = "";
                            Session["cancelPolicy"] = "";

                            if (detailModel.PolicyStatus == "Buy Now!")
                                detailModel.PolicyStatus = detailModel.PolicyStatus + "&nbsp;&nbsp; <a href=\"" + parentPath + "/owner/ptsPurchaseppp.aspx?Origin=myreservation&resno=" + detailModel.ReservationNo + "&ResortNo=" + detailModel.ResortNo + "\" class=\"textlink\" style=\"color:red\"><b>Add the Points Protection Plan to this reservation now</b></a>";

                            if (Convert.ToDateTime(detailModel.CheckIn) != System.DateTime.Today)
                            {
                                //If the reservation was set as not Points, it can not be cancel from the web.
                                //It must be cancel by whoever generate the reservation, like, RCI.
                                //Based on the new requirements reservation can only be cancel by whoever issued it:. Luiz - 04/25/2009
                                if (detailModel.ReservationType == "Points" || detailModel.ReservationType == "Bonus Time")
                                {
                                    detailModel.CancellationPolicy = GetCancellationPolicyForEmailConfirmation(reservationItem._AS400UnitType, reservationItem._ReservationType, reservationItem._OwnerProjectNumber, BXGOwner.accountNumber, detailModel);

                                    Session["cancelPolicy"] = detailModel.CancellationPolicy;

                                    if (detailModel.PolicyStatus == "Protected")
                                    {
                                        Session["PPPStatus"] = detailModel.PolicyStatus;
                                        Session["PPPFee"] = detailModel.PolicyPrice;
                                    }

                                }
                                else
                                {
                                    detailModel.CancellationPolicy = "This reservation cannot be cancelled online. If you need to cancel this reservation, please call 800.456.2582.";
                                    detailModel.CanCancel = false;
                                }
                            }
                            else
                            {
                                if (CheckPromoOut() && (detailModel.ReservationType == "Points" || detailModel.ReservationType == "Bonus Time"))
                                {
                                    detailModel.CancellationPolicy = GetCancellationPolicyForEmailConfirmation(reservationItem._AS400UnitType, reservationItem._ReservationType, reservationItem._OwnerProjectNumber, BXGOwner.accountNumber, detailModel);

                                    Session["cancelPolicy"] = detailModel.CancellationPolicy;

                                    if (detailModel.PolicyStatus == "Protected")
                                    {
                                        Session["PPPStatus"] = detailModel.PolicyStatus;
                                        Session["PPPFee"] = detailModel.PolicyPrice;
                                    }
                                }
                                else
                                {
                                    detailModel.CancellationPolicy = "This reservation cannot be cancelled online. If you need to cancel this reservation, please call 800.456.2582.";
                                    detailModel.CanCancel = false;
                                }
                            }

                            if (reservationItem._DateConfirmed == "")
                                detailModel.ConfirmationDate = "N/A";
                            else
                                detailModel.ConfirmationDate = ConvertAS400Date(reservationItem._DateConfirmed);


                            if (reservationItem._HistoryType == BGO.ResortsService.ReservationHistoryType.Past)
                            {
                                detailModel.CanCancel = false;
                                detailModel.CanUpdate = false;
                            }

                            Session["ReservationSelected4Details"] = reservationItem;
                        }
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
                errMsg.Append("Error happened fetching Reservations List");
                errMsg.Append(ex.Message);
                sendMail.sendMessage("OLPSupport@bxgcorp.com", "", "Exception Error - Getting Reservation List", errMsg);
            }

            //BGO.ResortsService.ReservationHistoryItem =
        }

        public void PopulateGuestList(ReservationDetailModel detailModel)
        {
            int countItems = 0;
            //bool isOwner = false;
            //int selectedCounterId = 0;
            List<OptionGroupItem> guestList = new List<OptionGroupItem>();
            guestList.Add(new OptionGroupItem(countItems.ToString(), "Please, select a Guest,", ""));

            //Add the Owners
            foreach (string strOwnerCheckInName in BXGOwner.AccountHolders)
            {
                countItems++;
                guestList.Add(new OptionGroupItem(countItems.ToString(), strOwnerCheckInName.Trim(), "Owner"));
                if (detailModel.SelectedGuest.Trim().ToUpper() == strOwnerCheckInName.Trim().ToUpper())
                    detailModel.SelectedGuestGroup = "Owner";
            }

            bool canAdd = true;
            bool isPointReservation = false;
            bool hasGuests = false;

            BGO.ResortsService.OwnerGuestList guests = BGO.OwnerGuest.GetGuests(BXGOwner.Arvact);
            if (guests.Guest.Length > 0)
            {
                hasGuests = true;
                foreach (BGO.ResortsService.Guest guest in guests.Guest)
                {
                    guestList.Add(new OptionGroupItem(guest.GuestID.ToString(), guest.LastName + ", " + guest.FirstName, "Guests"));
                    if (guest.LastName.Trim().ToUpper() + ", " + guest.FirstName.Trim().ToUpper() == detailModel.SelectedGuest.Trim().ToUpper())
                    {
                        //detailModel.SelectedGuest = detailModel.GuestName;
                        detailModel.SelectedGuestId = guest.GuestID;
                        detailModel.SelectedGuestGroup = "Guests";
                    }
                }
            }

            isPointReservation = (detailModel.ReservationType == "Points");

            if (BXGOwner.User[0].isSampler && isPointReservation)
            {
                if (BXGOwner.User[0].HomeProject == "51" || BXGOwner.User[0].HomeProject == "52")
                    canAdd = false;
            }


            detailModel.GuestTypes = new List<String>(new string[] { "", "Owner" });
            if (hasGuests)
                detailModel.GuestTypes.Add("Guests");

            if (canAdd)
            {
                guestList.Add(new OptionGroupItem("Add", "Add New Check-in Name", "New"));
                detailModel.GuestTypes.Add("New");
            }

            detailModel.GuestList = guestList;
        }

        public void PopulateRelationshipTypes(ReservationDetailModel detailModel)
        {
            List<string> relTypes = new List<string>();
            relTypes.Add("Select Guest Relationship Type");

            foreach (BGO.ResortsService.RelationshipType relationshipType in System.Enum.GetValues(typeof(BGO.ResortsService.RelationshipType)))
            {
                if (relationshipType.ToString() != "None")
                    relTypes.Add(relationshipType.ToString());
            }

            detailModel.RelationshipTypes = relTypes;
        }

        private void PopulateStates(ReservationDetailModel detailModel)
        {
            BGO.BluegreenOnline.clsDBConnectivity C = new BGO.BluegreenOnline.clsDBConnectivity();
            SqlDataReader dbDataReader;

            List<SelectListItem> states = new List<SelectListItem>();
            SelectListItem item = new SelectListItem();
            item.Value = "";
            item.Text = "Select a State";

            states.Add(item);

            C.dbCmnd.CommandText = "uspGetCountriesbyRegions";
            C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure;
            dbDataReader = C.dbCmnd.ExecuteReader();

            while (dbDataReader.Read())
            {
                SelectListItem stateItem = new SelectListItem();
                stateItem.Value = dbDataReader["StateId"].ToString();
                stateItem.Text = dbDataReader["StateDescription"].ToString();
                states.Add(stateItem);
            }

            detailModel.States = states;

            dbDataReader.Close();
            C.Close();
            C = null;
        }

        public void PopulateGuestForm(ReservationDetailModel detailModel)
        {
            if (detailModel.SelectedGuestGroup == "Owner")
            {
                //handle a very ODD exception that unfortunately can happen. Garbage in, Garbage out!
                //comma is not being used to separate Last Name and First Name. Also, we have learned that company name can be at the 
                //primary and/or secondary accounts names. Due to this ODD, definitely, there is no comma to identify last/first names.
                if (detailModel.SelectedGuest.IndexOf(",") != -1)
                {
                    detailModel.GuestFormFirstName = detailModel.SelectedGuest.Substring(detailModel.SelectedGuest.IndexOf(",") + 1);
                    detailModel.GuestFormLastName = detailModel.SelectedGuest.Substring(0, detailModel.SelectedGuest.IndexOf(","));

                    Session["garbageInFound"] = false;
                }
                else
                {
                    detailModel.GuestFormLastName = String.Empty;
                    detailModel.GuestFormFirstName = detailModel.SelectedGuest.ToString();
                    Session["garbageInFound"] = false;
                }

                detailModel.GuestFormEmail = BXGOwner.Email.ToString();
                detailModel.GuestFormCity = BXGOwner.City.ToString();
                detailModel.GuestFormPhone = BXGOwner.HomePhone.ToString();
                detailModel.GuestFormState = BXGOwner.StateAbr.ToString();
                detailModel.GuestFormRelationship = BGO.ResortsService.RelationshipType.Family.ToString();
                detailModel.GuestFormVisible = false;
            }

            if (detailModel.SelectedGuestGroup == "Guests")
            {
                detailModel.GuestFormFirstName = detailModel.SelectedGuest.Substring(detailModel.SelectedGuest.IndexOf(",") + 1);
                detailModel.GuestFormLastName = detailModel.SelectedGuest.Substring(0, detailModel.SelectedGuest.IndexOf(","));

                BGO.ResortsService.OwnerGuestList guests = BGO.OwnerGuest.GetGuests(BXGOwner.Arvact);
                if (guests.Guest.Length > 0)
                {
                    foreach (BGO.ResortsService.Guest guest in guests.Guest)
                    {
                        if (guest.GuestID == detailModel.SelectedGuestId)
                        {
                            detailModel.GuestFormVisible = true;
                            foreach (BGO.ResortsService.Email email in guest.Emails)
                            {
                                if (email.EmailType == BGO.ResortsService.EmailType.Primary)
                                    detailModel.GuestFormEmail = email.EmailAddress;
                            }

                            foreach (BGO.ResortsService.Address address in guest.Addresses)
                            {
                                if (address.AddressType == BGO.ResortsService.AddressType.Home ||
                                    address.AddressType == BGO.ResortsService.AddressType.Unknown)
                                {
                                    detailModel.GuestFormCity = address.City;

                                    if (address.StateCode == null)
                                        detailModel.GuestFormState = "";
                                    else
                                        detailModel.GuestFormState = address.StateCode;
                                }
                            }

                            foreach (BGO.ResortsService.Phone phone in guest.Phones)
                            {
                                if (phone.PhoneNumber != null)
                                    detailModel.GuestFormPhone = phone.PhoneNumber;
                            }

                            if (guest.Relationship == null)
                                detailModel.GuestFormRelationship = "Select Guest Relationship Type";
                            else
                                detailModel.GuestFormRelationship = guest.Relationship.ToString();
                        }
                    }
                }
            }

            if (detailModel.SelectedGuestGroup == "New")
            {
                detailModel.GuestFormRelationship = "Select Guest Relationship Type";
                detailModel.GuestFormFirstName = "";
                detailModel.GuestFormLastName = "";
                detailModel.GuestFormEmail = "";
                detailModel.GuestFormCity = "";
                detailModel.GuestFormPhone = "";
                detailModel.GuestFormState = "";
                detailModel.GuestFormVisible = true;
            }
        }

        public string NumericToDate(string source, string format)
        {
            if (source.Length == 0)
                return "00/00/0000";
            else if (source.Length == 3)
                source = "000" + source;
            else if (source.Length == 4)
                source = "00" + source;

            int numericChecker;

            if (source.Length >= 5 && int.TryParse(source, out numericChecker))
            {
                string strDate = "";
                if (source.Length == 5)
                    source = "0" + source;

                if (format == "yyMMdd")
                    strDate = source.Substring(2, 2) + "/" + source.Substring(4, 2) + "/" + source.Substring(0, 2);
                else if (format == "MMddy")
                    strDate = source.Substring(0, 2) + "/" + source.Substring(2, 2) + "/" + source.Substring(4, 2);

                return Convert.ToDateTime(strDate).ToString("d");
            }
            else
                return "00/00/0000";
        }

        public string CalcCheckOutDate(int days, string checkIn)
        {
            try
            {
                DateTime dtm = Convert.ToDateTime(NumericToDate(checkIn, "yyMMdd"));
                dtm = dtm.AddDays(days);
                return dtm.ToString("d");
            }
            catch (Exception ex)
            {
                return "00/00/00";
            }
        }

        public string isExchange(string x, ReservationDetailModel detailModel)
        {
            if (x.Trim() == "#")
                x = "Yes";
            else if (x.Trim() == "@")
                x = "Yes";
            else if (x.Trim() == "*")
                x = "Yes";
            else if (x.Trim() == "&")
                x = "Yes";
            else
                x = "No";

            //TODO: msgExchange
            detailModel.ExchangeVisible = (x == "Yes");

            return x;
        }

        public string ReservationType(string rt, string exch)
        {
            rt = Utilities.GetReservationDescription(rt.Trim());

            //Determine if this is an exchange reservation.  If it is, then exch will be populated.
            //if it is, then rt needs to be "exch*"  
            //This was added after a column labelled Exchange was removed from the reservation

            if (exch.Trim() == "#")
                rt = "Exch*";
            else if (exch.Trim() == "@")
                rt = "Exch*";
            else if (exch.Trim() == "*")
                rt = "Exch*";
            else if (exch.Trim() == "&")
                rt = "Exch*";

            return rt;
        }

        public string PolicyStatus(string Policy, string EligibilityDate, string ReservationCondition, string ReservationNbr, string ExchangeType, string ReservationType)
        {
            string _PolicyIsODate = "";
            string _dateTimeInfo = DateTime.Now.ToString("s").Substring(0, 10);
            int RowNo = 0;
            string _lnkClientId = "";
            string _ClientId = "";
            bool _PolicyEligible = false;
            string _Status = "";
            string policyStatus = "";

            if (ReservationCondition == "Future")
            {
                //--------------------------------------------------------------------------
                //PROMOCODE Program - There is a decision to open PPP out to all Future reservation without PPP.
                //Concern on this moment, on scenario, is 24 hours or today = check-in date to allow to buy PPP.
                //According to Chad today = check-in on his book the reservation will be allow to buy PPP
                //--------------------------------------------------------------------------
                if (CheckPromoOut())
                {
                    if (Policy == "A")
                        policyStatus = "Protected";
                    else
                        policyStatus = "Buy Now!";

                    //if reservation type is not <Points> do not allow it to buy PPP. Some type: Exchange, Flex or Bonus type, PM Unit Upgrade as <G>  and New Onwer/VIP as <Q>". Luiz 08/20/2010
                    //If Trim(ReservationType) = "F" Or Trim(ReservationType) = "B" Or Trim(ExchangeType) = "#" Or Trim(ExchangeType) = "@" Or Trim(ExchangeType) = "*" Or Trim(ExchangeType) = "&" Or Trim(ExchangeType) = "G" Or Trim(ExchangeType) = "Q" Then
                    if (ReservationType.Trim() != "P")
                        policyStatus = "";
                    else if (ExchangeType.Trim() == "#" || ExchangeType.Trim() == "@" || ExchangeType.Trim() == "*" || ExchangeType.Trim() == "&" || ExchangeType.Trim() == "G" || ExchangeType.Trim() == "Q")
                        policyStatus = "";
                }
                else
                {
                    //It's not suppose EligibilityDate be zero out, meaning, something got error or it can be an Exchange.
                    //Though Cancellation will procede without see the PPP status in Blank.
                    if (EligibilityDate != "0")
                    {
                        _PolicyIsODate = Convert.ToDateTime(EligibilityDate.Substring(4, 2) + "/" + EligibilityDate.Substring(6, 2) + "/" + EligibilityDate.Substring(0, 4)).ToString("d");

                        if (Convert.ToDateTime(_dateTimeInfo) <= Convert.ToDateTime(_PolicyIsODate))
                            _PolicyEligible = true;
                        else
                            _PolicyEligible = false;

                        if (Policy == "A")
                            policyStatus = "Protected";
                        else if (Policy == "D")
                        {
                            if (_PolicyEligible && ReservationType.Trim() == "P")
                                policyStatus = "Buy Now!";
                            else
                                policyStatus = "Not Protected";
                        }
                        else
                        {
                            ErrorHappened(ReservationNbr, Policy);
                        }

                        if (ExchangeType.Trim() == "#" || ExchangeType.Trim() == "@" || ExchangeType.Trim() == "*" || ExchangeType.Trim() == "&" || ExchangeType.Trim() == "G" || ExchangeType.Trim() == "Q")
                            policyStatus = "";
                    }
                    else
                    {
                        //if reservation type is not <Points> do not allow it to buy PPP. Some type: Exchange, Flex or Bonus type, PM Unit Upgrade as <G>  and New Onwer/VIP as <Q>". Luiz 08/20/2010
                        //If Trim(ReservationType) = "F" Or Trim(ReservationType) = "B" Or Trim(ExchangeType) = "#" Or Trim(ExchangeType) = "@" Or Trim(ExchangeType) = "*" Or Trim(ExchangeType) = "&" Or Trim(ExchangeType) = "G" Or Trim(ExchangeType) = "Q" Then
                        if (ReservationType.Trim() != "P")
                            policyStatus = "";
                        else if (ExchangeType.Trim() == "#" || ExchangeType.Trim() == "@" || ExchangeType.Trim() == "*" || ExchangeType.Trim() == "&" || ExchangeType.Trim() == "G" || ExchangeType.Trim() == "Q")
                            policyStatus = "";

                    }
                }
            }
            else //<---- Past Reservations
            {
                if (policyStatus == "A")
                    policyStatus = "Protected";
                else if (policyStatus == "S")
                    policyStatus = "Consumed";
                else if (policyStatus == "D" || policyStatus == "" || policyStatus == " ")
                    policyStatus = "Not Protected";
                else if (policyStatus == "E")
                    policyStatus = "Expired";
                else if (policyStatus == "T")
                    policyStatus = "Transfered";
                else if (policyStatus == "X")
                    policyStatus = "Refunded";
                else
                    policyStatus = "-";
            }

            return policyStatus;
        }

        public void ErrorHappened(string ReservationNbr, string PPPStatus)
        {
            StringBuilder errMsg = new StringBuilder();
            errMsg.Append("Reservation #:" + ReservationNbr + System.Environment.NewLine);
            errMsg.Append("PPP Status:" + PPPStatus + System.Environment.NewLine);

            sendMail.sendMessage("olpsupport@bluegreencorp.com", "", "PPP status irregular ", errMsg);
        }

        public string ConvertAS400Date(string _date)
        {
            string dateString = _date; //Format:ymmdd
            string month = "";
            string day = "";
            string year = "";
            string resultMonth = "";
            string resultDay = "";
            string resultYear = "";

            //store string length in variable
            int strLength = dateString.Length - 1;

            //set the counter to start from the end of the string
            int cnt = dateString.Length - 1;

            if (dateString.Length == 5)
            {
                while (cnt >= 0)
                {
                    //get the two last characters and store them in day string variable
                    if (!(cnt == (strLength) - 2) & cnt > (strLength) - 2)
                    {
                        day += dateString[cnt];
                        cnt = cnt - 1; //decrement counter
                    }

                    //get two characters preceeding two last charcaters and store them in month variable
                    if (cnt <= (strLength) - 2 & !(cnt == (strLength) - 4))
                    {
                        month += dateString[cnt];
                        cnt = cnt - 1;
                    }

                    if (cnt == (strLength) - 4)
                    {
                        year += dateString[cnt];
                        cnt = cnt - 1;

                    }
                }

                //reversing strings order to get right date format
                cnt = day.Length - 1;

                while (cnt >= 0)
                {
                    resultDay += day[cnt];
                    cnt = cnt - 1;
                }

                cnt = month.Length - 1;

                while (cnt >= 0)
                {
                    resultMonth += month[cnt];
                    cnt = cnt - 1;
                }

                resultYear = "0" + year;
                return resultMonth + "/" + resultDay + "/" + resultYear;
            }
            else
            {
                while (cnt >= 0)
                {
                    //get the two last characters and store them in day string variable
                    if (!(cnt == (strLength) - 2) & cnt > (strLength) - 2)
                    {
                        day += dateString[cnt];
                        cnt = cnt - 1; //decrement counter
                    }

                    //get two characters preceeding two last charcaters and store them in month variable 
                    if (cnt <= (strLength) - 2 & cnt > (strLength) - 4)
                    {
                        month += dateString[cnt];
                        cnt = cnt - 1;
                    }

                    //get remaining character and store it in year variable
                    if (cnt <= (strLength) - 4)
                    {
                        year += dateString[cnt];
                        cnt = cnt - 1;
                    }
                }

                //reversing strings order to get right date format
                cnt = day.Length - 1;
                while (cnt >= 0)
                {
                    resultDay += day[cnt];
                    cnt = cnt - 1;
                }

                cnt = month.Length - 1;
                while (cnt >= 0)
                {
                    resultMonth += month[cnt];
                    cnt = cnt - 1;
                }

                cnt = year.Length - 1;

                while (cnt >= 0)
                {
                    resultYear += year[cnt];
                    cnt = cnt - 1;
                }

                return resultMonth + "/" + resultDay + "/" + resultYear;
            }
        }

        public string GetBGOVillaDescription(string AS400projectnumber, string AS400UnitType, ReservationDetailModel detailModel)
        {
            BGO.BluegreenOnline.clsDBConnectivityBGR CR = new BGO.BluegreenOnline.clsDBConnectivityBGR();
            string villaDescription = "";

            SqlDataReader drVillaDescription = null;

            try
            {
                CR.dbCmnd.CommandText = "uspSelectUnitDescription";
                CR.dbCmnd.CommandType = CommandType.StoredProcedure;
                CR.dbCmnd.Parameters.Clear();

                CR.dbCmnd.Parameters.Add(new SqlParameter("@projectnumber", System.Data.SqlDbType.Int)).Value = AS400projectnumber;
                CR.dbCmnd.Parameters.Add(new SqlParameter("@unitnumber", System.Data.SqlDbType.Char)).Value = "";
                CR.dbCmnd.Parameters.Add(new SqlParameter("@As400UnitType", System.Data.SqlDbType.Char)).Value = AS400UnitType;

                drVillaDescription = CR.dbCmnd.ExecuteReader();
                while (drVillaDescription.Read())
                {
                    return drVillaDescription["BluegreenRoomDescription"].ToString();
                }
            }
            catch (Exception ex)
            {
                StringBuilder errMsg = new StringBuilder();
                errMsg.Append(ex.Message);
                retrievalError("BGO Villa Description", detailModel);
            }
            finally
            {
                if (drVillaDescription != null)
                    drVillaDescription.Close();
                CR.Close();
            }

            return "";
        }

        public void populateOccupancy(ReservationDetailModel detailModel)
        {
            int maxOcc = Convert.ToInt32(detailModel.MaxOccupancy);
            List<int> occupancyList = new List<int>();

            for (int x = 1; x <= maxOcc; x++)
            {
                occupancyList.Add(x);
            }

            detailModel.Occupancy = occupancyList;
        }

        public bool HandiCapAccessible(string AS400projectnumber, string AS400UnitType, string AS400UnitNumber, bool Occupancy, ReservationDetailModel detailModel)
        {
            BGO.BluegreenOnline.clsDBConnectivityBGR CR = new BGO.BluegreenOnline.clsDBConnectivityBGR();
            bool HandiCapAccessible = true;
            string maxOccupancy = "";
            SqlDataReader drhandicap = null;

            try
            {
                CR.dbCmnd.CommandText = "uspGetUnitInfo";
                CR.dbCmnd.CommandType = CommandType.StoredProcedure;
                CR.dbCmnd.Parameters.Clear();

                CR.dbCmnd.Parameters.Add(new SqlParameter("@projectnumber", System.Data.SqlDbType.Int)).Value = AS400projectnumber;
                CR.dbCmnd.Parameters.Add(new SqlParameter("@UnitType", System.Data.SqlDbType.Char)).Value = AS400UnitType;
                CR.dbCmnd.Parameters.Add(new SqlParameter("@unitnumber", System.Data.SqlDbType.Char)).Value = AS400UnitNumber;

                drhandicap = CR.dbCmnd.ExecuteReader();
                while (drhandicap.Read())
                {
                    HandiCapAccessible = Convert.ToBoolean(drhandicap["HandicapAccessible"].ToString());
                }
            }
            catch (Exception ex)
            {
                StringBuilder errMsg = new StringBuilder();
                retrievalError("handcapAccessible", detailModel);
            }
            finally
            {
                if (drhandicap != null)
                    drhandicap.Close();
                CR.Close();
            }

            return HandiCapAccessible;
        }

        public string getVillaDescription(int pn, string ut, ReservationDetailModel detailModel)
        {
            string xx = "";

            try
            {
                xx = Utilities.getUnitDescription(pn, ut);

                if (string.IsNullOrEmpty(xx))
                    xx = "";
            }
            catch (Exception ex)
            {
                string Path = "";
                StringBuilder errMsg = new StringBuilder();

                string sHost = ((String)(Request.ServerVariables["HTTP_HOST"] + Request.ServerVariables["PATH_INFO"])).ToLower().Replace("www.", "");
                Path = "http://" + sHost.Substring(0, sHost.IndexOf("/")) + System.Configuration.ConfigurationManager.AppSettings["bxgwebImgPath"];

                errMsg.Append("Machine: " + Path);
                errMsg.Append(ex.Message);

                sendMail.sendMessage("OLPSupport@bluegreenvacation.com", "", " verifyScenary procedure ", errMsg);

                retrievalError(errMsg.ToString(), detailModel);
            }

            return xx;
        }

        public string GetCancellationPolicyForEmailConfirmation(string projectnumber, string ReservationTypeName, string OwnerProject, string OwnerAccount, ReservationDetailModel detailModel)
        {
            BGO.BluegreenOnline.clsDBConnectivityBGR CR = new BGO.BluegreenOnline.clsDBConnectivityBGR();
            SqlDataReader drReservations = null;

            try
            {
                CR.dbCmnd.CommandText = "uspSelectPolicyForEmailConfirmation";
                CR.dbCmnd.CommandType = CommandType.StoredProcedure;
                CR.dbCmnd.Parameters.Clear();

                if (OwnerProject == "50")
                    CR.dbCmnd.Parameters.Add(new SqlParameter("@ProjectNumber", System.Data.SqlDbType.Int)).Value = 50;
                else
                    CR.dbCmnd.Parameters.Add(new SqlParameter("@ProjectNumber", System.Data.SqlDbType.Int)).Value = Convert.ToInt32(OwnerProject);

                if (OwnerProject == "50")
                    CR.dbCmnd.Parameters.Add(new SqlParameter("@OwnerType", System.Data.SqlDbType.Char)).Value = "V";
                else if (OwnerAccount != null)
                {
                    if (OwnerAccount.StartsWith("88888") || OwnerAccount.StartsWith("99999") || OwnerAccount.StartsWith("77777") || OwnerAccount.StartsWith("22222"))
                        CR.dbCmnd.Parameters.Add(new SqlParameter("@OwnerType", System.Data.SqlDbType.Char)).Value = "O";
                    else
                        CR.dbCmnd.Parameters.Add(new SqlParameter("@OwnerType", System.Data.SqlDbType.Char)).Value = "NV";
                }

                CR.dbCmnd.Parameters.Add(new SqlParameter("@ReservationTypeName", System.Data.SqlDbType.Char)).Value = ReservationTypeName;

                drReservations = CR.dbCmnd.ExecuteReader();
                while (drReservations.Read())
                {
                    return drReservations["CancellationPolicy"].ToString();
                }
            }
            catch (Exception ex)
            {
                StringBuilder errMsg = new StringBuilder();
                errMsg.Append(ex.Message);
                retrievalError("detail", detailModel);
            }
            finally
            {
                if (drReservations != null)
                    drReservations.Close();
                CR.Close();
            }

            return "";
        }

        public bool CheckPromoOut()
        {
            BGO.BluegreenOnline.clsDBConnectivity connection = new BGO.BluegreenOnline.clsDBConnectivity();
            bool checkPromoOut = false;

            SqlDataReader Promo = null;

            try
            {
                connection.dbCmnd.CommandText = "uspPromoExecute";
                connection.dbCmnd.CommandType = CommandType.StoredProcedure;
                connection.dbCmnd.Parameters.Clear();

                connection.dbCmnd.Parameters.Add(new SqlParameter("@aka", System.Data.SqlDbType.Char)).Value = "PPP";

                Promo = connection.dbCmnd.ExecuteReader();
                while (Promo.Read())
                {
                    checkPromoOut = Convert.ToBoolean(Promo["Actived"]);
                }
            }
            catch (Exception ex)
            {
                StringBuilder errMsg = new StringBuilder();
                errMsg.Append(ex.Message);
                Response.Write("Promo status cannot be loaded " + ex.Message + "<br />");
            }
            finally
            {
                if (Promo != null)
                    Promo.Close();
                connection.Close();
            }

            return checkPromoOut;
        }

        public void retrievalError(string location, ReservationDetailModel detailModel)
        {
            detailModel.ErrorText = "We're sorry but we are unable to process your request at this time.  Please try again later.";
        }
    }
}
