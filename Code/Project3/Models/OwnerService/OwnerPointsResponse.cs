﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.OwnerService.OwnerPointsResponse
{


    public class PhoneNumber1
    {
        public string PhoneNumber { get; set; }
        public string Prefix { get; set; }
        public string Extension { get; set; }
        public string PhoneNumberType { get; set; }
    }

    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string ProvinceCode { get; set; }
        public string Subdivison { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string AddressType { get; set; }
    }

    public class EmailAddress
    {
        public string Email { get; set; }
        public string AddressType { get; set; }
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

    public class MetaData2
    {
        public string CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string ReferenceID { get; set; }
    }

    public class Security
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public MetaData2 MetaData { get; set; }
        public string Active { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string FirstLogin { get; set; }
        public string LastLogin { get; set; }
        public string LoginCount { get; set; }
        public string ChangePassword { get; set; }
        public string BirthDate { get; set; }
        public string SpouseFirstName { get; set; }
        public string SpouseLastName { get; set; }
    }

    public class Person
    {
        public int Index { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Prefix { get; set; }
        public List<PhoneNumber1> PhoneNumbers { get; set; }
        public List<Address> Addresses { get; set; }
        public List<EmailAddress> EmailAddresses { get; set; }
        public string TaxID { get; set; }
        public string Suffix { get; set; }
        public string Identifier { get; set; }
        public MetaData MetaData { get; set; }
        public Security Security { get; set; }
        public string BirthDate { get; set; }
        public string SpouseFirstName { get; set; }
        public string SpouseLastName { get; set; }
    }

    public class MetaData3
    {
        public string CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string ReferenceID { get; set; }
    }

    public class MembershipSecurity
    {
        public string Authenticated { get; set; }
    }

    public class Membership
    {
        public string Identifier { get; set; }
        public string Authenticated { get; set; }
        public string MembershipLevel { get; set; }
        public string MembershipLevelDescription { get; set; }
        public string MemberNumber { get; set; }
        public string MembershipName { get; set; }
        public string MembershipInGoodStanding { get; set; }
        public string MembershipExpired { get; set; }
        public string MembershipExpirationDate { get; set; }
        public MetaData3 MetaData { get; set; }
        public MembershipSecurity MembershipSecurity { get; set; }
    }

    public class MetaData4
    {
        public string CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string ReferenceID { get; set; }
    }

    public class MembershipSecurity2
    {
        public string Authenticated { get; set; }
    }

    public class Membership2
    {
        public string Identifier { get; set; }
        public string Authenticated { get; set; }
        public string MembershipLevel { get; set; }
        public string MembershipLevelDescription { get; set; }
        public string MemberNumber { get; set; }
        public string MembershipName { get; set; }
        public string MembershipInGoodStanding { get; set; }
        public string MembershipExpired { get; set; }
        public string MembershipExpirationDate { get; set; }
        public MetaData4 MetaData { get; set; }
        public MembershipSecurity2 MembershipSecurity { get; set; }
    }

    public class BluegreenOnlineMembership
    {
        public Membership2 Membership { get; set; }
    }

    public class MetaData5
    {
        public string CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string ReferenceID { get; set; }
    }

    public class MembershipSecurity3
    {
        public string Authenticated { get; set; }
    }

    public class Membership3
    {
        public string Identifier { get; set; }
        public string Authenticated { get; set; }
        public string MembershipLevel { get; set; }
        public string MembershipLevelDescription { get; set; }
        public string MemberNumber { get; set; }
        public string MembershipName { get; set; }
        public string MembershipInGoodStanding { get; set; }
        public string MembershipExpired { get; set; }
        public string MembershipExpirationDate { get; set; }
        public MetaData5 MetaData { get; set; }
        public MembershipSecurity3 MembershipSecurity { get; set; }
    }

    public class SamplerMembership
    {
        public Membership3 Membership { get; set; }
        public string SamplerExpirationDate { get; set; }
        public string IsSampler { get; set; }
    }

    public class MetaData6
    {
        public string CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string ReferenceID { get; set; }
    }

    public class MembershipSecurity4
    {
        public string Authenticated { get; set; }
    }

    public class Membership4
    {
        public string Identifier { get; set; }
        public string Authenticated { get; set; }
        public string MembershipLevel { get; set; }
        public string MembershipLevelDescription { get; set; }
        public string MemberNumber { get; set; }
        public string MembershipName { get; set; }
        public string MembershipInGoodStanding { get; set; }
        public string MembershipExpired { get; set; }
        public string MembershipExpirationDate { get; set; }
        public MetaData6 MetaData { get; set; }
        public MembershipSecurity4 MembershipSecurity { get; set; }
    }

    public class FourKPoints
    {
        public string AccountSetup { get; set; }
        public string OnAnniversaryDateWindow { get; set; }
        public string Requested { get; set; }
        public string AccountNumber { get; set; }
        public string ContractDate { get; set; }
        public string FirstAnniversaryDate { get; set; }
        public string SecondAnniversaryDate { get; set; }
    }

    public class BonusTime
    {
        public string BlueWhiteCertificateEligible { get; set; }
        public FourKPoints FourKPoints { get; set; }
    }

    public class Legacy
    {
        public BonusTime BonusTime { get; set; }
        public string AtLeastOneAccountQualify4MarketCampaign { get; set; }
        public string HasAccountInSecondaryMarket { get; set; }
        public string AllAccountsComesFromSecondaryMarketing { get; set; }
        public string MaintFeePaymentBalance { get; set; }
        public string MaintFeePaymentBalanceForReservation { get; set; }
    }

    public class Sampler
    {
        public string IsSampler { get; set; }
        public string SamplerExpirationDate { get; set; }
        public string HomeProjectNumber { get; set; }
    }

    public class MetaData7
    {
        public string CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string ReferenceID { get; set; }
    }

    public class VacationClubPoint
    {
        public MetaData7 MetaData { get; set; }
        public string AccountNumber { get; set; }
        public string PointType { get; set; }
        public string PointTypeDescription { get; set; }
        public string PointBalance { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string NextEarnDate { get; set; }
        public string NextEarnAmount { get; set; }
        public string SecondaryMarket { get; set; }
    }

    public class MetaData8
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
        public string SavePointsFee { get; set; }
        public string SavePointsMessage { get; set; }
        public string SavePointsPenalty { get; set; }
        public string SavePointsPopup { get; set; }
        public string SavePointsEligible { get; set; }
        public string SavePointsPopupNextDate { get; set; }
        public string SavePointsMessageID { get; set; }
        public string SavePointsOptionPaid { get; set; }
        public MetaData8 MetaData { get; set; }
    }

    public class Points
    {
        public List<VacationClubPoint> VacationClubPoint { get; set; }
        public string TotalSecondaryMarketPoints { get; set; }
        public int TotalFuturePoints { get; set; }          //MANUALLY CHANGED
        public int TotalAnnualPoints { get; set; }          //MANUALLY CHANGED
        public int TotalSavedPoints { get; set; }           //MANUALLY CHANGED
        public int TotalRestrictedPoints { get; set; }      //MANUALLY CHANGED
        public int TotalPoints { get; set; }                //MANUALLY CHANGED
        public SavePoints SavePoints { get; set; }
    }

    public class MetaData9
    {
    }

    public class Legacy2
    {
        public string ProjectNumber { get; set; }
        public string HomeProjectNumber { get; set; }
        public string Expiration { get; set; }
        public string ProjectName { get; set; }
        public string VIP { get; set; }
        public string MRCHID { get; set; }
        public string BILMNT { get; set; }
        public string OLDACT { get; set; }
        public string ARDAFL { get; set; }
        public string ARDAMT1 { get; set; }
    }

    public class Financial
    {
        public string PaymentDate { get; set; }
        public string LastPaymentAmount { get; set; }
        public string BalanceCurrent { get; set; }
        public string Balance30 { get; set; }
        public string Balance60 { get; set; }
        public string Balance90 { get; set; }
        public string Balance120 { get; set; }
        public string CollectionCode { get; set; }
        public string TotalDueAmount { get; set; }
        public string OverdueAmount { get; set; }
        public string AccountOverdue { get; set; }
    }

    public class AccountInfo
    {
        public MetaData9 MetaData { get; set; }
        public string PurchaseDate { get; set; }
        public string AccountNumber { get; set; }
        public string PointType { get; set; }
        public string PointTypeDescription { get; set; }
        public string PointBalance { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string NextEarnDate { get; set; }
        public string NextEarnAmount { get; set; }
        public string SecondaryMarket { get; set; }
        public Legacy2 Legacy { get; set; }
        public string Weeks { get; set; }
        public string SaleType { get; set; }
        public Financial Financial { get; set; }
    }

    public class Accounts
    {
        public List<AccountInfo> AccountInfo { get; set; }
    }

    public class Paperless
    {
        public string PaperlessRequested { get; set; }
        public string PaperlessDelivered { get; set; }
        public string SetPaperlessOp { get; set; }
    }

    public class VacationClubMembership
    {
        public Membership4 Membership { get; set; }
        public string AnniversaryStartDate { get; set; }
        public string AnniversaryEndDate { get; set; }
        public string PrimaryAccountNumber { get; set; }
        public Legacy Legacy { get; set; }
        public Sampler Sampler { get; set; }
        public Points Points { get; set; }
        public Accounts Accounts { get; set; }
        public Paperless Paperless { get; set; }
    }

    public class MetaData10
    {
        public string CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string ReferenceID { get; set; }
    }

    public class MembershipSecurity5
    {
        public string Authenticated { get; set; }
    }

    public class Membership5
    {
        public string Identifier { get; set; }
        public string Authenticated { get; set; }
        public string MembershipLevel { get; set; }
        public string MembershipLevelDescription { get; set; }
        public string MemberNumber { get; set; }
        public string MembershipName { get; set; }
        public string MembershipInGoodStanding { get; set; }
        public string MembershipExpired { get; set; }
        public string MembershipExpirationDate { get; set; }
        public MetaData10 MetaData { get; set; }
        public MembershipSecurity5 MembershipSecurity { get; set; }
    }

    public class MetaData11
    {
        public string CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string ReferenceID { get; set; }
    }

    public class Rewards
    {
        public MetaData11 MetaData { get; set; }
        public string TotalRewardsEarned { get; set; }
        public string NextRewardsExpiringDate { get; set; }
        public string NextExpiringRewards { get; set; }
        public string RewardsBalance { get; set; }
    }

    public class Legacy3
    {
        public string BGRewardsOwnerID { get; set; }
    }

    public class BluegreenRewardsMembership
    {
        public Membership5 Membership { get; set; }
        public Rewards Rewards { get; set; }
        public Legacy3 Legacy { get; set; }
    }

    public class MetaData12
    {
        public string CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string ReferenceID { get; set; }
    }

    public class MembershipSecurity6
    {
        public string Authenticated { get; set; }
    }

    public class Membership6
    {
        public string Identifier { get; set; }
        public string Authenticated { get; set; }
        public string MembershipLevel { get; set; }
        public string MembershipLevelDescription { get; set; }
        public string MemberNumber { get; set; }
        public string MembershipName { get; set; }
        public string MembershipInGoodStanding { get; set; }
        public string MembershipExpired { get; set; }
        public string MembershipExpirationDate { get; set; }
        public MetaData12 MetaData { get; set; }
        public MembershipSecurity6 MembershipSecurity { get; set; }
    }

    public class RCIMembership
    {
        public Membership6 Membership { get; set; }
        public string WeekNumber { get; set; }
        public string PointNumber { get; set; }
    }

    public class MetaData13
    {
        public string CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string ReferenceID { get; set; }
    }

    public class MembershipSecurity7
    {
        public string Authenticated { get; set; }
    }

    public class Membership7
    {
        public string Identifier { get; set; }
        public string Authenticated { get; set; }
        public string MembershipLevel { get; set; }
        public string MembershipLevelDescription { get; set; }
        public string MemberNumber { get; set; }
        public string MembershipName { get; set; }
        public string MembershipInGoodStanding { get; set; }
        public string MembershipExpired { get; set; }
        public string MembershipExpirationDate { get; set; }
        public MetaData13 MetaData { get; set; }
        public MembershipSecurity7 MembershipSecurity { get; set; }
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

    public class TravelerPlusMembership
    {
        public string TravelerPlusEligible { get; set; }
        public Membership7 Membership { get; set; }
        public Renewal Renewal { get; set; }
    }

    public class Memberships
    {
        public Membership Membership { get; set; }
        public BluegreenOnlineMembership BluegreenOnlineMembership { get; set; }
        public SamplerMembership SamplerMembership { get; set; }
        public VacationClubMembership VacationClubMembership { get; set; }
        public BluegreenRewardsMembership BluegreenRewardsMembership { get; set; }
        public RCIMembership RCIMembership { get; set; }
        public TravelerPlusMembership TravelerPlusMembership { get; set; }
    }

    public class Account
    {
        public Memberships Memberships { get; set; }
    }

    public class CreditCardPayment
    {
        public string CreditCardType { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardExpirationDate { get; set; }
        public string CreditCardCVV { get; set; }
    }

    public class InstallmentPayment
    {
        public int Index { get; set; }
        public int InstallmentPaymentAmount { get; set; }
        public string InstallmentPaymentDate { get; set; }
        public int InstallmentPaymentPaid { get; set; }
    }

    public class InstallmentPlan
    {
        public int Index { get; set; }
        public List<CreditCardPayment> CreditCardPayments { get; set; }
        public string InstallmentPlanStatus { get; set; }
        public string AccountNumber { get; set; }
        public string AmountDueTotal { get; set; }
        public List<InstallmentPayment> InstallmentPayments { get; set; }
    }

    public class InstallmentPlans
    {
        public List<InstallmentPlan> InstallmentPlan { get; set; }
    }

    public class Financial2
    {
        public InstallmentPlans InstallmentPlans { get; set; }
        public string InstallmentPlanEligible { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }

    public class CreditCardPayment2
    {
        public string TransactionID { get; set; }
        public string AuthorizationNumber { get; set; }
    }

    public class RestOwnerPointsResponse
    {
        public string Identifier { get; set; }
        public List<Person> People { get; set; }
        public Account Account { get; set; }
        public Financial2 Financial { get; set; }
        public List<Error> Errors { get; set; }
        public CreditCardPayment2 CreditCardPayment { get; set; }
    }


}