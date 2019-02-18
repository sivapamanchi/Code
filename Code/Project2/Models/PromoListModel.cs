using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class PromoListModel : MasterModel
    {
        public String ListTitle { get; set; }
        public List<PromoItemModel> PromoItems { get; set; }

        public PromoListModel()
        {
            PromoItems = new List<PromoItemModel>();
        }
    }
}