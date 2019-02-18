Namespace PurchaseReq
    Public Class PurchasePointsRequest
        Public Property ChoicePrivilegeID As String
        Public Property Person As List(Of Person)
        Public Property Order As List(Of Orders)
    End Class
    Public Class Person
        Public Property FirstName As String
        Public Property LastName As String
        Public Property MiddleName As String
    End Class
    Public Class Orders
        Public Property Points As Integer
        Public Property ProductType As String
    End Class
End Namespace
