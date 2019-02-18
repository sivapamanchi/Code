using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class NewsItemModel : MasterModel
    {
        public String PageTitle { get; set; }
        public String ItemTitle { get; set; }
        public String Blurb { get; set; }
        public String URL { get; set; }
        public String AdditionalInfoText { get; set; }
        public String DisplayImage { get; set; }
        public String DetailCopy { get; set; }
        public String InsideImage { get; set; }
        public Boolean IsHomePageNews { get; set; }
        public Boolean IsFeaturedNews { get; set; }
        public HashSet<String> SalesTypes { get; set; }
        public String RedirectUrl { get; set; }
        public String GoBackUrl { get; set; }
    }
}