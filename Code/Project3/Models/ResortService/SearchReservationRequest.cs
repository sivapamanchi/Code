
using System.Collections.Generic;

namespace BGSitecore.Models.ResortService.SearchReservationRequest
{

    public class SearchReservationRequest
    {
        public string SiteName { get; set; }
        public string OwnerID { get; set; }
        public string EffectiveDate { get; set; }
        public string ReservationType { get; set; }
        public string ReservationID { get; set; }
    }
}
