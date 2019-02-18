using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.BGOInventoryAvailability
{

    public class InventoryAvailability1
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
        public string Segments { get; set; }
    }

    public class BGOInventoryAvailabilityRequest
    {
        public List<InventoryAvailability1> InventoryAvailability { get; set; }
    }

}