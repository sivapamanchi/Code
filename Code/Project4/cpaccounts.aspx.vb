Imports System.Data
Imports System.Web.SessionState.HttpSessionState
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports IBM.Data.DB2.iSeries
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Net
Imports System.Threading
Imports System.Net.Mail
Imports VSSA.CPProfileServices
Imports VSSA.EditAccount
Imports VSSA.CPAccountServices
Imports VSSA.CPLoyalityServices
Imports VSSA.BoomiFundsSourceWS
Imports VSSA.ResortsService
Imports VSSA.ResortServices
Imports VSSA.PointsTrans
Imports System.Globalization
Imports VSSA.TotPointsReq
Imports VSSA.SendPoints
Imports VSSA.AcctResponse
Imports VSSA.OwnerServices
Imports VSSA.fetchresp
Partial Class cpaccounts
    Inherits VSSABaseClass
    Public owner As New BXG_Owner
    Public OwnerArvact As String
    Public Processed As Boolean = False
    Public TransactionError As String = ""
    Public ApplicationError As String = ""
    Public dtChoice As DataTable
    Public pastDue As String
    Public txtPastDue As String
    Public selectedCellIndex As Integer
    Public objaccountsList As AcctResponse.CPRetrieveAcctResponse
    Public revertpoints As Integer = 0

    Protected Sub cpaccounts_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Session("SearchParam") Is Nothing Then
            Session("SearchParam") = Nothing
        End If
        If Not Session("SearchData") Is Nothing Then
            Session("SearchData") = Nothing
        End If
        If Not IsNothing(Session("owner")) Then
            If Not IsNothing(Session("BoomiOwner")) Then
                bxgOwner = Session("BoomiOwner")
                owner = Session("owner")
                If bxgOwner.Identifier <> owner.ARVACT Then
                    Dim Ownersvc As New OwnerService()
                    Dim OwnerSvcResp As New OwnerFetchResponse()
                    OwnerSvcResp = Ownersvc.OwnerFetch(owner.ARVACT)
                    Session("BoomiOwner") = OwnerSvcResp
                    bxgOwner = Session("BoomiOwner")
                End If
                lblOwnerAccountError.Text = ""
            End If
        Else
            ApplicationError = "True"
        End If

        txtPastDue = " Due to the current status of one or more of your accounts, we cannot allow the conversion of your Bluegreen points at this time. Please call 800.456.CLUB(2582) for further assistance."
        OwnerArvact = owner.ARVACT
        Session("HistoryCount") = Session("HistoryCount") - 1
        If Not IsPostBack Then
            Session("HistoryCount") = -1
            Try
                Dim Ownersvc As New OwnerService()
                Dim OwnerSvcResp As New OwnerFetchResponse()
                OwnerSvcResp = Ownersvc.OwnerFetch(OwnerArvact)
                Session("BoomiOwner") = OwnerSvcResp
                bxgOwner = Session("BoomiOwner")

                Dim validOwner As Boolean = ValidateOwnerType()


                If Not validOwner Then
                    ApplicationError = "True"

                Else
                    getOwnerInfo()
                    LoadChoiceAccounts()
                    LoadChoiceAccountsHistory()
                    LoadPointConversionHistory()
                    populateCCDropDowns()
                    If Not (bxgOwner.Account Is Nothing) Then
                        If (Not (bxgOwner.Account.Memberships.VacationClubMembership.Points Is Nothing)) Then
                            If Not (bxgOwner.Account.Memberships.VacationClubMembership.Points.TotalAnnualPoints Is Nothing) Then
                                If Not (Session("OwnerTotalPoints") Is Nothing) Then
                                    lblAnnualPoints.Text = Session("OwnerTotalPoints")
                                    txteligiblepoints.Text = Session("OwnerTotalPoints")
                                Else
                                    lblAnnualPoints.Text = bxgOwner.Account.Memberships.VacationClubMembership.Points.TotalAnnualPoints
                                    txteligiblepoints.Text = bxgOwner.Account.Memberships.VacationClubMembership.Points.TotalAnnualPoints
                                End If
                            End If
                        End If

                    End If


                End If

                If IsNothing(Session("BXGOwner")) Then

                    ApplicationError = "True"
                    lblOwnerAccountError.Text = "We are unable to process your request."

                End If
                If Not (bxgOwner.Account Is Nothing) Then
                    If (Not bxgOwner.Account.Memberships.VacationClubMembership.Accounts Is Nothing) Then
                        If bxgOwner.Account.Memberships.VacationClubMembership.Accounts.AccountInfo(0).Legacy.MaintFeePaymentBalanceForReservation > 0 Then
                            lblAnnualPoints.Text = "0"
                            txteligiblepoints.Text = "0"
                        End If
                    End If
                End If

                If Not validOwner Then
                    ApplicationError = "True"
                    lblOwnerAccountError.Text = "This owner type/member does not qualify for the Choice Hotels Point conversion benefit."
                    lblAnnualPoints.Text = ""
                    txteligiblepoints.Text = ""
                End If


            Catch ex As Exception
                ApplicationError = "True"
                lblOwnerAccountError.Text = "We are unable to process your request."
                senderror(ex.Message.ToString(), " Failed to load the page.")
            End Try

            If ApplicationError = "True" Then
                UpdPnlConversion.Visible = False
                UpdPnlAccounts.Visible = False
                UpdPnlPointsConversionHistory.Visible = False
                UpdPnlAccountHistory.Visible = False
                Exit Sub
            End If
        End If

        bxgOwner = Session("BXGOwner")

    End Sub

    Private Sub getOwnerInfo()

        If OwnerArvact <> "" Then

            Dim ds As New DataSet
            ds = OwnerData.searchOwners(OwnerArvact, "", "", "", "")
            If ds.Tables(0).Rows.Count = 0 Then
                lblOwnerAccountError.Text = "Owner ARVACT number can't be blank"
                UpdPnlConversion.Visible = False
                UpdPnlAccounts.Visible = False
                UpdPnlPointsConversionHistory.Visible = False
                UpdPnlAccountHistory.Visible = False
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
                lblPoints.Text = "Available Points for Conversion:"
                lblRegistered.Text = Session("Registered").ToString()
                Dim iDate As String = Now().ToShortDateString()
                Dim oDate As DateTime = Convert.ToDateTime(iDate)
                If Not bxgOwner.Account.Memberships.TravelerPlusMembership Is Nothing Then
                    If Not bxgOwner.Account.Memberships.TravelerPlusMembership.TravelerPlusEligible = False Then
                        If bxgOwner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate = "" Then
                            Me.lblTPExp.Text = "NA"
                            Me.lblTPExp.ForeColor = Drawing.Color.Black
                        ElseIf Convert.ToDateTime(bxgOwner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate.ToString()) >= oDate Then
                            Me.lblTPExp.ForeColor = Drawing.Color.Green
                            Me.lblTPExp.Text = bxgOwner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate
                        ElseIf Convert.ToDateTime(bxgOwner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate.ToString()) < oDate Then
                            Me.lblTPExp.ForeColor = Drawing.Color.Red
                            Me.lblTPExp.Text = bxgOwner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate
                        End If
                    Else
                        Me.lblTPExp.Text = "NA"
                        Me.lblTPExp.ForeColor = Drawing.Color.Black
                    End If
                Else
                    Me.lblTPExp.Text = "NA"
                    Me.lblTPExp.ForeColor = Drawing.Color.Black
                End If

                Me.lbltswPersonID.Text = ds.Tables(0).Rows(0)("TSWPersonID").ToString()
                Me.lblTwsID.Visible = True

                If Not owner.TSWPersonID = 0 Then
                    lbltswPersonID.ForeColor = Drawing.Color.Green
                Else
                    lbltswPersonID.Text = "N/A"
                    lbltswPersonID.ForeColor = Drawing.Color.Red
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
            UpdPnlAccounts.Visible = False
            UpdPnlConversion.Visible = False
            UpdPnlAccountHistory.Visible = False
            lblOwnerAccountError.Visible = True
        End If

    End Sub
    Private Sub LoadEmptyTable()
        dtChoice = (New DataTable())


        ' Add four columns to the DataTable.
        dtChoice.Columns.Add("Identifier")
        dtChoice.Columns.Add("ChoicePrivilegeID")
        dtChoice.Columns.Add("FirstName")
        dtChoice.Columns.Add("LastName")
        ' and set the starting value and increment.
        dtChoice.Columns("Identifier").AutoIncrement = True
        dtChoice.Columns("Identifier").AutoIncrementSeed = 1
        dtChoice.Columns("Identifier").AutoIncrementStep = 1
        ' Set PersonID column as the primary key.
        Dim dcKeys As DataColumn() = New DataColumn(0) {}
        dcKeys(0) = dtChoice.Columns("Identifier")
        dtChoice.PrimaryKey = dcKeys


        Dim dtrows As Int16 = 0
        dtrows = dtChoice.Rows.Count
        dtChoice.Rows.Add(dtChoice.NewRow())
    End Sub
    Public Function BindingFirstName()
        If Not Session("cpAccounts") Is Nothing Then
            objaccountsList = Session("cpAccounts")
        End If
        Dim Ret As String = ""
        Try
            For value As Integer = 0 To objaccountsList.Accounts.Count - 1
                If GridViewCHCAccounts.Rows.Count = 0 Then
                    Ret = objaccountsList.Accounts(value).Person.FirstName
                    value += 1
                Else
                    Ret = objaccountsList.Accounts(value).Person.FirstName
                End If
            Next
        Catch
        End Try
        Return Ret
    End Function
    Public Function BindingLastName()
        If Not Session("cpAccounts") Is Nothing Then
            objaccountsList = Session("cpAccounts")
        End If
        Dim Ret As String = ""
        Try
            For value As Integer = 0 To objaccountsList.Accounts.Count - 1
                If GridViewCHCAccounts.Rows.Count = 0 Then
                    Ret = objaccountsList.Accounts(value).Person.LastName
                    value += 1
                Else
                    Ret = objaccountsList.Accounts(value).Person.LastName
                End If
            Next
        Catch
        End Try
        Return Ret
    End Function
    Private Sub LoadChoiceAccounts()
        Try
            Dim acctlist As New AccountService()
            Dim accoutsList = acctlist.RetrieveAcct(Session("arvact"))
            Dim dt As DataTable = Nothing
            objaccountsList = accoutsList
            Session("cpAccounts") = accoutsList
            If accoutsList.Accounts Is Nothing Then
                LoadEmptyTable()
                GridViewCHCAccounts.DataSource = dtChoice
                GridViewCHCAccounts.DataBind()
                GridViewCHCAccounts.Rows(0).Visible = False
            Else
                If accoutsList.Accounts.Count >= 2 Then
                    GridViewCHCAccounts.ShowFooter = False
                Else
                    GridViewCHCAccounts.ShowFooter = True
                End If
                GridViewCHCAccounts.DataSource = accoutsList.Accounts.ToList()
                GridViewCHCAccounts.DataBind()
            End If
            Dim liOwnerCheckingIn As New ListItem
            liOwnerCheckingIn.Text = "Please select Owner name"
            liOwnerCheckingIn.Value = String.Empty
            ddlOwner.Items.Add(liOwnerCheckingIn)

            Dim i As Int32 = 0
            If Not (accoutsList.Accounts Is Nothing) Then
                For value As Integer = 0 To accoutsList.Accounts.Count - 1
                    liOwnerCheckingIn = New ListItem
                    liOwnerCheckingIn = New ListItem
                    liOwnerCheckingIn.Text = accoutsList.Accounts(value).Person.LastName & ", " & accoutsList.Accounts(value).Person.FirstName
                    liOwnerCheckingIn.Value = accoutsList.Accounts(value).ChoicePrivilegeID
                    ddlOwner.Items.Add(liOwnerCheckingIn)
                Next
            End If


            Dim liOwnerCheckingIn1 As New ListItem
            liOwnerCheckingIn1.Text = "Other"
            liOwnerCheckingIn1.Value = "Other"
            ddlOwner.Items.Add(liOwnerCheckingIn1)
            If Not (accoutsList.Accounts Is Nothing) Then
                Session("Accounts") = accoutsList.Accounts.ToList()
            End If
        Catch ex As Exception
            ApplicationError = "True"
            senderror(ex.Message.ToString(), "Failed to get Choice accounts from services.")
        End Try



    End Sub
    Private Sub LoadPointConversionHistory()
        Dim SqlConnection As Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings("VSSA.bxgwebDBConnection"))
        Dim SqlCommand As New System.Data.SqlClient.SqlCommand
        Dim da As New SqlDataAdapter
        Dim dtTable As New DataTable

        Try
            SqlCommand.CommandText = "uspGetOwnerPointsConversionHistory"
            SqlCommand.CommandType = System.Data.CommandType.StoredProcedure
            SqlCommand.Parameters.Clear()
            SqlCommand.Connection = SqlConnection
            'SqlCommand.Connection.Open()

            SqlCommand.Parameters.Add(New System.Data.SqlClient.SqlParameter("@arvact", System.Data.SqlDbType.VarChar, 6)).Value = owner.ARVACT.ToString()

            da.SelectCommand = SqlCommand
            da.Fill(dtTable)

            GrdConversion.DataSource = dtTable
            GrdConversion.DataBind()

            If dtTable.Rows.Count = 0 Then
                UpdPnlPointsConversionHistory.Visible = False
            End If

        Catch ex As Exception
            TransactionError = ex.Message.ToString()
            senderror(ex.Message.ToString(), " Failed to get Owner Points Conversion History")
        Finally
            If SqlConnection.State = ConnectionState.Open Then
                SqlConnection.Close()
            End If
        End Try
    End Sub

    Private Sub LoadChoiceAccountsHistory()
        Try
            Dim acctlist As New AccountService
            Dim accthistlist = acctlist.RetrieveAcctHist(Session("arvact"))
            If Not (accthistlist.Accounts Is Nothing) Then
                If (accthistlist.Accounts.Count = 1) Then
                    If (accthistlist.Accounts(0).PrevChoicePrivilegeID = Nothing) Then
                        accthistlist.Accounts(0).PrevChoicePrivilegeID = DBNull.Value.ToString()
                    End If
                    rptAccountHistory.DataSource = accthistlist.Accounts.ToList()
                Else
                    rptAccountHistory.DataSource = accthistlist.Accounts.ToList()
                End If
                rptAccountHistory.DataBind()
            End If


        Catch ex As Exception
            Dim xc As String = ex.Message.ToString()
        End Try


    End Sub

    Protected Sub GridViewCHCAccounts_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewCHCAccounts.RowCommand

        If e.CommandName.Equals("Insert") Then
            Try
                If Processed = True Then
                    Exit Sub
                End If
                Dim validateProfileAccount As Boolean = False
                Dim modifyProfileAccount As Boolean = False
                LblErrMessage.Text = ""
                Dim validFlag As Boolean
                validFlag = False
                Dim _FirstName As TextBox = CType(GridViewCHCAccounts.FooterRow.FindControl("TxtInsFirstName"), TextBox)
                Dim _LastName As TextBox = CType(GridViewCHCAccounts.FooterRow.FindControl("TxtInsLastName"), TextBox)
                Dim _ChoicePrivilegeID As TextBox = CType(GridViewCHCAccounts.FooterRow.FindControl("TxtInsChoicePrivilegeID"), TextBox)


                'Dim account As New BXG.BGO.Choice.PrivilegesAccounts.Model.Account()
                Dim account As New EditedAccount()
                Dim person As EditAccount.Person = New EditAccount.Person()
                person.FirstName = _FirstName.Text
                Session("FirstName") = _FirstName.Text
                person.LastName = _LastName.Text
                Session("LastName") = _LastName.Text
                account.Person = New EditAccount.Person() {person}.ToList()
                account.OwnerID = owner.ARVACT

                account.ChoicePrivilegeID = _ChoicePrivilegeID.Text.Trim
                account.Requester = Environment.UserName.ToString()
                validFlag = ValidateRequest(account)

                If validFlag Then
                    LblErrMessage.Text = ""
                Else
                    Return
                End If


                'Validate account with choice
                validateProfileAccount = ChoiceProfileSearch(person.LastName, account.ChoicePrivilegeID)


                If Not validateProfileAccount Then
                    LblErrMessage.Text = "The Choice Privileges member number you entered is incorrect or does not match the name that was provided.  For U.S. based customers, your Choice Privileges member number will typically begin with three letters followed by four to six numbers. (ex. ABC12345)  Please check the name/number and try again or visit www.choiceprivileges.com to find your member number."
                    Return
                End If
                If Not (bxgOwner.Account.Memberships.VacationClubMembership.Membership Is Nothing) Then
                    If bxgOwner.Account.Memberships.VacationClubMembership.Membership.TravelerPlusEligible = True Then
                        Dim Tpexpiration As DateTime = bxgOwner.Account.Memberships.VacationClubMembership.Membership.MembershipExpirationDate
                        If Date.Compare(Tpexpiration, DateTime.Now) >= 0 Then

                            account.TravelerplusMember = True
                        Else
                            account.TravelerplusMember = False
                        End If
                    Else
                        account.TravelerplusMember = False
                    End If
                End If

                modifyProfileAccount = ModifyProfile(person.LastName, account.ChoicePrivilegeID, account.OwnerID, account.TravelerplusMember)

                If Not modifyProfileAccount Then
                    LblErrMessage.Text = "The Choice Privileges member number you entered is incorrect or does not match the name that was provided.  For U.S. based customers, your Choice Privileges member number will typically begin with three letters followed by four to six numbers. (ex. ABC12345)  Please check the name/number and try again or visit www.choiceprivileges.com to find your member number."
                    Return
                End If

                Dim result As Int32 = InsertAccount(account)



                If result = 0 Then
                    LblErrMessage.Text = "We are unable to process your request please try later..<br>"
                End If



                Processed = True

            Catch ex As Exception
                LblErrMessage.Text = "We are unable to process your request please try later..<br>"
                senderror(ex.Message.ToString(), "Failed to insert new account.")
            End Try



            If LblErrMessage.Text = "" Then
                Response.Redirect("cpaccounts.aspx")
            End If

        End If
        If e.CommandName.Equals("Cancel") Then
            LblErrMessage.Text = ""
            If Processed = True Then
                Exit Sub
            End If

            If Not IsNothing(Session("Accounts")) Then '
                GridViewCHCAccounts.EditIndex = -1
                Dim accoutsList = Session("Accounts")
                objaccountsList = Session("objAccountsList")
                If accoutsList.Count >= 2 Then
                    GridViewCHCAccounts.ShowFooter = False
                Else
                    GridViewCHCAccounts.ShowFooter = True
                End If
                GridViewCHCAccounts.DataSource = Session("Accounts")
                GridViewCHCAccounts.DataBind()
            Else
                LoadEmptyTable()
                GridViewCHCAccounts.ShowFooter = True
                GridViewCHCAccounts.DataSource = dtChoice
                GridViewCHCAccounts.DataBind()
                GridViewCHCAccounts.Rows(0).Visible = False
            End If

            Processed = True
        End If
    End Sub

    Protected Sub GridViewCHCAccounts_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridViewCHCAccounts.RowCancelingEdit

        'If Not IsNothing(Session("Accounts")) Then '
        '    GridViewCHCAccounts.EditIndex = -1
        '    Dim accoutsList = Session("Accounts")
        '    If accoutsList.Count >= 2 Then
        '        GridViewCHCAccounts.ShowFooter = False
        '    Else
        '        GridViewCHCAccounts.ShowFooter = True
        '    End If
        '    GridViewCHCAccounts.DataSource = Session("Accounts")
        '    GridViewCHCAccounts.DataBind()
        'End If


    End Sub

    Protected Sub GridViewCHCAccounts_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewCHCAccounts.RowDeleting
        Try
            Dim validFlag As Boolean = False
            'Dim account As New BXG.BGO.Choice.PrivilegesAccounts.Model.Account()
            Dim account As New EditAccount.EditedAccount()
            Try
                If Processed = True Then
                    Exit Sub
                End If
                'Dim gvRow As GridViewRow = CType(CType(sender, Control).Parent.Parent,  _
                '                        GridViewRow)
                'Dim index As Integer = gvRow.RowIndex
                GridViewCHCAccounts.EditIndex = e.RowIndex
                Dim row = GridViewCHCAccounts.EditIndex
                'Dim row = GridViewCHCAccounts.Rows(GridViewCHCAccounts.EditIndex)
                Dim id As Integer = Convert.ToInt32(GridViewCHCAccounts.DataKeys(row).Values(0))
                'Dim row As GridViewRow = Convert.ToInt32(GridViewCHCAccounts.DataKeys(row.RowIndex).Values(0)) ' GridViewCHCAccounts.Rows(e.RowIndex)
                Dim _rowID = Convert.ToInt64(id)

                'account.Id = _rowID
                account.Identifier = id
                account.OwnerID = owner.ARVACT
                account.Requester = Environment.UserName.ToString()

            Catch ex As Exception
                senderror(ex.Message.ToString(), "Failed delete choice account.")
            End Try

            Dim result = DisableAccount(account)
            If Not result Then
                LblErrMessage.Text = "We are unable to process your request please try later..<br>"
            Else
                Processed = True
                LoadChoiceAccounts()
            End If
        Catch ex As Exception
            LblErrMessage.Text = "We are unable to process your request please try later..<br>"
            senderror(ex.Message.ToString(), "Failed delete choice account.")
        End Try

        If LblErrMessage.Text = "" Then
            Response.Redirect("cpaccounts.aspx")
        End If

    End Sub

    Protected Sub GridViewCHCAccounts_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridViewCHCAccounts.RowEditing

        'If Processed = True Then
        '    Exit Sub
        'End If

        GridViewCHCAccounts.EditIndex = e.NewEditIndex
        GridViewCHCAccounts.DataSource = Session("Accounts")
        GridViewCHCAccounts.DataBind()

        GridViewCHCAccounts.FooterRow.Visible = False

        For Each row As GridViewRow In GridViewCHCAccounts.Rows
            If row.RowIndex <> e.NewEditIndex Then
                row.Visible = False
            End If
        Next


        'GridViewCHCAccounts.ShowFooter = False



    End Sub

    Protected Sub GridViewCHCAccounts_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewCHCAccounts.RowUpdating

        Dim validateProfileAccount As Boolean = False
        Dim validFlag As Boolean = False
        Dim account As New EditAccount.EditedAccount()
        Dim person As EditAccount.Person = New EditAccount.Person()
        Try
            If Processed = True Then
                Exit Sub
            End If
            Dim row = GridViewCHCAccounts.Rows(GridViewCHCAccounts.EditIndex)
            Dim id As Integer = Convert.ToInt32(GridViewCHCAccounts.DataKeys(row.RowIndex).Values(0))
            Dim _FirstName As TextBox = CType(row.FindControl("TxtEdtFirstName"), TextBox)

            Dim _LastName As TextBox = CType(row.FindControl("TxtEdtLastName"), TextBox)
            Dim _ChoicePrivilegeID As TextBox = CType(row.FindControl("TxtEdtChoicePrivilegeID"), TextBox)
            account.Identifier = id
            account.OwnerID = owner.ARVACT
            person.FirstName = _FirstName.Text
            person.LastName = _LastName.Text
            Session("FirstName") = _FirstName.Text
            Session("LastName") = _LastName.Text
            account.Person = New EditAccount.Person() {person}.ToList()
            account.ChoicePrivilegeID = _ChoicePrivilegeID.Text.Trim
            account.Requester = Environment.UserName.ToString()
            If Not (bxgOwner.Account.Memberships.VacationClubMembership.Membership Is Nothing) Then
                If bxgOwner.Account.Memberships.VacationClubMembership.Membership.TravelerPlusEligible = True Then
                    Dim Tpexpiration As DateTime = bxgOwner.Account.Memberships.VacationClubMembership.Membership.MembershipExpirationDate
                    If Date.Compare(Tpexpiration, DateTime.Now) >= 0 Then

                        account.TravelerplusMember = True
                    Else
                        account.TravelerplusMember = False
                    End If
                Else
                    account.TravelerplusMember = False
                End If
            End If
            validFlag = ValidateRequest(account)
        Catch ex As Exception
            LblErrMessage.Text = "We are unable to process your request please try later..<br>"
            senderror(ex.Message.ToString(), "Failed to update choice account.")
        End Try
        If validFlag Then
            LblErrMessage.Text = ""
        Else
            Return
        End If
        validateProfileAccount = ModifyProfile(person.LastName, account.ChoicePrivilegeID, account.OwnerID, account.TravelerplusMember)
        If Not validateProfileAccount Then
            LblErrMessage.Text = "The Choice Privileges member number you entered is incorrect or does not match the name that was provided.  For U.S. based customers, your Choice Privileges member number will typically begin with three letters followed by four to six numbers. (ex. ABC12345)  Please check the name/number and try again or visit www.choiceprivileges.com to find your member number."
            Return
        End If

        Dim result As Int32 = UpdateAccount(account)

        If result = 0 Then
            LblErrMessage.Text = "We are unable to process your request please try later..<br>"
        Else
            Processed = True
            GridViewCHCAccounts.EditIndex = -1
            Response.Redirect("cpaccounts.aspx")
        End If
    End Sub

    Public Function InsertAccount(ByVal account As EditAccount.EditedAccount) As Boolean
        Dim validate As Boolean = False
        Dim result As Int32 = 0
        Dim insertrequest As New EditAccount.EditedAccount()
        Dim person As EditAccount.Person = New EditAccount.Person()
        insertrequest.Person = New EditAccount.Person() {person}.ToList()
        person.FirstName = Session("FirstName").ToString()
        person.LastName = Session("LastName").ToString()
        insertrequest.ChoicePrivilegeID = account.ChoicePrivilegeID
        insertrequest.OwnerID = account.OwnerID
        insertrequest.Requester = account.Requester
        Try
            Dim insaccount As New AccountService
            Dim insResult As InsertAcctResponse.InsertAcctResponse = Nothing
            insResult = insaccount.InsertAcct(insertrequest)
            If (insResult.Success = True) Then
                result = 1
            End If
        Catch ex As Exception
            result = 0
            senderror(ex.Message.ToString(), "Failed to insert choice account.")
        End Try
        Return result
    End Function

    Public Function UpdateAccount(ByVal account As EditAccount.EditedAccount) As Boolean
        Dim validate As Boolean = False
        Dim result As Int32 = 0
        Dim updateRequest As New EditAccount.EditedAccount()
        Dim person As EditAccount.Person = New EditAccount.Person()
        updateRequest.Person = New EditAccount.Person() {person}.ToList()
        person.FirstName = Session("FirstName").ToString()
        person.LastName = Session("LastName").ToString()
        updateRequest.ChoicePrivilegeID = account.ChoicePrivilegeID
        updateRequest.OwnerID = account.OwnerID
        updateRequest.Identifier = account.Identifier
        updateRequest.Requester = account.Requester
        Try
            Dim updateacct As New AccountService
            Dim upResult As UpAcctResponse.UpdateAcctResponse = Nothing
            upResult = updateacct.UpdateAcct(updateRequest)
            If (upResult.Success = True) Then
                result = 1
            End If
        Catch ex As Exception
            result = 0
            senderror(ex.Message.ToString(), "Failed to update choice account.")
        Finally
        End Try
        Return result
    End Function

    Public Function DisableAccount(ByVal account As EditedAccount) As Boolean
        Dim validate As Boolean = False
        Dim disablerequest As New EditedAccount()
        disablerequest.Identifier = account.Identifier
        disablerequest.OwnerID = account.OwnerID
        disablerequest.Requester = account.Requester
        Dim disable As New CPAccountServices.AccountService
        Dim result = disable.DisableAcct(disablerequest)
        If result IsNot Nothing AndAlso result.Success = "true" Then
            validate = True
        End If
        Return validate
    End Function

    Public Function ValidateRequest(ByVal account As EditAccount.EditedAccount) As Boolean
        Dim person As EditAccount.Person = New EditAccount.Person()
        person.FirstName = Session("FirstName").ToString()
        person.LastName = Session("LastName").ToString()
        account.Person = New EditAccount.Person() {person}.ToList()
        Dim validFlag As Boolean = False
        'Dim validateProfileAccount As Boolean = False
        LblErrMessage.Text = ""


        If person.FirstName.Trim.Length = 0 Then
            LblErrMessage.Text = "First Name value must not be blank.<br>"
        End If

        If person.LastName.Length = 0 Then
            LblErrMessage.Text &= "Last Name value must not be blank.<br>"
        End If

        If account.ChoicePrivilegeID.Length = 0 Then
            LblErrMessage.Text &= "Choice Privilege ID value must not be blank.<br>"
        End If

        If LblErrMessage.Text.Length > 0 Then
            validFlag = False
        Else
            validFlag = True
        End If
        Processed = True

        If validFlag Then
            validFlag = ValidateDuplicateChoiceAccount(account.ChoicePrivilegeID)

        End If


        Return validFlag

    End Function

    Public Function ValidateDuplicateChoiceAccount(ByVal ChoiceId As String) As Boolean
        Dim result As Boolean = True
        For Each row As GridViewRow In GridViewCHCAccounts.Rows
            'read the label   

            Dim _ChoicePrivilegeID As Label = DirectCast(row.FindControl("LblChoicePrivilegeID"), Label)
            If Not IsNothing(_ChoicePrivilegeID) Then
                If ChoiceId = _ChoicePrivilegeID.Text Then
                    LblErrMessage.Text &= "The Choice Privileges member ID you entered matches an ID that is already stored on your account.  Please check the number or enter a different member ID.<br>"
                    result = False
                End If
            End If

        Next

        Return result
    End Function
    Public Function ChoiceProfileSearch(ByVal LastName As String, ByVal ChoiceAcctId As String) As Boolean
        Dim response As Boolean = False
        Try
            Dim result As SearchResponse.ProfileSearchResponse = Nothing
            Dim searchRequest As New EditAccount.EditedAccount()
            Dim person As EditAccount.Person = New EditAccount.Person()
            searchRequest.Person = New EditAccount.Person() {person}.ToList()
            person.LastName = LastName
            searchRequest.ChoicePrivilegeID = ChoiceAcctId
            Dim Profsearch As New ProfileService
            result = Profsearch.SearchProfile(searchRequest)
            If result.People.Count > 0 Then
                response = True
            Else
                response = False
            End If
        Catch ex As Exception
            senderror(ex.Message.ToString(), "Failed to search choice profile account.")
        End Try
        Return response
    End Function
    Public Function ModifyProfile(ByVal LastName As String, ByVal ChoiceAcctId As String, ByVal OwnerID As String, ByVal TravelerplusMember As Boolean) As Boolean
        Dim response As Boolean = False
        Try
            Dim modifyRequest As New EditAccount.EditedAccount()
            Dim person As EditAccount.Person = New EditAccount.Person()
            modifyRequest.Person = New EditAccount.Person() {person}.ToList()
            person.LastName = LastName
            modifyRequest.ChoicePrivilegeID = ChoiceAcctId
            modifyRequest.OwnerID = Session("arvact").ToString()
            If Not (bxgOwner.Account.Memberships.VacationClubMembership.Membership Is Nothing) Then
                If bxgOwner.Account.Memberships.VacationClubMembership.Membership.TravelerPlusEligible = True Then
                    Dim Tpexpiration As DateTime = bxgOwner.Account.Memberships.TravelerPlusMembership.Membership.MembershipExpirationDate
                    If Date.Compare(Tpexpiration, DateTime.Now) >= 0 Then
                        modifyRequest.TravelerplusMember = True
                    Else
                        modifyRequest.TravelerplusMember = False
                    End If
                Else
                    modifyRequest.TravelerplusMember = False
                End If
            End If
            Dim Profsearch As New ProfileService
            Dim result = Profsearch.ModifyProfile(modifyRequest)
            If (result IsNot Nothing AndAlso result.Success = "true") Then
                response = True
            Else
                response = False
            End If

        Catch ex As Exception
            senderror(ex.Message.ToString(), "Failed to search Choice Profile Account.")
        End Try
        Return response
    End Function
    Private Sub getAccountInfo()


        Try
            Dim liOwnerCheckingIn As New ListItem
            liOwnerCheckingIn.Text = "Please select Owner name"
            liOwnerCheckingIn.Value = String.Empty
            ddlOwner.Items.Add(liOwnerCheckingIn)

            Dim acctlist As New AccountService
            Dim accoutsList = acctlist.RetrieveAcct(Session("arvact"))
            Dim i As Int32 = 0
            For value As Integer = 0 To accoutsList.Accounts.Count - 1
                liOwnerCheckingIn = New ListItem
                liOwnerCheckingIn = New ListItem
                liOwnerCheckingIn.Text = accoutsList.Accounts(value).Person.LastName & ", " + accoutsList.Accounts(value).Person.FirstName
                liOwnerCheckingIn.Value = accoutsList.Accounts(value).ChoicePrivilegeID
                ddlOwner.Items.Add(liOwnerCheckingIn)
            Next

            Dim liOwnerCheckingIn1 As New ListItem
            liOwnerCheckingIn1.Text = "Other"
            liOwnerCheckingIn1.Value = "Other"
            ddlOwner.Items.Add(liOwnerCheckingIn1)



        Catch ex As Exception
            senderror(ex.Message.ToString(), "Failed to owner account information.")
        Finally

        End Try


    End Sub


    Protected Sub ddlOwner_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOwner.SelectedIndexChanged
        If ddlOwner.SelectedValue = "Other" Then
            PnlOtherId.Visible = True
            PnlChoiceId.Visible = False
        ElseIf ddlOwner.SelectedIndex > 0 Then
            LblChoiceId.Text = ddlOwner.SelectedValue
            txtFirstName.Text = ""
            txtLastName.Text = ""
            txtChoiceId.Text = ""
            PnlOtherId.Visible = False
            PnlChoiceId.Visible = True
        ElseIf ddlOwner.SelectedValue = String.Empty Then
            LblChoiceId.Text = ""
            txtFirstName.Text = ""
            txtLastName.Text = ""
            txtChoiceId.Text = ""
            PnlOtherId.Visible = False
            PnlChoiceId.Visible = True
        Else
            ' LblChoiceId.Text = ""
            PnlOtherId.Visible = False
            PnlChoiceId.Visible = True
        End If
    End Sub


    Protected Sub txtBvcToChcPts_TextChanged(sender As Object, e As System.EventArgs) Handles txtBvcToChcPts.TextChanged
        lblError.Text = ""
        imgAlert.Visible = False
        txtChcPts.Text = ""


        Dim regex As New Regex("^[0-9]+$")

        If bxgOwner.Account.Memberships.VacationClubMembership.Legacy.MaintFeePaymentBalanceForReservation > 0 Then
            pnlError.Visible = True
            lblError.Visible = True
            imgAlert.Visible = True
            lblError.Text = txtPastDue
            Exit Sub
        End If


        If Not IsNumeric(txtBvcToChcPts.Text) Then
            pnlError.Visible = True
            lblError.Visible = True
            imgAlert.Visible = True
            lblError.Visible = True
            lblError.Text = "Invalid Entry: Number of Points to Convert value may not contain any commas or decimals and must be greater than 0."
        ElseIf Not CheckIfInteger(txtBvcToChcPts.Text) Then
            pnlError.Visible = True
            lblError.Visible = True
            imgAlert.Visible = True
            lblError.Visible = True
            lblError.Text = "Invalid Entry: Number of Points to Convert value may not contain any commas or decimals and must be greater than 0."
        ElseIf Convert.ToInt64(txtBvcToChcPts.Text) < 1 Then
            pnlError.Visible = True
            imgAlert.Visible = True
            lblError.Visible = True
            lblError.Text = "Invalid Entry: Number of Points to Convert value may not contain any commas or decimals and must be greater than 0."


        ElseIf Convert.ToInt64(txteligiblepoints.Text) < 1 Then
            pnlError.Visible = True
            imgAlert.Visible = True
            lblError.Visible = True
            lblError.Text = "Invalid Entry: Number of Points to Convert value may not contain any commas or decimals and must be greater than 0."
        ElseIf Convert.ToInt64(txtBvcToChcPts.Text) > Convert.ToInt64(txteligiblepoints.Text) Then
            pnlError.Visible = True
            imgAlert.Visible = True
            lblError.Visible = True
            lblError.Text = "Your request exceeds the number of eligible Bluegreen Points on your account."




        Else
            If Not regex.IsMatch(txtBvcToChcPts.Text) Then
                pnlError.Visible = True
                imgAlert.Visible = True
                lblError.Visible = True
                lblError.Text = "Invalid entry"
                Exit Sub
            End If
            txtBvcToChcPts.Text = txtBvcToChcPts.Text.ToString.TrimStart("0")
            Dim SqlConnection As Data.SqlClient.SqlConnection = New SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings("SamplerPlusConnString"))

            Dim SqlCommand As New SqlClient.SqlCommand

            Try

                SqlCommand.CommandText = "uspChoiceUpdateConversionRates"
                SqlCommand.CommandType = CommandType.StoredProcedure
                SqlCommand.Connection = SqlConnection

                SqlCommand.Parameters.Add(New SqlParameter("RateAbove5000", 0))
                SqlCommand.Parameters.Add(New SqlParameter("RateBelow5000", 0))
                SqlCommand.Parameters.Add(New SqlParameter("AdminUserID", ""))
                SqlCommand.Parameters.Add(New SqlParameter("Active", ""))
                SqlCommand.Parameters.Add(New SqlParameter("action", "select"))

                Dim Results As SqlDataReader

                SqlCommand.Connection.Open()
                Results = SqlCommand.ExecuteReader(CommandBehavior.CloseConnection)

                While Results.Read()
                    If txtBvcToChcPts.Text >= 5000 Then
                        txtChcPts.Text = Convert.ToInt64(txtBvcToChcPts.Text) * Results("RateAbove5000")
                    End If
                    If txtBvcToChcPts.Text < 5000 Then
                        txtChcPts.Text = Convert.ToInt64(txtBvcToChcPts.Text) * Results("RateBelow5000")
                    End If

                End While
            Catch ex As Exception
                txtChcPts.Text = 0
                Me.senderror(ex.Message.ToString(), "Failed to convert  BXG points to Choice points. ")
            Finally
                SqlConnection.Close()
            End Try

        End If

        txtNameOnCard.Focus()
    End Sub

    Public Function CheckIfInteger(number As String) As Boolean
        Dim intOutput As Integer = 0
        If Int32.TryParse(number, intOutput) Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub btnCCsubmit_Click(sender As Object, e As System.EventArgs) Handles btnCCsubmit.Click
        lblError.Text = ""
        If Not Session("Error") Is Nothing Then
            Session("Error") = ""
        End If
        Dim choiceAccountId As String = ""
        Dim choiceAccountLastName As String = ""
        Dim choiceAccountFirstName As String = ""
        Dim PointsDeductStartTime As DateTime = Nothing
        Dim PointsDeductEndTime As DateTime = Nothing
        Dim validateChoiceAccount As Boolean = False

        If validateForm() Then

            If ddlOwner.SelectedValue = "Other" Then
                choiceAccountFirstName = txtFirstName.Text
                choiceAccountLastName = txtLastName.Text
                choiceAccountId = txtChoiceId.Text
                validateChoiceAccount = ChoiceProfileSearch(txtLastName.Text, choiceAccountId)
            Else
                Dim name As String() = ddlOwner.SelectedItem.Text.Split(New Char() {","c})
                choiceAccountFirstName = name(1)
                choiceAccountLastName = name(0)
                choiceAccountId = LblChoiceId.Text
                validateChoiceAccount = ChoiceProfileSearch(choiceAccountLastName, choiceAccountId)
            End If
            If Not validateChoiceAccount Then
                lblError.Text &= "<br/><font color=red>The Choice Privileges member number you entered is incorrect or does not match the name that was provided.  For U.S. based customers, your Choice Privileges member number will typically begin with three letters followed by four to six numbers. (ex. ABC12345)  Please check the name/number and try again or visit www.choiceprivileges.com to find your member number.</font>"
                Session("Error") = lblError.Text
                GoTo IfError
            End If

            Try
                PointsDeductStartTime = DateTime.Now
                Dim GetPointsResponse As String = GetTotalPoints()
                If Not GetPointsResponse = "Success" Then
                    Session("ProcessError") = "True"
                    TransactionError &= "AS400"
                    lblError.Text &= "<br/><font color=red>Failed to get points from owner account process.</font>"
                    Session("Error") = lblError.Text
                    GoTo IfError
                End If
            Catch ex As Exception
                senderror(ex.Message.ToString(), "Failed to get points from owner account process.")
            End Try

            Dim points As New SendPoints.Points
            revertpoints += 1
            points.ChoicePrivilegeID = choiceAccountId
            Dim orders As New SendPoints.Order
            orders.ProductType = "BLUGRNBAS"
            orders.Points = txtChcPts.Text
            Dim person As New SendPoints.Person
            If ddlOwner.SelectedValue = "Other" Then
                person.LastName = choiceAccountLastName
            Else
                Dim name As String() = ddlOwner.SelectedItem.Text.Split(New Char() {","c})
                choiceAccountFirstName = name(1)
                choiceAccountLastName = name(0)
                person.LastName = choiceAccountLastName
            End If
            Try
                Dim SendPointsResults As String = SendPoints(points.ChoicePrivilegeID, orders.Points, person.LastName, orders.ProductType)
                If Not SendPointsResults = "Success" Then
                    Session("ProcessError") = "True"
                    TransactionError &= "Choice"
                    TransactionError &= "AS400"
                    lblError.Text = "<font color=red>Failed in sending points to Choice.</font>"
                    Session("Error") = lblError.Text
                    GoTo IfError
                Else
                    revertpoints += 1
                End If
            Catch ex As Exception
                senderror(ex.Message.ToString(), "Failed in sending points to Choice.")
            End Try

            If validateCC() Then
                Try
                    Dim expDate As String = Me.ddlExpMonth.SelectedValue & Me.ddlExpYear.SelectedValue
                    Dim objPayment As New paymentdetail(ddlCCtype.SelectedValue, txtBxCCNumber.Text, expDate, ddlAmount.SelectedValue, "", bxgOwner.People(0).FirstName & "  " & bxgOwner.People(0).LastName)
                    'CC process
                    Dim ccConfirm As paymentdetail = SubmitPayment(objPayment)
                    If Trim(ccConfirm.CONFNUMBER) = "" Then
                        Try
                            Dim SendPointsResults As String = SendPoints(points.ChoicePrivilegeID, orders.Points, person.LastName, orders.ProductType)
                            If Not SendPointsResults = "Success" Then
                                Session("ProcessError") = "True"
                                TransactionError &= "Choice"
                                TransactionError &= "AS400"
                                lblError.Text &= "<br/><font color=red>Failed in sending points to Choice.</font>"
                                Session("Error") = lblError.Text
                            Else
                                lblError.Text = "<font color=red>Error processing your payment, please contact customer service at 800.459.1597</font>"
                                Session("Error") = lblError.Text
                                revertpoints = 0
                            End If
                            GoTo IfError
                        Catch ex As Exception
                            senderror(ex.Message.ToString(), "Failed to send points to Choice.")
                        End Try
                    ElseIf ccConfirm.CONFNUMBER.Length > 3 Then
                        Session("authcode") = ccConfirm.CONFNUMBER
                        Try
                            PointsDeductStartTime = DateTime.Now
                            Dim GetPointsResponse As String = deductPoints()
                            If Not GetPointsResponse = "Success" Then
                                Session("ProcessError") = "True"
                                TransactionError &= "AS400"
                                lblError.Text &= "<br/><font color=red>Failed at deduct points from owner account process.</font>"
                                Session("Error") = lblError.Text
                                GoTo IfError
                            End If
                        Catch ex As Exception
                            senderror(ex.Message.ToString(), "Failed at deduct points from owner account process.")
                        End Try
                        Thread.Sleep(ConfigurationManager.AppSettings("PointDeductPageReloadTime"))
                        PointsDeductEndTime = DateTime.Now
                        If ConfigurationManager.AppSettings("VerifyLatestPoints") = "1" Then
                            Dim VerifyPointsResults As String = VerifyPointsAfterDeduction(PointsDeductStartTime, PointsDeductEndTime)
                            If Not VerifyPointsResults = "Success" Then
                                Session("ProcessError") = "True"
                                TransactionError &= "Choice"
                                TransactionError &= "AS400"
                                lblError.Text &= "<br/><font color=red>Failed to verify the points after deduction.</font>"
                                Session("Error") = lblError.Text
                                GoTo IfError
                            End If
                        End If
                        Dim comments As String = "  "
                        comments = "Conv " & txtBvcToChcPts.Text & " BG Pts to " & txtChcPts.Text & " Choice Pts. CP ID " & choiceAccountId & ". "
                        Try
                            Dim commentsResultsOne As Boolean = insertComments(comments)
                            If Not commentsResultsOne Then
                                Session("ProcessError") = "True"
                                TransactionError &= "AS400"
                                lblError.Text &= "<br/><font color=red>Failed at insert comments in AS400.</font>"
                                Session("Error") = lblError.Text
                                GoTo IfError
                            End If
                        Catch ex As Exception
                            senderror(ex.Message.ToString(), "Failed at insert comments in AS400.")
                            Session("ProcessError") = "True"
                            TransactionError &= "AS400"
                            lblError.Text &= "<br/><font color=red>Failed at insert comments in AS400.</font>"
                            Session("Error") = lblError.Text
                            GoTo IfError
                        End Try
                        comments = "Conv pts Amount " & ddlAmount.SelectedValue & ". Auth Code " & Session("authcode")
                        Try
                            'write cooments at owner account
                            Dim commentsResultsTwo As Boolean = insertComments(comments)
                            If Not commentsResultsTwo Then
                                Session("ProcessError") = "True"
                                TransactionError &= "AS400"
                                lblError.Text &= "<br/><font color=red>Failed at insert comments in AS400.</font>"
                                Session("Error") = lblError.Text
                                GoTo IfError
                            End If
                        Catch ex As Exception
                            senderror(ex.Message.ToString(), "Failed at insert comments in AS400.")
                            Session("ProcessError") = "True"
                            TransactionError &= "AS400"
                            lblError.Text &= "<br/><font color=red>Failed at insert comments in AS400.</font>"
                            Session("Error") = lblError.Text
                            GoTo IfError
                        End Try
                        'write transaction in SQL for reports
                        Dim LogResult As Boolean = TransactionLog(choiceAccountId, choiceAccountFirstName, choiceAccountLastName)
                        'send points convert confirmation email to owner
                        SendConversionPointConfirmation(choiceAccountId, choiceAccountFirstName, choiceAccountLastName)
                        LblOwnerId.Text = Session("arvact").ToString()
                        LblBvcPoints.Text = txtBvcToChcPts.Text
                        LblChcId.Text = choiceAccountId
                        LblName.Text = choiceAccountFirstName & " " & choiceAccountLastName
                        LblChcPoints.Text = txtChcPts.Text
                        LblPay.Text = ddlAmount.SelectedValue
                        LblAuthCode.Text = ccConfirm.CONFNUMBER
                        ModalPopupExtender1.Show()
                    End If
                Catch ex As Exception
                    senderror(ex.Message.ToString(), "Failed at button submit cc process.")
                End Try
            End If
        End If
IfError:
        If Not Session("Error") Is Nothing Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            lblError.Text = Session("Error")
            pnlError.Focus()
            pnlError.Visible = True
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

    Private Function validateForm() As Boolean
        Dim inputList As New List(Of Boolean)
        Dim validzip As String
        validzip = checkForNumbers(txtZipCode.Text)

        If ddlOwner.SelectedValue = String.Empty Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><font color=red>Please select owner name on your Choice Privileges account.</font>"
                inputList.Add(False)
            Else
                lblError.Text = "<font color=red>Please select owner name on your Choice Privileges account.</font>"
                inputList.Add(False)
            End If
        Else
            inputList.Add(True)
        End If

        If ddlOwner.SelectedValue = "Other" Then
            If Me.txtFirstName.Text.Trim.Length = 0 Then
                Me.imgAlert.Visible = True
                Me.lblError.Visible = True
                If Me.lblError.Text <> "" Then
                    lblError.Text &= "<br/><font color=red>Please enter the first name on your Choice Privileges account.</font>"
                    inputList.Add(False)
                Else
                    lblError.Text = "<font color=red>Please enter the first name on your Choice Privileges account.</font>"
                    inputList.Add(False)
                End If
            Else
                inputList.Add(True)
            End If

            If Me.txtLastName.Text.Trim.Length = 0 Then
                Me.imgAlert.Visible = True
                Me.lblError.Visible = True
                If Me.lblError.Text <> "" Then
                    lblError.Text &= "<br/><font color=red>Please enter the last name on your Choice Privileges account.</font>"
                    inputList.Add(False)
                Else
                    lblError.Text = "<font color=red>Please enter the last name on your Choice Privileges account.</font>"
                    inputList.Add(False)
                End If
            Else
                inputList.Add(True)
            End If

            If Me.txtChoiceId.Text.Trim.Length = 0 Then
                Me.imgAlert.Visible = True
                Me.lblError.Visible = True
                If Me.lblError.Text <> "" Then
                    lblError.Text &= "<br/><font color=red>Please enter your Choice Privileges® Member number.</font>"
                    inputList.Add(False)
                Else
                    lblError.Text = "<font color=red>Please enter your Choice Privileges® Member number.</font>"
                    inputList.Add(False)
                End If
            Else
                inputList.Add(True)
            End If

        End If


        If Me.txteligiblepoints.Text = 0 Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><font color=red>Your request exceeds the number of eligible Bluegreen Points on your account.</font>"
                inputList.Add(False)
            Else
                lblError.Text = "<font color=red>Your request exceeds the number of eligible Bluegreen Points on your account.</font>"
                inputList.Add(False)
            End If
        Else
            inputList.Add(True)
        End If

        If Me.txtBvcToChcPts.Text.Trim.Length = 0 Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><font color=red>Please enter the number of Points you wish to convert.</font>"
            Else
                lblError.Text &= "<font color=red>Please enter the number of Points you wish to convert.</font>"
            End If

            inputList.Add(False)
        ElseIf Not IsNumeric(Me.txtBvcToChcPts.Text) Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><font color=red>Invalid entry</font>"
            Else
                lblError.Text &= "<font color=red>Invalid entry</font>"
            End If

            inputList.Add(False)
        ElseIf Convert.ToInt64(Me.txtBvcToChcPts.Text) < 1 Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><font color=red>Invalid entry</font>"
            Else
                lblError.Text &= "<font color=red>Invalid entry</font>"
            End If

            inputList.Add(False)

        Else
            inputList.Add(True)
        End If


        If Me.txtChcPts.Text.Trim.Length = 0 Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><font color=red>Invalid entry.</font>"
            Else
                lblError.Text &= "<font color=red>Invalid entry.</font>"
            End If

            inputList.Add(False)
        ElseIf Not IsNumeric(Me.txtChcPts.Text) Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><font color=red>Invalid entry.</font>"
            Else
                lblError.Text &= "<font color=red>Invalid entry.</font>"
            End If

            inputList.Add(False)
        ElseIf Convert.ToInt64(Me.txtChcPts.Text) < 1 Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><font color=red>Invalid entry.</font>"
            Else
                lblError.Text &= "<font color=red>Invalid entry.</font>"
            End If

            inputList.Add(False)

        Else
            inputList.Add(True)
        End If

        If Me.txtNameOnCard.Text.Trim.Length = 0 Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><font color=red>Please enter the name on the credit card.</font>"
                inputList.Add(False)
            Else
                lblError.Text = "<font color=red>Please enter the name on the credit card.</font>"
                inputList.Add(False)
            End If
        Else
            inputList.Add(True)
        End If

        If Me.txtBxCCNumber.Text.Trim.Length = 0 Then
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            If Me.lblError.Text <> "" Then
                lblError.Text &= "<br/><font color=red>Please enter credit card number.</font>"
                inputList.Add(False)
            Else
                lblError.Text = "<font color=red>Please enter  credit card number.</font>"
                inputList.Add(False)
            End If
        Else
            inputList.Add(True)
        End If

        If (ChkUs.Checked = True) Then
            If (Me.txtZipCode.Text.Length >= 0) Then
                inputList.Add(True)
            End If
        Else
            txtZipCode.MaxLength = "5"
            If (validzip.Length < 5) Then
                Me.imgAlert.Visible = True
                Me.lblError.Visible = True
                If Me.lblError.Text <> "" Then
                    lblError.Text &= "<br/><font color=red>Invalid zip code.</font>"
                    inputList.Add(False)
                Else
                    lblError.Text = "<font color=red>Invalid zip code.</font>"
                    inputList.Add(False)
                End If
            Else
                If (validzip.Length = 5) Then
                    inputList.Add(True)
                End If
            End If
        End If


        For Each check As Boolean In inputList
            If check = False Then
                pnlError.Focus()
                pnlError.Visible = True
                Return check
            End If
        Next
        'at this point, all the values in the list are true, so return true
        Return True
    End Function
    Private Function ValidateCCDate() As Boolean
        Dim bValid As Boolean = True
        Dim dtCreditCardDate As DateTime
        dtCreditCardDate = DateTime.Parse(Me.ddlExpMonth.SelectedValue & "/1/" & Me.ddlExpYear.SelectedItem.Text)
        dtCreditCardDate = dtCreditCardDate.AddMonths(1)
        'checking credit card expiration date
        If dtCreditCardDate.CompareTo(Now) < 0 Then
            bValid = False
        End If
        Return bValid
    End Function
    Private Function validateCC() As Boolean
        Dim cardNumIsValid As Boolean = True
        Dim cardDateIsValid As Boolean = True
        Dim cardValidate As Boolean = True
        'checking credit card number and credit card type 
        If Not Me.CheckCC(Me.txtBxCCNumber.Text) Or Not Me.IsValidForCardType(Me.txtBxCCNumber.Text, Me.ddlCCtype.SelectedValue) Then
            cardNumIsValid = False
            cardValidate = False
        End If
        If Not Me.ValidateCCDate() Then
            lblError.Focus()
            cardDateIsValid = False
            cardValidate = False
        End If
        If Not cardNumIsValid Then
            pnlError.Visible = True
            lblError.Focus()
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            lblError.Text = "<font color=red>The credit card you entered is not valid. Please check the number again or try another card.</font>"
            If Not cardDateIsValid Then
                Me.lblError.Text &= "<br/><font color=red>Your credit card has expired. Please check the date again or try another card.</font>"
            End If
            Return False
        ElseIf Not cardDateIsValid Then
            pnlError.Visible = True
            lblError.Focus()
            Me.imgAlert.Visible = True
            Me.lblError.Visible = True
            lblError.Text = "<font color=red>Your credit card has expired. Please check the date again or try another card.</font>"
            Return False
        Else
            Me.imgAlert.Visible = False
            lblError.Visible = False
            lblError.Text = ""
            pnlError.Visible = False
        End If
        Return cardValidate
    End Function
    Private Function IsValidForCardType(ByVal cardnumber As String, ByVal cardtyp As String) As Boolean
        Dim validType As Boolean = False
        Select Case cardtyp
            Case "V"
                If (Regex.IsMatch(cardnumber, "^(4)")) And (cardnumber.Length = 16 Or cardnumber.Length = 13) Then
                    validType = True
                End If
            Case "M"
                If (Regex.IsMatch(cardnumber, "^5[1-5]\d{14}$|^2(?:2(?:2[1-9]|[3-9]\d)|[3-6]\d\d|7(?:[01]\d|20))\d{12}$")) And cardnumber.Length = 16 Then
                    validType = True
                End If
            Case "A"
                If (Regex.IsMatch(cardnumber, "^3(4|7)")) And cardnumber.Length = 15 Then
                    validType = True
                End If
            Case "D"
                If (Regex.IsMatch(cardnumber, "^(6011)")) And cardnumber.Length = 16 Then
                    validType = True
                End If
            Case Else
                validType = False
        End Select
        Return validType
    End Function
    Private Function CheckCC(ByVal strCCNo As String) As Boolean
        Try
            Dim i, w, x, y As Integer
            Dim CCNo As String

            y = 0
            CCNo = Replace(Replace(Replace(CStr(strCCNo), "-", ""), " ", ""), ".", "") 'Ensure proper format of the input
            If strCCNo <> CCNo Then
                'Whoah there, spaces and dashes and periods oh my!
                CheckCC = False
            Else
                'Process digits from right to left, drop
                '     last digit if total length is even
                w = 2 * (Len(CCNo) Mod 2)
                For i = Len(CCNo) - 1 To 1 Step -1
                    x = Mid(CCNo, i, 1)
                    If IsNumeric(x) Then
                        Select Case (i Mod 2) + w
                            Case 0, 3 'Even Digit - Odd where total length is odd (eg. Visa vs. Amx)
                                y = y + CInt(x)
                            Case 1, 2 'Odd Digit - Even where total length is odd (eg. Visa vs. Amx)
                                x = CInt(x) * 2
                                If x > 9 Then
                                    'Break the digits (eg. 19 becomes 1 + 9)
                                    '     
                                    y = y + (x \ 10) + (x - 10)
                                Else
                                    y = y + x
                                End If
                        End Select
                    End If
                Next
                'Return the 10's complement of the total
                '     
                y = 10 - (y Mod 10)
                If y > 9 Then y = 0
                CheckCC = (CStr(y) = Right(CCNo, 1))
            End If
        Catch ex As Exception
            'Whoops, that wasn't a number!
            CheckCC = False
        End Try
    End Function
    Private Sub populateCCDropDowns()
        'Load CheckIn and CheckOut drop downs
        Dim x As Integer

        For x = Year(Now) To Year(Now) + 12
            Dim li2 As New ListItem
            li2.Text = x
            li2.Value = (x - 2000) 'CC accept only YY format, so modified to YY
            If x = Today.Year Then
                li2.Selected = True
            End If
            Me.ddlExpYear.Items.Add(li2)
        Next
    End Sub




    Private Function SubmitPayment(ByVal paydetail As paymentdetail) As paymentdetail

        Try


            Dim CVC As New clsDBConnectivityVC
            'NEED TO SELECT THE MERCHANT ID HERE
            Try

                Dim reader As SqlClient.SqlDataReader
                CVC.dbCmnd.CommandText = "uspSelectMerchantID"
                CVC.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                CVC.dbCmnd.Parameters.Clear()
                CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("action", System.Data.SqlDbType.VarChar, 20)).Value = System.Configuration.ConfigurationManager.AppSettings("CPConversionMerchant")
                reader = CVC.dbCmnd.ExecuteReader()
                While reader.Read
                    paydetail.MERCHANTID = reader("MerchantID")
                End While
                reader.Close()

            Catch ex As Exception
                paydetail.ERRDETAIL = ex.Message.ToString
                paydetail.CONFNUMBER = " "
                CVC.Close()
                CVC = Nothing
                Return paydetail

            Finally
                CVC.Close()
            End Try

            'process credit card
            Try

                Dim TransactionRequestData As New VSSA.BoomiFundsSourceWS.TransactionRequestData()
                Dim TransactionRequestDataMetaData As VSSA.BoomiFundsSourceWS.TransactionRequestDataMetaData = New VSSA.BoomiFundsSourceWS.TransactionRequestDataMetaData()
                Dim TransactionResponseData As New VSSA.BoomiFundsSourceWS.TransactionResponseData

                TransactionRequestData.Address1 = ""
                TransactionRequestData.Address2 = ""
                TransactionRequestData.City = ""
                TransactionRequestData.State = ""

                '  Dim zipcode As String = owner.OwnerPostalCode 'System.Web.HttpContext.Current.Session("billingzip")
                If IsNothing(txtZipCode.Text) Then
                    TransactionRequestData.PostalCode = "11111"
                ElseIf txtZipCode.Text.Length < 2 Or txtZipCode.Text.Length > 5 Then
                    TransactionRequestData.PostalCode = "11111"
                Else
                    TransactionRequestData.PostalCode = txtZipCode.Text
                End If

                TransactionRequestData.NameOnTheCard = txtNameOnCard.Text
                TransactionRequestData.Country = ""
                TransactionRequestData.PaymentAmount = Convert.ToDecimal(paydetail.CCTOTAL)
                TransactionRequestData.CreditCardExpirationDate = paydetail.CCEXP
                TransactionRequestData.CreditCardCVV = ""
                TransactionRequestData.CreditCardNumber = paydetail.CCNUMBER
                TransactionRequestData.CreditCardType = paydetail.CCTYPE
                TransactionRequestData.TransactionType = System.Configuration.ConfigurationManager.AppSettings("BoomiFundsSourceTransactionType")
                TransactionRequestData.MerchantAccountNumber = paydetail.MERCHANTID
                TransactionRequestData.Country = ""
                TransactionRequestDataMetaData.ReferenceID = Session("arvact").ToString()
                TransactionRequestDataMetaData.CreatedBy = "VSSA"
                TransactionRequestData.MetaData = New VSSA.BoomiFundsSourceWS.TransactionRequestDataMetaData() {TransactionRequestDataMetaData}.ToList()

                Dim transaction = New VSSA.BoomiFundsSourceWS.FundsSourceService()
                TransactionResponseData = transaction.ProcessTransaction(TransactionRequestData)

                If (TransactionResponseData.MetaData IsNot Nothing AndAlso TransactionResponseData.MetaData.Description = "Failed") Then
                    Dim errorList = TransactionResponseData.Errors.[Select](Function(row) New TransactionResponseDataError With {.Code = row.Code, .ShortText = row.ShortText}).ToList()
                    paydetail.ERRDETAIL = errorList(0).ShortText
                    paydetail.ERRORCODE = errorList(0).Code
                ElseIf (TransactionResponseData.MetaData IsNot Nothing AndAlso TransactionResponseData.MetaData.Description = "Success") Then
                    paydetail.CONFNUMBER = TransactionResponseData.AuthCode
                Else
                    Dim errorList = TransactionResponseData.Errors.[Select](Function(row) New TransactionResponseDataError With {.Code = row.Code, .ShortText = row.ShortText}).ToList()
                    paydetail.ERRDETAIL = errorList(0).ShortText
                    paydetail.ERRORCODE = errorList(0).Code
                End If

            Catch ex As Exception
                paydetail.ERRORCODE = ex.Message.ToString()
                paydetail.CONFNUMBER = " "
                Return paydetail
            End Try
        Catch ex As Exception
            Me.senderror(ex.Message.ToString(), "CC process error")
            paydetail.ERRORCODE = ex.Message.ToString()
            paydetail.CONFNUMBER = " "
            Return paydetail
        End Try

        Return paydetail

    End Function
    Private Function GetTotalPoints() As String
        Try
            Dim response As String = ""
            Dim eligiblePoints As Long
            Dim pointsTransactionsRequest As New PointsTrans.PointsTransaction()
            Dim repo As New ResortService
            Dim rsrepo As New ResortService
            Dim totpointsRequest As New TotPointsReq.TotalPointsRequest()
            totpointsRequest.OwnerID = Session("arvact").ToString()
            totpointsRequest.Functions = "CHOICE"
            totpointsRequest.SiteName = "VSSA"
            Dim todaysDate As String = DateTime.Now.ToString("dd/MM/yyyy")
            totpointsRequest.EffectiveDate = todaysDate.ToString()
            Dim pointsResponse As TotPointsResp.TotalPointsResponse = Nothing
            pointsResponse = rsrepo.TotalPointsTransaction(totpointsRequest)
            eligiblePoints = pointsResponse.Points
            Dim errorMessage As String = ""
            If pointsTransactionsRequest.Success = 0 Then
                errorMessage = pointsTransactionsRequest.Message.ToString()
            ElseIf eligiblePoints < Convert.ToInt64(Trim(txtBvcToChcPts.Text)) Then
                errorMessage = "Owner don't have enough points to deduct."
            End If
            If eligiblePoints < Convert.ToInt64(Trim(txtBvcToChcPts.Text)) Then
                response = "Owner don't have enough points to deduct"
                Session("ProcessError") = "True"
                Me.senderror(response, "Owner Arvact:" & CInt(Session("arvact") & ",Choice Id: " & Trim(LblChoiceId.Text) & ",BVC convert points:" & Convert.ToInt64(Trim(txtBvcToChcPts.Text)) & ",choice points:" & txtChcPts.Text))
                Return response
            End If
            If Not (pointsResponse.Points = Nothing) Then
                response = "Success"
            Else
                Session("ProcessError") = "True"
                response = pointsResponse.Errors(0).ShortText
                Me.senderror(response, "Owner Arvact:" & CInt(Session("arvact")) & ",Choice Id: " & Trim(LblChoiceId.Text) & ",BVC convert points:" & Convert.ToInt64(Trim(txtBvcToChcPts.Text)) & ",choice points:" & txtChcPts.Text)
            End If
            Return response
        Catch ex As Exception
            Return ex.Message.ToString()
        End Try
    End Function
    Private Function deductPoints() As String
        Try
            Dim response As String = ""
            Dim pointsTransactionsRequest As New PointsTrans.PointsTransaction()
            Dim repo As New ResortService
            Dim errorMessage As String = ""
            Dim referenceNumber = repo.GenerateRefNumber()
            pointsTransactionsRequest.OwnerID = Session("arvact").ToString()
            pointsTransactionsRequest.Reference = referenceNumber
            pointsTransactionsRequest.TransactionPoints = Convert.ToInt64(txtBvcToChcPts.Text)
            pointsTransactionsRequest.Agent = Environment.UserName
            pointsTransactionsRequest.Functions = "CHOICE"
            pointsTransactionsRequest.PointTransactionType = "DeductReservationPoints"
            pointsTransactionsRequest.SiteName = "VSSA"
            Dim toDate As String = DateTime.Now.ToString("dd/MM/yyyy")
            pointsTransactionsRequest.EffectiveDate = toDate.ToString()
            pointsTransactionsRequest.Comment = ConfigurationManager.AppSettings("ChoiceDeductTransactionCode") & " " & referenceNumber
            Dim pointsTransactioResponse As ProcessPointsRes.ProcessPointsTransactionRes = Nothing
            pointsTransactioResponse = repo.ProcessPointsTransaction(pointsTransactionsRequest)
            If Not (pointsTransactioResponse.OwnerID = Nothing) Then
                response = "Success"
            Else
                Session("ProcessError") = "True"
                response = pointsTransactioResponse.Errors(0).ShortText
                Me.senderror(response, "Owner Arvact:" & CInt(Session("arvact")) & ",Choice Id: " & Trim(LblChoiceId.Text) & ",BVC convert points:" & Convert.ToInt64(Trim(txtBvcToChcPts.Text)) & ",choice points:" & txtChcPts.Text)
            End If
            Return response
        Catch ex As Exception
            Return ex.Message.ToString()
        End Try
    End Function

    Public Function insertComments(ByVal _msg As String) As Boolean

        Dim status As Boolean = False

        Dim year As Integer = Now.Year
        Dim month As Integer = Now.Month
        Dim day As Integer = Now.Day

        Dim tmpcmtdate As Integer = Nothing
        Dim mMonth As String = CStr(Now.Month)
        Dim mDay As String = CStr(Now.Day)


        If mMonth.Length = 1 Then
            mMonth = "0" & mMonth
        End If

        If mDay.Length = 1 Then
            mDay = "0" & mDay
        End If

        tmpcmtdate = (Mid(CStr(year), 3, 2)) & mMonth & mDay
        Dim cmtdate As Int64 = Nothing
        cmtdate = CInt(tmpcmtdate)

        Dim comments As String = ""
        Dim s As String = ""

        Using conn As IDbConnection = New iDB2Connection

            Try
                Dim Username As String = Environment.UserName
                Dim VCAcctNumbers As List(Of String) = New List(Of String)()
                VCAcctNumbers = Session("VCAccountNumber")
                Dim x As New StringBuilder
                x.Append(_msg)
                x.Append(" //")
                conn.ConnectionString = ConfigurationManager.AppSettings("VSSA.AS400.ConnectionString")
                Dim cmd As New iDB2Command '= conn
                cmd.Connection = conn
                cmd.CommandTimeout = 5000
                conn.Open()
                cmd.CommandText = "ZODBC.INSERTCMTS"
                cmd.CommandType = CommandType.StoredProcedure
                iDB2CommandBuilder.DeriveParameters(cmd)
                For i As Integer = 0 To VCAcctNumbers.Count - 1
                    cmd.Parameters("PROJECT").Value = CType("50", Int64)
                    cmd.Parameters("COMMENT").Value = x.ToString
                    cmd.Parameters("ACCOUNT").Value = VCAcctNumbers(i)
                    cmd.Parameters("USERID").Value = Username
                    cmd.Parameters("CMNTDATE").Value = cmtdate
                    cmd.Parameters.AddWithValue("@STATAUS", IBM.Data.DB2.iSeries.iDB2DbType.iDB2Char)
                    cmd.Parameters("@STATAUS").Value = "XX"
                    cmd.Parameters("@STATAUS").Direction = ParameterDirection.InputOutput
                    cmd.Parameters("@STATAUS").Size = 2
                    cmd.ExecuteNonQuery()
                Next

                status = True

            Catch ex As Exception
                senderror(ex.Message.ToString(), "Failed to add a record ZODBC.INSERTCMTS.")
                status = False
                Return ex.Message

            Finally
                conn.Close()
            End Try

        End Using

        Return status

    End Function

    Private Function TransactionLog(ByVal choiceAccountId As String, ByVal choiceAccountFirstName As String, ByVal choiceAccountLastName As String) As Boolean

        Dim result As Integer = Nothing
        Dim TPMemeber As Boolean = False
        Dim connection As New SqlConnection(ConfigurationManager.ConnectionStrings("VSSA.bxgwebDBConnection").ConnectionString)
        Dim cmnd As SqlCommand = connection.CreateCommand


        Try
            bxgOwner = Session("BXGOwner")
            If bxgOwner.Account.Memberships.VacationClubMembership.Membership.TravelerPlusEligible = True Then

                Try
                    Dim Tpexpiration As DateTime = bxgOwner.Account.Memberships.VacationClubMembership.Membership.MembershipExpirationDate
                    If Date.Compare(Tpexpiration, DateTime.Now) >= 0 Then
                        TPMemeber = True
                    End If

                Catch ex As Exception
                    senderror(ex.Message.ToString(), "Failed to to comapre TP expiration dates.")
                End Try


            End If

        Catch ex As Exception

        End Try

        Try
            Using connection
                connection.Open()
                cmnd.CommandType = CommandType.StoredProcedure
                cmnd.CommandText = "uspInsertBXGChoicePointConversion"
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Arvact", System.Data.SqlDbType.Int)).Value = Session("arvact")
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FirstName", System.Data.SqlDbType.VarChar, 50)).Value = choiceAccountFirstName
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LastName", System.Data.SqlDbType.VarChar, 50)).Value = choiceAccountLastName
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ZipCode", System.Data.SqlDbType.VarChar, 5)).Value = txtZipCode.Text
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 100)).Value = "Points Conversion"
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Choiceid", System.Data.SqlDbType.VarChar, 20)).Value = choiceAccountId
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BVCPoints", System.Data.SqlDbType.Int)).Value = txtBvcToChcPts.Text
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ChoicePoints", System.Data.SqlDbType.Int)).Value = txtChcPts.Text
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CCLastFour", System.Data.SqlDbType.Int)).Value = 0
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CardType", System.Data.SqlDbType.VarChar, 1)).Value = ddlCCtype.SelectedValue
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NameOntheCard", System.Data.SqlDbType.VarChar, 100)).Value = bxgOwner.People(0).FirstName & " ," & bxgOwner.People(0).LastName
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AuthCode", System.Data.SqlDbType.VarChar, 10)).Value = Session("authcode")
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Amount", System.Data.SqlDbType.VarChar, 10)).Value = ddlAmount.SelectedValue
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@User", System.Data.SqlDbType.VarChar, 20)).Value = Environment.UserName
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProcessError", System.Data.SqlDbType.Bit)).Value = Session("ProcessError")
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TpMember", System.Data.SqlDbType.Bit)).Value = TPMemeber
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Source", System.Data.SqlDbType.VarChar, 20)).Value = "BLUGRNBAS"
                cmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ErrDesc", System.Data.SqlDbType.VarChar, 250)).Value = TransactionError
                result = cmnd.ExecuteNonQuery()
            End Using


        Catch ex As Exception
            senderror(ex.Message.ToString(), "Failed to add a record to transactionlog.")
        Finally
            connection.Close()
        End Try

        Return True

    End Function

    Public Function SendPoints(ByVal choiceAccountId As String, ByVal choiceAccountPoint As Integer, ByVal choiceAccountLastName As String, ByVal choiceAccounProductType As String) As String
        Dim response As String = ""
        Try
            Dim pointstosend As New SendPoints.Points()
            Dim person As SendPoints.Person = New SendPoints.Person()
            Dim orders As SendPoints.Order = New SendPoints.Order()
            pointstosend.Person = New SendPoints.Person() {person}.ToList()
            pointstosend.Order = New SendPoints.Order() {orders}.ToList()
            pointstosend.ChoicePrivilegeID = choiceAccountId
            If revertpoints = 1 Then
                orders.Points = choiceAccountPoint
            Else
                orders.Points = String.Concat("-", choiceAccountPoint)
            End If
            person.LastName = choiceAccountLastName
            orders.ProductType = choiceAccounProductType
            Dim pointsSave As New LoyalityService
            Dim purchasePointsresponse As purchaseResponse.PurchasePointsResponse = Nothing
            purchasePointsresponse = pointsSave.SendPoints(pointstosend)
            If purchasePointsresponse Is Nothing Then
                response = "Failed to send points to Choice."

            ElseIf purchasePointsresponse.Success = False Then
                response = purchasePointsresponse.Errors(0).ShortText.ToString()
            Else

                response = "Success"
            End If
        Catch ex As Exception
            senderror(ex.Message.ToString(), "Failed to send points to Choice.")
        End Try
        Return response
    End Function

    Sub senderror(ByVal ex As String, ByVal msg As String)
        Try
            bxgOwner = Session("BXGOwner")

            Dim BodyText As String = ""
            Dim message As New MailMessage()
            message.From = New MailAddress(System.Configuration.ConfigurationManager.AppSettings("FromEmailTP"))

            'message.[To].Add(New MailAddress("krishna.guda@bluegreenvacations.com"))
            message.[To].Add(New MailAddress("siva.pamanchi@bluegreenvacations.com"))
            message.Subject = "Choice Accounts/Points convertion Error "
            BodyText &= "<ul> "
            BodyText &= "<li>" & msg & "</li>"
            BodyText &= "<li> Owner ARVACT:" & Session("arvact") & " </li> "
            BodyText &= "<li>" & ex & " </li> "
            BodyText &= "<li>" & Request.Url.AbsoluteUri & " </li> "
            BodyText &= "</ul>"
            message.Body = BodyText

            Dim client As New SmtpClient(System.Configuration.ConfigurationManager.AppSettings("bxgwebSMTPServer"), System.Configuration.ConfigurationManager.AppSettings("bxgwebSMTPServerPort"))
            client.Credentials = CredentialCache.DefaultCredentials
            client.Send(message)
            client.Dispose()
            message.Dispose()
        Catch exc As Exception

        End Try




    End Sub

    Protected Sub btnCloseWindow_Click(sender As Object, e As System.EventArgs) Handles btnCloseWindow.Click
        ModalPopupExtender1.Hide()
        Response.Redirect("cpaccounts.aspx")
    End Sub

    Public Function ValidateOwnerType() As Boolean
        Dim ValidOwner As Boolean = False
        bxgOwner = Session("BoomiOwner")
        Dim counter As Integer = 0
        Try
            If Not bxgOwner.Account.Memberships.VacationClubMembership.Legacy.MaintFeePaymentBalanceForReservation Is Nothing Then
                If Not bxgOwner.Account.Memberships.VacationClubMembership.Legacy.MaintenanceFees Is Nothing Then
                    Do Until counter = bxgOwner.Account.Memberships.VacationClubMembership.Legacy.MaintenanceFees.MaintenanceFeePaymentBalance.Count
                        With bxgOwner.Account.Memberships.VacationClubMembership.Legacy.MaintenanceFees.MaintenanceFeePaymentBalance(counter)
                            If .ProjectNumber = "50" Then
                                ValidOwner = True
                                Exit Do
                            End If
                        End With
                        counter = counter + 1
                    Loop
                End If
            End If
            If Not (bxgOwner.Account.Memberships.VacationClubMembership.Sampler Is Nothing) Then
                If bxgOwner.Account.Memberships.VacationClubMembership.Sampler.IsSampler = True Then
                    ValidOwner = False
                End If
            End If

        Catch ex As Exception

        End Try

        Return ValidOwner
    End Function

    Public Sub SendConversionPointConfirmation(ByVal choiceAccountId As String, ByVal choiceAccountFirstName As String, ByVal choiceAccountLastName As String)

        Dim result As Boolean

        Dim formDataTemplate = "API_BGV_OWN_BGO_Choice_Points_Conversion_Confirm|1262"

        Dim subjectofEmail = "Choice Accounts/Points convertion Error."

        bxgOwner = Session("BXGOwner")

        Dim dataDictionary As New Dictionary(Of String, String)
        dataDictionary.Add("ARVACT", Session("arvact"))
        If Not bxgOwner.People(0).EmailAddresses Is Nothing Then
            dataDictionary.Add("EMAIL_ADDRESS_", bxgOwner.People(0).EmailAddresses(0).Email)
        End If
        dataDictionary.Add("FIRST_NAME", choiceAccountFirstName)
        dataDictionary.Add("LAST_NAME", choiceAccountLastName)
        dataDictionary.Add("CHOICE_ID", choiceAccountId)
        dataDictionary.Add("BG_POINTS", If(String.IsNullOrEmpty(txtBvcToChcPts.Text), "0", txtBvcToChcPts.Text))
        dataDictionary.Add("CHOICE_POINTS", If(String.IsNullOrEmpty(txtChcPts.Text), "0", txtChcPts.Text))

        result = SendDataToThirdpartyResponsys(dataDictionary, formDataTemplate, Session("arvact"), subjectofEmail)

    End Sub


    Public Function VerifyPointsAfterDeduction(ByVal starttime As String, ByVal endtime As String) As String
        Dim response As String = ""
        Try
            Dim s As New StringBuilder()
            Dim PointsAfterDeduction As Integer = Convert.ToInt64(txteligiblepoints.Text) - Convert.ToInt64(txtBvcToChcPts.Text)
            Dim rsPoints As New ResortService
            Dim totpointsRequest As New TotPointsReq.TotalPointsRequest()
            totpointsRequest.OwnerID = Session("arvact").ToString()
            totpointsRequest.Functions = "CHOICE"
            totpointsRequest.SiteName = "VSSA"
            Dim todaysDate As String = DateTime.Now.ToString("dd/MM/yyyy")
            totpointsRequest.EffectiveDate = todaysDate.ToString()
            Dim result As SearchResponse.ProfileSearchResponse = Nothing
            Dim ownerTotalPoints As TotPointsResp.TotalPointsResponse = Nothing
            ownerTotalPoints = rsPoints.TotalPointsTransaction(totpointsRequest)
            Session("OwnerTotalPoints") = ownerTotalPoints.Points
            If ownerTotalPoints.Points > PointsAfterDeduction Then
                s.Append("Points After Deduction" & PointsAfterDeduction & ";")
                s.Append("Owner Points from WS" & ownerTotalPoints.Points & ";")
                s.Append(" Points deducted in AS400 at " & starttime & ". No change in the points at " & endtime)
                senderror(s.ToString(), "Comparing the owner points in SQL \with AS400")
            End If
            If Not ownerTotalPoints.Errors.Count = 0 Then

                response = ownerTotalPoints.Errors(0).ShortText.ToString()
            Else
                response = "Success"
            End If
        Catch ex As Exception
            senderror(ex.Message.ToString(), "Failed at VerifyPointsAfterDeduction process.")
        End Try
        Return response
    End Function
End Class
