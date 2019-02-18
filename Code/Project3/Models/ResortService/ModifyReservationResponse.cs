using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.ModifyReservationResponse
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
        public int GuestID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Email> Emails { get; set; }
    }

    public class PointsEligibility
    {
        public string ExpirationDate { get; set; }
        public string PointsAvailable { get; set; }
        public string PointsType { get; set; }
        public string EligibleMessage { get; set; }
        public string IsEligible { get; set; }
    }

    public class ScreeningBookReservation
    {
        public int SPECost { get; set; }
        public int PPPCost { get; set; }
        public string BookStatus { get; set; }
        public string OwnerGoodStand { get; set; }
        public string OwnerHasPastDueAccount { get; set; }
        public string OwnerEligible { get; set; }
        public List<Guest> Guests { get; set; }
        public List<PointsEligibility> PointsEligibility { get; set; }
    }

    public class ConfirmReservation
    {
        public string ReservationNumber { get; set; }
        public string PointsProtectionEligible { get; set; }
        public int PointsProtectionPrice { get; set; }
        public string PointsProtectionEligibleDate { get; set; }
    }

    public class HoldUnit
    {
        public string ReservationNumber { get; set; }
        public string ProjectNumber { get; set; }
        public string UnitTypeCode { get; set; }
    }

    public class ModifyReservation
    {
        public string ReservationNumber { get; set; }
    }

    public class CancelReservation
    {
        public string CancellationNumber { get; set; }
        public string CancelCode { get; set; }
        public string CancellationDate { get; set; }
    }

    public class ModifyReservationResponse
    {
        public List<Error> Errors { get; set; }
        public ScreeningBookReservation ScreeningBookReservation { get; set; }
        public ConfirmReservation ConfirmReservation { get; set; }
        public HoldUnit HoldUnit { get; set; }
        public ModifyReservation ModifyReservation { get; set; }
        public CancelReservation CancelReservation { get; set; }
    }

}