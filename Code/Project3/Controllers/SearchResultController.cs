using BGSitecore.Models.ResortService.BGOInventoryAvailability;
using BGSitecore.Components;
using BGSitecore.Models;
using BGSitecore.Models.Resort;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BGSitecore.Models.ResortService.ResortsAvailabilityByDate;
using BGSitecore.Utils;
using BGSitecore.Services;
using BGSitecore.Models.ResortService.ScreeningBookReservationRequest;
using BGSitecore.Models.ResortService.ScreeningBookReservationResponse;
using System.Globalization;
using BGSitecore.Models.ResortService.PointRatesDetailRequest;
using BGSitecore.Models.ResortService.InventoryCalendarByResortRequest;
using BGSitecore.Models.ResortService.ResortsAvailabilityByState;
using BGSitecore.Models.ResortService.InventoryCalendarByResortResponse;

namespace BGSitecore.Controllers
{
    public class SearchResultController : GlassController
    {
        public ActionResult Search()
        {

            var model = GetLayoutItem<SearchResult>();

            //SearchUtils.ValidateSearchOptions(model);
            model.SavedSearches = SearchUtils.GetSavedSearches();


            //TODO move this code to new contol for resort
            var searchParamters = getQuerystringParameters();
            if (!string.IsNullOrEmpty(searchParamters.Destination ) && !searchParamters.Destination.ToLower().Contains("all-"))
            {
                searchParamters.CheckInDate = DateTime.MinValue;
                searchParamters.CheckOutDate = DateTime.MinValue;

                BlueGreenContext bxgContext = new BlueGreenContext();
                model.AllResorts = ResortManager.GetResortIdForUserForDestination(searchParamters.ReservationType, searchParamters.Destination, searchParamters.ReservationType, bxgContext.IsSecondaryMarketUser);

                if (model.AllResorts != null && model.AllResorts.Count > 0)
                {
                    var allTypes = (from m in model.AllResorts
                                    from p in m.Projects
                                    from r in p.Rooms
                                    select r.BluegreenUnitCode);
                    if (allTypes != null)
                    {
                        model.AvailableUnitTypeForDestination = allTypes.Distinct().ToList();
                    }
                }

            }
            else
            {
                model.ShowDateSearchOptions = true;
            }
            return View(model);
        }
       
        public ActionResult SaveSearch()
        {
            var model = GetLayoutItem<SearchResult>();

            var searchParamtersManager = new SearchParametersManager(); // get the default values stored in session
            model.searchParameters = searchParamtersManager.parameter;
            return View(model);
        }

        [HttpPost]
        public ActionResult ReservationValidation(string ResortId, DateTime CheckInDate, DateTime CheckOutDate, string ReservationType, string wheelchairaccessible, string data)
        {
            ReservationParameters reservationContext = (ReservationParameters)SearchUtils.StringToObject(data);

            ReservationVerification response = new ReservationVerification();
            var notEnoughMessage = SearchManager.GetNotEnoughPoints();
            ScreeningBookReservationResponse unitResponse = null;
            response.isValid = false;

            try
            {
                BlueGreenContext bgContext = new BlueGreenContext();

                //if (ReservationType == SearchParameters.RESERVATION_TYPE_POINTS && bgContext.IsSavePoints)
                //{
                //    response.ErrorMessage = notEnoughMessage.SavePointsToContinue;
                //}

                if (string.IsNullOrEmpty(response.ErrorMessage))
                {
                    var resCount = GetReservationsCount(ResortId);
                    if (resCount >= 10)
                    {
                        response.ErrorMessage = notEnoughMessage.ReservationExceedsLimit;


                    }
                    else
                    {
                        var resortService = new ResortService();

                        unitResponse = resortService.ScreeningBookReservation(int.Parse(ResortId), reservationContext.ProjectNumber, reservationContext.UnitType, CheckInDate, CheckOutDate, ReservationType, int.Parse(bgContext.OwnerId), bgContext.GetOwnerAccountProject(), reservationContext.PointsRequired, reservationContext.SeasonCode);

                        if (unitResponse != null && unitResponse.Errors == null)
                        {

                            if (unitResponse.ScreeningBookReservation.BookStatus == BGSitecore.Models.Resort.BookStatusType.CannotBeDone.ToString())
                            {
                                response.CheckInDate = CheckInDate;
                                response.CheckOutDate = CheckOutDate;
                                response.ReservationType = ReservationType;
                                response.showSavePointsMessage = true;
                                if (unitResponse.ScreeningBookReservation.OwnerGoodStand == true && unitResponse.ScreeningBookReservation.OwnerHasPastDueAccount == false && unitResponse.ScreeningBookReservation.OwnerEligible == true)
                                {
                                    response.ErrorMessage = notEnoughMessage.NotEligibleError;
                                    response.TotalPointsRequired = reservationContext.PointsRequired;
                                    response.Rates = GetReservationDailyRates(CheckInDate, CheckOutDate, reservationContext.ProjectNumber, reservationContext.UnitType);
                                    int elgPoints = 0;
                                    response.Accounts = GetOwnerPoints(unitResponse.ScreeningBookReservation.PointsEligibility, CheckInDate, ref elgPoints);
                                    if (elgPoints > 0)
                                    {
                                        response.TotalEligiblePoints = FormatUtils.FormatPoints(elgPoints);
                                    }
                                    else
                                    {
                                        response.TotalEligiblePoints = elgPoints.ToString();
                                    }
                                }
                                else
                                {
                                    response.ErrorMessage = notEnoughMessage.AccoutnStatusError;
                                }


                            }
                            else 
                            {
                                Session["BookingScreeningResponse"] = unitResponse;

                                reservationContext.CheckInDate = CheckInDate;
                                reservationContext.CheckOutDate = CheckOutDate;
                                reservationContext.OwnerId = int.Parse(bgContext.OwnerId);
                                reservationContext.wheelchairaccessible = wheelchairaccessible;
                                reservationContext.ReservationType = ReservationType;
                                reservationContext.ResortId = int.Parse(ResortId);
                                reservationContext.PPPCost = Convert.ToDecimal(unitResponse.ScreeningBookReservation.PPPCost);
                                reservationContext.SPECost = Convert.ToDecimal(unitResponse.ScreeningBookReservation.SPECost);

                                Session["ReservationContext"] = reservationContext;

                                //For sampler24 don't show the Use FuturePoints Message ans simply continue
                                if (unitResponse.ScreeningBookReservation.BookStatus == BGSitecore.Models.Resort.BookStatusType.CanBeBookedwithFuture.ToString() &&
                                     bgContext.GetOwnerType() == BlueGreenContext.OWNER_TYPE_SAMPLER24)
                                {
                                    unitResponse.ScreeningBookReservation.BookStatus = BGSitecore.Models.Resort.BookStatusType.CanBeBooked.ToString();
                                }


                                if (unitResponse.ScreeningBookReservation.BookStatus == BGSitecore.Models.Resort.BookStatusType.CanBeBookedwithFuture.ToString())
                                {
                                    response.showFuturePointMessage = true;
                                    response.isValid = false;

                                }
                                else if (unitResponse.ScreeningBookReservation.BookStatus == BGSitecore.Models.Resort.BookStatusType.CanBeBookedwithFutureSPE.ToString())
                                {
                                    if (bgContext.GetOwnerType() != BlueGreenContext.OWNER_TYPE_SAMPLER24)
                                    {
                                        response.showFuturePointMessage = true;
                                    }
                                    response.showSavePointsMessage = true;
                                    response.ErrorMessage = notEnoughMessage.SavePointsToContinue;

                                    response.isValid = false;
                                }
                                else if (unitResponse.ScreeningBookReservation.BookStatus == BGSitecore.Models.Resort.BookStatusType.CanBeBookedwithSPE.ToString())
                                {
                                    response.showSavePointsMessage = true;
                                    response.ErrorMessage = notEnoughMessage.SavePointsToContinue;
                                    response.isValid = false;
                                }
                                else if (unitResponse.ScreeningBookReservation.BookStatus == BGSitecore.Models.Resort.BookStatusType.CanBeBooked.ToString())
                                {

                                    response.isValid = true;

                                    if (ReservationType == SearchParameters.RESERVATION_TYPE_POINTS)
                                    {
                                        response.RedirectURL = "/owner/confirm-reservation";
                                    }
                                    else if (ReservationType == SearchParameters.RESERVATION_TYPE_BONUSTIME)
                                    {
                                        response.RedirectURL = "/owner/bonus-time-reservation";
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (unitResponse.Errors != null && unitResponse.Errors.Count() > 0 && unitResponse.Errors[0].ShortText.Contains("99,999"))
                            {
                                response.ErrorMessage = unitResponse.Errors[0].ShortText;
                            }
                            else
                            {
                                response.ErrorMessage = notEnoughMessage.TechnicalIssueError;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = notEnoughMessage.ErrorTryAgain;

                Sitecore.Diagnostics.Log.Error("Unexpected exception Calling Reservation Validation", ex);
            }

            return Json(response);
        }


        [HttpPost]
        public ActionResult SaveSearchAjax(string json)
        {
            ActionResult result = Json(new { Response = "Error" });
            try
            {
                SearchService ss = new SearchService();
                SavedSearch item = new SavedSearch(Request.QueryString);
                ss.Save(item);
                result = Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Unexpected exception saving search record", ex);
            }

            return result;
        }

        [HttpPost]
        public ActionResult SaveSearchDelete(string json)
        {
            ActionResult result = Json(new { Response = "Error" });
            try
            {
                string name = Server.HtmlEncode(Request.QueryString["nm"]);
                SearchService ss = new SearchService();
                SavedSearch item = new SavedSearch(name);
                if (ss.Delete(item))
                {
                    result = Json(new { Response = "Success" });
                }
                else
                {
                    result = Json(new { Response = "Error deleting " + name });
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Unexpected exception saving search record", ex);
            }

            return result;
        }


        [HttpPost]
        public ActionResult MonthSearch(string destination, string reservationType, string startMonth, string duration, string searchWindow, string maxSearchWindow)
        {
            bool showSummerMonthError = false;
            var model = new SearchResult();
            model.searchParameters = new SearchParameters();
            model.searchParameters.Destination = destination;
            model.searchParameters.ReservationType = reservationType;
            model.searchParameters.monthsearchduration = duration;
            model.searchParameters.MonthSearch = FormatMonthSearch(startMonth);

           
            var allResortsforQuery = ResortManager.GetResortIdForUserForDestination(model.searchParameters.ReservationType, model.searchParameters.Destination, model.searchParameters.ReservationType, false);
            if (allResortsforQuery != null)
            {
                model.ShowNotAllResortUsed = FilterResortForMonthSearch(allResortsforQuery, model.searchParameters, searchWindow, out showSummerMonthError);
                model.ShowSummerHolidayError = showSummerMonthError;
            }
            int searchWindowInMonths = 1;  //set default value 
            int.TryParse(searchWindow, out searchWindowInMonths);
            int durationInt = 1;
            int.TryParse(duration, out durationInt);

            GetMonthInventory(model, searchWindowInMonths, maxSearchWindow);

            MergeAndFilterResults(model, model.searchParameters, true, false);

            var retValue = new SearchResultMonthList();
            retValue.allResults = new  List<SearchResultMonth>();
            retValue.isSummerMonth = showSummerMonthError;
            retValue.ShowInternalError = model.ShowInternalError;
            if (model != null && model.Inventories != null)
            {
                var allResort = ResortManager.GetAllResortForUser(null);

                var AllSecondaryMarketResort = GetAllResortForSecondaryMarket(model.searchParameters.ReservationType);
                BlueGreenContext bgContext = new BlueGreenContext();

                var userOwnerType = bgContext.GetOwnerType();
                List<SummerMonth> summerMonthList = null;

                if (userOwnerType == BlueGreenContext.OWNER_TYPE_SAMPLER || userOwnerType == BlueGreenContext.OWNER_TYPE_SAMPLER24)
                {
                    summerMonthList = SearchManager.GetSummerRestrictions();
                }

                foreach (var item in model.Inventories)
                {
                    var allowAdd = true;

                    if (userOwnerType == BlueGreenContext.OWNER_TYPE_SAMPLER || userOwnerType == BlueGreenContext.OWNER_TYPE_SAMPLER24)
                    {
                        ResortDetails filterOnResort = allResort.Where(x => x.ResortId.ToString() == item.Resort.ResortID).First();

                        if (filterOnResort != null && !filterOnResort.AllowSamplerSummerBooking)
                        {
                            if (summerMonthList != null)
                            {
                                
                                foreach (SummerMonth summer in summerMonthList)
                                {
                                    //User are allow to checkout on the first day of the summer month
                                    if ((Convert.ToDateTime(item.CheckInDate) >= summer.FromDate.AddDays(-durationInt)
                                        && Convert.ToDateTime(item.CheckInDate) <= summer.ToDate.AddDays(durationInt))
                                         && (Convert.ToDateTime(item.CheckOutDate) >= summer.FromDate.AddDays(-durationInt)
                                        && Convert.ToDateTime(item.CheckOutDate) <= summer.ToDate.AddDays(durationInt)))
                                    {

                                        allowAdd = false;
                                    }
                                }
                            }
                        }
                    }


                    if (bgContext.IsSecondaryMarketUser && model.searchParameters.ReservationType == SearchParameters.RESERVATION_TYPE_BONUSTIME)
                    {
                        int resortId = 0;
                        int.TryParse(item.Resort.ResortID, out resortId);
                        if (!AllSecondaryMarketResort.Contains(resortId))
                        {
                            allowAdd = false;
                        }
                    }
                    if (allowAdd)
                    {
                        var inv = new SearchResultMonth();
                        inv.checkInDate = item.CheckInDate;
                        inv.resortId = item.Resort.ResortID;
                        if (item.WebUnitTypes != null && item.WebUnitTypes.WebUnitTypeCode != null && item.WebUnitTypes.WebUnitTypeCode.Count() > 0)
                        {
                            inv.WebUnitCode = item.WebUnitTypes.WebUnitTypeCode;
                        }
                        else
                        {
                            inv.WebUnitCode = new List<string>();
                        }
                        inv.HandicapAccessible = item.HandicapAccessible;

                        retValue.allResults.Add(inv);
                    }
                }
            }
            return Json(retValue);
        }

        public ActionResult SearchResult()
        {
            DebugUtils.StartLogEvent("SearchResultController.SearchResult");

            int recCount = 0;
            //Clear the old sessions
            ReservationUtils.DeleteContextReservation();
            Session["BookingScreeningResponse"] = null;

            var viewName = "";

            var model = GetLayoutItem<SearchResult>();
			model.SavedSearches = SearchUtils.GetSavedSearches();

			BlueGreenContext bxgContext = new BlueGreenContext();
            model.disableBooking = OwnerUtils.IsAccountPastDue();

            model.isShowAvailabilityColumn = (bxgContext.OwnerId != "430142");  //Remove Availability column for Travelerplus user
            model.ShowInternalError = false;
            model.ShowNotAllResortUsed = false;
            model.ShowSampler2NightStayMessage = false;
            model.ShowCheckinOutsideSearchWindowBT = false;
            model.ShowCheckinOutsideSearchWindowPoints = false;
            model.ShowSamplerHolidayError = false;
            model.ShowSummerHolidayError = false;
            model.ShowCheckinMoreThen48BT = false;
            model.ShowBookingDurationError = false;
            model.ShowBooking1DayBonusTimeMessage = false;
            model.ShowBooking1DayPointsMessage = false;
            model.ShowPanamaCityMessage = false;
			model.ShowDateSearchOptions = false;
			var searchParamters = getQuerystringParameters();
            if (searchParamters.Destination == null)
            {
                Response.Redirect("/home");
            }

            model.searchParameters = searchParamters;
            model.ShowSearchBar = true;
            SearchUtils.ValidateSearchOptions(model);
            if (!model.SearchParametersNotValid)
            {

                if (string.IsNullOrEmpty(searchParamters.ResortId) &&
                    (searchParamters == null || string.IsNullOrEmpty(searchParamters.Destination) || searchParamters.Destination.ToLower().Contains("all-")))
                {
                    model.ShowDateSearchOptions = true;

                    GetAllInventory(model);
                    if (model.resortList != null)
                    {
                        MergeAndFilterResults(model, searchParamters, true, false);
                        recCount = CountRecords(model);
                        model.ShowSearchParameters = true;
                        viewName = "SearchResultAll";
                    }
                    model.ShowSearchBar =true;

                }
                else if (string.IsNullOrEmpty(searchParamters.ResortId) &&
                     (searchParamters == null || string.IsNullOrEmpty(searchParamters.Destination) ||
                     searchParamters.Destination.ToLower().Contains("state-")) &&
                     searchParamters.CheckInDate > DateTime.MinValue)

                {
                    GetStateInventory(model);
                    model.ShowSearchParameters = false;
                    model.ShowSearchBar = false;

                    //We no longer need to display those error then executing this search (it's already display on the initial page load)
                    model.ShowNotAllResortUsed = false;
                    model.ShowSampler2NightStayMessage = false;
                    model.ShowCheckinOutsideSearchWindowBT = false;
                    model.ShowCheckinOutsideSearchWindowPoints = false;
                    model.ShowSamplerHolidayError = false;
                    model.ShowSummerHolidayError = false;
                    model.ShowCheckinMoreThen48BT = false;
                    model.ShowBookingDurationError = false;
                    model.ShowBooking1DayBonusTimeMessage = false;
                    model.ShowBooking1DayPointsMessage = false;
                    model.ShowPanamaCityMessage = false;
                    if (model.resortList != null)
                    {
                        MergeAndFilterResults(model, searchParamters, true, false);
                        recCount = CountRecords(model);
                        viewName = "SearchResultState";
                    }
                    else
                    {
                        model.ShowInternalError = true;
                    }
                }
                else
                {
                    if (searchParamters.CheckInDate > DateTime.MinValue)
                    {
                        if (string.IsNullOrEmpty(searchParamters.ResortId))
                        {
                            var allResortsforQuery = ResortManager.GetResortIdForUserForDestination(searchParamters.ReservationType, searchParamters.Destination, searchParamters.ReservationType, bxgContext.IsSecondaryMarketUser);
                            if (allResortsforQuery != null)
                            {
                                model.ShowNotAllResortUsed = FilterResortIdForQuery(allResortsforQuery, searchParamters, model.ShowSummerHolidayError);

                            }
                        }

                        model.searchParameters = searchParamters;
                        //No need to run the search if no resort match the search parameters
                        if (!string.IsNullOrEmpty(searchParamters.ResortId))
                        {
                            GetResortData(model);
                            model.ShowSearchBar = false;

                            if (model.resortList != null)
                            {
                                MergeAndFilterResults(model, searchParamters, true, true);
                            }
                            //We no longer need to display those error then executing this search (it's already display on the initial page load)
                            model.ShowNotAllResortUsed = false;
                            model.ShowSampler2NightStayMessage = false;
                            model.ShowCheckinOutsideSearchWindowBT = false;
                            model.ShowCheckinOutsideSearchWindowPoints = false;
                            model.ShowSamplerHolidayError = false;
                            model.ShowSummerHolidayError = false;
                            model.ShowCheckinMoreThen48BT = false;
                            model.ShowBookingDurationError = false;
                            model.ShowBooking1DayBonusTimeMessage = false;
                            model.ShowBooking1DayPointsMessage = false;
                            model.ShowPanamaCityMessage = false;
                            recCount = CountRecords(model);

                            viewName = "SearchResultResort";
                        }
                        else
                        {

                            viewName = "SearchResult";

                        }
                    }
                    else
                    {
                        var allResortsforQuery = ResortManager.GetResortIdForUserForDestination(model.searchParameters.ReservationType, model.searchParameters.Destination, model.searchParameters.ReservationType, false);
                        if (allResortsforQuery != null)
                        {
                            bool showSummerMonthError = false;
                            model.ShowNotAllResortUsed = FilterResortForMonthSearch(allResortsforQuery, model.searchParameters, "1", out showSummerMonthError);
                        }
                       

                    }
                }


				ReservationUtils.OwnerWebStats(searchParamters, recCount);
            }
            if (model.ShowInternalError || model.SearchParametersNotValid)
            {
                model.ShowDateSearchOptions = true;

                viewName = "NoResults";
            }
            DebugUtils.EndLogEvent("SearchResultController.SearchResult");

            return View(viewName, model);
        }

        /// <summary>
        /// Merges the SiteCore Resort data to the result set.  Filters the Search result by Resort Min/Max stay and available dates.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private void MergeAndFilterResults(SearchResult model, SearchParameters searchParamters, bool applyFilter, bool addProject)
        {
           
            if (model.resortList != null)
            {
                MergeInventoryWithSitecoreResort(model, model.AllResorts, addProject, searchParamters.ReservationType, searchParamters.Duration);
                FilterResultUsingParameters(model);
           
            }
          
        }


        private bool FilterResultUsingParameters(SearchResult model)
        {
            var originalItemCount = model.resortList.Count();

            model.resortList =
                        (from res in model.resortList
                         where model.searchParameters.Duration >= res.MinNightStay && model.searchParameters.Duration <= res.MaxNightStay
                         select res).ToList();

            model.resortList = (from res in model.resortList
                                where model.searchParameters.CheckInDate <= DateTime.Now.AddDays(res.AdvanceSearchWindow)
                                select res).ToList();

            return (model.resortList.Count() != originalItemCount);
        }


        private bool FilterResortIdForQuery(List<ResortDetails> resortList, SearchParameters searchParamters, bool ShowSummerHolidayError)
        {
            var resortIds = "";
            var showError = false;
            //sampler users are not allow to book most of the resort during summer time.  FIlter the result
            if (ShowSummerHolidayError)
            {
                if (resortList != null && resortList.Count() > 0)
                {
                    resortList = resortList.Where(x => x.AllowSamplerSummerBooking).ToList();
                }
            }

            if (resortList != null && resortList.Count() > 0)
            {


                IEnumerable<int> filteringQuery = new List<int>();
                if (searchParamters.Duration != 7)
                {
                    var fullWeekCount =
                        (from res in resortList
                         where res.MaximumNightStayPoints >= 7 && res.MaximumNightStayPoints <= 7
                         select res.ResortId).Count();
                    if (fullWeekCount == resortList.Count())
                    {
                        //Update query to search for 7 days
                        searchParamters.CheckOutDate = searchParamters.CheckInDate.AddDays(7);
                        showError = true;
                    }
                }


                if (searchParamters.ReservationType == SearchParameters.RESERVATION_TYPE_POINTS)
                {
                    filteringQuery =
                        from res in resortList
                        where res.MinimumNightStayPoints <= searchParamters.Duration && res.MaximumNightStayPoints >= searchParamters.Duration
                        select res.ResortId;
                }
                else
                {
                    filteringQuery =
                       from res in resortList
                       where res.MinimumNightStayBonusTime <= searchParamters.Duration && res.MaximumNightStayBonusTime >= searchParamters.Duration
                       select res.ResortId;
                }
                if (filteringQuery != null && filteringQuery.Count() > 0)
                {
                    resortIds = string.Join(", ", filteringQuery);
                }
                searchParamters.ResortId = resortIds;
            }
            return showError;
        }

        private void MergeInventoryWithSitecoreResort(SearchResult inventory, IEnumerable<ResortDetails> AllResorts, bool addProject, string reservationType, double duration)
        {
            var allResort = inventory.resortList.ToList();
            foreach (SearchResultResortList item in allResort.Reverse<SearchResultResortList>())
            {
                ResortDetails foundItem = AllResorts.FirstOrDefault(m => m.ResortId == item.ResortId);
                if (foundItem != null)
                {
                    item.ResortName = foundItem.ResortName;
                    item.AddressLine1 = foundItem.AddressLine1;
                    item.AddressLine2 = foundItem.AddressLine2;
                    item.City = foundItem.City;
                    item.PhoneNumber = foundItem.PhoneNumber;
                    item.ClubAffiliation = foundItem.ClubAffiliation;
                    item.ZipCode = foundItem.ZipCode;
                    item.State = foundItem.State;
                    item.ResortLink = foundItem.Url;
                    item.ResortImage = foundItem.MainResortImage.ImageFullUrl();
                    if (reservationType == SearchParameters.RESERVATION_TYPE_POINTS)
                    {
                        item.MinNightStay = foundItem.MinimumNightStayPoints;
                        item.MaxNightStay = foundItem.MaximumNightStayPoints;
                        item.AdvanceSearchWindow = (DateTime.Now.AddMonths(foundItem.AdvanceSearchWindowPoints) - DateTime.Now).Days;
                    }
                    else if (reservationType == SearchParameters.RESERVATION_TYPE_BONUSTIME)
                    {
                        item.MinNightStay = foundItem.MinimumNightStayBonusTime;
                        item.MaxNightStay = foundItem.MaximumNightStayBonusTime;
                        item.AdvanceSearchWindow = foundItem.AdvanceSearchWindowBonusTime;

                    }
                    if (addProject)
                    {
                        var tmp = foundItem.Projects.Count();
                        if (item.availableRoom != null)
                        {
                            foreach (SearchResultResortRoom room in item.availableRoom.Reverse<SearchResultResortRoom>())
                            {
                                Project ProjectTmp = foundItem.Projects.FirstOrDefault(m => m.ProjectId == room.ProjectNumber);
                                if (ProjectTmp != null)
                                {
                                    Room RoomTmp = ProjectTmp.Rooms.FirstOrDefault(m => m.UnitType == room.UnitType);
                                    if (RoomTmp != null)
                                    {
                                        room.RoomTitle = RoomTmp.ViewTitle;
                                        room.RoomDescription = RoomTmp.RoomDescription;
                                        room.UnitSize = RoomTmp.BluegreenUnitCode;
                                    }
                                }
                                else
                                {
                                    item.availableRoom.Remove(room);  //if the room is not in sitecore remove it
                                }
                            }
                        }
                    }
                }
                else
                {
                    allResort.Remove(item);
                    //item.ResortName = "missing (" + item.ResortId.ToString() + ")";

                }

            }

            inventory.resortList = allResort;
        }


        private void GetAllInventory(SearchResult searchresult)
        {
            DebugUtils.StartLogEvent("SearchResultController.GetAllInventory");
            var bgContext = new BlueGreenContext();

			ResortsAvailabilityByDateRequest request = new ResortsAvailabilityByDateRequest();
            if (searchresult.searchParameters.ReservationType == SearchParameters.RESERVATION_TYPE_POINTS)
            {
                request.SiteName = "OnlinePoints";
                request.ReservationType = "P";
                request.OwnerType = bgContext.ComputeOwnerType();

            }
            else
            {
                request.SiteName = "BonusTime";
                request.ReservationType = "B";
                request.OwnerType = bgContext.ComputeOwnerType();

            }

            request.CheckInDate = searchresult.searchParameters.CheckInDate.ToString("MM/dd/yy");
            request.LengthOfStay = (searchresult.searchParameters.CheckOutDate - searchresult.searchParameters.CheckInDate).TotalDays.ToString();

        //    request.HandicapAccessible = (searchresult.searchParameters.WheelchairAccessible ? "1" : "0");
            request.WebUnitType = "ANY";
            request.Accomodates = "2";          //TODO review what this value should be
            request.ReservationSource = "";

            request.FullWeekResort = request.LengthOfStay == "7" ? "1" : "0";
            request.IsPrimium = "0";
            request.UnitsCount = "10";
            request.SearchWindow = "1";
            request.Return1UnitPerUnitType = "1";

            var service = new BGSitecore.Services.ResortService();
            var result = service.ResortsAvailability(request);
            var searchResult = new SearchResult();


            if (result == null)
            {
                searchresult.ShowInternalError = true;
                searchResult.RawServiceError = "Service yielded a null result ";
                Sitecore.Diagnostics.Log.Error("Service yielded a null result ", "");  //TODO review how to log service error
            }
            else if (result.Errors != null && result.Errors.Count() > 0)
            {
                searchresult.ShowInternalError = true;
                searchResult.RawServiceError = result.Errors.First().ShortText;
                Sitecore.Diagnostics.Log.Error("Error calling ResortsAvailability: ", result.Errors.First().ShortText);  //TODO review how to log service error
            }
            else
            {
                var resortList = new List<SearchResultResortList>();

                if (result.Inventories != null && result.Inventories.Count > 0)
                {
                    List<ResortDetails> filterOnResort = null;

                    if (bgContext.IsSecondaryMarketUser && searchresult.searchParameters.ReservationType == SearchParameters.RESERVATION_TYPE_BONUSTIME)
                    {
                        var allAccounts = bgContext.bxgOwner.Accounts;
                        filterOnResort = new List<ResortDetails>();
                        foreach (var item in allAccounts)
                        {
                            var ResortDetails = ResortManager.GetResortByProject(FormatUtils.ConvertStringToInt(item.ProjectHome));
                            if (ResortDetails != null)
                            {
                                filterOnResort.Add(ResortDetails);
                            }
                        }
                                               
                    }


                    //sampler users are not allow to book most of the resort during summer time.  FIlter the result
                    if (searchresult.ShowSummerHolidayError)
                    {
                        if (filterOnResort == null)
                        {
                            filterOnResort = ResortManager.GetAllResortForUser(searchresult.searchParameters.ReservationType).Where(x => x.AllowSamplerSummerBooking).ToList();
                        }
                        else
                        {
                            filterOnResort = filterOnResort.Where(x => x.AllowSamplerSummerBooking).ToList();
                        }
                    }

                    foreach (Models.ResortService.ResortsAvailabilityByDate.Inventory item in result.Inventories)
                    {
                        bool addResortToResult = false;
                        var intResortId = Convert.ToInt32(item.Resort.ResortID);

                        if (filterOnResort == null)
                        {
                            addResortToResult = true; //We did a All Destination search add all resort
                        }
                        else
                        {
                            var countResort = (from resort in filterOnResort
                                               where resort.ResortId == intResortId
                                               select resort).ToList();
                            if (countResort != null && countResort.Count > 0)
                            {
                                addResortToResult = true;
                            }
                        }
                        if (addResortToResult)
                        {
                            var alreadyadded = true;
                            var tmp = resortList.Find(x => x.ResortId == intResortId);
                            if (tmp == null)
                            {
                                tmp = new SearchResultResortList();
                                alreadyadded = false;
                            }
                            
                            tmp.ResortId = int.Parse(item.Resort.ResortID);

                            if (item.Resort.Addresses != null && item.Resort.Addresses.Count() > 0)
                            {
                                tmp.StateCode = item.Resort.Addresses[0].StateCode;
                            }
                            if (item.WebUnitTypes != null && item.WebUnitTypes.WebUnitTypeCode != null)
                            {
                                var webUnitTypes = new List<string>();
                                foreach (string webUnitCode in item.WebUnitTypes.WebUnitTypeCode)
                                {
                                    webUnitTypes.Add(webUnitCode);
                                }
                                if (item.HandicapAccessible == "0")
                                {
                                    tmp.WebUnitTypesNonHandicap = webUnitTypes;
                                }
                                else 
                                {
                                    tmp.WebUnitTypesHandicap = webUnitTypes;
                                }
                            }
                            if (!alreadyadded)
                            {
                                resortList.Add(tmp);
                            }
                        }
                    }
                    resortList = resortList.OrderBy(m => m.State).ToList();
                }
                searchresult.resortList = resortList;
            }
            DebugUtils.EndLogEvent("SearchResultController.GetAllInventory");

        }

        private void GetStateInventory(SearchResult searchresult)
        {
            DebugUtils.StartLogEvent("SearchResultController.GetStateInventory");
            var bgContext = new BlueGreenContext();

            ResortsAvailabilityByStateRequest request = new ResortsAvailabilityByStateRequest();
            if (searchresult.searchParameters.ReservationType == SearchParameters.RESERVATION_TYPE_POINTS)
            {
                request.SiteName = "OnlinePoints";
                request.ReservationType = "P";
                request.OwnerType = bgContext.ComputeOwnerType();
            }
            else
            {
                request.SiteName = "BonusTime";
                request.ReservationType = "B";
                request.OwnerType = bgContext.ComputeOwnerType();
            }


            var allResortsforQuery = ResortManager.GetResortIdForUserForDestination(searchresult.searchParameters.ReservationType, searchresult.searchParameters.Destination, searchresult.searchParameters.ReservationType, bgContext.IsSecondaryMarketUser);
            if (allResortsforQuery != null)
            {
                searchresult.ShowNotAllResortUsed = FilterResortIdForQuery(allResortsforQuery, searchresult.searchParameters, searchresult.ShowSummerHolidayError);
            }


            searchresult.searchParameters.ResortId = "";



            request.State = searchresult.searchParameters.Destination.Replace("state-", "");
            request.CheckInDate = searchresult.searchParameters.CheckInDate.ToString("MM/dd/yy");

            //DateTime tmpDate = searchresult.searchParameters.MonthSearch;

            //DateTime checkIn = new DateTime(tmpDate.Year, tmpDate.Month, 1);
            //checkIn = checkIn.AddDays(-(int)checkIn.DayOfWeek);

            //request.CheckInDate = checkIn.ToString("MM/dd/yy");

            request.LengthOfStay = searchresult.searchParameters.monthsearchduration;


            //  request.Handicapped = (searchresult.searchParameters.WheelchairAccessible ? "1" : "0");
            request.WebUnitType = "ANY";
            request.Accomodates = "2";          //TODO review what this value should be
            request.ReservationSource = "";

            request.FullWeekResort = request.LengthOfStay == "7" ? "1" : "0";
            request.IsPrimium = "0";
            request.UnitsCount = "10";
            request.SearchWindow = "1";
//            request.SearchWindow = string.Format("{0}", searchWindow);

            request.Return1UnitPerUnitType = "1";

            var service = new BGSitecore.Services.ResortService();
            var result = service.ResortsAvailabilityByState(request);


            if (result == null)
            {
                searchresult.ShowInternalError = true;
                searchresult.RawServiceError = "Service yielded a null result ";
                Sitecore.Diagnostics.Log.Error("Service yielded a null result ", "");  //TODO review how to log service error
            }
            else if (result.Errors != null && result.Errors.Count() > 0)
            {
                // searchresult.ShowInternalError = true;
                //searchresult.RawServiceError = result.Errors.First().ShortText;
                //searchresult.ShowInternalError = true;
                //Sitecore.Diagnostics.Log.Error("Error calling ResortsAvailability: ", result.Errors.First().ShortText);  //TODO review how to log service error
            }
            else
            {
                var resortList = new List<SearchResultResortList>();

                if (result.ResortAvails != null && result.ResortAvails.Count > 0)
                {
                    List<int> homeResortId = new List<int>();
                    if (bgContext.IsSecondaryMarketUser && searchresult.searchParameters.ReservationType == SearchParameters.RESERVATION_TYPE_BONUSTIME)
                    {
                        if (bgContext.bxgOwner.Accounts != null && bgContext.bxgOwner.Accounts.Count() > 0)
                        {
                            foreach (var account in bgContext.bxgOwner.Accounts)
                            {
                                var homeProjectId = account.ProjectHome;
                                if (!string.IsNullOrEmpty(homeProjectId))
                                {
                                    var resort = ResortManager.GetResortByProject(homeProjectId);
                                    if (resort != null)
                                    {
                                        homeResortId.Add(resort.ResortId);
                                    }
                                }
                            }
                        }

                       
                    }

                    foreach (ResortAvail item in result.ResortAvails)
                    {
                        bool allowAdd = true;
                        //sampler users are not allow to book most of the resort during summer time.  FIlter the result
                        if (searchresult.ShowSummerHolidayError)
                        {
                            var resortDetails = ResortManager.FindResort(item.Resort.ResortID);
                            if (resortDetails != null && !resortDetails.AllowSamplerSummerBooking)
                            {
                                allowAdd = false;
                            }
                        }
                        if (bgContext.IsSecondaryMarketUser && searchresult.searchParameters.ReservationType == SearchParameters.RESERVATION_TYPE_BONUSTIME)
                        {
                            if (!homeResortId.Contains(item.Resort.ResortID))
                            {
                                allowAdd = false;
                            }
                        }

                        if (allowAdd)
                        {
                            if (!resortList.Any(x => x.ResortId == item.Resort.ResortID))
                            {
                                var tmp = new SearchResultResortList();
                                tmp.ResortId = item.Resort.ResortID;
                                if (item.Resort.Addresess != null && item.Resort.Addresess.Count() > 0)
                                {
                                    tmp.StateCode = item.Resort.Addresess[0].StateCode;
                                }
                                if (item.WebUnitTypes != null && item.WebUnitTypes.WebUnitTypeCode != null)
                                {
                                    if (item.HandicapAccessible == "1")
                                    {
                                        tmp.WebUnitTypesHandicap = new List<string>();
                                        foreach (char webUnitCode in item.WebUnitTypes.WebUnitTypeCode)
                                        {
                                            tmp.WebUnitTypesHandicap.Add(webUnitCode.ToString());
                                        }
                                    }
                                    else
                                    {
                                        tmp.WebUnitTypesNonHandicap = new List<string>();
                                        foreach (char webUnitCode in item.WebUnitTypes.WebUnitTypeCode)
                                        {
                                            tmp.WebUnitTypesNonHandicap.Add(webUnitCode.ToString());
                                        }
                                    }
                                }
                                resortList.Add(tmp);
                            }
                            else
                            {
                                SearchResultResortList tmp = resortList.Where(x => x.ResortId == item.Resort.ResortID).First();
                                if (item.WebUnitTypes != null && item.WebUnitTypes.WebUnitTypeCode != null)
                                {
                                    if (item.HandicapAccessible == "1")
                                    {
                                       if (tmp.WebUnitTypesHandicap == null)
                                        {
                                            tmp.WebUnitTypesHandicap = new List<string>();
                                        }
                                       // foreach (char webUnitCode in item.WebUnitTypes.WebUnitTypeCode)
                                       // {

                                            tmp.WebUnitTypesHandicap.Add(item.WebUnitTypes.WebUnitTypeCode.ToString());
                                       // }
                                    }
                                    else
                                    {
                                        if (tmp.WebUnitTypesNonHandicap == null)
                                        {
                                            tmp.WebUnitTypesNonHandicap = new List<string>();
                                        }

                                        //foreach (char webUnitCode in item.WebUnitTypes.WebUnitTypeCode)
                                        // {
                                        tmp.WebUnitTypesNonHandicap.Add(item.WebUnitTypes.WebUnitTypeCode.ToString());
                                       // }
                                    }
                                }
                            }
                        }
                    }
                    resortList = resortList.OrderBy(m => m.State).ToList();
                }
                searchresult.resortList = resortList;
            }
            DebugUtils.StartLogEvent("SearchResultController.GetStateInventory");

        }

        private int GetSearchWindowsforBonusTime(string resortId) 
        {
            int searchWindow = 93;
            if (!string.IsNullOrEmpty( resortId))
            {
                var splitResortId = resortId.Split(',');
                foreach (string item in splitResortId)
                {
                    var resort = ResortManager.FindResort(FormatUtils.ConvertStringToInt(item));
                    if (resort.AdvanceSearchWindowBonusTime < searchWindow)
                    {
                        searchWindow = resort.AdvanceSearchWindowBonusTime;
                    } 

                }
            }

            return searchWindow;
        }

        private bool verifyResortsForSampler(string resortId)
        {
            var retValue = false;
            var arrayResortId = resortId.Split(',');
            foreach (var id in arrayResortId)
            {
                var resort = ResortManager.FindResort(FormatUtils.ConvertStringToInt(id));
                if (resort.AllowSamplerSummerBooking)
                {
                    retValue = true;
                }
            }

            return retValue;

        }

        private void GetMonthInventory(SearchResult searchResult, int searchWindowInMonths, string maxSearchWindow)
        {
            DebugUtils.StartLogEvent("SearchResultController.GetMonthInventory");
            var bgContext = new BlueGreenContext();

            bool byPoints = (searchResult.searchParameters.ReservationType == SearchParameters.RESERVATION_TYPE_POINTS);
            string ownerType = "V";
           
            DateTime tmpDate = searchResult.searchParameters.MonthSearch;
   
            DateTime checkIn = new DateTime(tmpDate.Year, tmpDate.Month, 1);
          
            DateTime lastDate = checkIn.AddMonths(searchWindowInMonths);

            if (checkIn < DateTime.Now)
            {
                checkIn = DateTime.Now.Date.AddDays(1);
            }

            int searchWindow = Convert.ToInt16((lastDate - checkIn).TotalDays);

            int advanceSearchWindow = 93;
            if (byPoints)
            {
                //when using point use the value from the config (number of months)
                 advanceSearchWindow = SearchUtils.GetMaxSearchWindowForPoints();
            }
            else
            {
                //when using Bonus points use the value from the UI,  this will depend on the resort selected (48 or 93)
                advanceSearchWindow = int.Parse(maxSearchWindow);
                //advanceSearchWindow = Int32.Parse(Sitecore.Configuration.Settings.GetSetting("MaxSearchWindowForBonusTime"));
            }

            if (lastDate > DateTime.Now.AddDays(advanceSearchWindow))
            {
                DateTime lastDateTmp = DateTime.Now.AddDays(advanceSearchWindow + 1);

                searchWindow = Convert.ToInt16((lastDateTmp.Date - checkIn.Date).Days);
            }
            string resortId = searchResult.searchParameters.ResortId;

            var service = new BGSitecore.Services.ResortService();


            InventoryCalendarByResortRequest request = new InventoryCalendarByResortRequest();
            request.SiteName = (byPoints) ? "OnlinePoints" : "BonusTime";
            request.ReservationType = (byPoints) ? "P" : "B";
            request.OwnerType = ownerType;

           
            request.CheckInDate = checkIn.ToString("MM/dd/yy");
            request.LengthOfStay = searchResult.searchParameters.monthsearchduration;
            request.ResortID = resortId;

            request.HandicapAccessible = "";
            request.WebUnitType = "ANY";
            request.Accomodates = "2";
            request.ReservationSource = "";

            request.FullWeekResort = request.LengthOfStay == "7" ? "1" : "0";
            request.IsPrimium = "0";
            request.UnitsCount = "10";

            request.SearchWindow = string.Format("{0}", searchWindow);

            request.Return1UnitPerUnitType = "0";
            InventoryCalendarByResortResponse result = null;
            searchResult.ShowInternalError = false;

            try
            {
                result = service.InventoryCalendarByResort(request);
            }
            catch(Exception ex)
            {
                searchResult.ShowInternalError = true;
            }
            if (result != null)
            {
                if (result.Errors != null && result.Errors.Count() > 0)
                {
                    //   searchresult.ShowInternalError = true;
                    searchResult.RawServiceError = result.Errors.First().ShortText;
                    Sitecore.Diagnostics.Log.Error("Error calling GetMonthInventory: ", result.Errors.First().ShortText);  //TODO review how to log service error
                }
                else
                {
                    var userOwnerType = bgContext.GetOwnerType();
                          if (userOwnerType == BlueGreenContext.OWNER_TYPE_SAMPLER || userOwnerType == BlueGreenContext.OWNER_TYPE_SAMPLER24)
                    {
                        var holidayList = SearchManager.GetHoliday();
                        if (holidayList != null)
                        {
                            int duration = int.Parse(searchResult.searchParameters.monthsearchduration);
                            foreach (Holiday holiday in holidayList)
                            {
                                result.Inventories = result.Inventories.Where(x => (Convert.ToDateTime(x.CheckInDate) < holiday.HolidayDate.AddDays(-duration)
                                               || Convert.ToDateTime(x.CheckInDate) > holiday.HolidayDate.AddDays(1))
                                               ).ToList();
                            }
                        }
                    }
                   

                    searchResult.Inventories = result.Inventories;

                }
            }
            DebugUtils.EndLogEvent("SearchResultController.GetMonthInventory");
        }

        private void GetResortData(SearchResult searchResult)
        {
            DebugUtils.StartLogEvent("SearchResultController.GetResortData");
            var bgContext = new BlueGreenContext();
            var service = new BGSitecore.Services.ResortService();
            var useUnitsCount = "10";
            var request = new BGOInventoryAvailabilityRequest();
            var inventory = new InventoryAvailability1();

            inventory.ResortID = searchResult.searchParameters.ResortId;
            inventory.OwnerType = bgContext.ComputeOwnerType();

            if (searchResult.searchParameters.ReservationType == SearchParameters.RESERVATION_TYPE_POINTS)
            {
                inventory.SiteName = "OnlinePoints";
                inventory.ReservationType = "P";
            }
            else
            {
                inventory.SiteName = "BonusTime";
                inventory.ReservationType = "B";
            }

            inventory.CheckInDate = string.Format("{0}{1:00}{2:00} 000000.000", searchResult.searchParameters.CheckInDate.Year, searchResult.searchParameters.CheckInDate.Month, searchResult.searchParameters.CheckInDate.Day); //"20170201 000000.000";
            inventory.LengthOfStay = (searchResult.searchParameters.CheckOutDate - searchResult.searchParameters.CheckInDate).TotalDays.ToString();

            inventory.HandicapAccessible = "0";  //TODO this parameter should be remove to get all inventory (service must be updated)

            inventory.Accomodates = "2";  //This field is no longer used. hard code the value to 2
            inventory.UnitsCount = useUnitsCount;
            inventory.WebUnitType = "ANY";
            inventory.Return1UnitPerUnitType = "1";
            inventory.Segments = "";
            inventory.SearchWindow = "1";

            if (searchResult.searchParameters.ResortId != null && searchResult.searchParameters.ResortId != "")
            {
                string[] ResortIds = searchResult.searchParameters.ResortId.Split(',');
                int FirstResortId = 0;
                FirstResortId = ((ResortIds.Length > 0) ? int.Parse(ResortIds[0]) : int.Parse(searchResult.searchParameters.ResortId));
                var resortDetailsforFullWeek = ResortManager.FindResort(FirstResortId);
                inventory.FullWeekResort = resortDetailsforFullWeek.FullWeekResort == true ? "1" : "0";
            }
            else
            {
                inventory.FullWeekResort = "0";
            }

            inventory.UnitType = "";
            inventory.ReservationSource = "";
            inventory.IsPrimium = "";


            request.InventoryAvailability = new List<InventoryAvailability1>();
            request.InventoryAvailability.Add(inventory);


            var result = service.InventoryAvailability(request);
            if (result == null)
            {
                //TODO not sure how to handle NULL value from service call
                //  searchResult.ShowInternalError = true;
                //   Sitecore.Diagnostics.Log.Error("Error calling ResortsAvailabilityByDate, result is null", "");  //TODO review how to log service error

            }
            else if (result.Errors != null && result.Errors.Count > 0)
            {
                if (!result.Errors.First().ShortText.Contains("No records found"))
                {
                    //searchResult.ShowInternalError = true;
                    // searchResult.RawServiceError = result.Errors.First().ShortText;
                    Sitecore.Diagnostics.Log.Error("Error calling ResortsAvailabilityByDate: ", result.Errors.First().ShortText);  //TODO review how to log service error
                    searchResult.InternalErrorMessage = result.Errors.First().ShortText;
                    searchResult.ShowInternalError = true;
                }
            }
            else
            {
                List<SearchResultResortList> resortList = new List<SearchResultResortList>();


                if (result != null && result.InventoryAvailability != null)
                {
                    ResortDetails resortDetails = null;

                    foreach (Models.ResortService.BGOInventoryAvailability.InventoryAvailability item in result.InventoryAvailability)
                    {
                        var intResortId = FormatUtils.ConvertStringToInt(item.ResortInfo.ResortID);

                        SearchResultResortRoom room = new SearchResultResortRoom();
                        ReservationParameters reservationParameters = new ReservationParameters();

                        if (item.BTRatesItem != null)
                        {
                            room.BTRateItem = new Models.Resort.BTRatesItem();
                            room.BTRateItem.BTRatesItemID = item.BTRatesItem.BTRatesItemID;
                            room.BTRateItem.DailyRate = item.BTRatesItem.DailyRate;
                            room.BTRateItem.TaxRate = item.BTRatesItem.TaxRate;
                            room.BTRateItem.TotalPrice = double.Parse(item.BTRatesItem.TotalPrice);


                        }
                        if (item.RateSummary != null)
                        {
                            room.SeasonCode = item.RateSummary.SeasonName;
                            room.PointsRate = int.Parse(item.RateSummary.PointsRate);
                            room.DailyRates = BuildDailyRates(item);
                        }
                        if (item.Inventory != null)
                        {
                            room.UnitType = item.Inventory.UnitType;
                            room.ProjectNumber = int.Parse(item.Inventory.ProjectNumber);

                            room.RoomTitle = "(+)" + item.Inventory.ViewDescription;
                            room.RoomDescription = "(+)" + item.Inventory.RoomDescription;
                            room.Accomodates = int.Parse(item.Inventory.Accomodates);
                            int totalUnits = 0;
                            int.TryParse(item.Inventory.TotalUnits, out totalUnits);

                            if (totalUnits > 10)
                            {
                                room.TotalUnits = "10+";

                            }
                            else
                            {
                                room.TotalUnits = item.Inventory.TotalUnits + ((item.Inventory.TotalUnits == useUnitsCount) ? "+" : "");
                            }
                            room.HandicapAccessible = item.Inventory.HandicapAccessible;
                        }


                        //Build object to to store in the UI 

                        reservationParameters.ProjectNumber = room.ProjectNumber;
                        reservationParameters.UnitType = room.UnitType;
                        reservationParameters.PointsRequired = room.PointsRate;
                        reservationParameters.Duration = Int32.Parse(inventory.LengthOfStay);
                        reservationParameters.wheelchairaccessible = item.Inventory.HandicapAccessible;
                        if (room.BTRateItem != null)
                        {
                            reservationParameters.BT_TotalCost = room.BTRateItem.TotalPrice;
                            reservationParameters.NumberOfNightChanged = reservationParameters.Duration;
                            if (resortDetails == null || resortDetails.ResortId != intResortId)
                            {
                                resortDetails = ResortManager.FindResort(intResortId);
                            }
                            //Logic to verify if we charge 2 night for 1 night stay
                            if ((inventory.ReservationType == "B" || inventory.ReservationType == "2") && reservationParameters.Duration == 1)
                            {
                                if (resortDetails.TwoNightChargeForOneNightStayBT)
                                {
                                    reservationParameters.BT_TotalCost = room.BTRateItem.TotalPrice * 2;
                                    reservationParameters.NumberOfNightChanged = 2;
                                }
                            }
                            //Add the tax

                            reservationParameters.BT_TotalCost += CalculateTax(resortDetails, reservationParameters.BT_TotalCost);
                            reservationParameters.DailyPrice = room.BTRateItem.DailyRate;
                            reservationParameters.TaxRate = CalculateTax(resortDetails, Convert.ToDouble(room.BTRateItem.DailyRate)).ToString();
                            room.BTRateItem.TotalPrice = reservationParameters.BT_TotalCost;
                        }
                        reservationParameters.SeasonCode = room.SeasonCode;
                        reservationParameters.MaxOccupancy = room.Accomodates;

                        room.EncodedData = SearchUtils.ObjectToString(reservationParameters);
                        if (item.ResortInfo != null)
                        {

                            var resortAlreadyAdded = resortList.Where(i => i.ResortId == intResortId);
                            if (resortAlreadyAdded != null && resortAlreadyAdded.Count() > 0)
                            {
                                resortAlreadyAdded.First().availableRoom.Add(room);
                            }
                            else
                            {
                                var tmp = new SearchResultResortList();
                                tmp.ResortId = intResortId;
                                tmp.availableRoom = new List<SearchResultResortRoom>();
                                tmp.availableRoom.Add(room);

                                resortList.Add(tmp);
                            }
                        }
                    }
                }
                searchResult.resortList = resortList.OrderBy(m => m.State).ToList();
            }
            DebugUtils.EndLogEvent("SearchResultController.GetResortData");

        }

        private double CalculateTax(ResortDetails resort, double amount)
        {
            double tax = 0;
            if (resort != null)
            {
                tax = resort.TaxRate;
                tax = tax * Convert.ToDouble(amount);
                tax = tax / 100;
                tax = Math.Round(tax, 2, MidpointRounding.AwayFromZero);  //Round up tax charge to a penny.
                                                                          //double total = tax * reservation.NumberOfNights + Convert.ToDouble(reservation.Amount);

            }
            return tax;
        }

        private List<RoomDailyRate> BuildDailyRates(InventoryAvailability item)
        {
            var returnValue = new List<RoomDailyRate>();

            if (item.DailyRates != null)
            {
                foreach (DailyRate rate in item.DailyRates)
                {
                    try
                    {
                        RoomDailyRate newRate = new RoomDailyRate();
                        newRate.CalendarDate = DateTime.ParseExact(rate.CalendarDate, "yyyyMMdd 000000.000", System.Globalization.CultureInfo.InvariantCulture);
                        newRate.DayNumber = rate.DayNumber;
                        newRate.SeasonCode = rate.SeasonCode;
                        newRate.UnitRate = int.Parse(rate.UnitRate);
                        newRate.WeekNumber = rate.WeekNumber;
                        newRate.SeasonName = rate.SeasonName;
                        newRate.DayName = rate.DayName;
                        returnValue.Add(newRate);
                    }
                    catch (Exception ex)
                    {
                        Sitecore.Diagnostics.Log.Error("Unable to convert DailyDate on inventory search ", ex);

                    }
                }
            }
            return returnValue;

        }

        /// <summary>
        /// get the search parameters from query string.  This is when the search is called from Javascript on the search page
        /// </summary>
        /// <returns></returns>
        private SearchParameters getQuerystringParameters()
        {
            var searchParamtersManager = new SearchParametersManager(); // get the default values stored in session
            var searchParamters = searchParamtersManager.parameter;
            searchParamters.ResortId = "";  //Rsort Id should not be stored in the session
            if (Request.QueryString["ShowAvailabilityOnly"] != null)
            {
                searchParamters.ShowAvailabilityOnly = bool.Parse(Request.QueryString["ShowAvailabilityOnly"]);
            }

            if (Request.QueryString["destination"] != null)
            {
                searchParamters.Destination = Request.QueryString["destination"].ToString();
                searchParamters.DestinationBonusTime = "";
                searchParamters.ResortId = "";
            }

            if (Request.QueryString["checkin"] != null)
            {
                searchParamters.CheckInDate = DateTime.Parse(Request.QueryString["checkin"].ToString());
            }

            if (Request.QueryString["checkout"] != null)
            {
                searchParamters.CheckOutDate = DateTime.Parse(Request.QueryString["checkout"].ToString());
            }

            if (Request.QueryString["MonthSearch"] != null)
            {
                searchParamters.MonthSearch = FormatMonthSearch(Request.QueryString["MonthSearch"].ToString());
            }
            if (Request.QueryString["MonthSearchDuration"] != null)
            {
                searchParamters.monthsearchduration = Request.QueryString["MonthSearchDuration"].ToString();
            }

            if (Request.QueryString["reservationtype"] != null)
            {
                searchParamters.ReservationType = Request.QueryString["reservationtype"].ToString();
            }

            if (Request.QueryString["resortId"] != null)
            {
                searchParamters.ResortId = Request.QueryString["resortId"].ToString();
            }

            return searchParamters;
        }


        public static List<PointsStatus> GetOwnerPoints(List<PointsEligibility> PointsEligibility, System.DateTime checkinDate, ref int elgPoints)
        {

            List<PointsStatus> Accounts = new List<PointsStatus>();



            if (PointsEligibility != null && PointsEligibility.Count() > 0)
            {
                foreach (var item in PointsEligibility)
                {
                    PointsStatus account = new PointsStatus();
                    DateTime strExpireDate = default(DateTime);
                    if (item.ExpirationDate != null)
                    {
                        strExpireDate = DateTime.ParseExact(item.ExpirationDate.Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        account.ExpDate = string.Format("{0:MM/dd/yyyy}", strExpireDate);

                        if (item.PointsType != null)
                        {
                            account.PointsType = PointsTypeDesc(item.PointsType.Trim());
                        }
                        if (item.PointsAvailable != null)
                        {
                            account.Points = string.Format("{0:#,#}", Convert.ToInt32(item.PointsAvailable));
                        }
                        if (item.EligibleMessage != null)
                        {
                            account.EligibilityMsg = item.EligibleMessage.Trim();
                        }
                        account.IsEligible = item.IsEligible.ToString();
                        if ((account.EligibilityMsg.IndexOf("Eligible") != -1))
                        {
                            elgPoints = elgPoints + Convert.ToInt32(item.PointsAvailable);
                            account.IsEligible = "True";
                        }


                        Accounts.Add(account);
                    }
                }
            }
            return Accounts;

        }
        private static string PointsTypeDesc(string pointsType)
        {
            string pointsDesc = "";

            switch (pointsType)
            {
                case "A":
                    pointsDesc = "Annual";
                    break;
                case "B":
                    pointsDesc = "Borrowed";
                    break;
                case "X":
                    pointsDesc = "Additional Points";
                    break;
                case "F":
                    pointsDesc = "AIM Points";
                    break;
                case "H":
                    pointsDesc = "Anniversary";
                    break;
                case "J":
                    pointsDesc = "New Sale Incentive";
                    break;
                case "K":
                    pointsDesc = "CustomerSatisfactionCert";
                    break;
                case "S":
                    pointsDesc = "Saved";
                    break;
                case "M":
                    pointsDesc = "Conversion Certificate";
                    break;
                case "T":
                    pointsDesc = "Biennial Points";
                    break;
                case "U":
                    pointsDesc = "Biennial Borrowed";
                    break;
                case "R":
                    pointsDesc = "Rented";
                    break;
                default:

                    break;
            }

            return pointsDesc;

        }

        private static List<DailyRates> GetReservationDailyRates(DateTime CheckInDate, DateTime CheckOutDate, int ProjectNumber, string UnitType)
        {
            List<DailyRates> dailyRates = new List<DailyRates>();

            ResortService resortService = new ResortService();

            PointRatesDetailRequest dailyPointRequest = new PointRatesDetailRequest();
            dailyPointRequest.CheckInDate = CheckInDate.ToString("yyyyMMdd 000000.000", System.Globalization.CultureInfo.InvariantCulture);
            dailyPointRequest.CheckOutDate = CheckOutDate.ToString("yyyyMMdd 000000.000", System.Globalization.CultureInfo.InvariantCulture);
            dailyPointRequest.ProjectNumber = ProjectNumber.ToString();
            dailyPointRequest.UnitType = UnitType;
            dailyPointRequest.SiteName = "OnlinePoints";

            var dailyRateResponse = resortService.PointRatesDetail(dailyPointRequest);



            foreach (var ratesDailyItem in dailyRateResponse.PointDailyRates)
            {
                //if (rowCount == 14 & DateTime.Parse(ratesDailyItem.CalendarDate) > CheckOutDate.AddDays(-1))
                //{
                //    break; // TODO: might not be correct. Was : Exit For
                //}

                DailyRates rate = new DailyRates();

                rate.DailyRate = ratesDailyItem.UnitRate;
                rate.DayName = ratesDailyItem.DayName;
                rate.SeasonName = ratesDailyItem.SeasonName;
                rate.WeekNumber = ratesDailyItem.WeekNumber;
                rate.CalendarDate = DateTime.ParseExact(ratesDailyItem.CalendarDate, "yyyyMMdd 000000.000", CultureInfo.InvariantCulture);
                rate.CalendarDay = rate.CalendarDate.Day.ToString();
                rate.DayNumber = ratesDailyItem.DayNumber;
                rate.SeasonCode = ratesDailyItem.SeasonCode;
                rate.UnitType = ratesDailyItem.UnitType;
                rate.SeasonCss = SearchUtils.GetSeasonCss(ratesDailyItem.SeasonName);
                if (rate.CalendarDate >= CheckInDate & rate.CalendarDate <= CheckOutDate.AddDays(-1))
                {
                    rate.DaySelected = true;
                    rate.SeasonCss += " active";

                }

                dailyRates.Add(rate);
                // rowCount = rowCount + 1;
            }

            return dailyRates;


        }

        public static DateTime FormatMonthSearch(string  dateSelected)
        {
            var retValue = new DateTime();

            if (DateTime.TryParse(dateSelected, out retValue))
            {
                retValue = new DateTime(retValue.Year, retValue.Month, DateTime.DaysInMonth(retValue.Year, retValue.Month));  //Alway use the last day of the month
            }
            return retValue;
        }
        public ActionResult LastMinuteAvailability()
        {
            ISitecoreContext service = new SitecoreContext();
            LastMinuteAvailability model = service.GetCurrentItem<LastMinuteAvailability>();
            //var model = GetLayoutItem<LastMinuteAvailability>();
            return View(model);
        }

        private int GetReservationsCount(string resortId)
        {
            int reservationsCount = 0;
            try
            {
                ResortService service = new ResortService();
                BlueGreenContext bgcontext = new BlueGreenContext();

                ResortDetails resort = ResortManager.FindResort(FormatUtils.ConvertStringToInt(resortId));
                if (resort.ResortRegion != null)
                {
                    var futureReservation = service.GetFutureReservations(bgcontext.OwnerId, DateTime.Now, null);
                    List<string> resortIdByRegion = ResortManager.GetAllProjectIdForRegion(resort.ResortRegion.Code);

                    var query1 = futureReservation.Reservations.Where(x => resortIdByRegion.Any(x1 => x1 == x.ProjectStay));
                    if (query1 != null)
                    {
                        reservationsCount = query1.Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Unexpected exception Calling GetReservationsCount", ex);

            }

            return reservationsCount;
        }

        private int CountRecords(SearchResult model)
        {
            int itemCount = 0;
            if (model != null && model.resortList != null)
            {
                foreach (var resort in model.resortList)
                {
                    if (resort.availableRoom != null)
                    {
                        itemCount += resort.availableRoom.Count();
                    }
                    else
                    {
                        itemCount += 1;  
                    }
                }
            }
            return itemCount;
        }

        private bool FilterResortForMonthSearch(List<ResortDetails> resortList, SearchParameters searchParamters, string searchWindow, out bool showSummerMonthError)
        {
            var resortIds = "";
            var showError = false;
            showSummerMonthError = false;
            //sampler users are not allow to book most of the resort during summer time.  FIlter the result
            var userContext = new BlueGreenContext();
            var ownerType = userContext.GetOwnerType();

            if (ownerType == BlueGreenContext.OWNER_TYPE_SAMPLER || ownerType == BlueGreenContext.OWNER_TYPE_SAMPLER24)
            {
                if (!SearchUtils.VerifySummer(searchParamters, searchWindow))
                {
                    showSummerMonthError = true;
                }
            }
            if (resortList != null && resortList.Count() > 0)
            {


                IEnumerable<int> filteringQuery = new List<int>();
                int duration = int.Parse(searchParamters.monthsearchduration);
                if (duration != 7)
                {
                    var fullWeekCount =
                        (from res in resortList
                         where res.MaximumNightStayPoints >= 7 && res.MaximumNightStayPoints <= 7
                         select res.ResortId).Count();
                    if (fullWeekCount == resortList.Count())
                    {
                        //Update query to search for 7 days
                        searchParamters.monthsearchduration = "7";
                        showError = true;
                    }
                }


                if (searchParamters.ReservationType == SearchParameters.RESERVATION_TYPE_POINTS)
                {
                    filteringQuery =
                        from res in resortList
                        where res.MinimumNightStayPoints <= duration && res.MaximumNightStayPoints >= duration
                        select res.ResortId;
                }
                else
                {
                    filteringQuery =
                       from res in resortList
                       where res.MinimumNightStayBonusTime <= duration && res.MaximumNightStayBonusTime >= duration
                       select res.ResortId;
                }
                if (filteringQuery != null && filteringQuery.Count() > 0)
                {
                    resortIds = string.Join(", ", filteringQuery);
                }
                searchParamters.ResortId = resortIds;
            }
            return showError;
        }

        public List<int> GetAllResortForSecondaryMarket(string reservationType)
        {
            BlueGreenContext bgContext = new BlueGreenContext();

            List<int> homeResortId = new List<int>();
            if (bgContext.IsSecondaryMarketUser && reservationType == SearchParameters.RESERVATION_TYPE_BONUSTIME)
            {
                if (bgContext.bxgOwner.Accounts != null && bgContext.bxgOwner.Accounts.Count() > 0)
                {
                    foreach (var account in bgContext.bxgOwner.Accounts)
                    {
                        var homeProjectId = account.ProjectHome;
                        if (!string.IsNullOrEmpty(homeProjectId))
                        {
                            var resort = ResortManager.GetResortByProject(homeProjectId);
                            if (resort != null)
                            {
                                homeResortId.Add(resort.ResortId);
                            }
                        }
                    }
                }

               
            }

            return homeResortId;
        }
    }
}