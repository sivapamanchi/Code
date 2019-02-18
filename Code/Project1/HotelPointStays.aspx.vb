Namespace BluegreenOnline
    Partial Class TravelerPlus_owner_HotelPointStays
        Inherits System.Web.UI.Page

        Public BXGowner As New OwnerWS.Owner
        Public TPLevel As String

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Session("BXGOwner") Is Nothing Then
                Session("_path_info") = Request.RawUrl
                Response.Redirect("http://" & Request.ServerVariables("HTTP_HOST") & "/default.aspx?sess=timeout", True)
            End If

            BXGowner = Session("BXGOwner")

            If Not Session("IsTravelerPlusEmployee") = "TRUE" Then
                If BXGowner.TravelerPlusMembership.IsTravelerPlusEligible = False Then
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "default.aspx?sess=timeout", True)
                End If
            ElseIf Session("IsTravelerPlusEmployee") = "TRUE" Then
                TPLevel = "2"
            Else
                TPLevel = ""
            End If

        End Sub

    End Class

End Namespace

