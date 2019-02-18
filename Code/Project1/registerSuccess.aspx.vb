Imports BXGDiagnostics
Imports ResponsysWs
Imports System
Imports System.Drawing
Imports System.Net
Imports System.IO
Imports System.Xml
Imports System.Collections
Imports System.Web.Services.Protocols
Imports BXG.BGO.Choice.PrivilegesAccounts.ResponsysWS57

Namespace BluegreenOnline


    Partial Class registerSuccess
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents rdoDemoAge As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents rdoDemoIncome As System.Web.UI.WebControls.RadioButtonList


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Dim loggedIn As Boolean
        Dim service As ResponsysWS57
        Dim sessionheader As SessionHeader
        Public bxgOwner As New OwnerWS.Owner
        Dim owner As New Bluegreenowner
        Dim Tplevel As String
        Private objLogging As BXGDiagnostics.EventLogging

        Private Class clsAcctContract
            Public PRORDER As String
            Public ACCT As String
            Public PRONAME As String
        End Class

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            If IsAS400Available() Then
                If Not IsPostBack Then

                    getCountries()
                    getAccountStats()
                    changeCountries()
                    LoadAcctContractInfo()

                End If
            Else
                Response.Redirect("siteMaintenance.aspx", True)
            End If
        End Sub

        Private Sub LoadAcctContractInfo()

            If Convert.ToInt32(Session("EnrollAcctNo")) > 0 Then

                Dim C As New clsDBConnectivity

                Try
                    Dim drAcct As SqlClient.SqlDataReader
                    Dim iCurrProj As Integer = 0
                    Dim iProjOrder As Integer = 1
                    Dim alCurrProj As New ArrayList

                    C.dbCmnd.CommandText = "uspGetOwnerAccountsAS400"
                    C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                    C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@arvact", System.Data.SqlDbType.VarChar, 6)).Value = Convert.ToInt32(Session("EnrollAcctNo"))

                    drAcct = C.dbCmnd.ExecuteReader()
                    Dim cAC As New clsAcctContract

                    While drAcct.Read()
                        cAC = New clsAcctContract
                        cAC.PRORDER = iProjOrder.ToString()
                        cAC.PRONAME = drAcct("ProjNm")
                        cAC.ACCT = drAcct("ACCT")
                        alCurrProj.Add(cAC)
                        iProjOrder = iProjOrder + 1
                    End While

                    'Get the last guy on the list
                    If Not drAcct.HasRows Then
                        rptContracts.Visible = False
                    End If

                    drAcct.Close()

                    'Bind repeater
                    rptContracts.DataSource = alCurrProj
                    rptContracts.DataBind()
                Catch ex As Exception
                    rptContracts.Visible = False
                End Try

                C.Close()
                C = Nothing

            Else
                rptContracts.Visible = False
            End If

        End Sub
        Sub fetchOwnerInfoFromOwnerService()

            Dim ownerService As New OwnerWS.OwnerWS1SoapClient
            Try
                bxgOwner = ownerService.fetchOwner(lblAcctNo.Text, "", "", "", "")
                Tplevel = CInt(bxgOwner.TravelerPlusMembership.TPLevel)

                If Tplevel Is Nothing Or Tplevel = "" Then
                    Tplevel = "0"
                End If

            Catch ex As Exception
            End Try

        End Sub

        Private Sub getAccountStats()
            Dim dbDataReader As SqlClient.SqlDataReader
            Dim C As New clsDBConnectivity

            'Pull up account info for Enrollment Completion
            C.dbCmnd.CommandText = "uspGetAccountStatsForEnrollment"
            C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
            C.dbCmnd.Parameters.Clear()
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcctNo", System.Data.SqlDbType.Int)).Value = Convert.ToInt32(Session("EnrollAcctNo"))

            dbDataReader = C.dbCmnd.ExecuteReader()

            If dbDataReader.HasRows Then
                dbDataReader.Read()
                lblAcctNo.Text = dbDataReader("ARVACT")
                lblFirstName.Text = dbDataReader("NameFirst") & ""
                lblLastName.Text = dbDataReader("NameLast") & ""
                ddlCountry.ClearSelection()
                If (dbDataReader("CountryCode") & "") <> "" Then
                    ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dbDataReader("CountryCode") & ""))
                End If
                Session("enrollStateHolder") = dbDataReader("State") & ""
                If Session("enrollStateHolder") = "" Then Session("enrollStateHolder") = Right("  " & dbDataReader("RegionCode"), 2)
                txtHomePhone.Text = dbDataReader("PhoneHome") & ""
                txtDayPhone.Text = dbDataReader("PhoneCell") & ""
                txtEmail.Text = dbDataReader("Email") & ""
                txtCity.Text = dbDataReader("City") & ""
                txtZip.Text = dbDataReader("PostalCode") & ""

                'Present form fields for US or international users
                If dbDataReader("CountryCode").ToString() = "US" Then
                    pnlCountryNotUS.Visible = False
                    pnlCountryUS.Visible = True
                    lblAdd2Req.Visible = False

                    txtAddress1.Text = dbDataReader("Address1") & ""
                    txtAddress2.Text = dbDataReader("Address2") & ""
                Else
                    pnlCountryNotUS.Visible = True
                    pnlCountryUS.Visible = False
                    lblAdd2Req.Visible = True

                    Dim sAddr As String = dbDataReader("Address1")
                    Dim arAddr() As String
                    Try
                        arAddr = sAddr.Split(ControlChars.CrLf)
                        txtAddress1.Text = Trim(arAddr(0))
                        txtAddress2.Text = Trim(arAddr(1))
                        txtAddress3.Text = Trim(arAddr(2))
                    Catch ex As Exception

                    End Try

                    'Try to set the country dropdown
                    For Each li As ListItem In ddlCountry.Items
                        If sAddr.IndexOf(li.Text) > 0 Or dbDataReader("PostalCode").ToString().IndexOf(li.Text) > 0 Then
                            ddlCountry.ClearSelection()
                            ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByText(li.Text))
                            Exit For
                        End If
                    Next
                End If
            End If

            dbDataReader.Close()

            C.Close()
            C = Nothing

        End Sub

        Private Sub getCountries()

            Dim dbDataReader As SqlClient.SqlDataReader
            Dim C As New clsDBConnectivity
            'Pull out countries to populate corresponding dropdown

            'Put a US option at the top of the list.
            Dim liUS As New ListItem
            liUS.Value = "US"
            liUS.Text = "UNITED STATES"
            ddlCountry.Items.Add(liUS)

            Session.Remove("msg")
            'By Default its US Domestic Address
            Session("msg") = True

            C.dbCmnd.CommandText = "uspGetCountries"
            C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
            dbDataReader = C.dbCmnd.ExecuteReader()

            While dbDataReader.Read()
                Dim li As New ListItem

                li.Value = dbDataReader("CountryCode")
                li.Text = dbDataReader("CountryName")

                If dbDataReader("CountryCode") = "US" Then
                    li.Selected = True
                Else
                    li.Selected = False
                    Session("msg") = False

                End If
                Session("msg") = True
                ddlCountry.Items.Add(li)


            End While

            dbDataReader.Close()
            C.Close()
            C = Nothing

        End Sub


        Private Sub changeCountries()

            If ddlCountry.SelectedValue <> "US" Then
                'lblPostal.Text = "Postal Code:"
                pnlCountryNotUS.Visible = True
                pnlCountryUS.Visible = False
                lblAdd2Req.Visible = True
            Else
                'lblPostal.Text = "Zip:"
                pnlCountryNotUS.Visible = False
                pnlCountryUS.Visible = True
                lblAdd2Req.Visible = False

                Dim dbDataReader As SqlClient.SqlDataReader
                Dim C As New clsDBConnectivity

                'Pull out states/provinces to populate corresponding dropdown
                C.dbCmnd.CommandText = "uspGetStatesProvinces"
                C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                C.dbCmnd.Parameters.Clear()
                C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CountryCode", System.Data.SqlDbType.VarChar)).Value = ddlCountry.SelectedValue

                dbDataReader = C.dbCmnd.ExecuteReader()
                ddlState.Items.Clear()

                If dbDataReader.HasRows Then
                    Dim liBlank As New ListItem
                    liBlank.Text = ""
                    liBlank.Value = ""
                    ddlState.Items.Add(liBlank)

                    While dbDataReader.Read()
                        Dim li2 As New ListItem

                        li2.Value = Right("  " & dbDataReader("RegionCode"), 2)
                        li2.Text = dbDataReader("Subdivision1")
                        ddlState.Items.Add(li2)

                    End While
                Else
                    Dim liBlank As New ListItem
                    liBlank.Text = "N/A"
                    liBlank.Value = ""
                    ddlState.Items.Add(liBlank)
                End If

                dbDataReader.Close()
                C.Close()
                C = Nothing

                ddlState.ClearSelection()
                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(Session("enrollStateHolder") & ""))
            End If
        End Sub

        Private Sub ddlCountry_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCountry.SelectedIndexChanged
            changeCountries()
        End Sub

        Private Function ErrorCheck() As Boolean

            'Error check for form data

            If txtAddress1.Text.Length = 0 Then
                lblAddVerifyErr.Visible = True
                lblAddVerifyErr.Text = "You must enter your street address."
                Return False
            End If

            If ddlCountry.SelectedValue <> "US" Then
                If txtAddress2.Text.Length = 0 Then
                    lblAddVerifyErr.Visible = True
                    lblAddVerifyErr.Text = "You must enter the second line of your address."
                    Return False
                End If
            Else
                If txtCity.Text.Length = 0 Then
                    lblAddVerifyErr.Visible = True
                    lblAddVerifyErr.Text = "You must enter your city."
                    Return False
                End If

                If txtZip.Text.Length = 0 Then
                    lblAddVerifyErr.Visible = True
                    lblAddVerifyErr.Text = "You must enter your ZIP code."
                    Return False
                End If
            End If


            If txtHomePhone.Text.Length > 0 Then
                If Not IsNumeric(txtHomePhone.Text) Then
                    lblAddVerifyErr.Visible = True
                    lblAddVerifyErr.Text = "Please only use numeric digits in your phone number."
                    Return False
                End If
                If txtHomePhone.Text.Length < 10 Then
                    lblAddVerifyErr.Visible = True
                    lblAddVerifyErr.Text = "Please enter at least 10 numerical digits for your phone number."
                    Return False
                End If
            Else
                lblAddVerifyErr.Visible = True
                lblAddVerifyErr.Text = "You must enter your home phone number."
                Return False
            End If

            If txtDayPhone.Text.Length > 0 Then
                If Not IsNumeric(txtDayPhone.Text) Then
                    lblAddVerifyErr.Visible = True
                    lblAddVerifyErr.Text = "Please only use numeric digits in your phone number."
                    Return False
                End If
                If txtDayPhone.Text.Length < 10 Then
                    lblAddVerifyErr.Visible = True
                    lblAddVerifyErr.Text = "Please enter at least 10 numerical digits for your phone number."
                    Return False
                End If
            Else
                lblAddVerifyErr.Visible = True
                lblAddVerifyErr.Text = "You must enter your alternate phone number."
                Return False
            End If

            Dim cEmCheck As New clsEmailer
            If Not cEmCheck.IsValidEmail(txtEmail.Text) Then
                lblAddVerifyErr.Visible = True
                lblAddVerifyErr.Text = "You must enter your valid email address."
                Return False
            End If

            Return True
        End Function

        Private Function VerifyAddress() As Boolean
            'Only check inside the United States
            If ddlCountry.SelectedValue = "US" Then
                Dim objAdd As New ADDRESSOBJECTLib.AddressCheck

                'Set the license code for MelissaData, if fail then jump out
                If objAdd.SetLicenseString("IygmNBuW+I+a7rBhEr1HVL==M9lEEl48yYPs4eLB9O+/SO==") <> 1 Then
                    Return False
                End If

                'Initialize our address verification object
                If objAdd.Initialize(System.Configuration.ConfigurationManager.AppSettings("MelissaDataPath"), System.Configuration.ConfigurationManager.AppSettings("MelissaDataPath"), "") <> 0 Then
                    lblAddVerifyErr.Visible = True
                    lblAddVerifyErr.Text = "We are unable to verify your address at this time.  Please try again later."

                    'Log that the MelissaData component was unable to initialize - generates an email alert to us
                    objLogging.LogEvent(Server.MachineName & ": MelissaData is now expired!", Diagnostics.EventLogEntryType.Error)

                    Return False
                Else
                    'Add the address to verify
                    Dim sSelectedState As String = ddlState.SelectedValue
                    objAdd.ClearProperties()
                    objAdd.Address = txtAddress1.Text
                    'objAdd.Address2 = txtAddress2.Text
                    objAdd.City = txtCity.Text
                    objAdd.State = sSelectedState
                    objAdd.Zip = txtZip.Text

                    'Verify the address
                    If objAdd.VerifyAddress() = 0 Then
                        Dim errCode As String = objAdd.ErrorCode
                        Dim errString As String = objAdd.ErrorString
                        'Bad address, return specific feedback or general error
                        If errCode = "R" Then
                            lblAddVerifyErr.Text = "The address you entered was not found.  Please recheck and try again."
                        Else
                            lblAddVerifyErr.Text = errString
                        End If
                        imgAlert.Visible = True
                        lblAddVerifyErr.Visible = True
                        Return False
                    Else
                        'We have a good address, set the fields with scrubbed data
                        lblAddVerifyErr.Visible = False
                        txtAddress1.Text = objAdd.Address
                        'txtAddress2.Text = objAdd.Address2
                        txtCity.Text = objAdd.City
                        ddlState.ClearSelection()
                        Try
                            ddlState.Items.FindByValue(objAdd.State).Selected = True
                        Catch ex As Exception
                            ddlState.Items.FindByValue(sSelectedState).Selected = True
                        End Try
                        txtZip.Text = objAdd.Zip & "-" & objAdd.Plus4
                    End If
                End If

                objAdd = Nothing
            End If

            'If we made it this far then we are good to go!
            Return True
        End Function

        Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUpdate.Click
            'Prep label for feedback
            lblAddVerifyErr.ForeColor = Color.Red
            lblAddVerifyErr.Visible = False


            fetchOwnerInfoFromOwnerService()
                Dim result As Boolean
                Dim formDataTemplate = "API_BGV_OWN_BGO_Registration_Confirmation|1202"
                Dim subjectofEmail = "Registration Success Page Email Error."
            '  bxgOwner = Session("BXGOwner")
            Dim dataDictionary As New Dictionary(Of String, String)
                dataDictionary.Add("ARVACT", bxgOwner.Arvact)
                dataDictionary.Add("FIRST_NAME", bxgOwner.firstName)
                dataDictionary.Add("LAST_NAME", bxgOwner.lastName)
                dataDictionary.Add("EMAIL_ADDRESS_", bxgOwner.Email)
                dataDictionary.Add("FORM_SOURCE", "BGO")
                Dim ownerType As String = String.Empty
                If Not IsNothing(bxgOwner) Then
                    If bxgOwner.User(0).isSampler = True And bxgOwner.User(0).project = "50" Then
                        ownerType = "Sampler"
                        If bxgOwner.User(0).HomeProject = 52 Then
                            ownerType = "Sampler24"
                        ElseIf bxgOwner.User(0).HomeProject = 51 Then
                            ownerType = "Value Sampler"
                        End If
                    ElseIf bxgOwner.User(0).isSampler = False And bxgOwner.User(0).project = "50" Then
                        ownerType = "Vacation Club"
                    Else
                        ownerType = "Fixed"
                    End If
                End If
                dataDictionary.Add("OWNERCONTRACTTYPE", ownerType)
                dataDictionary.Add("MEMBER_TP_ACTIVE", Tplevel)
                dataDictionary.Add("AccountExpired", bxgOwner.TravelerPlusMembership.AccountExpired)
            'result = SendDataToThirdpartyResponsys(dataDictionary, formDataTemplate, bxgOwner.Arvact, subjectofEmail)

            Session("newUser") = True
            Response.Redirect("loginWait.aspx")

        End Sub

        Protected Function Login() As Boolean
            Dim result As Boolean = False

            Try
                Dim loginResult As LoginResult
                loginResult = service.login("ITapi@bluegreencorp.com", "Responsysapi")
                Dim sessionId = loginResult.sessionId
                If sessionId <> Nothing Then
                    sessionheader = New SessionHeader()
                    sessionheader.sessionId = sessionId
                    service.SessionHeaderValue = sessionheader
                    Console.WriteLine("Setting the Client Timeout to 1 hour")
                    service.Timeout = (1000 * 60 * 60)
                    loggedIn = True
                    result = loggedIn
                End If

            Catch e As System.Web.Services.Protocols.SoapException
                senderror(e.Message.ToString, "Login()")

            Catch e1 As Exception
                senderror(e1.Message.ToString, "Login()")

            End Try

            Return result

        End Function

        Sub senderror(ByVal ex As String, ByVal message As String)

            Dim str As String = ex
            Dim errormessage As String = ""

            Dim EM As New clsEmailer
            EM.ToEmail = "olpsupport@bluegreencorp.com"
            EM.FromEmail = System.Configuration.ConfigurationManager.AppSettings("FromEmailTP")
            EM.Subject = "BGO - Register Error"
            EM.BodyText = "<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01 Transitional//EN""><!-- ""http://www.w3.org/TR/html4/loose.dtd"" -->" & _
                          "<html><head><title>Register with BluegreenOnline</title></head><body topmargin=0 leftmargin=0 marginheight=0 marginwidth=0>" & _
                          "<table width=490 cellpadding=0 cellspacing=0 border=0 class=text>" & _
                          "<tr>" & _
                          "<td valign=top>Register Exception:<br> " & message & " </td></tr>" & _
                          "<tr>" & _
                          "<td valign=top>Register Exception:<br>" & Session("OwnerNumber") & "</td></tr>" & _
                          "<tr>" & _
                          "<td valign=top>Register Exception:<br>" & str & "</td></tr></table></body></html>"

            EM.BodyText &= "Account number:" & Session("AccountNumber") & " <br>"
            'EM.emailBodyFormat = Mail.MailFormat.Html
            EM.sendEmail()

            Session("renewalConfirm") = ""

        End Sub

        Protected Sub Logout()
            service.logout()
        End Sub

    End Class

End Namespace
