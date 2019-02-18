using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Common
{
    [SitecoreType(AutoMap = true)]
    public class FolderItems<T> : BaseComponent
    {
        [SitecoreChildren]
        public virtual IEnumerable<T> Children { get; set; }
    }
}