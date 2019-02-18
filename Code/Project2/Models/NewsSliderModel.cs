using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class NewsSliderModel
    {
        public List<dynamic> NewsAndPromoList { get; set; }

        public NewsSliderModel()
        {
            NewsAndPromoList = new List<dynamic>();
        }
    }
}