Public Class IVRRequest
#Region "Private Fields"
    Private _Arvact As String
    Private _CustomerId As String
    Private _ANIPhone As String
    Private _ValidationPhone As String
    Private _MenuId As String
    Private _Disposition As String
    Public _TransaferId As String
#End Region

#Region "Properties"
    Public Property Arvact() As String
        Get
            Return _Arvact
        End Get
        Set(ByVal value As String)
            _Arvact = value
        End Set
    End Property

    Public Property CustomerId() As String
        Get
            Return _CustomerId
        End Get
        Set(ByVal value As String)
            _CustomerId = value
        End Set
    End Property

    Public Property ANIPhone() As String
        Get
            Return _ANIPhone
        End Get
        Set(ByVal value As String)
            _ANIPhone = value
        End Set
    End Property

    Public Property ValidationPhone() As String
        Get
            Return _ValidationPhone
        End Get
        Set(ByVal value As String)
            _ValidationPhone = value
        End Set
    End Property

    Public Property MenuId() As String
        Get
            Return _MenuId
        End Get
        Set(ByVal value As String)
            _MenuId = value
        End Set
    End Property

    Public Property Disposition() As String
        Get
            Return _Disposition
        End Get
        Set(ByVal value As String)
            _Disposition = value
        End Set
    End Property

    Public Property TransaferId() As String
        Get
            Return _TransaferId
        End Get
        Set(ByVal value As String)
            _TransaferId = value
        End Set
    End Property

   

#End Region
End Class
