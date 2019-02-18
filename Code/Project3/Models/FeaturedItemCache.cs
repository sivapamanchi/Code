using System;


namespace BGSitecore.Models
{
    [Serializable]    
    public class FeaturedItemCache 
    {
        public string PageTitle { get; set; }
        public string ImageSrc { get; set; }
        public string ImageHref { get; set; }
        public string ImageCaption { get; set; }
        public string Category { get; set; }
    }
}