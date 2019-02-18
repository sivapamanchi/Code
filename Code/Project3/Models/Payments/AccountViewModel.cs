using BGSitecore.Models.MortgageService;
using Glass.Mapper.Sc.Fields;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BGSitecore.Models.Payments
{
    [Serializable]
    public class AccountViewModel
    {
        public int GroupID { get; set; }
        public string GroupTitle { get; set; }
        public List<AccountBalanceInfo> UserPayInfoList { get; set; }
        public decimal TotalDues { get; set; }
        public decimal ARDADues { get; set; }
        public bool IsPaymentAllowed { get; set; }
        public string ViewStatementLink { get; set; }
        public string ViewStatementLinkText { get; set; }
        public bool IsAccountSecondaryLinkAllowed { get; set; }
        public bool IsErrorMessage { get; set; }

        public decimal TotalAmount
        {
            get
            {
                return UserPayInfoList != null && UserPayInfoList.Count > 0 ? UserPayInfoList.Sum(x => x.PaymentAmount) : 0;
            }
        }

        public string TotalAmountString
        {
            get
            {
               return TotalAmount.ToString("N", Sitecore.Context.Culture);
            }
        }
    }
}