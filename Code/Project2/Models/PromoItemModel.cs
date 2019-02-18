using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class PromoItemModel : MasterModel
    {
        public String PageTitle { get; set; }
        public String PromoTitle { get; set; }
        public String Summary { get; set; }
        public String Thumbnail { get; set; }
        public String PromoDetail { get; set; }
        public String MainImage { get; set; }
        public String AdditionalInfoText { get; set; }
        public String URL { get; set; }
        public Boolean IsHomePageItem { get; set; }
        public Boolean IsFeaturedItem { get; set; }
        public HashSet<String> SalesTypes { get; set; }
        public String RedirectUrl { get; set; }
        public String GoBackUrl { get; set; }
    }
}