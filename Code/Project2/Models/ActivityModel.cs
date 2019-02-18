using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class ActivityModel : MasterModel
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public String Date { get; set; }
        public String Frequency { get; set; }
    }
}