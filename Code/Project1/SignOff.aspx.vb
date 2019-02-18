Imports System.IdentityModel.Services

Public Class SignOff
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Replaced Clear with Abandon in order to completely remove the session id on Signoff
        Session.Abandon()

        FormsAuthentication.SignOut()

        If Request.Cookies("ASP.NET_SessionId") IsNot Nothing Then
            Response.Cookies("ASP.NET_SessionId").Value = String.Empty
            Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-20)
        End If

        For Each cookie As String In Request.Cookies.AllKeys
            Dim tmpCookie As HttpCookie = Request.Cookies(cookie)
            If Not tmpCookie Is Nothing AndAlso tmpCookie.Name.ToLower <> "ownerinfo" Then
                Request.Cookies.Remove(cookie)
                'Making Sure we expire same cookie from response object
                Response.Cookies(tmpCookie.Name).Value = String.Empty
                Response.Cookies(tmpCookie.Name).Expires = DateTime.Now.AddMonths(-20)
            End If
        Next

        'Try
        '    WSFederationAuthenticationModule.FederatedSignOut(New Uri(ConfigurationManager.AppSettings("ADFS_URL")), New Uri(ConfigurationManager.AppSettings("SignOffRedirect")))
        'Catch
        '    Response.Redirect(String.Format("{0}/adfs/ls/?wa=wsignout1.0", ConfigurationManager.AppSettings("ADFS_URL")))
        'End Try

        Response.Redirect(ConfigurationManager.AppSettings("SitecoreHostPath") + "?signoff=true")


    End Sub

End Class