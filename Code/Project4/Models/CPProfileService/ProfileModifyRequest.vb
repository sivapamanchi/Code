Namespace ModifReq
    Public Class ProfileModifyRequest
        Public Property ChoicePrivilegeID As String
        Public Property OwnerID As String
        Public Property Person As List(Of Person)
        Public Property TravelerPlusMember As String

    End Class
    Public Class RootObject

        Public Property logInResult As List(Of ProfileModifyRequest)
    End Class
    Public Class Person
        Public Property FirstName As String
        Public Property LastName As String
    End Class
End Namespace

