using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{
    public class SignInHelp: BasePage
    {
        [SitecoreField(FieldName = SitecoreReferenceSignInHelp.HelpQuestion)]
        public virtual string HelpQuestion { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceSignInHelp.HelpAnswer)]
        public virtual string HelpAnswer { get; set; }
    }
}