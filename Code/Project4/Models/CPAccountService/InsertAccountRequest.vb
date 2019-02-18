Namespace InsertAcct
    Public Class InsertAccountRequest
        Public Property ChoicePrivilegeID As String
        Public Property OwnerID As String
        Public Property Person As List(Of Person)

        Public Property Requester As String
    End Class
    Public Class Person
        Public Property FirstName As String
        Public Property LastName As String
    End Class
End Namespace

