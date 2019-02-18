Imports System.Net

Imports Newtonsoft.Json
Imports VSSA.EditAccount


Namespace CPProfileServices
    Public Class ProfileService
        Private Function BuildEndpoint(ByVal endpoint As String) As String
            ServicePointManager.ServerCertificateValidationCallback = (Function(sender, certificate, chain, sslPolicyErrors) True)
            Dim result As String = ConfigurationManager.AppSettings("RestCPProfileServiceEndpoint")
            Return result & endpoint
        End Function


        Public Function ModifyProfile(ByVal Account As EditedAccount)
            Dim result As ModifyResponse.ProfileModifyResponse = Nothing
            Dim request As New ModifReq.ProfileModifyRequest()
            Try
                request.Person = Account.Person.[Select](Function(x) New ModifReq.Person() With {.LastName = x.LastName}).ToList()
                request.ChoicePrivilegeID = Account.ChoicePrivilegeID
                request.OwnerID = Account.OwnerID
                request.TravelerPlusMember = Account.TravelerplusMember
                result = ModifyList(request)
            Catch

            End Try
            Return result
        End Function
        Public Function ModifyList(ByVal request As ModifReq.ProfileModifyRequest) As ModifyResponse.ProfileModifyResponse
            Dim result As ModifyResponse.ProfileModifyResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/ProfileModify")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim jstring = client.PostData.Replace("[", " ")
                Dim finalpostdata = jstring.Replace("]", " ")
                client.PostData = finalpostdata
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of ModifyResponse.ProfileModifyResponse)(json)
            Catch ex As Exception
            End Try

            Return result
        End Function
        Public Function SearchProfile(ByVal Account As EditedAccount)
            Dim result As SearchResponse.ProfileSearchResponse = Nothing
            Dim request As New SearchReq.ProfileSearchRequest()
            Try
                request.Person = Account.Person.[Select](Function(x) New SearchReq.Person() With {.LastName = x.LastName}).ToList()
                request.ChoicePrivilegeID = Account.ChoicePrivilegeID
                result = SearcList(request)
                If (result IsNot Nothing AndAlso result.People.Count > 0) Then
                    result.Success = "true"
                End If
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function SearcList(ByVal request As SearchReq.ProfileSearchRequest) As SearchResponse.ProfileSearchResponse
            Dim result As SearchResponse.ProfileSearchResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/ProfileSearch")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim jstring = client.PostData.Replace("[", " ")
                Dim finalpostdata = jstring.Replace("]", " ")
                client.PostData = finalpostdata

                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of SearchResponse.ProfileSearchResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
    End Class
End Namespace
