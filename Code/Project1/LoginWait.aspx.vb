Imports System.Web.Configuration

Namespace BluegreenOnline

    Partial Class LoginWait
        Inherits System.Web.UI.Page
        Public path_info As String

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim VSSA_Login As Boolean = False
            Dim Sitecore_Login As Boolean = True
            Dim sReferer As String = Request.ServerVariables("HTTP_REFERER")

            If Session("_path_info") <> "" And Session("_path_info") <> "/" Then
                path_info = Session("_path_info")
                'path_info = path_info.Replace("/bluegreenonline", "")
                'path_info = path_info
            End If

            'Preserve Sales user login state
            If Session("SalesUser") = "BLUEGREEN" Then
                Session.Clear()
                RegenerateSessionID()
                Session("SalesUser") = "BLUEGREEN"
                Session("_path_info") = path_info
            Else
                If Not Session("newUser") Then
                    Session.Clear()
                    RegenerateSessionID()
                End If
                Session("_path_info") = path_info
            End If

            If Not String.IsNullOrEmpty(sReferer) AndAlso sReferer.ToLower.Contains("bluegreenowner.com") Then
                Sitecore_Login = True
            ElseIf Not String.IsNullOrEmpty(sReferer) AndAlso sReferer.ToLower.Contains("vssa.bxgcorp.com") Then
                VSSA_Login = True
                Session.Clear()
                Sitecore_Login = False
            Else
                Sitecore_Login = False
            End If

            'ReInitiate Timeout by getting default values from Web Config inorder to Reset The user to Session Default value.
            Dim sessionSection = DirectCast(WebConfigurationManager.GetSection("system.web/sessionState"), SessionStateSection)
            HttpContext.Current.Session.Timeout = sessionSection.Timeout.TotalMinutes

            If Not Session("newUser") Then
                'Try
                'Collect information from when a rep logs in as an owner
                Session("LoginEmail") = Request.Form("txtEmail").Trim
                Session("LoginPassword") = Request.Form("txtPassword").Trim
                Session("IsTravelerPlusOwner") = Request.Form("IsTravelerPlusOwner")
                Session("AgentLoginID") = Request.Form("AgentLoginID")
                Session("IsTravelerPlusEmployee") = Request.Form("IsTravelerPlusEmployee")
                Session("TPEmployeeEmail") = Request.Form("txtEmail").Trim
                Session("TPEmployeePassword") = Request.Form("txtPassword").Trim
                Session("ownerACCT") = Request.Form("ownerACCT")
                Session("ownerARVACT") = Request.Form("ownerARVACT")
                Session("fromVSSA") = Request.Form("fromVSSA")
                Session("IsTutorialTransfer") = Request.Form("IsTutorialTransfer")
                Session("IsEncoreRewardsOwner") = Request.Form("IsEncoreRewardsOwner")
                Session("sLOCATION") = Request.Form("Location")
                Session("redirect_LCD") = Request.Form("redirect_LCD")
                If Request.Form("AgentLoginID") = "Sales_LCD" Then
                    Session("_path_info") = Session("redirect_LCD")
                    createCookieForKiosk(Request.Form("txtEmail").Trim)
                End If

                If VSSA_Login Then
                    Session("fromVSSA") = "TRUE" ' testing for Kiosk should be set to TRUE for VSSA
                    loginAsUser()
                ElseIf Not Sitecore_Login AndAlso Session("LoginEmail").ToString.ToLower.Equals("travelerplus@bxgcorp.com") Then
                    ' If Patrick Jones account and not from VSSA or Sitecore login, assume this is a Sales Kiosk
                    Session("fromVSSA") = "FALSE"
                    loginAsUser()
                ElseIf Not Sitecore_Login Then
                    'unauthorized access
                    Dim loginPage As String = System.Configuration.ConfigurationManager.AppSettings("SitecoreHostPath")
                    FormsAuthentication.SignOut()
                    For Each cookie As String In Request.Cookies.AllKeys
                        Request.Cookies.Remove(cookie)
                    Next
                    Response.Redirect(loginPage)
                End If

            End If

            Session("UnsecuredURL") = ConfigurationSettings.AppSettings("bxgwebUnsecureURL")


            'Login hijack for Sales users
            Try
                If Request.Form("txtEmail").ToLower() = "sales" And Request.Form("txtPassword").ToLower() = "bluegreen" Then
                    Session("SalesUser") = "BLUEGREEN"
                    Response.Redirect("SalesLogin.aspx", True)
                End If
            Catch
            End Try
        End Sub

        Sub createCookieForKiosk(ByVal email As String)
            ' MTW, 8/18/2015 - create the forms authentication 
            Dim ticket As FormsAuthenticationTicket = New FormsAuthenticationTicket(1, email, DateTime.Now, DateTime.Now.AddMinutes(30), False, "Authenticated", FormsAuthentication.FormsCookiePath)

            ' Encrypt the ticket using the machine key
            Dim encryptedTicket As String = FormsAuthentication.Encrypt(ticket)

            ' Add the cookie to the request to save it
            Dim cookie As HttpCookie = New HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            cookie.HttpOnly = True
            ' set the domain as parent for use by all subdomains
            cookie.Domain = "bluegreenowner.com"

            If ticket.IsPersistent Then
                cookie.Expires = ticket.Expiration
            End If

            Response.Cookies.Add(cookie)
        End Sub

        Sub loginAsUser()
            Dim MyremotePost As New BGSitecoreRemotePost
            Dim RemotePostURL As String = System.Configuration.ConfigurationManager.AppSettings("SitecoreRemoteSignIn")
            Try
                'IIf(RemotePostURL.EndsWith("/"), RemotePostURL = String.Format("{0}{1}", RemotePostURL, "SignInProcess.aspx"), RemotePostURL = String.Format("{0}{1}{2}", RemotePostURL, "/", "SignInProcess.aspx"))
                MyremotePost.Url = RemotePostURL
                MyremotePost.add("txtPassword", Session("LoginPassword"))
                MyremotePost.add("txtEmail", Session("LoginEmail"))
                MyremotePost.add("ownerARVACT", Session("ownerARVACT"))
                MyremotePost.add("AgentLoginID", Session("AgentLoginID"))
                MyremotePost.add("ownerACCT", Session("ownerACCT"))
                MyremotePost.add("fromVSSA", Session("fromVSSA"))

                MyremotePost.Post()

            Catch
                '' do nothing
            End Try

        End Sub

        'Regenerate session id for fixing Session Identifier Not Updated.

        Sub RegenerateSessionID()
            Dim manager As SessionIDManager
            Dim oldId As String
            Dim newId As String
            Dim isRedir As Boolean
            Dim isAdd As Boolean
            Dim ctx As HttpApplication
            Dim mods As HttpModuleCollection
            Dim ssm As System.Web.SessionState.SessionStateModule
            Dim fields() As System.Reflection.FieldInfo
            Dim rqIdField As System.Reflection.FieldInfo
            Dim rqLockIdField As System.Reflection.FieldInfo
            Dim rqStateNotFoundField As System.Reflection.FieldInfo
            Dim store As SessionStateStoreProviderBase
            Dim field As System.Reflection.FieldInfo
            Dim lockId
            manager = New System.Web.SessionState.SessionIDManager
            oldId = manager.GetSessionID(Context)
            newId = manager.CreateSessionID(Context)
            manager.SaveSessionID(Context, newId, isRedir, isAdd)
            ctx = HttpContext.Current.ApplicationInstance
            mods = ctx.Modules
            ssm = CType(mods.Get("Session"), System.Web.SessionState.SessionStateModule)
            fields = ssm.GetType.GetFields(System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)
            store = Nothing : rqLockIdField = Nothing : rqIdField = Nothing : rqStateNotFoundField = Nothing
            For Each field In fields
                If (field.Name.Equals("_store")) Then store = CType(field.GetValue(ssm), SessionStateStoreProviderBase)
                If (field.Name.Equals("_rqId")) Then rqIdField = field
                If (field.Name.Equals("_rqLockId")) Then rqLockIdField = field
                If (field.Name.Equals("_rqSessionStateNotFound")) Then rqStateNotFoundField = field
            Next
            lockId = rqLockIdField.GetValue(ssm)
            If ((Not IsNothing(lockId)) And (Not IsNothing(oldId))) Then store.ReleaseItemExclusive(Context, oldId, lockId)
            rqStateNotFoundField.SetValue(ssm, True)
            rqIdField.SetValue(ssm, newId)

        End Sub
    End Class
    Public Class BGSitecoreRemotePost


        Dim Inputs As New System.Collections.Specialized.NameValueCollection


        Public Url As String = ""
        Public Method As String = "post"
        Public FormName As String = "frmLoginAsUser"

        Sub add(ByVal name As String, ByVal value As String)

            Inputs.Add(name, value)

        End Sub


        Sub Post()
            Try
                Dim i As Integer = 0

                Dim PageStream As New StringBuilder

                PageStream.Append("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">")
                PageStream.Append("<html xmlns=""http://www.w3.org/1999/xhtml""><head><title></title></head>")
                PageStream.Append("<body onload='document." & FormName & ".submit()'>")
                PageStream.Append("<form name='" & FormName & "' method='" & Method & "' action='" & Url & "' >")

                System.Web.HttpContext.Current.Response.Clear()

                While i < Inputs.Keys.Count

                    PageStream.Append("<input name='" & Inputs.Keys(i) & "' type='hidden' value='" & Inputs(Inputs.Keys(i)) & "' />")
                    i = i + 1

                End While
                PageStream.Append("</form></body></html>")


                System.Web.HttpContext.Current.Response.Write(PageStream.ToString)
                System.Web.HttpContext.Current.Response.End()

            Catch taEx As System.Threading.ThreadAbortException
                ''do nothing
            End Try


        End Sub


    End Class

End Namespace
