Namespace BoomiFundsSourceWS

    Public Class TransactionResponseData

        Public Property PaymentAmount As String
        Public Property AuthCode As String
        Public Property TransactionID As String

        Public Property MetaData As TransactionResponseDataMetaData
        Public Property Errors As List(Of TransactionResponseDataError)
    End Class
    Public Class TransactionResponseDataError
        Public Property Code As String
        Public Property ShortText As String
    End Class

    Public Class TransactionResponseDataMetaData
        Public Property CreatedBy As String
        Public Property Description As String
        Public Property ReferenceID As String
    End Class

End Namespace
