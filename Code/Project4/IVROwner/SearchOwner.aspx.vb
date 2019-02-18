Public Class SearchOwner
    Inherits VSSABaseClass

    Public reqParamsStr As String
    Public ivrReqParams As New IVRRequest
    Public ANI As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

    Private Sub PopulateSearchResults()
        Dim hPhone As String = ""
        Dim txtTransferPoint As String = ""
        Dim URL As String = ""


        Try
            ANI = Request.QueryString("ani")
            reqParamsStr = Request.QueryString("UUI")
            SetRequestParmsViewState(reqParamsStr, ANI)


            ivrReqParams = GetRequestParameters(reqParamsStr, ANI)

            If ivrReqParams.Arvact <> "" Then
                URL = "../IVROwner/ownerprofile.aspx?ani=" & ANI.Trim & "&UUI=" &
                      String.Format("{0, 6}", ivrReqParams.Arvact) &
                      String.Format("{0, 10}", ivrReqParams.CustomerId) &
                      String.Format("{0, 10}", ivrReqParams.ValidationPhone) &
                      String.Format("{0, 1}", ivrReqParams.MenuId) &
                      "RF" &
                      String.Format("{0, 2}", ivrReqParams.TransaferId)

                Response.Redirect(Replace(URL, " ", "_"), False)
                Exit Sub
            End If


            If ivrReqParams.ANIPhone <> "" Then
                lblCaptured.Text = String.Format("{0:(###) ###-####}", Long.Parse(ivrReqParams.ANIPhone))
            End If

            If ivrReqParams.ValidationPhone <> "" Then
                lblSearched.Text = String.Format("{0:(###) ###-####}", Long.Parse(ivrReqParams.ValidationPhone))
            End If

            If ClubServiceMapOptionTable(ivrReqParams.MenuId).Trim() = "" Then
                pnlMenu.Visible = False
            Else
                lblMenuOption.Text = ClubServiceMapOptionTable(ivrReqParams.MenuId)
            End If

            lblDisposition.Text = GetDispositionDesc(ivrReqParams.Disposition)

            lblTransferTxt.Text = ClubServiceTransferTextTable(ivrReqParams.TransaferId)

            If Trim(ivrReqParams.ValidationPhone) <> "" Then
                hPhone = ivrReqParams.ValidationPhone.Trim()
            Else
                hPhone = ANI
            End If



            If Trim(hPhone) = "" Then
                'lblError.Text = "Request Parameters can't be empty."
                Exit Sub
            End If

            Dim ds As New DataSet
            ds = OwnerData.searchOwners("", "", "", "", hPhone)

            If ds.Tables(0).Rows.Count = 0 Then
                'lblError.Text = "No Records Found."
                Exit Sub
            End If


            Dim Name2 As DataColumn = New DataColumn("Reg")
            Name2.DataType = System.Type.GetType("System.String")
            ds.Tables(0).Columns.Add(Name2)

            For Each row As DataRow In ds.Tables(0).Rows
                'If row.Item("email") Then
                If row.Item("email") Is System.DBNull.Value Then

                    Session("Registered") = "No"
                    row.Item("Reg") = "No"

                Else
                    Try
                        If row.Item("Email").ToString().Length = 0 Or row.Item("Password").ToString().Length = 0 Then
                            Session("Registered") = "No"
                            row.Item("Reg") = "No"
                        Else
                            Session("Registered") = "Yes"
                            row.Item("Reg") = "Yes"
                        End If
                    Catch ex As Exception

                        Session("Registered") = "No"
                        row.Item("Reg") = "No"

                    End Try

                    'End If

                End If

                If row.Item("PhoneHome") IsNot Nothing Then
                    row.Item("PhoneHome") = FormatPhone(row.Item("PhoneHome").ToString())
                End If

            Next

            gvSearchResults.DataSource = ds
            gvSearchResults.DataBind()


        Catch ex As Exception
            lblError.Text = ex.Message.ToString()
        End Try


    End Sub

    Private Sub gvSearchResults_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSearchResults.RowCommand
        If e.CommandName = "redirect" Then
            ANI = DirectCast(ViewState("ani"), String)
            ivrReqParams = GetRawParameters(reqParamsStr, ANI)
            Dim pad As Char
            pad = Convert.ToChar("_")
            Dim arvact As String = e.CommandArgument.ToString.PadLeft(6, pad)
            Dim URL As String = "ownerprofile.aspx?ani=" & ANI.Trim & "&UUI="
            Dim params As String = ""

            params = arvact & ivrReqParams.CustomerId & ivrReqParams.ValidationPhone & ivrReqParams.MenuId & "RF" & ivrReqParams.TransaferId
            Response.Redirect(URL & params)

        End If
    End Sub
End Class