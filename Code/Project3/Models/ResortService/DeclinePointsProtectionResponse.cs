using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.DeclinePointsProtection
{

    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }

    public class AcceptPointsProtection
    {
        public string AuthenticationCode { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyStatus { get; set; }
    }

    public class DeclinePointsProtectionResponse
    {
        public List<Error> Errors { get; set; }
        public string ReservationNumber { get; set; }
        public AcceptPointsProtection AcceptPointsProtection { get; set; }
    }


}