Namespace BluegreenOnline

Partial Class error404
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
            Try
                'Handle the redirection to specific resorts detail pages if the url is detected
                Dim sResortPage As String
                Dim sQueryString As String = Request.ServerVariables("QUERY_STRING")
                For y As Integer = 0 To sQueryString.Length - 1
                    If sQueryString.Substring(y, 1) <> "/" Then sResortPage &= sQueryString.Substring(y, 1)
                    If (sQueryString.Substring(y, 1) = "/") And (y + 1 < sQueryString.Length) Then sResortPage = ""
                Next

                If sResortPage.Length > 0 Then
                    Dim sResortID As String = ""
                    Dim C As New clsDBConnectivity
                    Dim drResortID As SqlClient.SqlDataReader
                    C.dbCmnd.CommandText = "uspSelectResortFromURL"
                    C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                    C.dbCmnd.Parameters.Clear()
                    C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SearchPhrase", System.Data.SqlDbType.VarChar, 100)).Value = sResortPage

                    drResortID = C.dbCmnd.ExecuteReader()

                    While drResortID.Read()
                        sResortID = drResortID("ResortID")
                    End While

                    'Cleanup
                    drResortID.Close()
                    C.Close()
                    drResortID = Nothing
                    C = Nothing

                    If sResortID.Length > 0 Then
                        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "explore/resortDetail.aspx?ShowAvail=No&ResortID=" & sResortID, True)
                    End If
                End If
            Catch ex As Exception
                'Response.Write(ex.Message & ":" & ex.StackTrace)
            End Try
    End Sub

End Class

End Namespace
