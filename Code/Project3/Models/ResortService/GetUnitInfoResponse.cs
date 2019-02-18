
using System;
using System.Collections.Generic;

namespace BGSitecore.Models.ResortService.GetUnitInfoResponse
{


    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }

    public class GetUnitInfoResponse
    {
        public List<Error> Errors { get; set; }
        public string ProjectNumber { get; set; }
        public string UnitNumber { get; set; }
        public string UnitType { get; set; }
        public string Accomodates { get; set; }
        public string ViewType { get; set; }
        public string AreaType { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneExtension { get; set; }
        public string HandicapAccessible { get; set; }
        public string Smoking { get; set; }
        public string SplitRoom { get; set; }
        public string PrimaryRoom { get; set; }
        public string UnitDescription { get; set; }
    }

}
