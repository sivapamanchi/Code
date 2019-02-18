using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Common
{

    public class CollectionCodeConfiguration
    {
        public const string CollectionCodeConfigurationItemId = "{097B6014-E16A-416C-90D8-748F9FD7350D}";
        public const string NotAllowedCollectionCodesId = "{C8757365-8BCF-40C3-AF8D-565AE89020B9}";
        public const string UseSitecoreCollectionCodesId = "{C7670F55-7AB3-4137-91AF-F5A8711E269B";

        public CollectionCodeConfiguration()
        {
        
        }

        [SitecoreField(FieldName = NotAllowedCollectionCodesId)]
        public virtual string NotAllowedCollectionCodes { get; set; }

        [SitecoreField(FieldName = UseSitecoreCollectionCodesId)]
        public virtual bool UseSitecoreCollectionCodes { get; set; }

        [SitecoreIgnore]
        public List<string> NotAllowedCollectionCodesList { get; set; }
        public List<string> GetAllowedCollectionCodesList()
        {
            var sitecoreContext = new SitecoreContext();
            CollectionCodeConfiguration getContextItem = sitecoreContext.GetItem<CollectionCodeConfiguration>(CollectionCodeConfiguration.CollectionCodeConfigurationItemId);
            string targetItem = getContextItem.NotAllowedCollectionCodes;
            if (!string.IsNullOrEmpty(targetItem))
                return new List<string>(targetItem.Split(','));
            return null;
        }

    }
}
