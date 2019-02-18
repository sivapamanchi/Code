using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.ReservationsList
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

    public class Reservation
    {
        public string EligibleDate { get; set; }
        public string PolicyStatus { get; set; }
        public string PolicyPrice { get; set; }
        public string DateConfirmed { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int NumberOfNights { get; set; }     //manually changed
        public string Owner { get; set; }
        public string NumberOfAdults { get; set; }
        public string Points { get; set; }
        public string OwnerProjectNumber { get; set; }
        public string ReservationNumber { get; set; }
        public string ExchangeCode { get; set; }
        public string ExchangeDate { get; set; }
        public string ReservationStatus { get; set; }
        public string HistoryType { get; set; }
        public string TakenBy { get; set; }
        public double Amount { get; set; }             //Manually changed
        public string ReservationType { get; set; }
        public string AS400UnitType { get; set; }
        public string UnitNumber { get; set; }
        public string OwnerID { get; set; }
        public string MaximumOccupancy { get; set; }
        public string ProjectStay { get; set; }
        public double AmountDue { get; set; }      //Manually changed
        public string UnitView { get; set; }
        public string OwnerAccount { get; set; }
        public string Handicap { get; set; }
        public string AvailableWaivers { get; set; }
        public string Comments { get; set; }
        public List<Guest> Guests { get; set; }
    }

    public class ReservationsListResponse
    {
        public string Success { get; set; }
        public List<Error> Errors { get; set; }
        public List<Reservation> Reservations { get; set; }
    }

}