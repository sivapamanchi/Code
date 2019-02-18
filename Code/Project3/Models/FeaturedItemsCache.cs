
using System;
using System.Collections.Generic;

namespace BGSitecore.Models
{
    [Serializable]
    public class FeaturedItemsCache
    {

        public string PageTitle { get; set; }
        public virtual List<FeaturedItemCache> AllFeaturedItems { get; set; }

    }
}