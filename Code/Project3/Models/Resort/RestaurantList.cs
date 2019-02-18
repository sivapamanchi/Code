using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

using Sitecore.Data.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{
  
    public class RestaurantList 
    {

        public  IEnumerable<Restaurant> Restaurants { get; set; }

        public string RemoteImageUrl { get; set; }


    }
}