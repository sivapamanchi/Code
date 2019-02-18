Imports System.Net
Imports Newtonsoft.Json
Imports System.Globalization
Namespace ResortServices
    Public Class ResortService
        Private Function BuildEndpoint(ByVal endpoint As String) As String
            ServicePointManager.ServerCertificateValidationCallback = (Function(sender, certificate, chain, sslPolicyErrors) True)
            Dim result As String = ConfigurationManager.AppSettings("RestResortServiceEndpoint")
            Return result & endpoint
        End Function
        Public Function GenerateRefNumber() As String
            Dim randNumber As String = ""
            Dim r As Random = New Random()

            Dim randNum As Integer = r.[Next](1000000)

            Dim sevenDigitNumber As String = randNum.ToString("D7")
            Try
                randNumber = "NR-" + sevenDigitNumber
            Catch ex As Exception
            End Try
            Return randNumber
        End Function
        Public Function TotalPointsTransaction(ByVal totpoints As TotPointsReq.TotalPointsRequest)
            Dim result As TotPointsResp.TotalPointsResponse = Nothing
            Dim request As New TotPointsReq.TotalPointsRequest()
            Try
                request.SiteName = totpoints.SiteName
                request.OwnerID = totpoints.OwnerID
                request.Functions = totpoints.Functions
                request.EffectiveDate = totpoints.EffectiveDate
                result = TotalPointsList(request)
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function TotalPointsList(ByVal request As TotPointsReq.TotalPointsRequest) As TotPointsResp.TotalPointsResponse
            Dim result As TotPointsResp.TotalPointsResponse = Nothing

            Try
                Dim endPoint As String = BuildEndpoint("/TotalPointsTransaction")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of TotPointsResp.TotalPointsResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function

        Public Function WaiversAvailable(ByVal ownwaivers As WaiversReq.OwnerWaiversRequest)
            Dim result As WaiversResp.OwnerWaiversResponse = Nothing
            Dim request As New WaiversReq.OwnerWaiversRequest()
            Try
                request.OwnerID = ownwaivers.OwnerID
               
                result = WaiversList(request)
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function WaiversList(ByVal request As WaiversReq.OwnerWaiversRequest) As WaiversResp.OwnerWaiversResponse
            Dim result As WaiversResp.OwnerWaiversResponse = Nothing

            Try
                Dim endPoint As String = BuildEndpoint("/OwnerWaivers")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of WaiversResp.OwnerWaiversResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function

        Public Function ProcessPointsTransaction(ByVal totpoints As PointsTrans.PointsTransaction)
            Dim result As ProcessPointsRes.ProcessPointsTransactionRes = Nothing
            Dim request As New ProcessPointsReq.ProcessPointsTransactionReq()
            Try
                request.SiteName = totpoints.SiteName
                request.OwnerID = totpoints.OwnerID
                request.Functions = totpoints.Functions
                request.EffectiveDate = totpoints.EffectiveDate
                request.Points = totpoints.TransactionPoints
                request.PointsTransactionType = totpoints.PointTransactionType
                request.Agent = totpoints.Agent
                request.Comment = totpoints.Comment
                request.ReservationNumber = totpoints.Reference
                result = ProcessPointsList(request)
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function ProcessPointsList(ByVal request As ProcessPointsReq.ProcessPointsTransactionReq) As ProcessPointsRes.ProcessPointsTransactionRes
            Dim result As ProcessPointsRes.ProcessPointsTransactionRes = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/ProcessPointsTransaction")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of ProcessPointsRes.ProcessPointsTransactionRes)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
    End Class
End Namespace