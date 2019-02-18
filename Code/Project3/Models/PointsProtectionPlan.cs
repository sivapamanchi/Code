using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models
{
    public class PointsProtectionPlan : BasePage
    {
        public const string InstructionalTextId = "{DA6D64BC-1A83-48D7-BC82-91124A717FAA}";
        [SitecoreField(FieldName = InstructionalTextId)]
        public virtual string InstructionalText { get; set; }

        public const string RequiredFieldsId = "{D2D665AF-522C-4BB0-85F0-0E2D9504818B}";
        [SitecoreField(FieldName = RequiredFieldsId)]
        public virtual string RequiredFields { get; set; }

        public const string PrivacyPolicyId = "{18D8BB65-20FF-4E3E-BD07-2A664BD18A23}";
        [SitecoreField(FieldName = PrivacyPolicyId)]
        public virtual string PrivacyPolicy { get; set; }

        public const string SecureTransmissionTextId = "{394BF27E-B214-4CDC-9B32-C9664C18E783}";
        [SitecoreField(FieldName = SecureTransmissionTextId)]
        public virtual string SecureTransmissionText { get; set; }

        public const string ExceptTextId = "{7EEBA87E-164D-42EE-A239-A02B6A28F3EE}";
        [SitecoreField(FieldName = ExceptTextId)]
        public virtual string ExceptText { get; set; }

        public const string BillingInformationId = "{6C62016F-B480-44C6-9610-2D3C6C7177BC}";
        [SitecoreField(FieldName = BillingInformationId)]
        public virtual string BillingInformation { get; set; }

        public const string PointsProtectedId = "{961DFC6C-3C07-4843-9E19-FC455ACB9A5A}";
        [SitecoreField(FieldName = PointsProtectedId)]
        public virtual string PointsProtected { get; set; }

        public const string TotalPaymentId = "{5260793A-60EA-48EA-B6F5-944B5DACE6FC}";
        [SitecoreField(FieldName = TotalPaymentId)]
        public virtual string TotalPayment { get; set; }

        public const string CardTypeId = "{1D973240-7ECF-4803-917D-DB17A5C862F9}";
        [SitecoreField(FieldName = CardTypeId)]
        public virtual string CardType { get; set; }

        public const string TermsAndConditionsId = "{D6FDA384-D2DE-4430-93BB-A806AC27A866}";
        [SitecoreField(FieldName = TermsAndConditionsId)]
        [RequiredTranslated(ErrorMessage = "Profile_GenericRequired")]
        public virtual string TermsAndConditions { get; set; }

        public const string ProtectMyPointsId = "{A2B975DC-F770-46D5-B9FD-D17FFA26B7D2}";
        [SitecoreField(FieldName = ProtectMyPointsId)]
        public virtual string ProtectMyPoints { get; set; }

        public const string NoThankYouId = "{3012122D-96F3-4B76-A7D9-E37556AC1D71}";
        [SitecoreField(FieldName = NoThankYouId)]
        public virtual string NoThankYou { get; set; }

        public const string CancellationsId = "{F6C0A874-8AC9-4B41-BBA0-E6490B220733}";
        [SitecoreField(FieldName = CancellationsId)]
        public virtual string Cancellations { get; set; }

        public const string ProtectMyPointsNowId = "{059FE100-1B9D-494C-9F1D-28125073F226}";
        [SitecoreField(FieldName = ProtectMyPointsNowId)]
        public virtual string ProtectMyPointsNow { get; set; }

        public const string NotInterestedId = "{CEBFC825-CDE5-4ED2-A8E9-2C621BF9C36F}";
        [SitecoreField(FieldName = NotInterestedId)]
        public virtual string NotInterested { get; set; }

        public const string TermsAndConditionsAlertId = "{CF4EB995-631C-4993-BD19-5AA14B2674D4}";
        [SitecoreField(FieldName = TermsAndConditionsAlertId)]
        public virtual string TermsAndConditionsAlert { get; set; }

        [SitecoreQuery("./*", IsRelative = true)]
        public virtual IEnumerable<Testimonial> Testimonials { get; set; }

        [SitecoreIgnore]
        public decimal points { get; set; }

        [SitecoreIgnore]
        public string payment { get; set; }


    }
}