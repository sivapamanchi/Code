Namespace BluegreenOnline

Partial Class SalesLogin
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

    Public sEmail As String
    Public sPass As String
        Public sSSLURL As String = System.Configuration.ConfigurationManager.AppSettings("bxgwebSecureURL")

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Protect this page with login
        If Session("SalesUser") <> "BLUEGREEN" Then
            Response.Redirect("default.aspx", True)
        End If

        Dim email_id, pass As String
        email_id = Session("email_id")
        pass = Session("password")

        sEmail = "Email Address"
        sPass = ""

        Session.Clear()
        Session("SalesUser") = "BLUEGREEN"

        If Not IsPostBack Then
            Try
                'Allow logins from elsewhere to feed into this page
                If email_id <> "" And pass <> "" Then
                    sEmail = email_id
                    sPass = pass
                End If
            Catch
            End Try
        End If
    End Sub

End Class

End Namespace
