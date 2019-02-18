
Partial Class ChangeAddress
    Inherits VSSABaseClass

    Protected Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If Not IsPostBack Then
            lnkVssa.NavigateUrl = "default.aspx"
            If (Request.QueryString("arvact") <> String.Empty) Then
                lnkVssa.NavigateUrl &= "?arvact=" + Request.QueryString("arvact")
            End If
        End If
    End Sub
End Class
