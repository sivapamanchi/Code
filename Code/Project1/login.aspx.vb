Namespace BluegreenOnline

Partial Class LoginChoice
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cboVCAccounts As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboNonVCAccounts As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnChoose As System.Web.UI.WebControls.Button
       

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
        Public isSamplerOwner As Boolean
        Public homeProject As String
        Dim BXGOwner As OwnerWS.Owner
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                If Session("BXGOwner") Is Nothing Then
                    Response.Redirect(ConfigurationManager.AppSettings("SitecoreHostPath"), True)
                End If
                BXGOwner = Session("BXGOwner")

                If Not IsNothing(BXGOwner.User(0)) Then
                    isSamplerOwner = BXGOwner.User(0).isSampler
                    homeProject = BXGOwner.User(0).HomeProject

                Else
                    isSamplerOwner = False
                    homeProject = "0"
                End If


            Catch ex As Exception

            End Try
        If Not IsPostBack Then
            'Load the radio button list
            PopulateRadioList()
        End If

    End Sub

    Private Sub PopulateRadioList()

        'Loop through all the stored accounts and add them to the radio button list
        For x As Integer = 0 To Session("LoginAccountList").Count - 1
            Dim UserAccount As New clsLoginAccount
            Dim liAcct As New ListItem
            UserAccount = Session("LoginAccountList")(x)
                If UserAccount.ProjNum = "50" And isSamplerOwner = True And homeProject = "52" Then
                    liAcct.Text = "Sampler 24"
                ElseIf UserAccount.ProjNum = "50" And isSamplerOwner = True And homeProject = "51" Then
                    liAcct.Text = "Sampler"
                Else
                    liAcct.Text = UserAccount.ResortName
                End If

            liAcct.Value = x
            rdoAccountList.Items.Add(liAcct)
        Next

    End Sub

    Private Sub rdoAccountList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoAccountList.SelectedIndexChanged

        'Populate the session based on the chosen contract
        For x As Integer = 0 To Session("LoginAccountList").Count - 1
            Dim UserAccount As New clsLoginAccount
            UserAccount = Session("LoginAccountList")(x)

                If rdoAccountList.SelectedValue = x Then
                    If UserAccount.ProjNum = "50" And isSamplerOwner = True Then
                        Session("OwnerContractType") = "Sampler"

                    Else
                        Session("OwnerContractType") = UserAccount.ContractType
                    End If

                    Session("OwnerHomeResort") = UserAccount.ResortID
                    Session("OwnerHomeProjectNumber") = UserAccount.ProjNum
                    Session("OwnerContractNumber") = UserAccount.ContractID
                    Session("OwnerHomeResortWeeks") = UserAccount.Weeks
                    Session("OwnerVIP") = UserAccount.VIP
                End If
        Next

        If Session("OwnerContractType") = "Vacation Club" Then
            'Vacation Club Member
            Session("siteLogo") = "bgvc_logo2.gif"
            Session("sitePhoneNumberImg") = "hdrTag_x.gif"
            Session("sitePhoneNumberMouseover") = "hdrTag_o.gif"
            Session("sitePhoneNumber") = "800.456.CLUB (2582)"
            Session("siteLogoAlt") = "Bluegreen Vacation Club"
            Session("siteLogoHeight") = "63"
            Session("siteNavjs") = "owner_data"
            Session("siteTemplateHeight") = "253"
                Session("siteTemplateImage") = "ownerBkgd1b_lft.gif"

                bxgOwner = Session("BXGOwner")
                If bxgOwner.InstallmentPlan(0).InstallmentStatus = "IS" Or bxgOwner.InstallmentPlan(0).InstallmentStatus = "ID" Then

                    Response.Redirect("owner/acctStatus.aspx")

                ElseIf CDbl(bxgOwner.ReservationDuePaymentBalance) > 0.0 And bxgOwner.InstallmentPlan(0).InstallmentStatus <> "IP" Then


                    'Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("SitecoreHostPath").ToString() + "/payments/payment-reminder")
                    Response.Redirect("owner/paymentReminder.aspx")
                ElseIf bxgOwner.AnnualPointsExpiration.SavePointsEligible = True And bxgOwner.AnnualPointsExpiration.SavePointsPopup = True Then

                    Response.Redirect("owner/myPoints.aspx?display=1")

                ElseIf bxgOwner.TravelerPlusMembership.TPRenewPopup = True Then
                    Try

                        Response.Redirect("TravelerPlus/owner/ownerrenewal.aspx?display=1")

                    Catch ex As Exception

                    End Try
             
                Else
                    Response.Redirect("owner/home.aspx")
                End If
            ElseIf Session("OwnerContractType") = "Sampler" Then
                Session("siteLogo") = "bgvc_logo2.gif"
                Session("sitePhoneNumberImg") = "hdrTag_x.gif"
                Session("sitePhoneNumberMouseover") = "hdrTag_o.gif"
                If Session("OwnerContractType") = "Sampler" Then
                    Session("sitePhoneNumber") = "800.459.1597"
                Else
                    Session("sitePhoneNumber") = "800.456.CLUB (2582)"
                End If



                Session("siteLogoAlt") = "Bluegreen Vacation Club"
                Session("siteLogoHeight") = "63"
                If Session("IsTravelerPlusEligible") <> "TRUE" Then
                    Session("siteNavjs") = "owner_data"
                End If
                Session("siteTemplateHeight") = "253"
                Session("siteTemplateImage") = "ownerBkgd1b_lft.gif"

                Response.Redirect("owner/home.aspx")
            Else
                'Some other club - Fixed, etc.
                Session("siteLogo") = "bgvc_logo2.gif"
                Session("sitePhoneNumberImg") = "nonclubowner_phone.gif"
                Session("sitePhoneNumberMouseover") = "nonclubowner_phone.gif"
                Session("sitePhoneNumber") = "877.688.9889"
                Session("siteLogoAlt") = "Bluegreen Resort Management"
                Session("siteLogoHeight") = "63"
                Session("siteNavjs") = "ownerNVC_data"
                Session("siteTemplateHeight") = "223"
                Session("siteTemplateImage") = "ownerBkgd1_lft.gif"
                Response.Redirect("owner/homeFixed.aspx")
            End If
        End Sub
        Function GetFirstDay(ByVal aDate As DateTime)
            Dim dteFirstDayNextMonth As DateTime

            dteFirstDayNextMonth = DateSerial(Year(aDate), Month(aDate) + 1, 1)
            GetFirstDay = DateAdd("m", -1, dteFirstDayNextMonth)
        End Function

        Function GetLastDay(ByVal aDate As DateTime)
            Dim dteFirstDayNextMonth As DateTime

            dteFirstDayNextMonth = DateSerial(Year(aDate), Month(aDate) + 1, 1)
            GetLastDay = Day(DateAdd("d", -1, dteFirstDayNextMonth))
        End Function
End Class
End Namespace
