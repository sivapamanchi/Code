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
    public class AlertType
    {

        public const string AlertPriorityId = "{4BCFD496-7F74-496D-BB9F-3BDABECD4017}";
        public const string ClassId = "{2225669C-C98A-422B-9400-77793F7273B7}";
   
        [SitecoreField(FieldName = AlertPriorityId)]
        public virtual int AlertPriority { get; set; }

        [SitecoreField(FieldName = ClassId)]
        public virtual string Class { get; set; }




    }
}