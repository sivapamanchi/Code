using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Common
{
    [SitecoreType]
    public class AlertsList
    {
        public const string AlertFolderID = "{C3F51EC7-D5E2-44B0-A555-764594431F62}";
        public const string AlertTemplateID = "{BFA077EF-2EC0-45CB-83B4-48E4E277C745}";

        [SitecoreQuery("/sitecore/content//*[@@id='" + AlertFolderID + "']//*[@@templateid='" + AlertTemplateID + "']")]
        public virtual IEnumerable<Alert> AllAlert { get; set; }
    }
}