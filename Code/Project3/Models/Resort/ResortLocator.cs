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
    public class ResortLocator: BasePage
    {
        public const string NodeId = "{C41B6619-DDE7-48EE-87AC-F52E73B08418}";
        public const string ExperienceHeaderId = "{346D2FE0-5B48-42A0-B8DC-609F334A0FBD}";
        public const string DestinationHeaderId = "{73400915-5171-4BC3-A74C-42B7EB27A9C8}";
        public const string ResortHeaderId = "{3ACEF08B-736E-465F-B5AD-695AA3CA6AA4}";

        [SitecoreField(FieldName = ExperienceHeaderId)]
        public virtual string ExperienceHeader { get; set; }

        [SitecoreField(FieldName = DestinationHeaderId)]
        public virtual string DestinationHeader { get; set; }

        [SitecoreField(FieldName = ResortHeaderId)]
        public virtual string ResortHeader { get; set; }

       // [SitecoreQuery("/sitecore/content//*[@@templatename='Experience Content']", IsRelative = false)]
        public virtual List<Experience> AllExperience { get; set; }

      //  [SitecoreQuery("/sitecore/content//*[@@templatename='Club Affiliations']", IsRelative = false)]
        public virtual IEnumerable<ClubAffiliation> ClubAffiliation { get; set; }


      //  [SitecoreQuery("/sitecore/content//*[@@templatename='Resort Page']", IsRelative = false)]
        public virtual IEnumerable<ResortDetails> AllResorts { get; set; }

        /// <summary>
        /// Builds up a JSON version of the resorts list for the Map
        /// </summary>
        /// <returns></returns>
        public string AllResortsToJson()
        {
            return ResortUtils.BuildResortsJson(this.AllResorts);
        }
    }
}