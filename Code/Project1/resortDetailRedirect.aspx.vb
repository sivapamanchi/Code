Namespace BluegreenOnline

Partial Class resortDetailRedirect
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
        Dim sQuerystring As String
        sQuerystring = "ShowAvail=" & Request.QueryString("ShowAvail") & "&ResortID=" & Request.QueryString("ResortID")

        Select Case Session("ResortLocator")
            Case "OWN"
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("bxgwebSecureURL") & "owner/resortDetail.aspx?" & sQuerystring)
                Case "CRP"
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnSecureURL") & "explore/resortDetail.aspx?" & sQuerystring)
            Case "EXP"
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnSecureURL") & "explore/resortDetail.aspx?" & sQuerystring)
                Case Else
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnSecureURL") & "explore/resortDetail.aspx?" & sQuerystring)
            End Select
    End Sub

End Class

End Namespace
