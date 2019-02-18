Namespace ActiveCandidatebyYearResp
    Public Class GetActiveProxyCandidatesByVotingYearResponse
        Public Property Status As String
        Public Property StatusDescription As String
        Public Property Records As List(Of Record)
    End Class
    Public Class Record
        Public Property CandidateID As String
        Public Property CandidateName As String
        Public Property CandidateOwnerID As String
        Public Property VotingYear As String
        Public Property IsActive As String
        Public Property IsWriteInCandidate As String
        Public Property ModifiedDate As String
        Public Property ModifiedBy As String
    End Class

End Namespace


