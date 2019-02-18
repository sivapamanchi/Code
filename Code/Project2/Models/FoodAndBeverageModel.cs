using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class FoodAndBeverageModel
    {
        public String Name { get; set; }
        public List<RestaurantModel> Restaurants { get; set; }

        public FoodAndBeverageModel()
        {
            Restaurants = new List<RestaurantModel>();
        }
    }
}