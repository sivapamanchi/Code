using BGSitecore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.MortgageService
{
    public class SubmitPaymentBatchPayResponse
    {
        public string BatchID { get; set; }
        public string confirmationNumber { get; set; }

        public string retCode { get; set; }
        public List<Error> Errors { get; set; }
    }
}