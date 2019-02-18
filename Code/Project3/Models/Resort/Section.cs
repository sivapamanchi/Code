using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

using Sitecore.Data.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{

    [SitecoreType(AutoMap = true)]
    public class Section : BasePage
    {
        public const string DisplaynameId = "{4A95894E-EA36-4743-BF37-72B409B7433C}";
        public const string classnameId = "{3A7A32CD-0065-4D67-AFB6-B1A10D772D58}";
        public const string HideNameId = "{A5B747A0-6AEB-49D5-8507-B02F01194045}";
        public const string ShowJumpToSectionId = "{E8069B9E-BC2A-440F-8558-27FBFE06339A}";
        public const string HideSectionWhenPrintingId = "{E9E48978-319E-4504-B0CD-C63461BD6CEC}";
        public const string ShowLineAfterSectionId = "{0D38F44B-A0FE-47BD-9EA3-6CAB6406F8CB}";

        public const string JumpDisplayNameId = "{34C8FD4F-D93F-4DBD-A81C-0AEF92066DA3}";
        public const string JumpUniqueIdId = "{0649CC7A-C935-434E-8FEF-A9FB028E0D6B}";
        public const string JumpCaptionId = "{BB05602D-57D5-4687-B2C8-F07F97028E28}";

        public const string RestrictionRuleId = "{85B5A4F7-0974-4455-85FA-B33FFAF397E0}";

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

        [SitecoreField(FieldName = JumpUniqueIdId)]
        public virtual string JumpUniqueId { get; set; }

        [SitecoreQuery("./*[@@templateid='{8976072F-9388-4B9C-A99E-84F6952CCCA5}']", IsRelative = true, IsLazy =false)]
        public virtual IEnumerable<ResortImage> AllImages { get; set; }

        [SitecoreQuery("./*[@@templateid='{2375C555-3FDE-42F1-8D04-4664439A3917}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<Video> AllVideos { get; set; }

        [SitecoreQuery("./*[@@templateid='{54DDFD1F-71AA-4AA7-91D6-80EA91DB1B79}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<RichText> AllRichText { get; set; }

        [SitecoreQuery("./*[@@templateid='{991E9D46-D83B-4258-A0AA-96862FF656BB}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<tours360> AllTours360 { get; set; }

        [SitecoreQuery("./*[@@templateid='{A6A55221-D827-4D03-8AD9-42FE4F801412}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<ImportantNote> AllNotes { get; set; }

        [SitecoreQuery("./*[@@templateid='{2DB18E15-AFFD-471A-8378-F42961E3B4F1}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<AverageWeather> AllWeather { get; set; }

        [SitecoreQuery("./*[@@templateid='{336B3C32-C438-47AB-B5DD-E52311FB8EE2}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<MapAndDirections> AllMapAndDirections { get; set; }

        [SitecoreQuery("./*[@@templateid='{657B8449-C71F-43F7-AF3E-CD85C88B51C7}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<ResortMap> AllResortMaps { get; set; }

        [SitecoreQuery("./*[@@templateid='{9D070E5F-10CF-4CC8-ADD9-E25BC7C695FB}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<Restaurant> AllRestaurant { get; set; }

        [SitecoreQuery("./*[@@templateid='{585B7C6C-E838-4BC5-9399-B7324A087009}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<ResortAmenities> AllAmenities { get; set; }

        [SitecoreQuery("./*[@@templateid='{11FAFFE9-D1EA-4CE5-8153-F1EC32BE9AFC}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<PointsCategory> AllPointsCategory { get; set; }

        [SitecoreQuery("./*[@@templateid='{F0435C3C-2750-4B99-993F-7F4592FC71D8}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<ActivityCategory> AllPointsActivity { get; set; }

        public bool HideSection { get; set; }

        public string BuildJumpToLink()
        {
            return "jumpto-" + Displayname.ToLower().Replace(' ', '-');
        }
    }
}