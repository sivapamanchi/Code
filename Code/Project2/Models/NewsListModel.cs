using System;
using System.Collections.Generic;

namespace BGModern.Models
{
    public class NewsListModel : MasterModel
    {
        public String ListTitle { get; set; }
        public List<NewsItemModel> NewsItems { get; set; }

        public NewsListModel()
            : base()
        {
            NewsItems = new List<NewsItemModel>();
        }
    }
}