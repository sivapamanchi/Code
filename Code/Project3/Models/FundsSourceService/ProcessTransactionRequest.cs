using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.FundsSourceService
{
    public class ProcessTransactionRequest
    {
        public string CreditCardType { get; set; }
        public string CreditCardNumber { get; set; }
        public string TransarmorToken { get; set; }
        public string CreditCardExpirationDate { get; set; }
        public string CreditCardCVV { get; set; }
        public string PaymentAmount { get; set; }
        public string MerchantAccountNumber { get; set; }
        public string TransactionType { get; set; }
        public string NameOnTheCard { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string AuthCode { get; set; }
        public string TransactionID { get; set; }
        public ProcessTransactionRequestMetadata MetaData { get; set; }       
    }

    public class ProcessTransactionRequestMetadata
    {
        public string CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string ReferenceID { get; set; }
    }
   
}