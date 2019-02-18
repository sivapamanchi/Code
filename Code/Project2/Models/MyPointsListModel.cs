using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class MyPointsListModel
    {
        //Data Grid elements
        public List<PointModel> AllDetailPoints { get; set; }

        public List<PointModel> CurrentDetailPoints { get; set; }
        public List<PointModel> FutureDetailPoints { get; set; }

        public List<PointModel> CurrentSummaryPoints { get; set; }
        public List<PointModel> FutureSummaryPoints { get; set; }
        public Boolean HideCurrentPoints { get; set; }
        public Boolean HideFuturePoints { get; set; }

    }

}