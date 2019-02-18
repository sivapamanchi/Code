using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.MortgageService
{
    public class UserPayInfoResponseModel
    {
        public IEnumerable<UserPayInfo> MaintenanceFeeAS400 { get; set; }
    }

    public class UserPayInfo
    {
        public string ACCT { get; set; }
        public string MRCHID { get; set; }
        public string PMTDTE { get; set; }
        public decimal LSTPMT { get; set; }
        public decimal BALCU { get; set; }
        public decimal BAL30 { get; set; }
        public decimal BAL60 { get; set; }
        public decimal BAL90 { get; set; }
        public decimal BAL120 { get; set; }
        public string BILMNT { get; set; }
        public string OLDACT { get; set; }
        public string ARVACT { get; set; }
        public string PROJNUM { get; set; }
        public string PRONAME { get; set; }
        public string ARDAFL { get; set; }
        public string WEEKS { get; set; }
        public string ARSLTY { get; set; }
        public decimal ARDAMT1 { get; set; }
        public string Colstas { get; set; }
        public int AccountStatus { get; set; }

    }
}