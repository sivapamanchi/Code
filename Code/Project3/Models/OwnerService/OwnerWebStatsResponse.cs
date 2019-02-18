using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.OwnerService.OwnerWebStatsResponse
{


    public class Error
    {
        public string Code { get; set; }
        public string Shorttext { get; set; }
    }

    public class OwnerWebStatsResponse
    {
        public List<Error> Errors { get; set; }
        public string Status { get; set; }
    }
}