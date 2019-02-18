Namespace InsertAcctResponse
   Public Class Person
        Public Property FirstName As String
        Public Property LastName As String
        Public Property PrevFirstName As String
        Public Property PrevLastName As String
    End Class

    Public Class MetaData
        Public Property Requester As String
        Public Property CreateDate As String
        Public Property Action As String
    End Class

    Public Class Account
        Public Property Identifier As Integer
        Public Property OwnerID As Integer
        Public Property ChoicePrivilegeID As String
        Public Property IsActive As String
        Public Property Person As Person
        Public Property MetaData As MetaData
    End Class
    Public Class InsertAcctResponse
        Public Property Accounts As List(Of Account)
        Public Property Errors As List(Of ResponseError)
        Public Property Success As String
    End Class
    Public Class ResponseError
        Public Property Code As String
        Public Property ShortText As String
    End Class
End Namespace


