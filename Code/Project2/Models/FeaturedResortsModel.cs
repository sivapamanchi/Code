using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Models;

namespace BGModern.Models
{
    public class FeaturedResortsModel
    {
        public String ResortId { get; set; }
        public String ResortName { get; set; }
        public String ResortLink { get; set; }
        public String ResortDescShort { get; set; }
        public String ImageName { get; set; }
    }
}