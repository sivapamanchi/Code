Namespace EligibleVotersResp
    Public Class GetAllActiveEligibleVotersResponse
        Public Property Status As String
        Public Property StatusDescription As String
        Public Property Records As List(Of Record)
    End Class
    Public Class Record
        Public Property EligibleVotersID As String
        Public Property VotingYear As String
        Public Property EligibleVoters As String
        Public Property IsActive As String
        Public Property ModifiedBy As String
        Public Property ModifiedDate As String
    End Class
End Namespace
