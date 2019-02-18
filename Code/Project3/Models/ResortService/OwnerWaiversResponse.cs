using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.OwnerWaivers
{

    public class OwnerWaivers
    {
        public string WaiversAvailable { get; set; }
    }

    public class OwnerWaiversResponse
    {
        public List<object> Errors { get; set; }
        public List<object> OwnerProjects { get; set; }
        public OwnerWaivers OwnerWaivers { get; set; }
    }

}