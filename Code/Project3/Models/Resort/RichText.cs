using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data;

namespace BGSitecore.Models.Resort
{

    [SitecoreType]
    public class RichText : BasePage
    {

        public const string RichtextContentId = "{0EC50255-00AE-401C-A445-0DB8781821FF}";
        public const string ShowCollapsedId = "{D24CDE2B-9E53-4E7E-8EFD-09C95A007DE9}";
        public const string ClassIdId = "{12F3D71A-0271-4EE7-8BDE-1113A0399147}";

        [SitecoreId]
        public virtual ID ItemId { get; set; }

        [SitecoreField(FieldName = RichtextContentId)]
        public virtual string htmlContent { get; set; }

        [SitecoreField(FieldName = ShowCollapsedId)]
        public virtual bool ShowCollapsed { get; set; }

        [SitecoreField(FieldName = ClassIdId)]
        public virtual string ClassId { get; set; }        
    }
}