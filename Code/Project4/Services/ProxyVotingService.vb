Imports System.Net
Imports Newtonsoft.Json
Imports VSSA.ActiveCandidate
Imports VSSA.ActiveCandidatebyYear
Imports VSSA.ActiveCandidatebyYearResp
Imports VSSA.ActiveCandidateReq
Imports VSSA.ActiveCandidateResp
Imports VSSA.ActiveProxyCandidateResp
Imports VSSA.Activevoters
Imports VSSA.ActivevotersResp
Imports VSSA.ActiveVotingDates
Imports VSSA.ActiveVotingDatesResp
Imports VSSA.ArvactandVotingYear
Imports VSSA.ArvactandVotingYearResp
Imports VSSA.EligibleVotersReq
Imports VSSA.EligibleVotersResp
Imports VSSA.GetCartItems
Imports VSSA.InactivateVoter
Imports VSSA.InactivateVoterResp
Imports VSSA.InactiveCandidateReq
Imports VSSA.InactiveCandidateResp
Imports VSSA.InsertCandidate
Imports VSSA.InsertCandidateResp
Imports VSSA.InsertDesignated
Imports VSSA.InsertDesignatedResp
Imports VSSA.InsertEligibleReq
Imports VSSA.InsertEligibleResp
Imports VSSA.InsertProxyDatesReq
Imports VSSA.InsertProxyDatesResp
Imports VSSA.InsertVote
Imports VSSA.InsertVoteResp
Imports VSSA.ModifyCandidate
Imports VSSA.ModifyCandidateResp

Namespace ProxyVotingServices
    Public Class ProxyVotingService
        Private Function BuildEndpoint(ByVal endpoint As String) As String
            ServicePointManager.ServerCertificateValidationCallback = (Function(sender, certificate, chain, sslPolicyErrors) True)
            Dim result As String = ConfigurationManager.AppSettings("RestProxyVotingServiceEndpoint")
            Return result & endpoint
        End Function
        'GetActiveDesignatedVoterByArvact
        Public Function GetActiveDesignatedVoterByArvact(ByVal OwnerArvact As String) As GetActiveDesignatedVoterByArvactResponse
            Dim result As GetActiveDesignatedVoterByArvactResponse = Nothing
            Dim request As New GetActiveDesignatedVoterByArvactRequest
            request.OwnerArvact = OwnerArvact
            result = GetActiveDesignatedVoterByArvactList(request)
            Return result
        End Function
        Public Function GetActiveDesignatedVoterByArvactList(ByVal request As GetActiveDesignatedVoterByArvactRequest) As GetActiveDesignatedVoterByArvactResponse
            Dim result As GetActiveDesignatedVoterByArvactResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/GetActiveDesignatedVoterByArvact")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of GetActiveDesignatedVoterByArvactResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'GetActiveProxyCandidateByArvactAndVotingYear
        Public Function GetActiveProxyVoteByArvactAndVotingYear(ByVal OwnerArvact As String, ByVal VotingYear As String) As GetActiveProxyVoteByArvactAndVotingYearResponse
            Dim result As GetActiveProxyVoteByArvactAndVotingYearResponse = Nothing
            Dim request As New GetActiveProxyVoteByArvactAndVotingYearRequest
            request.OwnerArvact = OwnerArvact
            request.VotingYear = VotingYear
            result = GetActiveProxyVoteByArvactAndVotingYearList(request)
            Return result
        End Function
        Public Function GetActiveProxyVoteByArvactAndVotingYearList(ByVal request As GetActiveProxyVoteByArvactAndVotingYearRequest) As GetActiveProxyVoteByArvactAndVotingYearResponse
            Dim result As GetActiveProxyVoteByArvactAndVotingYearResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/GetActiveProxyVoteByArvactAndVotingYear")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of GetActiveProxyVoteByArvactAndVotingYearResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'GetActiveProxyCandidateByNameAndVotingYear
        Public Function GetActiveProxyCandidateByNameAndVotingYear(ByVal CandidateName As String, ByVal VotingYear As String) As GetActiveProxyCandidateByNameAndVotingYearResponse
            Dim result As GetActiveProxyCandidateByNameAndVotingYearResponse = Nothing
            Dim request As New GetActiveProxyCandidateByNameAndVotingYearRequest
            request.CandidateName = CandidateName
            request.VotingYear = VotingYear
            result = GetActiveProxyCandidateByNameAndVotingYearList(request)
            Return result
        End Function
        Public Function GetActiveProxyCandidateByNameAndVotingYearList(ByVal request As GetActiveProxyCandidateByNameAndVotingYearRequest) As GetActiveProxyCandidateByNameAndVotingYearResponse
            Dim result As GetActiveProxyCandidateByNameAndVotingYearResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/GetActiveProxyCandidateByNameAndVotingYear")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of GetActiveProxyCandidateByNameAndVotingYearResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'GetActiveProxyCandidatesByVotingYear
        Public Function GetActiveProxyCandidatesByVotingYear(ByVal VotingYear As String) As GetActiveProxyCandidatesByVotingYearResponse
            Dim result As GetActiveProxyCandidatesByVotingYearResponse = Nothing
            Dim request As New GetActiveProxyCandidatesByVotingYearRequest
            request.VotingYear = VotingYear
            result = GetActiveProxyCandidatesByVotingYearList(request)
            Return result
        End Function
        Public Function GetActiveProxyCandidatesByVotingYearList(ByVal request As GetActiveProxyCandidatesByVotingYearRequest) As GetActiveProxyCandidatesByVotingYearResponse
            Dim result As GetActiveProxyCandidatesByVotingYearResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/GetActiveProxyCandidatesByVotingYear")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of GetActiveProxyCandidatesByVotingYearResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'GetActiveProxyVotingDates
        Public Function GetActiveProxyVotingDates(ByVal IsActive As String) As GetActiveProxyVotingDatesResponse
            Dim result As GetActiveProxyVotingDatesResponse = Nothing
            Dim request As New GetActiveProxyVotingDatesRequest
            request.IsActive = IsActive
            result = GetActiveProxyVotingDatesList(request)
            Return result
        End Function
        Public Function GetActiveProxyVotingDatesList(ByVal request As GetActiveProxyVotingDatesRequest) As GetActiveProxyVotingDatesResponse
            Dim result As GetActiveProxyVotingDatesResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/GetActiveProxyVotingDates")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of GetActiveProxyVotingDatesResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'InactivateProxyDesignatedVoter
        Public Function InactivateProxyDesignatedVoter(ByVal OwnerArvact As String, ByVal DateInactivated As String, ByVal ModifiedBy As String) As InactivateProxyDesignatedVoterResponse
            Dim result As InactivateProxyDesignatedVoterResponse = Nothing
            Dim request As New InactivateProxyDesignatedVoterRequest
            request.OwnerArvact = OwnerArvact
            request.DateInactivated = DateInactivated
            request.ModifiedBy = ModifiedBy
            result = InactivateProxyDesignatedVoterList(request)
            Return result
        End Function
        Public Function InactivateProxyDesignatedVoterList(ByVal request As InactivateProxyDesignatedVoterRequest) As InactivateProxyDesignatedVoterResponse
            Dim result As InactivateProxyDesignatedVoterResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/InactivateProxyDesignatedVoter")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of InactivateProxyDesignatedVoterResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'InsertProxyCandidate
        Public Function InsertProxyCandidate(ByVal CandidateName As String, ByVal CandidateOwnerID As String, ByVal VotingYear As String, ByVal IsActive As String, ByVal IsWriteInCandidate As String, ByVal ModifiedDate As String, ByVal ModifiedBy As String) As InsertProxyCandidateResponse
            Dim result As InsertProxyCandidateResponse = Nothing
            Dim request As New InsertProxyCandidateRequest
            request.CandidateName = CandidateName
            request.CandidateOwnerID = CandidateOwnerID
            request.VotingYear = VotingYear
            request.IsActive = IsActive
            request.IsWriteInCandidate = IsWriteInCandidate
            request.ModifiedDate = ModifiedDate
            request.ModifiedBy = ModifiedBy
            result = InsertProxyCandidateList(request)
            Return result
        End Function
        Public Function InsertProxyCandidateList(ByVal request As InsertProxyCandidateRequest) As InsertProxyCandidateResponse
            Dim result As InsertProxyCandidateResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/InsertProxyCandidate")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of InsertProxyCandidateResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'InsertProxyVote
        Public Function InsertProxyVote(ByVal OwnerArvact As String, ByVal VotingYear As String, ByVal OwnerFirstName As String, ByVal OwnerLastName As String, ByVal CandidateID As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String) As InsertProxyVoteResponse
            Dim result As InsertProxyVoteResponse = Nothing
            Dim request As New InsertProxyVoteRequest
            request.OwnerArvact = OwnerArvact
            request.VotingYear = VotingYear
            request.OwnerFirstName = OwnerFirstName
            request.OwnerLastName = OwnerLastName
            request.CandidateID = CandidateID
            request.ModifiedBy = ModifiedBy
            request.ModifiedDate = ModifiedDate
            result = InsertProxyVoteList(request)
            Return result
        End Function
        Public Function InsertProxyVoteList(ByVal request As InsertProxyVoteRequest) As InsertProxyVoteResponse
            Dim result As InsertProxyVoteResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/InsertProxyVote")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of InsertProxyVoteResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'InsertProxyDesignatedVoter
        Public Function InsertProxyDesignatedVoter(ByVal OwnerArvact As String, ByVal DesignatedVoter As String, ByVal ModifiedBy As String, ByVal DateCreated As String) As InsertProxyDesignatedVoterResponse
            Dim result As InsertProxyDesignatedVoterResponse = Nothing
            Dim request As New InsertProxyDesignatedVoterRequest
            request.OwnerArvact = OwnerArvact
            request.DesignatedVoter = DesignatedVoter
            request.ModifiedBy = ModifiedBy
            request.DateCreated = DateCreated
            result = InsertProxyDesignatedVoterList(request)
            Return result
        End Function
        Public Function InsertProxyDesignatedVoterList(ByVal request As InsertProxyDesignatedVoterRequest) As InsertProxyDesignatedVoterResponse
            Dim result As InsertProxyDesignatedVoterResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/InsertProxyDesignatedVoter")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of InsertProxyDesignatedVoterResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'ModifyProxyCandidate
        Public Function ModifyProxyCandidate(ByVal CandidateID As String, ByVal CandidateName As String, ByVal CandidateOwnerID As String, ByVal VotingYear As String, ByVal ModifiedDate As String, ByVal ModifiedBy As String) As ModifyProxyCandidateResponse
            Dim result As ModifyProxyCandidateResponse = Nothing
            Dim request As New ModifyProxyCandidateRequest
            request.CandidateID = CandidateID
            request.CandidateName = CandidateName
            request.CandidateOwnerID = CandidateOwnerID
            request.VotingYear = VotingYear
            request.ModifiedDate = ModifiedDate
            request.ModifiedBy = ModifiedBy
            result = ModifyProxyCandidateList(request)
            Return result
        End Function
        Public Function ModifyProxyCandidateList(ByVal request As ModifyProxyCandidateRequest) As ModifyProxyCandidateResponse
            Dim result As ModifyProxyCandidateResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/ModifyProxyCandidate")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of ModifyProxyCandidateResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'InsertProxyDates
        Public Function InsertProxyDates(ByVal VotingYear As String, ByVal StartDate As String, ByVal EndDate As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String, ByVal IsActive As String) As InsertProxyDatesResponse
            Dim result As InsertProxyDatesResponse = Nothing
            Dim request As New InsertProxyDatesRequest
            request.VotingYear = VotingYear
            request.StartDate = StartDate
            request.EndDate = EndDate
            request.ModifiedBy = ModifiedBy
            request.ModifiedDate = ModifiedDate
            request.IsActive = IsActive
            result = InsertProxyDatesList(request)
            Return result
        End Function
        Public Function InsertProxyDatesList(ByVal request As InsertProxyDatesRequest) As InsertProxyDatesResponse
            Dim result As InsertProxyDatesResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/InsertProxyDates")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of InsertProxyDatesResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'InsertProxyEligibleVoters
        Public Function InsertProxyEligibleVoters(ByVal VotingYear As String, ByVal EligibleVoters As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String) As InsertProxyEligibleVotersResponse
            Dim result As InsertProxyEligibleVotersResponse = Nothing
            Dim request As New InsertProxyEligibleVotersRequest
            request.VotingYear = VotingYear
            request.EligibleVoters = EligibleVoters
            request.ModifiedBy = ModifiedBy
            request.ModifiedDate = ModifiedDate
            result = InsertProxyEligibleVotersList(request)
            Return result
        End Function
        Public Function InsertProxyEligibleVotersList(ByVal request As InsertProxyEligibleVotersRequest) As InsertProxyEligibleVotersResponse
            Dim result As InsertProxyEligibleVotersResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/InsertProxyEligibleVoters")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of InsertProxyEligibleVotersResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'GetAllActiveEligibleVoters
        Public Function GetAllActiveEligibleVoters(ByVal isActive As String) As GetAllActiveEligibleVotersResponse
            Dim result As GetAllActiveEligibleVotersResponse = Nothing
            Dim request As New GetAllActiveEligibleVotersRequest
            request.isActive = isActive
            result = GetAllActiveEligibleVotersList(request)
            Return result
        End Function
        Public Function GetAllActiveEligibleVotersList(ByVal request As GetAllActiveEligibleVotersRequest) As GetAllActiveEligibleVotersResponse
            Dim result As GetAllActiveEligibleVotersResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/GetAllActiveEligibleVoters")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of GetAllActiveEligibleVotersResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'GetAllActiveProxyCandidates
        Public Function GetAllActiveProxyCandidates(ByVal isActive As String) As GetAllActiveProxyCandidatesResponse
            Dim result As GetAllActiveProxyCandidatesResponse = Nothing
            Dim request As New GetAllActiveProxyCandidatesRequest
            request.isActive = isActive
            result = GetAllActiveProxyCandidatesList(request)
            Return result
        End Function
        Public Function GetAllActiveProxyCandidatesList(ByVal request As GetAllActiveProxyCandidatesRequest) As GetAllActiveProxyCandidatesResponse
            Dim result As GetAllActiveProxyCandidatesResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/GetAllActiveProxyCandidates")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of GetAllActiveProxyCandidatesResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
        'InactivateProxyCandidate
        Public Function InactivateProxyCandidate(ByVal CandidateName As String, ByVal CandidateOwnerID As String, ByVal VotingYear As String, ByVal ModifiedDate As String, ByVal ModifiedBy As String) As InactivateProxyCandidateResponse
            Dim result As InactivateProxyCandidateResponse = Nothing
            Dim request As New InactivateProxyCandidateRequest
            request.CandidateName = CandidateName
            request.CandidateOwnerID = CandidateOwnerID
            request.VotingYear = VotingYear
            request.ModifiedDate = ModifiedDate
            request.ModifiedBy = ModifiedBy
            result = InactivateProxyCandidateList(request)
            Return result
        End Function
        Public Function InactivateProxyCandidateList(ByVal request As InactivateProxyCandidateRequest) As InactivateProxyCandidateResponse
            Dim result As InactivateProxyCandidateResponse = Nothing
            Try
                Dim endPoint As String = BuildEndpoint("/InactivateProxyCandidate")
                Dim client = New RestClient()
                client.EndPoint = endPoint
                client.Method = HttpVerb.POST
                client.PostData = JsonConvert.SerializeObject(request)
                Dim json = client.MakeRequest()
                result = JsonConvert.DeserializeObject(Of InactivateProxyCandidateResponse)(json)
            Catch ex As Exception
            End Try
            Return result
        End Function
    End Class
End Namespace