
using System.Collections.Generic;

namespace BGSitecore.Models.ResortService.PointRatesDetailResponse
{
    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }

    public class PointRatesSummary
    {
        public string ProjectNumber { get; set; }
        public string UnitType { get; set; }
        public string PointsRate { get; set; }
        public string SeasonName { get; set; }
        public string WeekNumber { get; set; }
    }

    public class BTRatesSummary
    {
        public string ProjectNumber { get; set; }
        public string DailyRate { get; set; }
        public string TotalPrice { get; set; }
        public string TaxRate { get; set; }
    }

    public class PointDailyRate
    {
        public string ProjectNumber { get; set; }
        public string UnitType { get; set; }
        public string CalendarDate { get; set; }
        public string WeekNumber { get; set; }
        public string DayNumber { get; set; }
        public string UnitRate { get; set; }
        public string SeasonCode { get; set; }
        public string SeasonName { get; set; }
        public string DayName { get; set; }
    }

    public class PointRatesDetailResponse
    {
        public string Success { get; set; }
        public List<Error> Errors { get; set; }
        public PointRatesSummary PointRatesSummary { get; set; }
        public BTRatesSummary BTRatesSummary { get; set; }
        public List<PointDailyRate> PointDailyRates { get; set; }
    }
}
