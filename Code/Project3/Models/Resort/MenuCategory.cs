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
    public class MenuCategory
    {
        public const string MenuCategoryNameId = "{BC047D92-51ED-42A8-9692-8289455C3FB3}";
        public const string MenuCategorySummaryId = "{19A70171-33B8-4D7D-B01A-9D604B1FB335}";
        public const string MenuCategoryFooterId = "{25AAD9C5-712E-4E65-8C05-4DF259584D65}";

        [SitecoreId]
        public virtual Guid ID { get; set; }

        [SitecoreField(FieldName = "__Sortorder")]
        public virtual string SortOrder { get; set; }

        [SitecoreField(FieldName = MenuCategoryNameId)]
        public virtual string MenuCategoryName { get; set; }

        [SitecoreField(FieldName = MenuCategorySummaryId)]
        public virtual string MenuCategorySummary { get; set; }

        [SitecoreField(FieldName = MenuCategoryFooterId)]
        public virtual string MenuCategoryFooter { get; set; }

        [SitecoreQuery("./*[@@templateid='{3C57D91E-11E4-4DAC-B195-0081044FC79E}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<MenuCategoryItem> MenuCategoryItems { get; set; }
    }
}