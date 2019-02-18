Imports BXGDiagnostics
Imports System.Diagnostics
Imports System.Text
Imports System.Data.SqlClient.SqlConnection
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports BluegreenOnline

Namespace BluegreenOnline
    Partial Class payAcctBal
        Inherits System.Web.UI.Page

        Public BXGOwner As OwnerWS.Owner

#Region "FIELDS"
        Private objLogging As BXGDiagnostics.EventLogging
        Public srvRoot As String
        Public totalDue As Double
        Public ARDAmnt As Double
        Public PayAmount As Double
        Public newAmount As Double
        Public accTransferList As New List(Of Account)
        Public accAcountNumberList As New List(Of String)
        Public payamountList As New List(Of Double)
        Public bIncreasedFees As Boolean
        Public bColStat As Boolean
        Public nameCollection As New MultipleValueDictionary(Of String, String)
        Public instTotalAmount As String
        Public jQuery As String = System.Configuration.ConfigurationManager.AppSettings("googleJQuery")
        Dim count As Integer = 0
        Dim valid As Boolean = False

#End Region

#Region "GRID INITIALIZE"

        Sub loadGridData()
            Dim projNm As New List(Of String)
            'List containing unique project numbers
            Dim distNumList As IList = GetDistinctProjNum()
            For index As Integer = 0 To distNumList.Count - 1
                Dim dv As DataView = PayInfoDetail(distNumList(index), 0)
                If Not IsNothing(dv) Then
                    If dv.Count > 0 Then
                        projNm.Add(distNumList(index).ToString())
                    End If
                Else
                    Continue For
                End If
            Next
            'populating outer grid with unique proj. numbers:(distNumList) 
            gvOUTER.DataSource = projNm
            gvOUTER.DataBind()
            'If nothing is returned then the account is pending payments 
            If gvOUTER.Rows.Count = 0 Then
                If Not pnlDoesNotQualify.Visible Then
                    Me.pnlPending.Visible = True
                    Me.pnlPaidinFull.Visible = False
                    Me.btnPrepayment.Visible = False
                    Me.pnlPayInfo.Visible = False
                    Me.pnlPayments.Visible = False
                    Me.pnlARDA.Visible = False
                    Me.vegasPromo.Visible = False
                    Exit Sub
                End If
            End If

            Dim cnt As Integer = 0
            Dim bAllowPayment As Boolean
            Dim ProjNum As String
            Dim arslty As String = String.Empty
            Dim strAcctLink As String
            Dim acctLink As String


            'The accounts in array AccntNoPay() should not have the ability to be paid online.
            Dim AccntNoPay() As Integer = {2, 3, 4, 7, 10, 12, 13, 16, 17, 27, 29, 32, 35, 37, 38, 44, 45, 46, 47, 8, 49, 51, 52, 55, 56, 57, 58, 59, 65, 69, 70, 71, 72, 75, 76, 77, 81, 82, 85, 94, 95, 97, 98, 99}
            Dim lAccntNoPay As New List(Of Integer)
            lAccntNoPay.AddRange(AccntNoPay)

            '*************************************************************************
            ' BEGIN LOOP ON OUTER GRID                                               *
            '*************************************************************************
            For Each row As GridViewRow In gvOUTER.Rows

                totalDue = 0
                ARDAmnt = 0

                'put the projectNumber and the projectName in the top of the outergrid.
                Dim labelText As Label = CType(row.FindControl("lblPRONAME"), Label)
                'Retrieving name specific to project number from name collection dictionary in a list 
                'distNumList.Item(cnt).ToString() : this statement is used to retrieve a unique project number per row in outer grid
                'distNumList is the initial datasource of the outer grid
                Dim names As List(Of String) = Me.nameCollection.getvaluesByKey(projNm.Item(cnt).ToString())
                'displaying name in header label in outer grid 
                labelText.Text = names(0)
                Dim hfPROJNUM As HiddenField = CType(row.FindControl("hfPROJNUM"), HiddenField)
                Dim rblPayMethodACH As RadioButtonList = CType(row.FindControl("rblPAYMENTMETHOD"), RadioButtonList) ' radiobutton list containing radio button payments
                Dim rblPayMethodACHCC As RadioButtonList = CType(row.FindControl("rblPAYMENTMETHOD2"), RadioButtonList)


                ProjNum = projNm.Item(cnt).ToString()
                'Session variable containing the project name 
                Session("PaymentProjName") = labelText.Text
                hfPROJNUM.Value = ProjNum
                'if project 50 only credit card payment allowed
                If ProjNum <> 50 Then
                    rblPayMethodACH.Items.RemoveAt(1)
                End If


                'Owner is not eligible for installment option remove the installment option from the radio button list
                If Session("BXGOwner") IsNot Nothing Then

                    If Not BXGOwner.InstallmentPlan(0).InstallmentPaymentEligible = "1" Then
                        rblPayMethodACH.Items.RemoveAt(0)
                    End If

                    If BXGOwner.InstallmentPlan(0).InstallmentStatus = "ID" Or BXGOwner.InstallmentPlan(0).InstallmentStatus = "IS" Then
                        rblPayMethodACH.Items.RemoveAt(0)
                    End If

                Else
                    rblPayMethodACH.Items.RemoveAt(0)
                End If

                'The accounts in array AccntNoPay() should not have the ability to be paid online.
                If lAccntNoPay.Contains(ProjNum) Then
                    bAllowPayment = False
                Else
                    bAllowPayment = True
                End If

                Dim hl2 As HyperLink = CType(row.FindControl("viewst"), HyperLink)
                Dim ownerNumber As String = Session("OwnerNumber")
                Dim statementLink As String = "BillStatementWait.aspx?parmnum=7&HED1=Statement Date&VAR1=" & (Now.Month).ToString & "/" & (Now.Day).ToString & "/" & (Now.Year).ToString & "&PROC2=SP_LIST_ALL_ASSOCIATIONS&HED2=Association&VAR2=" & hfPROJNUM.Value & "&HED3=Comment1&VAR3=&HED4=Comment2&VAR4=&PROC5=USPRPTOPTDETAILORSUMMARY&HED5=Detail/Summary&VAR5=(Detail)&HED6=OwnerNumber&VAR6=" & ownerNumber & "&HED7=&VAR7="

                hl2.Text = "View Statement"
                hl2.NavigateUrl = statementLink
                hl2.Target = "_blank"
                hl2.Visible = True

                If bAllowPayment = False Then

                    Dim PayMethodRBL As RadioButtonList = CType(row.FindControl("rblPAYMENTMETHOD"), RadioButtonList)
                    Dim MethodPayLBL As Label = CType(row.FindControl("lblPaymentMethod"), Label)
                    Dim SubmitPAY As ImageButton = CType(MethodPayLBL.FindControl("submitPayment"), ImageButton)

                    SubmitPAY.Visible = False

                    PayMethodRBL.Visible = False

                    'MethodPayLBL.Visible = False
                    MethodPayLBL.ForeColor = Drawing.Color.Red
                    MethodPayLBL.Text = "<img src=./images/payByPhone.GIF />"

                End If

                'Populate nested grid
                Dim gvINNER As GridView = CType(row.FindControl("gvDETAIL"), GridView)
                '  Dim proNum As String = GetUserPayInfo().Tables(0).Rows(cnt).Item("PROJNUM").ToString()
                Dim proNum As String = ProjNum
                gvINNER.DataSource = PayInfoDetail(ProjNum, totalDue)
                gvINNER.DataBind()

                'if gVINNER contains no rows due to bad collection status, do not display 
                'associated controls
                If gvINNER.Rows.Count = 0 Then

                    Dim tbl As HtmlTable = CType(row.FindControl("headertable"), HtmlTable)
                    tbl.Visible = False
                    Dim rbl As RadioButtonList = CType(row.FindControl("rblPAYMENTMETHOD"), RadioButtonList)
                    rbl.Visible = False
                    Dim tbTotalDue As TextBox = CType(row.FindControl("TotalDue"), TextBox)
                    tbTotalDue.Visible = False
                    Dim submitButton As ImageButton = CType(row.FindControl("submitPayment"), ImageButton)
                    submitButton.Visible = False
                    Dim lblTotalDue As Label = CType(row.FindControl("lblTotalDue"), Label)
                    lblTotalDue.Visible = False
                    Dim lblPayMethod As Label = CType(row.FindControl("lblPaymentMethod"), Label)
                    lblPayMethod.Visible = False
                Else
                    Session("isExistingProj") = True
                End If

                Dim ardaList As New List(Of Double)
                For Each rowINNER As GridViewRow In gvINNER.Rows

                    Dim hf As HiddenField = CType(rowINNER.FindControl("hfARDA"), HiddenField)
                    Dim hfColSta As HiddenField = CType(rowINNER.FindControl("hfColStat"), HiddenField)
                    Dim cb As CheckBox = CType(rowINNER.FindControl("cbARDA"), CheckBox)
                    Dim payamntBox As TextBox = CType(cb.FindControl("PayAmount"), TextBox)
                    Dim hfAcct As HiddenField = CType(rowINNER.FindControl("hfACCT"), HiddenField)
                    Dim hfProjN As HiddenField = CType(rowINNER.FindControl("hfProjNum"), HiddenField)
                    Dim hlAcctN As HyperLink = CType(rowINNER.FindControl("hlAcctNo"), HyperLink)
                    Dim hfARSLTYPE As HiddenField = CType(rowINNER.FindControl("hfARSLTY"), HiddenField)
                    Dim ardamountField As HiddenField = CType(rowINNER.FindControl("hfARDAMT1"), HiddenField)
                    Dim ardaAmount As Double = CType(ardamountField.Value, Double)

                    If payamntBox.Text <= 0 Then
                        payamntBox.BorderStyle = BorderStyle.None
                        payamntBox.BackColor = Drawing.Color.Transparent
                        payamntBox.ReadOnly = True
                        cb.Enabled = False
                        cb.Checked = False
                    End If

                    Try
                        'arslty = GetUserARSLTY(hfAcct.Value, hfProjN.Value)
                        arslty = hfARSLTYPE.Value

                        arslty = Convert.ToString(arslty).ToLower
                    Catch ex As Exception

                        arslty = ""

                    End Try

                    'Determine help link for account. (copied&pasted from previous version due to time restriction)
                    If arslty = "c" Then
                        acctLink = "<a class='textlink' "
                        acctLink += "onclick="
                        acctLink += """javascript:newWin=window.open(" & "'helpWin_A.aspx?>"
                        acctLink += "','newWinPC','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=239,height=402,left=50,top=50');return false;"""
                        acctLink += "href=""#"">"
                        acctLink += hlAcctN.Text
                        acctLink += "</a>"
                        hlAcctN.Text = acctLink
                    ElseIf arslty = "n" Then
                        acctLink = "<a class='textlink' "
                        acctLink += "onclick="
                        acctLink += """javascript:newWin=window.open(" & "'helpWin_B.aspx?>"
                        acctLink += "','newWinPC','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=245,height=420,left=50,top=50');return false;"""
                        acctLink += "href=""#"">"
                        acctLink += hlAcctN.Text
                        acctLink += "</a>"
                        hlAcctN.Text = acctLink
                    ElseIf arslty = "b" Or arslty = "g" Then
                        acctLink = "<a class='textlink' "
                        acctLink += "onclick="
                        acctLink += """javascript:newWin=window.open(" & "'helpWin_C.aspx?>"
                        acctLink += "','newWinPC','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=245,height=420,left=50,top=50');return false;"""
                        acctLink += "href=""#"">"
                        acctLink += hlAcctN.Text
                        acctLink += "</a>"
                        hlAcctN.Text = acctLink
                    ElseIf arslty = "p" Or arslty = "q" Then
                        acctLink = " <a class='textlink' "
                        acctLink += "onclick="
                        acctLink += """javascript:newWin=window.open(" & "'helpWin_D.aspx?>"
                        acctLink += "','newWinPC','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=245,height=420,left=50,top=50');return false;"""
                        acctLink += "href=""#"">"
                        acctLink += hlAcctN.Text
                        acctLink += "</a>"
                        hlAcctN.Text = acctLink
                    ElseIf arslty = "y" Or arslty = "z" Or arslty = "a" Or arslty = "u" Then
                        acctLink = " <a class='textlink' "
                        acctLink += "onclick="
                        acctLink += """javascript:newWin=window.open(" & "'helpWin_E.aspx?"
                        acctLink += "','newWinPC','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=245,height=420,left=50,top=50');return false;"""
                        acctLink += "href=""#"">"
                        acctLink += hlAcctN.Text
                        acctLink += "</a>"
                        hlAcctN.Text = acctLink
                        bIncreasedFees = True
                    End If

                    'Disable checkbox if online payment are not allowed for specified account
                    If bAllowPayment = False Then
                        payamntBox.BorderStyle = BorderStyle.None
                        payamntBox.BackColor = Drawing.Color.Transparent
                        payamntBox.Enabled = False
                        cb.Enabled = False
                    End If

                    'set ardaFlag Conditions 
                    If ardaAmount > 0 Then
                        cb.Visible = True
                        cb.Checked = True
                        cb.Visible = True
                        Dim increment As Double = CType(payamntBox.Text, Double)
                        payamntBox.Text = String.Format("{0:n}", increment)
                    End If

                    'End If
                Next 'end work on nested grid

                'Populate Total Due
                Dim txtPayAmount As TextBox = CType(row.FindControl("totalDue"), TextBox)
                txtPayAmount.Text = String.Format("{0:c}", totalDue)
                Dim hlActMsg As HyperLink = CType(row.FindControl("hlAcctMsg"), HyperLink)

                '
                '
                'YOU WANT TO BE HERE
                '                '
                '

                '    If bIncreasedFees = True Then
                '        'hlActMsg.Text = strAcctLink
                '        hlActMsg.Visible = False
                '    Else
                '        hlActMsg.Visible = False
                '    End If

                '    If Session("hlAcctMsg") = "visible" Then
                '        hlActMsg.Visible = True
                '    End If

                If InStr("N,P,Q,Y,Z,A,U", arslty.ToUpper()) > 0 Then
                    hlActMsg.Visible = True
                Else
                    hlActMsg.Visible = False
                End If

                cnt += 1
            Next
        End Sub
#End Region

#Region "PAGE LOAD"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Session("BXGOwner") Is Nothing Then
                Session("_path_info") = Request.RawUrl
                Response.Redirect("http://" & Request.ServerVariables("HTTP_HOST") & "/default.aspx?sess=timeout", True)
            End If

            BXGOwner = Session("BXGOwner")
            Session("pmntTyp") = Nothing
            'redirect sampler to mortgagesummary page
            If Not IsNothing(BXGOwner.User(0)) Then
                If BXGOwner.User(0).isSampler Then
                    Response.Redirect("mortgagesummary.aspx")
                End If
            End If

            'verify the owner installment plan eligibility and installment plan status

            If BXGOwner.InstallmentPlan(0).InstallmentPaymentEligible = "1" Then
                If BXGOwner.InstallmentPlan(0).InstallmentStatus = "IP" Or BXGOwner.InstallmentPlan(0).InstallmentStatus = "IC" Then

                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("bxgwebsecureURL") & "owner/payinstallpmts.aspx")

                End If

                If BXGOwner.InstallmentPlan(0).InstallmentStatus = "IS" Then
                    Session("PaymentType") = "installmentsus"
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("bxgwebsecureURL") & "owner/payInstallPlan.aspx")

                End If

            End If

            If BXGOwner.InstallmentPlan(0).InstallmentStatus = "IS" Then
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("bxgwebsecureURL") & "owner/acctstatus.aspx")
            End If

            If BXGOwner.InstallmentPlan(0).InstallmentStatus = "IC" Then

                Me.pnlPending.Visible = False
                Me.pnlPaidinFull.Visible = True
                btnPrepayment.Visible = True
                Me.pnlPayInfo.Visible = False
                Me.pnlPayments.Visible = False
                Me.pnlARDA.Visible = False
                Me.vegasPromo.Visible = False

                Exit Sub

            End If

            If Session("OwnerNumber") <> "" Then

                Dim bAllowPayments As Boolean = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("AllowPayments"))
                If bAllowPayments Then

                    Try
                        If IsAS400Available() Then
                            'objLogging = New EventLogging("BlueGreenOnline", "payAcctBal")
                            Response.Cache.SetCacheability(HttpCacheability.NoCache)
                            Response.Cache.SetExpires(Now())
                            Response.Expires = 0
                            If IsPostBack Then
                                processPostBack()
                            ElseIf Not IsPostBack Then 'add
                                'Does this user owe any money?
                                If Session("OwnerPaymentBalance") <= 0 And Session("OwnerPastDue") = False Then
                                    Me.vegasPromo.Visible = False
                                    Me.pnlPaidinFull.Visible = True
                                    Me.btnPrepayment.Visible = True
                                    Exit Sub
                                Else
                                    Me.pnlPayInfo.Visible = True
                                    Me.pnlARDA.Visible = True
                                    Me.pnlPayments.Visible = True

                                    loadGridData()


                                End If 'end if Session("ownernumber")
                            End If 'end if POSTBACK
                        End If 'End if as400isAvailable
                    Catch ex As Exception
                        If Not ex.Message.Contains("Conversion from string") Then
                            Me.pnlPaidinFull.Visible = False
                            Me.btnPrepayment.Visible = False
                            Me.pnlPayInfo.Visible = False
                            Me.pnlPayments.Visible = False
                            Me.vegasPromo.Visible = False
                            Me.pnlPending.Visible = False
                            Me.pnlARDA.Visible = False
                            Me.pnlErrorAccessing500.Visible = True

                        End If

                        Response.Write(ex.Message)
                    End Try

                End If 'End if bAllowPayments
            End If
        End Sub
#End Region

#Region "SUBS/EVENTS "

        Sub processPostBack()
            'loop through each row of the outher grid 
            For Each row As GridViewRow In gvOUTER.Rows
                Dim amntCollection As New List(Of Double)
                Session("amntColl") = New List(Of Double)

                'newAmount must be set to 0 for each iteration to calculate accurate total for each project group
                newAmount = 0.0
                'in each row of the outer grid, retrieve the nested grid
                Dim gvINNER As GridView = CType(row.FindControl("gvDETAIL"), GridView)
                For Each inrow As GridViewRow In gvINNER.Rows
                    'in each row of the nested grid, retrieve the payamount textbox
                    Dim newpayamount As TextBox = CType(inrow.FindControl("PayAmount"), TextBox)
                    String.Format("{0:N2}", newpayamount.Text)
                    Dim mAmt As Double

                    ' to check if  new payment amount entered is not numeric 
                    If Not Double.TryParse(newpayamount.Text, mAmt) Then
                        lblErrors.Visible = True
                        lblErrors.Text = "<table><tr><td valign=top><img src=./images/alert.gif /></td><td><font color=red>Input value must be numeric.</font></td></tr></table>"
                        GoTo nextRow 'if not numeric go to next row.
                    End If

                    'store all collected amounts in list
                    amntCollection.Add(CType(newpayamount.Text, Double))
nextRow:

                Next
                Session("amntColl") = amntCollection
                For Each amnt As Double In amntCollection
                    'compute new total
                    newAmount += amnt
                Next
                'after completing nested grid operations, set the new totalbox text property with proper string format
                Dim totalbox As TextBox = CType(row.FindControl("TotalDue"), TextBox)
                totalbox.Text = String.Format("{0:N2}", newAmount)

            Next

        End Sub

        Public Function GetUserPayInfo() As DataSet
            Dim ds As DataSet = New DataSet()
            Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("bxgwebDBConnectionVC"))
            Dim cmd As New SqlCommand("sp_maint_fee_get_col_as400", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@arvact", SqlDbType.VarChar, 6).Value = Session("OwnerNumber")
            cmd.Parameters.Add("@ProjNo", SqlDbType.Int, 4).Value = 0
            cmd.Parameters.Add("@action", SqlDbType.VarChar, 5).Value = "line"
            Dim adpt As New SqlDataAdapter(cmd)
            adpt.Fill(ds, "Fees")
            'for each row in the table fees, add a unique proj number with its associated names


            For Each account In BXGOwner.maintFees()
                Me.nameCollection.Add(account.projnum.ToString(), account.proname.ToString())
            Next

            'For Each row As DataRow In ds.Tables("Fees").Rows
            '    Me.nameCollection.Add(row("PROJNUM").ToString(), row("PRONAME").ToString())
            'Next

            Return ds
        End Function
        ' 
        Public Function UserAccountGoodStanding(ByVal col As String) As Boolean
            ' col is collection code number from webmain1(as400)
            ' return true is good standing otherwise it's bad (don't show the record
            'dim xx as boolean =UserAccountGoodStanding('41')

            Dim CVC As New clsDBConnectivityVC

            Try

                Dim drAcct As System.Data.SqlClient.SqlDataReader
                Dim iResultCode As Integer = 0

                CVC.dbCmnd.CommandText = "uspOwnerAccountStatusVerify"
                CVC.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                CVC.dbCmnd.Parameters.Clear()
                CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@col", System.Data.SqlDbType.VarChar, 4)).Value = col

                drAcct = CVC.dbCmnd.ExecuteReader()

                While drAcct.Read()
                    iResultCode = drAcct("status")
                End While

                drAcct.Close()
                drAcct = Nothing

                CVC.Close()
                CVC = Nothing

                If iResultCode = 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                CVC.Close()
                CVC = Nothing

                Return True
            End Try
        End Function

        Public Function GetDistinctProjNum() As IList
            Dim ds As DataSet = GetUserPayInfo()
            Dim projNumList As IList = SelectDistinct(ds.Tables(0), "projNum")
            Return projNumList
        End Function

        Public Function GetDistinctProjNames() As IList
            Dim ds As DataSet = GetUserPayInfo()
            Dim projNameList As IList = SelectDistinct(ds.Tables(0), "PRONAME")
            Return projNameList
        End Function

        Public Function PayInfoDetail(ByVal e As String, ByRef f As Double) As DataView

            Dim PastDue As Double
            Dim i As Integer = 0 'Counter
            Dim ds As DataSet = GetUserPayInfo()
            Dim myView As DataView

            myView = ds.Tables("Fees").DefaultView
            myView.RowFilter = "ProjNum = " & e


            '####################################################################
            'VEGAS promotion
            'If the project number is 50 then we need to display the banner for the vegas promo
            'this promo will run between 10/7/2007 and 02/15/2008

            'If (DateTime.Compare(Date.Now, #2/15/2008#) <= 0) And (e = 50) Then
            '    Me.vegasPromo.Visible = f
            'Else
            '    Me.vegasPromo.Visible = False
            'End If
            '####################################################################

            'Create and add the new columns we need that are not in the original table
            'these will hold the calculated values
            Dim dcPastDue As New DataColumn("PastDue", GetType(Double))
            Dim dcPayAmount As New DataColumn("PayAmount", GetType(Double))
            Dim dcARDA As New DataColumn("ARDA", GetType(String))
            Dim bColCode As Boolean
            Dim COLSTAT As String

            If e = "50" Then
                For Each account In BXGOwner.Accounts()
                    For rowIndex As Integer = 0 To myView.Count() - 1
                        If account.AcctNum.Trim() = myView.Item(rowIndex)("ACCT").ToString().Trim() And (account.ProjectHome = "51" Or account.ProjectHome = "52") Then
                            myView.Item(rowIndex).Delete()
                            Exit For
                        End If
                    Next
                Next
            End If
            Dim Counter As Integer
            Counter = myView.Count() - 1
            'Dim RowCount As Integer = myView.Count() - 1
            Dim index As Integer = 0
            Dim Credit As Double = 0

            While Counter >= index
                COLSTAT = myView.Item(Counter)("COLSTAS")
                bColCode = UserAccountGoodStanding(COLSTAT)
                If bColCode = False Then
                    myView.Delete(Counter)
                    If Session("isExistingProj") = True Then
                        Return Nothing
                        Exit Function
                    End If

                    gvOUTER.Visible = False
                    Me.pnlPaidinFull.Visible = False
                    Me.btnPrepayment.Visible = False
                    Me.pnlPayInfo.Visible = False
                    Me.pnlPayments.Visible = False
                    Me.vegasPromo.Visible = False
                    Me.pnlErrorAccessing500.Visible = False
                    Me.pnlARDA.Visible = False
                    Me.pnlDoesNotQualify.Visible = True
                    Me.pnlPending.Visible = False
                    Return Nothing
                    Exit Function
                Else
                    gvOUTER.Visible = True
                    Me.pnlPaidinFull.Visible = False
                    Me.btnPrepayment.Visible = False
                    Me.pnlPayInfo.Visible = True
                    Me.pnlPayments.Visible = True
                    Me.vegasPromo.Visible = False
                    Me.pnlErrorAccessing500.Visible = False
                    Me.pnlARDA.Visible = True
                    Me.pnlDoesNotQualify.Visible = False
                End If
                Counter -= 1
            End While

            'add the columns to the view
            myView.Table.Columns.Add(dcPastDue)
            myView.Table.Columns.Add(dcPayAmount)
            myView.Table.Columns.Add(dcARDA)

            ' Try
            Dim t = myView.Count
            For Each dv As DataRowView In myView
                Try
                    'Calculate totals for each account association
                    PastDue = (dv("BAL120") + dv("BAL90") + dv("BAL60") + dv("BAL30"))
                    Session("PastDue") = PastDue
                    PayAmount = (PastDue + dv("BALCU"))

                    If dv("ARDAFL") = "Y" Then

                        Session("hlAcctMsg") = "visible"

                    End If

                    'Increment the total due for each account
                    totalDue += PayAmount
                    ARDAmnt += dv("ARDAMT1")


                    'add the values to the table
                    dv("PastDue") = PastDue
                    dv("PayAmount") = PayAmount
                    'i += 1

                    'End If
                Catch ex As Exception

                End Try

            Next

            'If TOTAL ARDA = TOTAL DUE THEN DON'T DISPLAY ANYTHING
            If bColCode = False Then
                If totalDue = ARDAmnt Then
                    Me.gvOUTER.Visible = False
                    Me.pnlPaidinFull.Visible = False
                    Me.btnPrepayment.Visible = False
                    Me.pnlPayInfo.Visible = False
                    Me.pnlPayments.Visible = False
                    Me.vegasPromo.Visible = False
                    Me.pnlErrorAccessing500.Visible = False
                    Me.pnlARDA.Visible = False
                    Me.pnlDoesNotQualify.Visible = True
                    Me.pnlPending.Visible = False
                    Return Nothing
                    Exit Function
                End If
            End If

            If myView.Count = 0 Then
                gvOUTER.Visible = False
                Me.pnlPaidinFull.Visible = False
                Me.btnPrepayment.Visible = False
                Me.pnlPayInfo.Visible = False
                Me.pnlPayments.Visible = False
                Me.vegasPromo.Visible = False
                Me.pnlErrorAccessing500.Visible = False
                Me.pnlARDA.Visible = False
                Me.pnlDoesNotQualify.Visible = True
                Me.pnlPending.Visible = False
                Return Nothing
                Exit Function
            Else
                gvOUTER.Visible = True
                Me.pnlDoesNotQualify.Visible = False
            End If

            'This is where the problem is
            If totalDue <= ARDAmnt Then
                gvOUTER.Visible = False
                Me.pnlPaidinFull.Visible = True
                Me.btnPrepayment.Visible = True
                Me.pnlPayInfo.Visible = False
                Me.pnlPayments.Visible = False
                Me.vegasPromo.Visible = False
                Me.pnlErrorAccessing500.Visible = False
                Me.pnlARDA.Visible = False
                Me.pnlDoesNotQualify.Visible = False
                Return Nothing
                Exit Function
            End If

            Return myView

        End Function

        'Executes a select distinct query on a dataset
        Public Function SelectDistinct(ByRef tX As DataTable, ByVal strField As String) As IList
            Dim rQuery() As DataRow
            Dim lRET As New Collection
            Dim i As Integer
            Dim j As Integer

            'Sort the DataTable by the Field
            rQuery = tX.Select("", strField & " ASC")

            Select Case rQuery.GetUpperBound(0)
                Case Is = -1
                    'no rows
                    Exit Select
                Case Is = 0
                    '1 row
                    lRET.Add(CType(rQuery.GetValue(0), DataRow).Item(strField))
                Case Else
                    'more than 1 row
                    lRET.Add(CType(rQuery.GetValue(0), DataRow).Item(strField))
                    j = 1
                    For i = 1 To rQuery.GetUpperBound(0)
                        If rQuery(i).Item(strField) <> lRET.Item(j) Then
                            lRET.Add(CType(rQuery.GetValue(i), DataRow).Item(strField))
                            j += 1
                        End If
                    Next
            End Select
            'function return
            Return lRET

        End Function

        Dim totalToValidate As Double = 0.0

        Sub CheckPaymentNotZero(ByVal sender As Object, ByVal args As ServerValidateEventArgs)
            'loop through each row of the outher grid (Make sure that the total is collected before validation occurs
            Dim totalAmount As New List(Of Double)
            For Each row As GridViewRow In gvOUTER.Rows
                Dim amntCollection As New List(Of Double)
                Dim c As CustomValidator = CType(sender, CustomValidator)
                'newAmount must be set to 0 for each iteration to calculate accurate total for each project group
                newAmount = 0.0
                'in each row of the outer grid, retrieve the nested grid
                Dim gvINNER As GridView = CType(row.FindControl("gvDETAIL"), GridView)
                For Each inrow As GridViewRow In gvINNER.Rows
                    'in each row of the nested grid, retrieve the payamount textbox
                    Dim newpayamount As TextBox = CType(inrow.FindControl("PayAmount"), TextBox)
                    String.Format("{0:N2}", newpayamount.Text)
                    'store all collected amounts in list
                    Try 'make sre that input is numeric; otherwise, throw an exception
                        amntCollection.Add(CType(newpayamount.Text, Double))
                    Catch ex As Exception
                        c.ErrorMessage = "<font color=red>Input value must be numeric</font>"
                        args.IsValid = False
                        Exit Sub
                    End Try
                Next

                For Each amnt As Double In amntCollection
                    'compute new total by retrieving amounts from list 
                    newAmount += amnt
                Next
                'after completing nested grid operations, set the new totalbox text property with proper string format
                Dim totalbox As TextBox = CType(row.FindControl("TotalDue"), TextBox)
                totalbox.Text = String.Format("{0:N2}", newAmount)
                If gvINNER.Rows.Count > 0 Then
                    totalAmount.Add(newAmount)
                End If
            Next
            Dim cv As CustomValidator = CType(sender, CustomValidator)
            For Each amnt As Double In totalAmount
                If amnt <= 0.0 Then
                    If count < 1 Then
                        cv.Text = "*"
                        cv.ErrorMessage = "<font color=red>The payment amount cannot be zero.</font>"
                        count = count + 1
                        If lblErrors.Visible Then
                            lblErrors.Visible = False
                            lblErrors.Text = ""
                        End If
                    End If
                    args.IsValid = False
                    Exit Sub
                End If
            Next
            args.IsValid = True

        End Sub

        Sub CheckPaymentNotLess(ByVal sender As Object, ByVal args As ServerValidateEventArgs)
            'loop through each row of the outher grid (Make sure that the total is collected before validation occurs)
            For Each row As GridViewRow In gvOUTER.Rows
                Dim amntCollection As New List(Of Double)
                Dim c As CustomValidator = CType(sender, CustomValidator)
                'newAmount must be set to 0 for each iteration to calculate accurate total for each project group
                newAmount = 0.0
                'in each row of the outer grid, retrieve the nested grid
                Dim gvINNER As GridView = CType(row.FindControl("gvDETAIL"), GridView)
                For Each inrow As GridViewRow In gvINNER.Rows
                    'in each row of the nested grid, retrieve the payamount textbox
                    Dim newpayamount As TextBox = CType(inrow.FindControl("PayAmount"), TextBox)
                    String.Format("{0:N2}", newpayamount.Text)
                    'store all collected amounts in list
                    Try 'make sre that input is numeric; otherwise, throw an exception
                        amntCollection.Add(CType(newpayamount.Text, Double))
                    Catch ex As Exception
                        c.ErrorMessage = "<font color=red>Input value must be numeric</font>"
                        args.IsValid = False
                        Exit Sub
                    End Try
                Next

                For Each amnt As Double In amntCollection
                    'compute new total by retrieving amounts from list 
                    newAmount += amnt
                Next
                'after completing nested grid operations, set the new totalbox text property with proper string format
                Dim totalbox As TextBox = CType(row.FindControl("TotalDue"), TextBox)
                totalbox.Text = String.Format("{0:N2}", newAmount)
            Next
            Dim cv As CustomValidator = CType(sender, CustomValidator)
            'if total amount is 0 display error
            If newAmount <= 0.0 Then
                cv.Text = "*"
                cv.ErrorMessage = "<font color=red>The payment amount cannot be zero.</font>"
                args.IsValid = False
                Exit Sub
            End If
            args.IsValid = True

        End Sub

        Sub CheckPaymentNotOver(ByVal sender As Object, ByVal args As ServerValidateEventArgs)
            'retrieving the validator that sent the error
            Dim cv As CustomValidator = CType(sender, CustomValidator)
            Dim ardaBox As CheckBox = CType(cv.FindControl("cbARDA"), CheckBox)
            'finding the textbox associated to that validator object
            Dim tb As TextBox = CType(cv.FindControl("PayAmount"), TextBox)
            Try ' Purposely throw an exception if user input is not numeric
                Double.Parse(tb.Text)
            Catch ex As Exception ' do nothing if parsing exception is thrown to avoid conflict with validato in charge of parsing
                cv.Text = "" ' make custom val text null so it doesn't repeat for each validation event per textbox
                If Not cv.ErrorMessage = "" Then
                    cv.ErrorMessage = ""
                End If
                args.IsValid = False
                Exit Sub
            End Try
            'retrieving the hidden field associated to that validator(it contains the maxPay amount)
            Dim hf2 As HiddenField = CType(cv.FindControl("hfPayment"), HiddenField)
            Dim maxRange As Double = CType(hf2.Value, Double)
            Dim Range As Double = CType(hf2.Value, Double)

            If ardaBox.Checked Then
                Dim amountToValidate As Double = CType(tb.Text, Double)
                If amountToValidate > CType(maxRange, Double) Then
                    args.IsValid = False
                    cv.Text = "*"
                    cv.ErrorMessage = "<font color=red>dollar amount cannot be more than payment amount due.</font>"
                    Exit Sub
                End If
            Else
                Dim amountToValidate As Double = CType(tb.Text, Double)
                If amountToValidate > CType(Range, Double) Then 'CType(hf2.Value, Double) Then
                    args.IsValid = False
                    cv.Text = "*"
                    cv.ErrorMessage = "<font color=red>dollar amount cannot be more than payment amount due.</font>"
                    Exit Sub
                End If
            End If
            args.IsValid = True
        End Sub
        Function checkZeroAmount(ByVal projName As String) As Boolean
            'loop through each row of the outher grid (Make sure that the total is collected before validation occurs
            ' Dim totalAmount As New List(Of Double)
            For Each row As GridViewRow In gvOUTER.Rows
                Dim labelText As Label = CType(row.FindControl("lblPRONAME"), Label)

                Dim amntCollection As New List(Of Double)
                'check for which project you arewilling to pay
                If labelText.Text.Trim() = projName.Trim() Then

                    'newAmount must be set to 0 for each iteration to calculate accurate total for each project group
                    newAmount = 0.0
                    'in each row of the outer grid, retrieve the nested grid
                    Dim gvINNER As GridView = CType(row.FindControl("gvDETAIL"), GridView)
                    For Each inrow As GridViewRow In gvINNER.Rows
                        'in each row of the nested grid, retrieve the payamount textbox
                        Dim newpayamount As TextBox = CType(inrow.FindControl("PayAmount"), TextBox)
                        String.Format("{0:N2}", newpayamount.Text)
                        'store all collected amounts in list
                        Try 'make sre that input is numeric; otherwise, throw an exception
                            amntCollection.Add(CType(newpayamount.Text, Double))
                        Catch ex As Exception
                            lblErrors.Visible = True
                            lblErrors.Text = "<table><tr><td valign=top><img src=./images/alert.gif /></td><td><font color=red>Input value must be numeric.</font></td></tr></table>"
                            Return False
                            Exit Function
                        End Try
                    Next

                    For Each amnt As Double In amntCollection
                        'compute new total by retrieving amounts from list 
                        newAmount += amnt
                    Next
                    'after completing nested grid operations, set the new totalbox text property with proper string format
                    Dim totalbox As TextBox = CType(row.FindControl("TotalDue"), TextBox)
                    totalbox.Text = String.Format("{0:N2}", newAmount)

                End If
            Next

            If newAmount <= 0.0 Then
                If count < 1 Then
                    lblErrors.Visible = True
                    lblErrors.Text = "<table><tr><td valign=top><img src=./images/alert.gif /></td><td><font color=red>The payment amount cannot be zero.</font></td></tr></table>"
                    Return False
                    Exit Function
                End If
            End If

            Return True
        End Function

        'Handles the paymount textbox textchanged event
        Protected Sub PayAmount_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                'textbox that triggered the event
                Dim triggerTextBox As TextBox = CType(sender, TextBox)
                Dim ardaBox As CheckBox = triggerTextBox.FindControl("cbARDA")

                If ardaBox.Enabled = False Then
                    ardaBox.Enabled = True
                End If

                'check the project name for which you are making payment
                Dim labelText As HiddenField = CType(triggerTextBox.FindControl("hfProjName"), HiddenField)
                Dim projNam As String = CType(labelText.Value, String)

                'hidden field containing the billed amount    
                Dim maxField As HiddenField = CType(triggerTextBox.FindControl("hfPayment"), HiddenField)
                'billed amount
                Dim mRange As Double = CType(maxField.Value, Double)
                'Make sure user input is not less than zero
                If CType(triggerTextBox.Text, Double) < 0 Then
                    lblErrors.Visible = True
                    lblErrors.Text = "<table><tr><td valign=top><img src=./images/alert.gif /></td><td><font color=red>The payment amount cannot be less than zero.</font></td></tr></table>"
                    triggerTextBox.Text = String.Format("{0:N2}", mRange)
                    Me.processPostBack()
                    Exit Sub
                End If
                'billed amount 
                Dim range As Double = CType(maxField.Value, Double)
                'hiddenfield containing arda amount as value property
                Dim ardamountField As HiddenField = CType(ardaBox.FindControl("hfARDAMT1"), HiddenField)
                Dim ardaincrement As Double = CType(ardamountField.Value, Double)
                'when user inputs data in textbox, onTextChanged, check if associated checkbox is not checked. 
                If ardaBox.Checked Then ' if it is the case...
                    '  mRange += ardaincrement 'make sure max range is incremented by arda amount
                    If CType(triggerTextBox.Text, Double) > mRange And mRange > 0.0 Then 'check if amount entered is greater than the max allowed range(see sub checkPaymentNotOver)
                        triggerTextBox.Text = String.Format("{0:N2}", mRange) 'if it is, set it back to the maxrange
                        Me.processPostBack() 'recalculate total 
                        Exit Sub
                    End If
                    'if Validation passed, since ardabox is checked, automatically add arda amount to entered value 
                    Dim ardaAmount As Double = CType(triggerTextBox.Text, Double)
                    'if amount entered by user less than billed amount;then, user is implicitly opting out arda...
                    If ardaAmount < mRange Then
                        ardaBox.Checked = False ' therefore, uncheck the ardaBox
                    End If
                    triggerTextBox.Text = String.Format("{0:N2}", ardaAmount)
                    Me.processPostBack() ' recalculate total
                Else 'if ardabox is not checked...
                    Dim amount As Double = CType(triggerTextBox.Text, Double)
                    If amount > range Then 'check if amount(without arda) entered is greater than range (see sub checkPaymentNotOver)
                        triggerTextBox.Text = String.Format("{0:N2}", range) ' if it is set value back to max range allowed

                        'if user enters an amount less than the billed amount, it will uncheck the ardabox; however, if the next time around, the user 
                        'overpays, the pay amount is set back to the billed amount. Hence, we must make sure that ardabox is checked again if original 
                        'pay amount included an arda amount
                        If ardaBox.Checked = False Then
                            If ardaincrement > 0 Then
                                ardaBox.Checked = True
                            End If
                        End If
                        Me.processPostBack() 'recalculate total
                    Else 'if input is valie, format it 
                        triggerTextBox.Text = String.Format("{0:N2}", amount)
                    End If
                End If
                'to check if a payment amount is numeric or zero
                'if yes recalculate total
                valid = checkZeroAmount(projNam)
                If valid Then
                    lblErrors.Text = ""
                    lblErrors.Visible = False
                Else
                    If mRange < 0 Then
                        'if the max range is negative, than payment amount shouldnot appear as negative balance
                        triggerTextBox.Text = "0.00"
                    Else
                        triggerTextBox.Text = String.Format("{0:N2}", mRange)
                    End If
                    Me.processPostBack()
                    Exit Sub
                End If
            Catch
                Dim trigBox As TextBox = CType(sender, TextBox)
                Dim cBox As CheckBox = CType(trigBox.FindControl("cbARDA"), CheckBox)
                cBox.Enabled = False
                Exit Sub
            End Try
        End Sub

        'Handles ardabox checkchange event
        Protected Sub cbARDA_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                'retrieve the texbox firing the checkchanged event
                Dim ardaBox As CheckBox = CType(sender, CheckBox)
                'check the project name for which you are making payment
                Dim labelText As HiddenField = CType(ardaBox.FindControl("hfProjName"), HiddenField)
                Dim projNam As String = CType(labelText.Value, String)
                'Find the textbox associated with the checkbox that fired the event (ardabox.FindControl)
                Dim newpayamount As TextBox = CType(ardaBox.FindControl("PayAmount"), TextBox)
                'retrieve hidden field that has original payment
                Dim payamountField As HiddenField = CType(ardaBox.FindControl("hfPayment"), HiddenField)
                Dim mRange As Double = CType(payamountField.Value, Double)
                'hiddenfield containing arda amount as value property
                Dim ardamountField As HiddenField = CType(ardaBox.FindControl("hfARDAMT1"), HiddenField)
                Dim ardaincrement As Double = CType(ardamountField.Value, Double)

                'in case ARDA is negative ,it will be treated as zero
                If ardaincrement <= 0 Then
                    ardaincrement = 0.0
                End If

                If CType(newpayamount.Text, Double) > 0.0 Then
                    If Not ardaBox.Checked Then ' if ardabox unchecked, deduct arda amount
                        newpayamount.Text = String.Format("{0:N2}", CType(newpayamount.Text, Double) - ardaincrement)
                        If CType(newpayamount.Text, Double) < 0.0 Then ' if the payment amount after deductinf arda is negative, recaluate billed amount
                            newpayamount.Text = String.Format("{0:N2}", mRange)
                            Me.processPostBack() 'recalculate total
                        End If
                    Else ' else if checked add the arda amount 
                        newpayamount.Text = String.Format("{0:N2}", CType(newpayamount.Text, Double) + ardaincrement)
                        'make sure that user input + arda amount when user checks the box  does not exceed the billed amount
                        If CType(newpayamount.Text, Double) > mRange Then ' if it is set payment amount value to billed amount
                            newpayamount.Text = String.Format("{0:N2}", mRange)
                            Me.processPostBack() 'recalculate total
                        End If
                    End If
                End If
                Me.processPostBack() 'recalculate total on postback
                valid = checkZeroAmount(projNam)
                If valid Then
                    lblErrors.Text = ""
                    lblErrors.Visible = False
                Else
                    Exit Sub
                End If
            Catch ex As Exception 'disable checkbox if invalid input exception is caught
                Dim textPolice As CheckBox = CType(sender, CheckBox)
                textPolice.Enabled = False
                Exit Sub
            End Try
        End Sub

        'Handles the outer grid rowcomand event
        Protected Sub gvOUTER_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvOUTER.RowCommand
            Try
                'If command is sent by butto, do the following:
                Dim amntCollection As New List(Of Double)
                Dim ardaCollection As New List(Of String)
                Dim verifyFinalPay As Double

                If e.CommandName = "getAccount" Then
                    'retrieve nested grid associated to that button by using the command arguments of that button to index the right row

                    Dim gvInner As GridView = CType(gvOUTER.Rows(e.CommandArgument).FindControl("gvDETAIL"), GridView)
                    'Retrieving project Number associated to that row
                    Dim projNum As HiddenField = CType(gvOUTER.Rows(e.CommandArgument).FindControl("hfPROJNUM"), HiddenField)
                    Dim projNm As Label = CType(gvOUTER.Rows(e.CommandArgument).FindControl("lblPRONAME"), Label)
                    Session("PaymentProjNo") = Convert.ToString(projNum.Value)
                    Session("ProjectName") = Convert.ToString(projNm.Text)
                    Dim pNumber As Integer = CType(projNum.Value, Integer)
                    Dim tbTotalDue As TextBox = CType(gvOUTER.Rows(e.CommandArgument).FindControl("TotalDue"), TextBox)
                    Session("TotalDue") = tbTotalDue.Text
                    'retrieve data from related grid, create an account object, store it in a list.
                    Dim billedAmnt As HiddenField
                    For Each row As GridViewRow In gvInner.Rows
                        Dim payBox As TextBox = CType(row.FindControl("PayAmount"), TextBox)
                        Dim accNumber As HiddenField = CType(row.FindControl("hfACCT"), HiddenField)
                        Dim merchID As HiddenField = CType(row.FindControl("merchID"), HiddenField)
                        billedAmnt = CType(row.FindControl("hfBilledAmount"), HiddenField)
                        'creating an object for each account 
                        Dim account As New Account(merchID.Value, accNumber.Value, pNumber, CType(payBox.Text, Double))
                        'adding account to list 
                        'Response.Write(account.paymentamount.ToString() & "<br/>")
                        Me.accAcountNumberList.Add(account.accountNumber)
                        Me.payamountList.Add(account.paymentamount)
                        Me.accTransferList.Add(account)


                        Dim ardaBox As CheckBox = CType(row.FindControl("cbARDA"), CheckBox)
                        Dim ardamountField As HiddenField = CType(ardaBox.FindControl("hfARDAMT1"), HiddenField)
                        Dim ardaincrement As Double = CType(ardamountField.Value, Double)



                        If Not ardaBox.Checked Then

                            amntCollection.Add(ardaincrement)

                            If ardaincrement > 0 Then
                                ardaCollection.Add("Y")

                            Else

                                ardaCollection.Add(" ")

                            End If

                        Else

                            ardaCollection.Add(" ")

                        End If
                    Next
                    'Storing resulting list in a session variable
                    Session("AccountList") = Me.accTransferList
                    Session("accNumList") = Me.accAcountNumberList
                    Session("PaymentPayments") = Me.payamountList
                    Session("billedAmount") = billedAmnt.Value
                    Session("ArdaCollections") = ardaCollection

                    If CType(tbTotalDue.Text, Double) > 0 Then
                        Dim PaymentMethod As RadioButtonList = CType(gvOUTER.Rows(e.CommandArgument).FindControl("rblPaymentMethod"), RadioButtonList)
                        If PaymentMethod.SelectedValue = "" Then

                            Me.lblErrors.Visible = "true"
                            Me.lblErrors.Text = "<table><tr><td valign=top><img src=./images/alert.gif /></td><td><font color=red>Please select a payment method.</font></td></tr></table>"

                        ElseIf PaymentMethod.SelectedValue = "cc" Then
                            Session("PaymentType") = "creditcard"
                            Response.Redirect("payinfo.aspx")
                        ElseIf PaymentMethod.SelectedValue = "cs" Then
                            Session("PaymentType") = "checking"
                            Response.Redirect("payACH.aspx")
                        ElseIf PaymentMethod.SelectedValue = "in" Then
                            verifyFinalPay = CType(Session("billedAmount"), Double)


                            For Each amnt As Double In amntCollection
                                'compute new total
                                verifyFinalPay -= amnt
                            Next
                            If verifyFinalPay <> tbTotalDue.Text Then
                                'tbTotalDue.Text = Session("billedAmount")
                                Me.lblErrors.Visible = "true"

                                Me.lblErrors.Text = "<table><tr><td valign=top><img src=./images/alert.gif /></td><td><font color=red>You cannot adjust the payment amount if you elect to pay by installments. ARDA fees, when applicable, are voluntary and may be adjusted by selecting or deselecting the checkbox.</font></td></tr></table>"
                                Exit Sub
                            End If
                            Session("PaymentType") = "installmentnew"
                            Response.Redirect("payInstallPlan.aspx")
                        End If
                    Else
                        If lblErrors.Visible = False Then
                            If CType(tbTotalDue.Text, Double) < 0 Then
                                lblErrors.Text = "<table><tr><td valign=top><img src=./images/alert.gif /></td><td><font color=red>The payment amount cannot be less than zero.</font></td></tr></table>"
                                lblErrors.Visible = True
                            Else
                                lblErrors.Text = "<table><tr><td valign=top><img src=./images/alert.gif /></td><td><font color=red>The payment amount cannot be zero.</font></td></tr></table>"
                                lblErrors.Visible = True
                            End If
                        Else
                            If CType(tbTotalDue.Text, Double) < 0 Then
                                lblErrors.Text = "<table><tr><td valign=top><img src=./images/alert.gif /></td><td><font color=red>The payment amount cannot be less than zero.</font></td></tr></table>"
                                lblErrors.Visible = True
                            Else
                                lblErrors.Text = "<table><tr><td valign=top><img src=./images/alert.gif /></td><td><font color=red>The payment amount cannot be zero.</font></td></tr></table>"
                                lblErrors.Visible = True
                            End If
                        End If
                    End If
                End If
                Dim list As List(Of Account) = CType(Session("AccountList"), List(Of Account))
                Session("OwnerAccountProjectCount") = list.Count

            Catch ex As Exception
                processPostBack()
            End Try
        End Sub

        ' Handles the outerGrid row created event; it happens each time a row is created
        Protected Sub gvOUTER_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOUTER.RowCreated
            'make sure the row is not a header row
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Find the image button control in that row 
                Dim button As ImageButton = CType(e.Row.FindControl("submitPayment"), ImageButton)
                'assign the row index to the command argument of that button
                button.CommandArgument = e.Row.RowIndex.ToString
            End If
        End Sub
#End Region

        Protected Sub btnPrepayment_Click(sender As Object, e As System.EventArgs) Handles btnPrepayment.Click
            Response.Redirect("PrePayment.aspx", True)
        End Sub
    End Class

End Namespace
