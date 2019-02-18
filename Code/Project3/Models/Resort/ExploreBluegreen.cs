using BGSitecore.Models.Resort;
using BGSitecore.Utils;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public class ExploreBluegreen: BasePage
    {
 
    //    [SitecoreQuery("/sitecore/content//*[@@templatename='Experience Content']", IsRelative = false)]
        public virtual IEnumerable<Experience> AllExperience { get; set; }

 
        //[SitecoreQuery("/sitecore/content//*[@@templatename='Resort Page']", IsRelative = false)]
        public virtual List<ResortDetails> AllResorts { get; set; }

        /// <summary>
        /// Builds up a JSON version of the resorts list for the Explorer filtering
        /// </summary>
        /// <returns></returns>
        public string AllResortsToJson()
        {
            return ResortUtils.BuildResortsJson(this.AllResorts);
        }

    }
}