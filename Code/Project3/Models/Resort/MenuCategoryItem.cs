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
    public class MenuCategoryItem
    {
        public const string MenuCategoryItemNameId = "{38331FE7-C163-45F3-ACB9-8153EA4C99CB}";
        public const string MenuCategoryItemSummaryId = "{CF3AA8D5-F052-4F41-A533-512E81F29E4C}";
        public const string MenuCategoryItemFooterId = "{75C9D135-733B-4463-9340-BD6284D9F334}";

        [SitecoreId]
        public virtual Guid ID { get; set; }

        [SitecoreField(FieldName = "__Sortorder")]
        public virtual string SortOrder { get; set; }

        [SitecoreField(FieldName = MenuCategoryItemNameId)]
        public virtual string MenuCategoryItemName { get; set; }

        [SitecoreField(FieldName = MenuCategoryItemSummaryId)]
        public virtual string MenuCategoryItemSummary { get; set; }

        [SitecoreField(FieldName = MenuCategoryItemFooterId)]
        public virtual string MenuCategoryItemFooter { get; set; }

        [SitecoreQuery("./*[@@templateid='{5D2E3B2C-CF51-4B90-980D-F41ADB5E82C0}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<MenuItem> MenuItems { get; set; }
    }
}