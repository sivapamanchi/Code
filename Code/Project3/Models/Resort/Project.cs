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
    public class Project: BasePage
    {
        public const string ProjectIdId = "{BBA64CBF-FB2E-4994-A650-141C57C34040}";
        public const string ProjectNameId = "{5A6FEF2C-2F56-4CEC-BB38-8E7A4F58C36B}";

        [SitecoreField(FieldName = ProjectIdId)]
        public virtual int ProjectId { get; set; }

        [SitecoreField(FieldName = ProjectNameId)]
        public virtual string ProjectName { get; set; }

        [SitecoreQuery("./*[@@templatename='Room Content']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<Room> Rooms { get; set; }

    }
}