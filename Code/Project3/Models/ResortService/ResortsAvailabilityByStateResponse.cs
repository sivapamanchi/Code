
using System;
using System.Collections.Generic;

namespace BGSitecore.Models.ResortService.ResortsAvailabilityByState
{

    [Serializable]
    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }
    public class Addresess
    {
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
    }

    public class Phone
    {
        public string PhoneNumber { get; set; }
    }

    public class Resort
    { 
        public int ResortID { get; set; }     //THIS WAS MANUALLY CHANGED
        public string ResortName { get; set; }
        public List<Addresess> Addresess { get; set; }
        public List<Phone> Phones { get; set; }
    }

    public class WebUnitTypes
    {
        public string WebUnitTypeCode { get; set; }
    }

    public class ResortAvail
    {
        public Resort Resort { get; set; }
        public WebUnitTypes WebUnitTypes { get; set; }
        public string HandicapAccessible  { get; set; }
        public string TotalUnits { get; set; }
    }

    public class ResortsAvailabilityByStateResponse
    {
        public List<Error> Errors { get; set; }
        public List<ResortAvail> ResortAvails { get; set; }
    }


}
