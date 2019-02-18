using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.MortgageService
{
    public class ProcessTotalPaymentRequest
    {

        public ProcessTotalPaymentRequest() {
            status = "P";
            user = "BGO";
            date = DateTime.Now.ToString("yyyyMMdd");
            time = DateTime.Now.ToString("hhmmss");
            whacoviaflg = "N";
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
    }
}