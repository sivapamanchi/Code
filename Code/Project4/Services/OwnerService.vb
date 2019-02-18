Imports System.Net
Imports Newtonsoft.Json
Imports VSSA.ptsBucketReq
Imports VSSA.ptsBucketRes
Imports VSSA.BoomiWS
Imports VSSA.fetchresp
Imports Newtonsoft.Json.Linq
Imports VSSA.FetchReq
Imports VSSA.OwnerAcctResp
Imports VSSA.OwnerAcctReq

Namespace OwnerServices
    Public Class OwnerService
        Public Property Accounts As List(Of fetchresp.Accounts)

        Private Function BuildEndpoint(ByVal endpoint As String) As String
            ServicePointManager.ServerCertificateValidationCallback = (Function(sender, certificate, chain, sslPolicyErrors) True)
            Dim result As String = ConfigurationManager.AppSettings("RestOwnerServiceEndpoint")
            Return result & endpoint
        End Function

        Public Function OwnerFetch(ByVal ownerId As String) As OwnerFetchResponse
            Dim result As OwnerFetchResponse = Nothing
            Dim request As New FetchReq.OwnerFetchRequest
            request.OwnerID = ownerId
            result = OwnerInfo(request)
            Return result
        End Function
        Public Function OwnerInfo(ByVal request As OwnerFetchRequest) As OwnerFetchResponse
            Dim result As OwnerFetchResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/OwnerFetch")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()


                result = JsonConvert.DeserializeObject(Of OwnerFetchResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function OwnerAccounts(ByVal ownerId As String) As OwnerAcctResponse
            Dim result As OwnerAcctResponse = Nothing
            Dim request As New OwnerAcctReq.OwnerAcctRequest
            request.OwnerID = ownerId
            result = OwnerAcctInfo(request)
            Return result
        End Function
        Public Function OwnerAcctInfo(ByVal request As OwnerAcctRequest) As OwnerAcctResponse
            Dim result As OwnerAcctResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/OwnerAccounts")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of OwnerAcctResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function fetchOwnerPointsBuckets(ByVal ownerId As String) As ptsBucketRes.PointsBucketResponse
            Dim result As ptsBucketRes.PointsBucketResponse = Nothing
            Dim request As New ptsBucketReq.PointsBucketRequest
            request.OwnerID = ownerId
            result = OwnerPointsBucketsList(request)
            If ((Not (result) Is Nothing) AndAlso (Not (result.Account) Is Nothing)) Then
                'result.Account = result.Account.GroupBy(Function(test) test.Identifier).[Select](Function(grp) grp.First()).ToList()
            End If
            Return result
        End Function
        Public Function OwnerPointsBucketsList(ByVal request As ptsBucketReq.PointsBucketRequest) As ptsBucketRes.PointsBucketResponse
            Dim result As ptsBucketRes.PointsBucketResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/OwnerPoints")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of ptsBucketRes.PointsBucketResponse)(json)
                If ((Not (result) Is Nothing) AndAlso (Not (result.Account) Is Nothing)) Then

                    'result.Account = result.Account.GroupBy(Function(test) test..Identifier).[Select](Function(grp) grp.First()).ToList()
                End If
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function FetchAS400SavePointsBucket(ByVal as400points As AS400Req.AS400PointsRequest)
            Dim result As AS400Resp.AS400PointsResponse = Nothing
            Dim request As New AS400Req.AS400PointsRequest
            request.Arvact = as400points.Arvact
            request.ListType = as400points.ListType
            request.PointTypes = as400points.PointTypes
            result = AS400SavePointsBucket(request)
            Return result
        End Function
        Public Function AS400SavePointsBucket(ByVal request As AS400Req.AS400PointsRequest) As AS400Resp.AS400PointsResponse
            Dim result As AS400Resp.AS400PointsResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/FetchAS400SavePointsBucket")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of AS400Resp.AS400PointsResponse)(json)
                Return result
            Catch ex As Exception
            End Try
            Return result
        End Function

    End Class
End Namespace
