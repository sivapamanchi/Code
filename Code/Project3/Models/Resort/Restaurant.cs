using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{

    [SitecoreType(AutoMap = true)]
    public class Restaurant 
    {
        public const string RestaurantNameId = "{749B29E8-006C-451D-9D9A-E9BB7FECD9B9}";
        public const string HoursOfOperationId = "{4281E667-6CE2-4C14-82F9-642642EB519F}";
        public const string PhoneId = "{872FE99F-3136-431C-93F9-F90B515DFB82}";
        public const string LocationId = "{82FD697A-EB23-47DF-A33D-203C9AA78DC0}";
        public const string RestaurantTeaserId = "{0AD2D1A1-9C84-4F4B-8DA1-7F6EBC52F27B}";
        public const string RestaurantHeaderImageId = "{A1B1F3C8-D52F-4E51-AFCB-3190CC8C773E}";
        public const string RestaurantLogoImageId = "{28A681D6-FBD3-4D62-8B66-3C9D8CF36590}";

        [SitecoreId]
        public virtual Guid ID { get; set; }

        [SitecoreField(FieldName = "__Sortorder")]
        public virtual string SortOrder { get; set; }

        [SitecoreField(FieldName = RestaurantNameId)]
        public virtual string RestaurantName { get; set; }

        [SitecoreField(FieldName = HoursOfOperationId)]
        public virtual string HoursOfOperation { get; set; }

        [SitecoreField(FieldName = PhoneId)]
        public virtual string Phone { get; set; }

        [SitecoreField(FieldName = LocationId)]
        public virtual string Location { get; set; }

        [SitecoreField(FieldName = RestaurantTeaserId, Setting = SitecoreFieldSettings.RichTextRaw)]
        public virtual string RestaurantTeaser { get; set; }

        [SitecoreField(FieldName = RestaurantHeaderImageId)]
        public virtual Image RestaurantHeaderImage { get; set; }

        [SitecoreField(FieldName = RestaurantLogoImageId)]
        public virtual Image RestaurantLogoImage { get; set; }

        [SitecoreQuery("./*[@@templateid='{E6F3C72C-0A3A-43E4-B7A4-9DFB97CBFAE0}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<MenuCategory> MenuCategory { get; set; }

    }
}