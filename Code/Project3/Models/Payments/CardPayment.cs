using BGSitecore.Models.Common;
using BGSitecore.Models.Interface;
using Glass.Mapper.Sc.Configuration.Attributes;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models.Payments
{
    [SitecoreType(AutoMap = true)]
    public class CardPayment : BasePage, IPayment
    {
        #region General Items

        public const string RequiredFieldsId = "{1061BB47-77CF-471E-836E-F54BB531B939}";
        [SitecoreField(FieldName = RequiredFieldsId)]
        public virtual string RequiredFields { get; set; }

        public const string CreditCardInstructionsId = "{6D0F1341-3F0B-44CF-AC6B-B1355796449E}";
        [SitecoreField(FieldName = CreditCardInstructionsId)]
        public virtual string CreditCardInstructions { get; set; }

        public const string CVVInstructionsId = "{71CB831C-2C1E-43AE-A3CA-93C07B5B1011}";
        [SitecoreField(FieldName = CVVInstructionsId)]
        public virtual string CVVInstructions { get; set; }

        public const string InternationalPostalCodeId = "{9BD432BC-5937-4AF7-AFC1-554220FB547D}";
        [SitecoreField(FieldName = InternationalPostalCodeId)]
        public virtual string InternationalPostalCode { get; set; }

        public const string SecureTransmissionTextId = "{8AA8B22C-FEC4-464D-83AD-94ED34E9A2AC}";
        [SitecoreField(FieldName = SecureTransmissionTextId)]
        public virtual string SecureTransmissionText { get; set; }

        public const string TermsAndConditionsId = "{0098CEC7-978B-45AB-BBF8-C57D7BF95C0E}";
        [SitecoreField(FieldName = TermsAndConditionsId)]
        [RequiredTranslated(ErrorMessage = "Profile_GenericRequired")]
        public virtual string TermsAndConditions { get; set; }

        public const string ButtonSubmitId = "{81895818-2CC1-46FA-8B4B-444C20D3EB7E}";
        [SitecoreField(FieldName = ButtonSubmitId)]
        public virtual string ButtonSubmit { get; set; }

        public const string SubmitButtonHintTextId = "{3D3C392D-F0B0-4B5B-A9B9-9E49FDB7F3ED}";
        [SitecoreField(FieldName = SubmitButtonHintTextId)]
        public virtual string SubmitButtonHintText { get; set; }

        public const string TermsAndConditionsAlertId = "{5E1FF4CF-28DB-4CEE-9644-63689FE9448C}";
        [SitecoreField(FieldName = TermsAndConditionsAlertId)]
        public virtual string TermsAndConditionsAlert { get; set; }

        public const string TotalPaymentId = "{8C296406-5660-4972-8C09-290039E6B978}";
        [SitecoreField(FieldName = TotalPaymentId)]
        public virtual string TotalPayment { get; set; }

        public const string CardTypeId = "{C38715B0-5704-4046-903A-B8B5B0C055D7}";
        [SitecoreField(FieldName = CardTypeId)]
        public virtual string CardType { get; set; }

        #endregion

        #region Credit Card Form Items

        public const string NameOnCardId = "{CEEAB05D-D677-40CF-B929-999102CDBED8}";
        [SitecoreField(FieldName = NameOnCardId)]
        public virtual string NameOnCard { get; set; }

        public const string CardNumberId = "{0D6EB8E6-6EA5-41C7-9EB9-352CBD1C0341}";
        [SitecoreField(FieldName = CardNumberId)]
        public virtual string CardNumber { get; set; }

        public const string CVVId = "{2DD36E49-22A6-43E1-9541-DA17C284AA19}";
        [SitecoreField(FieldName = CVVId)]
        public virtual string CVV { get; set; }

        public const string CVVLinkId = "{D5A920E0-A41D-4A4B-9A57-E844E532E4F9}";
        [SitecoreField(FieldName = CVVLinkId)]
        public virtual string CVVLink { get; set; }

        public const string ExpirationMonthId = "{389E4C81-53C9-4720-B54C-DC6227D407B8}";
        [SitecoreField(FieldName = ExpirationMonthId)]
        public virtual string ExpirationMonth { get; set; }

        public const string ExpirationYearId = "{CB7B6B90-956E-4717-A6D6-8FF7FAD7692E}";
        [SitecoreField(FieldName = ExpirationYearId)]
        public virtual string ExpirationYear { get; set; }

        public const string ExpirationDateId = "{8C37C8F6-C558-419C-9025-F5C39BA5FF07}";
        [SitecoreField(FieldName = ExpirationDateId)]
        public virtual string ExpirationDate { get; set; }

        public const string PostalCodeId = "{CD9B811F-65BA-48D6-AC56-93D2F5A2A733}";
        [SitecoreField(FieldName = PostalCodeId)]
        public virtual string PostalCode { get; set; }

        #endregion

        [SitecoreIgnore]
       // [RequiredTranslated(ErrorMessage ="CreditCard_Name_Required")]
        public string txtNameOnTheCard { get; set; }

        [SitecoreIgnore]
        //[RequiredTranslated(ErrorMessage = "CreditCard_Number_Required")]
        //[CreditCardTranslated(ErrorMessage ="Credit_Card_Number_Incorrect")]
        public string txtCardNumber { get; set; }

        [SitecoreIgnore]
        //[RequiredTranslated(ErrorMessage = "CreditCard_Type_Required")]
        public string txtCardType { get; set; }

        [SitecoreIgnore]
        //[RequiredTranslated(ErrorMessage ="CreditCard_Expiration_Required")]
        //[RegularExpressionTranslated("",ErrorMessage = "CreditCard_Expiration_Incorrect")]
        public string txtCardExpMonthYear { get; set; }


        [SitecoreIgnore]
        //[RequiredTranslated(ErrorMessage = "CreditCard_CVV_Required")]
        //[MaxLengthTranslated(3,ErrorMessage = "CreditCard_CVV_Incorrect")]
        //[MinLengthTranslated(3, ErrorMessage = "CreditCard_CVV_Incorrect")]
        public string txtCVV { get; set; }

        [SitecoreIgnore]
        //[RequiredIfTranslated("IsInternationalZipCode",false,ErrorMessage ="CreditCard_Zip_Code")]
        public string txtZipCode { get; set; }

        [SitecoreIgnore]
        public bool txtIsInternationalZipCode { get; set; }

        [SitecoreIgnore]
        //[RequiredTranslated(ErrorMessage = "Amount_Required")]
        public decimal Amount { get; set; }

        [SitecoreIgnore]
        //[RequiredTranslated(ErrorMessage = "Amount_Required")]
        public string AmountString { get; set; }

        [SitecoreIgnore]
        public string AuthCode { get; set; }

        [SitecoreIgnore]
        public string MerchantID { get; set; }

        [SitecoreIgnore]
        public int LogID { get; set; }

        [SitecoreIgnore]
        public string ERRDETAIL { get; set; }

        [SitecoreIgnore]
        public Error Error { get; set; }

    }   
}