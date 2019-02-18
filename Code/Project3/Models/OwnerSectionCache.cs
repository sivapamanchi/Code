using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{
    public class OwnerSectionCache
    {

        public ResortService.ReservationsList.ReservationsListResponse reservation { get; set; }
        public OwnerService.PointBalanceListResponse.PointBalanceListResponse PointBalanceList { get; set; }
        public OwnerService.OwnerPointsResponse.RestOwnerPointsResponse GetOwnerPoints { get; set; }
        public RewardService.RewardResponse rewardsActivity { get; set; }
        public ReferralService.ReferralResponse referralResponse { get; set; }
        public List<ThirdParty.CDataResponseObjects.TaxRecordSearchCDataResponse> taxRecordSearchCDataResponse { get; set; }
    }
}