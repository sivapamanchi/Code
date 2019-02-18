Imports VSSA.ResortsService
Imports ReservationLibrary
Imports System.Collections.Generic
Imports System.ComponentModel
Imports BxgCorp.Mortgage
Imports VSSA.IVRSearchSvc

Public Class CustomerProfileMain
    Inherits VSSABaseClass

    Public CustomerNumber As String
    Public ivrReqParams As New IVRRequest
    Public reqParamsStr As String
    Public ANI As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        reqParamsStr = Request.QueryString("UUI")
        ANI = Request.QueryString("ani")

        If Not IsNothing(reqParamsStr) Then

            Dim results As String = ValidateRequest(reqParamsStr, ANI, "OWNER")

            If results <> "" Then
                'lblError.Text = results
                Exit Sub
            End If


            GetCustomerInfo()
            If CustomerNumber = "" Or ivrReqParams.Disposition = "MR" Or ivrReqParams.Disposition = "NZ" Or ivrReqParams.Disposition = "SU" Then
                Response.Redirect("SearchCustomer.aspx?ani=" & ANI & "&UUI=" & reqParamsStr)
            End If

            GetAccountsInfoByCustomer()

        Else
            'lblError.Text = "Request Parameters can't be empty."
        End If
    End Sub


    Public Sub GetCustomerInfo()
        Dim resp As Popup_Customers_ResponseCustomer()
        Try

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
            lblTransferTxt.Text = ClubServiceTransferTextTable(ivrReqParams.TransaferId)

            If lblTransferTxt.Text <> "" Then
                pnlTransferPt.Visible = True
            Else
                pnlTransferPt.Visible = False
            End If

            CustomerNumber = ivrReqParams.CustomerId

            resp = GetConciergeCustomerInfo(CustomerNumber)

            If resp IsNot Nothing And resp.Count > 0 Then

                lblCustNbr.Text = CType(IIf(resp(0).CustomerNumber Is Nothing, "N/A", resp(0).CustomerNumber), String)
                lblFName.Text = CType(IIf(resp(0).FirstName Is Nothing, "N/A", resp(0).FirstName), String)
                lblLName.Text = CType(IIf(resp(0).LastName Is Nothing, "N/A", resp(0).LastName), String)
                lblSpFName.Text = CType(IIf(resp(0).SpouseFirstName Is Nothing, "N/A", resp(0).SpouseFirstName), String)
                lblSpLName.Text = CType(IIf(resp(0).SpouseLastName Is Nothing, "N/A", resp(0).SpouseLastName), String)
                lblAddress1.Text = CType(IIf(resp(0).Address1 Is Nothing, "N/A", resp(0).Address1), String)
                lblAddress2.Text = CType(IIf(resp(0).Address2 Is Nothing, "N/A", resp(0).Address2), String)
                lblCity.Text = CType(IIf(resp(0).City Is Nothing, "N/A", resp(0).City), String)
                lblHome.Text = CType(IIf(resp(0).HomePhone Is Nothing, "N/A", FormatPhone(resp(0).HomePhone)), String)
                lblAlt.Text = CType(IIf(resp(0).AltPhone Is Nothing, "N/A", FormatPhone(resp(0).AltPhone)), String)
                lblEmail.Text = CType(IIf(resp(0).Email Is Nothing, "N/A", resp(0).Email), String)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Public Sub GetAccountsInfoByCustomer()

        Dim resp As Popup_Customer_Accounts_ResponseAccount()
        Try

            resp = GetConciergeAccountInfoByCustomer(CustomerNumber)

            If resp IsNot Nothing Then
                dtAccounts.DataSource = resp
                dtAccounts.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub dtAccounts_ItemDataBound(sender As Object, e As DataListItemEventArgs)

        If (e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem) Then

            Dim lblVendNbr As Label = CType(e.Item.FindControl("lblVendNbr"), Label)

            If lblVendNbr.Text = "" Then
                lblVendNbr.Text = "N/A"
            End If

            Dim lblVendName As Label = CType(e.Item.FindControl("lblVendName"), Label)

            If lblVendName.Text = "" Then
                lblVendName.Text = "N/A"
            End If

            Dim lblPkgType As Label = CType(e.Item.FindControl("lblPkgType"), Label)

            If lblPkgType.Text = "" Then
                lblPkgType.Text = "N/A"
            End If

            Dim lblSaleDt As Label = CType(e.Item.FindControl("lblSaleDt"), Label)

            If lblSaleDt.Text = "" Then
                lblSaleDt.Text = "N/A"
            End If

            Dim lblOfferID As Label = CType(e.Item.FindControl("lblOfferID"), Label)

            If lblOfferID.Text = "" Then
                lblOfferID.Text = "N/A"
            End If

            Dim lblOfferDesc As Label = CType(e.Item.FindControl("lblOfferDesc"), Label)

            If lblOfferDesc.Text = "" Then
                lblOfferDesc.Text = "N/A"
            End If

            Dim lblResCrDate As Label = CType(e.Item.FindControl("lblResCrDate"), Label)

            If lblResCrDate.Text = "" Then
                lblResCrDate.Text = "N/A"
            End If

            Dim lblResvArrDt As Label = CType(e.Item.FindControl("lblResvArrDt"), Label)

            If lblResvArrDt.Text = "" Then
                lblResvArrDt.Text = "N/A"
            End If

            Dim lblResvDepDt As Label = CType(e.Item.FindControl("lblResvDepDt"), Label)

            If lblResvDepDt.Text = "" Then
                lblResvDepDt.Text = "N/A"
            End If

            Dim lblDest As Label = CType(e.Item.FindControl("lblDest"), Label)

            If lblDest.Text = "" Then
                lblDest.Text = "N/A"
            End If

            Dim lblComment As Label = CType(e.Item.FindControl("lblComment"), Label)

            If lblComment.Text = "" Then
                lblComment.Text = "N/A"
            End If

            Dim lblPromo As Label = CType(e.Item.FindControl("lblPromo"), Label)

            If lblPromo.Text = "" Then
                lblPromo.Text = "N/A"
            End If

            Dim lblRecommendations As Label = CType(e.Item.FindControl("lblRecommendations"), Label)

            If lblRecommendations.Text = "" Then
                lblRecommendations.Text = "N/A"
            End If

            If lblRecommendations.Text.Trim().ToUpper() = "OK TO REFUND/CANCEL" Then
                lblRecommendations.Text = "<FONT color=red>OK to Refund/Cancel</font>"
            End If

            Dim lblPromoDesc As Label = CType(e.Item.FindControl("lblPromoDesc"), Label)

            If lblPromoDesc.Text = "" Then
                lblPromoDesc.Text = "N/A"
            End If

        End If

    End Sub
End Class

