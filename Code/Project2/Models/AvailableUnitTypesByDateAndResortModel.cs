using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class AvailableUnitTypesByDateAndResortModel
    {
        public string CheckInDate { get; set; }
        public string ResortID { get; set; }
        public string UnitCodeList { get; set; }
    }
}