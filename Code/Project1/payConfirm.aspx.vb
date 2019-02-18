Imports System.Collections.Generic

Namespace BluegreenOnline

    Partial Class payConfirm
        Inherits System.Web.UI.Page

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

        Dim approval As String = ""
        Dim Amount As String = ""
        Dim ProjNo As String = ""
        Dim cc_exp_dt As String = ""
        Dim txtQCVV As String = ""
        Dim txtCCNum As String = ""
        Dim LogId As Integer
        Dim ccprocessobj As New CCProcess
        Public paperlessrequested As Boolean
        Public BXGOwner As OwnerWS.Owner

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(Now())
            Response.Expires = 0

            Session("CCFOUR") = Right(Session("txtCCNum"), 4)
            Session("amount") = Session("totalDue")
            Dim dateToCompare As Date = Date.Now

            If Session("BXGOwner") Is Nothing Then
                Response.Redirect("http://" & Request.ServerVariables("HTTP_HOST") & "/default.aspx?sess=timeout", True)
            End If

            BXGOwner = Session("BXGOwner")

            'Only display if project number is 50 and current date > 10/01/2007 and <=2/15/2008
            If Session("PaymentProjNo") = "50" And Date.Compare(dateToCompare, #10/1/2007#) > 0 And Date.Compare(dateToCompare, #2/15/2008#) < 0 Or Date.Compare(dateToCompare, #2/15/2008#) = 0 Then
                Me.pnlPromotion.Visible = False
            End If

            'Paperless
            If BXGOwner.Paperless.PaperlessRequested Then
                paperless.Visible = False

            Else
                paperless.Visible = True

            End If

            Dim Owner As New Bluegreenowner
            Owner = Bluegreenowner.CurrentOwner

            SendConfirmation("00", "failure", "50 started transaction")

            If Session("PaymentType") = "creditcard" Then
                Amount = Session("totalDue")

                Dim expDtMM = Left(Session("cc_exp_dt"), 2)
                Dim expDtYY = Right(Session("cc_exp_dt"), 2)
                Dim expdate1 = Nothing

                If Len(expDtYY) = 2 Then
                    If Len(expDtMM) = 1 Then expDtMM = "0" & expDtMM
                    expdate1 = "20" & expDtYY & expDtMM
                End If

                Dim objPayment As New paymentdetail(Session("cboType"), Session("txtCCNum"), expdate1, Amount, "", Session("txtCCName"))

                If Session("processedProjNo") <> Session("PaymentProjNo") Then

                    'submit the cc info to gateway
                    MaintenanceFeeLog("Insert", 1, "")

                    Dim ccConfirm As paymentdetail = SubmitPayment(Owner, objPayment)

                    If IsNothing(ccConfirm.CONFNUMBER) Then
                        SendConfirmation(Amount, "failure", "58 creditcard failed")
                        MaintenanceFeeLog("Update", 8, ccConfirm.ERRDETAIL)
                        Response.Redirect("payInfo.aspx?msg=procerr")
                    ElseIf ccConfirm.CONFNUMBER.Trim.Length > 3 Then

                        Session("auth_code") = ccConfirm.CONFNUMBER
                        Session("approvalCode") = ccConfirm.CONFNUMBER
                        Session("payamountinstallList") = Session("PaymentPayments")
                        Dim result As String = ccprocessobj.LogTransaction(Owner, ccConfirm)

                        SendConfirmation(Amount, "failure", "55 completed the log transaction" & result)

                        Try
                            MaintenanceFeeLog("Update", 8, result)

                        Catch ex As Exception
                            SendConfirmation(Amount, "failure", "56" & ex.Message.ToString)
                        End Try

                        If Trim(result) <> "Success" Then
                            Try
                                MaintenanceFeeLog("Update", 9, result)

                            Catch ex As Exception
                                SendConfirmation(Amount, "failure", "56-1" & ex.Message.ToString)
                            End Try

                            SendConfirmation(Amount, "failure", "57-1" & result)
                            Response.Redirect("payInfo.aspx?msg=procerr")

                        End If

                        Me.pnlCredit.Visible = True
                        lblAmount.Text = FormatCurrency(Session("totalDue"))
                        lblConfirmation.Text = ccConfirm.CONFNUMBER
                        Me.pnlChecking.Visible = False
                        SendConfirmation(Amount, "failure", "57-2" & result & " " & Session("totalDue") & "  " & ccConfirm.CONFNUMBER)

                    Else

                        SendConfirmation(Amount, "failure", "58 creditcard failed")
                        MaintenanceFeeLog("Update", 8, ccConfirm.ERRDETAIL)
                        Response.Redirect("payInfo.aspx?msg=procerr")

                    End If

                Else
                    SendConfirmation(Amount, "failure", "59 payaccountbal")
                    Response.Redirect("PayAcctBal.aspx", True)

                End If

            Else
                achlblAmount.Text = FormatCurrency(Request.QueryString("amt"))
                Me.pnlChecking.Visible = True
                Me.pnlCredit.Visible = False
            End If

            UcMenu1.CrOwnerMenu()

        End Sub

        Private Function SubmitPayment(ByVal owner As Bluegreenowner, ByVal paydetail As paymentdetail) As paymentdetail

            Dim CVC As New clsDBConnectivityVC

            Try
                'If the owner made the payment in same session don't allow to make another payment
                Try

                    CVC.dbCmnd.CommandText = "uspCheckMaintFeeSettlementBatch"
                    CVC.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                    CVC.dbCmnd.Parameters.Clear()
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Arvact", System.Data.SqlDbType.BigInt)).Value = Convert.ToInt32(owner.OwnerVCNumber)
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@projNum", System.Data.SqlDbType.VarChar)).Value = Session("PaymentProjNo")
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@date", System.Data.SqlDbType.VarChar)).Value = Now.Month & "/" & Now.Day & "/" & Now.Year
                    Dim reader2 As SqlClient.SqlDataReader
                    reader2 = CVC.dbCmnd.ExecuteReader()
                    While reader2.Read

                        If reader2("RECORDS") = "True" Then
                            paydetail.CONFNUMBER = " "
                            Return paydetail
                        End If

                    End While

                    reader2.Close()

                Catch ex As Exception
                    paydetail.ERRDETAIL = ex.Message.ToString()
                    CVC.Close()
                    CVC = Nothing
                    Return paydetail
                Finally

                End Try

                Dim MerchantID As String = ""

                'NEED TO SELECT THE MERCHANT ID HERE

                Try

                    'Dim CVC As New clsDBConnectivityVC
                    Dim reader As SqlClient.SqlDataReader
                    CVC.dbCmnd.CommandText = "uspSelectMerchantID"
                    CVC.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                    CVC.dbCmnd.Parameters.Clear()
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("action", System.Data.SqlDbType.VarChar, 20)).Value = Session("PaymentProjNo")
                    reader = CVC.dbCmnd.ExecuteReader()
                    While reader.Read
                        MerchantID = reader("MerchantID")
                    End While

                    reader.Close()

                Catch Ex As Exception

                    'SendConfirmation(owner, Amount, "failure", "11" & Ex.Message.ToString)
                    paydetail.ERRDETAIL = Ex.Message.ToString
                    paydetail.CONFNUMBER = " "
                    CVC.Close()
                    CVC = Nothing
                    Return paydetail

                Finally

                    CVC.Close()

                End Try

                'validate merchant id
                If IsNothing(MerchantID) Then
                    paydetail.ERRORCODE = "Invalid merchani Id"
                    paydetail.CONFNUMBER = " "
                    Return paydetail

                ElseIf MerchantID.Length < 5 Then
                    paydetail.ERRORCODE = "Invalid merchani Id"
                    paydetail.CONFNUMBER = " "
                    Return paydetail

                Else

                    paydetail.MERCHANTID = MerchantID

                End If

                'process credit card

                Try
                    Dim ccprocessobj As New CCProcess
                    Dim objpayresponse As paymentdetail = ccprocessobj.processPayment(owner, paydetail)
                    paydetail = objpayresponse
                Catch ex As Exception
                    paydetail.ERRORCODE = ex.Message.ToString()
                    paydetail.CONFNUMBER = " "
                    Return paydetail
                End Try


            Catch ex As Exception

                paydetail.ERRORCODE = ex.Message.ToString()
                paydetail.CONFNUMBER = " "
                Return paydetail

            End Try

            Return paydetail

        End Function
      

        Public Sub MaintenanceFeeLog(ByVal status As String, ByVal stepnumber As Integer, ByVal failreason As String)
            Dim CVC As New clsDBConnectivityVC
            Dim drLog As SqlClient.SqlDataReader
            Dim postalcode As String = ""

            If BXGOwner.PostalCode = "" Then
                postalcode = "11111"
            Else
                postalcode = BXGOwner.PostalCode
            End If

            If status = "Insert" Then
                Try
                    CVC.dbCmnd.CommandText = "uspMaintenanceFeeStats"
                    CVC.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                    CVC.dbCmnd.Parameters.Clear()
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Arvact", System.Data.SqlDbType.Int)).Value = BXGOwner.Arvact
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FirstName", System.Data.SqlDbType.VarChar, 30)).Value = Session("txtCCName")
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LastName", System.Data.SqlDbType.VarChar, 30)).Value = ""
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CCBillingCity", System.Data.SqlDbType.VarChar, 30)).Value = BXGOwner.City
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CCBillingState", System.Data.SqlDbType.VarChar, 30)).Value = BXGOwner.StateAbr
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CCBillingZip", System.Data.SqlDbType.VarChar, 30)).Value = postalcode
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CcLast4", System.Data.SqlDbType.VarChar, 4)).Value = Session("CCFOUR")
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TransactionAmt", System.Data.SqlDbType.VarChar, 6)).Value = Session("amount")
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RequestDateTime", System.Data.SqlDbType.DateTime)).Value = DateTime.Today
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@stepnumber", System.Data.SqlDbType.TinyInt)).Value = stepnumber
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Logid", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output

                    drLog = CVC.dbCmnd.ExecuteReader()


                    Try
                        Session("LogiD") = CVC.dbCmnd.Parameters("@Logid").Value
                    Catch ex As Exception

                    End Try



                    drLog.Close()
                Catch ex As Exception
                    'Response.Write(ex.Message)
                Finally
                    CVC.Close()
                    CVC = Nothing

                End Try
            End If

            If status = "Update" Then
                Dim auth_code As String = ""
                Dim TransactionID As String = ""
                Dim responseflag As Integer = 0

                If Not IsNothing(Session("auth_code")) Then
                    auth_code = Session("auth_code")
                End If
                If Not IsNothing(Session("approvalCode")) Then
                    TransactionID = Session("approvalCode")
                    responseflag = 1
                End If
                Try
                    CVC.dbCmnd.CommandText = "uspUpdateMaintenanceFeeStats"
                    CVC.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                    CVC.dbCmnd.Parameters.Clear()
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LogID", System.Data.SqlDbType.Int)).Value = Session("LogiD")
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Arvact", System.Data.SqlDbType.Int)).Value = BXGOwner.Arvact
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AuthCode", System.Data.SqlDbType.VarChar, 6)).Value = auth_code
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FailReason", System.Data.SqlDbType.VarChar, 1000)).Value = failreason
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@StepNumber", System.Data.SqlDbType.Int)).Value = stepnumber
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TransactionID", System.Data.SqlDbType.VarChar, 30)).Value = TransactionID
                    CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ResponseFlag", System.Data.SqlDbType.Char, 1)).Value = responseflag
                    If Not IsNothing(Session("approvalCode")) Then
                        CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@GatewayResponseDate", System.Data.SqlDbType.DateTime)).Value = DateTime.Today
                    End If
                    CVC.dbCmnd.ExecuteNonQuery()

                Catch ex As Exception
                    'Response.Write(ex.Message)
                Finally
                    CVC.Close()
                    CVC = Nothing

                End Try
            End If

        End Sub

        Private Sub SendConfirmation(ByVal _amount As String, ByVal _ResultCode As String, ByVal _transactionInfo As String)
            Dim sTemplate As String
            Dim sEmailBody As String = ""
            Dim EM As New clsEmailer


            If _ResultCode = "success" Then
                'Load email template        
                sTemplate = IO.Path.Combine(Server.MapPath(""), "EmailTemplates\PaymentConfirmed.html")

                Dim sr As New IO.StreamReader(sTemplate)
                Do While Not sr.Peek = -1
                    sEmailBody &= sr.ReadLine()
                Loop
                sr.Close()

                'Replace template with data

                sEmailBody = sEmailBody.Replace("|IMAGEPATH|", ConfigurationSettings.AppSettings("ImagePath") & "")
                sEmailBody = sEmailBody.Replace("|CONFNUM|", Session("auth_code") & "")
                sEmailBody = sEmailBody.Replace("|NAME|", BXGOwner.fullName & "")
                sEmailBody = sEmailBody.Replace("|AMOUNT|", FormatCurrency(_amount, 2))
                sEmailBody = sEmailBody.Replace("|ARVACT|", BXGOwner.Arvact & "")

                'Send the email to Owner
                EM.ToEmail = BXGOwner.Email
                EM.BCCEmail = "007@bluegreencorp.com"
                EM.FromEmail = """Bluegreen Resorts"" <Resorts@bluegreenonline.com>"
                EM.Subject = "Payment Confirmation from Bluegreen Resorts"
                EM.BodyText = sEmailBody.Replace("™", "&trade;")
                EM.emailBodyFormat = Mail.MailFormat.Html
                EM.sendEmail()

            ElseIf _ResultCode = "failure" Then
                Try
                    Dim eBody As New StringBuilder
                    eBody.Append("The processing a transaction: <br /><ul> ")
                    eBody.Append("<li>" & BXGOwner.fullName & "</li>")
                    eBody.Append("<li>" & BXGOwner.Arvact & "</li>")
                    eBody.Append(Now())
                    eBody.Append("<li>" & _transactionInfo & "</li>")
                    eBody.Append("<li>" & Now() & "</li>")
                    EM.ToEmail = "krishna.guda@bluegreencorp.com"
                    EM.FromEmail = "Resorts@bluegreenonline.com"
                    EM.Subject = "Error processing Credit Card"
                    EM.BodyText = eBody.ToString
                    EM.emailBodyFormat = Mail.MailFormat.Html
                    EM.sendEmail()

                Catch ex As Exception

                End Try

            End If

        End Sub

    End Class

End Namespace

