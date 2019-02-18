using BGSitecore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.MortgageService
{
    public class ProcessPaymentResponse
    {
        public List<Error> Errors { get; set; }
    }
}