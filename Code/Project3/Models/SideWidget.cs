using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models;
using Glass.Mapper.Sc.Fields;
using Glass.Mapper.Sc.Configuration;

namespace BGSitecore.Models
{
    [SitecoreType]
    public class SideWidget: BaseComponent
    {

        public const string WidgetTitleId = "{45B9157E-BA6F-41AA-A6CA-E5F111D46AEB}";
      


        [SitecoreField(FieldName = WidgetTitleId)]
        public virtual string WidgetTitle { get; set; }

        [SitecoreQuery("./*", IsRelative = true)]
        public virtual IEnumerable<SideWidgetContent> WidgetContents { get; set; }



    }
}