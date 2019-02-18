using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BGSitecore.Models
{
    [SitecoreType]
    public class SignIn: BasePage
    {
        public const string RegistrationHeaderId = "{BB2DF7F0-A7C7-4C85-9365-EDDF58C754E9}";
        public const string RegistrationUpperTextId = "{08D8543D-B217-4406-A351-A4E59E9EFD83}";
        public const string RegistrationLowerTextId = "{2B96B740-0A11-4103-BE7F-3E66A89F2B8F}";
        public const string MedalliaPopupIdId = "{92DADE37-7A4D-4DB2-9456-37C8483594AD}";
        public const string RandomImageLocationId= "{861041A1-CED4-4BA2-97AE-5928AA83E1A9}";

        [SitecoreField(FieldName = RegistrationHeaderId )]
        public virtual string RegistrationHeader { get; set; }

        [SitecoreField(FieldName = RegistrationUpperTextId)]
        public virtual string RegistrationUpperText { get; set; }

        [SitecoreField(FieldName = RegistrationLowerTextId)]
        public virtual string RegistrationLowerText { get; set; }

        [SitecoreField(FieldName = MedalliaPopupIdId)]
        public virtual string MedalliaPopup { get; set; }

        [SitecoreField(FieldName = RandomImageLocationId)]
        public virtual IEnumerable<Image> RandomImageLocation { get; set; }

        [SitecoreChildren]
        public virtual IEnumerable<Image> Children { get; set; }

        [SitecoreIgnore]
        public string txtEmail { get; set; }

        [SitecoreIgnore]
        [AllowHtml]
        public string txtPassword { get; set; }

        public bool noAckError { get; set; }
        public string IsTutorialTransfer { get; set; }
        public string IsTravelerPlusLogin { get; set; }
        public string IsEncoreRewardsLogin { get; set; }
        public string AgentLoginID { get; set; }
        public string OwnerId { get; set; }
        public string OwnerType { get; set; }
        public string TPStatus { get; set; }
        public string sMessage { get; set; }
        public int SurveyPercent { get; set; }

    }
}