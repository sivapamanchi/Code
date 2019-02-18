using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.CancelReservationRequest
{


    public class Payment
    {
        public string CreditCardNumber { get; set; }
        public string CreditCardType { get; set; }
        public string CreditCardExpirationDate { get; set; }
        public string CreditCardName { get; set; }
        public string CreditCardAuthorization { get; set; }
        public int CreditCardTotal { get; set; }
        public int NonTaxTotal { get; set; }
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
        public int GuestID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Email> Emails { get; set; }
    }

    public class Attribute
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class CancelReservationRequest
    {
        public string SiteName { get; set; }
        public int OwnerID { get; set; }
        public int ResortID { get; set; }
        public string OwnerProjectNumber { get; set; }
        public string ReservationProjectNumber { get; set; }
        public string UnitTypeCode { get; set; }
        public string AccountNumber { get; set; }
        public string CheckInDate { get; set; }
        public int LengthOfStay { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public string ReservationNumber { get; set; }
        public string ReservationSource { get; set; }
        public string ReservationType { get; set; }
        public Payment Payment { get; set; }
        public string Comments { get; set; }
        public List<Guest> Guests { get; set; }
        public string Agent { get; set; }
        public int Points { get; set; }
        public string HandicapAccessible { get; set; }
        public List<Attribute> Attributes { get; set; }
        public string Season { get; set; }
    }


}