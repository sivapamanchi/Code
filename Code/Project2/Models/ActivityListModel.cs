using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class ActivityListModel : MasterModel
    {
        public String Title { get; set; }
        public String ShortListDescription { get; set; }
        //public List<ActivityModel> Activities { get; set; }
        public String CompleteActivityListMarkup { get; set; }

        public ActivityListModel()
        {
            //Activities = new List<ActivityModel>();
        }
    }
}