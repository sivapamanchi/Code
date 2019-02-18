using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace BGSitecore.Models
{

    [SitecoreType]
    public class FaqCategoryImage : BasePage
    {

        public const string RemoteImageId = "{3196596B-6193-4959-A734-3D5E8B832109}";
        public const string LocalImageId = "{38A77F75-BBFB-4FF2-93F5-7E791D7D19E6}";
        public const string ModifierId = "{643CCCAB-1A46-4D3D-B55C-18BBC2D15441}";
        public const string CaptionId = "{EEC8A909-6FB3-4A09-BA29-19D208516AD4}";
        public const string ImageUrlId = "{CAA34478-A7DF-409D-9A66-5EC5EBD17F05}";
        public const string IsVisibleId = "{08BB3100-E7AC-469B-9E41-7475FA04C996}";
        public const string CategoryId = "{2C24729A-E77E-4579-BE88-1ABAE07E5455}";

        [SitecoreField(FieldName = "__Sortorder")]
        public virtual string SortOrder { get; set; }

        [SitecoreField(FieldName = RemoteImageId)]
        public virtual string RemoteImage { get; set; }

        [SitecoreField(FieldName = LocalImageId)]
        public virtual Glass.Mapper.Sc.Fields.Image LocalImage { get; set; }

        [SitecoreField(FieldName = ModifierId)]
        public virtual string Modifier { get; set; }

        [SitecoreField(FieldName = CaptionId)]
        public virtual string Caption { get; set; }

        [SitecoreField(FieldName = ImageUrlId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link ImageUrl { get; set; }
        
        [SitecoreField(FieldName = IsVisibleId)]
        public virtual bool isVisible { get; set; }

        [SitecoreField(FieldName = CategoryId, FieldType = SitecoreFieldType.Droplist)]
        public virtual string  category { get; set; }

        public string ImageFullUrl()
        {
            string retValue = "";
            if (!string.IsNullOrEmpty(RemoteImage))
            {
                if (RemoteImage.ToLower().Contains("http"))
                {
                    retValue = RemoteImage;
                }
                else
                {
                    if (string.IsNullOrEmpty(Modifier))
                    {
                        retValue = string.Format("{0}/{1}", SiteSettings.RemoteImageUrl, RemoteImage);
                    }
                    else
                    {
                        if (Modifier.StartsWith("?"))
                        {
                            retValue = string.Format("{0}/{1}{2}", SiteSettings.RemoteImageUrl, RemoteImage, Modifier);

                        }
                        else
                        {
                            retValue = string.Format("{0}/{1}?{2}", SiteSettings.RemoteImageUrl, RemoteImage, Modifier);
                        }
                    }
                }
            }
            else
            {
                retValue = LocalImageFullUrl();
            }
            return retValue;
        }
        
        public string LocalImageFullUrl()
        {
            //Build the URL for the image to include the server name.  This is required when adding the image in the header since this header 
            // is also render on 3rd party site
            string retValue = "";
            if (this.LocalImage != null)
            {
                var context = new SitecoreContext();
                var imageItem = context.GetItem<Item>(this.LocalImage.MediaId);

                if (imageItem != null)
                {
                    var mediaOptions = new MediaUrlOptions { AlwaysIncludeServerUrl = true };
                    retValue = MediaManager.GetMediaUrl(imageItem, mediaOptions);
                }
            }
            return retValue;
        }
    }
}