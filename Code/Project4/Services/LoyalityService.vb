Imports System.Net
Imports Newtonsoft.Json


Namespace CPLoyalityServices
    Public Class LoyalityService
        Private Function BuildEndpoint(ByVal endpoint As String) As String
            ServicePointManager.ServerCertificateValidationCallback = (Function(sender, certificate, chain, sslPolicyErrors) True)
            Dim result As String = ConfigurationManager.AppSettings("RestLoyalityServiceEndpoint")
            Return result & endpoint
        End Function
        Public Function SendPoints(ByVal points As SendPoints.Points)
            Dim result As purchaseResponse.PurchasePointsResponse = Nothing
            Dim request As New PurchaseReq.PurchasePointsRequest()
            Try
                request.Person = points.Person.[Select](Function(x) New PurchaseReq.Person() With {.LastName = x.LastName}).ToList()
                request.Order = points.Order.[Select](Function(x) New PurchaseReq.Orders() With {.Points = x.Points, .ProductType = x.ProductType}).ToList()
                request.ChoicePrivilegeID = points.ChoicePrivilegeID
                result = PointsList(request)
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function PointsList(ByVal request As PurchaseReq.PurchasePointsRequest) As purchaseResponse.PurchasePointsResponse
            Dim result As purchaseResponse.PurchasePointsResponse = Nothing

            Try
                Dim endPoint As String = BuildEndpoint("/SendPoints?wsdl=single")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim jstringname = client.PostData.Replace("[", " ")
                Dim finalformat = jstringname.Replace("]", " ")
                client.PostData = finalformat
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of purchaseResponse.PurchasePointsResponse)(json)
                'If (result.Success) Then

                'Else

                'End If
            Catch ex As Exception
            End Try
            Return result
        End Function
    End Class
End Namespace
