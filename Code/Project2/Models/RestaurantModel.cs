using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class RestaurantModel : MasterModel
    {
        public String HeaderImageURL { get; set; }
        public String LogoImageURL { get; set; }
        public String Name { get; set; }
        public String HoursOfOperation { get; set; }
        public String Phone { get; set; }
        public String Location { get; set; }
        public int DisplayOrder { get; set; }
        public String TeaserLine { get; set; }
        public List<MenuCategoryModel> MenuCategories { get; set; }

        public RestaurantModel()
        {
            MenuCategories = new List<MenuCategoryModel>();
        }
    }
}