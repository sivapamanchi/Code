using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Models;

namespace BGModern.Models
{
    public class DynamicSpecialsModel
    {
        public bool ShowVG { get; set; }
        public bool Show4K { get; set; }
        public String BonusTimeEnabled { get; set; }
        public String OwnerContractType { get; set; }
        public String OwnerContractDate { get; set; }
        public String ServiceRoot { get; set; }
        public String SiteNavJS { get; set; }
        public String TPLevel { get; set; }
        public String TravelerPlusEligible { get; set; }
        public String TravelerPlusPlusLevelCon { get; set; }
        public String VacationGuardPath { get; set; }
    }
}