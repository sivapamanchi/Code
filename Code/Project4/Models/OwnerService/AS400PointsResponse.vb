Namespace AS400Resp
    Public Class AS400PointsResponse
        Public Property Errors As List(Of ResponseError)
        Public Property PointsRecord As List(Of PointsRecords)

        Public Class ResponseError
        Public Property Code As String
        Public Property ShortText As String
    End Class
        Public Class PointsRecords
            Public Property AcctNum As String
            Public Property ExpireDate As String
            Public Property PointBal As String
            Public Property Action As String
            Public Property PointTypeDesc As String
        End Class
    End Class
End Namespace
