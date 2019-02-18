using System;
using System.Collections.Generic;

namespace BGModern.Models
{
    public class MenuItemCategoryModel : MasterModel
    {
        public String Name { get; set; }
        public int DisplayOrder { get; set; }
        public String ItemCategorySummary { get; set; }
        public String ItemCategoryFooter { get; set; }
        public List<MenuItemModel> MenuItems { get; set; }

        public MenuItemCategoryModel()
        {
            MenuItems = new List<MenuItemModel>();
        }
    }
}