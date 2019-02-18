using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BGO.OwnerWS;

namespace BGModern.Models
{
    public class OwnerModel : MasterModel
    {
        public bool BonusTimeEnabled { get; set; }
        public BGO.OwnerWS.Owner BxgOwner { get; set; }
        public bool DisplayPointsDetail { get; set; }
        public string HomeProject { get; set; }
        public bool IsAccountExpired { get; set; }
        public bool IsSamplerOwner { get; set; }
        public bool IsPendingOwner { get; set; }
        public bool IsTravelerPlusEligible { get; set; }
        public string OwnerContractType { get; set; }
        public string OwnerHomeResortWeeks { get; set; }

        public string FullName { get; set; }
        public String OwnershipLevel { get; set; }
        public int AvailablePoints { get; set; }
        public DateTime TravelerPlusExpiration { get; set; }
        public int AnnualPoints { get; set; }
        public int SavedPoints { get; set; }
        public int FuturePoints { get; set; }
        public int RestrictedPoints { get; set; }
        public Decimal PaymentBalance { get; set; }
        public int EncoreDividends { get; set; }
    }
}