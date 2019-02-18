Namespace ArvactandVotingYearResp
    Public Class GetActiveProxyVoteByArvactAndVotingYearResponse
        Public Property Status As String
        Public Property StatusDescription As String
        Public Property Records As List(Of Record)
    End Class
    Public Class Record
        Public Property ProxyVoteID As String
        Public Property CandidateName As String
        Public Property VotingYear As String
        Public Property ModifiedBy As String
        Public Property ModifiedDate As String
    End Class
End Namespace
