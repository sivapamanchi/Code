using System;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace BGSitecore.Models
{
    [SitecoreType(AutoMap = true)]
    public class GalleryListItem : Image
    {
        public const string CaptionDescritionId = "{34C87FA7-9B82-4399-A56E-34072A6CFC29}";
        public const string InitialDate = "{068DD2CF-0E7B-41E1-B5A9-B6639E2D46DF}";
        public const string ExpiryDate = "{97C8FC9D-E624-4204-A67A-5413F954FB3B}";

        [SitecoreId]
        public virtual Guid id { get; set; }

        [SitecoreField(FieldName = CaptionDescritionId)]
        public virtual string CaptionDescription { get; set; }


        [SitecoreField(FieldName = InitialDate)]
        public virtual DateTime StartDate { get; set; }

        [SitecoreField(FieldName = ExpiryDate)]
        public virtual DateTime EndDate { get; set; }
        
    }
}