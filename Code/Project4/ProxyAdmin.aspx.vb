
Imports VSSA.ActiveCandidateResp
Imports VSSA.ActiveProxyCandidateResp
Imports VSSA.ActiveVotingDatesResp
Imports VSSA.EligibleVotersResp
Imports VSSA.InactiveCandidateResp
Imports VSSA.InsertCandidateResp
Imports VSSA.InsertEligibleResp
Imports VSSA.InsertProxyDatesResp
Imports VSSA.ModifyCandidateResp
Imports VSSA.ProxyVotingServices

Public Class ProxyAdmin
    Inherits System.Web.UI.Page
    Public Shared lstCandidate As List(Of ProxyCandidate)
    Public Shared lstYears As List(Of String)
    Public Shared lstProxyDates As List(Of ProxyDate)
    Public Shared lstEligibleVoters As List(Of ProxyEligibleVoters)
    Public Shared lstCandidates As List(Of ProxyCandidate)
    Public Shared lstEnableHistoricalDataEdit As List(Of Boolean)
    Public Shared result As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("DisplayProxyAdmin")) OrElse Not Session("DisplayProxyAdmin").Equals("True") Then
            Response.Redirect("./Default.aspx")
        End If
        If Not Session("SearchParam") Is Nothing Then
            Session("SearchParam") = Nothing
        End If
        If Not Session("SearchData") Is Nothing Then
            Session("SearchData") = Nothing
        End If
        If Not Page.IsPostBack Then
            lstEnableHistoricalDataEdit = Nothing
            pnlAddCandidate.Visible = False
            pnlCandidateMessage.Visible = False
            bindData()
        End If
        lblGvCandidateMessage.Text = String.Empty
        bindCandidates()

    End Sub

    Private Sub bindData()
        bindHistoricalDataEdit()
        bindYearsDropdowns()
        bindTImeDropdowns()
        bindProxyDates()
        bindEligibleVoters()
    End Sub

    Private Sub bindHistoricalDataEdit()
        If IsNothing(lstEnableHistoricalDataEdit) OrElse lstEnableHistoricalDataEdit.Count <= 0 Then
            lstEnableHistoricalDataEdit = New List(Of Boolean)
            Dim isHistoricalDataEditEnabled As Boolean = False
            isHistoricalDataEditEnabled = ConfigurationManager.AppSettings("VSSA.ProxyEnableHistoricalData")
            lstEnableHistoricalDataEdit.Add(isHistoricalDataEditEnabled)
        End If
    End Sub

    Private Sub bindTImeDropdowns()
        Dim lstTimes As New List(Of String)
        lstTimes.Add("12:00 AM")
        lstTimes.Add("01:00 AM")
        lstTimes.Add("02:00 AM")
        lstTimes.Add("03:00 AM")
        lstTimes.Add("04:00 AM")
        lstTimes.Add("05:00 AM")
        lstTimes.Add("06:00 AM")
        lstTimes.Add("07:00 AM")
        lstTimes.Add("08:00 AM")
        lstTimes.Add("09:00 AM")
        lstTimes.Add("10:00 AM")
        lstTimes.Add("11:00 AM")
        lstTimes.Add("12:00 PM")
        lstTimes.Add("01:00 PM")
        lstTimes.Add("02:00 PM")
        lstTimes.Add("03:00 PM")
        lstTimes.Add("04:00 PM")
        lstTimes.Add("05:00 PM")
        lstTimes.Add("06:00 PM")
        lstTimes.Add("07:00 PM")
        lstTimes.Add("08:00 PM")
        lstTimes.Add("09:00 PM")
        lstTimes.Add("10:00 PM")
        lstTimes.Add("11:00 PM")
        ddlStartTime.DataSource = lstTimes
        ddlEndTime.DataSource = lstTimes
        ddlStartTime.DataBind()
        ddlEndTime.DataBind()
    End Sub

    Private Sub bindEligibleVoters()
        If IsNothing(lstEligibleVoters) Then
            lstEligibleVoters = GetEligibleVoters()
        End If
        Dim ev As ProxyEligibleVoters = (From e As ProxyEligibleVoters In lstEligibleVoters Where e.VotingYear.Equals(ddlYear3.SelectedValue) Select e).SingleOrDefault
        If Not IsNothing(ev) Then
            txtEligibleVoters.Text = ev.EligibleVoters
        Else
            txtEligibleVoters.Text = String.Empty
        End If
    End Sub

    Private Sub bindYearsDropdowns()
        If IsNothing(lstYears) Then
            lstYears = GetYears()
        End If
        ddlYear.DataSource = lstYears
        ddlYear2.DataSource = lstYears
        ddlYear3.DataSource = lstYears
        ddlYear.DataBind()
        ddlYear2.DataBind()
        ddlYear3.DataBind()
    End Sub

    Private Sub bindProxyDates()
        Dim bRefreshData As Boolean = False
        Dim pd As ProxyDate = Nothing
        If IsNothing(lstProxyDates) Then
            lstProxyDates = GetProxyDates()
            bRefreshData = True
        End If
        If Not Page.IsPostBack Then
            pd = (From d As ProxyDate In lstProxyDates Where d.votingYear.Equals(CStr(Date.Today.Year)) Select d).SingleOrDefault
        ElseIf bRefreshData = True Then
            pd = (From d As ProxyDate In lstProxyDates Where d.votingYear.Equals(CStr(ddlYear2.SelectedValue)) Select d).SingleOrDefault
        End If
        If Not Page.IsPostBack OrElse bRefreshData = True Then
            If Not IsNothing(pd) Then
                ddlYear2.SelectedValue = pd.votingYear
                txtStartDate.Text = FormatDateTime(pd.startDate, DateFormat.ShortDate).ToString()
                txtEndDate.Text = FormatDateTime(pd.endDate, DateFormat.ShortDate).ToString()
                Dim startTime As String = pd.startDate.ToString("hh:mm tt")
                ddlStartTime.SelectedValue = startTime
                Dim endTime As String = pd.endDate.ToString("hh:mm tt")
                ddlEndTime.SelectedValue = endTime
            Else
                txtStartDate.Text = String.Empty
                txtEndDate.Text = String.Empty
            End If
        End If
    End Sub
    Private Sub bindCandidates()
        lstCandidate = New List(Of ProxyCandidate)
        lstCandidate = GetProxyCandidates()
        ' show candidate id column before databind and hide after, to view value need to make column visible example: gvCandidates_SelectedIndexChanged
        gvCandidates.Columns(0).Visible = True
        gvCandidates.DataSource = lstCandidate
        gvCandidates.DataBind()
        gvCandidates.Columns(0).Visible = False
    End Sub
    Private Function GetYears() As List(Of String)
        ' dynamic function for getting years for dropdown from current year up to current year + 5
        lstYears = New List(Of String)
        Dim strYear As Integer
        If Not IsNothing(lstEnableHistoricalDataEdit) AndAlso lstEnableHistoricalDataEdit(0) = False Then
            strYear = Date.Today.Year
        Else
            If Date.Today.Year = 2017 Then
                strYear = Date.Today.AddYears(-1).Year
            Else
                strYear = 2017
            End If
        End If
        Dim lastYear As Integer = CInt(Date.Today.AddYears(5).Year)
        Do While strYear <= lastYear
            lstYears.Add(strYear.ToString())
            strYear += 1
        Loop
        Return lstYears
    End Function
    Public Function GetProxyDates() As List(Of ProxyDate)
        Dim lstDates As New List(Of ProxyDate)

        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim VotingDates As New GetActiveProxyVotingDatesResponse()
            Dim IsActive As Integer = 1
            VotingDates = ProxySvc.GetActiveProxyVotingDates(IsActive)
            If Not (VotingDates.Records Is Nothing) Then
                For i As Integer = 0 To VotingDates.Records.Count - 1
                    Dim PD As New ProxyDate
                    PD.startDate = FormatDateTime(VotingDates.Records(i).StartDate, DateFormat.GeneralDate).ToString()
                    PD.startTime = PD.startDate.ToString("hh:mm tt")
                    PD.endDate = FormatDateTime(VotingDates.Records(i).EndDate, DateFormat.GeneralDate).ToString()
                    PD.endTime = PD.endDate.ToString("hh:mm tt")
                    PD.votingYear = VotingDates.Records(i).VotingYear
                    lstDates.Add(PD)
                Next
            End If
        Catch ex As Exception
        End Try
        Return lstDates
    End Function
    Private Function GetEligibleVoters() As List(Of ProxyEligibleVoters)
        lstEligibleVoters = New List(Of ProxyEligibleVoters)

        Dim retVal As Integer = Integer.MinValue
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim EligibleVoters As New GetAllActiveEligibleVotersResponse()
            Dim isActive As Integer = 1
            EligibleVoters = ProxySvc.GetAllActiveEligibleVoters(isActive)
            If Not (EligibleVoters.Records Is Nothing) Then
                For i As Integer = 0 To EligibleVoters.Records.Count - 1
                    Dim ev As New ProxyEligibleVoters
                    ev.VotingYear = EligibleVoters.Records(i).VotingYear
                    ev.EligibleVoters = EligibleVoters.Records(i).EligibleVoters
                    lstEligibleVoters.Add(ev)
                Next
            End If
        Catch ex As Exception
        End Try
        Return lstEligibleVoters
    End Function
    Private Function GetProxyCandidates() As List(Of ProxyCandidate)
        lstCandidates = New List(Of ProxyCandidate)

        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim AllActiveCandidates As New GetAllActiveProxyCandidatesResponse()
            Dim isActive As Integer = 1
            AllActiveCandidates = ProxySvc.GetAllActiveProxyCandidates(isActive)
            If Not (AllActiveCandidates.Records Is Nothing) Then
                For i As Integer = 0 To AllActiveCandidates.Records.Count - 1
                    Dim PC As New ProxyCandidate
                    PC.CandidateID = AllActiveCandidates.Records(i).CandidateID
                    PC.CandidateName = AllActiveCandidates.Records(i).CandidateName
                    PC.OwnerID = AllActiveCandidates.Records(i).CandidateOwnerID
                    PC.VotingYear = AllActiveCandidates.Records(i).VotingYear
                    PC.Active = AllActiveCandidates.Records(i).IsActive
                    PC.IsWriteInCandidate = AllActiveCandidates.Records(i).IsWriteInCandidate
                    PC.ModifiedBy = AllActiveCandidates.Records(i).ModifiedBy
                    PC.ModifiedDate = AllActiveCandidates.Records(i).ModifiedDate
                    'PC.Action = "Delete"
                    lstCandidates.Add(PC)
                Next
            End If
        Catch ex As Exception
        End Try
        Return lstCandidates
    End Function
    Private Sub DeleteCandidate(ByVal Candidate As ProxyCandidate)
        Dim success As Boolean = True
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim DeleteCandidate As New InactivateProxyCandidateResponse()
            DeleteCandidate = ProxySvc.InactivateProxyCandidate(Candidate.CandidateName, Candidate.OwnerID, Candidate.VotingYear, String.Format("{0:yyyy-MM-dd hh:mm:ss.fff }", DateTime.Now), Environment.UserName.ToString())
            lstCandidate.Remove(Candidate)
        Catch ex As Exception
            success = False
        End Try
    End Sub
    Private Sub UpdateCandidate(ByVal Candidate As ProxyCandidate)
        Dim success As Boolean = True
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim InsertVote As New ModifyProxyCandidateResponse()
            InsertVote = ProxySvc.ModifyProxyCandidate(Candidate.CandidateID, Candidate.CandidateName, Candidate.OwnerID, Candidate.VotingYear, String.Format("{0:yyyy-MM-dd hh:mm:ss.fff }", DateTime.Now), Environment.UserName.ToString())
        Catch ex As Exception
            success = False
        End Try
    End Sub
    Private Sub InsertProxyDate(ByVal PD As ProxyDate)
        Dim success As Boolean = True
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim InsertProxyDate As New InsertProxyDatesResponse()
            Dim IsActive = 1
            Dim startDate As String = FormatDateTime(PD.startDate, DateFormat.GeneralDate).ToString() + " " + FormatDateTime(PD.startTime, DateFormat.LongTime).ToString()
            Dim endDate As String = FormatDateTime(PD.endDate, DateFormat.GeneralDate).ToString() + " " + FormatDateTime(PD.endTime, DateFormat.LongTime).ToString()
            InsertProxyDate = ProxySvc.InsertProxyDates(PD.votingYear, startDate, endDate, Environment.UserName.ToString(), String.Format("{0:yyyy-MM-dd hh:mm:ss.fff }", DateTime.Now), IsActive)
            Me.lblselectDates.Visible = True
            Me.lblselectDates.Text = "Your changes have been saved"
            Me.lblselectDates.ForeColor = Drawing.Color.Green
            bindProxyDates()
        Catch ex As Exception
            success = False
        End Try
    End Sub
    Private Sub InsertCandidate(ByVal Candidate As ProxyCandidate)
        Dim success As Boolean = True
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim InsertCandidate As New InsertProxyCandidateResponse()
            InsertCandidate = ProxySvc.InsertProxyCandidate(Candidate.CandidateName, Candidate.OwnerID, Candidate.VotingYear, 1, 0, String.Format("{0:yyyy-MM-dd hh:mm:ss.fff }", DateTime.Now), Environment.UserName.ToString())
            bindCandidates()
            pnlAddCandidate.Visible = False
        Catch ex As Exception
            success = False
        End Try
    End Sub
    Private Sub InsertEligibleVoters()
        Me.lblStatus.Text = String.Empty
        Dim success As Boolean = True
        Try
            Dim ProxySvc As New ProxyVotingService()
            Dim InsertEligibleVoters As New InsertProxyEligibleVotersResponse()
            InsertEligibleVoters = ProxySvc.InsertProxyEligibleVoters(ddlYear3.SelectedValue, txtEligibleVoters.Text, Environment.UserName.ToString(), String.Format("{0:yyyy-MM-dd hh:mm:ss.fff }", DateTime.Now))
            Me.lblStatus.Visible = True
            Me.lblStatus.Text = "Your changes have been saved"
            Me.lblStatus.ForeColor = Drawing.Color.Green
            lstEligibleVoters = Nothing
            bindEligibleVoters()
        Catch ex As Exception
            success = False
        End Try
    End Sub
    Private Sub ModifyCandidate(ByVal Candidate As ProxyCandidate)
        pnlAddCandidate.Visible = True
        If Candidate.VotingYear < Date.Today.Year Then
            If Not IsNothing(lstEnableHistoricalDataEdit) AndAlso lstEnableHistoricalDataEdit.Count > 0 AndAlso lstEnableHistoricalDataEdit(0) = False Then
                lblGvCandidateMessage.Text = "Editing of historical data is disabled, to enable contact an administrator."
                lblGvCandidateMessage.ForeColor = Drawing.Color.Red
                btnSave.Enabled = False
                txtCandidateName.Enabled = False
                txtCandidateOwnerID.Enabled = False
                pnlCandidateMessage.Visible = True
            End If
        Else
            txtCandidateName.Enabled = True
            txtCandidateOwnerID.Enabled = True
            btnSave.Enabled = True
            pnlCandidateMessage.Visible = False
        End If
        bindYearsDropdowns()
        If Not ddlYear.Items.Contains(New ListItem(Candidate.VotingYear)) Then
            ddlYear.Items.Insert(0, Candidate.VotingYear)
        End If
        ddlYear.SelectedValue = Candidate.VotingYear
        ddlYear.Enabled = False
        txtCandidateName.Text = Candidate.CandidateName
        txtCandidateOwnerID.Text = Candidate.OwnerID
    End Sub
    Protected Sub gvCandidates_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvCandidates.RowDeleting
        Dim row = gvCandidates.Rows(e.RowIndex)
        If Not IsNothing(row) Then
            Dim votingYear As String = row.Cells(3).Text
            If votingYear < Date.Today.Year AndAlso Not IsNothing(lstEnableHistoricalDataEdit) AndAlso lstEnableHistoricalDataEdit(0) = False Then
                pnlCandidateMessage.Visible = True
                lblGvCandidateMessage.Visible = True
                lblGvCandidateMessage.Text = "Editing of historical data is disabled, to enable contact an administrator."
                lblGvCandidateMessage.ForeColor = Drawing.Color.Red
                Exit Sub
            End If
        End If
        lblGvCandidateMessage.Text = String.Empty
        pnlCandidateMessage.Visible = False
        gvCandidates.Columns(0).Visible = True
        Dim CID As String = row.Cells(0).Text
        gvCandidates.Columns(0).Visible = False
        Dim PC As ProxyCandidate = (From C As ProxyCandidate In lstCandidate Where C.CandidateID.Equals(CID) Select C).SingleOrDefault
        If result = 1 Then
            DeleteCandidate(PC)
            lstCandidate = New List(Of ProxyCandidate)
            lstCandidate = GetProxyCandidates()
            bindCandidates()
            bindData()
        End If
    End Sub
    Protected Sub ddlYear2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlYear2.SelectedIndexChanged
        bindProxyDates()
        Dim pd As ProxyDate = (From d As ProxyDate In lstProxyDates Where d.votingYear.Equals(CStr(ddlYear2.SelectedValue)) Select d).SingleOrDefault
        If Not IsNothing(pd) Then
            txtStartDate.Text = FormatDateTime(pd.startDate, DateFormat.ShortDate).ToString()
            txtEndDate.Text = FormatDateTime(pd.endDate, DateFormat.ShortDate).ToString()
            ddlStartTime.SelectedValue = pd.startTime
            ddlEndTime.SelectedValue = pd.endTime
        Else
            txtStartDate.Text = String.Empty
            txtEndDate.Text = String.Empty
            ddlStartTime.SelectedIndex = 0
            ddlEndTime.SelectedIndex = 0
        End If
    End Sub
    Private Sub ddlYear3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear3.SelectedIndexChanged
        bindEligibleVoters()
    End Sub
    Protected Sub gvCandidates_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvCandidates.SelectedIndexChanged
        lblGvCandidateMessage.Text = String.Empty
        pnlCandidateMessage.Visible = False
        If gvCandidates.SelectedIndex >= 0 AndAlso Not String.IsNullOrEmpty(Session("CandidateRowForEdit")) AndAlso Session("CandidateRowForEdit") = gvCandidates.SelectedIndex Then
            'clicked edit again for same row, unselect the row
            gvCandidates.SelectedIndex = -1
            Session("CandidateRowForEdit") = Nothing
            pnlAddCandidate.Visible = False
            btnSave.Enabled = True
            txtCandidateName.Enabled = True
            txtCandidateOwnerID.Enabled = True
            hdnCandidateID.Value = String.Empty
            txtCandidateName.Text = String.Empty
            txtCandidateOwnerID.Text = String.Empty
            ddlYear.Enabled = True
            ddlYear.SelectedIndex = 0
        Else
            Dim row = gvCandidates.SelectedRow
            gvCandidates.Columns(0).Visible = True
            Dim CID As String = row.Cells(0).Text
            gvCandidates.Columns(0).Visible = False
            Dim PC As ProxyCandidate = (From C As ProxyCandidate In lstCandidate Where C.CandidateID.ToLower.Equals(CID) Select C).SingleOrDefault
            If Not IsNothing(PC) Then
                PC.CandidateName = row.Cells(1).Text
                PC.ModifiedBy = Environment.UserName.ToString()
                PC.ModifiedDate = DateTime.Now
                PC.OwnerID = row.Cells(2).Text
                hdnCandidateID.Value = PC.CandidateID
                ModifyCandidate(PC)
                Session("CandidateRowForEdit") = gvCandidates.SelectedIndex
            End If
        End If
    End Sub
    Protected Sub gvCandidates_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvCandidates.PageIndexChanging
        gvCandidates.PageIndex = e.NewPageIndex
        gvCandidates.DataBind()
    End Sub
    Protected Sub lbAddNew_Click(ByVal sender As Object, e As EventArgs) Handles lbAddNew.Click
        Me.lblStatus.Visible = False
        bindYearsDropdowns()
        Session("CandidateRowForEdit") = Nothing
        If gvCandidates.SelectedIndex > -1 OrElse Not pnlAddCandidate.Visible Then
            gvCandidates.SelectedIndex = -1
            pnlAddCandidate.Visible = True
            btnSave.Enabled = True
            txtCandidateName.Enabled = True
            txtCandidateOwnerID.Enabled = True
            hdnCandidateID.Value = String.Empty
            txtCandidateName.Text = String.Empty
            txtCandidateOwnerID.Text = String.Empty
            ddlYear.Enabled = True
            ddlYear.SelectedIndex = 0
        ElseIf pnlAddCandidate.Visible Then
            pnlAddCandidate.Visible = False
            lblGvCandidateMessage.Text = String.Empty
            pnlCandidateMessage.Visible = False
        Else
            pnlAddCandidate.Visible = True
            btnSave.Enabled = True
            txtCandidateName.Enabled = True
            txtCandidateOwnerID.Enabled = True
            hdnCandidateID.Value = String.Empty
            txtCandidateName.Text = String.Empty
            txtCandidateOwnerID.Text = String.Empty
            ddlYear.Enabled = True
            ddlYear.SelectedIndex = 0
        End If
    End Sub
    Public Function GetWriteInCandidate(ByVal inCandidateName As String, ByVal ActiveYear As String) As Integer
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
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Dim intCandidateID As Integer = Integer.MinValue
        Try
            If (txtCandidateName.Text.Length = 0) Then
                pnlCandidateMessage.Visible = True
                lblGvCandidateMessage.Text = "Please enter Candidate Name"
                lblGvCandidateMessage.ForeColor = Drawing.Color.Red
                Exit Sub
            End If
            If (txtCandidateOwnerID.Text.Length = 0) Then
                pnlCandidateMessage.Visible = True
                lblGvCandidateMessage.Text = "Please enter Owner ID"
                lblGvCandidateMessage.ForeColor = Drawing.Color.Red
                Exit Sub
            End If
            If Not IsNumeric(txtCandidateOwnerID.Text.Trim) Then
                pnlCandidateMessage.Visible = True
                lblGvCandidateMessage.Text = "Please enter numeric Owner ID"
                lblGvCandidateMessage.ForeColor = Drawing.Color.Red
                Exit Sub
            End If
            If Not isValidCandidateName(txtCandidateName.Text.Trim) Then
                pnlCandidateMessage.Visible = True
                lblGvCandidateMessage.Text = "Please enter a valid Candidate Name"
                lblGvCandidateMessage.ForeColor = Drawing.Color.Red
                Exit Sub
            End If
        Catch ex As Exception
        End Try
        If Not String.IsNullOrEmpty(hdnCandidateID.Value) Then
            Dim PC As ProxyCandidate = (From C As ProxyCandidate In lstCandidate Where C.CandidateID.ToLower.Equals(hdnCandidateID.Value) Select C).SingleOrDefault
            PC.CandidateName = txtCandidateName.Text.Trim
            PC.OwnerID = txtCandidateOwnerID.Text.Trim
            UpdateCandidate(PC)
            lstCandidate = New List(Of ProxyCandidate)
            lstCandidate = GetProxyCandidates()
            bindCandidates()
            pnlAddCandidate.Visible = False
            gvCandidates.SelectedIndex = -1
        Else
            intCandidateID = GetWriteInCandidate(txtCandidateName.Text.Trim, ddlYear.SelectedValue)
            If intCandidateID = Integer.MinValue Then
                Dim PC As New ProxyCandidate
                PC.CandidateName = txtCandidateName.Text.Trim
                PC.OwnerID = txtCandidateOwnerID.Text.Trim
                PC.VotingYear = ddlYear.SelectedValue
                PC.ModifiedDate = DateTime.Now
                PC.ModifiedBy = Environment.UserName.ToString()
                InsertCandidate(PC)
                lstCandidate = New List(Of ProxyCandidate)
                lstCandidate = GetProxyCandidates()
                bindCandidates()
                pnlAddCandidate.Visible = False
                gvCandidates.SelectedIndex = -1
            Else
                pnlCandidateMessage.Visible = True
                lblGvCandidateMessage.Text = "Candidate Name already exists"
                lblGvCandidateMessage.ForeColor = Drawing.Color.Red
            End If
        End If
        Session("CandidateRowForEdit") = Nothing
    End Sub
    Private Sub btnSaveDates_Click(sender As Object, e As EventArgs) Handles btnSaveDates.Click
        Me.lblselectDates.Text = String.Empty
        Try
            If (txtStartDate.Text.Length = 0) Then
                Me.lblselectDates.Visible = True
                Me.lblselectDates.Text = "Please select Start Date"
                Me.lblselectDates.ForeColor = Drawing.Color.Red
                Exit Try
            End If
            If (txtEndDate.Text.Length = 0) Then
                Me.lblselectDates.Visible = True
                Me.lblselectDates.Text = "Please select End Date"
                Me.lblselectDates.ForeColor = Drawing.Color.Red
                Exit Try
            End If
            Dim startDate As DateTime = FormatDateTime(txtStartDate.Text, DateFormat.ShortDate).ToString()
            Dim endDate As DateTime = FormatDateTime(txtEndDate.Text, DateFormat.ShortDate).ToString()
            Dim strYear As String = ddlYear2.SelectedValue
            Dim pd As New ProxyDate
            pd.startDate = startDate
            pd.startTime = ddlStartTime.SelectedValue
            pd.endDate = endDate
            pd.endTime = ddlEndTime.SelectedValue
            pd.votingYear = strYear
            InsertProxyDate(pd)
            lstProxyDates = Nothing
            bindProxyDates()
            ' Reset the proxy voting active status since this might have changed
            _Default.isProxyVotingActive = New List(Of Boolean)
        Catch
            'TODO: handle error and add msg to error label
        End Try
    End Sub
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
    Private Sub btnSave2_Click(sender As Object, e As EventArgs) Handles btnSave2.Click
        Try
            If (txtEligibleVoters.Text.Length = 0) Then
                Me.lblStatus.Visible = True
                Me.lblStatus.Text = "Enter number of eligible owners to vote"
                Me.lblStatus.ForeColor = Drawing.Color.Red
                Exit Try
            End If
            InsertEligibleVoters()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub AddConfirmDelete(gv As GridView, e As GridViewRowEventArgs)
        pnlAddCandidate.Visible = False
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim VotingYear As String = e.Row.Cells(3).Text
            If IsNothing(lstEnableHistoricalDataEdit) Then
                bindHistoricalDataEdit()
            End If
            If Not IsNothing(lstEnableHistoricalDataEdit) AndAlso lstEnableHistoricalDataEdit(0) = False AndAlso VotingYear < Date.Today.Year Then
                ' Only add confirm alert if not historical data or else if edit of historical data is enabled
                Exit Sub
            End If
            For Each dcf As DataControlField In gv.Columns
                If dcf.ToString() = "CommandField" Then
                    If DirectCast(dcf, CommandField).ShowDeleteButton = True Then
                        e.Row.Cells(gv.Columns.IndexOf(dcf)).Attributes.Add("onclick", "return confirm(""Are you sure you want to delete this candidate?"")")
                        result = 1
                    End If
                End If
            Next
        End If
    End Sub
    Protected Sub gvCandidates_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCandidates.RowDataBound
        AddConfirmDelete(DirectCast(sender, GridView), e)
    End Sub
End Class
Public Class ProxyCandidate
    Private _CandidateID As Integer
    Private _CandidateName As String
    Private _VotingYear As String
    Private _ModifiedBy As String
    Private _ModifiedDate As String
    Private _OwnerID As String
    Private _Action As String
    Private _Active As String
    Private _IsWriteInCandidate As String
    Public Property IsWriteInCandidate As String
        Get
            Return _IsWriteInCandidate
        End Get
        Set(value As String)
            _IsWriteInCandidate = value
        End Set
    End Property
    Public Property CandidateID As String
        Get
            Return _CandidateID
        End Get
        Set(value As String)
            _CandidateID = value
        End Set
    End Property
    Public Property OwnerID As String
        Get
            Return _OwnerID
        End Get
        Set(value As String)
            _OwnerID = value
        End Set
    End Property
    Public Property VotingYear As String
        Get
            Return _VotingYear
        End Get
        Set(value As String)
            _VotingYear = value
        End Set
    End Property
    Public Property CandidateName As String
        Get
            Return _CandidateName
        End Get
        Set(value As String)
            _CandidateName = value
        End Set
    End Property

    Public Property ModifiedBy As String
        Get
            Return _ModifiedBy
        End Get
        Set(value As String)
            _ModifiedBy = value
        End Set
    End Property
    Public Property ModifiedDate As String
        Get
            Return _ModifiedDate
        End Get
        Set(value As String)
            _ModifiedDate = value
        End Set
    End Property
    Public Property Action As String
        Get
            Return _Action
        End Get
        Set(value As String)
            _Action = value
        End Set
    End Property
    Public Property Active As String
        Get
            Return _Active
        End Get
        Set(value As String)
            _Active = value
        End Set
    End Property
End Class
Public Class ProxyEligibleVoters
    Private _VotingYear As String
    Private _EligibleVoters As String
    Public Property VotingYear As String
        Get
            Return _VotingYear
        End Get
        Set(value As String)
            _VotingYear = value
        End Set
    End Property
    Public Property EligibleVoters As String
        Get
            Return _EligibleVoters
        End Get
        Set(value As String)
            _EligibleVoters = value
        End Set
    End Property
End Class