using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ReferralService
{
    public class SaveReferralResponse
    {
        public string Success { get; set; }
        
        public List<Error> Errors { get; set; }

        public string Status { get; set; }

        public string TransactionID { get; set; }
    }

    public class ReferralResponse
    {
        public List<Referrals> Referrals { get; set; } = new List<Referrals>();
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }

    public class Referrals
    {
        public string Identifier { get; set; }
        public List<Person> People { get; set; } //= new List<Person>();
        public Legacy Legacy { get; set; } = new Legacy();

        public MetaData MetaData { get; set; } = new MetaData();

    }

    public class PeopleResponse
    {
        public string PersonType { get; set; }
        public string Identifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public PhoneNumbersResponse PhoneNumbers { get; set; } = new PhoneNumbersResponse();
        public AddressesResponse Addresses { get; set; } = new AddressesResponse();
        public EmailAddressesResponse EmailAddresses { get; set; } = new EmailAddressesResponse();

    }
    public class LegacyResponse
    {
        public string ResortCode { get; set; }
        public string BGRewardsOwnerID { get; set; }
        public MarketSourceResponse MarketSource { get; set; } = new MarketSourceResponse();
    }

    public class MarketSourceResponse
    {
        public string MarketSourceCode { get; set; }
    }
    public class PhoneNumbersResponse
    {
        public string PhoneNbr { get; set; }
        public string PhoneNumberType { get; set; }
    }
    public class AddressesResponse
    {
        public string ProvinceCode { get; set; }
    }
    public class EmailAddressesResponse
    {
        public string Email { get; set; }
    }
}