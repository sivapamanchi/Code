using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models
{

    [SitecoreType]
    public class ForgotPassword : BasePage
    {
        public const string IntroTextId = "{CC4723AF-D1CB-4898-BF4F-56F2A254E6CE}";

        [SitecoreField(FieldName = IntroTextId)]
        public virtual string IntroText { get; set; }


        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "Profile_EmailRequired")]
        [RegularExpressionTranslated(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "Profile_EmailFormatInvalid")]
        public string txtEmail { get; set; }


        [SitecoreIgnore]
        public bool noMatchFound { get; set; }


        [SitecoreIgnore]
        public bool isAccountLocked { get; set; }

        

    }
}