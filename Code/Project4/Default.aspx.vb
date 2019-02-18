Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Data
Imports System.Collections.Generic
Imports System.Reflection
Imports VSSA.BluegreenOnline
Imports VSSA.OwnerServices
Imports VSSA.fetchresp
Imports System.Net
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Drawing
Imports VSSA.ProxyVotingServices
Imports VSSA.ActiveVotingDatesResp

Module TSWPerson
    Public Property TSWEditUsers As String
    Sub New()
        TSWEditUsers = WebConfigurationManager.AppSettings("VSSA.TSWPersonEdit")
    End Sub
End Module
Partial Class _Default
    Inherits VSSABaseClass

    Public owner As New BXG_Owner
    Public externalCall As Boolean = False
    Public raisedRedFlag As Boolean = False
    Public tmpOwner As New BXG_Owner
    Public ChangedOwner As New BXG_Owner
    Public FormData As New BXG_Owner
    Public OriginalOwner As New BXG_Owner
    Public AddressPass As String
    Public Shared lstAllowedAddressUploadUsers As List(Of String)
    Public Shared lstAllowedProxyAdminUsers As List(Of String)
    Public Shared lstAllowedProxyVotingUsers As List(Of String)
    Public Shared lstAllowedGroupReset As List(Of String)
    Public Shared isProxyVotingActive As List(Of Boolean)
    '' Public Shared lstAuthenticatedUsers As List(Of String)
    Public TransactionError As String = ""
    Dim vssaOwner As New Bluegreenowner
    Public Err As String = "Pass"
    Public emailOnPageLoad As String
    Private Shared ReadOnly SpecialChars As Char() = "`:;!@*()$%^&~_+{}[]|\?><""".ToCharArray()
    Public bxgowner As New OwnerFetchResponse
    Public lstAllowedToEditTSW = New List(Of String)
    Protected Sub lbAddressUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbAddressUpload.Click
        Server.Transfer("./AddressUpload/AddressUpload.aspx")
    End Sub
    Protected Sub lbProxyAdmin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbProxyAdmin.Click
        Response.Redirect("ProxyAdmin.aspx")
    End Sub

    Protected Sub lbResetGroupsForAccess_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbResetGroupsForAccess.Click
        lstAuthenticatedUsers = New List(Of String)
        lstAllowedAddressUploadUsers = New List(Of String)
        lstAllowedProxyAdminUsers = New List(Of String)
        lstAllowedProxyVotingUsers = New List(Of String)
        lstAllowedGroupReset = New List(Of String)
        isProxyVotingActive = New List(Of Boolean)
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, e As EventArgs) Handles btnSearch.Click
        If Not Session("SearchParam") Is Nothing Then
            Session("SearchParam") = Nothing
        End If
        If Not Session("SearchData") Is Nothing Then
            Session("SearchData") = Nothing
        End If
        If Not Session("NoTSWPersonID") = Nothing Then
            Session("NoTSWPersonID") = Nothing
        End If
        If Not Session("CurrentTSWPersonID") = Nothing Then
            Session("CurrentTSWPersonID") = Nothing
        End If
        If Not Session("UpdatedTSWPersonID") = Nothing Then
            Session("UpdatedTSWPersonID") = Nothing
        End If
        If Not Session("searchAllOwners") Is Nothing Then
            Session("searchAllOwners") = Nothing
        End If
        Me.fvOwner.Visible = False
        Me.ownerMoreDetails.Visible = False
        Me.gvContractInfo.Visible = False
        Me.gvFirstLogin.Visible = False
        Me.Label1.Visible = False
        Me.gvLastLogin.Visible = False
        Me.gvLoginCount.Visible = False
        Me.gvAccountHolders.Visible = False
        Me.gvARVACT.Visible = False
        Me.gvCollectionCode.Visible = False
        Me.pnlHelpText.Visible = True
        Me.label2.Visible = False
        Me.lblTwsID.Visible = False
        Me.lblCollCode.Visible = False
        Me.gvSearchResults.Visible = False
        Me.moreInfo.Visible = False
        Me.lblfeedback.Visible = False
        Me.pnlGeneralMessage.Visible = False
        If Not Session("OwnerTotalPoints") Is Nothing Then
            Session("OwnerTotalPoints") = Nothing
        End If
        Dim SearchParam As List(Of String) = New List(Of String)()
        If Me.txtARVACT.Text = "" And Me.txtEmail.Text = "" And Me.txtFName.Text = "" And Me.txtLName.Text = "" And Me.txtPhone.Text = "" And Me.tbSrchTSW.Text = "" Then


            Me.lblTPExp.Text = "You must enter search criteria."
            Me.lblRegistered.Visible = False
            Me.Reg.Visible = False
            Me.lblTPExp.ForeColor = Drawing.Color.Red
            Me.lblTPExp.Visible = True

        Else
            If Not (Me.txtARVACT.Text = "") Then
                SearchParam.Add(Me.txtARVACT.Text)
            Else
                SearchParam.Add("")
            End If
            If Not (Me.txtEmail.Text = "") Then
                SearchParam.Add(Me.txtEmail.Text)
            Else
                SearchParam.Add("")
            End If
            If Not (Me.txtFName.Text = "") Then
                SearchParam.Add(Me.txtFName.Text)
            Else
                SearchParam.Add("")
            End If
            If Not (Me.txtLName.Text = "") Then
                SearchParam.Add(Me.txtLName.Text)
            Else
                SearchParam.Add("")
            End If
            Dim validphone As String = checkForNumbers(txtPhone.Text)
            Me.txtPhone.Text = validphone
            If Not (Me.txtPhone.Text = "") Then
                SearchParam.Add(Me.txtPhone.Text)
            Else
                SearchParam.Add("")
            End If
            If Not (Me.tbSrchTSW.Text = "") Then
                SearchParam.Add(Me.tbSrchTSW.Text)
            Else
                SearchParam.Add("")
            End If

            Session("SearchParam") = SearchParam
            Me.pnlHelpText.Visible = False
            SortDirection = "ASC"
            SortColumn = "ARVACT"
            BindGridView()
        End If

    End Sub
    Protected Sub gvSearchResults_DataBound(sender As Object, e As EventArgs)
        Dim PrimaryArray As List(Of String) = New List(Of String)()
        If Not Session("PrimaryArray") Is Nothing Then
            PrimaryArray = Session("PrimaryArray")

            For Each row As GridViewRow In gvSearchResults.Rows
                For Each cell As TableCell In row.Cells
                    Dim rowFirstName As String = row.Cells(2).Text
                    Dim rowLastName As String = row.Cells(3).Text
                    If rowFirstName.Contains("&#39;") Then
                        rowFirstName = rowFirstName.Replace("&#39;", "'")
                    End If
                    If rowLastName.Contains("&#39;") Then
                        rowLastName = rowFirstName.Replace("&#39;", "'")
                    End If
                    Dim rowFullName As String = rowLastName + ", " + rowFirstName
                    If PrimaryArray.Contains(rowFullName) OrElse PrimaryArray.Contains(rowFirstName) OrElse PrimaryArray.Contains(rowLastName) Then
                        cell.Font.Bold = True

                    End If
                Next
            Next
        End If
        Dim columnIndex As Integer = 0
        For Each headerCell As DataControlFieldHeaderCell In gvSearchResults.HeaderRow.Cells

            If headerCell.ContainingField.SortExpression = SortColumn Then
                columnIndex = gvSearchResults.HeaderRow.Cells.GetCellIndex(headerCell)
                Exit For
            End If
        Next
    End Sub
    Public Property SortColumn As String
        Get
            Return Convert.ToString(ViewState("SortColumn"))
        End Get
        Set(ByVal value As String)
            ViewState("SortColumn") = value
        End Set
    End Property
    Public Property SortDirection As String
        Get
            Return Convert.ToString(ViewState("SortDirection"))
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property
    Private Sub BindGrid()
        Dim dt = Session("searchAllOwners")

        If dt IsNot Nothing Then
            If Not dt.TableName.Equals("tblError") Then
                If dt.Rows.Count = 0 Then
                    Me.lblError.Text = "No User found."
                    Me.lblTPExp.ForeColor = Drawing.Color.Red
                    Me.lblTPExp.Visible = True
                    txtFName.Text = txtFName.Text.Trim()
                    txtLName.Text = txtLName.Text.Trim()
                Else
                    dt.DefaultView.Sort = SortColumn & " " & SortDirection
                    gvSearchResults.DataSource = dt
                    gvSearchResults.DataBind()
                End If
            Else
                Me.lblError.Text = "No User found."
                Me.lblTPExp.ForeColor = Drawing.Color.Red
                Me.lblTPExp.Visible = True
            End If
        End If
    End Sub
    Private Function isNumber(ByVal s As String) As Boolean
        For i As Integer = 0 To s.Length - 1
            If Char.IsDigit(s(i)) = False Then Return False
        Next

        Return True
    End Function
    Private Function BindGridView() As DataTable
        Session("BoomiOwner") = Nothing
        If Not Session("CurrentPage") Is Nothing Then
            Session("CurrentPage") = 0
            gvSearchResults.PageIndex = 0
        End If
        Me.pnlGeneralMessage.Visible = False
        Dim sPhone As String = ""
        For x As Integer = 0 To txtPhone.Text.Length - 1
            If IsNumeric(txtPhone.Text.Substring(x, 1)) Then
                sPhone &= txtPhone.Text.Substring(x, 1)
            End If
        Next
        Try
            If (txtARVACT.Text.Length > 0) AndAlso isNumber(txtARVACT.Text) Then
                If isNumber(txtARVACT.Text) Then
                    Dim Ownersvc As New OwnerService()
                    Dim OwnerSvcResp As New OwnerFetchResponse()
                    OwnerSvcResp = Ownersvc.OwnerFetch(txtARVACT.Text)
                    Dim AcctNumbers As List(Of String) = New List(Of String)()
                    For x As Integer = 0 To OwnerSvcResp.Account.Memberships.VacationClubMembership.Accounts.AccountInfo.Count - 1
                        If OwnerSvcResp.Account.Memberships.VacationClubMembership.Accounts.AccountInfo(x).Legacy.ProjectNumber = "50" Then
                            AcctNumbers.Add(OwnerSvcResp.Account.Memberships.VacationClubMembership.Accounts.AccountInfo(x).AccountNumber)
                        End If
                    Next
                    Session("VCAccountNumber") = AcctNumbers
                    Session("BoomiOwner") = OwnerSvcResp
                    bxgowner = Session("BoomiOwner")
                    Session("arvact") = txtARVACT.Text
                    'Check arvact against the black list
                    raisedRedFlag = OwnerBlackList.isOwnerIntheBlackList(Convert.ToInt32(txtARVACT.Text))
                    If raisedRedFlag = True Then
                        pnlGeneralMessage.Visible = True
                        Me.lblGeneralMessage.ForeColor = Drawing.Color.Red
                        Me.lblGeneralMessage.Text = "ARVACT number requested is blacklisted. Please contact the IT Dept for further information."
                    End If
                Else
                    Exit Try
                End If
            End If
        Catch ex As Exception

        End Try

        Dim ds As New DataSet
        Dim dsPrimary As New DataSet
        Dim dt As DataTable = New DataTable
        Dim dtPrimary As DataTable = New DataTable
        Dim da As New SqlDataAdapter()
        Dim primaryfirst As String = Nothing
        Dim primarylast As String = Nothing
        Dim PrimaryFullName As String = Nothing
        Dim PrimaryArray As List(Of String) = New List(Of String)()
        Try
            If raisedRedFlag = False Then
                dsPrimary = OwnerData.searchPrimaryOwners(txtARVACT.Text, txtEmail.Text, txtLName.Text, txtFName.Text, sPhone, tbSrchTSW.Text)
                dtPrimary = dsPrimary.Tables(0)
                Session("searchPrimaryOwners") = dtPrimary
                For i As Integer = 0 To dtPrimary.Rows.Count - 1
                    Dim dr As DataRow = dtPrimary.Rows(i)
                    If Not dtPrimary.Rows.Item(i).Item("NameFirst").ToString() = "NULL" AndAlso Not dtPrimary.Rows.Item(i).IsNull("NameFirst") AndAlso Not dtPrimary.Rows.Item(i).Item("NameFirst") Is DBNull.Value Then
                        primaryfirst = dtPrimary.Rows.Item(i).Item("NameFirst").ToString()
                    Else
                        primaryfirst = String.Empty
                    End If
                    If Not dtPrimary.Rows.Item(i).Item("NameLast").ToString() = "NULL" AndAlso Not dtPrimary.Rows.Item(i).IsNull("NameLast") AndAlso Not dtPrimary.Rows.Item(i).Item("NameLast") Is DBNull.Value Then
                        primarylast = dtPrimary.Rows.Item(i).Item("NameLast").ToString()
                    Else
                        primarylast = String.Empty
                    End If
                    If primaryfirst Is Nothing Or primaryfirst = "" Then
                        PrimaryFullName = primarylast.Trim()
                    ElseIf primarylast Is Nothing Or primarylast = "" Then
                        PrimaryFullName = primaryfirst.Trim()
                    Else
                        PrimaryFullName = primarylast + ", " + primaryfirst
                    End If

                    PrimaryArray.Add(PrimaryFullName)
                    Session("PrimaryArray") = PrimaryArray
                Next
                txtFName.Text = txtFName.Text.Trim()
                txtFName.Text = txtFName.Text.PadLeft(txtFName.Text.Length + 1, " "c)
                ds = OwnerData.searchAllOwners(txtARVACT.Text, txtEmail.Text, txtLName.Text, txtFName.Text, sPhone, tbSrchTSW.Text)
                dt = ds.Tables(0)
                Dim dv As DataView = dt.AsDataView
                If ds.Tables.Contains("tblError") Then
                    Me.lblTPExp.Text = "No User found."
                    Me.lblTPExp.ForeColor = Drawing.Color.Red
                    Me.lblTPExp.Visible = True
                    Me.lblError.Text = "No User found"
                    Me.lblError.ForeColor = Drawing.Color.Red
                    Exit Try
                End If
                If ds.Tables.Count = 0 Then
                    Me.gvSearchResults.Visible = False
                    Me.gvFirstLogin.Visible = False
                    Me.gvLastLogin.Visible = False
                    Me.gvLoginCount.Visible = False
                    Me.gvContractInfo.Visible = False
                    Me.fvOwner.Visible = False
                    Me.ownerMoreDetails.Visible = False
                    Me.gvARVACT.Visible = False
                    Me.gvAccountHolders.Visible = False
                    Me.Label1.Visible = False
                    Me.label2.Visible = False
                    Me.lblCollCode.Visible = False
                    Me.lblTwsID.Visible = False
                    Me.lblTPExp.Text = "No User found."
                    Me.lblTPExp.ForeColor = Drawing.Color.Red
                    Me.lblTPExp.Visible = True
                    Me.pnlHelpText.Visible = True
                    Me.lblfeedback.Visible = False
                    Me.lblfeedback.Text = ""
                    Exit Try
                ElseIf ds.Tables(0).Rows.Count = 0 Then
                    Me.gvSearchResults.Visible = False
                    Me.gvFirstLogin.Visible = False
                    Me.gvLastLogin.Visible = False
                    Me.gvLoginCount.Visible = False
                    Me.gvContractInfo.Visible = False
                    Me.fvOwner.Visible = False
                    Me.ownerMoreDetails.Visible = False
                    Me.gvARVACT.Visible = False
                    Me.gvAccountHolders.Visible = False
                    Me.Label1.Visible = False
                    Me.label2.Visible = False
                    Me.lblTwsID.Visible = False
                    Me.lblCollCode.Visible = False
                    Me.lblTPExp.Text = "No User found."
                    Me.lblTPExp.ForeColor = Drawing.Color.Red
                    Me.lblTPExp.Visible = True
                    Me.lblError.Text = "No User Found."
                    Me.lblError.ForeColor = Drawing.Color.Red
                    Me.pnlHelpText.Visible = False
                    Me.lblfeedback.Visible = False
                    Me.lblfeedback.Text = ""
                    txtFName.Text = txtFName.Text.Trim()
                    txtLName.Text = txtLName.Text.Trim()
                    Exit Try
                Else
                    Try
                        'Sometimes an account might not have an email address.  If there is no email address then you can't verify if the user is registered.
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
                                        txtcurrentEmail.Text = row.Item("Email")
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
                            If Not row.Item("secFullName") = String.Empty Then
                                Dim strValue As String = row.Item("secFullName").ToString().Trim()
                                Dim ext As String = strValue.Substring(0, strValue.LastIndexOf(",") + 1)
                                If ext.Contains(","c) Then
                                    Dim index As Integer = strValue.IndexOf(","c)
                                    Dim Last As String = strValue.Substring(0, index)
                                    Dim First As String = strValue.Substring(strValue.IndexOf(",") + 1).Trim()
                                    If Last = "" Then
                                        row.Item("NameLast") = System.DBNull.Value
                                    Else
                                        row.Item("NameLast") = Last
                                    End If
                                    If First = "" Then
                                        row.Item("NameFirst") = System.DBNull.Value
                                    Else
                                        row.Item("NameFirst") = First
                                    End If
                                Else
                                    Dim OnlyLast As String = strValue
                                    Dim OnlyFirst As String = Nothing
                                    If OnlyLast = "" Then
                                        row.Item("NameLast") = System.DBNull.Value
                                    Else
                                        row.Item("NameLast") = OnlyLast
                                    End If
                                    row.Item("NameFirst") = ""

                                End If
                            End If
                        Next

                        If ds.Tables(0).Columns.Contains("arvact") Then

                            If dt IsNot Nothing Then
                                If (Not (SortColumn) Is Nothing) Then
                                    dt.DefaultView.Sort = SortColumn & " " & SortDirection
                                    gvSearchResults.DataSource = dv
                                    Session("SearchData") = dv.ToTable()
                                Else
                                    gvSearchResults.DataSource = dt
                                    Session("SearchData") = dt.Copy()
                                End If
                                gvSearchResults.DataBind()
                            End If
                            txtFName.Text = txtFName.Text.Trim()
                            Me.gvSearchResults.Visible = True
                            Me.div_ownerInfo.Visible = False
                            Me.divSearchResults.Visible = True
                            Me.fvOwner.Visible = False
                            Me.ownerMoreDetails.Visible = False
                            Me.moreInfo.Visible = False
                            Me.lblError.Text = ""
                            Me.lblfeedback.Text = ""
                        Else
                            Me.lblError.Text = "Network error has occurred."
                            Me.lblError.ForeColor = Drawing.Color.Red
                            Me.lblError.Visible = True
                        End If
                        Session("ownerEmail") = ds.Tables(0).Rows(0).Item("Email").ToString
                    Catch ex As Exception
                        Me.lblError.Text = ex.Message
                        Me.lblError.ForeColor = Drawing.Color.Red
                    End Try
                End If
                'Session(ViewState("_PageID").ToString() & "searchAllOwners") = dt
                Session("searchAllOwners") = dt
                Return dt

            End If
        Catch ex As Exception

        End Try

        Return dt


    End Function
    Protected Sub gvSearchResults_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSearchResults.RowCommand
        If e.CommandName = "GetDetail" Then
            Try
                'Save this to session var to be used later if rep logs in as owner.
                Session("senderDetails") = sender
                Session("senderArgs") = e
                Dim link As LinkButton = e.CommandSource
                Dim row As GridViewRow = link.NamingContainer
                Dim data2 As String = row.Cells(1).Text
                Dim data As String = IIf(String.IsNullOrEmpty(data2), "No", data2)
                Session("Registered") = data
                getOwnerDetails(sender, e)
            Catch ex As Exception

                Me.lblTPExp.Text = "An error occurred getting data."
                Me.lblTPExp.ForeColor = Drawing.Color.Red
                Me.lblTPExp.Visible = True

            End Try

        End If

    End Sub
    Public Function checkForNumbers(input As String) As String
        Dim sb As New StringBuilder()
        For Each c As Char In input
            If [Char].IsDigit(c) Then
                sb.Append(c)
            End If
        Next
        Return sb.ToString()
    End Function

    Protected Sub fvOwner_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewCommandEventArgs) Handles fvOwner.ItemCommand

        Dim fvRow As FormViewRow = fvOwner.Row
        Dim lblArvact As Label = fvRow.FindControl("lblARVACT")
        Dim ddlCountry As DropDownList = fvRow.FindControl("ddlCountry")
        Dim ddlState As DropDownList = fvRow.FindControl("ddlState")
        Dim txtAddress1 As TextBox = fvRow.FindControl("txtAddress1")
        Dim txtAddress2 As TextBox = fvRow.FindControl("txtAddress2")
        Dim txtAddress3 As TextBox = fvRow.FindControl("txtAddress3")
        Dim txtCity As TextBox = fvRow.FindControl("txtCity")
        Dim txtZIP As TextBox = fvRow.FindControl("txtZIP")
        Dim txtPhoneHome As TextBox = fvRow.FindControl("txtHomePhone")
        Dim txtPhoneAlt As TextBox = fvRow.FindControl("txtAltPhone")
        Dim vZip As RequiredFieldValidator = fvRow.FindControl("vzip")
        Dim txtcurrentEmail As TextBox = fvRow.FindControl("txtcurrentEmail")
        Dim txtConfirmEmail As TextBox = fvRow.FindControl("txtConfirmEmail")
        Dim cvEmail As CompareValidator = fvRow.FindControl("cvEmail")
        Dim lblConfirmEmail As Label = fvRow.FindControl("lbConfirmEmail")
        Dim btnUpdateEmail As ImageButton = fvRow.FindControl("btnUpdate2")
        Dim btnCancelEmailUpdate As ImageButton = fvRow.FindControl("btnCancelEmailUpdate")
        Dim pnlUserInfo As Panel = fvRow.FindControl("pnlUserInfo")
        Dim btnChangePwd As ImageButton = fvRow.FindControl("btnChangePwd")
        Dim btnVerify As ImageButton = fvRow.FindControl("iBtnVerify")
        Dim lblUpdateMessage As Label = fvRow.FindControl("lblUpdateMessage")

        If Session.IsNewSession Then

            Me.fvOwner.Visible = False
            Me.ownerMoreDetails.Visible = False
            Me.gvAccountHolders.Visible = False
            Me.gvARVACT.Visible = False
            Me.gvCollectionCode.Visible = False
            Me.gvContractInfo.Visible = False
            Me.gvDebug.Visible = False
            Me.gvFirstLogin.Visible = False
            Me.gvLastLogin.Visible = False
            Me.gvLoginCount.Visible = False
            Me.gvSearchResults.Visible = False
            Me.divSearchResults.Visible = False
            Me.moreInfo.Visible = False
            Me.lblTPExp.Text = "Session Expired. Please run search again."
            Me.lblTPExp.Visible = True
            Me.lblTPExp.ForeColor = Drawing.Color.Red
            Me.pnlHelpText.Visible = True
            Me.pnlSearch.Visible = True
            Return

        End If

        'Pull the owner object from the session
        owner = Session("owner")

        If owner Is Nothing Then

            If Session Is Nothing Then
                Response.Redirect("default.aspx")
                Return
            End If

            owner = OwnerData.searchOwners(lblArvact.Text)

        End If

        'Clone to verify changed and then compared to the 
        'original owner to track changes.

        tmpOwner = owner.cloneOwner()

        With tmpOwner
            .Address1 = txtAddress1.Text
            .Address2 = txtAddress2.Text
            .Address3 = txtAddress3.Text
            .City = txtCity.Text
            .State = ddlState.SelectedItem.Value
            .Subdivision = ddlState.SelectedItem.Text
            .PostalCode = txtZIP.Text
            .CountryCode = ddlCountry.SelectedValue
            .PhoneHome = txtPhoneHome.Text
            .PhoneCell = txtPhoneAlt.Text

        End With
        If e.CommandName = "UpdateContactInfo" Then
            If Not Session("changes") = Nothing Then
                lblUpdateMessage.Text = Session("changes").ToString
                If lblUpdateMessage.Text = "Since no change was detected, no information was updated." Then
                    lblUpdateMessage.ForeColor = Drawing.Color.Red
                    lblUpdateMessage.Visible = True
                Else
                    lblUpdateMessage.ForeColor = Drawing.Color.Green
                    lblUpdateMessage.Visible = True
                End If
            Else
                If Not ddlCountry.SelectedValue = "US" Then
                    If Not Session("ValidationMessage") Is Nothing Then
                        lblUpdateMessage.Text = Session("ValidationMessage")
                    Else
                        lblUpdateMessage.Text = "Since no change was detected, no information was updated."
                    End If
                    lblUpdateMessage.ForeColor = Drawing.Color.Red
                    lblUpdateMessage.Visible = True
                End If

            End If
        End If
        If e.CommandName = "updateEmail" Then

            Dim ErrCheck = checkAddressFields("")

            If ErrCheck <> "Pass" Then

                lblfeedback.Text = ErrCheck
                lblfeedback.ForeColor = Drawing.Color.Red
                lblfeedback.Visible = True
                Return

            End If

            tmpOwner.Email = txtcurrentEmail.Text.Trim()
            If (ModifyOwnerInfo(tmpOwner, "updateEmail", owner)) Then
                If OwnerData.LogChanges(owner, tmpOwner) = "ChangesDone" Then

                    insertAs400Comments("Email address has been changed from VSSA")
                    lblfeedback.Text = "Email Update Successful!"
                    lblfeedback.ForeColor = Drawing.Color.Green
                    lblfeedback.Visible = True

                    'SEND AN EMAIL HERE TO BOTH THE OLD AND NEW EMAIL ADDRESS.
                    'SO THAT THE USER KNOWS THAT IT HAS BEEN CHANGED.
                    Dim sTemplate As String
                    Dim msgBody As New StringBuilder

                    Dim changesMadeFrom As String = ConfigurationManager.AppSettings("VSSA.GetEnvironment").ToString()
                    ' only send email if this is being done in Prod
                    If changesMadeFrom.ToUpper.Equals("PROD") Then
                        Try
                            sTemplate = IO.Path.Combine(Server.MapPath(""), "EmailTemplates\VSSA2.html")
                            Dim sr As New IO.StreamReader(sTemplate)

                            Do While Not sr.Peek = -1
                                msgBody.Append(sr.ReadLine)
                            Loop
                            sr.Close()

                            msgBody.Replace("%%First  Last%%", owner.FullName)
                            msgBody.Replace("%%dd/mm/yyyy%%", Now().Date)
                            msgBody.Replace("%%old email address%%", owner.Email)
                            msgBody.Replace("%%new email address%%", txtcurrentEmail.Text)
                            msgBody.Replace("|IMAGEPATH|", System.Configuration.ConfigurationManager.AppSettings("TemplateImgPath"))

                            sendMessage(owner.Email, tmpOwner.Email, "Bluegreen Online Email Address Updated", msgBody)
                            sendMessage(tmpOwner.Email, owner.Email, "Bluegreen Online Email Address Updated", msgBody)

                            lblConfirmEmail.Visible = False

                            owner = OwnerData.searchOwners(owner.ARVACT)
                            owner.Accounts = Session("AccountInfo")
                            Session("owner") = owner
                        Catch ex As Exception

                            Me.lblError.ForeColor = Drawing.Color.Red
                            Me.lblError.Text = "Error sending email: " & ex.Message

                        End Try
                    End If



                End If
            Else

                lblfeedback.Text = "Email Update Failed!"
                If Session("HouseAccount") = True Then
                    lbResetGroupsForAccess.Visible = False
                    lblResetGroupsDivider.Visible = False
                    lbProxyAdmin.Visible = False
                    lblProxyAdminDivider.Visible = False
                    lbAddressUpload.Visible = False
                    lblAddressUploadDivider.Visible = False
                End If
                lblfeedback.ForeColor = Drawing.Color.Red
                lblfeedback.Visible = True
                txtcurrentEmail.Text = owner.Email


            End If

            makeCookie(owner)

        End If

        If e.CommandName = "cancelUpdateEmail" Then

            lblConfirmEmail.Visible = False
            txtcurrentEmail.Enabled = False
            txtcurrentEmail.BorderStyle = BorderStyle.None

            btnCancelEmailUpdate.Visible = False

        End If

        If e.CommandName = "loginAsOwner" Then
            Dim scriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            scriptManager.RegisterPostBackControl(sender)

            '            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            'scriptManager.RegisterPostBackControl(this.btnExcelExport);


            loginAsUser()

        End If

        If e.CommandName = "registerOwner" Then

            If txtcurrentEmail.Text = "" Then

                Me.lblError.Text = "An email address is required to register an owner."
                Me.lblError.BackColor = Drawing.Color.Red
                Exit Sub

            Else
                owner.Email = txtcurrentEmail.Text

            End If

            Dim SamplerType As String = IsSampler(owner.ARVACT)
            If SamplerType = "SAMPLERPLUS" Then
                Me.lblError.Text = "Please visit <a href="" http://www.samplerplus.com"">www.SamplerPlus.com</a> to register and log into your online Sampler Plus account.  <a href="" http://www.samplerplus.com"">Click here</a> to go to the Sampler Plus member web site now.<br>"
                Exit Sub
            ElseIf SamplerType = "VALUESAMPLER" Then
                Me.lblError.Text = "Because Value Sampler membership follows different guidelines than full ownership, we are not able to accommodate your online account. One of the benefits of Bluegreen ownership is full access to the many features we offer online. Until you become an owner, you may explore Bluegreen resorts by clicking <a href="" http://www.bluegreenonline.com/explore/"">here</a>.<br>"
                Exit Sub
            End If

            If Register(owner) Then
                Me.lblError.Text = "Successful Registration!"
                Me.lblError.ForeColor = Drawing.Color.Green
            Else
                Me.lblError.Text = "Registration Failed"
                Me.lblError.ForeColor = Drawing.Color.Red
            End If

        End If

        '#########################################################################################################################################
        '#########################################################################################################################################
        '#########################################################################################################################################

        If e.CommandName = "VerifyOwnerLogin" Then

            If VerifyLogin(txtcurrentEmail.Text, owner.Password) Then
                lblfeedback.Text = "Login Verification Successful!"
                lblfeedback.ForeColor = Drawing.Color.Green
            Else
                lblfeedback.Text = "Login Verification Failed!"
                lblfeedback.ForeColor = Drawing.Color.Red
            End If

            lblfeedback.Visible = True

        End If

    End Sub

    Sub loginAsUser()
        Dim strHostName As String = String.Empty
        Dim ipAddress As String = String.Empty
        strHostName = Dns.GetHostName()
        Dim ipEntry As IPHostEntry = Dns.GetHostEntry(strHostName)
        Dim addr As IPAddress() = ipEntry.AddressList
        For i As Integer = 0 To addr.Length - 1
            ipAddress = addr(i).ToString()
        Next

        insertAs400Comments("logged in BGO as owner via VSSA from: " + ipAddress + " @ " + DateAndTime.TimeOfDay.ToString("hh:mm tt"))
        Dim MyremotePost As New RemotePost
        MyremotePost.Url = System.Configuration.ConfigurationManager.AppSettings("BGO_login")
        MyremotePost.add("txtPassword", owner.Password)
        MyremotePost.add("txtEmail", owner.Email)
        MyremotePost.add("ownerARVACT", owner.ARVACT)
        MyremotePost.add("AgentLoginID", Me.lblAdminName.Text)
        MyremotePost.add("ownerACCT", owner.Accounts(0).AcctNum)
        MyremotePost.add("fromVSSA", "TRUE")

        MyremotePost.Post()

    End Sub
    Public Function IsValidAddress(inputString As String) As Boolean
        Dim r As New Regex("[a-zA-Z0-9\#\(/)\-\'\?\.\,\ ]+$$")

        If r.IsMatch(inputString) Then
            Try
                Dim indexOf As Integer = 0
                indexOf = inputString.IndexOfAny(SpecialChars)
                If indexOf = -1 Then
                    Return True
                Else
                    Err = "Special characters are not allowed"
                    Return False
                    Exit Try
                End If
            Catch ex As Exception
            End Try
            Return True
        Else
            Err = "Special characters are not allowed"
            Return False
        End If
    End Function
    Public Function checkAddressFields(ByVal _action As String) As String

        Dim fvRow As FormViewRow = fvOwner.Row
        Dim lblArvact As Label = fvRow.FindControl("lblARVACT")
        Dim ddlCountry As DropDownList = fvRow.FindControl("ddlCountry")
        Dim ddlState As DropDownList = fvRow.FindControl("ddlState")
        Dim txtAddress1 As TextBox = fvRow.FindControl("txtAddress1")
        Dim txtAddress2 As TextBox = fvRow.FindControl("txtAddress2")
        Dim txtAddress3 As TextBox = fvRow.FindControl("txtAddress3")
        Dim txtCity As TextBox = fvRow.FindControl("txtCity")
        Dim txtZIP As TextBox = fvRow.FindControl("txtZIP")
        Dim txtPhoneHome As TextBox = fvRow.FindControl("txtHomePhone")
        Dim txtPhoneAlt As TextBox = fvRow.FindControl("txtAltPhone")
        Dim vZip As RequiredFieldValidator = fvRow.FindControl("vzip")
        Dim txtConfirmEmail As TextBox = fvRow.FindControl("txtConfirmEmail")
        Dim cvEmail As CompareValidator = fvRow.FindControl("cvEmail")
        Dim lblConfirmEmail As Label = fvRow.FindControl("lbConfirmEmail")
        Dim btnUpdateEmail As ImageButton = fvRow.FindControl("btnUpdate2")
        Dim btnCancelEmailUpdate As ImageButton = fvRow.FindControl("btnCancelEmailUpdate")
        Dim pnlUserInfo As Panel = fvRow.FindControl("pnlUserInfo")
        Dim btnChangePwd As ImageButton = fvRow.FindControl("btnChangePwd")
        Dim btnVerify As ImageButton = fvRow.FindControl("iBtnVerify")
        Dim lblFeedback As Label = fvRow.FindControl("lblFeedback")
        Dim validzip As String = checkForNumbers(txtZIP.Text)

        Dim validHomePhone As String = checkForNumbers(txtPhoneHome.Text)
        Dim validCellPhone As String = checkForNumbers(txtPhoneAlt.Text)

        Try
            If _action = "UpdateContactInfo" Then
                If ddlCountry.SelectedValue = "US" Then
                    If txtAddress1.Text.Trim = "" AndAlso txtCity.Text.Trim = "" AndAlso txtZIP.Text.Trim = "" AndAlso txtPhoneHome.Text.Trim = "" Then
                        Err = String.Format("Address1 Cannot be Blank." + "</br>" + "City Cannot be Blank." + "</br>" + "ZIP code Cannot be Blank." + "</br>" + "Primary Phone number Cannot be Blank.")
                        txtAddress1.Focus()
                        Exit Try
                    ElseIf txtAddress1.Text.Trim = "" AndAlso txtCity.Text.Trim = "" AndAlso txtPhoneHome.Text.Trim = "" Then
                        Err = String.Format("Address1 Cannot be Blank." + "</br>" + "City Cannot be Blank." + "</br>" + "Primary Phone number Cannot be Blank.")
                        txtAddress1.Focus()
                        Exit Try
                    ElseIf txtAddress1.Text.Trim = "" AndAlso txtCity.Text.Trim = "" AndAlso txtZIP.Text.Trim = "" Then
                        Err = String.Format("Address1 Cannot be Blank." + "</br>" + "City Cannot be Blank." + "</br>" + "ZIP code Cannot be Blank.")
                        txtAddress1.Focus()
                        Exit Try
                    ElseIf txtAddress1.Text.Trim = "" AndAlso txtCity.Text.Trim = "" Then
                        Err = String.Format("Address1 Cannot be Blank." + "</br>" + "City Cannot be Blank.")
                        txtAddress1.Focus()
                        Exit Try
                    ElseIf txtAddress1.Text.Trim = "" AndAlso txtCity.Text.Trim = "" AndAlso txtZIP.Text.Trim = "" Then
                        Err = String.Format("Address1 Cannot be Blank." + "</br>" + "City Cannot be Blank." + "</br>" + "ZIP code Cannot be Blank.")
                        txtAddress1.Focus()
                        Exit Try
                    ElseIf txtAddress1.Text.Trim = "" AndAlso txtZIP.Text.Trim = "" AndAlso txtPhoneHome.Text.Trim = "" Then
                        Err = String.Format("Address1 Cannot be Blank." + "</br>" + "ZIP code Cannot be Blank." + "</br>" + "Primary Phone number Cannot be Blank")
                        txtAddress1.Focus()
                        Exit Try

                    ElseIf txtCity.Text.Trim = "" AndAlso txtZIP.Text.Trim = "" AndAlso txtPhoneHome.Text.Trim = "" Then
                        Err = String.Format("City Cannot be Blank." + "</br>" + "ZIP code Cannot be Blank." + "</br>" + "Primary Phone number Cannot be Blank.")
                        txtCity.Focus()
                        Exit Try
                    ElseIf txtZIP.Text.Trim = "" AndAlso txtPhoneHome.Text.Trim = "" Then
                        Err = String.Format("ZIP code Cannot be Blank." + "</br>" + "Primary Phone number Cannot be Blank.")
                        txtZIP.Focus()
                        Exit Try
                    ElseIf txtAddress1.Text.Trim = "" AndAlso txtZIP.Text.Trim = "" Then
                        Err = String.Format("Address1 Cannot be Blank." + "</br>" + "ZIP code Cannot be Blank.")
                        txtAddress1.Focus()
                        Exit Try
                    ElseIf txtAddress1.Text.Trim = "" AndAlso txtPhoneHome.Text.Trim = "" Then
                        Err = String.Format("Address1 Cannot be Blank." + "</br>" + "Primary Phone number Cannot be Blank.")
                        txtAddress1.Focus()
                        Exit Try
                    ElseIf txtCity.Text.Trim = "" AndAlso txtPhoneHome.Text.Trim = "" Then
                        Err = String.Format("City Cannot be Blank." + "</br>" + "Primary Phone number Cannot be Blank.")
                        txtCity.Focus()
                        Exit Try
                    ElseIf txtCity.Text.Trim = "" AndAlso txtZIP.Text.Trim = "" Then
                        Err = String.Format("City Cannot be Blank." + "</br>" + "ZIP code Cannot be Blank.")
                        txtCity.Focus()
                        Exit Try
                    ElseIf txtPhoneHome.Text.Trim = "" AndAlso txtPhoneAlt.Text.Trim = "" Then
                        Err = String.Format("Primary Phone number Cannot be Blank.")
                        txtPhoneHome.Focus()
                        Exit Try
                    ElseIf txtPhoneHome.Text = "" Then
                        Err = String.Format("Primary Phone number Cannot be Blank.")
                        txtPhoneHome.Focus()
                        Exit Try
                    ElseIf ((validHomePhone.Length = 0) AndAlso (validCellPhone.Length = 0)) Then
                        Err = String.Format("Invalid Primary Phone Number")
                        txtPhoneHome.Focus()
                    ElseIf ((validHomePhone.Length < 10) AndAlso (validCellPhone.Length < 10)) Then
                        If (validCellPhone.Length = 0) Then
                            Err = String.Format("Invalid Primary Phone Number")
                            txtPhoneHome.Focus()
                        Else
                            Err = String.Format("Invalid Primary Phone Number" + "</br>" + "Invalid Alternate Phone Number")
                        End If
                    ElseIf ((Not (validCellPhone.Length = 0)) AndAlso (validCellPhone.Length < 10)) Then
                        Err = "Invalid Alternate Phone Number"
                    ElseIf (validHomePhone.Length < 10) Then
                        Err = "Invalid Primary Phone Number"
                        txtPhoneHome.Focus()
                    ElseIf txtPhoneHome.Text.Length < 10 AndAlso txtPhoneAlt.Text.Length = 10 Then
                        Err = String.Format("Invalid Primary Phone Number")
                        txtPhoneHome.Focus()
                        Exit Try
                    ElseIf txtPhoneHome.Text.Length < 10 Then
                        Err = String.Format("Invalid Primary Phone Number")
                        txtPhoneHome.Focus()
                        Exit Try
                    End If
                    If txtAddress1.Text.Trim = "" Then
                        Err = "Address1 Cannot be Blank."
                        txtAddress1.Focus()
                        Exit Try
                    Else
                        IsValidAddress(txtAddress1.Text.Trim)
                        If Not Err = "Pass" Then
                            txtAddress1.Focus()
                            Exit Try
                        End If
                    End If
                Else
                    If txtAddress1.Text.Trim = "" AndAlso txtPhoneHome.Text.Trim = "" Then
                        Err = String.Format("Address1 Cannot be Blank." + "</br>" + "Primary Phone number Cannot be Blank.")
                        txtAddress1.Focus()
                        Exit Try
                    ElseIf txtAddress1.Text.Trim = "" Then
                        Err = String.Format("Address1 Cannot be Blank.")
                        txtAddress1.Focus()
                        Exit Try
                    ElseIf txtPhoneHome.Text.Trim = "" Then
                        Err = String.Format("Primary Phone number Cannot be Blank.")
                        txtPhoneHome.Focus()
                        Exit Try
                    Else
                    End If
                End If
                If Not (txtAddress2.Text.Length = 0) Then
                    IsValidAddress(txtAddress2.Text.Trim)
                    If Not Err = "Pass" Then
                        txtAddress2.Focus()
                        Exit Try
                    End If
                End If
                If (validHomePhone = "") Then
                    Err = String.Format("Invalid Primary Phone Number")
                    txtPhoneHome.Focus()
                    Exit Try
                ElseIf ((validHomePhone.Length = 0) AndAlso (validCellPhone.Length = 0)) Then
                    Err = String.Format("Primary Phone Number Cannot be Blank")
                    txtPhoneHome.Focus()
                    Exit Try
                ElseIf ((validHomePhone.Length < 10) AndAlso (validCellPhone.Length < 10)) Then
                    If (validHomePhone.Length < 10) Then
                        Err = String.Format("Invalid Primary Phone Number")
                        txtPhoneHome.Focus()
                        Exit Try
                    ElseIf (validCellPhone.Length < 10) Then
                        Err = String.Format("Invalid Alternate Phone Number")
                    Else
                        Err = String.Format("Invalid Primary Phone Number" + "</br>" + "Invalid Alternate Phone Number")
                    End If
                ElseIf ((Not (validCellPhone.Length = 0)) AndAlso (validCellPhone.Length < 10)) Then
                    Err = "Invalid Alternate Phone Number"
                ElseIf ((validHomePhone.Length < 10) AndAlso (validCellPhone.Length = 10)) Then
                    Err = String.Format("Invalid Primary Phone Number")
                    txtPhoneHome.Focus()
                    Exit Try
                ElseIf (validHomePhone.Length = 0) Then
                    Err = String.Format("Primary Phone Number Cannot be Blank")
                    txtPhoneHome.Focus()
                    Exit Try
                End If
                If ddlCountry.SelectedValue = "US" Then
                    If txtCity.Text.Trim = "" Then
                        Err = "City Cannot be Blank."
                    Else
                        IsValidAddress(txtCity.Text)
                        If Not Err = "Pass" Then
                            txtCity.Focus()
                            Exit Try
                        End If
                    End If

                    If (validzip.Length < 5) Then
                        Err = "Invalid ZIP code."
                        txtZIP.Focus()
                    Else
                        txtZIP.Text = validzip
                    End If
                Else
                    If Not (txtAddress3.Text.Length = 0) Then
                        IsValidAddress(txtAddress3.Text.Trim)
                        If Not Err = "Pass" Then
                            txtAddress3.Focus()
                            Exit Try
                        End If
                    End If
                End If
            Else
                If txtcurrentEmail.Text.Length < 5 Then
                    Err = "Email Cannot be Blank."
                End If
                Dim Expression As New System.Text.RegularExpressions.Regex("^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$")
                If Not Expression.IsMatch(txtcurrentEmail.Text) Then
                    If Session("HouseAccount") = True Then
                        lbResetGroupsForAccess.Visible = False
                        lblResetGroupsDivider.Visible = False
                        lbProxyAdmin.Visible = False
                        lblProxyAdminDivider.Visible = False
                        lbAddressUpload.Visible = False
                        lblAddressUploadDivider.Visible = False
                    End If
                    Err = "Email Address Is Not In Proper Format."
                End If
            End If
        Catch ex As Exception
        End Try
        Return Err
    End Function

    Public Sub ResetTextBoxes()
        Dim owner As BXG_Owner
        owner = Session("owner")

        Dim fvRow As FormViewRow = fvOwner.Row
        Dim ddlCountry As DropDownList = fvRow.FindControl("ddlCountry")
        Dim ddlState As DropDownList = fvRow.FindControl("ddlState")
        Dim txtAddress1 As TextBox = fvRow.FindControl("txtAddress1")
        Dim txtAddress2 As TextBox = fvRow.FindControl("txtAddress2")
        Dim txtAddress3 As TextBox = fvRow.FindControl("txtAddress3")
        Dim txtCity As TextBox = fvRow.FindControl("txtCity")
        Dim txtZIP As TextBox = fvRow.FindControl("txtZIP")
        Dim txtPhoneHome As TextBox = fvRow.FindControl("txtHomePhone")
        Dim txtPhoneAlt As TextBox = fvRow.FindControl("txtAltPhone")
        Dim lblAddress3 As Label = fvRow.FindControl("lblAddress3")
        Dim lblCity As Label = fvRow.FindControl("lblCity")
        Dim lblState As Label = fvRow.FindControl("lblState")
        Dim lblZIP As Label = fvRow.FindControl("lblZIP")
        Dim lblFeedback As Label = fvRow.FindControl("lblFeedback")

        If lblFeedback.Text.Contains("Success") Then
            lblFeedback.Visible = False
        End If

        If ddlCountry.SelectedValue = "US" Then
            txtAddress1.Visible = True
            txtAddress1.Text = ""
            txtAddress2.Visible = True
            txtAddress2.Text = ""
            lblAddress3.Visible = False
            txtAddress3.Visible = False
            txtAddress3.Text = ""
            txtCity.Visible = True
            lblCity.Visible = True
            lblState.Visible = True
            ddlState.Visible = True
            lblZIP.Visible = True
            txtZIP.Visible = True
        Else
            txtAddress1.Visible = True
            txtAddress1.Text = ""
            txtAddress2.Visible = True
            txtAddress2.Text = ""
            lblAddress3.Visible = True
            txtAddress3.Visible = True
            txtAddress3.Text = ""
            txtCity.Visible = False
            txtCity.Text = "NA"
            lblCity.Visible = False
            lblState.Visible = False
            ddlState.Visible = False
            lblZIP.Visible = False
            txtZIP.Visible = False
            txtZIP.Text = "NA"
        End If

        If ddlCountry.SelectedValue = owner.CountryCode Then
            If owner.CountryCode = "US" Then
                txtAddress1.Text = owner.Address1
                txtAddress2.Text = owner.Address2
                txtCity.Text = owner.City
                If owner.State = "AA" Then
                    ddlState.SelectedValue = "FL"
                Else
                    ddlState.SelectedValue = owner.State
                End If

                txtZIP.Text = owner.PostalCode
            Else
                txtAddress1.Text = owner.Address1
                txtAddress2.Text = owner.Address2
                txtAddress3.Text = owner.Address3
            End If

        End If

    End Sub

    Public Sub makeCookie(ByVal _owner As BXG_Owner)

        Dim AgentCookie As HttpCookie = New HttpCookie("vssa")

        AgentCookie.Values.Add("email", _owner.Email)

        'use the tmpOwner password here just in case the password has been changed
        AgentCookie.Values.Add("passwd", _owner.Password)
        AgentCookie.Values.Add("agent", Session("AdminName"))
        AgentCookie.Values.Add("arvact", _owner.ARVACT)
        AgentCookie.Values.Add("acct", owner.Accounts(0).AcctNum)

        Response.Cookies.Add(AgentCookie)

    End Sub

    Protected Sub notUSDisplay()

        Dim fvRow As FormViewRow = fvOwner.Row
        Dim ddlCountry As DropDownList = fvRow.FindControl("ddlCountry")
        Dim ddlState As DropDownList = fvRow.FindControl("ddlState")
        Dim txtAddress1 As TextBox = fvRow.FindControl("txtAddress1")
        Dim txtAddress2 As TextBox = fvRow.FindControl("txtAddress2")
        Dim txtAddress3 As TextBox = fvRow.FindControl("txtAddress3")
        Dim txtCity As TextBox = fvRow.FindControl("txtCity")
        Dim txtZIP As TextBox = fvRow.FindControl("txtZIP")
        Dim txtPhoneHome As TextBox = fvRow.FindControl("txtHomePhone")
        Dim txtPhoneAlt As TextBox = fvRow.FindControl("txtAltPhone")
        Dim lblAddress3 As Label = fvRow.FindControl("lblAddress3")
        Dim lblCity As Label = fvRow.FindControl("lblCity")
        Dim lblState As Label = fvRow.FindControl("lblState")
        Dim lblZIP As Label = fvRow.FindControl("lblZIP")

        txtAddress1.Visible = True
        txtAddress2.Visible = True
        txtAddress3.Visible = True
        lblState.Visible = False
        ddlState.Visible = False
        lblZIP.Visible = False
        txtZIP.Visible = False
        lblCity.Visible = False
        txtCity.Visible = False

    End Sub

    Protected Sub getOwnerDetails(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        btnUpdateTSW.Visible = False
        btnCancel.Visible = False
        Dim arvact As String

        If IsNothing(sender) And IsNothing(e) Then
            arvact = txtARVACT.Text
        Else
            Dim selectedRow As GridViewRow = CType(e.CommandSource, LinkButton).NamingContainer
            'Get the ARVACT from the text property of the label
            Dim lb As LinkButton = CType(selectedRow.FindControl("lbARVACT"), LinkButton)
            Dim ddlState As DropDownList = CType(selectedRow.FindControl("ddlState"), DropDownList)
            Dim ddlCountry As DropDownList = CType(selectedRow.FindControl("ddlCountry"), DropDownList)

            arvact = lb.Text
        End If

        Try
            Dim Ownersvc As New OwnerService()
            Dim OwnerSvcResp As New OwnerFetchResponse()
            OwnerSvcResp = Ownersvc.OwnerFetch(arvact)
            Session("BoomiOwner") = OwnerSvcResp
            'populate the owner object
            owner = OwnerData.searchOwners(arvact)
            If Not (owner.Email = Nothing) Then
                Dim svc As New SitecoreProfileSvc.ProfileSoapClient
                Dim req As New SitecoreProfileSvc.IsUserRegisterRequest
                Dim reqBody As New SitecoreProfileSvc.IsUserRegisterRequestBody

                reqBody.ARVACT = owner.ARVACT
                reqBody.email = owner.Email
                req.Body = reqBody

                Dim resp As SitecoreProfileSvc.IsUserRegisterResponse = svc.SitecoreProfileSvc_ProfileSoap_IsUserRegister(req)
                If resp.Body.IsUserRegisterResult.Equals(True) Then
                    Session("Registered") = "Yes"

                End If
            Else
                Session("Registered") = "No"
            End If




            Session("arvact") = owner.ARVACT

            fetchOwnerAccountsHolders(arvact)

            owner.Accounts = Session("AccountInfo")
            owner.AccountHolders = Session("accountHolder")

            If String.IsNullOrEmpty(owner.State) Then
                owner.State = getStateBySubdivision(owner.Subdivision)
            ElseIf String.IsNullOrEmpty(owner.Subdivision) Then
                owner.Subdivision = getSubdivisionByState(owner.State)
            End If

            Session("owner") = owner

        Catch ex As Exception

            Response.Write("Error filling owner object: <br /> " & ex.Message)

        End Try

        'Add the owner object to an arrayList so that it can be bound to a form view
        'the form view is the larger area that displays the owner's address and other
        'contact information
        Dim alResult As New ArrayList
        alResult.Add(owner)

        fvOwner.DataSource = alResult
        fvOwner.DataBind()
        fvOwner.Visible = True 'Not fvOwner.Visible
        ownerMoreDetails.Visible = True

        populateStatesDDL()
        populateCountriesDDL()

        Me.gvContractInfo.DataSource = CType(Session("owner"), BXG_Owner).Accounts
        gvContractInfo.DataBind()

        If gvContractInfo.Rows.Count = 0 Then

            Dim tblNoData As DataTable
            tblNoData = New DataTable("tblEmpty")
            Dim Row1 As DataRow

            Dim Name As DataColumn = New DataColumn("id")
            Name.DataType = System.Type.GetType("System.String")
            tblNoData.Columns.Add(Name)

            Dim Name2 As DataColumn = New DataColumn("acct")
            Name2.DataType = System.Type.GetType("System.String")
            tblNoData.Columns.Add(Name2)

            Dim Name3 As DataColumn = New DataColumn("proj")
            Name3.DataType = System.Type.GetType("System.String")
            tblNoData.Columns.Add(Name3)

            Dim Name4 As DataColumn = New DataColumn("projNM")
            Name3.DataType = System.Type.GetType("System.String")
            tblNoData.Columns.Add(Name4)

            Row1 = tblNoData.NewRow()
            Row1.Item("id") = "NA"
            Row1.Item("acct") = "NA"
            Row1.Item("proj") = "NA"
            Row1.Item("projNM") = "NA"

            tblNoData.Rows.Add(Row1)
            gvContractInfo.DataSource = tblNoData

        End If

        Me.gvContractInfo.DataBind()
        gvContractInfo.Visible = Not gvContractInfo.Visible

        'Account Holders
        gvAccountHolders.DataSource = CType(Session("owner"), BXG_Owner).AccountHolders
        gvAccountHolders.DataBind()

        If gvAccountHolders.Rows.Count = 0 Then

            Dim tblNoData As DataTable
            tblNoData = New DataTable("tblEmpty")
            Dim Row1 As DataRow
            Dim Name As DataColumn = New DataColumn("OwnerName")
            Name.DataType = System.Type.GetType("System.String")
            tblNoData.Columns.Add(Name)
            Row1 = tblNoData.NewRow()
            Row1.Item("OwnerName") = "No Data Found"
            tblNoData.Rows.Add(Row1)
            gvAccountHolders.DataSource = tblNoData

        End If

        Dim adGroupNames As String() = TSWPerson.TSWEditUsers.Split(";")
        Dim ADControllers As String() = ConfigurationManager.AppSettings("VSSA.GetDomainControllerForAD").ToString().Split(";")

        Dim ad As New ActiveDirectory
        For Each strDomain As String In ADControllers
            Dim countOfUsersForDomain As Integer = 0
            For Each strGroup As String In adGroupNames
                Dim lstUsers As List(Of String) = ad.ListADGroupMembers(strDomain, strGroup)
                lstAllowedToEditTSW.AddRange(lstUsers)
                countOfUsersForDomain += lstUsers.Count
            Next
            If (countOfUsersForDomain > 0) Then
                Exit For
            End If
        Next
        If (lstAllowedToEditTSW.Contains(Environment.UserName.ToString())) Then
            btnEdit.Visible = True
            lbltswPersonID.Visible = True
            tbEditable.Visible = False
            lblTSWMessage.Visible = False
            tbSrchTSW.Visible = True
            lblINFO5.Visible = True
        Else
            btnEdit.Visible = False
            tbSrchTSW.Visible = False
            lblINFO5.Visible = False
        End If
        gvAccountHolders.Visible = Not gvAccountHolders.Visible

        Dim alARVACT As New ArrayList
        alARVACT.Add(owner.ARVACT)

        gvARVACT.DataSource = alARVACT
        gvARVACT.DataBind()
        gvARVACT.Visible = Not gvARVACT.Visible

        'the following uses gridviews for formatting purposes only.  Attempts at using labels cause strange layout issues.
        Dim alFirstLogin As New ArrayList
        Dim alLastLogin As New ArrayList
        Dim alLoginCount As New ArrayList
        Dim IsHouseAccount As Boolean = Nothing
        With owner
            For x As Integer = 0 To owner.Accounts.Count - 1
                If ((owner.Accounts(x).AcctNum = "88888888") Or (owner.Accounts(x).AcctNum = "88888881") Or (owner.Accounts(x).AcctNum = "88888882") Or (owner.Accounts(x).AcctNum = "88888883") Or (owner.Accounts(x).AcctNum = "88888884") Or (owner.Accounts(x).AcctNum = "88888885") Or (owner.Accounts(x).AcctNum = "88888886") Or (owner.Accounts(x).AcctNum = "77777777") Or (owner.Accounts(x).AcctNum = "99999999")) Then
                    IsHouseAccount = True
                Else
                    IsHouseAccount = False
                End If
            Next
            If IsHouseAccount = True Then
                btnLoginasOwner.Visible = False
                btnProxyVote.Visible = False
                btnresetUserPassword.Visible = False
                lblChoicePrivileges.Visible = False
                lblRentAdditionalPoints.Visible = False
                lblOwnerReservation.Visible = False
                lbResetGroupsForAccess.Visible = False
                lblResetGroupsDivider.Visible = False
                lbProxyAdmin.Visible = False
                lblProxyAdminDivider.Visible = False
                lbAddressUpload.Visible = False
                lblAddressUploadDivider.Visible = False
                Session("HouseAccount") = True
                .FirstLogin = "NA"
                .LastLogin = "NA"
                .LoginCount = "NA"
                alFirstLogin.Add(.FirstLogin)
                alLastLogin.Add(.LastLogin)
                alLoginCount.Add(.LoginCount)

                gvFirstLogin.DataSource = alFirstLogin
                gvFirstLogin.DataBind()
                gvFirstLogin.Visible = Not gvFirstLogin.Visible

                gvLastLogin.DataSource = alLastLogin
                gvLastLogin.DataBind()
                gvLastLogin.Visible = Not gvLastLogin.Visible

                gvLoginCount.DataSource = alLoginCount
                gvLoginCount.DataBind()
                gvLoginCount.Visible = Not gvLoginCount.Visible
                If Not String.IsNullOrEmpty(Session("Registered")) Then
                    lblRegistered.Text = Session("Registered")
                    lblRegistered.Visible = True
                End If
                Dim iDate As String = Now().ToShortDateString()
                Dim oDate As DateTime = Convert.ToDateTime(iDate)
                If Not Session("BoomiOwner") Is Nothing Then
                    bxgowner = Session("BoomiOwner")
                End If
                If Not bxgowner.Account.Memberships.TravelerPlusMembership Is Nothing Then
                    If Not bxgowner.Account.Memberships.TravelerPlusMembership.TravelerPlusEligible = False Then
                        If bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate = "" Then
                            Me.lblTPExp.Text = "NA"
                            Me.lblTPExp.ForeColor = Drawing.Color.Black
                            lblRentAdditionalPoints.Visible = False
                        ElseIf Convert.ToDateTime(bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate.ToString()) >= oDate Then
                            Me.lblTPExp.ForeColor = Drawing.Color.Green
                            Me.lblTPExp.Text = bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate
                            lblRentAdditionalPoints.Visible = True
                        ElseIf Convert.ToDateTime(bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate.ToString()) < oDate Then
                            Me.lblTPExp.ForeColor = Drawing.Color.Red
                            Me.lblTPExp.Text = bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate
                            lblRentAdditionalPoints.Visible = False
                        End If
                    Else
                        Me.lblTPExp.Text = "NA"
                        Me.lblTPExp.ForeColor = Drawing.Color.Black
                        lblRentAdditionalPoints.Visible = False
                    End If
                Else
                    Me.lblTPExp.Text = "NA"
                    Me.lblTPExp.ForeColor = Drawing.Color.Black
                    lblRentAdditionalPoints.Visible = False
                End If

                If lblTPExp.Text IsNot Nothing Then
                    Label1.Visible = True
                    lblTPExp.Visible = True
                    Reg.Visible = True
                End If


                Me.lblCollCode.Text = owner.AccountStatus
                Me.lblCollCode.Visible = True

                Me.lbltswPersonID.Text = owner.TSWPersonID
                Me.lblTwsID.Visible = True
                Session("NoTSWPersonID") = lbltswPersonID.Text.ToString()

                If Not owner.TSWPersonID = 0 Then
                    lbltswPersonID.ForeColor = Drawing.Color.Green
                Else
                    lbltswPersonID.Text = "N/A"
                    lbltswPersonID.ForeColor = Drawing.Color.Red
                End If

                If Not lblCollCode.Text.Contains(" - User in good standing ") Then
                    lblCollCode.ForeColor = Drawing.Color.Red
                Else
                    lblCollCode.ForeColor = Drawing.Color.Green
                End If

                If lblCollCode.Text IsNot Nothing Then
                    label2.Visible = True
                End If
            Else
                Session("HouseAccount") = False
                If .FirstLogin = "" Then
                    .FirstLogin = "NA"
                    .LastLogin = "NA"
                    .LoginCount = "NA"
                End If
                alFirstLogin.Add(.FirstLogin)
                alLastLogin.Add(.LastLogin)
                alLoginCount.Add(.LoginCount)

                gvFirstLogin.DataSource = alFirstLogin
                gvFirstLogin.DataBind()
                gvFirstLogin.Visible = Not gvFirstLogin.Visible

                gvLastLogin.DataSource = alLastLogin
                gvLastLogin.DataBind()
                gvLastLogin.Visible = Not gvLastLogin.Visible

                gvLoginCount.DataSource = alLoginCount
                gvLoginCount.DataBind()
                gvLoginCount.Visible = Not gvLoginCount.Visible
                btnLoginasOwner.Visible = True
                btnresetUserPassword.Visible = True
                lblChoicePrivileges.Visible = True
                lblRentAdditionalPoints.Visible = True
                lblOwnerReservation.Visible = True
                If Not String.IsNullOrEmpty(Session("Registered")) Then
                    lblRegistered.Text = Session("Registered")
                    lblRegistered.Visible = True
                End If


                Dim iDate As String = Now().ToShortDateString()
                Dim oDate As DateTime = Convert.ToDateTime(iDate)

                If Not Session("BoomiOwner") Is Nothing Then
                    bxgowner = Session("BoomiOwner")
                End If
                If Not bxgowner.Account.Memberships.TravelerPlusMembership Is Nothing Then
                    If Not bxgowner.Account.Memberships.TravelerPlusMembership.TravelerPlusEligible = False Then
                        If bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate = "" Then
                            Me.lblTPExp.Text = "NA"
                            Me.lblTPExp.ForeColor = Drawing.Color.Black
                            lblRentAdditionalPoints.Visible = False
                        ElseIf Convert.ToDateTime(bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate.ToString()) >= oDate Then
                            Me.lblTPExp.ForeColor = Drawing.Color.Green
                            Me.lblTPExp.Text = bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate
                            lblRentAdditionalPoints.Visible = True
                        ElseIf Convert.ToDateTime(bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate.ToString()) < oDate Then
                            Me.lblTPExp.ForeColor = Drawing.Color.Red
                            Me.lblTPExp.Text = bxgowner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate
                            lblRentAdditionalPoints.Visible = False
                        End If
                    Else
                        Me.lblTPExp.Text = "NA"
                        Me.lblTPExp.ForeColor = Drawing.Color.Black
                        lblRentAdditionalPoints.Visible = False
                    End If
                Else
                    Me.lblTPExp.Text = "NA"
                    Me.lblTPExp.ForeColor = Drawing.Color.Black
                    lblRentAdditionalPoints.Visible = False
                End If

                If lblTPExp.Text IsNot Nothing Then
                    Label1.Visible = True
                    lblTPExp.Visible = True
                    Reg.Visible = True
                End If

                Me.lbltswPersonID.Text = owner.TSWPersonID
                Me.lblTwsID.Visible = True
                Session("CurrentTSWPersonID") = lbltswPersonID.Text.ToString()
                If Not owner.TSWPersonID = 0 Then
                    lbltswPersonID.ForeColor = Drawing.Color.Green
                Else
                    lbltswPersonID.Text = "N/A"
                    lbltswPersonID.ForeColor = Drawing.Color.Red
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
                If (lstAllowedProxyVotingUsers.Contains(Environment.UserName.ToString()) AndAlso isProxyVotingActive(0)) Then
                    If Not (Session("BoomiOwner") Is Nothing) Then
                        bxgowner = Session("BoomiOwner")
                    End If
                    Dim oAcc As fetchresp.AccountInfo = Nothing
                    If Not IsNothing(bxgowner) Then
                        If Not (bxgowner.Account.Memberships.VacationClubMembership.Accounts Is Nothing) Then
                            oAcc = (From acc In bxgowner.Account.Memberships.VacationClubMembership.Accounts.AccountInfo Where acc.Legacy.ProjectNumber = 50 Select acc).FirstOrDefault()
                        End If
                    End If
                    If Not IsNothing(oAcc) Then
                        If Not (bxgowner.Account.Memberships.VacationClubMembership.Sampler Is Nothing) Then
                            If bxgowner.Account.Memberships.VacationClubMembership.Sampler.IsSampler = False Then
                                btnProxyVote.Visible = True
                                Session("DisplayProxyVoting") = "True"
                            End If
                        End If

                    Else
                        ' not a vacation club owner
                        btnProxyVote.Visible = False

                    End If
                End If
            End If
        End With

        makeCookie(owner)

        'Hide the gridview that contains the initial search results
        Me.gvSearchResults.Visible = Not Me.gvSearchResults.Visible
        Me.div_ownerInfo.Visible = True
        Me.gvARVACT.Visible = True
        Me.moreInfo.Visible = True

        Session("Owner") = owner
        Session("OwnerEmail") = owner.Email
        Session("password") = owner.Password
        If Not (Session("ownerEmail") Is Nothing) Then
            emailOnPageLoad = Session("ownerEmail")
        End If
        txtcurrentEmail.Text = owner.Email
    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Dim owner As BXG_Owner
        owner = Session("owner")

        Dim fvRow As FormViewRow = fvOwner.Row
        Dim ddlCountry As DropDownList = fvRow.FindControl("ddlCountry")
        Dim ddlState As DropDownList = fvRow.FindControl("ddlState")
        Dim txtAddress1 As TextBox = fvRow.FindControl("txtAddress1")
        Dim txtAddress2 As TextBox = fvRow.FindControl("txtAddress2")
        Dim txtAddress3 As TextBox = fvRow.FindControl("txtAddress3")
        Dim txtCity As TextBox = fvRow.FindControl("txtCity")
        Dim txtZIP As TextBox = fvRow.FindControl("txtZIP")
        Dim txtPhoneHome As TextBox = fvRow.FindControl("txtHomePhone")
        Dim txtPhoneAlt As TextBox = fvRow.FindControl("txtAltPhone")
        Dim lblAddress3 As Label = fvRow.FindControl("lblAddress3")
        Dim lblCity As Label = fvRow.FindControl("lblCity")
        Dim lblState As Label = fvRow.FindControl("lblState")
        Dim lblZIP As Label = fvRow.FindControl("lblZIP")
        Dim lblFeedback As Label = fvRow.FindControl("lblFeedback")

        If ddlCountry.SelectedValue = "US" Then
            txtAddress1.Visible = True
            txtAddress1.Text = ""
            txtAddress2.Visible = True
            txtAddress2.Text = ""
            lblAddress3.Visible = False
            txtAddress3.Visible = False
            txtAddress3.Text = ""
            txtCity.Visible = True
            lblCity.Visible = True
            lblState.Visible = True
            ddlState.Visible = True
            lblZIP.Visible = True
            txtZIP.Visible = True
        Else
            txtAddress1.Visible = True
            txtAddress1.Text = ""
            txtAddress2.Visible = True
            txtAddress2.Text = ""
            lblAddress3.Visible = True
            txtAddress3.Visible = True
            txtAddress3.Text = ""
            txtCity.Visible = False
            txtCity.Text = "NA"
            lblCity.Visible = False
            lblState.Visible = False
            ddlState.Visible = False
            lblZIP.Visible = False
            txtZIP.Visible = False
            txtZIP.Text = "NA"
        End If

        If ddlCountry.SelectedValue = owner.CountryCode Then
            If owner.CountryCode = "US" Then
                txtAddress1.Text = owner.Address1
                txtAddress2.Text = owner.Address2
                txtCity.Text = owner.City
                ddlState.SelectedValue = owner.State
                txtZIP.Text = owner.PostalCode
            Else
                txtAddress1.Text = owner.Address1
                txtAddress2.Text = owner.Address2
                txtAddress3.Text = owner.Address3
            End If
        End If


    End Sub

    Private Sub populateStatesDDL()

        Dim fvRow As FormViewRow = fvOwner.Row
        Dim ddlStates As DropDownList = fvRow.FindControl("ddlState")
        ddlStates.ClearSelection()
        ddlStates.DataSource = CommonData.getStates()
        ddlStates.DataTextField = "StateDescription"
        ddlStates.DataValueField = "Stateid"
        ddlStates.DataBind()

        With owner
            'Response.Write("state: " & .State.ToString)
            If .State IsNot "" Then
                ddlStates.SelectedValue = .State
            Else
                ddlStates.Text = "NA"
            End If

        End With

    End Sub

    Private Sub populateCountriesDDL()

        Dim fvRow As FormViewRow = fvOwner.Row
        Dim ddlCountry As DropDownList = fvRow.FindControl("ddlCountry")
        Dim ddlState As DropDownList = fvRow.FindControl("ddlState")
        Dim lblState As Label = fvRow.FindControl("lblState")
        Dim lblAddress3 As Label = fvRow.FindControl("lblAddress3")
        Dim txtAddress3 As TextBox = fvRow.FindControl("txtAddress3")
        Dim txtCity As TextBox = fvRow.FindControl("txtCity")
        Dim lblCity As Label = fvRow.FindControl("lblCity")
        Dim lblZIP As Label = fvRow.FindControl("lblZIP")
        Dim txtZip As TextBox = fvRow.FindControl("txtZIP")

        ddlCountry.ClearSelection()

        If ddlCountry.DataSource Is Nothing Then
            ddlCountry.DataSource = CommonData.getCountries()

            ddlCountry.DataTextField = "CountryFullName"
            ddlCountry.DataValueField = "CountryCode"
            ddlCountry.DataBind()
        End If

        With owner
            ddlCountry.SelectedValue = .CountryCode
            If .CountryCode <> "US" Then
                lblAddress3.Visible = Not lblAddress3.Visible
                txtAddress3.Visible = Not txtAddress3.Visible
                txtCity.Visible = False
                ddlState.Visible = False
                lblZIP.Visible = False
                txtZip.Visible = False
                lblState.Visible = False
                lblCity.Visible = False
            End If
        End With

    End Sub

    Function Verify(ByVal _Country As String, ByVal _Military As String) As Boolean

        Dim verified As Boolean = True

        If _Country <> "US" Then
            verified = False
        End If

        If _Military = "AA" Then
            verified = False
        End If

        If _Military = "AE" Then
            verified = False
        End If

        Return verified

    End Function
    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Environment.UserName.Length > 8 Then
            Me.lblAdminName.Text = Environment.UserName.Substring(1, 8)
            Session("adminName") = Environment.UserName.Substring(1, 8)
        Else
            Me.lblAdminName.Text = Environment.UserName
            Session("adminName") = Environment.UserName
        End If
        If lstAllowedAddressUploadUsers Is Nothing OrElse lstAllowedAddressUploadUsers.Count.Equals(0) Then
            lstAllowedAddressUploadUsers = New List(Of String)
            Dim adGroupNames As String() = ConfigurationManager.AppSettings("VSSA.GetADForAddressUpload").ToString().Split(";")
            Dim ADControllers As String() = ConfigurationManager.AppSettings("VSSA.GetDomainControllerForAD").ToString().Split(";")

            Dim ad As New ActiveDirectory
            For Each strDomain As String In ADControllers
                Dim countOfUsersForDomain As Integer = 0
                For Each strGroup As String In adGroupNames
                    Dim lstUsers As List(Of String) = ad.ListADGroupMembers(strDomain, strGroup)
                    lstAllowedAddressUploadUsers.AddRange(lstUsers)
                    countOfUsersForDomain += lstUsers.Count
                Next
                If (countOfUsersForDomain > 0) Then
                    Exit For
                End If
            Next

        End If

        If lstAllowedProxyAdminUsers Is Nothing OrElse lstAllowedProxyAdminUsers.Count.Equals(0) Then
            lstAllowedProxyAdminUsers = New List(Of String)
            Dim adGroupNames As String() = ConfigurationManager.AppSettings("VSSA.GetADForProxyAdmin").ToString().Split(";")
            Dim ADControllers As String() = ConfigurationManager.AppSettings("VSSA.GetDomainControllerForAD").ToString().Split(";")

            Dim ad As New ActiveDirectory
            For Each strDomain As String In ADControllers
                Dim countOfUsersForDomain As Integer = 0
                For Each strGroup As String In adGroupNames
                    Dim lstUsers As List(Of String) = ad.ListADGroupMembers(strDomain, strGroup)
                    lstAllowedProxyAdminUsers.AddRange(lstUsers)
                    countOfUsersForDomain += lstUsers.Count
                Next
                If (countOfUsersForDomain > 0) Then
                    Exit For
                End If
            Next

        End If

        If lstAllowedProxyVotingUsers Is Nothing OrElse lstAllowedProxyVotingUsers.Count.Equals(0) Then
            lstAllowedProxyVotingUsers = New List(Of String)
            Dim adGroupNames As String() = ConfigurationManager.AppSettings("VSSA.GetADForProxyVoting").ToString().Split(";")
            Dim ADControllers As String() = ConfigurationManager.AppSettings("VSSA.GetDomainControllerForAD").ToString().Split(";")

            Dim ad As New ActiveDirectory
            For Each strDomain As String In ADControllers
                Dim countOfUsersForDomain As Integer = 0
                For Each strGroup As String In adGroupNames
                    Dim lstUsers As List(Of String) = ad.ListADGroupMembers(strDomain, strGroup)
                    lstAllowedProxyVotingUsers.AddRange(lstUsers)
                    countOfUsersForDomain += lstUsers.Count
                Next
                If (countOfUsersForDomain > 0) Then
                    Exit For
                End If
            Next

        End If

        If lstAllowedGroupReset Is Nothing OrElse lstAllowedGroupReset.Count.Equals(0) Then
            lstAllowedGroupReset = New List(Of String)
            Dim adGroupNames As String() = ConfigurationManager.AppSettings("VSSA.GetADForProxyVoting").ToString().Split(";")
            Dim ADControllers As String() = ConfigurationManager.AppSettings("VSSA.GetDomainControllerForAD").ToString().Split(";")

            Dim ad As New ActiveDirectory
            For Each strDomain As String In ADControllers
                Dim countOfUsersForDomain As Integer = 0
                For Each strGroup As String In adGroupNames
                    Dim lstUsers As List(Of String) = ad.ListADGroupMembers(strDomain, strGroup)
                    lstAllowedGroupReset.AddRange(lstUsers)
                    countOfUsersForDomain += lstUsers.Count
                Next
                If (countOfUsersForDomain > 0) Then
                    Exit For
                End If
            Next

        End If

        If isProxyVotingActive Is Nothing OrElse isProxyVotingActive.Count.Equals(0) Then
            isProxyVotingActive = New List(Of Boolean)
            Dim isVotingActive As Boolean = False
            Dim lstDates As New List(Of ProxyDate)
            Try
                Dim ProxySvc As New ProxyVotingService()
                Dim VotingDates As New GetActiveProxyVotingDatesResponse()
                Dim IsActive As Integer = 1
                VotingDates = ProxySvc.GetActiveProxyVotingDates(IsActive)
                If Not (VotingDates.Records Is Nothing) Then
                    For i As Integer = 0 To VotingDates.Records.Count - 1
                        Dim PD As New ProxyDate
                        PD.startDate = VotingDates.Records(i).StartDate
                        PD.endDate = VotingDates.Records(i).EndDate
                        PD.votingYear = VotingDates.Records(i).VotingYear
                        If DateTime.Now >= PD.startDate AndAlso DateTime.Now <= PD.endDate Then
                            lstDates.Add(PD)
                        End If
                    Next
                End If
                If lstDates IsNot Nothing AndAlso lstDates.Count > 0 AndAlso DateTime.Now >= lstDates(0).startDate AndAlso DateTime.Now <= lstDates(0).endDate Then
                    isVotingActive = True
                Else
                    isVotingActive = False
                End If
                isProxyVotingActive.Add(isVotingActive)
            Catch ex As Exception

            End Try
        End If

        If (lstAllowedProxyAdminUsers.Contains(Environment.UserName.ToString())) Then
            lbProxyAdmin.Visible = True
            lblProxyAdminDivider.Visible = True
            Session("DisplayProxyAdmin") = "True"
        End If

        If (lstAllowedGroupReset.Contains(Environment.UserName.ToString())) Then
            lbResetGroupsForAccess.Visible = True
            lblResetGroupsDivider.Visible = True
            Session("AllowGroupsReset") = "True"
        End If

        If (lstAllowedAddressUploadUsers.Contains(Environment.UserName.ToString())) Then
            lbAddressUpload.Visible = True
            lblAddressUploadDivider.Visible = True
            Session("DisplayAddressUpload") = "True"
        End If
        If lstAllowedToEditTSW Is Nothing OrElse lstAllowedToEditTSW.Count.Equals(0) Then
            Dim adGroupNames As String() = TSWPerson.TSWEditUsers.Split(";")
            Dim ADControllers As String() = ConfigurationManager.AppSettings("VSSA.GetDomainControllerForAD").ToString().Split(";")

            Dim ad As New ActiveDirectory
            For Each strDomain As String In ADControllers
                Dim countOfUsersForDomain As Integer = 0
                For Each strGroup As String In adGroupNames
                    Dim lstUsers As List(Of String) = ad.ListADGroupMembers(strDomain, strGroup)
                    lstAllowedToEditTSW.AddRange(lstUsers)
                    countOfUsersForDomain += lstUsers.Count
                Next
                If (countOfUsersForDomain > 0) Then
                    Exit For
                End If
            Next
            If (lstAllowedToEditTSW.Contains(Environment.UserName.ToString())) Then
                hideTSW.Visible = True
                hideTSWhint.Visible = True
            Else

                hideTSW.Visible = False
                hideTSWhint.Visible = False
            End If
        End If
        If Not IsPostBack Then
            If (Session("SearchData") Is Nothing) Then
                '--------------------------------------------------------------------------------------------------------
                'Before any process process fire-up load the black list of arvact(s) not allowed to log In.
                '--------------------------------------------------------------------------------------------------------
                If (OwnerBlackList.LoadOwnerBlackList()) Then

                    If Not IsNothing(Request.QueryString("arvact")) Then
                        Me.txtARVACT.Text = Request.QueryString("arvact")

                        externalCall = True

                        RemoveQueryString()

                        'Retrieve owner info automatically since the QueryString argument exist!
                        Me.pnlHelpText.Visible = False
                        SortDirection = "ASC"
                        SortColumn = "ARVACT"
                        BindGridView()
                        If Not Session("SearchParam") Is Nothing Then
                            Session("SearchParam") = Nothing
                        End If
                        If Not Session("SearchData") Is Nothing Then
                            Session("SearchData") = Nothing
                        End If
                        If raisedRedFlag = False Then
                            getOwnerDetails(Nothing, Nothing)
                        End If


                    End If
                Else
                    pnlGeneralMessage.Visible = True
                    Me.lblGeneralMessage.ForeColor = Drawing.Color.Red
                    Me.lblGeneralMessage.Text = "Error loading owner's blacklisted. Please contact the IT Dept for further information."
                    Me.pnlSearch.Visible = False
                    Me.pnlHelpText.Visible = False
                    Me.txtARVACT.Focus()

                End If
            Else
                If Not (Session("SearchData") Is Nothing) Then
                    Me.pnlHelpText.Visible = False
                    Dim list = TryCast(Session("SearchParam"), List(Of String))
                    Me.txtARVACT.Text = list(0)
                    Me.txtEmail.Text = list(1)
                    Me.txtFName.Text = list(2)
                    Me.txtLName.Text = list(3)
                    Me.txtPhone.Text = list(4)
                    Me.tbSrchTSW.Text = list(5)
                    SortDirection = "ASC"
                    SortColumn = "ARVACT"
                    BindGridView()
                    gvSearchResults.DataSource = DirectCast(Session("SearchData"), DataTable)
                    gvSearchResults.DataBind()
                End If
            End If
        End If
    End Sub

    Function GeneratePassword(ByVal length As Integer,
                ByVal numberOfNonAlphanumericCharacters As Integer) As String
        'Make sure length and numberOfNonAlphanumericCharacters are valid....
        '... checks omitted for brevity ... see live demo for full code ...

        'Do While True
        Dim i As Integer
        Dim nonANcount As Integer = 0
        Dim buffer1 As Byte() = New Byte(length - 1) {}

        'chPassword contains the password's characters as it's built up
        Dim chPassword As Char() = New Char(length - 1) {}

        'chPunctionations contains the list of legal non-alphanumeric characters
        Dim chPunctuations As Char() = "AABCUCDEEFGHIJKLMNOPQRSTQ".ToCharArray()
        'Dim chPunctuations As Char() = "!?".ToCharArray()

        'Get a cryptographically strong series of bytes
        Dim rng As New System.Security.Cryptography.RNGCryptoServiceProvider
        rng.GetBytes(buffer1)

        For i = 0 To length - 1
            'Convert each byte into its representative character
            Dim rndChr As Integer = (buffer1(i) Mod 87)
            If (rndChr < 10) Then
                chPassword(i) = Convert.ToChar(Convert.ToUInt16(48 + rndChr))
            Else
                If (rndChr < 36) Then
                    chPassword(i) = Convert.ToChar(Convert.ToUInt16((65 + rndChr) - 10))
                Else
                    If (rndChr < 62) Then
                        chPassword(i) = Convert.ToChar(Convert.ToUInt16((97 + rndChr) - 36))
                    Else
                        chPassword(i) = chPunctuations(rndChr - 62)
                        nonANcount += 1
                    End If
                End If
            End If
        Next

        If nonANcount < numberOfNonAlphanumericCharacters Then
            Dim rndNumber As New Random
            For i = 0 To (numberOfNonAlphanumericCharacters - nonANcount) - 1
                Dim passwordPos As Integer
                Do
                    passwordPos = rndNumber.Next(0, length)
                Loop While Not Char.IsLetterOrDigit(chPassword(passwordPos))
                chPassword(passwordPos) =
                        chPunctuations(rndNumber.Next(0, chPunctuations.Length))
            Next
        End If

        Return New String(chPassword)
        'Loop

    End Function

    Protected Sub lbNewSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNewSearch.Click

        Me.txtARVACT.Text = ""
        Me.txtEmail.Text = ""
        Me.txtFName.Text = ""
        Me.txtLName.Text = ""
        Me.txtPhone.Text = ""
        Me.tbSrchTSW.Text = ""
        Me.gvSearchResults.Visible = False
        Me.divSearchResults.Visible = True
        Me.div_ownerInfo.Visible = False
        Me.lblError.Text = String.Empty
        Me.pnlHelpText.Visible = True
        Me.pnlSearch.Visible = True
        Session.Abandon()
        Session.RemoveAll()
        Me.moreInfo.Visible = False
        Me.fvOwner.Visible = False
        Me.ownerMoreDetails.Visible = False

        If Not IsNothing(Request.QueryString("arvact")) Then

            RemoveQueryString()
        End If

    End Sub

    Sub RemoveQueryString()
        ' reflect to readonly property
        Dim isreadonly As PropertyInfo = GetType(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance Or BindingFlags.NonPublic)
        ' make collection editable
        isreadonly.SetValue(Me.Request.QueryString, False, Nothing)

        ' remove
        Request.QueryString.Remove("arvact")
    End Sub
    Protected Sub SendEmailChangeConfirmation()
        If Not Session("BoomiOwner") Is Nothing Then
            bxgowner = Session("BoomiOwner")
        End If

        Dim requestObject As BoomiServiceResetEmail.SendDataToResponsysReq = New BoomiServiceResetEmail.SendDataToResponsysReq
        '' Static Fields
        requestObject.FolderName = "API_BGV_OWN_BGO_Email_Reset"
        requestObject.SupplymentTableName = "API_BGV_OWN_BGO_Email_Reset_MasterList"
        requestObject.EventID = "4602"

        ''Dynamic Fields
        owner = Session("owner")

        Dim ownerFirstName As New BoomiServiceResetEmail.SendDataToResponsysReqKeyValue()
        ownerFirstName.Key = "FIRST_NAME"
        ownerFirstName.Value = owner.NameFirst

        Dim ownerLastName As New BoomiServiceResetEmail.SendDataToResponsysReqKeyValue()
        ownerLastName.Key = "LAST_NAME"
        ownerLastName.Value = owner.NameLast

        Dim ownerArvact As New BoomiServiceResetEmail.SendDataToResponsysReqKeyValue()
        ownerArvact.Key = "ARVACT"
        ownerArvact.Value = owner.ARVACT

        Dim dateTime As New BoomiServiceResetEmail.SendDataToResponsysReqKeyValue()
        dateTime.Key = "DATE_TIME"
        dateTime.Value = Date.Now

        Dim oldEmailAddress As New BoomiServiceResetEmail.SendDataToResponsysReqKeyValue()
        oldEmailAddress.Key = "OLD_EMAIL_ADDRESS"
        oldEmailAddress.Value = emailOnPageLoad

        Dim newEmailAddress As New BoomiServiceResetEmail.SendDataToResponsysReqKeyValue()
        newEmailAddress.Key = "NEW_EMAIL_ADDRESS"
        newEmailAddress.Value = Trim(txtcurrentEmail.Text)

        Dim premierStatus As New BoomiServiceResetEmail.SendDataToResponsysReqKeyValue()
        premierStatus.Key = "PREMIER_STATUS"
        premierStatus.Value = Trim(bxgowner.Account.Memberships.VacationClubMembership.Membership.MembershipLevelDescription)


        Dim emailAddress_ As New BoomiServiceResetEmail.SendDataToResponsysReqKeyValue()
        emailAddress_.Key = "EMAIL_ADDRESS_"
        emailAddress_.Value = Trim(txtcurrentEmail.Text)
        requestObject.KeyValues = New BoomiServiceResetEmail.SendDataToResponsysReqKeyValue() {ownerFirstName, ownerLastName, ownerArvact, dateTime, oldEmailAddress, newEmailAddress, emailAddress_, premierStatus}

        requestObject.PrimaryKey = New String() {"EMAIL_ADDRESS_"}
        requestObject.PrimaryKey = New String() {"ARVACT"}
        requestObject.PrimaryKey = New String() {"OLD_EMAIL_ADDRESS"}

        requestObject.EmailAddress = New String() {Trim(txtcurrentEmail.Text)}

        '' Sending Email To Updated Email Address
        SendResetEmailViaBoomiResponsys(requestObject, owner.ARVACT, "OwnerAccount- Send Email To New Email Confirmation")

        '' Switch EMAIL_ADDRESS_ and EmailAddress To send To Old Email Address
        emailAddress_ = New BoomiServiceResetEmail.SendDataToResponsysReqKeyValue()
        emailAddress_.Key = "EMAIL_ADDRESS_"
        emailAddress_.Value = Trim(emailOnPageLoad)
        requestObject.EmailAddress = New String() {Trim(emailOnPageLoad)}
        requestObject.KeyValues = New BoomiServiceResetEmail.SendDataToResponsysReqKeyValue() {ownerFirstName, ownerLastName, ownerArvact, dateTime, oldEmailAddress, newEmailAddress, emailAddress_, premierStatus}
        SendResetEmailViaBoomiResponsys(requestObject, Session("arvact"), "OwnerAccount-Send Email To Old Email Confirmation")

        '' Keep these objects updated to prevent resending of email.
        vssaOwner.OwnerEmailAddress = Trim(txtcurrentEmail.Text)
        If Not bxgowner.People(0).EmailAddresses Is Nothing Then
            bxgowner.People(0).EmailAddresses(0).Email = Trim(txtcurrentEmail.Text)
        End If
        emailOnPageLoad = Trim(txtcurrentEmail.Text)

    End Sub
    Protected Sub btnUpdateEmail_Click(sender As Object, e As EventArgs) Handles btnUpdateEmail.Click
        'Remove any message
        lblfeedback.Visible = False
        lblfeedback.Text = Nothing
        emailOnPageLoad = Session("ownerEmail")
        Dim ErrCheck = checkAddressFields("")

        If ErrCheck <> "Pass" Then

            lblfeedback.Text = ErrCheck
            lblfeedback.ForeColor = Drawing.Color.Red
            lblfeedback.Visible = True
            Return

        End If

        'Pull the owner object from the session
        owner = Session("owner")

        'Clone to verify changed and then compared to the 
        'original owner to track changes.
        tmpOwner = owner.cloneOwner()

        tmpOwner.Email = txtcurrentEmail.Text.Trim()

        If (ModifyOwnerInfo(tmpOwner, "updateEmail", owner)) Then
            If OwnerData.LogChanges(owner, tmpOwner) = "ChangesDone" Then
                If Session("HouseAccount") = True Then
                    lbResetGroupsForAccess.Visible = False
                    lblResetGroupsDivider.Visible = False
                    lbProxyAdmin.Visible = False
                    lblProxyAdminDivider.Visible = False
                    lbAddressUpload.Visible = False
                    lblAddressUploadDivider.Visible = False
                End If
                lblfeedback.Text = "Email Update Successful!"
                lblfeedback.ForeColor = Drawing.Color.Green
                lblfeedback.Visible = True

                'Insert commment to AS400
                insertAs400Comments("Email address has been changed from VSSA")

                'SEND AN EMAIL HERE TO BOTH THE OLD AND NEW EMAIL ADDRESS.
                'SO THAT THE USER KNOWS THAT IT HAS BEEN CHANGED.

                Try
                    If Session("Registered").ToLower.Equals("no") AndAlso Trim(txtcurrentEmail.Text).ToLower() <> Session("ownerEmail").ToLower() Then
                        SendEmailChangeConfirmation()
                    End If

                    owner = OwnerData.searchOwners(owner.ARVACT)
                    owner.Accounts = Session("AccountInfo")
                    Session("owner") = owner
                Catch ex As Exception

                    Me.lblError.ForeColor = Drawing.Color.Red
                    Me.lblError.Text = "Error sending email: " & ex.Message

                End Try

            Else
                lblfeedback.Text = "We did not detect any email address changes.  Update was not completed."
                lblfeedback.ForeColor = Drawing.Color.Red
                lblfeedback.Visible = True

            End If
        Else

            If tmpOwner.Email = owner.Email Then
                lblfeedback.Text = "We did not detect any email address changes.  Update was not completed."
                If Session("HouseAccount") = True Then
                    lbResetGroupsForAccess.Visible = False
                    lblResetGroupsDivider.Visible = False
                    lbProxyAdmin.Visible = False
                    lblProxyAdminDivider.Visible = False
                    lbAddressUpload.Visible = False
                    lblAddressUploadDivider.Visible = False
                End If
            Else
                lblfeedback.Text = "Email Update Failed!"
                If Session("HouseAccount") = True Then
                    lbResetGroupsForAccess.Visible = False
                    lblResetGroupsDivider.Visible = False
                    lbProxyAdmin.Visible = False
                    lblProxyAdminDivider.Visible = False
                    lbAddressUpload.Visible = False
                    lblAddressUploadDivider.Visible = False
                End If
            End If
            lblfeedback.ForeColor = Drawing.Color.Red
            lblfeedback.Visible = True
            txtcurrentEmail.Text = owner.Email

        End If

        makeCookie(owner)

    End Sub


    Protected Sub btnLoginasOwner_Click(sender As Object, e As EventArgs) Handles btnLoginasOwner.Click
        'Pull the owner object from the session
        owner = Session("owner")

        If txtcurrentEmail.Text.Trim() = "" Then
            lblfeedback.Text = "Email address can't be blank"
            lblfeedback.ForeColor = Drawing.Color.Red
            lblfeedback.Visible = True
            Return
        End If

        loginAsUser()

    End Sub

    Protected Sub btnresetUserPassword_Click(sender As Object, e As EventArgs) Handles btnresetUserPassword.Click

        Dim resetPasswordSvc As New SitecoreProfileSvc.ProfileSoapClient
        Dim resetPasswordRequest As New SitecoreProfileSvc.ForgotPasswordRemoteRequest
        Dim resetPasswordResponse As New SitecoreProfileSvc.WebServiceResponse
        Dim resetPasswordRequestBody As New SitecoreProfileSvc.ForgotPasswordRemoteRequestBody
        Dim resetPasswordResponseBody As New SitecoreProfileSvc.ForgotPasswordRemoteResponseBody

        'Remove any message
        lblfeedback.Visible = False
        lblfeedback.Text = Nothing

        'Pull the owner object from the session
        owner = Session("owner")

        If txtcurrentEmail.Text.Trim() = "" Then
            lblfeedback.Text = "Email address can't be blank"
            lblfeedback.ForeColor = Drawing.Color.Red
            lblfeedback.Visible = True
            Return
        End If

        If Session("Registered") = "Yes" Then
            resetPasswordRequestBody.ARVACT = owner.ARVACT
            resetPasswordRequestBody.email = owner.Email
            resetPasswordRequest.Body = resetPasswordRequestBody

            resetPasswordResponse = resetPasswordSvc.ForgotPasswordRemote(resetPasswordRequest.Body.email, resetPasswordRequest.Body.ARVACT)
        Else
            lblfeedback.Text = "Cannot reset password for unregistered user."
            lblfeedback.Visible = True
            lblfeedback.ForeColor = Drawing.Color.Red
            Return
        End If


        If resetPasswordResponse.isSuccessful Then
            If OwnerData.LogChanges(owner, tmpOwner) = "ChangesDone" Then
                lblfeedback.Text = "Password Reset Email Sent!"
                lblfeedback.ForeColor = Drawing.Color.Green
                lblfeedback.Visible = True
                makeCookie(owner)
            End If
        Else
            lblfeedback.Text = "Password Reset Email Send Failure! "
            lblfeedback.Visible = True
            lblfeedback.ForeColor = Drawing.Color.Red
            Return
        End If

    End Sub


    Protected Sub btnAddressUpload_Click(sender As Object, e As EventArgs)
        Response.Redirect("/AddressUpload/AddressUpload.aspx")
    End Sub

    Private Sub btnSearch_Load(sender As Object, e As EventArgs) Handles btnSearch.Load

    End Sub

    Private Sub btnUpdateEmail_Init(sender As Object, e As EventArgs) Handles btnUpdateEmail.Init

    End Sub

    Private Sub lbProxyVote_Click(sender As Object, e As EventArgs) Handles lbProxyVote.Click
        Try
            Dim Ownersvc As New OwnerService()
            Dim OwnerSvcResp As New OwnerFetchResponse()
            OwnerSvcResp = Ownersvc.OwnerFetch(txtARVACT.Text)
            Session("BoomiOwner") = OwnerSvcResp
            If Not (Session("BoomiOwner")) Is Nothing Then
                bxgowner = Session("BoomiOwner")
            End If
            If bxgowner.Account.Memberships.VacationClubMembership.Membership.MembershipInGoodStanding <> True Then
                lblfeedback.Visible = True
                lblfeedback.Text = "This owner is not in good standing and is not allowed to cast a vote"
                lblfeedback.ForeColor = Drawing.Color.Red
                Session("DisplayProxyVoting") = "False"
                Return
            End If
            If Not (bxgowner.Account.Memberships.VacationClubMembership.Legacy.MaintFeePaymentBalanceForReservation Is Nothing) Then
                If Not (bxgowner.Financial.InstallmentPlans Is Nothing) Then
                    If CDbl(bxgowner.Account.Memberships.VacationClubMembership.Legacy.MaintFeePaymentBalanceForReservation) > 50.0 And bxgowner.Financial.InstallmentPlans.InstallmentPlan(0).InstallmentPlanStatus <> "IP" Then
                        ' the error message will display on render based on session 
                        lblfeedback.Visible = True
                        lblfeedback.Text = "The balance for this owner’s account is greater than $50 and is not allowed to cast a vote"
                        lblfeedback.ForeColor = Drawing.Color.Red
                        Session("DisplayProxyVoting") = "False"
                    Else
                        Response.Redirect("ProxyVote.aspx")
                    End If
                Else
                    Response.Redirect("ProxyVote.aspx")
                End If
            Else
                Response.Redirect("ProxyVote.aspx")
            End If
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub btnCloseWindow_Click(sender As Object, e As System.EventArgs) Handles btnCloseWindow.Click
        If Not Session("NoTSWPersonID") = Nothing Then
            lbltswPersonID.Text = Session("NoTSWPersonID")
        ElseIf Not Session("CurrentTSWPersonID") = Nothing Then
            lbltswPersonID.Text = Session("CurrentTSWPersonID")
        End If
        If (UpdateTSWPersonID(txtARVACT.Text, lbltswPersonID.Text, tbEditable.Text)) Then
            lbltswPersonID.Text = tbEditable.Text
            Session("UpdatedTSWPersonID") = tbEditable.Text.ToString()
            lbltswPersonID.Visible = True
            btnUpdateTSW.Visible = False
            tbEditable.Visible = False
            btnCancel.Visible = False
            btnEdit.Visible = True
            lblTSWMessage.Visible = True
            lblTSWMessage.Text = "Updated TSW Person ID successfully."
            lblTSWMessage.ForeColor = System.Drawing.Color.Green
            lbltswPersonID.ForeColor = System.Drawing.Color.Green
            ModalPopupExtender1.Hide()
        Else
            lblTSWMessage.Text = "Updating TSW Person id failed."
            lblTSWMessage.ForeColor = System.Drawing.Color.Red
            btnUpdateTSW.Visible = False
            btnCancel.Visible = False
            btnEdit.Visible = True
            lbltswPersonID.Visible = True
            lbltswPersonID.Text = Session("CurrentTSWPersonID")
        End If

    End Sub
    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        lblTSWMessage.Visible = False
        lblTSWMessage.Text = String.Empty
        tbEditable.Visible = True
        tbEditable.Text = String.Empty
        tbEditable.Focus()
        lbltswPersonID.Visible = False
        lbltswPersonID.Visible = False
        tbEditable.ForeColor = System.Drawing.Color.Green
        btnUpdateTSW.Visible = True
        btnCancel.Visible = True
        btnEdit.Visible = False
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        btnEdit.Visible = True
        btnUpdateTSW.Visible = False
        btnCancel.Visible = False
        tbEditable.Visible = False
        lbltswPersonID.Visible = True
        If Session("UpdatedTSWPersonID") = Nothing Then
            If Not Session("NoTSWPersonID") = Nothing Then
                lbltswPersonID.Text = Session("NoTSWPersonID")
            ElseIf Not Session("CurrentTSWPersonID") = Nothing Then
                If Session("CurrentTSWPersonID") = 0 Then
                    lbltswPersonID.Text = "N/A"
                End If
            End If
        Else
            lbltswPersonID.Text = Session("UpdatedTSWPersonID")
        End If
    End Sub
    Public Sub invalid()
        lblTSWMessage.Text = ""
        lblTSWMessage.Visible = True
        lblTSWMessage.Text = "Invalid TSW Person id, should be numeric,3 to 10 digits & cannot begin with zero."
        lblTSWMessage.ForeColor = System.Drawing.Color.Red
        btnUpdateTSW.Visible = False
        btnCancel.Visible = False
        btnEdit.Visible = True
        lbltswPersonID.Visible = True
        tbEditable.Visible = False
        If Not Session("CurrentTSWPersonID") = Nothing Then
            If Session("CurrentTSWPersonID") = 0 Then
                lbltswPersonID.Text = "N/A"
            Else
                lbltswPersonID.Text = Session("CurrentTSWPersonID")
            End If
        End If

        ModalPopupExtender1.Hide()
    End Sub
    Protected Sub btnUpdateTSW_Click(sender As Object, e As EventArgs)
        If tbEditable.Text = "N/A" Then
            invalid()
        ElseIf tbEditable.Text = String.Empty Then
            invalid()
        ElseIf tbEditable.Text.Length < 3 Then
            invalid()
        ElseIf tbEditable.Text.Length > 10 Then
            invalid()
        ElseIf tbEditable.Text.Length > 0 And tbEditable.Text.Substring(0, 1) = "0" Then
            invalid()
        Else
            pnlOverlay.Visible = True
            TSWBindGrid()
            If Not TransactionError = "" Then
                lblTSWMessage.Visible = True
                lblTSWMessage.Text = "Invalid TSW Person id, the maximum integer value was exceeded, should be <=2147483647."
                lblTSWMessage.ForeColor = System.Drawing.Color.Red
                btnUpdateTSW.Visible = False
                btnCancel.Visible = False
                btnEdit.Visible = True
                tbEditable.Visible = False
                lbltswPersonID.Visible = True
                lbltswPersonID.Text = Session("CurrentTSWPersonID")
            Else
                If gvTSW.Rows.Count > 0 Then
                    ModalPopupExtender1.Show()
                Else
                    Dim i As Integer
                    If Integer.TryParse(tbEditable.Text, i) AndAlso i <= Integer.MaxValue Then
                        If (UpdateTSWPersonID(txtARVACT.Text, lbltswPersonID.Text, tbEditable.Text)) Then
                            lbltswPersonID.Text = tbEditable.Text
                            lbltswPersonID.Visible = True
                            lbltswPersonID.ForeColor = System.Drawing.Color.Green
                            btnUpdateTSW.Visible = False
                            tbEditable.Visible = False
                            btnCancel.Visible = False
                            btnEdit.Visible = True
                            lblTSWMessage.Visible = True
                            lblTSWMessage.Text = "Updated TSW Person id successfully."
                            lblTSWMessage.ForeColor = System.Drawing.Color.Green
                            ModalPopupExtender1.Hide()
                        Else
                            lblTSWMessage.Text = "Updating TSW Person id failed."
                            lblTSWMessage.ForeColor = System.Drawing.Color.Red
                            btnUpdateTSW.Visible = False
                            btnCancel.Visible = False
                            btnEdit.Visible = True
                            tbEditable.Visible = False
                            lbltswPersonID.Visible = True
                            lbltswPersonID.Text = Session("CurrentTSWPersonID")
                        End If
                    Else
                        lblTSWMessage.Visible = True
                        lblTSWMessage.Text = "Invalid TSW Person id, the maximum integer value was exceeded, should be <=2147483647."
                        lblTSWMessage.ForeColor = System.Drawing.Color.Red
                        btnUpdateTSW.Visible = False
                        btnCancel.Visible = False
                        btnEdit.Visible = True
                        tbEditable.Visible = False
                        lbltswPersonID.Visible = True
                        lbltswPersonID.Text = Session("CurrentTSWPersonID")
                    End If
                End If
            End If

        End If

    End Sub

    Protected Sub btnCancelWindow_Click(sender As Object, e As EventArgs) Handles btnCancelWindow.Click
        btnEdit.Visible = True
        btnUpdateTSW.Visible = False
        btnCancel.Visible = False
        tbEditable.Visible = False
        lbltswPersonID.Visible = True
        lblTSWMessage.Text = String.Empty
        lblTSWMessage.Visible = False
        If Not Session("NoTSWPersonID") = Nothing Then
            lbltswPersonID.Text = Session("NoTSWPersonID")
        ElseIf Not Session("CurrentTSWPersonID") = Nothing Then
            If Session("CurrentTSWPersonID") = 0 Then
                lbltswPersonID.Text = "N/A"
            End If
        End If
        ModalPopupExtender1.Hide()
    End Sub
    Private Sub TSWBindGrid()
        Dim SqlConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("VSSA.bxgwebDBConnection").ConnectionString)
        Dim SqlCommand As New System.Data.SqlClient.SqlCommand
        Dim da As New SqlDataAdapter
        Dim dtTable As New DataTable
        Try
            SqlCommand.CommandText = "uspSelectOwnerswithTSW"
            SqlCommand.CommandType = System.Data.CommandType.StoredProcedure
            SqlCommand.Parameters.Clear()
            SqlCommand.Connection = SqlConnection
            SqlCommand.Connection.Open()
            SqlCommand.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TSWPersonID", System.Data.SqlDbType.Int, 4)).Value = tbEditable.Text
            da.SelectCommand = SqlCommand
            da.Fill(dtTable)
            gvTSW.DataSource = dtTable
            gvTSW.DataBind()
        Catch ex As Exception
            TransactionError = ex.Message.ToString()
        Finally
            If SqlConnection.State = ConnectionState.Open Then
                SqlConnection.Close()
            End If
        End Try
    End Sub
    Public Function ShowLineBreaks(ByVal text As Object) As String
        Return (text.ToString().Replace(vbCrLf, "<br/>"))
    End Function
    Private Sub HandlingPostbacks()
        Dim fvRow = fvOwner.Row
        Dim lblUpdateMessage As Label = fvRow.FindControl("lblUpdateMessage")
        Dim ddlCountry As DropDownList = fvRow.FindControl("ddlCountry")
        Dim ddlState As DropDownList = fvRow.FindControl("ddlState")
        Dim txtAddress1 As TextBox = fvRow.FindControl("txtAddress1")
        Dim txtAddress2 As TextBox = fvRow.FindControl("txtAddress2")
        Dim txtAddress3 As TextBox = fvRow.FindControl("txtAddress3")
        Dim txtCity As TextBox = fvRow.FindControl("txtCity")
        Dim txtZIP As TextBox = fvRow.FindControl("txtZIP")
        Dim txtPhoneHome As TextBox = fvRow.FindControl("txtHomePhone")
        Dim txtPhoneAlt As TextBox = fvRow.FindControl("txtAltPhone")
        Dim UpdatePanel3 As UpdatePanel = fvRow.FindControl("UpdatePanel3")
        FormData = Session("owner")
        owner = Session("owner")
        ChangedOwner = Session("owner")
        OriginalOwner = Session("owner")
        Session("OriginalData") = Session("owner")
        Dim continueUpdating As Boolean = True
        lblUpdateMessage.Text = ""
        lblUpdateMessage.Visible = False
        Dim validzip As String = checkForNumbers(txtZIP.Text)
        Dim ErrCheck = checkAddressFields("UpdateContactInfo")
        If ErrCheck <> "Pass" Then

            lblUpdateMessage.Text = ErrCheck
            lblUpdateMessage.ForeColor = Drawing.Color.Red
            lblUpdateMessage.Visible = True
            continueUpdating = False

        End If
        ChangedOwner = owner.cloneOwner()
        FormData = owner.cloneOwner()
        If ddlCountry.SelectedValue = "US" Then
            With FormData
                .Address1 = txtAddress1.Text
                .Address2 = txtAddress2.Text
                .Address3 = txtAddress3.Text
                .City = txtCity.Text
                .State = ddlState.SelectedItem.Value
                .Subdivision = ddlState.SelectedItem.Text
                .PostalCode = txtZIP.Text
                .CountryCode = ddlCountry.SelectedValue
                .PhoneHome = txtPhoneHome.Text
                .PhoneCell = txtPhoneAlt.Text

            End With
            Session("formOwnerData") = FormData
            AddressPass = OwnerData.VerifyMelissaAddress(FormData)
            Dim message As String = Nothing
            Dim validationIssues As List(Of String) = New List(Of String)()
            Dim pipedArray = AddressPass.TrimEnd("|"c).Split("|"c)
            Dim splittedValue As String() = Nothing
            Dim splitValue As String = Nothing

            If Session("SuggestedAddress") IsNot Nothing Then
                AddressPass = AddressPass.Replace(".,", "<br/>")
                ChangedOwner = Session("SuggestedAddress")
                OriginalOwner = Session("formOwnerData")
                lblMeliStatus.Text = String.Format(AddressPass, "<br/>")
                lblMeliStatus.ForeColor = Drawing.Color.Red
                enterAddr1.Text = OriginalOwner.Address1
                suggAddr1.Text = ChangedOwner.Address1
                enterAddr2.Text = OriginalOwner.Address2
                suggAddr2.Text = ChangedOwner.Address2
                entercity.Text = OriginalOwner.City
                suggcity.Text = ChangedOwner.City
                enterstate.Text = OriginalOwner.State
                suggstate.Text = ChangedOwner.State
                enterpostal.Text = OriginalOwner.PostalCode
                suggpostal.Text = ChangedOwner.PostalCode
                rbsuggested.Checked = True
                rbentered.Checked = False
                SuggPanel.Visible = True
                rbconfirm.Checked = False
                ModalPopupExtender2.Show()
            Else
                OriginalOwner = Session("formOwnerData")
                AddressPass = AddressPass.Replace(".,", "<br/>")
                lblMStatus.Text = String.Format(AddressPass, "<br/>")
                Dim color As String = Session("FontColor")
                Dim resStatusCode = Session("ResStatusCode")
                If color.Contains("AE") Then
                    lblMStatus.ForeColor = Drawing.Color.Red
                ElseIf color.Contains("AS") Then
                    lblMStatus.ForeColor = Drawing.Color.Green
                ElseIf resStatusCode.Contains("AE") Then
                    lblMStatus.ForeColor = Drawing.Color.Red
                Else
                    lblMStatus.ForeColor = Drawing.Color.Green
                End If
                confirmaddr1.Text = OriginalOwner.Address1
                confirmaddr2.Text = OriginalOwner.Address2
                confirmcity.Text = OriginalOwner.City
                confirmstate.Text = OriginalOwner.State
                confirmpostal.Text = OriginalOwner.PostalCode
                SubmitnosuggPanel.Visible = True
                rbconfirm.Checked = True
                rbentered.Checked = False
                rbsuggested.Checked = False
                ModalPopupExtender3.Show()
            End If
        Else
            tmpOwner = owner.cloneOwner()
            With tmpOwner
                .Address1 = txtAddress1.Text
                .Address2 = txtAddress2.Text
                .Address3 = txtAddress3.Text
                .City = txtCity.Text
                .State = ddlState.SelectedItem.Value
                .Subdivision = ddlState.SelectedItem.Text
                .PostalCode = txtZIP.Text
                .CountryCode = ddlCountry.SelectedValue
                .PhoneHome = txtPhoneHome.Text
                .PhoneCell = txtPhoneAlt.Text

            End With
            If continueUpdating Then

                If Not Object.Equals(owner, tmpOwner) Then

                    If OwnerData.ModifyOwnerInfo(tmpOwner, "UpdateContact") Then
                        lblUpdateMessage.ForeColor = Drawing.Color.Green
                        lblUpdateMessage.Visible = True
                        If Session("HouseAccount") = True Then
                            lbResetGroupsForAccess.Visible = False
                            lblResetGroupsDivider.Visible = False
                            lbProxyAdmin.Visible = False
                            lblProxyAdminDivider.Visible = False
                            lbAddressUpload.Visible = False
                            lblAddressUploadDivider.Visible = False
                        End If
                    Else
                        lblUpdateMessage.Text = "Error! Update Failed!"
                        lblUpdateMessage.ForeColor = Drawing.Color.Red
                        lblUpdateMessage.Visible = True
                        continueUpdating = False
                    End If

                Else
                    lblUpdateMessage.Text = "No Changes Made."
                    lblUpdateMessage.ForeColor = Drawing.Color.Red
                    lblUpdateMessage.Visible = True
                    continueUpdating = False

                End If

                If continueUpdating Then
                    'Log new Owner information
                    If OwnerData.LogChanges(owner, tmpOwner) = "ChangesDone" Then

                        Dim changes As New StringBuilder
                        Dim everythingChanged As Boolean

                        If owner.Address1 <> tmpOwner.Address1 Or owner.Address2 <> tmpOwner.Address2 Or owner.Address3 <> tmpOwner.Address3 Or owner.City <> tmpOwner.City Or owner.State <> tmpOwner.State Or owner.PostalCode <> tmpOwner.PostalCode Or owner.CountryCode <> tmpOwner.CountryCode Then
                            If owner.PhoneCell <> tmpOwner.PhoneCell Or owner.PhoneHome <> tmpOwner.PhoneHome Then

                                changes.Append("Contact Information")
                                everythingChanged = True
                                insertAs400Comments("Owner contact information has been changed from VSSA")
                            End If

                        End If

                        If Not everythingChanged Then

                            If owner.Address1 <> tmpOwner.Address1 Or owner.Address2 <> tmpOwner.Address2 Or owner.Address3 <> tmpOwner.Address3 Or owner.City <> tmpOwner.City Or owner.State <> tmpOwner.State Or owner.PostalCode <> tmpOwner.PostalCode Or owner.CountryCode <> tmpOwner.CountryCode Then
                                changes.Append("Address information")
                                insertAs400Comments("Owner address information has been changed from VSSA")
                            End If


                            If owner.PhoneCell <> tmpOwner.PhoneCell Or owner.PhoneHome <> tmpOwner.PhoneHome Then
                                changes.Append("Phone information")
                                insertAs400Comments("Owner phone information has been changed from VSSA")
                            End If
                        End If

                        changes.Append(" changed successfully.")
                        lblUpdateMessage.Text = changes.ToString
                        lblUpdateMessage.ForeColor = Drawing.Color.Green
                        lblUpdateMessage.Visible = True

                        owner = OwnerData.searchOwners(owner.ARVACT)
                        Session("owner") = tmpOwner
                        Session("changes") = lblUpdateMessage.Text
                        With tmpOwner
                            txtAddress1.Text = .Address1
                            txtAddress2.Text = .Address2
                            txtAddress3.Text = .Address3
                            txtCity.Text = .City
                            ddlState.SelectedItem.Value = .State
                            ddlState.SelectedItem.Text = .Subdivision
                            txtZIP.Text = .PostalCode
                            ddlCountry.SelectedItem.Text = .CountryCode

                        End With
                        If Not Session("changes") = Nothing Then

                            lblUpdateMessage.Text = Session("changes").ToString
                            If lblUpdateMessage.Text = "Since no change was detected, no information was updated." Then
                                lblUpdateMessage.ForeColor = Drawing.Color.Red
                                lblUpdateMessage.Visible = True
                            Else
                                lblUpdateMessage.ForeColor = Drawing.Color.Green
                                lblUpdateMessage.Visible = True
                            End If

                        End If

                    Else
                        lblUpdateMessage.Text = "Since no change was detected, no information was updated."
                        lblUpdateMessage.ForeColor = Drawing.Color.Red
                        lblUpdateMessage.Visible = True
                        Session("changes") = lblUpdateMessage.Text
                    End If

                End If

            End If
        End If
    End Sub
    Public Function checkNonUSValidation(ByVal _action As String) As String
        Try
            Dim fvRow As FormViewRow = fvOwner.Row

            Dim txtAddress1 As TextBox = fvRow.FindControl("txtAddress1")
            Dim txtPhoneHome As TextBox = fvRow.FindControl("txtHomePhone")
            Dim validHomePhone As String = checkForNumbers(txtPhoneHome.Text)
            If txtAddress1.Text.Trim = "" Then
                Err = "Address1 Cannot be Blank."
                txtAddress1.Focus()
                Exit Try
            Else
                IsValidAddress(txtAddress1.Text.Trim)
                If Not Err = "Pass" Then
                    txtAddress1.Focus()
                    Exit Try
                End If
            End If
            If ((validHomePhone.Length = 0) AndAlso (validHomePhone.Length < 10)) Then
                Err = String.Format("Invalid Primary Phone Number")
                txtPhoneHome.Focus()
            End If
        Catch ex As Exception

        End Try
        Return Err
    End Function
    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)
        Dim fvRow = fvOwner.Row
        Dim lblUpdateMessage As Label = fvRow.FindControl("lblUpdateMessage")
        Dim ddlCountry As DropDownList = fvRow.FindControl("ddlCountry")
        If Not Session("changes") = Nothing Then
            Session("changes") = Nothing
            lblUpdateMessage.Text = ""
        End If
        If Not Session("ValidationMessage") Is Nothing Then
            Session("ValidationMessage") = Nothing
        End If
        Dim _action = "UpdateContactInfo"
        Dim ErrCheck = checkAddressFields(_action)

        If ErrCheck <> "Pass" Then
            lblUpdateMessage.Text = ErrCheck
            Session("ValidationMessage") = ErrCheck
            'lblfeedback.Text = ErrCheck
            lblUpdateMessage.ForeColor = Drawing.Color.Red
            lblUpdateMessage.Visible = True
            Return
        Else
            If Not Page.IsPostBack Then
                HandlingPostbacks()
                If Not ddlCountry.SelectedValue = "US" Then
                    If Not Session("changes") = Nothing Then

                        lblUpdateMessage.Text = Session("changes").ToString
                        If lblUpdateMessage.Text = "Since no change was detected, no information was updated." Then
                            lblUpdateMessage.ForeColor = Drawing.Color.Red
                            lblUpdateMessage.Visible = True
                        ElseIf lblUpdateMessage.Text = "Error! Update Failed!" Then
                            lblUpdateMessage.ForeColor = Drawing.Color.Red
                            lblUpdateMessage.Visible = True
                        ElseIf lblUpdateMessage.Text = "No Changes Made." Then
                            lblUpdateMessage.ForeColor = Drawing.Color.Red
                            lblUpdateMessage.Visible = True
                        Else
                            lblUpdateMessage.ForeColor = Drawing.Color.Green
                            lblUpdateMessage.Visible = True
                        End If

                    End If
                End If

            Else
                HandlingPostbacks()
                If Not ddlCountry.SelectedValue = "US" Then
                    If Not Session("changes") = Nothing Then

                        lblUpdateMessage.Text = Session("changes").ToString
                        If lblUpdateMessage.Text = "Since no change was detected, no information was updated." Then
                            lblUpdateMessage.ForeColor = Drawing.Color.Red
                            lblUpdateMessage.Visible = True
                        ElseIf lblUpdateMessage.Text = "Error! Update Failed!" Then
                            lblUpdateMessage.ForeColor = Drawing.Color.Red
                            lblUpdateMessage.Visible = True
                        ElseIf lblUpdateMessage.Text = "No Changes Made." Then
                            lblUpdateMessage.ForeColor = Drawing.Color.Red
                            lblUpdateMessage.Visible = True
                        Else
                            lblUpdateMessage.ForeColor = Drawing.Color.Green
                            lblUpdateMessage.Visible = True
                        End If

                    End If
                End If

            End If

        End If

    End Sub
    Protected Sub Suggok_Click(sender As Object, e As EventArgs)
        Dim fvRow = fvOwner.Row
        Dim lblUpdateMessage As Label = fvRow.FindControl("lblUpdateMessage")
        If Not Session("changes") = Nothing Then
            Session("changes") = Nothing
            lblUpdateMessage.Text = ""
        End If

        If rbsuggested.Checked = True Then
            tmpOwner = Session("SuggestedAddress")
        Else
            tmpOwner = Session("formOwnerData")
        End If
        ModalPopupExtender2.Hide()
        UpdateContactInfo()
        If Not Session("changes") = Nothing Then

            lblUpdateMessage.Text = Session("changes").ToString
            If lblUpdateMessage.Text = "Since no change was detected, no information was updated." Then
                lblUpdateMessage.ForeColor = Drawing.Color.Red
                lblUpdateMessage.Visible = True
            ElseIf lblUpdateMessage.Text = "Error! Update Failed!" Then
                lblUpdateMessage.ForeColor = Drawing.Color.Red
                lblUpdateMessage.Visible = True
            ElseIf lblUpdateMessage.Text = "No Changes Made." Then
                lblUpdateMessage.ForeColor = Drawing.Color.Red
                lblUpdateMessage.Visible = True
            Else
                lblUpdateMessage.ForeColor = Drawing.Color.Green
                lblUpdateMessage.Visible = True
            End If

        End If
        rbentered.Checked = False
        rbsuggested.Checked = True
        If Not Session("formOwnerData") Is Nothing Then
            Session("formOwnerData") = Nothing
        End If
        If Not Session("SuggestedAddress") Is Nothing Then
            Session("SuggestedAddress") = Nothing
        End If

    End Sub
    Private Sub UpdateContactInfo()
        Dim fvRow As FormViewRow = fvOwner.Row
        Dim lblUpdateMessage As Label = fvRow.FindControl("lblUpdateMessage")
        Dim ddlCountry As DropDownList = fvRow.FindControl("ddlCountry")
        Dim ddlState As DropDownList = fvRow.FindControl("ddlState")
        Dim txtAddress1 As TextBox = fvRow.FindControl("txtAddress1")
        Dim txtAddress2 As TextBox = fvRow.FindControl("txtAddress2")
        Dim txtAddress3 As TextBox = fvRow.FindControl("txtAddress3")
        Dim txtCity As TextBox = fvRow.FindControl("txtCity")
        Dim txtZIP As TextBox = fvRow.FindControl("txtZIP")
        Dim txtPhoneHome As TextBox = fvRow.FindControl("txtHomePhone")
        Dim txtPhoneAlt As TextBox = fvRow.FindControl("txtAltPhone")
        Dim vZip As RequiredFieldValidator = fvRow.FindControl("vzip")
        Dim pnlUserInfo As Panel = fvRow.FindControl("pnlUserInfo")
        Dim continueUpdating As Boolean = True
        Dim Suggesstion As New BXG_Owner()
        Dim InfoChangedOwner As New BXG_Owner()
        owner = Session("owner")
        If rbsuggested.Checked = True Then
            tmpOwner = Session("SuggestedAddress")
        ElseIf rbentered.Checked = True Then
            tmpOwner = Session("formOwnerData")
        ElseIf rbconfirm.Checked = True Then
            tmpOwner = Session("formOwnerData")
        End If
        lblUpdateMessage.Text = ""
        lblUpdateMessage.Visible = False

        Dim ErrCheck = checkAddressFields("UpdateContactInfo")
        If ErrCheck <> "Pass" Then

            lblUpdateMessage.Text = ErrCheck
            lblUpdateMessage.ForeColor = Drawing.Color.Red
            lblUpdateMessage.Visible = True
            continueUpdating = False

        End If
        If continueUpdating Then
            If Not Object.Equals(owner, tmpOwner) Then

                If OwnerData.ModifyOwnerInfo(tmpOwner, "UpdateContact") Then
                    lblUpdateMessage.ForeColor = Drawing.Color.Green
                    lblUpdateMessage.Visible = True
                    If Session("HouseAccount") = True Then
                        lbResetGroupsForAccess.Visible = False
                        lblResetGroupsDivider.Visible = False
                        lbProxyAdmin.Visible = False
                        lblProxyAdminDivider.Visible = False
                        lbAddressUpload.Visible = False
                        lblAddressUploadDivider.Visible = False
                    End If
                Else
                    lblUpdateMessage.Text = "Error! Update Failed!"
                    lblUpdateMessage.ForeColor = Drawing.Color.Red
                    lblUpdateMessage.Visible = True
                    continueUpdating = False
                    Session("changes") = lblUpdateMessage.Text
                End If

            Else
                lblUpdateMessage.Text = "No Changes Made."
                lblUpdateMessage.ForeColor = Drawing.Color.Red
                lblUpdateMessage.Visible = True
                continueUpdating = False
                Session("changes") = lblUpdateMessage.Text
            End If

            If continueUpdating Then
                'Log new Owner information
                If OwnerData.LogChanges(owner, tmpOwner) = "ChangesDone" Then

                    Dim changes As New StringBuilder
                    Dim everythingChanged As Boolean

                    If owner.Address1 <> tmpOwner.Address1 Or owner.Address2 <> tmpOwner.Address2 Or owner.Address3 <> tmpOwner.Address3 Or owner.City <> tmpOwner.City Or owner.State <> tmpOwner.State Or owner.PostalCode <> tmpOwner.PostalCode Or owner.CountryCode <> tmpOwner.CountryCode Then
                        If owner.PhoneCell <> tmpOwner.PhoneCell Or owner.PhoneHome <> tmpOwner.PhoneHome Then

                            changes.Append("Contact Information")
                            everythingChanged = True
                            insertAs400Comments("Owner contact information has been changed from VSSA")
                        End If

                    End If

                    If Not everythingChanged Then

                        If owner.Address1 <> tmpOwner.Address1 Or owner.Address2 <> tmpOwner.Address2 Or owner.Address3 <> tmpOwner.Address3 Or owner.City <> tmpOwner.City Or owner.State <> tmpOwner.State Or owner.PostalCode <> tmpOwner.PostalCode Or owner.CountryCode <> tmpOwner.CountryCode Then
                            changes.Append("Address information")
                            insertAs400Comments("Owner address information has been changed from VSSA")
                        End If


                        If owner.PhoneCell <> tmpOwner.PhoneCell Or owner.PhoneHome <> tmpOwner.PhoneHome Then
                            changes.Append("Phone information")
                            insertAs400Comments("Owner phone information has been changed from VSSA")
                        End If
                    End If

                    changes.Append(" changed successfully.")
                    lblUpdateMessage.Text = changes.ToString
                    lblUpdateMessage.ForeColor = Drawing.Color.Green
                    lblUpdateMessage.Visible = True
                    owner = OwnerData.searchOwners(owner.ARVACT)
                    Session("owner") = tmpOwner
                    With tmpOwner
                        txtAddress1.Text = .Address1
                        txtAddress2.Text = .Address2
                        txtAddress3.Text = .Address3
                        txtCity.Text = .City

                        If .CountryCode.ToUpper.Equals("US") Then
                            If String.IsNullOrEmpty(.State) Then
                                .State = getStateBySubdivision(.Subdivision)
                            End If
                            If String.IsNullOrEmpty(.Subdivision) OrElse .Subdivision.Length.Equals(2) Then
                                .Subdivision = getSubdivisionByState(.State)
                            End If
                            If Not String.IsNullOrEmpty(.State) Then
                                ddlState.SelectedValue = .State
                            ElseIf Not String.IsNullOrEmpty(.Subdivision) Then
                                ddlState.SelectedValue = .Subdivision
                            End If
                        End If

                        txtZIP.Text = .PostalCode
                        ddlCountry.SelectedItem.Text = .CountryCode
                        txtPhoneHome.Text = .PhoneHome
                        txtPhoneAlt.Text = .PhoneCell

                    End With
                    Session("owner") = tmpOwner
                    Session("changes") = lblUpdateMessage.Text

                Else
                    ddlState.Items.Clear()
                    populateStatesDDL()
                    Session("owner") = tmpOwner
                    With tmpOwner
                        txtAddress1.Text = .Address1
                        txtAddress2.Text = .Address2
                        txtAddress3.Text = .Address3
                        txtCity.Text = .City
                        If .CountryCode.ToUpper.Equals("US") Then
                            If String.IsNullOrEmpty(.State) Then
                                .State = getStateBySubdivision(.Subdivision)
                            End If
                            If String.IsNullOrEmpty(.Subdivision) OrElse .Subdivision.Length.Equals(2) Then
                                .Subdivision = getSubdivisionByState(.State)
                            End If
                            If Not String.IsNullOrEmpty(.State) Then
                                ddlState.SelectedValue = .State
                            ElseIf Not String.IsNullOrEmpty(.Subdivision) Then
                                ddlState.SelectedValue = .Subdivision
                            End If
                        End If
                        txtZIP.Text = .PostalCode
                        ddlCountry.SelectedItem.Text = .CountryCode

                        txtPhoneHome.Text = .PhoneHome
                        txtPhoneAlt.Text = .PhoneCell

                    End With
                    Session("owner") = tmpOwner
                    lblUpdateMessage.Text = "Since no change was detected, no information was updated."
                    lblUpdateMessage.ForeColor = Drawing.Color.Red
                    lblUpdateMessage.Visible = True
                    Session("changes") = lblUpdateMessage.Text
                End If
            Else
                lblUpdateMessage.Text = "Since no change was detected, no information was updated."
                lblUpdateMessage.ForeColor = Drawing.Color.Red
                lblUpdateMessage.Visible = True
                Session("changes") = lblUpdateMessage.Text
            End If

        End If

    End Sub
    Protected Sub rbsuggested_CheckedChanged(sender As Object, e As EventArgs)
        ModalPopupExtender2.Show()
        If rbsuggested.Checked Then
            rbentered.Checked = False
        End If
    End Sub
    Protected Sub rbentered_CheckedChanged(sender As Object, e As EventArgs)
        ModalPopupExtender2.Show()
        If rbentered.Checked Then
            rbsuggested.Checked = False
        End If
    End Sub
    Protected Sub btnconfirmSubmit_Click(sender As Object, e As EventArgs)
        If rbconfirm.Checked = True Then
            Dim fvRow = fvOwner.Row
            Dim lblUpdateMessage As Label = fvRow.FindControl("lblUpdateMessage")
            If Not Session("changes") = Nothing Then
                Session("changes") = Nothing
                lblUpdateMessage.Text = ""
            End If
            tmpOwner = Session("formOwnerData")
            ModalPopupExtender3.Hide()
            UpdateContactInfo()
            If Not Session("changes") = Nothing Then

                lblUpdateMessage.Text = Session("changes").ToString
                If lblUpdateMessage.Text = "Since no change was detected, no information was updated." Then
                    lblUpdateMessage.ForeColor = Drawing.Color.Red
                    lblUpdateMessage.Visible = True
                ElseIf lblUpdateMessage.Text = "Error! Update Failed!" Then
                    lblUpdateMessage.ForeColor = Drawing.Color.Red
                    lblUpdateMessage.Visible = True
                ElseIf lblUpdateMessage.Text = "No Changes Made." Then
                    lblUpdateMessage.ForeColor = Drawing.Color.Red
                    lblUpdateMessage.Visible = True
                Else
                    lblUpdateMessage.ForeColor = Drawing.Color.Green
                    lblUpdateMessage.Visible = True
                End If

            End If
            If Not Session("formOwnerData") Is Nothing Then
                Session("formOwnerData") = Nothing
            End If
            If Not Session("SuggestedAddress") Is Nothing Then
                Session("SuggestedAddress") = Nothing
            End If
        End If
    End Sub

    'Protected Sub btnconfirmCancel_Click(sender As Object, e As EventArgs)
    '    Dim fvRow = fvOwner.Row
    '    Dim lblUpdateMessage As Label = fvRow.FindControl("lblUpdateMessage")
    '    Dim ddlCountry As DropDownList = fvRow.FindControl("ddlCountry")
    '    Dim ddlState As DropDownList = fvRow.FindControl("ddlState")
    '    Dim txtAddress1 As TextBox = fvRow.FindControl("txtAddress1")
    '    Dim txtAddress2 As TextBox = fvRow.FindControl("txtAddress2")
    '    Dim txtAddress3 As TextBox = fvRow.FindControl("txtAddress3")
    '    Dim txtCity As TextBox = fvRow.FindControl("txtCity")
    '    Dim txtZIP As TextBox = fvRow.FindControl("txtZIP")
    '    Dim txtPhoneHome As TextBox = fvRow.FindControl("txtHomePhone")
    '    Dim txtPhoneAlt As TextBox = fvRow.FindControl("txtAltPhone")
    '    Dim continueUpdating As Boolean
    '    lblUpdateMessage.Text = "Address update canceled."
    '    lblUpdateMessage.ForeColor = Drawing.Color.Red
    '    lblUpdateMessage.Visible = True
    '    continueUpdating = False
    '    ddlState.Items.Clear()
    '    populateStatesDDL()
    '    owner = Session("OriginalData")
    '    txtAddress1.Text = owner.Address1
    '    txtAddress2.Text = owner.Address2
    '    txtAddress3.Text = owner.Address3
    '    txtCity.Text = owner.City
    '    With owner
    '        If .CountryCode.ToUpper.Equals("US") Then
    '            If String.IsNullOrEmpty(.State) Then
    '                .State = getStateBySubdivision(.Subdivision)
    '            End If
    '            If String.IsNullOrEmpty(.Subdivision) OrElse .Subdivision.Length.Equals(2) Then
    '                .Subdivision = getSubdivisionByState(.State)
    '            End If
    '            If Not String.IsNullOrEmpty(.State) Then
    '                ddlState.SelectedValue = .State
    '            ElseIf Not String.IsNullOrEmpty(.Subdivision) Then
    '                ddlState.SelectedValue = .Subdivision
    '            End If
    '        End If
    '    End With
    '    txtZIP.Text = owner.PostalCode
    '    ddlCountry.SelectedValue = owner.CountryCode
    '    txtPhoneHome.Text = owner.PhoneHome
    '    txtPhoneAlt.Text = owner.PhoneCell
    '    ModalPopupExtender3.Hide()
    'End Sub
    Protected Sub SuggCancel_Click(sender As Object, e As EventArgs)
        Dim fvRow = fvOwner.Row
        Dim lblUpdateMessage As Label = fvRow.FindControl("lblUpdateMessage")
        Dim ddlCountry As DropDownList = fvRow.FindControl("ddlCountry")
        Dim ddlState As DropDownList = fvRow.FindControl("ddlState")
        Dim txtAddress1 As TextBox = fvRow.FindControl("txtAddress1")
        Dim txtAddress2 As TextBox = fvRow.FindControl("txtAddress2")
        Dim txtAddress3 As TextBox = fvRow.FindControl("txtAddress3")
        Dim txtCity As TextBox = fvRow.FindControl("txtCity")
        Dim txtZIP As TextBox = fvRow.FindControl("txtZIP")
        Dim txtPhoneHome As TextBox = fvRow.FindControl("txtHomePhone")
        Dim txtPhoneAlt As TextBox = fvRow.FindControl("txtAltPhone")
        Dim lblAddress3 As Label = fvRow.FindControl("lblAddress3")
        Dim lblCity As Label = fvRow.FindControl("lblCity")
        Dim lblState As Label = fvRow.FindControl("lblState")
        Dim lblZIP As Label = fvRow.FindControl("lblZIP")
        Dim continueUpdating As Boolean
        ddlState.Items.Clear()
        populateStatesDDL()
        owner = Session("OriginalData")
        lblUpdateMessage.Text = "Address update canceled."
        lblUpdateMessage.ForeColor = Drawing.Color.Red
        lblUpdateMessage.Visible = True
        continueUpdating = False
        rbentered.Checked = False
        rbsuggested.Checked = False
        With owner
            If .CountryCode.ToUpper.Equals("US") Then
                If String.IsNullOrEmpty(.State) Then
                    .State = getStateBySubdivision(.Subdivision)
                End If
                If String.IsNullOrEmpty(.Subdivision) OrElse .Subdivision.Length.Equals(2) Then
                    .Subdivision = getSubdivisionByState(.State)
                End If
                If Not String.IsNullOrEmpty(.State) Then
                    ddlState.SelectedValue = .State
                ElseIf Not String.IsNullOrEmpty(.Subdivision) Then
                    ddlState.SelectedValue = .Subdivision
                End If
                txtAddress1.Text = owner.Address1
                txtAddress2.Text = owner.Address2
                txtAddress3.Text = owner.Address3
                txtCity.Text = owner.City
                txtZIP.Text = owner.PostalCode
                ddlCountry.SelectedValue = owner.CountryCode
                txtPhoneHome.Text = owner.PhoneHome
                txtPhoneAlt.Text = owner.PhoneCell
            Else
                txtAddress1.Visible = True
                txtAddress1.Text = owner.Address1
                txtAddress2.Visible = True
                txtAddress2.Text = owner.Address2
                lblAddress3.Visible = True
                txtAddress3.Visible = True
                txtAddress3.Text = owner.Address3
                txtCity.Visible = False
                lblCity.Visible = False
                lblState.Visible = False
                ddlState.Visible = False
                lblZIP.Visible = False
                txtZIP.Visible = False
                ddlCountry.SelectedValue = owner.CountryCode
                txtPhoneHome.Text = owner.PhoneHome
                txtPhoneAlt.Text = owner.PhoneCell
            End If
        End With

        ModalPopupExtender2.Hide()
    End Sub


    Protected Sub btnconfirmCancel_Click(sender As Object, e As EventArgs)
        Dim fvRow = fvOwner.Row
        Dim lblUpdateMessage As Label = fvRow.FindControl("lblUpdateMessage")
        Dim ddlCountry As DropDownList = fvRow.FindControl("ddlCountry")
        Dim ddlState As DropDownList = fvRow.FindControl("ddlState")
        Dim txtAddress1 As TextBox = fvRow.FindControl("txtAddress1")
        Dim txtAddress2 As TextBox = fvRow.FindControl("txtAddress2")
        Dim txtAddress3 As TextBox = fvRow.FindControl("txtAddress3")
        Dim txtCity As TextBox = fvRow.FindControl("txtCity")
        Dim txtZIP As TextBox = fvRow.FindControl("txtZIP")
        Dim txtPhoneHome As TextBox = fvRow.FindControl("txtHomePhone")
        Dim txtPhoneAlt As TextBox = fvRow.FindControl("txtAltPhone")
        Dim lblAddress3 As Label = fvRow.FindControl("lblAddress3")
        Dim lblCity As Label = fvRow.FindControl("lblCity")
        Dim lblState As Label = fvRow.FindControl("lblState")
        Dim lblZIP As Label = fvRow.FindControl("lblZIP")
        Dim continueUpdating As Boolean
        lblUpdateMessage.Text = "Address update canceled."
        lblUpdateMessage.ForeColor = Drawing.Color.Red
        lblUpdateMessage.Visible = True
        continueUpdating = False
        ddlState.Items.Clear()
        populateStatesDDL()
        owner = Session("OriginalData")

        With owner
            If .CountryCode.ToUpper.Equals("US") Then
                If String.IsNullOrEmpty(.State) Then
                    .State = getStateBySubdivision(.Subdivision)
                End If
                If String.IsNullOrEmpty(.Subdivision) OrElse .Subdivision.Length.Equals(2) Then
                    .Subdivision = getSubdivisionByState(.State)
                End If
                If Not String.IsNullOrEmpty(.State) Then
                    ddlState.SelectedValue = .State
                ElseIf Not String.IsNullOrEmpty(.Subdivision) Then
                    ddlState.SelectedValue = .Subdivision
                End If
                txtAddress1.Text = owner.Address1
                txtAddress2.Text = owner.Address2
                txtAddress3.Text = owner.Address3
                txtCity.Text = owner.City
                txtZIP.Text = owner.PostalCode
                ddlCountry.SelectedValue = owner.CountryCode
                txtPhoneHome.Text = owner.PhoneHome
                txtPhoneAlt.Text = owner.PhoneCell
            Else
                txtAddress1.Visible = True
                txtAddress1.Text = owner.Address1
                txtAddress2.Visible = True
                txtAddress2.Text = owner.Address2
                lblAddress3.Visible = True
                txtAddress3.Visible = True
                txtAddress3.Text = owner.Address3
                txtCity.Visible = False
                lblCity.Visible = False
                lblState.Visible = False
                ddlState.Visible = False
                lblZIP.Visible = False
                txtZIP.Visible = False
                ddlCountry.SelectedValue = owner.CountryCode
                txtPhoneHome.Text = owner.PhoneHome
                txtPhoneAlt.Text = owner.PhoneCell
            End If

        End With

        ModalPopupExtender3.Hide()
    End Sub
    Protected Sub rbconfirm_CheckedChanged(sender As Object, e As EventArgs)
        If rbconfirm.Checked Then
            ModalPopupExtender3.Show()
        End If
    End Sub

    Protected Sub gvSearchResults_Sorting(sender As Object, e As GridViewSortEventArgs)
        SortDirection = If((SortDirection = "ASC"), "DESC", "ASC")
        SortColumn = e.SortExpression
        BindGrid()
    End Sub

    Protected Sub gvSearchResults_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Me.gvSearchResults.PageIndex = e.NewPageIndex
        Session("CurrentPage") = e.NewPageIndex
        BindGrid()
    End Sub
End Class
