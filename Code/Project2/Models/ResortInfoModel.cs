using BGModern.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class ResortInfoModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }

        public List<PlanItSearchResultModel> AvailableUnits { get; set; }
        public List<UnitType> AvailableUnitTypes { get; set; }
    }
}