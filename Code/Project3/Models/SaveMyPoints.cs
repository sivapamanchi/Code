using Glass.Mapper.Sc.Configuration.Attributes;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models
{
    public class SaveMyPoints : BasePage
    {
        public const string LegendId = "{ACE8A6E6-5C25-4536-AE4F-3B7BC978B84D}";
        [SitecoreField(FieldName = LegendId)]
        public virtual string Legend { get; set; }

        public const string InstructionalTextId = "{6FD58C65-E596-421F-9468-4182608F8158}";
        [SitecoreField(FieldName = InstructionalTextId)]
        public virtual string InstructionalText { get; set; }

        public const string RequiredFieldsId = "{99904133-EC5F-4B9B-A166-CF1634512422}";
        [SitecoreField(FieldName = RequiredFieldsId)]
        public virtual string RequiredFields { get; set; }

        public const string CreditCardInstructionsId = "{BD5D6484-F2EC-4EC4-9E22-2F4313AF73F3}";
        [SitecoreField(FieldName = CreditCardInstructionsId)]
        public virtual string CreditCardInstructions { get; set; }

        public const string CVVInstructionsId = "{B840F2B1-F4C5-496D-A249-1CC35F7ED187}";
        [SitecoreField(FieldName = CVVInstructionsId)]
        public virtual string CVVInstructions { get; set; }

        public const string PrivacyPolicyId = "{374B0C9B-4B60-4A2A-A0E5-561B55E583AE}";
        [SitecoreField(FieldName = PrivacyPolicyId)]
        public virtual string PrivacyPolicy { get; set; }

        public const string SecureTransmissionTextId = "{E7476FE8-363C-4020-AD0D-42CF21329ACB}";
        [SitecoreField(FieldName = SecureTransmissionTextId)]
        public virtual string SecureTransmissionText { get; set; }

        public const string ExceptTextId = "{CA9C2F55-4137-4C10-AB57-A8BC543044BC}";
        [SitecoreField(FieldName = ExceptTextId)]
        public virtual string ExceptText { get; set; }

        public const string TermsAndConditionsId = "{C00E4128-286C-4F0A-A051-79651F543486}";
        [SitecoreField(FieldName = TermsAndConditionsId)]
        [RequiredTranslated(ErrorMessage = "Profile_GenericRequired")]
        public virtual string TermsAndConditions { get; set; }

        public const string SaveMyPointsBtnId = "{A3F5BAC6-F20C-46C0-A654-EF5A7DCF4DDF}";
        [SitecoreField(FieldName = SaveMyPointsBtnId)]
        public virtual string SaveMyPointsBtn { get; set; }

        public const string NoThankYouBtnId = "{6AA9778F-CAF4-4092-848C-E21BEC58551D}";
        [SitecoreField(FieldName = NoThankYouBtnId)]
        public virtual string NoThankYouBtn { get; set; }

        public const string TermsAndConditionsAlertId = "{DB641A03-1BAE-40C6-B612-43C3CF3E6637}";
        [SitecoreField(FieldName = TermsAndConditionsAlertId)]
        public virtual string TermsAndConditionsAlert { get; set; }

        public const string SaveMyPointsConfirmationMessageid = "{BF334F3E-5367-49EB-9081-29307D0358E3}";
        [SitecoreField(FieldName = SaveMyPointsConfirmationMessageid)]
        public virtual string SaveMyPointsConfirmationMessage { get; set; }

        public const string TotalPaymentId = "{1CA0C23B-B88D-4161-9714-7B560BCE768D}";
        [SitecoreField(FieldName = TotalPaymentId)]
        public virtual string TotalPayment { get; set; }
    }
}