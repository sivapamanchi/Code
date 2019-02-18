Namespace BluegreenOnline

    Partial Public Class _default
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox
        Protected WithEvents Textbox2 As System.Web.UI.WebControls.TextBox
        Protected WithEvents Imagebutton1 As System.Web.UI.WebControls.ImageButton
        Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtPassword As System.Web.UI.WebControls.TextBox
        Protected WithEvents imgLogin As System.Web.UI.WebControls.ImageButton


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.


            InitializeComponent()
        End Sub

#End Region

        Public sEmail As String
        Public sPass As String
        Public sMessage As String
        Public sTutorialTransfer As String
        Public sTravelerPlusLogin As String
        Public sEncoreRewardsLogin As String
        Public sAgentLoginID As String
        Public sSSLURL As String = System.Configuration.ConfigurationManager.AppSettings("bxgwebSecureURL")
        Public path_info As String
        Public sReferrer As String

        Public bxgOwner As New OwnerWS.Owner
        Property OwnerId As String = String.Empty
        Property OwnerType As String = String.Empty
        Property TPStatus As String = String.Empty
        Property HasOwnerInformation As Boolean = False


        ''' <summary>
        ''' Handles the Load event of the Page control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


            Dim SiteCoreLogin As String = System.Configuration.ConfigurationManager.AppSettings("SitecoreHostPath").ToString()
            Dim SiteCoreLoginQS = SiteCoreLogin + "?"
            Dim qs As String = Request.QueryString.ToString()
            If String.IsNullOrEmpty(qs) Then
                Response.Redirect(SiteCoreLogin)
            Else
                Response.Redirect(SiteCoreLoginQS + qs)
            End If
            '

            'Me.Form.DefaultFocus = txtEmail.ClientID
            'Me.Form.DefaultButton = txtPassword.UniqueID
            If HttpContext.Current.Request.Url.Host.ToLower() = "dev.bluegreenonline.com" Then
                Response.Redirect("http://mdev.bluegreenvacations.com/")
            ElseIf HttpContext.Current.Request.Url.Host.ToLower() = "stg.bluegreenonline.com" Then
                Response.Redirect("http://stg.bluegreenvacations.com/")
            ElseIf HttpContext.Current.Request.Url.Host.ToLower() = "bluegreenonline.com" Or HttpContext.Current.Request.Url.Host.ToLower() = "www.bluegreenonline.com" Then
                Response.Redirect("http://www.bluegreenvacations.com/")
            End If

            If Not String.IsNullOrEmpty(Request.ServerVariables("HTTP_REFERER")) AndAlso Request.ServerVariables("HTTP_REFERER").ToLower.Contains("bgvfs") Then
                OwnerId = Request.Cookies("OwnerInfo")("OwnerId")
                OwnerType = Request.Cookies("OwnerInfo")("OwnerType")
                TPStatus = Request.Cookies("OwnerInfo")("TPStatus")

                FormsAuthentication.SignOut()
                For Each cookie As String In Request.Cookies.AllKeys
                    Request.Cookies.Remove(cookie)
                Next



                bxgOwner = Session("BXGOwnerMedallia")

                If Not bxgOwner Is Nothing Then
                    HasOwnerInformation = True

                    OwnerId = bxgOwner.Arvact


                    If bxgOwner.User(0).isSampler Then
                        OwnerType = "SAMPLER"
                    Else
                        If Session("OwnerContractType") = "Vacation Club" Then
                            OwnerType = "VACCLUB"
                        Else
                            OwnerType = "TRADITIONAL"
                        End If
                    End If

                    If bxgOwner.TravelerPlusMembership.IsTravelerPlusEligible Then
                        If bxgOwner.TravelerPlusMembership.AccountExpired Then
                            TPStatus = "EXPIRED"
                        Else
                            TPStatus = "ACTIVE"
                        End If
                    Else
                        TPStatus = "NOTELIGIBLE"
                    End If
                End If
            End If

            Dim tutorialRedirect As String = Session("ReferrerURL")

            If tutorialRedirect IsNot Nothing Then
                If tutorialRedirect.Contains("/tutorials/default.aspx") Then
                    sTutorialTransfer = "True"
                End If
            End If

            'Check for user redirected for login. Assign the path info to  
            'a variable to re-assign to session variable in case of session values cleared

            'If Session("_path_info") <> "" Then
            '    path_info = Session("_path_info")
            'End If

            If Request.QueryString("ReturnUrl") <> "" Then
                path_info = Request.QueryString("ReturnUrl")
                path_info = path_info.Replace("%2f", "/")
            End If

            sMessage = ""
            sTravelerPlusLogin = ""
            sEncoreRewardsLogin = ""

            'Sales users that are logged in go to the sales login page
            If Session("SalesUser") = "BLUEGREEN" Then
                Response.Redirect("SalesLogin.aspx", True)
            End If

            HandleRedirects()

            Dim email_id, pass As String
            email_id = Session("email_id")
            pass = Session("password")

            'Allow Owner support agents to login as the owner from their application
            If Request.Form("AgentID") <> "" Then
                Dim sURL As String = Request.ServerVariables("HTTP_REFERER") & ""
                If Request.Form("AgentID") = "SALESKIOSK" And sURL.ToLower.IndexOf("saleskiosk") > -1 Then
                    email_id = "travelerplus@bxgcorp.com"
                    pass = "hut44#"
                ElseIf Request.Form("AgentID") = "SALESKIOSKVC" And sURL.ToLower.IndexOf("saleskioskvc") > -1 Then
                    email_id = "vacationclub@bxgcorp.com"
                    pass = "hut44#"
                Else
                    email_id = Request.Form("AgentLoginEmail")
                    pass = Request.Form("AgentLoginPassword")
                End If
                sAgentLoginID = Request.Form("AgentID")
            End If

            sEmail = ""
            sPass = ""
            bxgOwner = Session("BXGOwnerMedallia")
            Dim ownerContractType As String = Session("OwnerContractType")

            Session.Clear()
            Session("BXGOwnerMedallia") = bxgOwner
            Session("OwnerContractType") = ownerContractType

            're-assign path info to session variable
            Session("_path_info") = path_info
            If Not IsPostBack Then
                Try
                    'Allow logins from elsewhere to feed into this page
                    If email_id <> "" And pass <> "" Then
                        sEmail = email_id
                        sPass = pass
                    End If
                Catch
                End Try
            End If

            'Display message for session timeouts
            If Request.QueryString("sess") = "timeout" Then
                sMessage = GetMessage("Either you are requesting a page that requires sign in for access, or your previous Bluegreen Online session timed out after 20 minutes of inactivity. Please sign in below.")
            End If
            'Display message for blocked accounts
            If Request.QueryString("acctstat") = "block" Then
                sMessage = GetMessage("Your account does not qualify for online access at this time.  <BR>Please contact us at 800.456.CLUB(2582) and select option 2 to learn how to enable your account.")

            End If
            If Request.QueryString("ErrMessage") <> "" Then
                sMessage = GetMessage("Oasis Lakes owners: <font color='#000000'>The association news page has moved. To access it, please sign in below and click on the picture of The Fountains/Oasis Lakes clubhouse at the top of the home page. Then click on the <strong style='font-size:8pt'>Association Owners</strong> link on the left side of the Resort Detail page. If you have not yet enrolled in Bluegreen Online, click on the <strong style='font-size:8pt'>Not registered?</strong> link to do so.</font>")

            End If


            'Display an error at the top of the login page if the Login was unsuccessful
            If Request.QueryString("error") = "NoConn" Then
                sMessage = GetMessage("We have encountered an unexpected error. Please wait a few minutes and try to log in again. We apologize for the inconvenience.")
            End If

            If Request.QueryString("lo") = "1" Or Request.QueryString("error") = "NoACK" Then
                Session.Clear()
                Session.Abandon()
                Session.RemoveAll()

                If Request.Cookies("ASP.NET_SessionId") IsNot Nothing Then
                    Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                    Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-20)
                End If
            End If


        End Sub

        Public Function GetMessage(strMessage As String) As String
            Dim sMessage As String
            sMessage = "<div class=""alert alert-danger alert-dismissable""  role=""alert"">"
            sMessage &= "<button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button>"
            sMessage &= "<p>" & strMessage & "</p>"
            sMessage &= "</div>"
            Return sMessage
        End Function




        Public Sub HandleRedirects()
            'Get the HTTP_HOST AND HTTP_REFERER
            Dim sHost As String = Request.ServerVariables("HTTP_HOST").ToLower.Replace("www.", "")
            Dim sHostTP As String = (Request.ServerVariables("HTTP_HOST") & Request.ServerVariables("PATH_INFO")).ToLower.Replace("www.", "")
            sReferrer = Request.ServerVariables("HTTP_REFERER") & ""
            Dim sRedirectURL As String

            bxgOwner = Session("BXGOwnerMedallia")
            Dim ownerContractType As String = Session("OwnerContractType")
            Session.Clear()
            Session("BXGOwnerMedallia") = bxgOwner
            Session("OwnerContractType") = ownerContractType


            If Convert.ToString(System.Configuration.ConfigurationManager.AppSettings("LogLoginProcess")) = "yes" Then
                Dim objLogging As New BXGDiagnostics.EventLogging("BlueGreenOnline", "LoginProcess")
                objLogging.LogEvent("Host: " & sHost & "HostTP: " & sHostTP & " Refferrer: " & sReferrer, Diagnostics.EventLogEntryType.Information, True)
                objLogging.LogEvent("sHostTP.ToLower().IndexOf(encorerewards): " & sHostTP.ToLower().IndexOf("encorerewards"), Diagnostics.EventLogEntryType.Information, True)
                objLogging = Nothing
            End If

            If sReferrer.IndexOf(sHost) = -1 Then
                'Are we a traveler plus login?
                If (sHostTP.ToLower().IndexOf("travelerplus") > 0) Then
                    sTravelerPlusLogin = "TRUE"
                    'imgLogo.ImageUrl = "TravelerPlus/owner/images/bgtp_logo_lrg.gif"

                    'Are we a encore rewards login?
                ElseIf (sHostTP.ToLower().IndexOf("encorerewards") >= 0) Then
                    sEncoreRewardsLogin = "TRUE"
                    'imgLogo.ImageUrl = "images/e_logo.gif"
                Else
                    'If we came from somewhere else then check our redirect list
                    Dim C As New clsDBConnectivity
                    C.dbCmnd.CommandText = "uspCheckForRedirection"
                    C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                    C.dbCmnd.Parameters.Clear()
                    C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@URL", System.Data.SqlDbType.VarChar, 50)).Value = sHost

                    sRedirectURL = C.dbCmnd.ExecuteScalar()

                    C.Close()
                    C = Nothing

                    'We found a redirect page, so lets go there
                    If sRedirectURL <> Nothing Then
                        Response.Redirect(sRedirectURL, True)
                    End If
                End If
            End If
        End Sub
    End Class

End Namespace

