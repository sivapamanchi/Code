
using System.Collections.Generic;

namespace BGSitecore.Models.ResortService.ResortsAvailabilityByDate
{
    public class ResortsAvailabilityByDateRequest
    {
        public string SiteName { get; set; }
        public string OwnerType { get; set; }
        public string ResortID { get; set; }
        public string ProjectNumber { get; set; }
        public string UnitType { get; set; }
        public string WebUnitType { get; set; }
        public string CheckInDate { get; set; }
        public string LengthOfStay { get; set; }
        public string Accomodates { get; set; }
        public string ReservationSource { get; set; }
        public string ReservationType { get; set; }
        public string HandicapAccessible { get; set; }
        public string FullWeekResort { get; set; }
        public string IsPrimium { get; set; }
        public string UnitsCount { get; set; }
        public string SearchWindow { get; set; }
        public string Return1UnitPerUnitType { get; set; }
    }
}
