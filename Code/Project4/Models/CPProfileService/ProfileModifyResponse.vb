Namespace ModifyResponse
    Public Class ResponseError
        Public Property Code As String
        Public Property ShortText As String
    End Class
    Public Class ProfileModifyResponse
        Public Property Errors As List(Of ResponseError)
        Public Property Success As String
    End Class
End Namespace

