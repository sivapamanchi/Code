using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//This code was copied from the ReservationLibrary.dll
//This class is required to pass data from the MyReservation page to the Cancel page that is still in Legacy

using System.Diagnostics;

namespace BGSitecore.Models.ReferralService
{
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
    public class Addresses
    {

        public Address Address { get; set; }
    }
    public class PhoneNumber
    {

        public string __PhoneNumber { get; set; }

        public string Prefix { get; set; }

        public string Extension { get; set; }

        public string PhoneNbr { get; set; }
        public string PhoneNumberType { get; set; }

        public string CountryCode { get; set; }
    }

    public class PhoneNumbers
    {

        public PhoneNumber PhoneNumber { get; set; }
        
    }

    public class EmailAddress
    {

        public string Email { get; set; }

        public string AddressType { get; set; }
    }

    public class EmailAddresses
    {

        public EmailAddress EmailAddress { get; set; }
    }

    public class Security
    {

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Domain { get; set; }
    }

    public class Person
    {

        public string PersonType { get; set; }

        public string Identifier { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Address> Addresses { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; }

        public List<EmailAddress> EmailAddresses { get; set; }

        public string SpouseFirstName { get; set; }

        public string SpouseLastName { get; set; }

        public string MaritalStatus { get; set; }

        public string Relationship { get; set; }

        public Security Security { get; set; }

        public List<PhoneNumbers> PhoneNumbersList { get; set; } = new List<PhoneNumbers>();
        //public List<EmailAddresses> EmailAddressList { get; set; } = new List<EmailAddresses>();

    }

    public class Legacy
    {
        public string ResortCode { get; set; }
        public string BGRewardsOwnerID { get; set; }
        public MarketSource MarketSource { get; set; } = new MarketSource();
    }
    public class MetaData
    {
        public string CreateDate { get; set; }
        public string ExpirationDate { get; set; }
    }
    public class MarketSource
    {
        public string MarketSourceCode { get; set; }
    }
    public class People
    {

        public Person Person { get; set; }
    }

    public class ReferralReq
    {

        public string ConciergeAccountID { get; set; }

        public string BGRewardsOwnerID { get; set; }

        public string ResortCode { get; set; }

        public string MarketLink { get; set; }

        public string MarketSourceCode { get; set; }

        public string MailCode { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<Person> People { get; set; }
        
    }

    public class SendDataToResponsysReq
    {
        public string FolderName { get; set; }

        public string SupplymentTableName { get; set; }

        public string EventID { get; set; }

        public string EmailAddress { get; set; }

        public List<Keyvalue> KeyValues = new List<Keyvalue>();

        public List<PrimaryKeyModel> PrimaryKeys = new List<PrimaryKeyModel>();

        public class PrimaryKeyModel
        {
            public string PrimaryKey { get; set; }
        }
        public class Keyvalue
        {
            public string Key { get; set; }

            public string Value { get; set; }

        }
    }


}