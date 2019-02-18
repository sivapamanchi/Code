using BGSitecore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.FundsSourceService
{
    public class ProcessTransactionResponse
    {
        public string PaymentAmount { get; set; }
        public string AuthCode { get; set; }
        public string TransactionID { get; set; }
        public ProcessTransactionResponseMetaData MetaData { get; set; }
        public List<Error> Errors { get; set; }       
    }

    public class ProcessTransactionResponseMetaData
    {
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public string ReferenceID { get; set; }
    }    
}