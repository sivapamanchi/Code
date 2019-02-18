using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.MortgageService
{
    public class SubmitPaymentBatchPayRequest
    {
        public SubmitPaymentBatchPayRequest()
        {
            MPay = "mpay";
            Source = string.Empty;
            ccPayment = string.Empty;
            ccInit = string.Empty;
            ccNum = string.Empty;
            ccExp = string.Empty;
            ccName = string.Empty;
            address = string.Empty;
            zip = string.Empty;
            process = "C";
            roiID = string.Empty;
            errorDesc = string.Empty;
            param18 = string.Empty;
            param19 = string.Empty;
            address = string.Empty;
        }
        public string Arvact { get; set; }
        public string MerchantID { get; set; }
        public string MPay { get; set; }
        public string Source { get; set; }
        public string ProjNum { get; set; }
        public string accountNum { get; set; }
        public string ccPayment { get; set; }
        public string ccInit { get; set; }
        public string ccNum { get; set; }
        public string ccExp { get; set; }
        public string ccName { get; set; }
        public string address { get; set; }
        public string zip { get; set; }
        public string authCode { get; set; }
        public string authAmt { get; set; }
        public string authResult { get; set; }
        public string roiID { get; set; }
        public string errorDesc { get; set; }
        public string param18 { get; set; }
        public string param19 { get; set; }
        public string process { get; set; }
        public string ownerAcct { get; set; }
        public string TransactionSettled { get; set; }
        public string DeclineARDA { get; set; }
        public string installment { get; set; }
    }
}