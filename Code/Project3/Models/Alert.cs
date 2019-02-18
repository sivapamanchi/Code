using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models;
using Glass.Mapper.Sc.Fields;
using Glass.Mapper.Sc.Configuration;

namespace BGSitecore.Models
{
    [SitecoreType]
    public class Alert : BaseComponent
    {

        public const string AlertDisplayId = "{948E2D8F-7845-407C-9220-4EACE007B75A}";
        public const string AlertTextId = "{3B606C60-64AA-4455-A0C4-D91F26679C7B}";
        public const string DismissibleId = "{9DBD7EC4-B12E-4C7D-AB60-3996855DCA29}";
        public const string ShowButtonId = "{072DA9BE-30E8-4183-B958-AC66CC134531}";
        public const string ButtonTextId = "{52C878B4-2033-4844-A864-FFC1B29F3C29}";
        public const string ButtonLinkId = "{29B69467-492A-4216-BADD-903F6A180E93}";
        public const string AlertColorId = "{A120599D-A55D-46DE-9324-C268BA70AC2A}";
        public const string AlertRulesId = "{C3944387-46F4-47F8-9F58-FDF715602274}";
        public const string ShowInlineFormId = "{B1E46A5D-37DC-45A0-9CEC-7DD0F2896405}";
        public const string CustomIDItemId = "{8301EFA4-3A28-4BCF-BD17-2A7B2F592560}";
        public const string SecondaryButtonID = "{DDB11C46-510A-4B43-A9EE-F394C005CF2E}";
        public const string SecondaryTextID = "{9C0BEDF7-D6C0-4E14-AD64-C28EBB877EAB}";

        [SitecoreField(FieldName = AlertDisplayId)]
        public virtual bool AlertDisplay { get; set; }

        [SitecoreField(FieldName = AlertTextId)]
        public virtual string AlertText { get; set; }

        [SitecoreField(FieldName = DismissibleId)]
        public virtual bool Dismissible { get; set; }

        [SitecoreField(FieldName = ShowButtonId)]
        public virtual bool ShowButton { get; set; }

        [SitecoreField(FieldName = ButtonTextId)]
        public virtual string ButtonText { get; set; }

        [SitecoreField(FieldName = ButtonLinkId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link ButtonLink { get; set; }

        [SitecoreField(FieldName = SecondaryTextID)]
        public virtual string SecondaryButtonText { get; set; }

        [SitecoreField(FieldName = SecondaryButtonID, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link SecondaryButtonLink { get; set; }

        [SitecoreField(FieldName = AlertColorId)]
        public virtual AlertType AlertType { get; set; }

        [SitecoreField(FieldName = AlertRulesId)]
        public virtual string AlertRules { get; set; }

        [SitecoreField(FieldName = ShowInlineFormId)]
        public virtual bool ShowInlineForm { get; set; }

        [SitecoreField(FieldName = CustomIDItemId)]
        public virtual string CustomID { get; set; }

    }
}