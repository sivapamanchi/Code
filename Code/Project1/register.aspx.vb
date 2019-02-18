Imports System.Text.RegularExpressions
Namespace bluegreenonline

    Partial Class register
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents acctNo As System.Web.UI.WebControls.TextBox


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim sReferrer As String = Request.ServerVariables("HTTP_REFERER") & ""


            'KO - commented out this section since the routing to this page from the main page is already SSLed
            ''check to see if there is http. if so send to https
            ''first need to check if it is dev or stg
            ''there is no ssl on dev or stg so it would cause an infinite loop.

            Dim protocol As String

            Try

                Dim preFetchAcctNo As String = ""
                Dim preFetchEmail As String = ""

                preFetchAcctNo = Request.QueryString("oid")
                preFetchEmail = Request.QueryString("email")

                If Not preFetchAcctNo = "" Then
                    Me.txtAcctNo.Text = preFetchAcctNo.ToString
                End If

                If Not preFetchEmail = "" Then
                    Me.txtAcctEmail.Text = preFetchEmail.ToString
                End If

            Catch ex As Exception

            End Try


            If System.Configuration.ConfigurationManager.AppSettings("bxgwebSecureURL") = "https://bluegreenowner.com/" Then

                protocol = Request.ServerVariables("SERVER_PORT_SECURE")

                If (protocol = Nothing Or protocol = "0") Then

                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("bxgwebSecureURL") & "/register.aspx")
                End If

            End If


            If IsAS400Available() Then
                If sReferrer.ToLower().IndexOf("encorerewards") < 0 Then
                    If Not IsPostBack Then
                        If Request.QueryString("acct") <> "" Then
                            txtAcctNo.Text = Request.QueryString("acct")
                        End If
                        If Request.QueryString("ssn") <> "" Then
                            txtAcctSocial.Text = Request.QueryString("ssn")
                        End If
                        If Request.QueryString("phone") <> "" Then
                            txtAcctPhone.Text = Request.QueryString("phone")
                        End If
                        'Added for traveler plus promotional campaigns
                        If Request.QueryString("acct") <> "" And Request.QueryString("promo") = "TP53" Then
                            txtAcctNo.Text = Request.QueryString("acct")
                            Session("TPPromoCode") = "TP53"
                        End If
                    End If
                End If
            Else
                Response.Redirect("siteMaintenance.aspx", True)
            End If

        End Sub

        Private Sub registerBtn_Click(sender As Object, e As EventArgs) Handles registerBtn.Click

            Dim blnStopMe As Boolean
            blnStopMe = False

            'Perform some error checking to see if the info is submitted properly
            lblErrorSignIn.Text = ""

            If txtAcctNo.Text = "" Or Not IsNumeric(txtAcctNo.Text) Then
                imgAlert.Visible = True
                lblErrorSignIn.Visible = True
                lblErrorSignIn.Text &= "You must enter a valid owner number.<br />"
                blnStopMe = True
            Else
                Try
                    Dim iTst As Int32 = Convert.ToInt32(txtAcctNo.Text)
                Catch ex As Exception
                    imgAlert.Visible = True
                    lblErrorSignIn.Visible = True
                    lblErrorSignIn.Text &= "You must enter a valid owner number.<br />"
                    blnStopMe = True
                End Try
            End If

            If txtAcctSocial.Text = "" And txtAcctPhone.Text = "" Then
                imgAlert.Visible = True
                lblErrorSignIn.Visible = True
                lblErrorSignIn.Text &= "You must enter one of the following the last four digits of your social security number, OR the last ten digits of your home phone number.<br />"
                blnStopMe = True
            End If

            If txtAcctSocial.Text.Length > 0 Then
                Try
                    Dim iTst As Int32 = Nothing
                    If Not IsNumeric(txtAcctSocial.Text) Then
                        imgAlert.Visible = True
                        lblErrorSignIn.Visible = True
                        lblErrorSignIn.Text &= "Your social security number must be numeric.<br />"
                        blnStopMe = True
                    Else
                        iTst = Convert.ToInt32(txtAcctSocial.Text)
                    End If

                Catch ex As Exception
                    imgAlert.Visible = True
                    lblErrorSignIn.Visible = True
                    lblErrorSignIn.Text &= "Your social security number must be numeric.<br />"
                    blnStopMe = True
                End Try
            End If

            If txtAcctPhone.Text.Length > 0 Then
                Try
                    Dim iTst As Int64 = Nothing

                    If Not IsNumeric(txtAcctPhone.Text) Then
                        imgAlert.Visible = True
                        lblErrorSignIn.Visible = True
                        lblErrorSignIn.Text &= "Your home phone number must be numeric.<br />"
                        blnStopMe = True
                    Else
                        iTst = Convert.ToInt64(txtAcctPhone.Text)
                    End If


                Catch ex As Exception
                    imgAlert.Visible = True
                    lblErrorSignIn.Visible = True
                    lblErrorSignIn.Text &= "Your home phone number must be numeric.<br />"
                    blnStopMe = True
                End Try
            End If

            txtAcctEmail.Text = txtAcctEmail.Text.Trim()
            txtAcctEmail.Text = txtAcctEmail.Text.Trim()
            Dim cEmCheck As New clsEmailer
            If Not cEmCheck.IsValidEmail(txtAcctEmail.Text) Then
                imgAlert.Visible = True
                lblErrorSignIn.Visible = True
                lblErrorSignIn.Text &= "You must enter your email address.<br />"
                blnStopMe = True
            End If

            If txtAcctEmail.Text <> txtAcctEmail2.Text Then
                imgAlert.Visible = True
                lblErrorSignIn.Visible = True
                lblErrorSignIn.Text &= "The email addresses you entered do not match.<br />"
                blnStopMe = True
            End If

            If (txtAcctPassword.Text.Length < 6) Or (txtAcctPassword.Text.Length > 10) Or (Not Regex.IsMatch(txtAcctPassword.Text, "[0-9]")) Or (Not Regex.IsMatch(txtAcctPassword.Text, "[a-zA-Z]")) Then
                imgAlert.Visible = True
                lblErrorSignIn.Visible = True
                lblErrorSignIn.Text &= "You must enter a combination of between six and ten letters <b>AND</b> numbers for your password.<br />"
                blnStopMe = True
            End If

            If txtAcctPassword.Text <> txtAcctPassword2.Text Then
                imgAlert.Visible = True
                lblErrorSignIn.Visible = True
                lblErrorSignIn.Text &= "The passwords you entered do not match.<br />"
                blnStopMe = True
            End If

            Dim strSampler As String = "" 'IsSampler(txtAcctNo.Text)
            If strSampler <> "" Then
                imgAlert.Visible = True
                lblErrorSignIn.Visible = True
                If strSampler = "SAMPLERPLUS" Then
                    lblErrorSignIn.Text = "Please visit <a href=""http://www.samplerplus.com"">www.SamplerPlus.com</a> to register and log into your online Sampler Plus account.  <a href=""http://www.samplerplus.com"">Click here</a> to go to the Sampler Plus member web site now.<br />"
                ElseIf strSampler = "VALUESAMPLER" Then
                    lblErrorSignIn.Text = "Because Value Sampler membership follows different guidelines than full ownership, we are not able to accommodate your online account. One of the benefits of Bluegreen ownership is full access to the many features we offer online. Until you become an owner, you may explore Bluegreen resorts by clicking <a href=""http://www.bluegreenonline.com/explore/"">here</a>.<br />"
                End If
                blnStopMe = True
            End If

            If blnStopMe = False Then
                Dim C As New clsDBConnectivity
                Dim dbDataReader As SqlClient.SqlDataReader
                Dim blnOkay As Boolean

                'Check to see if this person has an account
                C.dbCmnd.CommandText = "uspCheckVacationAccount"
                C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                C.dbCmnd.Parameters.Clear()
                C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcctNo", System.Data.SqlDbType.Int)).Value = Convert.ToInt32(txtAcctNo.Text)

                If Not txtAcctSocial.Text.Length = 0 Then
                    C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcctSocial", System.Data.SqlDbType.VarChar)).Value = txtAcctSocial.Text
                    C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcctPhone", System.Data.SqlDbType.VarChar)).Value = ""
                ElseIf Not txtAcctPhone.Text.Length = 0 Then
                    C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcctSocial", System.Data.SqlDbType.VarChar)).Value = ""
                    C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcctPhone", System.Data.SqlDbType.VarChar)).Value = txtAcctPhone.Text
                End If

                dbDataReader = C.dbCmnd.ExecuteReader()

                'Check to see if we should add the Email/Pwd
                If dbDataReader.HasRows = True Then
                    blnOkay = True
                Else
                    blnOkay = False
                End If

                dbDataReader.Close()

                Session("LoginEmail") = txtAcctEmail.Text
                Session("LoginPassword") = txtAcctPassword.Text

                'Add the Email/Pwd

                If blnOkay = True Then
                    If checkEmailPassword() Then
                        Session("EnrollAcctNo") = txtAcctNo.Text
                        Session("ownerACCT") = txtAcctNo.Text
                        lblErrorSignIn.Visible = True
                        lblErrorSignIn.Text = "The email address specified is already in use. Please specify a different one."
                    Else
                        enrollEmailSQL() 'registering new owner email address to sql database

                    End If

                Else
                    sendUnsuccessful()
                End If

                C.Close()
            End If
        End Sub

        Private Function checkEmailPassword() As Boolean
            Dim bxgOwner As OwnerWS.Owner
            Dim ownerService As New OwnerWS.OwnerWS1SoapClient
            bxgOwner = ownerService.fetchOwner("", "", "", txtAcctEmail.Text, "")

            If Not String.IsNullOrEmpty(bxgOwner.Arvact) AndAlso Not bxgOwner.Arvact.Trim.Equals(txtAcctNo.Text.Trim) Then
                'email registered with different arvact
                Return True
            Else
                Return False
            End If
        End Function

        Private Sub enrollEmailAS400()
            Dim TPRenewalAS400DB2 As New clsDBConnectivityAS400DB2

            TPRenewalAS400DB2.dbCmnd.CommandText = "ZODBC.owneremail"
            TPRenewalAS400DB2.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure

            TPRenewalAS400DB2.dbCmnd.Parameters.Add("@vacationclub", IBM.Data.DB2.iSeries.iDB2DbType.iDB2Decimal)
            TPRenewalAS400DB2.dbCmnd.Parameters("@vacationclub").Value = CInt(txtAcctNo.Text)
            TPRenewalAS400DB2.dbCmnd.Parameters("@vacationclub").Direction = ParameterDirection.Input
            TPRenewalAS400DB2.dbCmnd.Parameters("@vacationclub").Precision = 6
            TPRenewalAS400DB2.dbCmnd.Parameters("@vacationclub").Scale = 0

            TPRenewalAS400DB2.dbCmnd.Parameters.Add("@email", IBM.Data.DB2.iSeries.iDB2DbType.iDB2Char)
            TPRenewalAS400DB2.dbCmnd.Parameters("@email").Value = txtAcctEmail.Text
            TPRenewalAS400DB2.dbCmnd.Parameters("@email").Direction = ParameterDirection.Input
            TPRenewalAS400DB2.dbCmnd.Parameters("@email").Size = 60

            Try
                TPRenewalAS400DB2.dbCmnd.CommandTimeout = 30
                TPRenewalAS400DB2.dbCmnd.ExecuteNonQuery()
            Catch ex As Exception

                senderror(ex.Message.ToString, "ZODBC.owneremail")
                Exit Sub
            Finally
                TPRenewalAS400DB2.Close()
            End Try
        End Sub

        Sub senderror(ByVal ex As String, ByVal message As String)

            Dim str As String = ex
            Dim errormessage As String = ""

            Dim EM As New clsEmailer
            EM.ToEmail = "krishna.guda@bluegreencorp.com"
            EM.FromEmail = System.Configuration.ConfigurationManager.AppSettings("FromEmailTP")
            EM.Subject = "TP Renewal Error"
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
            lblErrorSignIn.Visible = True
            lblErrorSignIn.Text = "There was an error processing your request. Please contact Customer Service at 1.800.459.1597."
            Exit Sub
        End Sub

        Private Sub enrollEmailSQL()
            Dim dbDataReader As SqlClient.SqlDataReader
            Dim C As New clsDBConnectivity

            C.dbCmnd.CommandText = "uspEnrollEmail"
            C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
            C.dbCmnd.Parameters.Clear()
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar)).Value = txtAcctEmail.Text
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Password", System.Data.SqlDbType.VarChar)).Value = txtAcctPassword.Text
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ARVACT", System.Data.SqlDbType.Int)).Value = Convert.ToInt32(txtAcctNo.Text)

            dbDataReader = C.dbCmnd.ExecuteReader()

            dbDataReader.Close()
            C.Close()
            enrollEmailAS400() 'registering new owner email address to AS400 database 
            Session("EnrollAcctNo") = txtAcctNo.Text

            ' call the new service that syncs password changes in the owner table with the ADFS LDAP entries
            Dim client As BGOOwnerToADFS.ADFSSyncServiceClient = New BGOOwnerToADFS.ADFSSyncServiceClient()
            client.RegisterOwner(txtAcctNo.Text, txtAcctEmail.Text, txtAcctPassword.Text)

            Response.Redirect("registerSuccess.aspx")

        End Sub

        Private Sub sendUnsuccessful()
            Response.Redirect("registerUnsuccess.aspx?error=N")
        End Sub

        Private Function IsSampler(ByVal arvact As String) As String

            Dim dbConn As New clsDBConnectivity
            Dim dataReader As SqlClient.SqlDataReader
            Dim strSampler As String = ""

            If arvact <> "" Then

                dbConn.dbCmnd.CommandText = "bluegreenonline.dbo.uspGetSamplerPlusOwnerInfo"
                dbConn.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                dbConn.dbCmnd.Parameters.Clear()
                dbConn.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@arvact", System.Data.SqlDbType.Float)).Value = arvact

                dataReader = dbConn.dbCmnd.ExecuteReader()

                If dataReader.HasRows Then
                    dataReader.Read()
                    If dataReader("ARSLTY") = "L" And dataReader("ARHOME") = "52" Then
                        strSampler = "SAMPLERPLUS"
                    ElseIf dataReader("ARSLTY") = "S" And dataReader("ARHOME") = "51" Then
                        strSampler = "VALUESAMPLER"
                    End If
                End If
            End If

            Return strSampler

        End Function

        Protected Sub txtAcctPhone_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAcctPhone.TextChanged

        End Sub


        Private Sub LookupAcctNum()
            Dim C As New clsDBConnectivity
            Dim dbDataReader As system.data.SqlClient.SqlDataReader
            Dim sAcctNum As String = ""
            Dim iSocial As Int16 = 0
            Dim iPhone As Int64 = 0

            Try
                iSocial = Convert.ToInt16(txtlookupAcctSocial.Value)
            Catch ex As Exception

            End Try

            Try
                iPhone = Convert.ToInt64(txtlookupAcctPhone.Value)
            Catch ex As Exception

            End Try

            C.dbCmnd.CommandText = "uspCheckVacationAccount"
            C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
            C.dbCmnd.Parameters.Clear()
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcctNo", System.Data.SqlDbType.Int)).Value = 0
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcctSocial", System.Data.SqlDbType.VarChar)).Value = txtlookupAcctSocial.Value
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcctPhone", System.Data.SqlDbType.VarChar)).Value = txtlookupAcctPhone.Value

            dbDataReader = C.dbCmnd.ExecuteReader()

            While dbDataReader.Read()
                sAcctNum = dbDataReader("ARVACT")
            End While

            dbDataReader.Close()
            C.Close()
            C = Nothing

            If sAcctNum = "" Then
                imgAlert.Visible = True
                lblErrorSignIn.Visible = True
                lblErrorSignIn.Text &= "Account not found. Please verify it!.<br />"
            Else
                txtAcctSocial.Text = txtlookupAcctSocial.Value
                txtAcctPhone.Text = txtlookupAcctPhone.Value
                txtAcctNo.Text = sAcctNum
                imgAlert.Visible = False
                lblErrorSignIn.Visible = False
            End If
        End Sub


        Protected Sub submitBtn_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles submitBtn.Click
            LookupAcctNum()
        End Sub
    End Class

End Namespace
