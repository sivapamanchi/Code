Namespace ActivevotersResp
    Public Class GetActiveDesignatedVoterByArvactResponse
        Public Property Records As List(Of Record)
        Public Property Errors As List(Of ResponseError)
    End Class
    Public Class ResponseError
        Public Property Status As String
        Public Property StatusDescription As String
    End Class
    Public Class Record
        Public Property DesignatedVoterID As String
        Public Property OwnerArvact As String
        Public Property DesignatedVoterName As String
        Public Property ModifiedBy As String
        Public Property DateCreated As String
    End Class
End Namespace