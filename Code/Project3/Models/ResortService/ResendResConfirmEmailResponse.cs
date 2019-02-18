using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.ResendResConfirmEmail
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

    public class ResendResConfirmEmailResponse
    {
        public List<Error> Errors { get; set; }
    }


}