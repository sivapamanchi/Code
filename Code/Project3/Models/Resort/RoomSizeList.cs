using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

using Sitecore.Data.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{

    [SitecoreType]
    public class RoomSizeList 
    {

        [SitecoreQuery("/sitecore/content/Data/Owner/Lookup/Room Unit Size/*[@@templateid='{D66DDF18-3B01-4A05-88E9-F9C83E44B6AB}']", IsRelative = false)]
        public virtual IEnumerable<RoomSize> AllRoomSize { get; set; }

       
    }
}