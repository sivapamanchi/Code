using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class PointModel
    {
        public String AcctNum { get; set; }
        public int PointBal { get; set; }
        public DateTime ExpireDate { get; set; }
        public String PointTypeDesc { get; set; }
        public String Action { get; set; }
        public String NextEarnDate { get; set; }
        public String NextEarnAmount { get; set; }

        public int AvailablePoints{ get; set; }
        public DateTime EarnedDate { get; set; }
        public String PointType { get; set; }
    }

}