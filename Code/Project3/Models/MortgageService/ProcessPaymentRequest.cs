using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.MortgageService
{
    public class ProcessPaymentRequest
    {
        public ProcessPaymentRequest()
        {
            user = "ACHMAINT";
            date = DateTime.Now.ToString("yyyyMMdd");
            time = DateTime.Now.ToString("hhmmss");
            whacoviaflg = "N";
            status = "0";
            member = "0";
            retcode = "0";
        }
        public string routing { get; set; }

        public string account { get; set; }
        public string vacclub { get; set; }
        public string project { get; set; }
        public string ownacct { get; set; }
        public string ownname { get; set; }
        public string checkingsaving { get; set; }
        public string amount { get; set; }
        public string status { get; set; }
        public string user { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string confirmation { get; set; }
        public string whacoviaflg { get; set; }
        public string posdate { get; set; }
        public string member { get; set; }
        public string retcode { get; set; }
    }
}