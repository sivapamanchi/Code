using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{

    [SitecoreType]
    public class Image: BasePage
    {

        public const string RemoteImageId = "{56A71601-D8F6-4C9D-9A5E-30FA6F1002FD}";
        public const string LocalImageId = "{58E7B8DC-C971-4728-B5AC-07D28D7AC29F}";
        public const string ModifierId = "{EACE8483-7784-4BF1-9794-6AFA1A693CFC}";
        public const string CaptionId = "{899F0F11-ACCD-4B6C-90B0-FFD099807EF0}";
        public const string ImageUrlId = "{7949A8A5-6B24-474E-A58C-7887F2DA01EA}";

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

        public string BuildResortCaption()
        {
            string result = "";
            if (!string.IsNullOrEmpty(Caption))
            {
                string[] tokens = Caption.Split('*');
                if (tokens.Length > 1)
                {
                    result = string.Format("<strong class='resort-name'>{0}</strong> <span class='resort-location'>{1}</span>", tokens[0], tokens[1]);
                } else
                {
                    result = string.Format("<strong class='resort-name'>{0}</strong>", Caption);
                }
            }
            return result;
        }


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
                    }else
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
                    retValue = MediaManager.GetMediaUrl((MediaItem)imageItem, mediaOptions);
                }
            }
            return retValue;
        }
    }
}