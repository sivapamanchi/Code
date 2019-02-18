using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.OwnerService.OwnerWebStatsRequest
{
    public class OwnerWebStatsRequest
    {
        public string WebSessionID { get; set; }
        public string SiteID { get; set; }
        public string OwnerID { get; set; }
        public string SearchTab { get; set; }
        public string SearchTabValue { get; set; }
        public string SearchTabSubVal { get; set; }
        public string ResortID { get; set; }
        public string BGOUnitType { get; set; }
        public string ProjectNumber { get; set; }
        public string UnitType { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string NumberOfGuests { get; set; }
        public string SumBal { get; set; }
        public string PPPFee { get; set; }
        public string PPPEligible { get; set; }
        public string ReservationNumber { get; set; }
        public string Phase { get; set; }
        public string RecordReturnCode { get; set; }
        public string ExtendedResort { get; set; }
        public string ExtendedStay { get; set; }
        public string Handycap { get; set; }
        public string QryTime { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }

}