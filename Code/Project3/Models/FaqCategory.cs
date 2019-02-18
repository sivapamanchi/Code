using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace BGSitecore.Models
{

    [SitecoreType]
    public class FaqCategory
    {
        public const string PlaceHolderTextId = "{0E95E85D-EBD6-4BA6-B83F-AA2AF0318A90}";
        public const string ButtonTextId = "{7D91E557-8599-4A7B-ABA2-78D9EB284753}";
        public const string NoResultsMessageId = "{799ECCDF-7BEF-4AFD-B57E-6A1599DBFC0E}";

        [SitecoreField(FieldName = PlaceHolderTextId)]
        public virtual string PlaceHolderText { get; set; }

        [SitecoreField(FieldName = ButtonTextId)]
        public virtual string ButtonText { get; set; }

        [SitecoreField(FieldName = NoResultsMessageId)]
        public virtual string NoResultsMessage { get; set; }

        [SitecoreQuery("./*[@@templateid='{ADF96AA6-AB65-47BB-B662-64B007E7D2E3}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<FaqCategoryImage> categoryList { get; set; }

        public IEnumerable<FaqCategoryImage> getFilteredCategoryList()
        {
            var allFeaturedItems = categoryList.Select(x => x).ToList(); ;
            foreach (var item in allFeaturedItems.Reverse<FaqCategoryImage>())
            {
                if (item.InnerItem.Fields[BaseComponent.RestrictionRuleId] != null)
                {
                    string rule = item.InnerItem.Fields[BaseComponent.RestrictionRuleId].Value;
                    if (!Utils.SitecoreUtils.EvaluateRules(rule, item.InnerItem))
                    {
                        allFeaturedItems.Remove(item);
                    }
                }
            }
            return allFeaturedItems.AsEnumerable();
        }
    }
}