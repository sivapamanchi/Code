Imports Newtonsoft.Json
Imports System.Net
Imports VSSA.AcctResponse
Imports VSSA.AcctHistResponse
Imports VSSA.EditAccount
Imports VSSA.UpAcctResponse
Imports VSSA.UpAcctReq


Namespace CPAccountServices
    Public Class AccountService

        Public Property Accounts As List(Of AcctResponse.Account)
        Public Property Accountshistory As List(Of AcctHistResponse.Account)
        Private Function BuildEndpoint(ByVal endpoint As String) As String
            ServicePointManager.ServerCertificateValidationCallback = (Function(sender, certificate, chain, sslPolicyErrors) True)
            Dim result As String = ConfigurationManager.AppSettings("RestCPAcctServiceEndpoint")
            Return result & endpoint
        End Function

        Public Function RetrieveAcct(ByVal ownerId As String) As CPRetrieveAcctResponse
            Dim result As CPRetrieveAcctResponse = Nothing
            Dim request As New RetrieveAccountRequest
            request.OwnerID = ownerId
            result = AccountsList(request)
            If result IsNot Nothing AndAlso result.Accounts IsNot Nothing Then
                result.Accounts = result.Accounts.GroupBy(Function(test) test.Identifier).[Select](Function(grp) grp.First()).ToList()
            End If
            Return result
        End Function
        Public Function AccountsList(ByVal request As RetrieveAccountRequest) As CPRetrieveAcctResponse
            Dim result As CPRetrieveAcctResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/RetrieveAccount")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of CPRetrieveAcctResponse)(json)
                If ((Not (result) Is Nothing) AndAlso (Not (result.Accounts) Is Nothing)) Then

                    result.Accounts = result.Accounts.GroupBy(Function(test) test.Identifier).[Select](Function(grp) grp.First()).ToList()
                End If
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function RetrieveAcctHist(ByVal ownerId As String) As RetrieveAcctHistoryResponse
            Dim result As RetrieveAcctHistoryResponse = Nothing
            Dim request As New RetrieveAcctHistoryRequest
            request.OwnerID = ownerId
            result = AccountsHistoryList(request)
            Return result
        End Function
        Public Function AccountsHistoryList(ByVal request As RetrieveAcctHistoryRequest) As RetrieveAcctHistoryResponse
            Dim result As RetrieveAcctHistoryResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/RetrieveAccountHistory")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of RetrieveAcctHistoryResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function

        Public Function UpdateAcct(ByVal Account As EditedAccount)
            Dim result As UpdateAcctResponse = Nothing
            Dim request As New UpdateAccountRequest()
            Try
                request.OwnerID = Account.OwnerID
                request.Identifier = Account.Identifier
                request.ChoicePrivilegeID = Account.ChoicePrivilegeID
                request.Person = Account.Person.[Select](Function(x) New UpAcctReq.Person() With {.FirstName = x.FirstName, .LastName = x.LastName}).ToList()
                request.Requester = Account.Requester
                result = UpdateAcctInfo(request)
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function UpdateAcctInfo(ByVal request As UpdateAccountRequest) As UpdateAcctResponse
            Dim result As UpdateAcctResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/UpdateAccount")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim jstring = client.PostData.Replace("[", " ")
                Dim finalpostdata = jstring.Replace("]", " ")
                client.PostData = finalpostdata
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of UpdateAcctResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function InsertAcct(ByVal Account As EditedAccount)
            Dim result As InsertAcctResponse.InsertAcctResponse = Nothing
            Dim request As New InsertAcct.InsertAccountRequest()

            Try
                request.OwnerID = Account.OwnerID
                request.ChoicePrivilegeID = Account.ChoicePrivilegeID
                request.Person = Account.Person.[Select](Function(x) New InsertAcct.Person() With {.FirstName = x.FirstName, .LastName = x.LastName}).ToList()
                request.Requester = Account.Requester
                result = InsertAcctInfo(request)
                If (result IsNot Nothing AndAlso result.Accounts.Count > 0) Then
                    result.Success = "True"
                End If
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function InsertAcctInfo(ByVal request As InsertAcct.InsertAccountRequest) As InsertAcctResponse.InsertAcctResponse
            Dim result As InsertAcctResponse.InsertAcctResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/InsertAccount")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim jstring = client.PostData.Replace("[", " ")
                Dim finalpostdata = jstring.Replace("]", " ")
                client.PostData = finalpostdata
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of InsertAcctResponse.InsertAcctResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function DisableAcctInfo(ByVal request As DisableReq.DisableAccountRequest) As DisableResp.DisableAccountResponse
            Dim result As DisableResp.DisableAccountResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/DisableAccount")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim jstring = client.PostData.Replace("[", " ")
                Dim finalpostdata = jstring.Replace("]", " ")
                client.PostData = finalpostdata
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of DisableResp.DisableAccountResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        Public Function DisableAcct(ByVal Account As EditAccount.EditedAccount)
            Dim result As DisableResp.DisableAccountResponse = Nothing
            Dim request As New DisableReq.DisableAccountRequest
            Try
                request.Identifier = Account.Identifier
                request.OwnerID = Account.OwnerID
                request.Requester = Account.Requester
                result = DisableAcctInfo(request)

            Catch ex As Exception
            End Try

            Return result
        End Function

    End Class
End Namespace