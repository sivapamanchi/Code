using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.OwnerService
{
    public class UpdateAccountStatsReq
    {
        public string ARVACT { get; set; } = string.Empty;
        public string address1 { get; set; } = string.Empty;
        public string address2 { get; set; } = string.Empty;
        public string address3 { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public string zip_code { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public string phoneHome { get; set; } = string.Empty;
        public string phoneBusiness { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string user { get; set; } = "BGOnline";
        public string action { get; set; } = "update all";
        public string isPaperless { get; set; } = string.Empty;
        public string stateAbbr { get; set; } = string.Empty;
    }

    public class UpdateAccountStatsResp
    {
        public Errors Errors { get; set; }
        public string Status { get;set;}
        
    }
    public class Errors
    {
        public string Error { get; set; }
    }
}