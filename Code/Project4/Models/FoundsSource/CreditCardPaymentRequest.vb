Namespace BoomiFundsSourceWS
    Public Class TransactionRequestData
        Public Property CreditCardType As String
        Public Property CreditCardNumber As String
        Public Property TransarmorToken As String
        Public Property CreditCardExpirationDate As String
        Public Property CreditCardCVV As String
        Public Property PaymentAmount As String
        Public Property MerchantAccountNumber As String
        Public Property TransactionType As String
        Public Property NameOnTheCard As String
        Public Property Address1 As String
        Public Property Address2 As String
        Public Property City As String
        Public Property State As String
        Public Property Country As String
        Public Property PostalCode As String
        Public Property AuthCode As String
        Public Property TransactionID As String
        Public Property MetaData As List(Of TransactionRequestDataMetaData)

    End Class

    Public Class TransactionRequestDataMetaData
        Public Property CreateDate As String
        Public Property CreatedBy As String
        Public Property ModifiedBy As String
        Public Property ModifiedDate As String
        Public Property Size As String
        Public Property Description As String
        Public Property ReferenceID As String
    End Class
End Namespace