using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.ReservationsList
{

    public class ReservationsListRequest
    {
        public string SiteName { get; set; }
        public string OwnerID { get; set; }
        public string EffectiveDate { get; set; }
        public string ReservationType { get; set; }
        public string ReservationID { get; set; }
    }
}