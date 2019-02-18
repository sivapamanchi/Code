using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class ResortFilterModel
    {
        public bool Included { get; set; }
        public int ProjectNumber { get; set; }                  // Databound to ResortId
        public string Name { get; set;  }                       // Databound to ResortName
        public List<ResortUnitModel> UnitTypes { get; set; }
        public bool HiddenProjStatus { get; set; }              // false by default
    }
}