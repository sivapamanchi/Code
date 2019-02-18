using System;
using System.Collections.Generic;

namespace BGModern.Models
{
    public class MenuCategoryModel : MasterModel
    {
        public String Name { get; set; }
        public int DisplayOrder { get; set; }
        public String CategorySummary { get; set; }
        public String CategoryFooter { get; set; }
        public List<MenuItemCategoryModel> MenuItemCategories { get; set; }

        public MenuCategoryModel()
        {
            MenuItemCategories = new List<MenuItemCategoryModel>();
        }
    }
}