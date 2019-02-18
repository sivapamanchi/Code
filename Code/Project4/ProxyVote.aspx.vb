Imports VSSA.ActiveCandidatebyYearResp
Imports VSSA.ActiveCandidateResp
Imports VSSA.ActivevotersResp
Imports VSSA.ActiveVotingDatesResp
Imports VSSA.ArvactandVotingYearResp
Imports VSSA.fetchresp
Imports VSSA.InactivateVoterResp
Imports VSSA.InsertCandidateResp
Imports VSSA.InsertDesignatedResp
Imports VSSA.InsertVoteResp
Imports VSSA.OwnerServices
Imports VSSA.ProxyVotingServices

Public Class ProxyVote
    Inherits VSSABaseClass
    Public owner As New BXG_Owner
    Public OwnerArvact As String
    Public ApplicationError As String = ""
    Public ActiveYear As String = ""
    Public DesignatedVoter As String = String.Empty
    Public bxgowner As New OwnerFetchResponse
    Protected Sub ProxyVote_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("SearchParam") Is Nothing Then
            Session("SearchParam") = Nothing
        End If
        If Not Session("SearchData") Is Nothing Then
            Session("SearchData") = Nothing
        End If
        If String.IsNullOrEmpty(Session("DisplayProxyVoting")) OrElse Not Session("DisplayProxyVoting").Equals("True") Then
            Response.Redirect("./Default.aspx")
        End If

        If Not IsNothing(Session("owner")) Then
            If Not IsNothing(Session("BoomiOwner")) Then
                bxgowner = Session("BoomiOwner")
                owner = Session("owner")
                If bxgowner.Identifier <> owner.ARVACT Then
                    Dim Ownersvc As New OwnerService()
                    Dim OwnerSvcResp As New OwnerFetchResponse()
                    OwnerSvcResp = Ownersvc.OwnerFetch(owner.ARVACT)
                    Session("BoomiOwner") = OwnerSvcResp
                    bxgowner = Session("BoomiOwner")
                End If
                lblOwnerAccountError.Text = ""
            End If
        Else
            ApplicationError = "True"
        End If
        OwnerArvact = owner.ARVACT
        If Not IsPostBack Then
            getOwnerInfo()
            Dim ProxySvc As New ProxyVotingService()
            Dim VoterByArvact As New GetActiveDesignatedVoterByArvactResponse()
            VoterByArvact = ProxySvc.GetActiveDesignatedVoterByArvact(OwnerArvact)
            If VoterByArvact.Records Is Nothing Then
                lblHeader.Text = String.Format("Record Vote for Owner #{0} ({1})", owner.ARVACT, owner.AccountHolders(0).OwnerName)
            Else
                DesignatedVoter = VoterByArvact.Records(0).DesignatedVoterName
                lblHeader.Text = String.Format("Record Vote for Owner #{0} ({1})", owner.ARVACT, DesignatedVoter)

            End If
        End If
        bindData()
    End Sub

    Private Sub bindData()
        Dim pd As ProxyDate = Nothing
        Dim lstVotes As New List(Of ProxyVotesGridHelper)
        Dim lstCandidates As New List(Of ProxyCandidate)
        Dim lstCandidateNames As New List(Of CandidateDropdownHelper)
        pd = getActiveProxyVoteDates()
        If Not IsNothing(pd) Then

            lstCandidates = getCandidates(pd.votingYear)
            lstVotes = getVotesForOwner(pd.votingYear, owner.ARVACT)
            gvVotes.DataSource = lstVotes
            gvVotes.DataBind()

            If Not IsPostBack Then
                Dim defaultCandidate As New ProxyCandidate
                defaultCandidate.CandidateName = "Select One"
                defaultCandidate.CandidateID = "-1"
                Dim writeInCandidate As New ProxyCandidate
                writeInCandidate.CandidateName = "Write In Candidate"
                writeInCandidate.CandidateID = "0"
                lstCandidates.Insert(0, defaultCandidate)
                lstCandidates.Insert(1, writeInCandidate)
                ddlCandidate.DataSource = lstCandidates
                ddlCandidate.DataTextField = "CandidateName"
                ddlCandidate.DataValueField = "CandidateID"
                ddlCandidate.DataBind()

                Dim lstAccountNames As List(Of String) = getAllAccountNames()
                If lstAccountNames.Count <= 1 Then
                    ' hide designated voter panel
                    pnlDesignatedVoter.Visible = False
                Else
                    lstAccountNames.Insert(0, "Select One")
                    ddlDesignatedVoter.DataSource = lstAccountNames
                    ddlDesignatedVoter.DataBind()
                    If Not String.IsNullOrEmpty(DesignatedVoter) Then
                        ddlDesignatedVoter.SelectedValue = DesignatedVoter
                        btnRemove.Visible = True
                    End If
                End If
            End If

        Else
            ' Today's date is not between start and end dates for any voting period, redirect to home page since the vote button should not be enabled on default.aspx
            Response.Redirect("default.aspx")
        End If
    End Sub

    Private Function getAllAccountNames() As List(Of String)
        Dim CP As New clsDBConnectivityProxyVote
        Dim lstOwners As New List(Of String)
        For Each ah As AccountHolder In owner.AccountHolders
            lstOwners.Add(ah.OwnerName)
        Next
        Return lstOwners
    End Function

    Private Function getActiveProxyVoteDates() As ProxyDate
        Dim pDate As New ProxyDate
        Dim PD As New ProxyDate
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim VotingDates As New GetActiveProxyVotingDatesResponse()
            Dim IsActive As Integer = 1
            VotingDates = ProxySvc.GetActiveProxyVotingDates(IsActive)
            If Not (VotingDates.Records Is Nothing) Then
                For i As Integer = 0 To VotingDates.Records.Count - 1
                    PD.startDate = VotingDates.Records(i).StartDate
                    PD.endDate = VotingDates.Records(i).EndDate
                    PD.votingYear = VotingDates.Records(i).VotingYear
                    If DateTime.Now >= PD.startDate AndAlso DateTime.Now <= PD.endDate Then
                        pDate = PD
                        ActiveYear = PD.votingYear
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
        If Not IsNothing(pDate.votingYear) Then
            Return pDate
        Else
            Return Nothing

        End If

    End Function

    Private Function getActiveCandidateIdByName(ByVal inCandidateName As String, ByVal inVotingYear As String) As List(Of ProxyCandidate)
        Dim lstCandidates As New List(Of ProxyCandidate)
        Dim PC As New ProxyCandidate
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim CandidateByNameandVotingYear As New GetActiveProxyCandidateByNameAndVotingYearResponse()
            CandidateByNameandVotingYear = ProxySvc.GetActiveProxyCandidateByNameAndVotingYear(inCandidateName, ActiveYear)
            If Not (CandidateByNameandVotingYear.Records Is Nothing) Then
                For i As Integer = 0 To CandidateByNameandVotingYear.Records.Count - 1
                    PC.CandidateID = CandidateByNameandVotingYear.Records(i).CandidateID
                    PC.CandidateName = CandidateByNameandVotingYear.Records(i).CandidateName
                    PC.VotingYear = CandidateByNameandVotingYear.Records(i).VotingYear
                    PC.ModifiedDate = CandidateByNameandVotingYear.Records(i).ModifiedDate
                    PC.ModifiedBy = CandidateByNameandVotingYear.Records(i).ModifiedBy
                    lstCandidates.Add(PC)
                Next
            End If
        Catch ex As Exception
        End Try

        Return lstCandidates
    End Function
    Private Function getCandidates(ByVal inVotingYear As String) As List(Of ProxyCandidate)
        Dim lstCandidates As New List(Of ProxyCandidate)
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim ActiveVotingDates As New GetActiveProxyCandidatesByVotingYearResponse()
            ActiveVotingDates = ProxySvc.GetActiveProxyCandidatesByVotingYear(inVotingYear)
            If Not (ActiveVotingDates.Records Is Nothing) Then
                For i As Integer = 0 To ActiveVotingDates.Records.Count - 1
                    Dim PC As New ProxyCandidate
                    PC.CandidateID = ActiveVotingDates.Records(i).CandidateID
                    PC.CandidateName = ActiveVotingDates.Records(i).CandidateName
                    PC.VotingYear = ActiveVotingDates.Records(i).VotingYear
                    PC.ModifiedDate = ActiveVotingDates.Records(i).ModifiedDate
                    PC.ModifiedBy = ActiveVotingDates.Records(i).ModifiedBy
                    lstCandidates.Add(PC)
                Next
            End If
        Catch ex As Exception
        End Try
        Return lstCandidates
    End Function
    Private Function getVotesForOwner(ByVal inVotingYear As String, inARVACT As String) As List(Of ProxyVotesGridHelper)
        Dim lstVotes As New List(Of ProxyVotesGridHelper)
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim ByArvactandVotingYear As New GetActiveProxyVoteByArvactAndVotingYearResponse()
            ByArvactandVotingYear = ProxySvc.GetActiveProxyVoteByArvactAndVotingYear(inARVACT, inVotingYear)
            If Not (ByArvactandVotingYear.Records Is Nothing) Then
                For i As Integer = 0 To ByArvactandVotingYear.Records.Count - 1
                    Dim PV As New ProxyVotesGridHelper
                    PV.Candidate = ByArvactandVotingYear.Records(i).CandidateName
                    PV.VotingYear = ByArvactandVotingYear.Records(i).VotingYear
                    PV.ModifiedDate = ByArvactandVotingYear.Records(i).ModifiedDate
                    PV.CreatedBy = ByArvactandVotingYear.Records(i).ModifiedBy
                    lstVotes.Add(PV)
                Next
            End If
        Catch ex As Exception
        End Try
        Return lstVotes
    End Function
    Private Function GetDesignatedVoterByArvact(ByVal inArvact As String) As String
        Dim retVal As String = String.Empty
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim DesignVoter As New GetActiveDesignatedVoterByArvactResponse()
            DesignVoter = ProxySvc.GetActiveDesignatedVoterByArvact(inArvact)
            If Not (DesignVoter.Records Is Nothing) Then
                For i As Integer = 0 To DesignVoter.Records.Count - 1
                    retVal = DesignVoter.Records(i).DesignatedVoterName
                Next
            End If
        Catch ex As Exception
        End Try
        Return retVal
    End Function

    Private Function SubmitDesignatedVoter(ByVal inDesignatedVoter As String) As Boolean
        Dim success As Boolean = True
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim InsertDesigVoter As New InsertProxyDesignatedVoterResponse()
            InsertDesigVoter = ProxySvc.InsertProxyDesignatedVoter(OwnerArvact, inDesignatedVoter, Environment.UserName.ToString(), String.Format("{0:yyyy-MM-dd hh:mm:ss.fff }", DateTime.Now))
        Catch ex As Exception
            success = False
        End Try
        Return success
    End Function
    Private Function SubmitVote(ByVal inCandidateID As Integer) As Boolean
        Dim success As Boolean = True
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim InsertVote As New InsertProxyVoteResponse()
            InsertVote = ProxySvc.InsertProxyVote(OwnerArvact, ActiveYear, bxgowner.People(0).FirstName, bxgowner.People(0).LastName, inCandidateID, Environment.UserName.ToString(), String.Format("{0:yyyy-MM-dd hh:mm:ss.fff }", DateTime.Now))
            Me.pnlVotingMessage.Visible = True
            Me.lblvoting.Text = "The vote was recorded"
            Me.lblvoting.ForeColor = Drawing.Color.Green
            Exit Try
        Catch ex As Exception
            success = False
        End Try
        Return success
    End Function
    Private Function RemoveDesignatedVoter() As Boolean
        Dim success As Boolean = True
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim RemoveVoter As New InactivateProxyDesignatedVoterResponse()
            RemoveVoter = ProxySvc.InactivateProxyDesignatedVoter(OwnerArvact, String.Format("{0:yyyy-MM-dd hh:mm:ss.fff }", DateTime.Now), Environment.UserName.ToString())
        Catch ex As Exception
            success = False
        End Try
        Return success
    End Function

    Private Sub SubmitWriteInCandidate(ByVal inCandidateName As String)
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim InsertCandidate As New InsertProxyCandidateResponse()
            InsertCandidate = ProxySvc.InsertProxyCandidate(inCandidateName, 0, ActiveYear, 1, 1, String.Format("{0:yyyy-MM-dd hh:mm:ss.fff }", DateTime.Now), Environment.UserName.ToString())
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetWriteInCandidate(ByVal inCandidateName As String) As Integer
        'return the newly created candidateid or existing candidateid if the name matches
        Dim retVal As Integer = Integer.MinValue
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim CandidateByNameandVotingYear As New GetActiveProxyCandidateByNameAndVotingYearResponse()
            CandidateByNameandVotingYear = ProxySvc.GetActiveProxyCandidateByNameAndVotingYear(inCandidateName, ActiveYear)
            If Not (CandidateByNameandVotingYear.Records Is Nothing) Then
                For i As Integer = 0 To CandidateByNameandVotingYear.Records.Count - 1
                    retVal = CandidateByNameandVotingYear.Records(i).CandidateID
                Next
            End If
        Catch ex As Exception
        End Try
        Return retVal
    End Function
    Private Function isValidCandidateName(ByVal inCandidateName As String) As Boolean
        Dim isValid As Boolean = False
        inCandidateName = inCandidateName.Replace(" ", "")
        If Not inCandidateName.All(AddressOf Char.IsLetterOrDigit) Then
            isValid = False
        Else
            isValid = True
        End If
        Return isValid
    End Function
    Private Sub getOwnerInfo()
        If Not (Session("BoomiOwner")) Is Nothing Then
            bxgowner = Session("BoomiOwner")
        End If
        If OwnerArvact <> "" Then

            Dim ds As New DataSet
            ds = OwnerData.searchOwners(OwnerArvact, "", "", "", "")
            If ds.Tables(0).Rows.Count = 0 Then
                lblOwnerAccountError.Text = "Owner ARVACT number can't be blank"
                'UpdPnlConversion.Visible = False
                'UpdPnlAccounts.Visible = False
                'UpdPnlPointsConversionHistory.Visible = False
                'UpdPnlAccountHistory.Visible = False
                lblOwnerAccountError.Visible = True
            Else

                Dim Name2 As DataColumn = New DataColumn("Reg")
                Name2.DataType = System.Type.GetType("System.String")
                ds.Tables(0).Columns.Add(Name2)

                For Each row As DataRow In ds.Tables(0).Rows
                    'If row.Item("email") Then
                    If row.Item("email") Is System.DBNull.Value Then

                        'If they don't have an email address in the system then it is not possible to be registered
                        Session("Registered") = "No"
                        row.Item("Reg") = "No"

                    Else
                        Try
                            If row.Item("Arvact") > 0 AndAlso row.Item("Email").length > 0 Then
                                'txtcurrentEmail.Text = row.Item("Email")
                                Dim svc As New SitecoreProfileSvc.ProfileSoapClient
                                Dim req As New SitecoreProfileSvc.IsUserRegisterRequest
                                Dim reqBody As New SitecoreProfileSvc.IsUserRegisterRequestBody

                                reqBody.ARVACT = row.Item("Arvact")
                                reqBody.email = row.Item("Email")
                                req.Body = reqBody

                                Dim resp As SitecoreProfileSvc.IsUserRegisterResponse = svc.SitecoreProfileSvc_ProfileSoap_IsUserRegister(req)
                                If resp.Body.IsUserRegisterResult.Equals(True) Then
                                    Session("Registered") = "Yes"
                                    row.Item("Reg") = "Yes"
                                Else
                                    Session("Registered") = "No"
                                    row.Item("Reg") = "No"
                                End If
                            Else
                                Session("Registered") = "No"
                                row.Item("Reg") = "No"
                            End If
                        Catch ex As Exception

                            Session("Registered") = "No"
                            row.Item("Reg") = "No"

                        End Try
                    End If

                Next

                gvSearchResults.DataSource = ds
                gvSearchResults.DataBind()

            End If

            With owner
                Dim iDate As String = Now().ToShortDateString()
                Dim oDate As DateTime = Convert.ToDateTime(iDate)
                lblRegistered.Text = Session("Registered").ToString()
                If Not bxgowner.Account.Memberships.TravelerPlusMembership Is Nothing Then
                    If Not bxgowner.Account.Memberships.TravelerPlusMembership.TravelerPlusEligible = False Then
                        If bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate = "" Then
                            Me.lblTPExp.Text = "NA"
                            Me.lblTPExp.ForeColor = Drawing.Color.Black
                        ElseIf Convert.ToDateTime(bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate.ToString()) >= oDate Then
                            Me.lblTPExp.ForeColor = Drawing.Color.Green
                            Me.lblTPExp.Text = bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate
                        ElseIf Convert.ToDateTime(bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate.ToString()) < oDate Then
                            Me.lblTPExp.ForeColor = Drawing.Color.Red
                            Me.lblTPExp.Text = bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate
                        End If
                    Else
                        Me.lblTPExp.Text = "NA"
                        Me.lblTPExp.ForeColor = Drawing.Color.Black
                    End If
                Else
                    Me.lblTPExp.Text = "NA"
                    Me.lblTPExp.ForeColor = Drawing.Color.Black
                End If


                If lblTPExp.Text IsNot Nothing Then
                    Label1.Visible = True
                    lblTPExp.Visible = True
                    Reg.Visible = True
                End If
                Me.lblCollCode.Text = owner.AccountStatus
                Me.lblCollCode.Visible = True
                If Not lblCollCode.Text.Contains(" - User in good standing ") Then
                    lblCollCode.ForeColor = Drawing.Color.Red
                Else
                    lblCollCode.ForeColor = Drawing.Color.Green
                End If

                If lblCollCode.Text IsNot Nothing Then
                    label2.Visible = True
                End If
            End With
        Else
            lblOwnerAccountError.Text = "Owner ARVACT number can't be blank"
            'UpdPnlAccounts.Visible = False
            'UpdPnlConversion.Visible = False
            'UpdPnlAccountHistory.Visible = False
            lblOwnerAccountError.Visible = True
        End If

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim strCandidate As String = String.Empty
        Dim intCandidateID As Integer = Integer.MinValue
        Me.lblvoting.Text = String.Empty

        Try
            If (ddlCandidate.SelectedItem.Text = "Select One") Then
                Me.pnlVotingMessage.Visible = True
                Me.lblvoting.Text = "Please enter Candidate Name"
                Me.lblvoting.ForeColor = Drawing.Color.Red
                Exit Try
            ElseIf ((ddlCandidate.SelectedItem.Text = "Write In Candidate") And (txtCandidate.Text.Trim.Length = 0)) Then
                Me.pnlVotingMessage.Visible = True
                Me.lblvoting.Text = "Please enter Candidate Name"
                Me.lblvoting.ForeColor = Drawing.Color.Red
                Exit Try
            ElseIf ((ddlCandidate.SelectedItem.Text = "Write In Candidate") And Not isValidCandidateName(txtCandidate.Text.Trim)) Then
                Me.pnlVotingMessage.Visible = True
                Me.lblvoting.Text = "Please enter a valid Candidate Name"
                Me.lblvoting.ForeColor = Drawing.Color.Red
                Exit Try
            End If


            If pnlDesignatedVoter.Visible AndAlso ddlDesignatedVoter.SelectedIndex > 0 AndAlso Not ddlDesignatedVoter.SelectedValue.Equals(DesignatedVoter) Then
                'TODO: Add code to popup alert
                'Add logic for popup, if OK is clicked then submit designated voter else exit sub..  this would best be handled via javascript
                If SubmitDesignatedVoter(ddlDesignatedVoter.SelectedValue) Then
                    'success
                    btnRemove.Visible = True
                Else
                    'TODO: add label error message indicating designated voter failed to insert
                End If
            End If
            If Len(txtCandidate.Text) > 0 And (ddlCandidate.SelectedItem.Text = "Write In Candidate") Then
                strCandidate = txtCandidate.Text.Trim()
                intCandidateID = GetWriteInCandidate(strCandidate)
                If intCandidateID = Integer.MinValue Then
                    SubmitWriteInCandidate(strCandidate)
                    intCandidateID = GetWriteInCandidate(strCandidate)
                End If
            ElseIf ddlCandidate.SelectedIndex > 0 Then
                intCandidateID = ddlCandidate.SelectedItem.Value
                strCandidate = ddlCandidate.SelectedItem.Text
            Else
                ' TODO: add error label text
                Exit Sub
            End If
            If intCandidateID > Integer.MinValue Then
                SubmitVote(intCandidateID)
                bindData()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If RemoveDesignatedVoter() Then
            btnRemove.Visible = False
            ddlDesignatedVoter.SelectedIndex = 0
        End If
    End Sub

    Protected Sub ddlDesignatedVoter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDesignatedVoter.SelectedIndexChanged
        If ddlDesignatedVoter.SelectedIndex = 0 Then
            btnRemove.Visible = False
        Else
            'btnRemove.Visible = True
        End If
    End Sub

    Private Sub ddlCandidate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCandidate.SelectedIndexChanged
        If ddlCandidate.SelectedIndex = 1 AndAlso InStr(ddlCandidate.SelectedItem.Text.ToLower, "write in") Then
            ' This is the write in candidate 
            pnlWriteInCandidate.Visible = True
        Else
            pnlWriteInCandidate.Visible = False
        End If
    End Sub
End Class

Public Class CandidateDropdownHelper
    Private _candidateID As Integer
    Private _candidate As String
    Public Property CandidateID As String
        Get
            Return _candidateID
        End Get
        Set(value As String)
            _candidateID = value
        End Set
    End Property
    Public Property Candidate As String
        Get
            Return _candidate
        End Get
        Set(value As String)
            _candidate = value
        End Set
    End Property
End Class

Public Class ProxyDate
    Private _startDate As DateTime
    Private _startTime As String
    Private _endDate As DateTime
    Private _endTime As String
    Private _votingYear As String
    Public Property startDate As DateTime
        Get
            Return _startDate
        End Get
        Set(value As DateTime)
            _startDate = value
        End Set
    End Property
    Public Property startTime As String
        Get
            Return _startTime
        End Get
        Set(value As String)
            _startTime = value
        End Set
    End Property
    Public Property endDate As DateTime
        Get
            Return _endDate
        End Get
        Set(value As DateTime)
            _endDate = value
        End Set
    End Property
    Public Property endTime As String
        Get
            Return _endTime
        End Get
        Set(value As String)
            _endTime = value
        End Set
    End Property
    Public Property votingYear As String
        Get
            Return _votingYear
        End Get
        Set(value As String)
            _votingYear = value
        End Set
    End Property
End Class

Public Class ProxyVotesGridHelper
    Private _createdBy As String
    Private _votingYear As String
    Private _modifiedDate As String
    Private _candidate As String
    Public Property CreatedBy As String
        Get
            Return _createdBy
        End Get
        Set(value As String)
            _createdBy = value
        End Set
    End Property
    Public Property VotingYear As String
        Get
            Return _votingYear
        End Get
        Set(value As String)
            _votingYear = value
        End Set
    End Property
    Public Property ModifiedDate As String
        Get
            Return _modifiedDate
        End Get
        Set(value As String)
            _modifiedDate = value
        End Set
    End Property
    Public Property Candidate As String
        Get
            Return _candidate
        End Get
        Set(value As String)
            _candidate = value
        End Set
    End Property
End Class