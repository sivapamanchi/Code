
using System.Collections.Generic;

namespace BGSitecore.Models.ResortService.InventoryCalendarByResortResponse
{

    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }

    public class Phone
    {
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneExt { get; set; }
    }

    public class Address
    {
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
    }

    public class Email
    {
        public string EmailType { get; set; }
        public string EmailAddress { get; set; }
    }

    public class Resort
    {
        public string ResortName { get; set; }
        public string ResortID { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Email> Emails { get; set; }
    }

    public class WebUnitTypes
    {
        public List<string> WebUnitTypeCode { get; set; }
        public List<string> WebUnitTypeCodeName { get; set; }
    }

    public class Inventory
    {
        public Resort Resort { get; set; }
        public string ProjectNumber { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string UnitType { get; set; }
        public string UnitNumber { get; set; }
        public WebUnitTypes WebUnitTypes { get; set; }
        public string ViewType { get; set; }
        public string ViewDesc { get; set; }
        public string Accomodates { get; set; }
        public string HandicapAccessible { get; set; }
        public string TotalUnits { get; set; }
    }

    public class InventoryCalendarByResortResponse
    {
        public string Success { get; set; }
        public List<Error> Errors { get; set; }
        public List<Inventory> Inventories { get; set; }
    }

}
