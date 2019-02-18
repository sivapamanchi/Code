Namespace TotPointsResp
    Public Class [Error]
        Public Property Code As String
        Public Property ShortText As String
    End Class
    Public Class TotalPointsResponse
        Public Property Points As Integer
        Public Property Errors As List(Of [Error]) = New List(Of [Error])()
    End Class
End Namespace
