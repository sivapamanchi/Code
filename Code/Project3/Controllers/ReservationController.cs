using BGO.ResortsService;
using BGSitecore.Components;
using BGSitecore.Models;
using BGSitecore.Models.OwnerService.OwnerWebStatsRequest;
using BGSitecore.Models.Resort;
using BGSitecore.Models.ResortService.AcceptPointsProtection;
using BGSitecore.Models.ResortService.BookReservationRequest;
using BGSitecore.Models.ResortService.BookReservationResponse;
using BGSitecore.Models.ResortService.DeclinePointsProtection;
using BGSitecore.Models.ResortService.ModifyReservationRequest;
using BGSitecore.Models.ResortService.ReservationsList;
using BGSitecore.Services;
using BGSitecore.Utils;
using Glass.Mapper.Sc.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace BGSitecore.Controllers
{
    public class ReservationController : GlassController
    {

        public ActionResult ResortPreviewDetails()
        {
            DebugUtils.StartLogEvent("ReservationController.ResortPreviewDetails");

            var model = GetLayoutItem<ResortPreviewDetails>();

            ResortDetails resortDetail = null;


            if (Session["ActiveReservation"] != null)
            {
                Reservation activeReservation = (Reservation)Session["ActiveReservation"];
                resortDetail = ResortManager.GetResortByProject(activeReservation.ProjectStay);
                model.ActiveReservation = activeReservation;
                model.HandicapAccessible = false;

                ResortService resortService = new ResortService();
                var UnitInfo = resortService.GetUnitInfo(model.ActiveReservation.ProjectStay, model.ActiveReservation.AS400UnitType, model.ActiveReservation.UnitNumber);
                if (UnitInfo != null)
                {
                    model.HandicapAccessible = (UnitInfo.HandicapAccessible =="1");
                }

                Session["ActiveReservation"] = null;
            }
            else
            {
                ReservationParameters reservationContext = ReservationUtils.GetContextReservation();

                if (reservationContext != null)
                {
                    resortDetail = ResortManager.FindResort(reservationContext.ResortId);
                    model.ActiveReservation = new BGSitecore.Models.ResortService.ReservationsList.Reservation();
                    model.ActiveReservation.AS400UnitType = reservationContext.UnitType;
                    model.ActiveReservation.ProjectStay = reservationContext.ProjectNumber.ToString();
                    model.ActiveReservation.CheckInDate = reservationContext.CheckInDate.ToString("yyyyMMdd");
                    model.ActiveReservation.CheckOutDate = reservationContext.CheckOutDate.ToString("yyyyMMdd");
                    model.ActiveReservation.ReservationType = reservationContext.ReservationType;
                    model.ActiveReservation.Points = reservationContext.PointsRequired.ToString();
                    model.ActiveReservation.MaximumOccupancy = reservationContext.MaxOccupancy.ToString();

                    model.ActiveReservation.Amount = reservationContext.BT_TotalCost;
                    model.HandicapAccessible = (!string.IsNullOrEmpty(reservationContext.wheelchairaccessible) && (reservationContext.wheelchairaccessible == "1"));

                }
            }
            model.ResortDetail = resortDetail;
            DebugUtils.EndLogEvent("ReservationController.ResortPreviewDetails");

            return View(model);
        }

        [HttpPost]
        public ActionResult BonusTimeReservationSubmit(ReservationParameters model)
        {
            model.AcceptTermsAndConditions = (HttpContext.Request.Form["confirm_toc"] != null);
            ReservationParameters reservationContext = ReservationUtils.GetContextReservation();

            reservationContext.CreditCard_Type = FormatUtils.ConvertCreditCard(model.CreditCard_Type); //Required for the validation
            var listOfError = ValidationUtils.GetCreditCardViolations(model);
            listOfError.AddRange( ValidationUtils.GetAddressViolations(model));
            listOfError.AddRange(ValidationUtils.GetGuestViolations(model, reservationContext.MaxOccupancy));
            Session["ChangedCountry"] = null;
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
                reservationContext = ReservationUtils.MapGuestValues(model, reservationContext);
                reservationContext = ReservationUtils.MapAddressValues(model, reservationContext);
                reservationContext = ReservationUtils.MapCreditCardValues(model, reservationContext);

                var reservationResponse = ExecuteBook(reservationContext);
                if (reservationResponse != null && reservationResponse.Errors == null)
                {
                    ReservationUtils.DeleteContextReservation();
                    //Response.Flush();
                    Response.Redirect("/owner/reservation-confirmation?type=Future&reservationNo=" + reservationResponse.ConfirmReservation.ReservationNumber, false);
                    HttpContext.ApplicationInstance.CompleteRequest();
                    return null;
                }
                else
                {
                    ModelState.AddModelError("", "We're sorry, but since you began your search, that villa type is no longer available. Please select BACK to begin a new search.");
                }
            }

            return base.Index();
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConfirmReservationSubmit(ReservationParameters model)
        {
            model.AcceptTermsAndConditions = (HttpContext.Request.Form["confirm_toc"] != null);

            ReservationParameters reservationContext = ReservationUtils.GetContextReservation();

            Session["PPPUiError"] = null;
            ValidationUtils.ClearModelErrors(ModelState);

            reservationContext = ReservationUtils.MapGuestValues(model, reservationContext);
            // var errors = ValidationUtils.GetGuestViolations(model, reservationContext.MaxOccupancy);
            var errors = new List<RuleViolation>();
            if (!ValidationUtils.ErrorsDetected(ModelState, errors))
            {
                Session["ReservationContext"] = reservationContext;
                var reservationResponse = ExecuteBook(reservationContext);

                if (reservationResponse != null && reservationResponse.Errors == null)
                {
                    OwnerUtils.SetContextToReloadPalett();

                    Session["ReservationNumber"] = reservationResponse.ConfirmReservation.ReservationNumber;
                    Response.Redirect("/owner/Points-Protection-Plan", false);
                    return null;
                }
                else
                {
                    ModelState.AddModelError("","We're sorry, but since you began your search, that villa type is no longer available. Please select BACK to begin a new search.");
                }
            }

            return base.Index();
        }

        [HttpPost]
        public ActionResult SubmitPPP(BGSitecore.Models.ReservationParameters model)
        {
            DebugUtils.StartLogEvent("ReservationController.SubmitPPP");

            ReservationParameters reservationContext = ReservationUtils.GetContextReservation();

            model.CreditCard_Type = FormatUtils.ConvertCreditCard(reservationContext.CreditCard_Type);
            if (model.btnSubmit == "action:nocreditcard")
            {
                DeclinePointsProtectionRequest request = new DeclinePointsProtectionRequest();
                request.SiteName = "";
                request.ReservationNumber = Session["ReservationNumber"].ToString();

                ResortService service = new ResortService();
                var response = service.DeclinePointsProtection(request);
                var reservationNUmber = Session["ReservationNumber"].ToString();
                PPPOwnerWebStats(reservationContext, false);

                ReservationUtils.DeleteContextReservation();

                Response.Redirect("/owner/reservation-confirmation?bv=true&type=Future&reservationNo=" + reservationNUmber, false);
                return null;

            }
            else
            {
                var listOfError = ValidationUtils.GetCreditCardViolations(model);
                if (listOfError.Count() <= 0)
                {
                    AcceptPointsProtectionRequest request = new AcceptPointsProtectionRequest();
                    request.SiteName = "OnlinePoints";
                    request.ReservationNumber = Session["ReservationNumber"].ToString();
                    request.Payment = new Models.ResortService.AcceptPointsProtection.Payment();
                    request.Payment.CreditCardNumber = model.CreditCard_Number;
                    request.Payment.CreditCardExpirationDate = ReservationUtils.GetExpDate(model.CreditCard_ExpDateMonth, model.CreditCard_ExpDateYear);
                    request.Payment.CreditCardType = "V"; // model.CreditCard_Type;
                    request.Payment.CreditCardName = model.CreditCard_Name;
                    request.Payment.CreditCardAuthorization = model.CreditCard_VerificationNumber;
                    request.Payment.CreditCardTotal = Convert.ToString(reservationContext.PPPCost);
                    request.Payment.NonTaxTotal = "";
                    ResortService service = new ResortService();
                    var pppResponse = service.AcceptPointsProtection(request);
                    if (pppResponse == null || pppResponse.Errors != null)
                    {
                        //TODO move this message in sitecore
                        listOfError.Add(new RuleViolation("", "", "Unfortunately, we have encountered a technical error while processing Points Protection Plan.Please call 800.456.CLUB(2582) to report the problem and receive assistance.Thank you."));
                        Session["PPPUiError"] = listOfError;

                    }
                    else
                    {
                        var reservationNUmber = Session["ReservationNumber"].ToString();
                        PPPOwnerWebStats(reservationContext, true);
                        ReservationUtils.DeleteContextReservation();
                        DebugUtils.StartLogEvent("ReservationController.SubmitPPP");

                        Response.Redirect("/owner/reservation-confirmation?bv=true&reservationNo=" + reservationNUmber, false);
                        return null;
                    }
                }
                else
                {
                    Session["PPPUiError"] = listOfError;
                }

            }

            DebugUtils.StartLogEvent("ReservationController.SubmitPPP");

            return base.Index();
        }

        public ActionResult PointsProtectionPlan()
        {
            var model = GetLayoutItem<PointsProtectionPlan>();
            if (Request.QueryString["reservationNo"] == null)
            {
                ReservationParameters reservationContext = ReservationUtils.GetContextReservation();
                if (reservationContext == null)
                {

                    ReservationUtils.HandelMissingReservationContext();
                }
                else
                {
                    model.payment = reservationContext.PPPCost.ToString();
                    model.points = reservationContext.PointsRequired;
                }
            }
            else
            {
                //Get the reservation details from service call
                BlueGreenContext bgcontext = new BlueGreenContext();
                var ActiveReservation = bgcontext.GetActiveReservation(Request.QueryString["reservationNo"], ResortService.RESERVATION_TYPE_FUTURE);
                model.payment = ActiveReservation.PolicyPrice;
                model.points = FormatUtils.ConvertStringToInt(ActiveReservation.Points);
                Session["ReservationNumber"] = ActiveReservation.ReservationNumber;
                ReservationParameters reservationContext = new ReservationParameters();
                reservationContext.PPPCost = Convert.ToDecimal( model.payment);
                reservationContext.PointsRequired = FormatUtils.ConvertStringToInt(ActiveReservation.Points);
                Session["ReservationContext"] = reservationContext;

            }
            if (Session["PPPUiError"] != null)
            {
                var allPreviousError = (List<RuleViolation>)Session["PPPUiError"];
                foreach (RuleViolation item in allPreviousError)
                {
                    ModelState.AddModelError("", item.ErrorMessage);

                }
                Session["PPPUiError"] = null;



            }

            return View(model);
        }

        public ActionResult CreditCardDetails(PointsProtectionPlan ppp)
        {

            var model = GetLayoutItem<PointsProtectionPlan>();

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateReservation(string ReservationNumber, string ReservationType, string AS400UnitType, string CheckInDate, string ProjectStay, string NumberOfNights, string DropDownName, string newGuest, string NumberOfGuest, string FirstName, string LastName, string Email, string Phone, string City, string State, string Relationship, string MaxOccupancy, string GuestID)
        {
            JsonResponse jsonResponse = new JsonResponse();
            jsonResponse.errors = new List<string>();
            jsonResponse.RetCode = "0";  //default to error
            ReservationHistoryItem sessionObject = (ReservationHistoryItem)Session["ReservationSelected4Details"];

            if (jsonResponse.errors.Count <= 0)
            {
                ModifyReservationRequest request = new ModifyReservationRequest();
                if (ReservationType == "P")
                {
                    request.SiteName = "onlinepoints";
                    request.ReservationType = "P";
                    request.Points = sessionObject._Points;
                }
                else
                {
                    request.SiteName = "BonusTime";
                    request.ReservationType = "B";

                }
                BlueGreenContext bgContext = new BlueGreenContext();
                request.OwnerID = FormatUtils.ConvertStringToInt(bgContext.OwnerId);
                request.ReservationProjectNumber = ProjectStay;
                request.UnitTypeCode = AS400UnitType;
                DateTime checkIn = DateTime.Parse(FormatUtils.GetDate(CheckInDate));

                request.CheckInDate = checkIn.ToString("MM/dd/yyyy");
                request.LengthOfStay = FormatUtils.ConvertStringToInt(NumberOfNights);
                request.ReservationNumber = ReservationNumber;
                request.NumberOfAdults = FormatUtils.ConvertStringToInt(NumberOfGuest);
                request.Guests = new List<Models.ResortService.ModifyReservationRequest.Guest>();
                if (newGuest == "OwnerSelected")
                {
                    Models.ResortService.ModifyReservationRequest.Guest guest = new Models.ResortService.ModifyReservationRequest.Guest();

                    guest.GuestType = "Primary";
                    //guest.GuestID = FormatUtils.ConvertStringToInt(GuestID);
                    guest.FirstName = FirstName;
                    guest.LastName = LastName;
                    request.Guests.Add(guest);
                }
                else
                {
                    Models.ResortService.ModifyReservationRequest.Guest guest = new Models.ResortService.ModifyReservationRequest.Guest();

                    guest.GuestType = "GuestOnPrimary";
                    guest.GuestID = FormatUtils.ConvertStringToInt(GuestID);
                    guest.FirstName = FirstName;
                    guest.LastName = LastName;
                    guest.Relationship = Relationship;
                    if (!string.IsNullOrEmpty(Email))
                    {
                        guest.EmailAddresses = new List<Models.ResortService.ModifyReservationRequest.EmailAddress>();
                        var email = new Models.ResortService.ModifyReservationRequest.EmailAddress();
                        email.AddressType = "Home";
                        email.Email = Email;
                        guest.EmailAddresses.Add(email);
                    }
                    guest.Addresses = new List<Models.ResortService.ModifyReservationRequest.Address>();
                    var address = new Models.ResortService.ModifyReservationRequest.Address();
                    address.AddressType = "Home";
                    address.City = City;
                    address.ProvinceCode = State;

                    guest.Addresses.Add(address);

                    if (!string.IsNullOrEmpty(Phone))
                    {
                        guest.Phones = new List<Models.ResortService.ModifyReservationRequest.Phone>();
                        var phone = new Models.ResortService.ModifyReservationRequest.Phone();
                        phone.PhoneNumberType = "Home";
                        phone.PhoneNumber = Phone;
                        guest.Phones.Add(phone);
                    }
                    request.Guests.Add(guest);
                }

                ResortService resortService = new Services.ResortService();

                var response = resortService.ModifyReservation(request);
                if (response.Errors == null)
                {
                    sessionObject._GuestFullName = LastName + ", " + FirstName;
                    sessionObject._NumberOfAdults = NumberOfGuest;
                    Session["ReservationSelected4Details"] = sessionObject;
                    jsonResponse.RetCode = "0";
                }
                else
                {
                    jsonResponse.errors.Add("Error updating the reservation");
                }

            }
            return Json(jsonResponse);
        }

        [HttpGet]
        public ActionResult ReservationConfirmation()
        {
            var model = GetLayoutItem<ReservationConfirmation>();
            BlueGreenContext bgcontext = new BlueGreenContext();
            var reservationNo = Request.QueryString["reservationNo"];
            if (!string.IsNullOrEmpty(reservationNo))
            {
                var reservationType = Request.QueryString["type"];
                model.ActiveReservation = bgcontext.GetActiveReservation(reservationNo, reservationType);

                model.BxgOwner = bgcontext.bxgOwner;
                if (model.ActiveReservation != null)
                {
                    model.taxTotal = ReservationUtils.CalculateReservationTax(model.ActiveReservation);
                    model.ActiveReservation.Amount = model.taxTotal + model.ActiveReservation.AmountDue;
                    model.Guest_NumberOfGuest = model.ActiveReservation.NumberOfAdults;

                    string specialCharacter = Sitecore.Configuration.Settings.GetSetting("IdentifierForSpecialRequestFromBGO");
                    model.ActiveReservation.Comments = StringUtils.Between(model.ActiveReservation.Comments, specialCharacter, specialCharacter);

                    Session["ActiveReservation"] = model.ActiveReservation;

                    Session["instanceResort"] =ResortUtils.GetResortInfoDetails(FormatUtils.ConvertStringToInt(model.ActiveReservation.ProjectStay), model.ActiveReservation.MaximumOccupancy);

                    //setup session object requied by the Cancel page
                    ReservationHistoryItem tmp = new ReservationHistoryItem();  //This is the object cancel expect in the session
                    DateTime DateConfirmed = DateTime.Parse(FormatUtils.GetDate(model.ActiveReservation.DateConfirmed));
                    tmp._DateConfirmed = DateConfirmed.ToString("yyyyMMdd");


                    tmp._Points = model.ActiveReservation.Points;
                    DateTime checkinDate = DateTime.Parse(FormatUtils.GetDate(model.ActiveReservation.CheckInDate));
                    tmp._CheckInDate = checkinDate.ToString("yyyyMMdd");
                    tmp._ReservationType = model.ActiveReservation.ReservationType;
                    tmp._NumberOfNights = model.ActiveReservation.NumberOfNights.ToString();
                    tmp._Amount = model.ActiveReservation.Amount.ToString();
                    tmp._AmountDue = model.ActiveReservation.AmountDue.ToString();
                    tmp._ReservationNumber = model.ActiveReservation.ReservationNumber;
                    tmp._ReservationStatus = model.ActiveReservation.ReservationStatus;
                    tmp._GuestFullName = model.ActiveReservation.Guests[0].FullName;
                    tmp._NumberOfAdults = model.ActiveReservation.NumberOfAdults;
                    tmp._MaximumOccupancy = model.ActiveReservation.MaximumOccupancy;
                    tmp._Handicap = model.ActiveReservation.Handicap;

                    var Room = ResortManager.GetRoom(Convert.ToInt16(model.ActiveReservation.ProjectStay), model.ActiveReservation.AS400UnitType);
                    if (Room != null)
                    {
                        tmp._VillaDescription = Room.ViewTitle;
                    }
                    tmp._PolicyStatus = FormatUtils.PolicyStatus(model.ActiveReservation.PolicyStatus, model.ActiveReservation.EligibleDate, reservationType, model.ActiveReservation.ReservationNumber, model.ActiveReservation.ExchangeCode, model.ActiveReservation.ReservationType);
                    Session["ReservationSelected4Details"] = tmp;


                    Session["PPPStatus"] = tmp._PolicyStatus;
                    Session["PPPFee"] = model.ActiveReservation.PolicyPrice;
                    Session["resConfNum"] = model.ActiveReservation.ReservationNumber;

                    //Only get the Guests when reservation is Points
                    if (model.ActiveReservation.ReservationType == "P")
                    {
                        BGSitecore.Services.ResortService resortService = new BGSitecore.Services.ResortService();
                        var allGuest = resortService.OwnerGuestList(bgcontext.OwnerId);
                        if (allGuest != null && allGuest.Guests != null && allGuest.Guests.Count() > 0)
                        {
                            if (model.allGuest == null)
                            {
                                model.allGuest = new List<BGSitecore.Models.ResortService.ScreeningBookReservationResponse.Guest>();
                            }
                            foreach (BGSitecore.Models.ResortService.OwnerGuestList.Guest guest in allGuest.Guests)
                            {
                                if (model.ActiveReservation.Guests != null && model.ActiveReservation.Guests.Count > 0)
                                {
                                    if (guest.GuestID == model.ActiveReservation.Guests[0].GuestID)
                                    {
                                        //Email and phone number are not return with the reservation.  We need to get those values from the list or guests 
                                        if (guest.Emails != null && guest.Emails.Count > 0)
                                        {
                                            model.ActiveReservation.Guests[0].Emails = new List<Models.ResortService.ReservationsList.Email>();
                                            Models.ResortService.ReservationsList.Email email = new Models.ResortService.ReservationsList.Email();
                                            email.EmailAddress = guest.Emails[0].EmailAddress;
                                            model.ActiveReservation.Guests[0].Emails.Add(email);
                                        }
                                        if (guest.Phones != null && guest.Phones.Count > 0)
                                        {
                                            model.ActiveReservation.Guests[0].Phones = new List<Models.ResortService.ReservationsList.Phone>();
                                            Models.ResortService.ReservationsList.Phone phone = new Models.ResortService.ReservationsList.Phone();
                                            phone.PhoneNumber = guest.Phones[0].PhoneNumber;
                                            model.ActiveReservation.Guests[0].Phones.Add(phone);
                                        }
                                    }
                                }
                                var findExistingGuest = from existingGuest in model.ActiveReservation.Guests
                                                        where existingGuest.FullName == guest.LastName + " ," + guest.FirstName
                                                        && existingGuest.GuestID == "0"
                                                        select existingGuest;

                                if (findExistingGuest != null && findExistingGuest.Count() > 0)
                                {
                                    findExistingGuest.First().GuestID = guest.GuestID;
                                }
                                else
                                {
                                    model.allGuest.Add(ReservationUtils.MapOwnerGuestToReservationGuest(guest));
                                }
                            }
                        }
                    }

                }
                return View(model);
            }
            else
            { return null;
            }
        }


        private BookReservationResponse ExecuteBook(ReservationParameters reservationContext)
        {
            // bool statusExecuteBook = false;
            BlueGreenContext bgContext = new BlueGreenContext();
            BookReservationRequest request = new BookReservationRequest();
            request.ReservationProjectNumber = reservationContext.ProjectNumber.ToString();

            request.OwnerID = reservationContext.OwnerId;
            request.UnitTypeCode= reservationContext.UnitType;


            request.CheckInDate = reservationContext.CheckInDate.ToString("MM/dd/yyyy");
            request.LengthOfStay = Convert.ToInt16((reservationContext.CheckOutDate - reservationContext.CheckInDate).TotalDays);
            request.NumberOfAdults = int.Parse(reservationContext.Guest_NumberOfGuest);   //TODO get value from context
            request.AccountNumber = bgContext.GetPrimaryAccountNumber();
            request.OwnerProjectNumber = bgContext.GetOwnerAccountProject().ToString();
            
            //Changed to Enclose Special Requests with Special Request for BGO only
            string specialCharacter = Sitecore.Configuration.Settings.GetSetting("IdentifierForSpecialRequestFromBGO");
            request.Comments = (reservationContext.text_SpecialRequests == null) ? "" : specialCharacter + reservationContext.text_SpecialRequests + specialCharacter;

            if (!string.IsNullOrEmpty(reservationContext.wheelchairaccessible) && reservationContext.wheelchairaccessible == "1")
            {
                request.HandicapAccessible = "Y";
                request.Comments = "REQHANDICAP " + request.Comments;
            }

            if (reservationContext.ReservationType == SearchParameters.RESERVATION_TYPE_BONUSTIME)
            {
                request.ReservationType = "B";
                request.SiteName = "BonusTime";
            }
            else
            {
                request.ReservationType = "P";
                request.SiteName = "onlinepoints";
                request.Points = reservationContext.PointsRequired;
            }
            if (reservationContext.hasCreditcard)
            {
                request.Payment = new Models.ResortService.BookReservationRequest.Payment();
                request.Payment.CreditCardAuthorization = reservationContext.CreditCard_VerificationNumber;
                request.Payment.CreditCardName = reservationContext.CreditCard_Name;
                request.Payment.CreditCardNumber = reservationContext.CreditCard_Number;
                request.Payment.CreditCardType = FormatUtils.ConvertCreditCard(reservationContext.CreditCard_Type);
                request.Payment.CreditCardExpirationDate = ReservationUtils.GetExpDate(reservationContext.CreditCard_ExpDateMonth, reservationContext.CreditCard_ExpDateYear);
                request.Payment.CreditCardTotal = Convert.ToDouble( reservationContext.BT_TotalCost);
                request.Payment.NonTaxTotal = Convert.ToDouble(Convert.ToDecimal(reservationContext.DailyPrice) * reservationContext.NumberOfNightChanged);
            }

            if (reservationContext.hasGuest)
            {
                request.Guests = new List<Models.ResortService.BookReservationRequest.Guest>();

                var guestOld = new Models.ResortService.BookReservationRequest.Guest();
                guestOld.GuestType = "Primary";
                guestOld.GuestID = "";
                guestOld.FirstName = reservationContext.Guest_FirstName;
                guestOld.LastName = reservationContext.Guest_LastName;
                guestOld.Relationship = reservationContext.Guest_Relationship;
                request.Guests.Add(guestOld);


                if (!string.IsNullOrEmpty(reservationContext.Guest_Email))
                {
                    guestOld.EmailAddresses = new List<Models.ResortService.BookReservationRequest.EmailAddress>();
                    var email = new Models.ResortService.BookReservationRequest.EmailAddress();
                    email.AddressType = "Home";
                    email.Email = reservationContext.Guest_Email;
                    guestOld.EmailAddresses.Add(email);
                }
                else
                {
                    guestOld.EmailAddresses = new List<Models.ResortService.BookReservationRequest.EmailAddress>();
                    var email = new Models.ResortService.BookReservationRequest.EmailAddress();
                    email.AddressType = "Home";
                    email.Email = bgContext.bxgOwner.Email;
                    guestOld.EmailAddresses.Add(email);
                }

                if (reservationContext.Guest_AddNew != "AddNew")
                {

                    if (!string.IsNullOrEmpty(reservationContext.Guest_PhoneNumber))
                    {
                        guestOld.Phones = new List<Models.ResortService.BookReservationRequest.Phone>();
                        var phone = new Models.ResortService.BookReservationRequest.Phone();
                        phone.PhoneNumberType = "Home";
                        phone.PhoneNumber = reservationContext.Guest_PhoneNumber;
                        guestOld.Phones.Add(phone);

                    }
                    if (!string.IsNullOrEmpty(reservationContext.Guest_City))
                    {
                        guestOld.Addresses = new List<Models.ResortService.BookReservationRequest.Address>();
                        var address = new Models.ResortService.BookReservationRequest.Address();
                        address.AddressLine1 = " ";
                        address.AddressLine2 = " ";
                        address.CountryCode = " ";
                        address.PostalCode = " ";
                        address.AddressType = "Home";
                        address.City = reservationContext.Guest_City;

                        // Here I'm assuming that no one will select state without city (they are both required fields anyway)
                        if (!string.IsNullOrEmpty(reservationContext.Guest_State))
                        {
                            address.ProvinceCode = reservationContext.Guest_State;
                        }

                        guestOld.Addresses.Add(address);
                    }
                }
                else
                {
                    var guest = new Models.ResortService.BookReservationRequest.Guest();
                    guest.GuestType = "GuestOnPrimary";
                    guest.FirstName = reservationContext.Guest_FirstName;
                    guest.LastName = reservationContext.Guest_LastName;
                    guest.Relationship = reservationContext.Guest_Relationship;
                    guest.GuestID = "";
                    if (!string.IsNullOrEmpty(reservationContext.Guest_Email))
                    {
                        guest.EmailAddresses = new List<Models.ResortService.BookReservationRequest.EmailAddress>();
                        var email = new Models.ResortService.BookReservationRequest.EmailAddress();
                        email.AddressType = "Home";

                        email.Email = reservationContext.Guest_Email;
                        guest.EmailAddresses.Add(email);
                    }

                    if (!string.IsNullOrEmpty(reservationContext.Guest_City))
                    {
                        guest.Addresses = new List<Models.ResortService.BookReservationRequest.Address>();
                        var address = new Models.ResortService.BookReservationRequest.Address();
                        address.AddressLine1 = " ";
                        address.AddressLine2 = " ";
                        address.CountryCode = " ";
                        address.PostalCode = " ";
                        address.AddressType = "Home";
                        address.City = reservationContext.Guest_City;

                        // Here I'm assuming that no one will select state without city (they are both required fields anyway)
                        if (!string.IsNullOrEmpty(reservationContext.Guest_State))
                        {
                            address.ProvinceCode = reservationContext.Guest_State;
                        }

                        guest.Addresses.Add(address);
                    }


                    if (!string.IsNullOrEmpty(reservationContext.Guest_PhoneNumber))
                    {
                        guest.Phones = new List<Models.ResortService.BookReservationRequest.Phone>();
                        var phone = new Models.ResortService.BookReservationRequest.Phone();
                        phone.PhoneNumberType = "Home";
                        phone.PhoneNumber = reservationContext.Guest_PhoneNumber;
                        guest.Phones.Add(phone);

                    }

                    request.Guests.Add(guest);
                }
            }
            if (reservationContext.hasAddress)
            {

            }

            if (Session["AgentLoginID"] == null)
            {
                request.Agent = "OWNER";
            }
            else
            {
                request.Agent = (string)Session["AgentLoginID"];
            }

            ResortService resortService = new ResortService();
            var reservationResponse = resortService.BookReservation(request);

            OwnerWebStats(request, reservationResponse);

            return reservationResponse;
        }

        public void OwnerWebStats(BookReservationRequest request,BookReservationResponse reservationResponse)
        {
            try
            {

                OwnerWebStatsRequest ownerRequest = new OwnerWebStatsRequest();
                string cursession = string.Empty;
                string ownerID = string.Empty;
                var searchtabvalue = string.Empty;
                var SearchTab = "Dest";
                BlueGreenContext bgContext = new BlueGreenContext();
                //if (searchtype = "2")
                //{
                //    SearchTab = "Dest";
                //    SearchTabSubVal = Session("SearchCity")
                //    }
                //else if searchtype = "3" Then
                //    SearchTab = "Exp"
                //    SearchTabSubVal = Session("SearchCity")
                //    searchtabvalue = Session("experience")

                //End If
                if (HttpContext.Session != null)
                {
                    cursession = HttpContext.Session.SessionID;
                    ownerID = bgContext.OwnerId;
                }

                ownerRequest.WebSessionID = cursession;
                ownerRequest.SiteID = "7";
                ownerRequest.OwnerID = request.OwnerID.ToString();
                ownerRequest.SearchTab = "";
                ownerRequest.SearchTabSubVal = "";
                ownerRequest.SearchTabValue = "";
                ownerRequest.ResortID = request.ResortID.ToString();
                ownerRequest.BGOUnitType = request.UnitTypeCode;
                ownerRequest.ProjectNumber = request.ReservationProjectNumber;
                ownerRequest.UnitType = request.UnitTypeCode;

                DateTime checkinDate = DateTime.Parse(request.CheckInDate);
                ownerRequest.CheckInDate = checkinDate.ToString("MM/dd/yyyy");
                ownerRequest.CheckOutDate = checkinDate.AddDays( request.LengthOfStay).ToString();

                ownerRequest.NumberOfGuests = request.NumberOfAdults.ToString();
                ownerRequest.SumBal = request.Points.ToString();
                ownerRequest.PPPEligible = "";
                ownerRequest.PPPFee = "";
                ownerRequest.ReservationNumber = reservationResponse.ConfirmReservation.ReservationNumber.ToString();
                ownerRequest.Phase = "3";
                ownerRequest.RecordReturnCode = "";
                ownerRequest.ExtendedResort = "0";
                ownerRequest.ExtendedStay = "0";
                ownerRequest.Handycap = "";
                ownerRequest.QryTime = "";
                ownerRequest.CreatedDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
                ownerRequest.CreatedBy = "Web";


                ownerRequest.UnitType = request.UnitTypeCode;


                ProfileService profileService = new ProfileService();

                profileService.OwnerWebStats(ownerRequest);
            }
            catch(Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Unexpected exception calling OwnerWebStats", ex, "reservationcontroller.OwnerWebStats");

            }
        }

        public void PPPOwnerWebStats(ReservationParameters reservationContext, bool purchasePPP)
        {
            try
            {

                OwnerWebStatsRequest ownerRequest = new OwnerWebStatsRequest();
                string cursession = string.Empty;
                string ownerID = string.Empty;
                var searchtabvalue = string.Empty;
                BlueGreenContext bgContext = new BlueGreenContext();

                if (HttpContext.Session != null)
                {
                    cursession = HttpContext.Session.SessionID;
                    ownerID = bgContext.OwnerId;
                }

                ownerRequest.PPPEligible = purchasePPP ? "1" : "0";
                ownerRequest.PPPFee = reservationContext.PPPCost.ToString();
                ownerRequest.ReservationNumber = Session["ReservationNumber"].ToString();
                ownerRequest.Phase = "3";
                ownerRequest.CreatedBy = "Web";
                ownerRequest.CreatedDate = DateTime.Now.Date.ToString("MM/dd/yyyy");

                ownerRequest.SiteID = "7";
                ownerRequest.WebSessionID = cursession;
                ownerRequest.OwnerID = ownerID;

                ownerRequest.SearchTab = "";
                ownerRequest.SearchTabSubVal = "";
                ownerRequest.SearchTabValue = "";
                ownerRequest.ResortID = "";
                ownerRequest.BGOUnitType = "";
                ownerRequest.ProjectNumber = "";
                ownerRequest.UnitType = "";

                ownerRequest.CheckInDate = "";
                ownerRequest.CheckOutDate = "";

                ownerRequest.NumberOfGuests = "";
                ownerRequest.SumBal = "";
                ownerRequest.RecordReturnCode = "";
                ownerRequest.ExtendedResort = "";
                ownerRequest.ExtendedStay = "";
                ownerRequest.Handycap = "";
                ownerRequest.QryTime = "";


                ProfileService profileService = new ProfileService();

                profileService.OwnerWebStats(ownerRequest);
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Unexpected exception calling PPPOwnerWebStats", ex, "reservationcontroller.PPPOwnerWebStats");

            }
        }
    }
}