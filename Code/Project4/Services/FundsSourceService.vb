Imports VSSA.HttpUtils
Imports Newtonsoft.Json
Imports System.Net
Imports VSSA.BoomiFundsSourceWS
Imports System.String

Namespace BoomiFundsSourceWS
    Public Class FundsSourceService
        Public Function BuildEndpoint(ByVal endpoint As String) As String
            ServicePointManager.ServerCertificateValidationCallback = (Function(sender, certificate, chain, sslPolicyErrors) True)
            Dim result As String = ConfigurationManager.AppSettings("RestFundsSourceServiceEndpoint")
            Return result & endpoint
        End Function
        Public Function ProcessTransaction(ByVal request As TransactionRequestData) As TransactionResponseData

            Dim result As TransactionResponseData = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/ProcessTransaction")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request).Replace("[", "").Replace("]", "")
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of TransactionResponseData)(json)
            Catch ex As Exception
                Return result
            End Try
            Return result
        End Function
    End Class
End Namespace
