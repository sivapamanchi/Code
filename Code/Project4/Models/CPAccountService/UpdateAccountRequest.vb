Namespace UpAcctReq
    Public Class UpdateAccountRequest
        Public Property ChoicePrivilegeID As String
        Public Property Person As List(Of Person)
        Public Property OwnerID As String
        Public Property Requester As String
        Public Property Identifier As Integer
    End Class
    Public Class Person
        Public Property FirstName As String
        Public Property LastName As String
    End Class
End Namespace

