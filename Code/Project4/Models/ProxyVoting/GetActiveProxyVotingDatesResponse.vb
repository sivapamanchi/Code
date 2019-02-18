Namespace ActiveVotingDatesResp
    Public Class GetActiveProxyVotingDatesResponse
        Public Property Status As String
        Public Property StatusDescription As String
        Public Property Records As List(Of Record)
    End Class
    Public Class Record
        Public Property ProxyDateID As String
        Public Property VotingYear As String
        Public Property StartDate As String
        Public Property EndDate As String
        Public Property ModifiedBy As String
        Public Property ModifiedDate As String
        Public Property IsActive As String
    End Class
End Namespace
