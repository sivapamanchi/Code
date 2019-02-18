using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.ScreeningBookReservationResponse
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
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string ProvinceCode { get; set; }
        public string Subdivison { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
    }

    [Serializable]
    public class Phone
    {
        public string PhoneNumber { get; set; }
        public string Prefix { get; set; }
        public string Extension { get; set; }
        public string PhoneNumberType { get; set; }
        public string CountryCode { get; set; }
    }

    [Serializable]
    public class EmailAddress
    {
        public string AddressType { get; set; }
        public string Email { get; set; }
    }

    [Serializable]
    public class Guest
    {
        public string GuestType { get; set; }
        public int GuestID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Phone> Phones { get; set; }
        public List<EmailAddress> EmailAddresses { get; set; }
    }

    [Serializable]
    public class PointsEligibility
    {
        public string ExpirationDate { get; set; }
        public string PointsAvailable { get; set; }
        public string PointsType { get; set; }
        public string EligibleMessage { get; set; }
        public string IsEligible { get; set; }
    }

    [Serializable]
    public class ScreeningBookReservation
    {
        public decimal SPECost { get; set; }            //This was manually changed
        public decimal PPPCost { get; set; }            //This was manully changed
        public string BookStatus { get; set; }
        public bool OwnerGoodStand { get; set; }            //manually changed
        public bool OwnerHasPastDueAccount { get; set; }    //manually changed
        public bool OwnerEligible { get; set; }      //manually changed
        public List<Guest> Guests { get; set; }
        public List<PointsEligibility> PointsEligibility { get; set; }
    }

    [Serializable]
    public class ConfirmReservation
    {
        public string ReservationNumber { get; set; }
        public string PointsProtectionEligible { get; set; }
        public decimal PointsProtectionPrice { get; set; }      //manually changed
        public string PointsProtectionEligibleDate { get; set; }
    }

    [Serializable]
    public class HoldUnit
    {
        public string ReservationNumber { get; set; }
        public string ProjectNumber { get; set; }
        public string UnitTypeCode { get; set; }
    }

    [Serializable]
    public class ModifyReservation
    {
        public string ReservationNumber { get; set; }
    }

    [Serializable]
    public class CancelReservation
    {
        public string CancellationNumber { get; set; }
        public string CancelCode { get; set; }
        public string CancellationDate { get; set; }
    }

    [Serializable]
    public class ScreeningBookReservationResponse
    {
        public List<Error> Errors { get; set; }
        public ScreeningBookReservation ScreeningBookReservation { get; set; }
        public ConfirmReservation ConfirmReservation { get; set; }
        public HoldUnit HoldUnit { get; set; }
        public ModifyReservation ModifyReservation { get; set; }
        public CancelReservation CancelReservation { get; set; }
    }



}