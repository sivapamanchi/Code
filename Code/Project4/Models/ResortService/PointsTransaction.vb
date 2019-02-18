Namespace PointsTrans
    Public Enum TransactionFunction
        CHOICE
        HOTWEEKS
        RCI
        DIRECTEX
        WORLD
        HOTELPT
        CTC
        CRUISE
    End Enum
    Public Enum SiteName
        BluegreenOnline
        TPPoints
        CTC
        ShellPoints
        OnlinePoints
        Prizzma
        OutdoorTraveler
        Promo2
        iPhone
        Choice
        VSSA
    End Enum
    Public Enum PointTransactionType
        DeductReservationPoints
        RefundReservationPoints
        ShortenReservationPoint
        ExtendReservationPoints
        'DEDUCT
        'REFUND
        'SHORTEN
        'EXTEND
    End Enum
    Public Class PointsTransaction

        Public Sub New()
            Message = ""
            Success = 0
        End Sub

        Public Property PointTransactionType As String 'PointTransactionType

        Public Property Functions As String

        Public Property SiteName As String

        Public Property OwnerID As String

        Public Property EffectiveDate As String

        Public Property Reference As String

        Public Property TransactionPoints As Integer

        Public Property Agent As String

        Public Property Success As Integer
        Public Property Points As Integer

        Public Property Message As String

        Public Property Comment As String
    End Class
End Namespace
