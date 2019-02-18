Namespace ptsBucketRes
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

    Public Class VacationClubMembership
        Public Property Points As Points
    End Class

    Public Class Memberships
        Public Property VacationClubMembership As VacationClubMembership
    End Class

    Public Class Account
        Public Property Memberships As Memberships
    End Class

    Public Class PointsBucketResponse
        Public Property Identifier As String
        Public Property Account As Account
    End Class

End Namespace
