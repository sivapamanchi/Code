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
  
    public class CreditCardInfo
    {
        public const string RequiredFieldsId = "{1152E7CE-BA7B-413A-A1CF-2D6523BA2540}";
        [SitecoreField(FieldName = RequiredFieldsId)]
        public virtual string RequiredFields { get; set; }

        public const string CreditCardInstructionsId = "{ED500BBF-F24F-4B87-B705-CCDEA33D5C4B}";
        [SitecoreField(FieldName = CreditCardInstructionsId)]
        public virtual string CreditCardInstructions { get; set; }

        public const string CVVInstructionsId = "{97B4B5C5-AF15-47D2-84FA-84B0B897FB84}";
        [SitecoreField(FieldName = CVVInstructionsId)]
        public virtual string CVVInstructions { get; set; }

   
        public const string BillingNameId = "{D6548FB8-6A78-483E-B7BC-2AF8E21E2009}";
        [SitecoreField(FieldName = BillingNameId)]
        // [RequiredTranslated(ErrorMessage = "Profile_BillingNameRequired")] TFS-62805
        public virtual string BillingName { get; set; }

        public const string CardNumberId = "{8594F886-B822-4EB1-A36C-F409480FFB8A}";
        [SitecoreField(FieldName = CardNumberId)]
        // [RequiredTranslated(ErrorMessage = "Profile_BillingCardNumberRequired")] TFS-62805
        public virtual string CardNumber { get; set; }

        public const string CVVId = "{6BAD2B11-5371-459B-B1B9-45109E046FFB}";
        [SitecoreField(FieldName = CVVId)]
        // [RequiredTranslated(ErrorMessage = "Profile_BillingCvvNumberRequired")] TFS-62805
        public virtual string CVV { get; set; }

        public const string CVVLinkId = "{49F35688-EF78-4790-B43F-294018D2AC87}";
        [SitecoreField(FieldName = CVVLinkId)]
        public virtual string CVVLink { get; set; }

        public const string ExpirationMonthId = "{96D0283F-28E5-4A10-86D5-B97C8368F4F4}";
        [SitecoreField(FieldName = ExpirationMonthId)]
        [RequiredTranslated(ErrorMessage = "Profile_GenericRequired")]
        public virtual string ExpirationMonth { get; set; }

        public const string ExpirationYearId = "{8AE122FC-A782-43AD-83D9-37D327ADB271}";
        [SitecoreField(FieldName = ExpirationYearId)]
        [RequiredTranslated(ErrorMessage = "Profile_GenericRequired")]
        public virtual string ExpirationYear { get; set; }

        public const string PostalCodeId = "{EF4C4AB3-CC3B-4422-8CD5-5A9F06833D4A}";
        [SitecoreField(FieldName = PostalCodeId)]
        // [RequiredTranslated(ErrorMessage = "Profile_BillingZipCodeRequired")] TFS-62805
        public virtual string PostalCode { get; set; }

        public const string PostalCodeNoteId = "{F8216565-1D6F-47A8-8444-E02750D6FCFE}";
        [SitecoreField(FieldName = PostalCodeNoteId)]
        public virtual string PostalCodeNote { get; set; }

        //Credit Card fields
        public string CreditCard_Name { get; set; }
        public string CreditCard_Number { get; set; }
        public string CreditCard_Type { get; set; }
        public string CreditCard_ExpDateMonth { get; set; }
        public string CreditCard_ExpDateYear { get; set; }
        public string CreditCard_VerificationNumber { get; set; }
        public string CreditCard_ZipCode { get; set; }
        public bool CreditCard_InternationalZipCode { get; set; }
    }
}