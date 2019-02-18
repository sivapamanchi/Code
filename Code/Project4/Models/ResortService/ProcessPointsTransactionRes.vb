Namespace ProcessPointsRes
  
    Public Class [Error]
        Public Property Code As String
        Public Property ShortText As String
    End Class

    Public Class ProcessPointsTransactionRes
        Public Property Errors As List(Of [Error]) = New List(Of [Error])()
        Public Property OwnerID As Integer
    End Class
End Namespace
