﻿using BGSitecore.Models;
using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using System.Collections.Generic;

namespace BGSitecore.Models.Reward
{
    [SitecoreType(AutoMap = true)]
    public class Section : BasePage
    {
        public const string DisplaynameId = "{C6F0E993-487E-4E62-9EB4-B8CBD6960765}";
        public const string classnameId = "{484F459A-606B-4940-B03E-24F829DC970E}";
        public const string HideNameId = "{DDEC4E3E-2114-4BDE-BF19-1045D5CF8356}";
        public const string ShowJumpToSectionId = "{27A8E3DB-364C-476B-99A9-F7F9009FF0AB}";
        public const string HideSectionWhenPrintingId = "{65B55698-79D4-42DA-9D8F-96FC2A8143D0}";
        public const string ShowLineAfterSectionId = "{DCB072ED-7BB1-47DD-9C72-B1BDEF151B5F}";

        public const string JumpDisplayNameId = "{E99C9A16-6CBF-4AA1-A8E3-5C8199793772}";
        public const string JumpUniqueIdId = "{21C11358-3CF4-4AD4-B8B6-7D800D9DC905}";
        public const string JumpCaptionId = "{3D919BCB-1F4A-412C-B3B1-3723BABAFA0F}";

        public const string isHeaderNumberingId = "{60194E30-DE13-46FA-863B-DA900D2BE005}";

        public const string SectionDescriptionId = "{DDDB36F2-741F-4041-9A69-9F2CA1B2EC48}";

        public const string RestrictionRuleId = "{85B5A4F7-0974-4455-85FA-B33FFAF397E0}";

        public const string ConfirmationMessage = "{BFA21F5A-660E-4767-AD02-305C385A670A}";

        [SitecoreField(FieldName = RestrictionRuleId)]
        public virtual string RestrictionRule { get; set; }

        [SitecoreField(FieldName = DisplaynameId)]
        public virtual string Displayname { get; set; }

        [SitecoreField(FieldName = classnameId)]
        public virtual string classname { get; set; }

        [SitecoreField(FieldName = HideNameId)]
        public virtual bool HideName { get; set; }

        [SitecoreField(FieldName = HideSectionWhenPrintingId)]
        public virtual bool HideSectionWhenPrinting { get; set; }

        [SitecoreField(FieldName = ShowJumpToSectionId)]
        public virtual bool ShowJumpToSection { get; set; }

        [SitecoreField(FieldName = ShowLineAfterSectionId)]
        public virtual bool ShowLineAfterSection { get; set; }

        [SitecoreField(FieldName = JumpDisplayNameId)]
        public virtual string JumpDisplayName { get; set; }

        [SitecoreField(FieldName = JumpCaptionId)]
        public virtual string JumpCaption { get; set; }

        [SitecoreField(FieldName = isHeaderNumberingId)]
        public virtual bool isHeaderNumbering { get; set; }

        [SitecoreField(FieldName = JumpUniqueIdId)]
        public virtual string JumpUniqueId { get; set; }

        [SitecoreField(FieldName = SectionDescriptionId)]
        public virtual string SliderGalleryDescription { get; set; }

        [SitecoreField(FieldName = ConfirmationMessage)]
        public virtual string SuccessMessage { get; set; }


        [SitecoreQuery("./*[@@templateid='{CA69AAB8-62C4-44D1-9FA9-E3306A9F1B2B}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<FeaturedItem> AllImages { get; set; }

        [SitecoreQuery("./*[@@templateid='{A87A00B1-E6DB-45AB-8B54-636FEC3B5523}']", IsRelative = true, IsLazy = false)]
        public virtual FolderWithFeaturedItems FolderWithFeaturedItems { get; set; }

        [SitecoreQuery("./*[@@templateid='{DD218605-4205-405C-A514-0D2A188735CD}']", IsRelative = true, IsLazy = false)]
        public virtual HeaderColumnList MaintenanceHeaders { get; set; }


        [SitecoreQuery("./*[@@templateid='{54DDFD1F-71AA-4AA7-91D6-80EA91DB1B79}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<RichText> AllRichText { get; set; }

        [SitecoreQuery("./*[@@templateid='{E16D0E46-BB1C-4A7E-8636-DF20B0302472}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<Grid> TableGrid { get; set; }

        [SitecoreIgnore]
        public virtual List<Item> AllItems { get; set; }
        //[SitecoreQuery("./*[@@templateid='{F7D7B1D3-5FF4-4EB2-BF4D-5E3B4953DF7F}']", IsRelative = true, IsLazy = false)]
        //public virtual GridLabels GridContentLabels { get; set; }

        

        public bool HideSection { get; set; }

        public string BuildJumpToLink()
        {
            return "jumpto-" + Displayname.ToLower().Replace(' ', '-');
        }

        [SitecoreIgnore]
        public string BalanceDue { get; set; } = string.Format("{0:c}", "0");

        [SitecoreIgnore]
        public string MaintenanceFee { get; set; } = string.Format("{0:c}", "0");
    }

    [SitecoreType]
    public class FolderWithFeaturedItems
    {

        [SitecoreQuery("./*[@@templateid='{A9DA0CFA-2532-4101-81FA-69D6E63DF97C}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<FeaturedItem> AllFeaturedItems { get; set; }

    }
}