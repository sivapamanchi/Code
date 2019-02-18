using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.OwnerGuestList
{

    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
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

    public class Phone
    {
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneExt { get; set; }
    }

    public class Email
    {
        public string EmailType { get; set; }
        public string EmailAddress { get; set; }
    }

    public class Guest
    {
        public string GuestType { get; set; }
        public string GuestID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Relationship { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Email> Emails { get; set; }
    }

    public class OwnerGuestListResponse
    {
        public string Success { get; set; }
        public List<Error> Errors { get; set; }
        public List<Guest> Guests { get; set; }
    }

}