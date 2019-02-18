
Namespace purchaseResponse
    Public Class ResponseError
        Public Property Code As String
        Public Property ShortText As String
    End Class
    Public Class PurchasePointsResponse
        Public Property Errors As List(Of ResponseError)
        Public Property Success As Boolean
    End Class
End Namespace

