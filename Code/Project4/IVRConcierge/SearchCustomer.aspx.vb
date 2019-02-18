Imports System.Collections.Generic
Imports System.ComponentModel
Imports BxgCorp.Mortgage
Imports VSSA.IVRSearchSvc
Public Class SearchCustomer
    Inherits System.Web.UI.Page
    Public reqParamsStr As String
    Public ivrReqParams As New IVRRequest
    Public ANI As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If (Session("isAuthenticated") Is Nothing) Then
        '    Response.Redirect("../Default.aspx?page=SearchCustomer.aspx")
        'ElseIf (Session("isAuthenticated") = False) Then
        '    Response.Redirect("../NoAccess.html")
        'End If

        If Not IsPostBack Then

            If IsNothing(Request.QueryString("ani")) Or IsNothing(Request.QueryString("UUI")) Then
                'lblError.Text = "Request Parameters can't be empty."
                Exit Sub
            End If
        End If

        If ViewState("requestParams") IsNot Nothing Then
            reqParamsStr = DirectCast(ViewState("requestParams"), String)

        Else
            ' ArrayList isn't in view state, so we need to load it from scratch.
            PopulateSearchResults()
        End If
    End Sub
    Private Sub Page_PreRender(sender As Object, e As EventArgs)
        ' Save PageArrayList before the page is rendered.
        ViewState.Add("requestParams", reqParamsStr)
        ViewState.Add("ani", ANI)
    End Sub

    Private Sub SetRequestParmsViewState(ByVal _reqParams As Object, ByVal _ani As Object)
        'Sets the ViewState
        ViewState("requestParams") = _reqParams
        ViewState("ani") = _ani

    End Sub

    Private Sub gvSearchResults_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSearchResults.RowCommand
        If e.CommandName = "redirect" Then
            ANI = DirectCast(ViewState("ani"), String)
            ivrReqParams = GetRawParameters(reqParamsStr, ANI)
            Dim pad As Char
            pad = Convert.ToChar("_")
            Dim customerid As String = e.CommandArgument.ToString.PadLeft(10, pad)
            Dim URL As String = "CustomerProfile.aspx?ani=" & ANI.Trim & "&UUI="
            Dim params As String = ""

            params = ivrReqParams.Arvact & customerid & ivrReqParams.ValidationPhone & ivrReqParams.MenuId & "RF" & ivrReqParams.TransaferId
            Response.Redirect(URL & params)

        End If
    End Sub

    Protected Sub gvSearchResults_RowDataBound(sender As Object, e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim HomePhoneCell As TableCell = e.Row.Cells(4)
            HomePhoneCell.Text = FormatPhone(HomePhoneCell.Text)

            Dim AltPhoneCell As TableCell = e.Row.Cells(5)
            AltPhoneCell.Text = FormatPhone(AltPhoneCell.Text)

        End If

    End Sub

    Private Sub PopulateSearchResults()
        Dim hPhone As String = ""
        If Not IsNothing(Request.QueryString("UUI")) Then

            ANI = Request.QueryString("ani")

            If IsNothing(reqParamsStr) Then
                reqParamsStr = Request.QueryString("UUI")
                SetRequestParmsViewState(reqParamsStr, ANI)
            End If

            ivrReqParams = GetRequestParameters(reqParamsStr, ANI)

            If ivrReqParams.ANIPhone IsNot Nothing Then
                lblCaptured.Text = FormatPhone(ivrReqParams.ANIPhone)
            End If

            If ivrReqParams.ValidationPhone IsNot Nothing Then
                lblSearched.Text = FormatPhone(ivrReqParams.ValidationPhone)
            End If

            lblMenuOption.Text = ClubServiceMapOptionTable(ivrReqParams.MenuId)

            If lblMenuOption.Text <> "" Then
                pnlMenu.Visible = True
            Else
                pnlMenu.Visible = False
            End If

            lblDisposition.Text = GetDispositionDesc(ivrReqParams.Disposition)

            If ANI.Length > 10 Then
                lblTransferTxt.Text = "International Caller"
            Else
                lblTransferTxt.Text = ClubServiceTransferTextTable(ivrReqParams.TransaferId)
            End If



            If lblTransferTxt.Text <> "" Then
                pnlTransferPt.Visible = True
            Else
                pnlTransferPt.Visible = False
            End If


            If Trim(ivrReqParams.ValidationPhone) <> "" Then
                hPhone = ivrReqParams.ValidationPhone.Trim()
            Else
                hPhone = ANI
            End If

            If Trim(hPhone) = "" Then
                'lblError.Text = "Request Parameters can't be empty."
                Exit Sub
            End If

            Dim resp As Popup_Customers_ResponseCustomer()
            Try

                resp = GetConciergeCustomersByPhone(hPhone)

                If resp IsNot Nothing Then

                    gvSearchResults.DataSource = resp.OrderByDescending(Function(x) Convert.ToDateTime(x.LastAccountCreateDt)).ToList()
                    gvSearchResults.DataBind()

                End If


            Catch ex As Exception
                lblError.Text = ex.Message
            End Try

        End If

    End Sub

End Class