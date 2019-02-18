Imports Newtonsoft.Json

Namespace fetchresp
    <Serializable>
       Public Class PhoneNumber
        Public Property PhoneNumber As String
    End Class
    <Serializable>
    Public Class Addresses
        Public Property AddressLine1 As String
        Public Property AddressLine2 As String
        Public Property City As String
        Public Property ProvinceCode As String
        Public Property Subdivison As String
        Public Property PostalCode As String
        Public Property CountryCode As String
    End Class
    <Serializable>
    Public Class EmailAddress
        Public Property Email As String
    End Class
    <Serializable>
    Public Class Security
        Public Property UserName As String
        Public Property FirstLogin As String
        Public Property LastLogin As String
        Public Property LoginCount As String
        Public Property ChangePassword As String
    End Class
    <Serializable>
    Public Class People
        Public Property FullName As String
        Public Property FirstName As String
        Public Property LastName As String
        Public Property PhoneNumbers As List(Of PhoneNumber)
        Public Property Addresses As List(Of Addresses)
        Public Property EmailAddresses As List(Of EmailAddress)
        Public Property TaxID As String
        Public Property Security As Security
    End Class
    <Serializable>
    Public Class Membership
        Public Property MembershipInGoodStanding As String
        Public Property MembershipLevelDescription As String
        Public Property MembershipLevel As String
        Public Property MembershipExpired As Boolean
        Public Property MembershipExpirationDate As String
        Public Property Identifier As String
        Public Property TravelerPlusEligible As Boolean
    End Class
    <Serializable>
    Public Class BluegreenOnlineMembership
        Public Property Membership As Membership
    End Class

    <Serializable>
    Public Class InstallmentPlans
        Public Property InstallmentPlan As List(Of InstallmentPlan)
    End Class
    <Serializable>
    Public Class InstallmentPlan
        Public Property InstallmentPlanStatus As String
        Public Property AccountNumber As String
        Public Property AmountDueTotal As String
        Public Property CreditCardPayments As List(Of CreditCardPayments)
        Public Property InstallmentPayments As List(Of InstallmentPayments)
    End Class
    <Serializable>
    Public Class MaintenanceFees
        Public Property MaintenanceFeeReservation As List(Of MaintenanceFeeReservation)
        Public Property MaintenanceFeePaymentBalance As List(Of MaintenanceFeePaymentBalance)
    End Class
    <Serializable>
    Public Class MaintenanceFeePaymentBalance
        Public Property Amount As String
        Public Property ProjectName As String
        Public Property ProjectNumber As String
        Public Property Weeks As String
        Public Property SalesType As String
    End Class
    <Serializable>
    Public Class MaintenanceFeeReservation
        Public Property Amount As String
        Public Property ProjectName As String
        Public Property ProjectNumber As String
        Public Property Weeks As String
        Public Property SalesType As String
    End Class
    <Serializable>
    Public Class BonusTime
        Public Property BlueWhiteCertificateEligible As String
    End Class

    <Serializable>
    Public Class Legacy
        Public Property AtLeastOneAccountQualify4MarketCampaign As String
        Public Property MaintFeePaymentBalanceForReservation As String
        Public Property MaintFeePaymentBalance As String
        Public Property ProjectNumber As Integer
        Public Property HomeProjectNumber As Integer
        Public Property Expiration As String
        Public Property AllAccountsComesFromSecondaryMarketing As String
        Public Property BonusTime As BonusTime
        Public Property ProjectName As String
        Public Property MaintenanceFees As MaintenanceFees
    End Class
    <Serializable>
    Public Class Sampler
        Public Property IsSampler As String
    End Class
    <Serializable>
    Public Class MetaData
        Public Property ReferenceID As String
    End Class
    <Serializable>
    Public Class VacationClubPoint
        Public Property MetaData As MetaData
        Public Property AccountNumber As String
        Public Property PointType As String
        Public Property PointTypeDescription As String
        Public Property PointBalance As String
        Public Property StartDate As String
        Public Property EndDate As String
        Public Property NextEarnDate As String
        Public Property NextEarnAmount As String
        Public Property SecondaryMarket As String
    End Class
    <Serializable>
    Public Class Points
        Public Property VacationClubPoint As List(Of VacationClubPoint)
        Public Property TotalSecondaryMarketPoints As String
        Public Property TotalFuturePoints As String
        Public Property TotalAnnualPoints As String
        Public Property TotalSavedPoints As String
        Public Property TotalRestrictedPoints As String
        Public Property TotalPoints As String
    End Class
  
    <Serializable>
    Public Class AccountInfo
        Public Property PurchaseDate As String
        Public Property AccountNumber As String
        Public Property Legacy As Legacy
        Public Property Financial As Financial
    End Class
    <Serializable>
    Public Class Accounts
        Public Property AccountInfo As List(Of AccountInfo)
    End Class
    <Serializable>
    Public Class Paperless
        Public Property PaperlessDelivered As String
    End Class
    <Serializable>
    Public Class VacationClubMembership
        Public Property Membership As Membership
        Public Property Legacy As Legacy
        Public Property Sampler As Sampler
        Public Property Points As Points
        Public Property Accounts As Accounts
        Public Property Paperless As Paperless
    End Class
    <Serializable>
    Public Class Rewards
        Public Property TotalRewardsEarned As String
        Public Property NextRewardsExpiringDate As String
        Public Property NextExpiringRewards As String
        Public Property RewardsBalance As String
        Public Property isBarclaysCardHolder As String
    End Class
    <Serializable>
    Public Class BluegreenRewardsMembership
        Public Property Membership As Membership
        Public Property Rewards As Rewards
    End Class
    <Serializable>
    Public Class RCIMembership
        Public Property WeekNumber As String
        Public Property PointNumber As String
    End Class
    <Serializable>
    Public Class Renewal
        Public Property AutoRenewal As String
        Public Property RenewalFee As String
        Public Property RenewalMessage As String
        Public Property AutoRenewalFee As String
        Public Property RenewalPenalty As String
        Public Property RenewalPopup As String
        Public Property CreditCardExpiration As String
    End Class
    <Serializable>
    Public Class TravelerPlusMembership
        Public Property TravelerPlusEligible As String
        Public Property Membership As Membership
        Public Property Renewal As Renewal
    End Class
    <Serializable>
    Public Class Profile
        Public Property Project As String
    End Class
    <Serializable>
    Public Class Memberships
        Public Property BluegreenOnlineMembership As BluegreenOnlineMembership
        Public Property VacationClubMembership As VacationClubMembership
        Public Property BluegreenRewardsMembership As BluegreenRewardsMembership
        Public Property RCIMembership As RCIMembership
        Public Property TravelerPlusMembership As TravelerPlusMembership
        Public Property Profile As Profile
    End Class

    '<Serializable>
    'Public Class InstallmentPlans
    '    Public Property InstallmentPlan As List(Of InstallmentPlan)
    'End Class
    '<Serializable>
    'Public Class InstallmentPlan
    '    Public Property InstallmentPlanStatus As String
    '    Public Property AccountNumber As String
    '    Public Property AmountDueTotal As String
    '    Public Property CreditCardPayments As List(Of CreditCardPayments)
    '    Public Property InstallmentPayments As List(Of InstallmentPayments)
    'End Class
    <Serializable>
    Public Class InstallmentPayments
        Public Property InstallmentPaymentAmount As String
        Public Property InstallmentPaymentDate As String
        Public Property InstallmentPaymentPaid As String
    End Class
    <Serializable>
    Public Class CreditCardPayments
        Public Property CreditCardType As String
        Public Property CreditCardExpirationDate As String
        Public Property CreditCardNumber As String
    End Class
    <Serializable>
    Public Class Account
        Public Property Memberships As Memberships
    End Class
    <Serializable>
    Public Class Financial
        Public Property InstallmentPlans As InstallmentPlans
        Public Property CollectionCode As String
        Public Property InstallmentPlanEligible As String
    End Class
    <Serializable>
   Public Class OwnerFetchResponse
        Public Property Identifier As String
        Public Property People As List(Of People)
        Public Property Account As Account
        Public Property Financial As Financial
    End Class
End Namespace
