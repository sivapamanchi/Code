using BGSitecore.Utils;
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

    [SitecoreType]
    public class ResetPassword : BasePage
    {
        public const string IntroTextId = "{DCE19854-C589-4916-89DC-E7CD018133B6}";
        public const string NewPasswordCaptionId = "{C20DBB50-0140-41FB-A3D3-F9F4693ED0BF}";
        public const string ConfirmPasswordCaptionId = "{1AAE9495-9D1E-4F7C-AD46-45E02C11C335}";
        public const string SaveCaptionId = "{0417E1CA-4195-4265-89B6-6BF17EAC064D}";
        public const string MessageLinkExpiredId = "{F1E28A86-F0A7-49FA-9AD6-3FB7DA9788B6}";
        public const string MessageLinkInvalidId = "{9E7F3049-43B3-4551-8C6B-224E053FAD26}";


        [SitecoreField(FieldName = IntroTextId)]
       public virtual string IntroText { get; set; }

        [SitecoreField(FieldName = NewPasswordCaptionId)]
        public virtual string NewPasswordCaption { get; set; }

        [SitecoreField(FieldName = ConfirmPasswordCaptionId)]
        public virtual string ConfirmPasswordCaption { get; set; }

        [SitecoreField(FieldName = SaveCaptionId)]
        public virtual string SaveCaption { get; set; }

        [SitecoreField(FieldName = MessageLinkExpiredId)]
        public virtual string MessageLinkExpired { get; set; }

        [SitecoreField(FieldName = MessageLinkInvalidId)]
        public virtual string MessageLinkInvalid { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "ChangePassword_New_Empty")]
        [RegularExpressionTranslated(LoginUtils.PasswordValidationRegEx, ErrorMessage = "Profile_PasswordInvalid")]
        [AllowHtml]
        public string txtNewPassword { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "profile_changePasswordConfirmEmpty")]
        [CompareTranslated("txtNewPassword", ErrorMessage = "Profile_PasswordDoNotMatch")]
        [AllowHtml]
        public string txtConfirmPassword { get; set; }

        public  string email { get; set; }
        public  Guid resetid { get; set; }
        
        public bool hideUIElement { get; set; }

    }
}