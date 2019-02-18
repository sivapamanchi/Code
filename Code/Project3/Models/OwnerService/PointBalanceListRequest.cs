using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.OwnerService.PointBalanceListRequest
{


    public class PointBalanceListRequest
    {
        public int OwnerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string ProvinceCode { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string SSN { get; set; }
        public string AccountBalanceAction { get; set; }
        public string ContractMaxDate { get; set; }
        public string ContractMinDate { get; set; }
        public string SiteName { get; set; }
        public string IsPaperlessRequest { get; set; }
    }

}