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
    public class ChangePassword : BasePage
    {

        public const string IntroTextSelfServiceId = "{98086976-B548-4961-AC35-9F08B4FDBC8E}";
        public const string IntroTextPolicyFailId = "{ED93F608-8D18-4214-AB09-B51F641BF1EE}";
        public const string CurrentPasswordCaptionId = "{C72C0535-4C57-44CF-B574-125CA1282FAF}";
        public const string NewPasswordCaptionId = "{3C7A79E9-09A2-4D6B-8DD3-D000CE213E07}";
        public const string ConfirmPasswordCaptionId = "{0CCCC78F-B827-4F44-B8B5-7E14FA53972F}";
        public const string CancelTextId = "{8DED3558-7E4C-4DA2-923A-5217AEF55953}";
        public const string SaveCaptionId = "{56243895-C986-4EDC-8D56-86537ED3C451}";
        public const string PasswordUpdatedMsgId = "{2107159C-6A87-4FB2-9627-6C35BCFFD1CF}";


        [SitecoreField(FieldName = IntroTextSelfServiceId)]
        public virtual string IntroTextSelfService { get; set; }


        [SitecoreField(FieldName = IntroTextPolicyFailId)]
        public virtual string IntroTextPolicyFail { get; set; }

        [SitecoreField(FieldName = CurrentPasswordCaptionId)]
        public virtual string CurrentPasswordCaption { get; set; }

        [SitecoreField(FieldName = NewPasswordCaptionId)]
        public virtual string NewPasswordCaption { get; set; }

        [SitecoreField(FieldName = ConfirmPasswordCaptionId)]
        public virtual string ConfirmPasswordCaption { get; set; }

        [SitecoreField(FieldName = CancelTextId)]
        public virtual string CancelText { get; set; }

        [SitecoreField(FieldName = SaveCaptionId)]
        public virtual string SaveCaption { get; set; }

        [SitecoreField(FieldName = PasswordUpdatedMsgId)]
        public virtual string PasswordUpdatedMsg { get; set; }

        [SitecoreIgnore]
        public bool isPasswordPolicyFail { get; set; }

        [SitecoreIgnore]
        public bool isShowPasswordUpdated { get; set; }

        [SitecoreIgnore]
        public bool isAccountLocked { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "ChangePassword_Current_Empty")]
        [AllowHtml]
        public string txtCurrentPassword { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "ChangePassword_New_Empty")]
        [RegularExpressionTranslated(LoginUtils.PasswordValidationRegEx, ErrorMessage = "ChangePassword_New_Invalid")]
        [AllowHtml]
        public string txtNewPassword { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "profile_changePasswordConfirmEmpty")]
        [CompareTranslated("txtNewPassword", ErrorMessage = "Profile_PasswordDoNotMatch")]
        [AllowHtml]
        public string txtConfirmPassword { get; set; }

        [SitecoreIgnore]
        public string btnSkip { get; set; }
    }
}