Imports VSSA.TotPointsResp
Imports VSSA.AcctResponse

Module [Global]

    Dim _ownerTotalPoints As TotalPointsResponse
    Dim _cpAccounts As CPRetrieveAcctResponse
    Public Property CPAccounts As CPRetrieveAcctResponse
        Get
            Return _cpAccounts
        End Get

        Set(ByVal value As CPRetrieveAcctResponse)
            _cpAccounts = value
        End Set
    End Property
    Public Property OwnerTotalPoints As TotalPointsResponse
        Get
            Return _ownerTotalPoints
        End Get

        Set(ByVal value As TotalPointsResponse)
            _ownerTotalPoints = value
        End Set
    End Property
End Module
