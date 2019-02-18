using BGSitecore.Models.Interface;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Payments
{
    public class MailPayment : BasePage, IPayment
    {
        private const string DescriptionID = "{5F8C9E26-A065-44F6-B06A-D97FAE86D175}";

        private const string TotalPaymentID = "{162D4AC7-9ECC-451A-9EDB-9281EBECF744}";
        public decimal Amount { get; set; }
        public string AmountString { get; set; }

        #region sitecore item
        [SitecoreField(FieldName = DescriptionID)]
        public string Description { get; set; }

        [SitecoreField(FieldName = TotalPaymentID)]
        public string TotalPayment { get; set; }

        #endregion
    }
}