using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.BGOInventoryAvailability
{


    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }

    public class Inventory
    {
        public string InventoryID { get; set; }
        public string ProjectNumber { get; set; }
        public string UnitType { get; set; }
        public string UnitNumber { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string Accomodates { get; set; }
        public string HandicapAccessible { get; set; }
        public string ViewType { get; set; }
        public string WebUnitCode { get; set; }
        public string ViewDescription { get; set; }
        public string RoomDescription { get; set; }
        public string TotalUnits { get; set; }
    }

    public class ResortInfo
    {
        public string ResortInfoID { get; set; }
        public string ResortID { get; set; }
        public string ResortName { get; set; }
        public string ResortAddr1 { get; set; }
        public string ResortAddr2 { get; set; }
        public string ResortCity { get; set; }
        public string ResortState { get; set; }
        public string ResortPostalCode { get; set; }
        public string ResortCountryCode { get; set; }
        public string ResortPhone { get; set; }
        public string MinimumPeople { get; set; }
        public string MaximumPeople { get; set; }
    }

    public class RateSummary
    {
        public string RateSummaryID { get; set; }
        public string Projectnumber { get; set; }
        public string UnitType { get; set; }
        public string PointsRate { get; set; }
        public string SeasonName { get; set; }
    }

    public class BTRatesItem
    {
        public string BTRatesItemID { get; set; }
        public string ResortID { get; set; }
        public string DailyRate { get; set; }
        public string TotalPrice { get; set; }
        public string TaxRate { get; set; }
    }

    public class DailyRate
    {
        public string ProjectNumber { get; set; }
        public string UnitType { get; set; }
        public string CalendarDate { get; set; }
        public string WeekNumber { get; set; }
        public string DayNumber { get; set; }
        public string DayName { get; set; }
        public string SeasonCode { get; set; }
        public string SeasonName { get; set; }
        public string UnitRate { get; set; }
        public string AS400DayNumber { get; set; }
    }

    public class InventoryAvailability
    {
        public Inventory Inventory { get; set; }
        public ResortInfo ResortInfo { get; set; }
        public RateSummary RateSummary { get; set; }
        public BTRatesItem BTRatesItem { get; set; }
        public List<DailyRate> DailyRates { get; set; }
    }

    public class BGOInventoryAvailabilityResponse
    {
        public string Success { get; set; }
        public List<Error> Errors { get; set; }
        public List<InventoryAvailability> InventoryAvailability { get; set; }
    }

}