Namespace EditAccount
    Public Class Person
        Public Property FirstName As String
        Public Property LastName As String
    End Class

    Public Class EditedAccount
        Public Property Person As List(Of Person)
        Public Property OwnerID As String
        Public Property ChoicePrivilegeID As String
        Public Property Identifier As String
        Public Property Requester As String
        Public Property TravelerplusMember As String
    End Class

End Namespace