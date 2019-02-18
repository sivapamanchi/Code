using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.OwnerService
{
    public class OwnerResp
    {
        public string Identifier { get; set; }
        public List<Person> People { get; set; } = new List<Person>();
        public Account Account { get; set; } = new Account();
        public Financial Financial { get; set; } = new Financial();
        public List<Error> Errors { get; set; } = new List<Error>();
        public CreditCardPayment CreditCardPayment { get; set; } = new CreditCardPayment();
    }

    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }

    }

    public class CreditCardPayment
    {
        public string TransactionID { get; set; }
        public string AuthorizationNumber { get; set; }
        public string CreditCardExpirationDate { get; set; }
        public string CreditCardType { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardCVV { get; set; }

    }

    public class Financial
    {
        public InstallmentPlans InstallmentPlans { get; set; } = new InstallmentPlans();
        public string InstallmentPlanEligible { get; set; }
        public string TotalDueAmount { get; set; }

    }

    public class InstallmentPlans
    {
       
        public InstallmentPlan[] InstallmentPlan { get; set; } 
        public string InstallmentPlanEligible { get; set; }
       
    }

    public class InstallmentPlan
    {
        public CreditCardPayment[] CreditCardPayments { get; set; } 
        public string AccountNumber { get; set; }
        public string AmountDueTotal { get; set; }

        public string InstallmentPlanStatus { get; set; }

        public InstallmentPayment[] InstallmentPayments { get; set; }

    }

    public class InstallmentPayment
    {
        public string InstallmentPaymentAmount { get; set; }
        public string InstallmentPaymentPaid { get; set; }

        public string InstallmentPaymentDate { get; set; }
    }

    public class Account
    {
        public Memberships Memberships { get; set; } = new Memberships();
        public AccountHolders AccountHolders { get; set; } = new AccountHolders();
    }

    public class AccountHolders
    {
        public string[] AccountHolder { get; set; }
    }

    public class Memberships
    {
        public Membership Membership { get; set; } = new Membership();
        public BluegreenOnlineMembership BluegreenOnlineMembership { get; set; } = new BluegreenOnlineMembership();
        public SamplerMembership SamplerMembership { get; set; } = new SamplerMembership();
        public VacationClubMembership VacationClubMembership { get; set; } = new VacationClubMembership();
        public BluegreenRewardsMembership BluegreenRewardsMembership { get; set; } = new BluegreenRewardsMembership();
        public RCIMembership RCIMembership { get; set; } = new RCIMembership();
        public TravelerPlusMembership TravelerPlusMembership { get; set; } = new TravelerPlusMembership();
        public Profile Profile { get; set; } = new Profile();
    }

    public class Profile {
        public string Project { get; set; }
    }

    public class SamplerMembership
    {
        public Membership Membership { get; set; } = new Membership();
        public string SamplerExpirationDate { get; set; }
        public string IsSampler { get; set; }

    }
    public class TravelerPlusMembership
    {
        public string TravelerPlusEligible { get; set; }
        public Membership Membership { get; set; } = new Membership();
        public Renewal Renewal { get; set; } = new Renewal();

    }

    public class Renewal
    {
        public string AutoRenewal { get; set; }
        public string RenewalMessage { get; set; }
        public string RenewalFee { get; set; }
        public string AutoRenewalFee { get; set; }
        public string RenewalPenalty { get; set; }
        public string RenewalPopup { get; set; }
        public string CreditCardExpiration { get; set; }
    }

    public class RCIMembership
    {
        public Membership Membership { get; set; } = new Membership();
        public string WeekNumber { get; set; }
        public string PointNumber { get; set; }
    }

    public class BluegreenOnlineMembership
    {
        public Membership Membership { get; set; } = new Membership();

    }

    public class VacationClubMembership
    {
        public string AnniversaryStartDate { get; set; }
        public Points Points { get; set; } = new Points();
        public Paperless Paperless { get; set; } = new Paperless();

        public Accounts Accounts { get; set; } = new Accounts();
        
        public Sampler Sampler { get; set; } = new Sampler();
        public Legacy Legacy { get; set; } = new Legacy();
        public string PrimaryAccountNumber { get; set; }
       
        public Membership Membership { get; set; } = new Membership();
        public string AnniversaryEndDate { get; set; }
        
    }

    public class Accounts
    {
        public List<AccountInfo> AccountInfo { get; set; } = new List<AccountInfo>();
    }
    public class Paperless
    {
        public string PaperlessRequested { get; set; }
        public string PaperlessDelivered { get; set; }
        public string SetPaperlessOp { get; set; }
    }
    public class AccountInfo
    {
        public string PurchaseDate { get; set; }
        public string AccountNumber { get; set; }
        public string NextEarnAmount { get; set; }
        public Legacy Legacy { get; set; } = new Legacy();

        public string Weeks { get; set; }
        public string SaleType { get; set; }
        public Financial Financial { get; set; } = new Financial();
    }

    public class Legacy
    {
        public BonusTime BonusTime { get; set; } = new BonusTime();
        public string MaintFeePaymentBalance { get; set; }
        public string MaintFeePaymentBalanceForReservation { get; set; }
        public MaintenanceFees MaintenanceFees { get; set; } = new MaintenanceFees();
        public string AtLeastOneAccountQualify4MarketCampaign { get; set; }
        public string HasAccountInSecondaryMarket { get; set; }
        public string AllAccountsComesFromSecondaryMarketing { get; set; }
        public string ProjectNumber { get; set; }
        public string HomeProjectNumber { get; set; }
        public string Expiration { get; set; }
        public string ProjectName { get; set; }
        public string BGRewardsOwnerID { get; set; }
        public string VIP { get; set; }

    }
    public class MaintenanceFees
    {
        public List<MaintenanceFee> MaintenanceFeeReservation { get; set; }
        public List<MaintenanceFee> MaintenanceFeePaymentBalance { get; set; }
    }
    public class MaintenanceFee
    {
        public string Amount { get; set; }
        public string ProjectName { get; set; }
        public string ProjectNumber { get; set; }
        public string Weeks { get; set; }
        public string VIP { get; set; }
        public string SalesType { get; set; }
    }
    public class BonusTime {
        public string BlueWhiteCertificateEligible { get; set; }
        //public FourKPoints FourKPoints { get; set; } //NOT TO BE USED
    }

    public class Sampler
    {
        public string IsSampler { get; set; }
        public string SamplerExpirationDate { get; set; }
        public string HomeProjectNumber { get; set; }

    }

    public class Points
    {
        public List<VacationClubPoint> VacationClubPoint { get; set; } = new List<VacationClubPoint>();
        public string TotalSecondaryMarketPoints { get; set; }
        public string TotalFuturePoints { get; set; }
        public string TotalAnnualPoints { get; set; }
        public string TotalSavedPoints { get; set; }
        public string TotalRestrictedPoints { get; set; }
        public string TotalPoints { get; set; }
        public SavePoints SavePoints { get; set; } = new SavePoints();
    }

    public class VacationClubPoint
    {
        public MetaData MetaData { get; set; } = new MetaData();
        public string AccountNumber { get; set; }
        public string PointType { get; set; }
        public string PointTypeDescription { get; set; }
        public string PointBalance { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SecondaryMarket { get; set; }

    }

    public class MetaData
    {
        public string CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string ReferenceID { get; set; }

    }

    public class SavePoints
    {
        public string SavePointsOptionPaid { get; set; }
        public string SavePointsMessageID { get; set; }
        public string SavePointsFee { get; set; }
        public string SavePointsMessage { get; set; }
        public string SavePointsPenalty { get; set; }
        public string SavePointsPopup { get; set; }
        public string SavePointsPopupNextDate { get; set; }
        public MetaData MetaData { get; set; } = new MetaData();
        public string SavePointsEligible { get; set; }

    }

    public class BluegreenRewardsMembership
    {
        public Membership Membership { get; set; } = new Membership();
        public Rewards Rewards { get; set; } = new Rewards();

        public Legacy Legacy { get; set; } = new Legacy();
    }
    public class Rewards
    {
        public string TotalRewardsEarned { get; set; }
        public string NextRewardsExpiringDate { get; set; }
        public string NextExpiringRewards { get; set; }
        public string RewardsBalance { get; set; }
        public string isBarclaysCardholder { get; set; }
    }
    public class Membership
    {
        public string MembershipName { get; set; }
        public string MemberNumber { get; set; }
        public string MembershipInGoodStanding { get; set; }
        public string Identifier { get; set; }
        public string MembershipLevel { get; set; }
        public string MembershipLevelDescription { get; set; }
        public string Authenticated { get; set; }
        public string MembershipExpired { get; set; }

        public MetaData MetaData { get; set; } = new MetaData();
        public string MembershipExpirationDate { get; set; }
        public MembershipSecurity MembershipSecurity { get; set; } = new MembershipSecurity();

    }
    public class MembershipSecurity {
        public string Authenticated { get; set; }
    }
 
    public class Person
    {
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<PhoneNumbers> PhoneNumbers { get; set; } = new List<PhoneNumbers>();
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<EmailAddress> EmailAddresses { get; set; } = new List<EmailAddress>();
        public string TaxID { get; set; }
        public Security Security { get; set; } = new Security();
    }

    public class Security
    {
        public string UserName { get; set; }
        public string FirstLogin { get; set; }
        public string LastLogin { get; set; }
        public string LoginCount { get; set; }
        public string ChangePassword { get; set; }
    }

    public class EmailAddress
    {
        public string Email { get; set; }

    }

    public class PhoneNumbers
    {
        public string PhoneNumber { get; set; }
    }

    public class Address
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }

        public string City { get; set; }

        public string ProvinceCode { get; set; }

        public string Subdivison { get; set; }

        public string PostalCode { get; set; }

        public string CountryCode { get; set; }
    }

}