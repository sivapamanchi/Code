Namespace SearchReq
    Public Class ProfileSearchRequest
        Public Property ChoicePrivilegeID As String
        Public Property Person As List(Of Person)
    End Class
    Public Class Person
        Public Property FirstName As String
        Public Property LastName As String
    End Class
End Namespace
