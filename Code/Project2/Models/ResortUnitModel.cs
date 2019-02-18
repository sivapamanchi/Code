using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class ResortUnitModel
    {
        public bool Included { get; set; }
        public string UnitTypeCode { get; set; }
        public string UnitTypeName { get; set; }
        public int SortOrder { get; set; }
    }
}