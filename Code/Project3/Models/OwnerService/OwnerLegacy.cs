using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.OwnerService
{
    //[Serializable]
    //public class OwnerLegacy : BGO.OwnerWS.Owner
    //{
    //    public OwnerLegacy() { }
    //}

    //[Serializable]
    //public class Owner 
    //{
    //    public Owner() { }

    //    public bool AccountExpired { get; set; }
    //    public List<string> AccountHolders { get; set; }
    //    public string accountNumber { get; set; }
    //    public AccountInfo[] Accounts { get; set; }
    //    public string Address1 { get; set; }
    //    public string Address2 { get; set; }
    //    public string Address3 { get; set; }
    //    public string AlternatePhone { get; set; }
    //    public DateTime anniversaryEndDate { get; set; }
    //    public DateTime anniversaryStartDate { get; set; }
    //    public AnnualPointsExpiration AnnualPointsExpiration { get; set; }
    //    public string Arvact { get; set; }
    //    public bool arvactVerify { get; set; }
    //    public bool Authenticated { get; set; }
    //    public bool ChPWDflag { get; set; }
    //    public string City { get; set; }
    //    public string CountryCode { get; set; }
    //    public string Eligible4kAccount { get; set; }
    //    public string Eligible4kContractDate { get; set; }
    //    public string Eligible4kFirstAnniversayDate { get; set; }
    //    public bool Eligible4kFlagged { get; set; }
    //    public bool Eligible4kOnAnniversaryDateWindow { get; set; }
    //    public bool Eligible4kRequested { get; set; }
    //    public string Eligible4kSecondAnniversayDate { get; set; }
    //    public string eligibleBlueWhiteCertificate { get; set; }
    //    public string Email { get; set; }
    //    public EncoreDividends EncoreBenefits { get; set; }
    //    public string encoreDividends { get; set; }
    //    public string errorMessage { get; set; }
    //    public string firstLogin { get; set; }
    //    public string firstName { get; set; }
    //    public string fullName { get; set; }
    //    public string HomePhone { get; set; }
    //    public InstallmentPlanProgram[] InstallmentPlan { get; set; }
    //    public bool isBarclaysCardholder { get; set; }
    //    public string last4SSN { get; set; }
    //    public string lastLogin { get; set; }
    //    public string lastName { get; set; }
    //    public string loginCount { get; set; }
    //    public MaintFee[] maintFees { get; set; }
    //    public string membershipLevel { get; set; }
    //    public string membershipLevelDesc { get; set; }
    //    public string middleName { get; set; }
    //    public string namePrefix { get; set; }
    //    public string nameSuffix { get; set; }
    //    public string objectSize { get; set; }
    //    public string OwnerExpiration { get; set; }
    //    public string ownerID { get; set; }
    //    public string ownerType { get; set; }
    //    public Paperless Paperless { get; set; }
    //    public string Password { get; set; }
    //    public double PaymentBalance { get; set; }
    //    public Point[] Points { get; set; }
    //    public int PointsTotal { get; set; }
    //    public int PointsTotalAnnual { get; set; }
    //    public int PointsTotalFuture { get; set; }
    //    public int PointsTotalRestricted { get; set; }
    //    public int PointsTotalSaved { get; set; }
    //    public int PointsTotalSecondaryMarket { get; set; }
    //    public string PostalCode { get; set; }
    //    public string queryExecuteTime { get; set; }
    //    public string rciPointNumber { get; set; }
    //    public string rciWeekNumber { get; set; }
    //    public string regionCode { get; set; }
    //    public string Registered { get; set; }
    //    public double ReservationDuePaymentBalance { get; set; }
    //    public string ResortOwnerShips { get; set; }
    //    public string serviceServer { get; set; }
    //    public string StateAbr { get; set; }
    //    public string Subdivision { get; set; }
    //    public string TravelerPlusEligible { get; set; }
    //    public TravelerPlusMembership TravelerPlusMembership { get; set; }
    //    public Profile[] User { get; set; }
    //    public bool userInGoodStanding { get; set; }


    //}

    //[Serializable]
    //public class Point
    //{
    //    public Point() { }

    //    public string AcctNum { get; set; }
    //    public DateTime beginDate { get; set; }
    //    public string errorMessage { get; set; }
    //    public DateTime expireDate { get; set; }
    //    public string nextEarnAmount { get; set; }
    //    public string nextEarnDate { get; set; }
    //    public int pointBal { get; set; }
    //    public string pointsType { get; set; }
    //    public string pointTypeDesc { get; set; }
    //    public string refNumb { get; set; }
    //    public bool SecondaryMarket { get; set; }
    //}

    //[Serializable]
    //public class Profile
    //{
    //    public Profile() { }

    //    public bool accountExpired { get; set; }
    //    public bool AllAccountsComesFromSecondaryMarketing { get; set; }
    //    public bool AtLeastOneAccountQualify4MarketCampaig { get; set; }
    //    public bool HasAccountInSecondaryMarket { get; set; }
    //    public string HomeProject { get; set; }
    //    public bool isSampler { get; set; }
    //    public string primaryAccountNumber { get; set; }
    //    public string project { get; set; }
    //    public string samplerExpiration { get; set; }
    //    public bool userInGoodStanding { get; set; }
    //}

    //[Serializable]
    //public class TravelerPlusMembership
    //{
    //    public TravelerPlusMembership() { }

    //    public bool AccountExpired { get; set; }
    //    public bool IsTravelerPlusAutoRenew { get; set; }
    //    public bool IsTravelerPlusEligible { get; set; }
    //    public string TPAutoRenewalFee { get; set; }
    //    public string TPCCExp { get; set; }
    //    public string TPExpiration { get; set; }
    //    public string TPLevel { get; set; }
    //    public string TPRenewalFee { get; set; }
    //    public string TPRenewalMessage { get; set; }
    //    public string TPRenewalPenality { get; set; }
    //    public bool TPRenewPopup { get; set; }
    //    public string TravelerPlusEligible { get; set; }

    //}

    //[Serializable]
    //public class Paperless 
    //{
    //    public Paperless() { }

    //    public bool PaperlessDelivered { get; set; }
    //    public bool PaperlessRequested { get; set; }
    //}

    //[Serializable]
    //public class MaintFee
    //{
    //    public MaintFee() { }

    //    public string acctNum { get; set; }
    //    public double amt { get; set; }
    //    public string projnum { get; set; }
    //    public string proname { get; set; }
    //    public string saleType { get; set; }
    //    public bool vip { get; set; }
    //    public string weeks { get; set; }
    //}

    //[Serializable]
    //public class InstallmentPlanProgram
    //{
    //    public InstallmentPlanProgram() { }

    //    public string Account { get; set; }
    //    public string CCEXP { get; set; }
    //    public string CCNUMBER { get; set; }
    //    public string CCTYPE { get; set; }
    //    public double DueAmountTotal { get; set; }
    //    public double InstallmentPaymentAmount1 { get; set; }
    //    public double InstallmentPaymentAmount2 { get; set; }
    //    public double InstallmentPaymentAmount3 { get; set; }
    //    public string InstallmentPaymentDate1 { get; set; }
    //    public string InstallmentPaymentDate2 { get; set; }
    //    public string InstallmentPaymentDate3 { get; set; }
    //    public string InstallmentPaymentEligible { get; set; }
    //    public string InstallmentStatus { get; set; }
    //    public string InstallPaid1 { get; set; }
    //    public string InstallPaid2 { get; set; }
    //    public string InstallPaid3 { get; set; }

    //}

    //[Serializable]
    //public class AccountInfo
    //{
    //    public AccountInfo() { }

    //    public string AcctNum { get; set; }
    //    public string Expiration { get; set; }
    //    public int index { get; set; }
    //    public string NextEarnAmount { get; set; }
    //    public string Proj { get; set; }
    //    public string ProjectHome { get; set; }
    //    public string projNM { get; set; }
    //    public string SalesDate { get; set; }

    //}

    //[Serializable]
    //public class AnnualPointsExpiration
    //{
    //    public AnnualPointsExpiration() { }

    //    public bool SavePointsEligible { get; set; }
    //    public string SavePointsFee { get; set; }
    //    public string SavePointsMessage { get; set; }
    //    public string SavepointsMessageId { get; set; }
    //    public string SavePointsPenality { get; set; }
    //    public string SavepointsPopNextDate { get; set; }
    //    public bool SavePointsPopup { get; set; }
    //}

    //[Serializable]
    //public class EncoreDividends
    //{
    //    public EncoreDividends() { }

    //    public decimal DividendsBalance { get; set; }
    //    public int EncoreOwnerID { get; set; }
    //    public decimal ExpiringDividends { get; set; }
    //    public DateTime NextDividendsExpiringDate { get; set; }
    //    public decimal TotalDividendsEarned { get; set; }

    //}
}