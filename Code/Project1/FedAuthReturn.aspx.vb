Imports System.IdentityModel
Imports System.IdentityModel.Claims
Imports System.Threading
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class FedAuthReturn
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("_path_info")) Then
            ' if there is no path stored to which to redirect the owner, default to the owner/home.aspx page
            Response.Redirect("~/owner/home.aspx")
        Else
            Response.Redirect(Session("_path_info"))
        End If

        ' If this page changes such that claims need to be evaluated to see how to handle the redirect, you can use some of the code below to find your claim(s) to evaluate. Of course, you would need to 
        ' move the first line to after your claims test(s)
        'Dim claimsPrincipal As IClaimsPrincipal = Page.User
        'Dim claimsIdentity As IClaimsIdentity = claimsPrincipal.Identity

        '' The code below shows claims found in the IClaimsIdentity.
        '' TODO: Change code below to do your processing using claims.

        'Dim claimsTable As Table = New Table()
        'Dim headerRow As TableRow = New TableRow()

        'Dim claimTypeCell As TableCell = New TableCell()
        'claimTypeCell.Text = "Claim Type"
        'claimTypeCell.BorderStyle = BorderStyle.Solid

        'Dim claimValueCell As TableCell = New TableCell()
        'claimValueCell.Text = "Claim Value"
        'claimValueCell.BorderStyle = BorderStyle.Solid

        'headerRow.Cells.Add(claimTypeCell)
        'headerRow.Cells.Add(claimValueCell)
        'claimsTable.Rows.Add(headerRow)

        'Dim newRow As TableRow
        'Dim newClaimTypeCell, newClaimValueCell As TableCell
        'For Each claim As Claim In claimsIdentity.Claims

        '    newRow = New TableRow()
        '    newClaimTypeCell = New TableCell()
        '    newClaimTypeCell.Text = claim.ClaimType

        '    newClaimValueCell = New TableCell()
        '    newClaimValueCell.Text = claim.Value

        '    newRow.Cells.Add(newClaimTypeCell)
        '    newRow.Cells.Add(newClaimValueCell)

        '    claimsTable.Rows.Add(newRow)
        'Next

        'Controls.Add(claimsTable)
    End Sub

End Class