
Public Class RetrieveAccountRequest
    Public Property Identifier As String
    Public Property ChoicePrivilegeID As String
    Public Property OwnerID As String
    Public Property FirstName As String
    Public Property LastName As String
    Public Property Requester As String
End Class
Public Class InsertInfo
    Public Property NewAccountInfo As List(Of RetrieveAccountRequest)
End Class
