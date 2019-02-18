Imports Newtonsoft.Json

Namespace BoomiWS

    Public Class PhoneNumber
        Public Property PhoneNumber As String
    End Class

    Public Class Address
        Public Property AddressLine1 As String
        Public Property AddressLine2 As String
        Public Property City As String
        Public Property ProvinceCode As String
        Public Property Subdivison As String
        Public Property PostalCode As String
        Public Property CountryCode As String
    End Class

    Public Class EmailAddress
        Public Property Email As String
    End Class

    Public Class Security
        Public Property UserName As String
        Public Property FirstLogin As String
        Public Property LastLogin As String
        Public Property LoginCount As String
        Public Property ChangePassword As String
    End Class

    Public Class People
        Public Property FullName As String
        Public Property FirstName As String
        Public Property LastName As String
        Public Property PhoneNumbers As List(Of PhoneNumber)
        Public Property Addresses As List(Of Address)
        Public Property EmailAddresses As List(Of EmailAddress)
        Public Property TaxID As String
        Public Property Security As Security
    End Class

    Public Class Membership
        Public Property MembershipInGoodStanding As String
        Public Property MembershipLevel As String
        Public Property MembershipExpired As Boolean
        Public Property MembershipExpirationDate As String
        Public Property Identifier As String
        Public Property TravelerPlusEligible As Boolean
    End Class

    Public Class BluegreenOnlineMembership
        Public Property Membership As Membership
    End Class

    Public Class Legacy
        Public Property AtLeastOneAccountQualify4MarketCampaign As String
        Public Property MaintFeePaymentBalanceForReservation As String
        Public Property ProjectNumber As Integer
        Public Property HomeProjectNumber As Integer
        Public Property Expiration As String
        Public Property ProjectName As String

    End Class

    Public Class Sampler
        Public Property IsSampler As String
    End Class

    Public Class MetaData
        Public Property ReferenceID As String
    End Class

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

    Public Class Points
        Public Property VacationClubPoint As List(Of VacationClubPoint)
        Public Property TotalSecondaryMarketPoints As String
        Public Property TotalFuturePoints As String
        Public Property TotalAnnualPoints As String
        Public Property TotalSavedPoints As String
        Public Property TotalRestrictedPoints As String
        Public Property TotalPoints As String
    End Class

    Public Class AccountInfo
        Public Property PurchaseDate As String
        Public Property AccountNumber As String
        Public Property Legacy As Legacy
    End Class

    Public Class Accounts
        Public Property AccountInfo As List(Of AccountInfo)
    End Class

    Public Class Paperless
        Public Property PaperlessDelivered As String
    End Class

    Public Class VacationClubMembership
        Public Property Membership As Membership
        Public Property Legacy As Legacy
        Public Property Sampler As Sampler
        Public Property Points As Points
        Public Property Accounts As Accounts
        Public Property Paperless As Paperless
    End Class

    Public Class BluegreenRewardsMembership
        Public Property Membership As Membership
    End Class

    Public Class RCIMembership
        Public Property WeekNumber As String
        Public Property PointNumber As String
    End Class

    Public Class Renewal
        Public Property AutoRenewal As String
        Public Property RenewalFee As String
        Public Property RenewalMessage As String
        Public Property AutoRenewalFee As String
        Public Property RenewalPenalty As String
        Public Property RenewalPopup As String
        Public Property CreditCardExpiration As String
    End Class

    Public Class TravelerPlusMembership
        Public Property Membership As Membership
        Public Property Renewal As Renewal
    End Class

    Public Class Memberships
        Public Property BluegreenOnlineMembership As BluegreenOnlineMembership
        Public Property VacationClubMembership As VacationClubMembership
        Public Property BluegreenRewardsMembership As BluegreenRewardsMembership
        Public Property RCIMembership As RCIMembership
        Public Property TravelerPlusMembership As TravelerPlusMembership
    End Class
    Public Class InstallmentPlan
        Public Property AmountDueTotal As String
        Public Property InstallmentPayments As InstallmentPayments
    End Class
    Public Class InstallmentPayments
        Public Property InstallmentPaymentAmount As String
        Public Property InstallmentPaymentPaid As String
    End Class
    Public Class Account
        Public Property Memberships As Memberships
    End Class
    Public Class Financial
        Public Property InstallmentPlans As InstallmentPlan
    End Class

    
    Public Class Owner
        Public Property Identifier As String
        Public Property People As List(Of People)
        Public Property Account As List(Of Account)
        Public Property Financial As List(Of Financial)
    End Class
   
End Namespace