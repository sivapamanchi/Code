Namespace SearchResponse
    Public Class Address
        Public Property City As String
        Public Property ProvinceCode As String
        Public Property PostalCode As String
        Public Property CountryCode As String
    End Class

    Public Class EmailAddress
        Public Property Email As String
    End Class

    Public Class Person
        Public Property FirstName As String
        Public Property LastName As String
        Public Property Prefix As String
        Public Property Addresses As List(Of Address)
        Public Property EmailAddresses As EmailAddress()
        Public Property MembershipID As String
    End Class
    Public Class ResponseError
        Public Property Code As String
        Public Property ShortText As String
    End Class
    Public Class ProfileSearchResponse
        Public Property People As List(Of Person)
        Public Property Errors As List(Of ResponseError)
        Public Property Success As String
    End Class
End Namespace
