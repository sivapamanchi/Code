using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public class SearchResultResortList
    {

        public string ResortName { get; set; }
        public int ResortId { get; set; }

        public string ResortImage { get; set; }

        public ClubAffiliation ClubAffiliation { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string ZipCode { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public State State { get; set; }
        public int MinNightStay { get; set; }
        public int MaxNightStay { get; set; }
        public int AdvanceSearchWindow { get; set; }

        //This is the code from the search
        public string StateCode { get; set; }

        public List<SearchResultResortRoom> availableRoom { get; set; }
        public List<string> WebUnitTypesHandicap { get; set; }
        public List<string> WebUnitTypesNonHandicap { get; set; }

        public string ResortLink { get; set; }
    }

    [Serializable]
    public class SearchResultResortRoom
    {

        public int ProjectNumber { get; set; }

        public string UnitType { get; set; }

        public string UnitSize { get; set; }

        public string RoomTitle { get; set; }

        public string RoomDescription { get; set; }

        public string SeasonCode { get; set; }

        public int Accomodates { get; set; }

        public string TotalUnits { get; set; }

        public int PointsRate { get; set; }

        public BTRatesItem BTRateItem { get; set; }

        public List<RoomDailyRate> DailyRates { get; set; }

        public string HandicapAccessible { get; set; }

        public string EncodedData { get; set; }

    }

    [Serializable]
    public class BTRatesItem
    {
        public string BTRatesItemID { get; set; }
        public string DailyRate { get; set; }
        public double TotalPrice { get; set; }
        public string TaxRate { get; set; }
       
    }

    [Serializable]
    public class RoomDailyRate
    {


        public DateTime CalendarDate { get; set; }

        public string WeekNumber { get; set; }

        public string DayNumber { get; set; }

        public string SeasonCode { get; set; }

        public string SeasonName { get; set; }

        public string DayName { get; set; }

        public int UnitRate { get; set; }

    }
}