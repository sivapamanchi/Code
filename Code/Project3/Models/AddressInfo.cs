using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models;
using Glass.Mapper.Sc.Fields;
using Glass.Mapper.Sc.Configuration;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models
{
  
    public class AddressInfo
    {
        public const string FirstNameId = "{F899C475-7831-46E7-A787-74F687B56002}";
        public const string LastNameId = "{285E5748-1E46-4079-9EE0-73109743BEE1}";
        public const string AddressLine1Id = "{29D6D316-08AB-4147-8281-FE0A25F10BF7}";
        public const string AddressLine2Id = "{90EE167C-2646-4987-B8DF-DDC6F86C40B5}";
        public const string AddressLine3Id = "{3633A058-9064-4A21-94CB-6D69386432C6}";
        public const string CityId = "{85812713-701A-45B9-AC4B-A84C81FE1261}";
        public const string StateId = "{102D776A-3113-49C4-BE9D-A12063791196}";
        public const string ZipCodeId = "{D3624AAB-52DA-4B99-BFE0-95C5EBDC573B}";
        public const string CountryId = "{E6D521AB-8AA6-4740-A6B9-22B4BFFD6DB2}";
        public const string EmailAddressId = "{6E3E7760-E0AB-4B80-9929-E3B1E1E535C4}";
        public const string PhoneNumberId = "{443FA6B9-2F74-4C06-B761-10345A82051A}";
        public const string AlternatePhoneNumberId = "{BCB555BB-66DB-410F-9129-4B049AD32DF3}";

        [SitecoreField(FieldName = FirstNameId)]
        public virtual string FirstNameLabel { get; set; }

        [SitecoreField(FieldName = LastNameId)]
        public virtual string LastNameLabel { get; set; }

        [SitecoreField(FieldName = AddressLine1Id)]
        public virtual string AddressLine1Label { get; set; }

        [SitecoreField(FieldName = AddressLine2Id)]
        public virtual string AddressLine2Label { get; set; }

        [SitecoreField(FieldName = AddressLine3Id)]
        public virtual string AddressLine3Label { get; set; }


        [SitecoreField(FieldName = CityId)]
        public virtual string CityLabel { get; set; }

        [SitecoreField(FieldName = StateId)]
        public virtual string StateLabel { get; set; }

        [SitecoreField(FieldName = ZipCodeId)]
        public virtual string ZipCodeLabel { get; set; }

        [SitecoreField(FieldName = CountryId)]
        public virtual string CountryLabel { get; set; }

        [SitecoreField(FieldName = EmailAddressId)]
        public virtual string EmailAddressLabel { get; set; }

        [SitecoreField(FieldName = PhoneNumberId)]
        public virtual string PhoneNumberLabel { get; set; }

        [SitecoreField(FieldName = AlternatePhoneNumberId)]
        public virtual string AlternatePhoneNumberLabel { get; set; }

        //Shared Address fields
        public string Address_LastName { get; set; }
        public string Address_FirstName { get; set; }
        public string Address_AddressLine1 { get; set; }
        public string Address_AddressLine2 { get; set; }
        public string Address_AddressLine3 { get; set; }
        public string Address_City { get; set; }
        public string Address_State { get; set; }
        public string Address_ZipCode { get; set; }
        public string Address_Country { get; set; }
        public bool isUS
        {
            get
            {
                if (string.IsNullOrEmpty(this.Address_Country))
                {
                    return false;
                }
                else
                {
                    return this.Address_Country.ToString().ToLower() == "us" || this.Address_Country.ToString().ToLower() == "united states" ? true : false;

                }
            }

        }
        public string Address_EmailAddress { get; set; }

        public string Address_PhoneNumber { get; set; }

        public string Address_AlternatePhoneNumber { get; set; }

    }
}