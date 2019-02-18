
Partial Class ReservPrintConfirmation
    Inherits VSSABaseClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'load page with HTML content stored in session variable. For more details, see ReservationPrint.aspx.vb :: btnPrinConfirmations_Click
        Me.litTemplate.Text = Session("ConfirmationTemplates")
    End Sub
End Class
