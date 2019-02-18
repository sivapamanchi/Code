Namespace BluegreenOnline

Partial Class AccountNumberLookup
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub cancelBtn_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cancelBtn.Click
        Response.Redirect("enroll.aspx")
    End Sub

    Private Sub submitBtn_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles submitBtn.Click
        Dim strError As String = ""

        If (txtAcctPhone.Text.Length <> 10) Or (Not IsNumeric(txtAcctPhone.Text)) Then
            strError = "Last 10 digits of home phone<BR>"
        End If

        If (txtAcctSocial.Text.Length <> 4) Or (Not IsNumeric(txtAcctSocial.Text)) Then
            strError &= "Last 4 digits of primary social security"
        End If

        If strError <> "" Then
            lblErrorPersonal.Text = "Please enter valid information for:<BR>" & strError
            lblErrorPersonal.Visible = True
            imgAlert.Visible = True
        Else
            LookupAcctNum()
        End If
    End Sub

    Private Sub LookupAcctNum()
        Dim C As New clsDBConnectivity
            Dim dbDataReader As system.data.SqlClient.SqlDataReader
            Dim sAcctNum As String = ""
            Dim iSocial As Int16 = 0
            Dim iPhone As Int64 = 0

            Try
                iSocial = Convert.ToInt16(txtAcctSocial.Text)
            Catch ex As Exception

            End Try

            Try
                iPhone = Convert.ToInt64(txtAcctPhone.Text)
            Catch ex As Exception

            End Try

            C.dbCmnd.CommandText = "uspCheckVacationAccount"
            C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
            C.dbCmnd.Parameters.Clear()
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcctNo", System.Data.SqlDbType.Int)).Value = 0
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcctSocial", System.Data.SqlDbType.Int)).Value = iSocial
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AcctPhone", System.Data.SqlDbType.BigInt)).Value = iPhone

        dbDataReader = C.dbCmnd.ExecuteReader()

        While dbDataReader.Read()
            sAcctNum = dbDataReader("ARVACT")
        End While

        dbDataReader.Close()
        C.Close()
        C = Nothing

        If sAcctNum <> "" Then
            'pnlEntry.Visible = False
            'pnlResult.Visible = True
            'lblAcctID.Visible = True
            'btnGoToEnroll.Visible = True
            'lblAcctID.Text = sAcctNum
            Response.Redirect("enroll.aspx?acct=" & sAcctNum & "&ssn=" & txtAcctSocial.Text)
        Else
            lblErrorPersonal.Visible = True
            lblErrorPersonal.Text = "No account was found matching the Phone number and Social Security number you entered"
            imgAlert.Visible = True
        End If
    End Sub

    Private Sub btnGoToEnroll_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnGoToEnroll.Click
        Response.Redirect("enroll.aspx?acct=" & lblAcctID.Text)
    End Sub
End Class

End Namespace
