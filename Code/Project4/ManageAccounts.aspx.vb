
Partial Class ManageAccounts
    Inherits VSSABaseClass

    Protected Sub ManageAccounts_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (Session("isAuthenticated") Is Nothing) Then
            Response.Redirect("Default.aspx?page=ManageAccounts.aspx")
        ElseIf (Session("isAuthenticated") = False) Then
            Response.Redirect("NoAccess.html")
        End If

        If Not IsPostBack Then
            lnkVssa.NavigateUrl = "default.aspx"
            If (Request.QueryString("arvact") <> String.Empty) Then
                lnkVssa.NavigateUrl &= "?arvact=" + Request.QueryString("arvact")
            End If
        End If
    End Sub
End Class
