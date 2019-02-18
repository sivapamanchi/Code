using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace BGModern.Models
{
    public class MasterModel : RenderModel
    {
        public string Title { get; set; }
        public string SiteName { get; set; }
        public string ParentUrl { get; set; }
        public int NodeID { get; set; }
        public String NodeURL { get; set; }

        public List<NavigationItemModel> MainNavigation { get; set; }

        public MasterModel()
            : base(UmbracoContext.Current.PublishedContentRequest.PublishedContent)
        {
            MainNavigation = new List<NavigationItemModel>();
        }

        public MasterModel(IPublishedContent content)
            : base(content)
        {
            // this constructor is for instances wherein the child of the "current page" is being mapped. There will not be a valid "UmbracoContext.Current.PublishedContentRequest" in this scenario
        }
    }
}