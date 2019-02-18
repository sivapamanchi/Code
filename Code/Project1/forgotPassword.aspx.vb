Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Web.SessionState.HttpSessionState
Imports System.Data.SqlClient
Imports IBM.Data.DB2.iSeries
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace BluegreenOnline

    Partial Class forgotPassword
        Inherits System.Web.UI.Page
        Dim bxgOwner As New OwnerWS.Owner
        Dim tempBxgOwner As New OwnerWS.Owner



#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim sReferrer As String = Request.ServerVariables("HTTP_REFERER") & ""
        End Sub

        Private Sub imgSubmit_Click(sender As Object, e As EventArgs) Handles imgSubmit.Click
            Dim sErrMsg As String = ""

            'Confirm proper information entered
            If Trim(txtEmailAddress.Text).Length = 0 Then
                sErrMsg &= "Please enter your <B>Email Address</B><BR>"
            End If


            If Trim(txtPhone.Text).Length <> 10 And Trim(txtSSN.Text).Length <> 4 Then
                sErrMsg &= "Please enter the last ten digits of either your <B>home phone</B> number OR last four digits of your <B>social security</B> number"
            End If

            If sErrMsg.Length > 0 Then
                'Fail to begin the check

                lblErrors.Text = sErrMsg
                imgAlert.Visible = True
                lblErrors.Visible = True
            Else
                'Do the password check and possibly send it in an email
                lblErrors.Visible = False
                imgAlert.Visible = False
                verifyEmail()
            End If
        End Sub

        Private Sub verifyEmail()
            Try
                'Declare containers for results
                Dim sEmail As String
                Dim sPass As String

                'Setup database
                Dim C As New clsDBConnectivity
                Dim drReader As SqlClient.SqlDataReader

                'Hit the DB with our query
                C.dbCmnd.CommandText = "uspForgotPassword"
                C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                C.dbCmnd.Parameters.Clear()
                C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 100)).Value = Trim(txtEmailAddress.Text)
                If Trim(txtSSN.Text).Length = 4 Then
                    C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SSN", System.Data.SqlDbType.VarChar, 4)).Value = Trim(txtSSN.Text)
                Else
                    If Trim(txtPhone.Text).Length = 10 Then
                        C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Phone", System.Data.SqlDbType.VarChar, 10)).Value = Trim(txtPhone.Text)
                    End If
                End If
                drReader = C.dbCmnd.ExecuteReader()

                'Get our results
                While drReader.Read()
                    Try
                        sEmail = drReader("Email")
                        sPass = drReader("Password")
                    Catch ex As Exception
                        sEmail = Nothing
                        sPass = Nothing
                    End Try
                End While

                'Cleanup 
                C.Close()
                C = Nothing


                'Send out email if match found
                If Not sEmail Is Nothing Or Not sPass Is Nothing Then
                    If (sEmail.Length > 0) And (sPass.Length > 0) Then
                        sendEmail(sEmail)
                    Else
                        'Failed to find a match

                        lblErrors.Text = "No match found.  Please recheck your information and try again."
                        imgAlert.Visible = True
                        lblErrors.Visible = True
                    End If
                Else
                    'Failed to find a match

                    lblErrors.Text = "No match found.  Please recheck your information and try again."
                    imgAlert.Visible = True
                    lblErrors.Visible = True
                End If
            Catch ex As Exception
                lblErrors.Text = "No match found.  Please recheck your information and try again."
                imgAlert.Visible = True
                lblErrors.Visible = True
            End Try


        End Sub
        Public Function ChangeUserPassword(ByVal _arvact As String, ByVal _oldPass As String, ByVal _newPass As String) As Boolean

            Dim changeCompleted As Boolean = False
            Dim sUpdateResult As String
            Dim C As New clsDBConnectivity
            Dim dr As SqlClient.SqlDataReader


            ' call the new service that syncs password changes in the owner table with the ADFS LDAP entries
            Dim client As BGOOwnerToADFS.ADFSSyncServiceClient = New BGOOwnerToADFS.ADFSSyncServiceClient()
            Dim resp As BGOOwnerToADFS.UpdateResponse = client.ResetPassword(_arvact.Trim(), txtEmailAddress.Text, _newPass)
            Try
                If client.State <> System.ServiceModel.CommunicationState.Faulted Then
                    client.Close()
                Else
                    client.Abort()
                End If
            Catch ex As Exception
                client.Abort()
            End Try

            If resp.IsSuccessful Then

                Try

                    C.dbCmnd.CommandType = CommandType.StoredProcedure
                    C.dbCmnd.CommandText = "uspUpdateOwnerPassword"
                    C.dbCmnd.Parameters.Clear()
                    C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ARVACT", System.Data.SqlDbType.VarChar, 100)).Value = _arvact.Trim()
                    If _oldPass = "" Then
                        C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OldPassword", System.Data.SqlDbType.VarChar, 100)).Value = DBNull.Value

                    Else
                        C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OldPassword", System.Data.SqlDbType.VarChar, 100)).Value = _oldPass.Trim()

                    End If
                    C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NewPassword", System.Data.SqlDbType.VarChar, 100)).Value = _newPass.Trim()
                    C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PswdChngFlag", System.Data.SqlDbType.VarChar, 100)).Value = 1

                    dr = C.dbCmnd.ExecuteReader()

                    While (dr.Read())
                        sUpdateResult = dr("")

                        If sUpdateResult = "Success" Then
                            changeCompleted = True
                        End If

                    End While

                Catch ex As Exception
                    Throw ex
                    changeCompleted = False

                Finally

                    C.dbCmnd.Dispose()
                    C.dbConn.Dispose()

                End Try

            Else
                changeCompleted = False
            End If

            Return changeCompleted

        End Function

        Private Sub sendEmail(ByVal strEmail As String)
            Try
                fetchOwnerInfoFromOwnerService(strEmail)
                Dim tempPass As String = GeneratePassword(8, 0)
                tempBxgOwner.Password = tempPass
                Dim sChangeResult As Boolean
                Dim bgoLink As String = ""
                Dim contactLink As String = ""

                sChangeResult = ChangeUserPassword(bxgOwner.Arvact, bxgOwner.Password, tempBxgOwner.Password)

                If sChangeResult Then
                    ' Dim tpService As New ThirdPartyWS.ThirdPartyServiceClient()
                    Dim data As New Dictionary(Of String, String)

                    data.Add("First_Name", bxgOwner.firstName)
                    data.Add("Last_Name", bxgOwner.lastName)
                    data.Add("ARVACT", bxgOwner.Arvact)
                    data.Add("Email_Address", bxgOwner.Email)
                    data.Add("Temporary_Password", tempPass)
                    ' tpService.SendDataToResponsys("BGO-Website-Password-Reset-V1_Form", data)

                    bxgOwner.Password = tempPass
                    Response.Redirect("forgotPasswordSent.aspx")
                Else

                    lblErrors.Text = "There was a problem sending your password at this time.  Please try again later.  <BR>If the problem persists then please call customer service."
                    lblErrors.Visible = True
                    imgAlert.Visible = True

                End If
            Catch ex As Exception
                lblErrors.Text = "There was a problem sending your password at this time.  Please try again later.  <BR>If the problem persists then please call customer service."
                lblErrors.Visible = True
                imgAlert.Visible = True
            End Try
        End Sub

        Sub fetchOwnerInfoFromOwnerService(ByVal emailAddress as String)

            Dim ownerService As New OwnerWS.OwnerWS1SoapClient
            Try
                bxgOwner = ownerService.fetchOwner("", "", "", emailAddress.Trim(), "")
            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Function GeneratePassword(ByVal length As Integer, _
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
                    chPassword(passwordPos) = _
                            chPunctuations(rndNumber.Next(0, chPunctuations.Length))
                Next
            End If

            Return New String(chPassword)
            'Loop
        End Function


    End Class

End Namespace
