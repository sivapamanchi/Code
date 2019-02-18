using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models
{
    [Serializable]
    public class MyProfileResponseParameters
    {

        public MyProfileResponseParameters ShallowCopy()
        {
            return (MyProfileResponseParameters)this.MemberwiseClone();
        }

        public string Address_FirstName_disabled { get; set; }
        public string Address_LastName_disabled { get; set; }
        public string Address_OwnerNumber_disabled { get; set; }      
        public string Address_FirstName
        {
            get
            {
                return this.Address_FirstName_disabled;
            }
            set
            {
                this.Address_FirstName_disabled = value;
            }
        }

        public string Address_LastName
        {
            get
            {
                return this.Address_LastName_disabled;
            }
            set
            {
                this.Address_LastName_disabled = value;
            }
        }

        public string Address_AddressLine1 { get; set; } = string.Empty;
        public string Address_AddressLine2 { get; set; } = string.Empty;
        public string Address_AddressLine3 { get; set; } = string.Empty;
        public string Address_City { get; set; } = string.Empty;
        public string Address_State { get; set; } = string.Empty;
        public string Address_ZipCode { get; set; } = string.Empty;
        public string Address_Country { get; set; } = string.Empty;

        [SitecoreIgnore]
        public string Address_EmailAddress { get; set; }
        [SitecoreIgnore]       
        public string Address_HomePhone { get; set; }
        [SitecoreIgnore]
        public string Address_AlternatePhoneNumber { get; set; }
        public string Address_OwnerNumber
        {
            get
            {
                return this.Address_OwnerNumber_disabled;
            }
            set
            {
                this.Address_OwnerNumber_disabled = value;
            }
        }


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
        public bool isPaperLessSelected { get; set; }

        public string correct_address { get; set; }

        public string successPageUrl { get; set; }
    }
}