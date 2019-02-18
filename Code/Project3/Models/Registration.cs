using BGSitecore.Utils;
using BGSitecore.Validator;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models
{

    [SitecoreType(TemplateId = "{EFE90C81-A513-46E0-85BF-3385589FDDAD}")]
    public class Registration : BasePage
    {

        private const string IntroTextId = "{5DACDA6E-40F5-4012-AF18-50B9E42FED0A}";
        private const string InstructionsId = "{80286C4A-C268-4999-A41A-0E4927DCE08A}";
        private const string SSNCaptionId = "{E07FC8CF-858F-41FC-9B98-AEA2F99F7113}";
        private const string PhoneCaptionId = "{63E2FD11-11FA-42DE-AD91-7ED1903AF73B}";
        private const string EmailCaptionId = "{CCA76A38-C5AF-4508-9BA3-780289E03C43}";
        private const string PasswordCaptionId = "{C0AB5D8B-2CB4-4B34-B180-5B4B224DC816}";
        private const string FootnotesId = "{1B844113-3D8E-4412-9351-2197421D9B33}";
        private const string LookupTitleId = "{91FABD76-102B-413C-AD4A-4E6A23F94227}";
        private const string LookupSSNCaptionId = "{B5ABE95F-8F54-4E05-BC65-D50480028C57}";
        private const string LookupPhoneCaptionId = "{5149B9C6-FAE7-44D8-8AF4-0D7A01B7F5AE}";
        private const string UnsuccessMessageId = "{5B5D0D95-F0C2-4606-BF68-C2BA8FE708B2}";
        private const string SuccessPageId = "{30827B5C-4AD7-452F-8774-913C3972D679}";


        [SitecoreField(FieldName = IntroTextId)]
        public virtual string IntroText { get; set; }

        [SitecoreField(FieldName = InstructionsId)]
        public virtual string Instructions { get; set; }

        [SitecoreField(FieldName = SSNCaptionId)]
        public virtual string SSNCaption { get; set; }

        [SitecoreField(FieldName = PhoneCaptionId)]
        public virtual string PhoneCaption { get; set; }

        [SitecoreField(FieldName = EmailCaptionId)]
        public virtual string EmailCaption { get; set; }

        [SitecoreField(FieldName = PasswordCaptionId)]
        public virtual string PasswordCaption { get; set; }

        [SitecoreField(FieldName = FootnotesId)]
        public virtual string Footnotes { get; set; }

        [SitecoreField(FieldName = LookupTitleId)]
        public virtual string LookupTitle { get; set; }

        [SitecoreField(FieldName = LookupSSNCaptionId)]
        public virtual string LookupSSNCaption { get; set; }

        [SitecoreField(FieldName = LookupPhoneCaptionId)]
        public virtual string LookupPhoneCaption { get; set; }

        [SitecoreField(FieldName = UnsuccessMessageId)]
        [AllowHtml]
        public virtual string UnsuccessMessage { get; set; }

        [SitecoreField(FieldName = SuccessPageId, FieldType = SitecoreFieldType.GeneralLink)]
        [AllowHtml]
        public virtual Link SuccessPage { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "Profile_OwnerIdRequired")]
        public string txtOwnerId { get; set; }

        [GroupRequiredTranslated("txtSSN", "txtPhone", ErrorMessage = "Profile_SSNorPhoneRequired")]

        [SitecoreIgnore]
        [RegularExpressionTranslated(@"([0-9]{4})", ErrorMessage = "Profile_SSNNotNumeric")]
        public string txtSSN { get; set; }

        [SitecoreIgnore]
        [RegularExpressionTranslated(@"([0-9]+)", ErrorMessage = "Profile_PhoneNotNumeric")]
        public string txtPhone { get; set; }

        [SitecoreIgnore]
        public string txtLookupSSN { get; set; }

        [SitecoreIgnore]
        public string txtLookupPhone { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "Profile_EmailRequired")]
        [RegularExpressionTranslated(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "Profile_EmailRequired")]
        public string txtAcctEmail { get; set; }

        [SitecoreIgnore]
        [CompareTranslated("txtAcctEmail", ErrorMessage = "Profile_EmailDoNotMatch")]
        public string txtAcctEmail2 { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "Profile_PasswordRequired")]
        [RegularExpressionTranslated(LoginUtils.PasswordValidationRegEx, ErrorMessage = "Profile_PasswordInvalid")]
        [AllowHtml]
        public string txtPassword { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "Profile_ConfirmPasswordRequired")]
        [CompareTranslated("txtPassword", ErrorMessage = "Profile_PasswordDoNotMatch")]
        [AllowHtml]
        public string txtAcctPassword2 { get; set; }

        [SitecoreIgnore]
        public string btnSubmit { get; set; }

        [SitecoreIgnore]
        public string PostbackSuccessPageUrl { get; set; }

    }
}