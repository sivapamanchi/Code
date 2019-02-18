using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BGSitecore.Models.Interface;
using Glass.Mapper.Sc.Configuration.Attributes;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models.Payments
{
    public class AccountPayment : BasePage, IPayment
    {
        private const string TotalPaymentID = "{162D4AC7-9ECC-451A-9EDB-9281EBECF744}";


        #region form Item
        [SitecoreIgnore]
        public decimal Amount { get; set; }
        public string AmountString { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "Payment_RoutingTransitNumber_Required")]
        [MinLengthTranslated(9, ErrorMessage = "Payment_RoutingTransitNumber_Length")]
        [MaxLengthTranslated(9, ErrorMessage = "Payment_RoutingTransitNumber_Length")]
        [RoutingNumberTranslated()]
        public string txtRouingTransitNumber { get; set; }


        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "Payment_BankAccountNumber_Required")]
        [MinLengthTranslated(10, ErrorMessage = "Payment_BankAccountNumber_Length")]
        [MaxLengthTranslated(12, ErrorMessage = "Payment_BankAccountNumber_Length")]
        public string txtBankAccountNumber { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "Payment_ReBankAccountNumber_Required")]
        [CompareTranslated("txtBankAccountNumber", ErrorMessage = "Payment_ReBankAccountNumber_Compare")]
        public string txtReBankAccountNumber { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "Payment_TermAndCondition_Required")]
        public bool AgreeToTermAndConditions { get; set; }
        #endregion

        #region siteocre Properties
        [SitecoreField(FieldName = TotalPaymentID)]
        public string TotalPayment { get; set; }
        #endregion
    }
}