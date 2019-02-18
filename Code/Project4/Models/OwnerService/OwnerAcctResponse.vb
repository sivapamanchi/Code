Namespace OwnerAcctResp
    <Serializable>
    Public Class Person
        Public Property FullName As String
    End Class
    <Serializable>
    Public Class Legacy
        Public Property AtLeastOneAccountQualify4MarketCampaign As String
        Public Property ProjectNumber As Integer
        Public Property HomeProjectNumber As Integer
        Public Property Expiration As String
        Public Property ProjectName As String
    End Class
    <Serializable>
    Public Class Sampler
        Public Property IsSampler As String
    End Class
    <Serializable>
    Public Class AccountInfo
        Public Property PurchaseDate As String
        Public Property AccountNumber As String
        Public Property NextEarnAmount As String
        Public Property Legacy As Legacy
    End Class
    <Serializable>
    Public Class Accounts
        Public Property AccountInfo As List(Of AccountInfo)
    End Class
    <Serializable>
    Public Class VacationClubMembership
        Public Property PrimaryAccountNumber As String
        Public Property Legacy As Legacy
        Public Property Sampler As Sampler
        Public Property Accounts As Accounts
    End Class

    Public Class Memberships
        Public Property VacationClubMembership As VacationClubMembership
    End Class
    <Serializable>
    Public Class Account
        Public Property Memberships As Memberships
    End Class
    <Serializable>
    Public Class OwnerAcctResponse
        Public Property Identifier As String
        Public Property People As Person()
        Public Property Account As Account
    End Class
End Namespace
