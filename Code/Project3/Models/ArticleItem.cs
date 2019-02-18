using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models;
using Glass.Mapper.Sc.Fields;

namespace BGSitecore.Models
{
    public class ArticleItem : BasePage
    {

        public const string TitleId = "{8F127FE9-3D01-43D7-B12E-A28F27DF3EFB}";
        public const string SummaryId = "{2127FC8A-0AF6-451D-A04B-B4917C5779E3}";
        public const string AnswerId = "{53404F0E-05E8-40C4-BE86-AE5874EE219C}";


        [SitecoreField(FieldName = TitleId)]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = SummaryId)]
        public virtual string Summary { get; set; }

        [SitecoreField(FieldName = AnswerId)]
        public virtual string Answer { get; set; }
}
}