using static BGSitecore.Validator.TranslatedValidator;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace BGSitecore.Models
{
    public class ContactInfo: BasePage
    {
        public const string OwnerNumberId = "{D9B1B6A4-4712-40DE-8E0D-0AED3B920F7E}";
        public const string FirstNameId = "{DBC8B832-1140-4517-9D0B-302C0455A13B}";
        public const string LastNameId = "{40D87638-EE71-40A2-AB16-3539F44DD69D}";
        public const string AddressLine1Id = "{BA4BC0DD-F8AD-42E7-ADE0-E51A9E6D3DD8}";
        public const string AddressLine2Id = "{AF01A47F-B8A3-4F29-98A5-7C251F3FD89F}";
        public const string AddressLine3Id = "{9CCA8B23-8433-4AFB-AE57-9383AD3CA857}";
        public const string CityId = "{7E189C9A-6CF8-4578-BBB0-76C60D6E410C}";
        public const string StateId = "{14742272-3CDC-432B-9905-710809E31E86}";
        public const string ZipCodeId = "{50FDFA6C-120C-4FCA-BC11-6714AE6A2321}";
        public const string CountryId = "{3F62C0BC-B704-4427-8635-10ADF3EDE24D}";        
        public const string HomePhoneId = "{CBE955C3-AF25-4AD1-AB3C-2C7873144D85}";
        public const string AlternatePhoneNumberId = "{6ADD2AF9-FE09-4534-AF08-D232BE67C4CC}";
        public const string EmailAddressId = "{B4980286-68ED-4077-871D-0612830A55A9}";

        [SitecoreField(FieldName = OwnerNumberId)]
        public virtual string OwnerNumberLabel { get; set; }

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

        [SitecoreField(FieldName = HomePhoneId)]
        public virtual string HomePhoneLabel { get; set; }

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

        [SitecoreIgnore]
        public string Address_EmailAddress { get; set; }

        [SitecoreIgnore]
       
        public string Address_HomePhone { get; set; }

        [SitecoreIgnore]
        public string Address_AlternatePhoneNumber { get; set; }

        public string Address_OwnerNumber { get; set; }

        public ContactInfo SuggestedAddress { get;}

        
    }
}