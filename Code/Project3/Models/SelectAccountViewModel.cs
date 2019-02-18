using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;

namespace BGSitecore.Models
{
    [SitecoreType]
    public class SelectAccountViewModel : BasePage
    {
        
        [SitecoreQuery("/sitecore/content/Data/Owner/Images/Home background Images//*[@@templatename='Image Content']", IsRelative = false)]
        public virtual IEnumerable<Image> RandomImageLocation { get; set; }

        [SitecoreChildren]
        public virtual IEnumerable<Image> Children { get; set; }

        [SitecoreIgnore]
        public virtual Dictionary<int, string> AccountList { get; set; } = new Dictionary<int, string>();

        [SitecoreIgnore]
        public virtual string selectedAccount { get; set; } = string.Empty;

    }
}