using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.MortgageService
{
    public class SubmitPaymentSettlementBatchRequest
    {
        public SubmitPaymentSettlementBatchRequest()
        {
            cbtype = string.Empty;
            cbrtid = string.Empty;
            cbname = string.Empty;
            cbauth = string.Empty;
            cbmid = string.Empty;
            cbrtrn = string.Empty;
        }

        public string cbtype { get; set; }
        public string cbrtid { get; set; }
        public string cbproj { get; set; }
        public string cbname { get; set; }
        public string cbauth { get; set; }
        public string cbmid { get; set; }
        public string cbrtrn { get; set; }
        public string cbacct { get; set; }
        public string cbamt { get; set; }
        public string agent { get; set; }
        public bool declineArda { get; set; }
        public string batch { get; set; }
    }
}