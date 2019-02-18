Imports Microsoft.Security.Application
Namespace BluegreenOnline

    Partial Class NewsletterSignup
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
            VerifyInput()
            AddSubscriber()
        End Sub

        Private Sub VerifyInput()
            Dim sValidationErrors As String = ""
            Dim sURL As String

            Try
                If Not System.Text.RegularExpressions.Regex.IsMatch(Encoder.HtmlEncode(Request.Form("nl_signup")), "\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") Then
                    sValidationErrors = "Email"
                End If
            Catch ex As Exception
                sValidationErrors = "Email"
            End Try
            Try
                If Request.Form("nl_signup2") = "Zip Code" Or Request.Form("nl_signup2").Length = 0 Then
                    If sValidationErrors.Length > 0 Then
                        sValidationErrors &= " and "
                    End If
                    sValidationErrors &= "Zip Code"
                End If
            Catch ex As Exception
                If sValidationErrors.Length > 0 Then
                    sValidationErrors &= " and "
                End If
                sValidationErrors &= "Zip Code"
            End Try

            If sValidationErrors.Length > 0 Then
                sValidationErrors = Server.UrlEncode("Invalid " & sValidationErrors)
                sURL = Request.ServerVariables("HTTP_REFERER") & ""
                If sURL.ToLower.IndexOf("bluegreen") = -1 Then
                    sURL = System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "corporate/index.aspx"
                End If
                If sURL.IndexOf("&NewsError=") > 0 Then
                    sURL = sURL.Substring(0, sURL.IndexOf("&NewsError="))
                End If
                If sURL.IndexOf("?NewsError=") > 0 Then
                    sURL = sURL.Substring(0, sURL.IndexOf("?NewsError="))
                End If

                If InStr(sURL, "?") > 0 Then
                    sURL &= "&NewsError=" & sValidationErrors
                Else
                    sURL &= "?NewsError=" & sValidationErrors
                End If

                Response.Redirect(sURL, True)
            End If
        End Sub

        Private Sub AddSubscriber()
            Dim C As New clsDBConnectivity
            Dim dbDataReader As SqlClient.SqlDataReader
            Dim sResult As String
            Dim sURL As String

            C.dbCmnd.CommandText = "uspAddSubscriber"
            C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
            C.dbCmnd.Parameters.Clear()
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 50)).Value = Encoder.HtmlEncode(Request.Form("nl_signup")).ToString()
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ZipCode", System.Data.SqlDbType.VarChar, 15)).Value = Encoder.HtmlEncode(Request.Form("nl_signup2")).ToString()

            If Request.ServerVariables("HTTP_REFERER").IndexOf("corporate") > 0 Then
                C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SiteID", System.Data.SqlDbType.Int)).Value = 5

            ElseIf Request.ServerVariables("HTTP_REFERER").IndexOf("explore") > 0 Then
                C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SiteID", System.Data.SqlDbType.Int)).Value = 6

            Else
                C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SiteID", System.Data.SqlDbType.Int)).Value = 1
                C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OwnerID", System.Data.SqlDbType.Int)).Value = Session("OwnerIdentity")
            End If

            dbDataReader = C.dbCmnd.ExecuteReader()
            If dbDataReader.Read() Then
                sResult = dbDataReader("Status")
            End If
            dbDataReader.Close()
            C.Close()
            C = Nothing

            sURL = Request.ServerVariables("HTTP_REFERER") & ""
            If sURL.ToLower.IndexOf("bluegreen") = -1 Then
                sURL = System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "corporate/index.aspx"
            End If

            If InStr(sURL, "?") > 0 Then
                sURL &= "&News=" & sResult
            Else
                sURL &= "?News=" & sResult
            End If

            Response.Redirect(sURL)
        End Sub
    End Class

End Namespace
