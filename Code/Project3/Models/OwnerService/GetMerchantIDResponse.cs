using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.OwnerService
{
    public class GetMerchantIDResponse
    {
        public String SerialNumber { get; set; }
        public String ProjectNumber { get; set; }
        public String MerchantID { get; set; }
        public String AccountDescription { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();
    }
}