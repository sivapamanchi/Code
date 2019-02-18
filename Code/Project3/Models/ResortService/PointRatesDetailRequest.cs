
using System.Collections.Generic;

namespace BGSitecore.Models.ResortService.PointRatesDetailRequest
{
    public class PointRatesDetailRequest
    {
        public string SiteName { get; set; }
        public string ProjectNumber { get; set; }
        public string UnitType { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string LengthOfStay { get; set; }
        public string RatesType { get; set; }
    }
}
