Namespace BluegreenOnline

Partial Class enrollUnsuccess
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

        If Not IsPostBack Then

            displayMessage()

        End If

    End Sub

    Private Sub displayMessage()

        If Request.QueryString("error") = "A" Then

            pnlEnrolled.Visible = "true"

        ElseIf Request.QueryString("error") = "N" Then

            pnlNoMatch.Visible = "true"

        End If

    End Sub


    'Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '   Session("email_id") = Session("emailTemp")
    '  Session("password") = Session("passwordTemp")
    ' Response.Redirect("default.aspx")

    ' End Sub

    Private Sub imgGoBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgGoBack.Click
        Response.Redirect("enroll.aspx")
    End Sub
End Class

End Namespace
