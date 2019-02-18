using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models
{

    public class BonusTimeReservation : BasePage
    {
        public const string DailyRateLabelId = "{65CCBF4F-B3A2-4689-8E00-469C99B884C7}";
        public const string LocalItemPerDayLabelId = "{87E064F9-45F5-4073-9BFE-25FE296EEB6D}";
        public const string TotalPaymentLabelId = "{627BAF42-E249-4CEE-8FEF-351F446DD114}";
        public const string ImportantNotesTitleId = "{3D53B5C9-B93B-4FC9-8FBC-91B5FBF6B7D3}";
        public const string ReservationTitleLabelId = "{BD4BA280-0914-4A9E-B08E-B04F5D679D05}";
        public const string OwnerLabelId = "{5CB5429F-CBC2-4143-B17C-21D358154907}";
        public const string SpecialRequestLabelId = "{CB2B7327-8D8B-4378-A895-1F7FC5E0904B}";
        public const string SpecialRequestInstructionId = "{168C7222-3073-4EC0-A809-17D9C9DE1090}";
        public const string BillingInformationTitleId = "{B610F2CF-A3EC-4DF6-8346-421C1696890A}";
        public const string PaymentInformationTitleId = "{A7A31D3F-125E-4813-BC81-7B1E89241E12}";
        public const string PrivacyPolicymessageId = "{1FEAA58C-A50B-4E78-B454-2C9F01376236}";
        public const string TermsAndConditionsMessageId = "{D8E980B7-FF16-4A16-B9E4-5961E03CBFC5}";
        public const string TermsAndConditionsAlertId = "{41F21E6E-AEED-4A6F-A088-1530A5252972}";
        public const string ConfirmReservationButtonId = "{E7B403FA-7EC5-4228-B56C-CD0F58BA2BB3}";
        public const string FooterMessageId = "{5DF85C2C-EA5E-4C66-8D49-C1A1C34937A5}";
        public const string RequiredLabelId = "{AA3AD2B2-72C1-4810-9807-F5E43607095F}";
        public const string FootnoteId = "{869B54F2-30DE-4469-9EBF-8AF01C153C16}";


        [SitecoreField(FieldName = DailyRateLabelId)]
        public virtual string DailyRateLabel { get; set; }

        [SitecoreField(FieldName = LocalItemPerDayLabelId)]
        public virtual string LocalItemPerDayLabel { get; set; }

        [SitecoreField(FieldName = TotalPaymentLabelId)]
        public virtual string TotalPaymentLabel { get; set; }

        [SitecoreField(FieldName = ImportantNotesTitleId)]
        public virtual string ImportantNotesTitle { get; set; }

        [SitecoreField(FieldName = ReservationTitleLabelId)]
        public virtual string ReservationTitleLabel { get; set; }

        [SitecoreField(FieldName = OwnerLabelId)]
        public virtual string OwnerLabel { get; set; }

        [SitecoreField(FieldName = SpecialRequestLabelId)]
        public virtual string SpecialRequestLabel { get; set; }

        [SitecoreField(FieldName = SpecialRequestInstructionId)]
        public virtual string SpecialRequestInstruction { get; set; }

        [SitecoreField(FieldName = BillingInformationTitleId)]
        public virtual string BillingInformationTitle { get; set; }

        [SitecoreField(FieldName = PaymentInformationTitleId)]
        public virtual string PaymentInformationTitle { get; set; }

        [SitecoreField(FieldName = PrivacyPolicymessageId)]
        public virtual string PrivacyPolicymessage { get; set; }

        [SitecoreField(FieldName = TermsAndConditionsMessageId)]
        public virtual string TermsAndConditionsMessage { get; set; }

        [SitecoreField(FieldName = TermsAndConditionsAlertId)]
        public virtual string TermsAndConditionsAlert { get; set; }

        [SitecoreField(FieldName = ConfirmReservationButtonId)]
        public virtual string ConfirmReservationButton { get; set; }

        [SitecoreField(FieldName = FooterMessageId)]
        public virtual string FooterMessage { get; set; }

        [SitecoreField(FieldName = RequiredLabelId)]
        public virtual string RequiredLabel { get; set; }

        [SitecoreField(FieldName = FootnoteId)]
        public virtual string Footnote { get; set; }


        ////Guest info fields
        //public string Text_NumberOfGuest { get; set; }
        //public string Text_GuestNewFirstName { get; set; }
        //public string Text_GuestNewLastName { get; set; }
        //public string Text_GuestNewEmail { get; set; }
        //public string Text_GuestNewPhoneNumber { get; set; }
        //public string Text_GuestNewCity { get; set; }
        //public string Text_GuestNewState { get; set; }
        //public string Text_GuestNewRelationship { get; set; }
        //public string Text_GuestSelected { get; set; }
        public string text_SpecialRequests { get; set; }

        ////Address Info Fields
        //public string Text_LastName { get; set; }
        //public string Text_FirstName { get; set; }
        //public string Text_AddressLIne1 { get; set; }
        //public string Text_City { get; set; }
        //public string Text_State { get; set; }
        //public string Text_ZipCode { get; set; }
        //public string Text_Country { get; set; }
        //public string Text_EmailAddress { get; set; }
        //public string Text_PhoneNumber { get; set; }
        //public string Text_AlternatePhoneNumber { get; set; }

        ////Credit card fields
        //public List<string> CreditCardInfoErrors { get; set; }
        //public string CreditCardName { get; set; }
        //public string CreditCardNumber { get; set; }
        //public string CreditCardType { get; set; }
        //public string CreditCardExpDateMonth { get; set; }
        //public string CreditCardExpDateYear { get; set; }
        //public string CreditCardVerificationNumber { get; set; }
        //public string CreditCardZipCode { get; set; }
        //public bool InternationalZipCode { get; set; }
    }
}