
using System;
using System.Collections.Generic;

namespace BGSitecore.Models.ResortService.SearchReservationResponse
{

    [Serializable]
    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }

    [Serializable]
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

    [Serializable]
    public class Phone
    {
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneExt { get; set; }
    }

    [Serializable]
    public class Email
    {
        public string EmailType { get; set; }
        public string EmailAddress { get; set; }
    }

    //[Serializable]
    //public class Guest
    //{
    //    public string GuestType { get; set; }
    //    public string GuestID { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string FullName { get; set; }
    //    public string Relationship { get; set; }
    //    public List<Address> Addresses { get; set; }
    //    public List<Phone> Phones { get; set; }
    //    public List<Email> Emails { get; set; }
    //}

    //[Serializable]
    //public class Reservation
    //{
    //    public string EligibleDate { get; set; }
    //    public string PolicyStatus { get; set; }
    //    public string PolicyPrice { get; set; }
    //    public string DateConfirmed { get; set; }
    //    public string CheckInDate { get; set; }
    //    public string CheckOutDate { get; set; }
    //    public string NumberOfNights { get; set; }
    //    public string Owner { get; set; }
    //    public string NumberOfAdults { get; set; }
    //    public string Points { get; set; }
    //    public string OwnerProjectNumber { get; set; }
    //    public string ReservationNumber { get; set; }
    //    public string ExchangeCode { get; set; }
    //    public string ExchangeDate { get; set; }
    //    public string ReservationStatus { get; set; }
    //    public string HistoryType { get; set; }
    //    public string TakenBy { get; set; }
    //    public string Amount { get; set; }
    //    public string ReservationType { get; set; }
    //    public string AS400UnitType { get; set; }
    //    public string UnitNumber { get; set; }
    //    public string OwnerID { get; set; }
    //    public string MaximumOccupancy { get; set; }
    //    public string ProjectStay { get; set; }
    //    public string AmountDue { get; set; }
    //    public string UnitView { get; set; }
    //    public string OwnerAccount { get; set; }
    //    public string Handicap { get; set; }
    //    public string AvailableWaivers { get; set; }
    //    public List<Guest> Guests { get; set; }
    //}

    [Serializable]
    public class SearchReservationResponse
    {
        public string Success { get; set; }
        public List<Error> Errors { get; set; }
        public List<BGSitecore.Models.ResortService.ReservationsList.Reservation> Reservations { get; set; }
    }

}
