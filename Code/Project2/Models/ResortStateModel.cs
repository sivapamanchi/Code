using BGModern.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class ResortStateModel
    {
        public string Name { get; set; }
        public List<ResortInfoModel> AvailableResorts { get; set; }
        public List<UnitType> AvailableUnitTypes { get; set; }
    }
}