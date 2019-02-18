Imports BxgCorp.EncoreRewards
Imports System.Collections.Generic
Imports IBM.Data.DB2.iSeries


Namespace BluegreenOnline

    Partial Class LoginProcess
        Inherits System.Web.UI.Page

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

        Dim owner As New Bluegreenowner
        Dim VSSAcookie As HttpCookie
        'Connect to the owner service
        Dim ownerService As New OwnerWS.OwnerWS1SoapClient
        Dim bxgOwner As New OwnerWS.Owner
        Public bxgOwnerTemp As New OwnerWS.Owner
        Dim bSuccess As Boolean
        Dim theRedirectSet As String = "owner/home.aspx" 'TODO: once BGO has been deployed to production with the inclusion of the new BGModern site, the "theRedirectSet" var can be set to the home route in BGModern
        Dim ProxyPage As String
        Public path_info As String
        Public StartTime As DateTime = Nothing
        Public StopTime As DateTime = Nothing
        Public ElapsedTime As TimeSpan = Nothing
        Public ChangePassword As Boolean = False


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'TODO: once BGO has been deployed to production with the inclusion of the new BGModern site, the "theRedirectSet" var can be set to the home route in BGModern
            ProxyPage = System.Configuration.ConfigurationSettings.AppSettings("LinkForHomePage").ToString()
            If String.IsNullOrWhiteSpace(ProxyPage) Or IsNothing(ProxyPage) Then 'Session("LinkForHomePage")) Then
                theRedirectSet = "owner/home.aspx"
            Else
                theRedirectSet = ProxyPage
                Session("LinkForHomePage") = ProxyPage
            End If

            'Session("StartTime") = Date.Now

            RegenerateSessionID()

            Dim xx As String = Date.Now.Hour.ToString & ":" & Date.Now.Minute.ToString & ":" & Date.Now.Second.ToString


            Dim loginEmail As String = Session("LoginEmail") 'VSSAcookie("email")
            Dim LoginPassword As String = Session("LoginPassword") 'VSSAcookie("passwd")
            Dim AgentUsername As String = Session("AgentLoginID") 'VSSAcookie("agent")

            'Check for user redirected for login. Assign the path info to  
            'a cookie to re-assign to session variable in case of session values cleared
            If Session("_path_info") <> "" Then
                If Request.Cookies("accountListCookie") IsNot Nothing Then
                    'if an owner is having multiple accounts,he would be redirected to "select account" screen after session timeout
                    If Request.Cookies("accountListCookie").Value > 1 Then
                        If Request.Cookies("userIdCookie") IsNot Nothing Then
                            If Session("LoginEmail") = Request.Cookies("userIdCookie").Value Then
                                Session("_path_info") = "login.aspx"
                            Else
                                Session("_path_info") = ""
                                Response.Cookies("accountListCookie").Expires = DateTime.Now.AddDays(-1D)
                            End If
                        End If
                    Else

                    End If
                Else
                    'whenever a different ID is used for login after session timeout,clear session path info 
                    If Request.Cookies("userIdCookie") IsNot Nothing Then
                        If Session("LoginEmail") <> Request.Cookies("userIdCookie").Value Then
                            Session("_path_info") = ""
                        End If
                    End If
                End If

            End If
            If Not IsPostBack Then
                If IsAS400Available() Then
                    DoLoginCheck(Session("LoginEmail"), Session("LoginPassword"))

                Else
                    LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "siteMaintenance.aspx", True)
                End If
            End If

            'Close connections if all else fails
            LoginEgress()


        End Sub

        Private Sub LoginEgress(Optional ByVal sRedirectTo As String = Nothing, Optional ByVal bStop As Boolean = True)

            Dim bLoginError As Boolean
            bLoginError = False
            If sRedirectTo <> Nothing Then
                If sRedirectTo.Contains("loginUnsuccessful") Or sRedirectTo.Contains("error") Or Not bxgOwner.Authenticated Then
                    bLoginError = True
                End If
            End If

            'Redirect to the page, where user redirected for sign on.
            If Session("_path_info") <> "" Then
                Dim strPathInfo As String = Session("_path_info")
                Session("_path_info") = Nothing
                'if successfully logged in, need to do a blind ADFS authentication.

                ' use Visual Studio's conditional compilation functionality to control if trying to authenticate through ADFS. Doing this so BG does not have to set up a Relying Party definition for 
                ' every workstation being used for development (ADFS requires an RP def for each machine trying to authenticate). Changing the type of build being done determines which of the branches 
                ' below is included in the binary.

                Response.Redirect(strPathInfo, bStop)


            End If

            If sRedirectTo <> Nothing Then
                '9/2/11 KO - added to redirect TPEmployee to the right page.  
                If Session("IsTravelerPlusEmployee") = "TRUE" And sRedirectTo.IndexOf("error") > 0 Then
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "TravelerPlus/loginUnsuccessful.aspx?ut=EMPLOYEE", bStop)
                End If

                ' use Visual Studio's conditional compilation functionality to control if trying to authenticate through ADFS. Doing this so BG does not have to set up a Relying Party definition for 
                ' every workstation being used for development (ADFS requires an RP def for each machine trying to authenticate). Changing the type of build being done determines which of the branches 
                ' below is included in the binary.

                Response.Redirect(sRedirectTo, bStop)

            End If
        End Sub

        'Private Sub ADFSLoginEgress(ByVal sRedirectAfterReturnFromADFS As String)
        '    ' Authencate the user with AD. If authentication fails, register the user.
        '    Dim client As BGOOwnerToADFS.ADFSSyncServiceClient = New BGOOwnerToADFS.ADFSSyncServiceClient()
        '    Dim authResponse As BGOOwnerToADFS.UpdateResponse = New BGOOwnerToADFS.UpdateResponse()
        '    authResponse = client.AuthenticateUser(bxgOwner.Email, bxgOwner.Password)

        '    If Not (authResponse.IsSuccessful) Then
        '        Dim regResponse As BGOOwnerToADFS.UpdateResponse = New BGOOwnerToADFS.UpdateResponse()
        '        Dim tmpOwner As New BGOOwnerToADFS.BGOOwner
        '        tmpOwner.extensionAttribute1 = bxgOwner.Arvact
        '        tmpOwner.userPrincipalName = bxgOwner.Email
        '        tmpOwner.password = bxgOwner.Password
        '        regResponse = client.RegisterOwnerBeforeDailySync(tmpOwner)
        '        If regResponse.IsSuccessful Then
        '            ' pause for configured amount of milliseconds to allow AD to process the new user
        '            Dim sSleepTime As Integer = CInt(System.Configuration.ConfigurationManager.AppSettings("SSORegisterOwnerSleepTimer"))
        '            System.Threading.Thread.Sleep(sSleepTime)
        '        End If
        '    End If

        '    ' ADFS always returns to a page defined in ADFS. In order to redirect the session to the correct page, we will store the desired URL in Session("_path_info") and redirect to their from the return page.
        '    Session("_path_info") = sRedirectAfterReturnFromADFS

        '    Response.Redirect("/BlindADFSLogin.aspx?" + sRedirectAfterReturnFromADFS)
        'End Sub

        Private Sub DoLoginCheck(ByVal Username As String, ByVal UserPassword As String)

            '####################
            With owner
                If Username <> "" Then
                    'save user name for an owner in a cookie
                    Dim userIdCookie As New HttpCookie("userIdCookie")
                    userIdCookie.Value = Session("LoginEmail")
                    userIdCookie.Expires = DateTime.Now.AddDays(1)
                    userIdCookie.Secure = True

                    Response.Cookies.Add(userIdCookie)

                    RegenerateSessionID()

                    Dim success As Integer
                    'Set flag to hide submit buttons for bookings if vacationclub or saleskiosk
                    If Session("AgentLoginID") = "SALESKIOSK" Then
                        Session("DEMOMODE") = "TRUE"
                        Session("IsTravelerPlusOwner") = "TRUE"
                        .OwnerType = "TravelerPlus"
                    End If

                    'Loop through configured demo account list
                    Dim sOmitItem() As String
                    sOmitItem = System.Configuration.ConfigurationManager.AppSettings("DemoAccountList").Split("|")

                    For x As Integer = 0 To UBound(sOmitItem)
                        If (Username.ToLower.IndexOf(sOmitItem(x).ToLower()) > -1) Then Session("DEMOMODE") = "TRUE"
                    Next

                    'If an agent logs in or it is set to enabled then expose the bonus time pages
                    checkExposeBonusTime()

                    'Check for Traveler Plus employee login
                    checkForTravelerPlusEmployeeLogin()

                    '------------------------------------------------------
                    'Fetching Owner Information from WebService OwnerWS1
                    '------------------------------------------------------
                    fetchOwnerInfoFromOwnerService()

                    If bxgOwner.Authenticated Then 'Found Owner Info

                        'There is a legacy owner object that needs to be populated to ensure pages that 
                        'still use it will remain to function.
                        'check for sampler authentication

                        Dim _accountExpired As Boolean = False
                        Dim _pointsListCount As Integer = 0

                        'Assign Session to recognise premier Owner
                        Dim premierOwnerLevels As String = "PLATINUM,GOLD,SILVER,BRONZE"

                        If Not (bxgOwner.membershipLevelDesc = "") Then
                            If (premierOwnerLevels.Contains(bxgOwner.membershipLevelDesc.ToUpper())) Then
                                Session("ispremierOwner") = IIf(bxgOwner.membershipLevelDesc = "", False, True)
                                Session("premierOwnerLevel") = IIf(bxgOwner.membershipLevelDesc = "", String.Empty, bxgOwner.membershipLevelDesc)
                            End If
                        End If

                        'Validate sampler plus accounts

                        Try

                            If Not IsNothing(bxgOwner.User(0).isSampler) Then
                                Dim expirationdate As DateTime = Convert.ToDateTime(bxgOwner.User(0).samplerExpiration)
                                If DateTime.Today > expirationdate Then
                                    Session("SamplerOnlyUser") = "SAMPLER"
                                    Session("ownernumber") = bxgOwner.Arvact
                                    LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "loginUnsuccessful.aspx?ut=SAMPLER")
                                End If

                            End If




                        Catch ex As Exception

                            Session("SamplerOnlyUser") = "SAMPLER"
                        End Try


                        populateOwnerLegacyObject()
                        populateOwnerLegacySessionVars()
                        checkPasswordChange()

                        '' MTW, 8/18/2015 - create the forms authentication 
                        'Dim ticket As FormsAuthenticationTicket = New FormsAuthenticationTicket(1, bxgOwner.Email, DateTime.Now, DateTime.Now.AddMinutes(30), False, "Authenticated", FormsAuthentication.FormsCookiePath)

                        '' Encrypt the ticket using the machine key
                        'Dim encryptedTicket As String = FormsAuthentication.Encrypt(ticket)

                        '' Add the cookie to the request to save it
                        'Dim cookie As HttpCookie = New HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                        'cookie.HttpOnly = True
                        '' set the domain as parent for use by all subdomains
                        'cookie.Domain = "bluegreenowner.com"

                        'If ticket.IsPersistent Then
                        '    cookie.Expires = ticket.Expiration
                        'End If

                        'Response.Cookies.Add(cookie)


                    Else
                        If Session("BXGOwner") IsNot Nothing Then
                            If bxgOwner.User(0).project = "51" OrElse bxgOwner.User(0).project = "52" Then
                                If Session("_path_info") <> "" Then
                                    Session("_path_info") = ""
                                End If
                                LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "loginUnsuccessful.aspx?ut=SAMPLER&from=1")
                            End If
                        Else
                            LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "default.aspx?error=NoACK")
                        End If
                    End If

                    Session("IsPendingOwner") = "FALSE"


                    Dim iNonVCAccountCount As Integer = 0
                    Dim alAccounts As New ArrayList

                    Try

                        'Get Authentication ID
                        If System.Configuration.ConfigurationManager.AppSettings("AllowBooking") <> "FALSE" Then
                        Else
                            success = 1
                        End If

                        If success = 1 Then
                            If System.Configuration.ConfigurationManager.AppSettings("AllowBooking") = "FALSE" Then
                                LoadPaymentBalance()
                            Else
                            End If

                            Bluegreenowner.CurrentOwner = owner
                            ' the above must be saved to the session for the BGModern site.
                            Session("BluegreenownerForUmbraco") = Bluegreenowner.CurrentOwner

                            '######################################################################################
                            'End of account expiration
                            '######################################################################################

                            checkTravelerplusRedirect()
                            CheckNonTravelerPlusRedirect()
                            TrackOwnerLogin()
                            bSuccess = True

                        Else
                            bSuccess = False

                        End If

                    Catch ex As Exception

                        bSuccess = False
                        Dim strEx = ex.Message

                    End Try

                    '#####################
                Else
                    If Session("SamplerOnlyUser") = "SAMPLER" Then
                        LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "loginUnsuccessful.aspx?ut=SAMPLER")
                    ElseIf Session("IsTravelerPlusEmployee") = "TRUE" Then
                        LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "TravelerPlus/loginUnsuccessful.aspx?ut=EMPLOYEE")
                    ElseIf Session("IsTravelerPlusOwner") = "TRUE" Then
                        LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "TravelerPlus/loginUnsuccessful.aspx")
                    ElseIf Session("IsEncoreRewardsOwner") = "TRUE" Then
                        LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "BluegreenRewards/loginUnsuccessful.aspx")
                    Else
                        LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "Owner/poploginhelp.aspx")
                    End If
                End If

                '###################
                'save object as session var 
                Bluegreenowner.CurrentOwner = owner
                ' the above doesn’t save to the session. it needs to be in Session for the BGModern site.
                Session("BluegreenownerForUmbraco") = Bluegreenowner.CurrentOwner

                'Persist owner data for Medallia Survey in a cookie
                SetMedalliaCookie()

            End With

            'Failed to log in
            If bSuccess = False Then

                redirectFailedLogin()

            ElseIf Session("SamplerOnlyUser") = "Players" And Session("Players") = "YES" Then
                redirectFailedLogin()
            ElseIf Session("SamplerOnlyUser") = "Pono" And Session("Pono") = "YES" Then
                redirectFailedLogin()
            Else
                ''Force User to Registration Confirmation irrespective of any condition during registration
                If Session("ownerACCT") IsNot Nothing And Session("ownerRegisterReferrer") IsNot Nothing Then
                    If Session("ownerRegisterReferrer").ToString().Contains("Register") Then
                        theRedirectSet = ConfigurationManager.AppSettings("SitecoreHostPath").ToString() + "/confirm-registration"
                    End If
                End If

                LoginEgress(theRedirectSet)

            End If

        End Sub

        Private Sub SetMedalliaCookie()
            Dim _userInfoCookies As New HttpCookie("OwnerInfo")
            Dim TPStatus As String = String.Empty
            If bxgOwner.TravelerPlusMembership IsNot Nothing AndAlso bxgOwner.TravelerPlusMembership.IsTravelerPlusEligible Then
                If bxgOwner.TravelerPlusMembership.AccountExpired Then
                    TPStatus = "EXPIRED"
                Else
                    TPStatus = "ACTIVE"
                End If
            Else
                TPStatus = "NOTELIGIBLE"
            End If
            Dim OwnerType As String = String.Empty
            If bxgOwner.User(0).isSampler Then
                OwnerType = "SAMPLER"
            Else
                If Session("OwnerContractType") = "Vacation Club" Then
                    OwnerType = "VACCLUB"
                Else
                    OwnerType = "TRADITIONAL"
                End If
            End If

            _userInfoCookies("OwnerId") = bxgOwner.Arvact
            _userInfoCookies("OwnerType") = OwnerType.ToUpper()
            _userInfoCookies("TPStatus") = TPStatus
            _userInfoCookies.Expires = DateTime.Now.AddDays(1)
            _userInfoCookies.Secure = True
            Response.Cookies.Add(_userInfoCookies)
        End Sub

        Sub redirectFailedLogin()

            If Session("SamplerOnlyUser") = "SAMPLER" Then
                LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "loginUnsuccessful.aspx?ut=SAMPLER")
            ElseIf Session("SamplerOnlyUser") = "Players" Then
                LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "loginUnsuccessful.aspx?ut=Players")
            ElseIf Session("SamplerOnlyUser") = "Pono" Then
                LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "loginUnsuccessful.aspx?ut=Pono")
            ElseIf Session("IsTravelerPlusEmployee") = "TRUE" Then
                LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "loginUnsuccessful.aspx?ut=EMPLOYEE")
            ElseIf Session("IsTravelerPlusOwner") = "TRUE" Then
                LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "default.aspx?error=11")
            ElseIf Session("IsEncoreRewardsOwner") = "TRUE" Then
                LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "default.aspx?error=12")
            Else
                LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "default.aspx?error=13")
            End If

            'ownerService.Dispose()
            ownerService = Nothing

        End Sub

        Sub CheckNonTravelerPlusRedirect()
            Dim isPopuupRedirect As Boolean = False
            With owner

                'Non traveler plus owners
                If Session("LoginAccountList").Count = 1 Then
                    'Use the default account info if there was only one account

                    'Set logo information for user session based on club membership
                    If Session("OwnerContractType") = "Vacation Club" Or Session("OwnerContractType") = "Sampler" Then
                        'Vacation Club Member
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



                        If Session("IsEncoreRewardsOwner") = "TRUE" Then
                            .OwnerType = "EncoreRewards"
                            If bxgOwner.ChPWDflag Then

                                'Commented for Bug# 83668-Arvact still getting old change password screen
                                'theRedirectSet = "owner/resetpassword.aspx"
                                'LoginEgress("owner/resetpassword.aspx")
                            Else
                                theRedirectSet = "BluegreenRewards/index.aspx"
                                'LoginEgress("BluegreenRewards/index.aspx")
                            End If


                        Else


                            Session("BXGOwner") = bxgOwner

                            If bxgOwner.ChPWDflag Then
                                'Commented for Bug# 83668-Arvact still getting old change password screen
                                'bSuccess = True
                                'theRedirectSet = "owner/resetpassword.aspx"

                            ElseIf Session("IsTutorialTransfer") = "True" Then
                                theRedirectSet = "owner/OwnerTutorial.aspx"
                            Else

                                If Session("sLOCATION") = "TravelerPlus" Then
                                    theRedirectSet = "owner/vcTravelerplus.aspx"
                                Else

                                    'if the account is overdue we need to redirect. However is the owner is under Installment Payment (IP) just go to home page.
                                    If bxgOwner.InstallmentPlan(0).InstallmentStatus = "IS" Or bxgOwner.InstallmentPlan(0).InstallmentStatus = "ID" Then

                                        Dim sitecoreURL As String = System.Configuration.ConfigurationManager.AppSettings("SitecoreHostPath")
                                        theRedirectSet = sitecoreURL + "/payments/installment-suspended"
                                        isPopuupRedirect = True

                                    ElseIf CDbl(bxgOwner.ReservationDuePaymentBalance) > 0.0 And bxgOwner.InstallmentPlan(0).InstallmentStatus <> "IP" Then
                                        'changing the redirection to sitecore
                                        Dim sitecoreURL As String = System.Configuration.ConfigurationManager.AppSettings("SitecoreHostPath")
                                        'theRedirectSet = sitecoreURL + "/payments/payment-reminder"
                                        theRedirectSet = "owner/paymentReminder.aspx"
                                        isPopuupRedirect = True

                                    ElseIf bxgOwner.AnnualPointsExpiration.SavePointsEligible = True And bxgOwner.AnnualPointsExpiration.SavePointsPopup = True Then

                                        If (bxgOwner.User(0).HomeProject = "52") Then
                                            If String.IsNullOrWhiteSpace(Session("LinkForHomePage")) Then
                                                theRedirectSet = "owner/home.aspx"
                                            End If
                                        Else
                                            theRedirectSet = "owner/myPoints.aspx?display=1"
                                            isPopuupRedirect = True
                                        End If

                                    ElseIf bxgOwner.TravelerPlusMembership.TPRenewPopup = True And isPopuupRedirect = False Then

                                        Try
                                            'Remove Traveler Plus reminder page from auto renewal TP owners
                                            If bxgOwner.TravelerPlusMembership.IsTravelerPlusAutoRenew = True Then
                                                'TODO: once BGO has been deployed to production with the inclusion of the new BGModern site, the "theRedirectSet" var can be set to the home route in BGModern
                                                If String.IsNullOrWhiteSpace(Session("LinkForHomePage")) Then
                                                    theRedirectSet = "owner/home.aspx"
                                                Else
                                                    theRedirectSet = Session("LinkForHomePage").ToString()
                                                End If
                                            Else
                                                If bxgOwner.User(0).HasAccountInSecondaryMarket = False And bxgOwner.PointsTotalSecondaryMarket <= 0 Then
                                                    theRedirectSet = "TravelerPlus/owner/ownerrenewal.aspx?display=1"
                                                End If
                                            End If
                                            isPopuupRedirect = True


                                        Catch ex As Exception

                                        End Try


                                    Else

                                        'This new logical address the new Marketing application "LCD".
                                        'Any modification MUST be discussed to Creative Dept.
                                        'The LCD application hardcode hidden info based on "travelerplus@bxgcorp.com" to log in on BGO without prompt and
                                        'redirect to whatever page.
                                        If Session("AgentLoginID") = "Sales_LCD" Then
                                            theRedirectSet = Session("redirect_LCD")

                                        Else
                                            'otherwise just go to home page
                                            'TODO: once BGO has been deployed to production with the inclusion of the new BGModern site, the "theRedirectSet" var can be set to the home route in BGModern
                                            If String.IsNullOrWhiteSpace(Session("LinkForHomePage")) Then
                                                theRedirectSet = "owner/home.aspx"
                                            Else
                                                theRedirectSet = Session("LinkForHomePage").ToString()
                                            End If
                                        End If

                                    End If

                                End If
                            End If

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

                        'TODO: once BGO has been deployed to production with the inclusion of the new BGModern site, the "theRedirectSet" var can be set to the home route in BGModern
                        If String.IsNullOrWhiteSpace(Session("LinkForHomePage")) Then
                            theRedirectSet = "owner/home.aspx"
                        Else
                            theRedirectSet = Session("LinkForHomePage").ToString()
                        End If

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



                        If Session("IsEncoreRewardsOwner") = "TRUE" Then
                            .OwnerType = "EncoreRewards"
                            theRedirectSet = "BluegreenRewards/index.aspx"
                        Else
                            theRedirectSet = "owner/homeFixed.aspx"
                        End If
                    End If

                ElseIf Session("LoginAccountList").Count > 1 Then
                    'save number of accounts an owner have in a cookie
                    Dim accCookie As New HttpCookie("accountListCookie")
                    accCookie.Value = Session("LoginAccountList").Count
                    accCookie.Secure = True
                    Response.Cookies.Add(accCookie)

                    RegenerateSessionID()

                    'Go to an account selection screen and let the user pick

                    theRedirectSet = "login.aspx"
                Else
                    bSuccess = False

                End If

            End With

        End Sub

        Sub checkTravelerplusRedirect()

            With owner

                Dim sIsTPEligible As String = "NO"
                If (owner.OwnerTPLevel = Travelerpluslevel.TravelerPlusPlusLevel) Or (owner.OwnerTPLevel = Travelerpluslevel.TravelerPlusLevel) Or (owner.OwnerTPLevel = Travelerpluslevel.TravelerPlus3) Then
                    sIsTPEligible = "YES"
                    Session("IsTravelerPlusEligible") = "TRUE"
                    Session("siteNavjs") = "ownerTP_data"
                    .OwnerIsTravelerPlusEligible = True
                Else
                    sIsTPEligible = "NO"
                    Session("IsTravelerPlusEligible") = "FALSE"
                    .OwnerIsTravelerPlusEligible = False
                End If



                'Redirect for traveler plus logins if qualified
                If Session("IsTravelerPlusOwner") = "TRUE" Then
                    .OwnerType = "TravelerPlus"

                    If sIsTPEligible = "YES" Then
                        'Fill in our template as a vacation club user
                        Session("siteLogo") = "bgvc_logo2.gif"
                        Session("sitePhoneNumberImg") = "hdrTag_x.gif"
                        Session("sitePhoneNumberMouseover") = "hdrTag_o.gif"
                        Session("sitePhoneNumber") = "800.456.CLUB (2582)"
                        Session("siteLogoAlt") = "Bluegreen Vacation Club"
                        Session("siteLogoHeight") = "63"
                        Session("siteTemplateHeight") = "253"
                        Session("siteTemplateImage") = "ownerBkgd1b_lft.gif"



                    End If

                End If
            End With

        End Sub

        Private Function updateSuffixInOwnerName(ByVal name As String) As String
            name = StrConv(Trim(name), VbStrConv.ProperCase)
            updateSuffixInOwnerName = name
            If Not String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings("ownerNameSuffix")) Then
                Dim nameSuffixes As String = System.Configuration.ConfigurationManager.AppSettings("ownerNameSuffix")
                Dim suffix() As String = nameSuffixes.Split("|")
                Dim ownerName() As String = updateSuffixInOwnerName.Trim().Split(" ")
                If ownerName.Length > 1 Then
                    For count As Integer = 0 To suffix.Length - 1
                        If updateSuffixInOwnerName.ToUpper().Contains(suffix(count)) Then
                            For counter As Integer = 0 To ownerName.Length - 1
                                If ownerName(counter).ToUpper() = suffix(count).ToUpper() Then
                                    updateSuffixInOwnerName = name.Replace(ownerName(counter), suffix(count))
                                End If
                            Next
                        End If
                    Next
                End If
            End If
            Return updateSuffixInOwnerName
        End Function

        Sub fetchOwnerInfoFromOwnerService()

            Try
                If System.Configuration.ConfigurationManager.AppSettings("PCEnvironment") = "Production" Then
                    'ownerService.Timeout = 25000
                Else
                    'ownerService.Timeout = 200000
                End If

                ' bxgOwner = ownerService.Authenticate(Session("LoginEmail"), Session("LoginPassword"))
                If Not bxgOwner.Authenticated Then
                    bxgOwner.Authenticated = True
                End If
                If bxgOwner.Authenticated Then
                    '###########################################################################
                    'Write owner to session. This is the new owner that is coming from the
                    'owner web service.  This is the object you should be using for everything 
                    'you create beyond 03/10/2009
                    bxgOwner = ownerService.fetchOwner("", "", "", Session("LoginEmail"), "")
                    bxgOwner.firstName = updateSuffixInOwnerName(bxgOwner.firstName)
                    bxgOwner.lastName = updateSuffixInOwnerName(bxgOwner.lastName)
                    'incase owner has only one account
                    If bxgOwner.Accounts.Length = 1 Then
                        If bxgOwner.User(0).project = "51" OrElse bxgOwner.User(0).project = "52" Then
                            bxgOwner.Authenticated = False
                        Else
                            bxgOwner.Authenticated = True
                        End If
                    Else
                        For count As Integer = 0 To bxgOwner.Accounts.Length - 1
                            If bxgOwner.User(0).project = "51" OrElse bxgOwner.User(0).project = "52" Then
                                bxgOwner.Authenticated = False
                            Else

                                'It looks weird the following since there is an initial condition above that through the code here. 
                                'The execution of fetchowner method on this case wiped out the authenticad value. On this case, it will be was "False"!
                                bxgOwner.Authenticated = True
                                Exit For
                            End If
                        Next
                    End If
                    'OWner information retrieved from web service is saved to session variable.
                    Session("BXGOwner") = bxgOwner

                End If

            Catch ex As System.Web.Services.Protocols.SoapException

                Dim Path As String = Nothing
                Dim errMsg As New StringBuilder

                Dim sHost As String = (Request.ServerVariables("HTTP_HOST") & Request.ServerVariables("PATH_INFO")).ToLower.Replace("www.", "")
                Path = "http://" & Left(sHost, sHost.IndexOf("/")) & System.Configuration.ConfigurationManager.AppSettings("bxgwebImgPath")

                errMsg.Append("Machine: " & sHost & "<br />")
                errMsg.Append("Owner : " & bxgOwner.Arvact & "<br />")
                errMsg.Append(ex.Message)
                sendMessage("OLPSupport@bluegreencorp.com", "", "Soap Exception - Error on Log in Process on BGO ", errMsg)

                LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "default.aspx?error=NoConn")

            Catch ex As Exception

                Dim Path As String = Nothing
                Dim errMsg As New StringBuilder

                Dim sHost As String = (Request.ServerVariables("HTTP_HOST") & Request.ServerVariables("PATH_INFO")).ToLower.Replace("www.", "")
                Path = "http://" & Left(sHost, sHost.IndexOf("/")) & System.Configuration.ConfigurationManager.AppSettings("bxgwebImgPath")

                errMsg.Append("Machine: " & sHost & "<br />")
                errMsg.Append("Owner : " & bxgOwner.Arvact & "<br />")
                errMsg.Append(ex.Message)
                sendMessage("OLPSupport@bluegreencorp.com", "", "Error on Log in Process on BGO ", errMsg)

                LoginEgress(System.Configuration.ConfigurationManager.AppSettings("bxgwebUnsecureURL") & "default.aspx?error=NoConn")

            Finally
                'close out the owner Service
                ownerService = Nothing
            End Try

        End Sub

        Private Sub LoadPaymentBalance()

            With owner
                Try
                    Dim dTotal As Double = 0
                    Dim sCurProj As String = ""
                    Dim alAccounts As New ArrayList
                    Dim bSamplerHolder As Boolean = False


                    Dim counter As Integer = 0
                    Do Until counter = bxgOwner.maintFees.Length

                        With bxgOwner.maintFees(counter)

                            'Accumulate all account information
                            Dim AccountInfo As New clsLoginAccount
                            Try

                                AccountInfo.VIP = .vip
                            Catch ex As Exception
                                AccountInfo.VIP = False
                            End Try

                            'Try

                            '    If .saleType = "S" Or .saleType = "L" Then
                            '        AccountInfo.Sampler = True
                            '    Else
                            '        AccountInfo.Sampler = False
                            '    End If

                            'Catch ex As Exception
                            '    AccountInfo.Sampler = False
                            'End Try


                            AccountInfo.ProjNum = .projnum
                            'AccountInfo.ContractID = .acctNum
                            If AccountInfo.ProjNum <> "51" And AccountInfo.ProjNum <> "52" Then

                                If .projnum = "50" Then
                                    AccountInfo.ContractType = "Vacation Club"
                                    AccountInfo.ResortName = "Vacation Club"
                                    AccountInfo.ResortID = 0
                                    Session("ProjNum") = .projnum
                                    Session("ContractType") = AccountInfo.ContractType
                                Else
                                    AccountInfo.ContractType = StrConv(Trim(.proname), VbStrConv.ProperCase)
                                    Dim sTmpRes() As String = GetLocalResort(.projnum).Split("|")
                                    AccountInfo.ResortID = sTmpRes(0)
                                    AccountInfo.ResortName = StrConv((sTmpRes(1) & ""), VbStrConv.ProperCase)
                                End If

                                AccountInfo.Weeks = .weeks

                                'Accumulate the same project, otherwise add new ones
                                If sCurProj = AccountInfo.ResortID Then
                                    'need to do this if owner was a sampler prior to becoming an owner; owner holds multiple account
                                    If alAccounts.Count = 0 Then

                                        If AccountInfo.ProjNum = "32" Then
                                            Session("Players") = "YES"
                                        ElseIf AccountInfo.ProjNum = "79" Then
                                            Session("Pono") = "YES"
                                        Else
                                            alAccounts.Add(AccountInfo)
                                        End If

                                    End If

                                    alAccounts(alAccounts.Count - 1).Weeks &= "," & AccountInfo.Weeks
                                    Dim cBal As Double = Convert.ToDouble(alAccounts(alAccounts.Count - 1).paymentBalance)
                                    cBal = cBal + Convert.ToDouble(AccountInfo.PaymentBalance)
                                    alAccounts(alAccounts.Count - 1).paymentBalance = cBal.ToString()
                                Else
                                    'Filter Out Sampler Owners
                                    If AccountInfo.ProjNum = "32" Then
                                        'Onwer Project is 50 and home project is 51 allow to access the web site.
                                        bSamplerHolder = True
                                        Session("Players") = "YES"
                                    ElseIf AccountInfo.ProjNum = "79" Then
                                        bSamplerHolder = True
                                        Session("Pono") = "YES"
                                    Else
                                        alAccounts.Add(AccountInfo)

                                    End If
                                End If



                                sCurProj = AccountInfo.ResortID

                                'Add up a total balance
                                dTotal = dTotal + Convert.ToDouble(.amt)
                                If AccountInfo.ProjNum = "51" OrElse AccountInfo.ProjNum = "52" Then
                                    alAccounts.Remove(AccountInfo)
                                End If

                            End If
                        End With
                        counter = counter + 1
                    Loop

                    'End While

                    Session("OwnerPaymentBalance") = dTotal
                    Session("EncoreMaintenanceFeeBalance") = Session("OwnerPaymentBalance")

                    Session("LoginAccountList") = alAccounts

                    'If user only holds a sampler account
                    If bSamplerHolder And alAccounts.Count = 0 Then

                        'Session("SamplerOnlyUser") = "SAMPLER"

                        If Session("Players") = "YES" Then
                            Session("SamplerOnlyUser") = "Players"
                        ElseIf Session("Pono") = "YES" Then
                            Session("SamplerOnlyUser") = "Pono"
                        End If

                    End If

                    'If there was only one account then let's get it set up

                    If alAccounts.Count = 1 And bxgOwner.User(0).isSampler = True Then
                        .OwnerContractType = "Sampler"
                        Session("OwnerContractType") = "Sampler"
                        Session("OwnerHomeResort") = alAccounts(0).ResortID
                        Session("OwnerHomeProjectNumber") = alAccounts(0).ProjNum
                        Session("OwnerHomeResortWeeks") = alAccounts(0).Weeks
                        Session("OwnerVIP") = alAccounts(0).VIP
                    ElseIf alAccounts.Count = 1 And bxgOwner.User(0).isSampler = False Then
                        .OwnerContractType = alAccounts(0).ContractType
                        Session("OwnerContractType") = alAccounts(0).ContractType
                        Session("OwnerHomeResort") = alAccounts(0).ResortID
                        Session("OwnerHomeProjectNumber") = alAccounts(0).ProjNum
                        Session("OwnerHomeResortWeeks") = alAccounts(0).Weeks
                        Session("OwnerVIP") = alAccounts(0).VIP
                    End If

                    alAccounts = Nothing

                Catch ex As Exception
                    Response.Redirect(ex.Message)
                    Dim sErr As String = ex.Message
                Finally

                End Try

            End With

        End Sub

        Private Function GetLocalResort(ByVal sResortID As String) As String

            Dim C As New clsDBConnectivity

            'Declarations
            Dim sLocalResort As String = " | "
            Dim drResort As SqlClient.SqlDataReader

            'Set up query
            C.dbCmnd.CommandText = "uspSelectResortID"
            C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
            C.dbCmnd.Parameters.Clear()
            C.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjNum", System.Data.SqlDbType.Int, 4)).Value = Convert.ToInt32(sResortID)
            drResort = C.dbCmnd.ExecuteReader()

            While drResort.Read()
                sLocalResort = drResort("ResortID") & "|" & Trim(drResort("ResortName") & "")
            End While

            'Cleanup
            drResort.Close()
            C.dbCmnd.Dispose()
            C.Close()

            Return sLocalResort

        End Function

        Private Sub TrackOwnerLogin()

            If ChangePassword Then

                If Session("AgentLoginID") <> "" Then
                    'LOG HERE to AS400
                    Dim msg As New StringBuilder
                    msg.Append("logged in BGO as owner from: ")
                    msg.Append(HttpContext.Current.Request.UserHostAddress)

                    insertAs400Comments(msg.ToString)
                End If

                'Commented for Bug# 83668-Arvact still getting old change password screen
                'LoginEgress("owner/resetpassword.aspx")
                LoginEgress(Nothing, True)
            End If

            'Log to general log if rep has logged in as an owner.
            'If VSSAcookie IsNot Nothing Then
            If Session("AgentLoginID") <> "" Then

                'GeneralLogEntry(Session("ownernumber"), userIP, "logged in as user", "Audit", 0)
                'LOG HERE to AS400
                Dim msg As New StringBuilder
                msg.Append("logged in BGO as owner from: ")
                msg.Append(HttpContext.Current.Request.UserHostAddress)

                insertAs400Comments(msg.ToString)

            End If

        End Sub

        Sub populateOwnerLegacySessionVars()

            'Populate session objects here
            Session("OwnerNumber") = bxgOwner.Arvact.ToString
            Session("OwnerIdentity") = bxgOwner.ownerID
            Session("OwnerNamePrefix") = StrConv(bxgOwner.namePrefix, VbStrConv.ProperCase)
            Session("OwnerNameFirst") = bxgOwner.firstName
            Session("OwnerNameMiddle") = StrConv(bxgOwner.middleName, VbStrConv.ProperCase)
            Session("OwnerNameLast") = bxgOwner.lastName
            Session("OwnerNameSuffix") = StrConv(bxgOwner.nameSuffix, VbStrConv.ProperCase)
            Session("OwnerEmailAddress") = bxgOwner.Email.ToString
            Session("OwnerAddress1") = bxgOwner.Address1
            Session("OwnerAddress2") = bxgOwner.Address2
            Session("OwnerCity") = bxgOwner.City
            Session("OwnerPostalCode") = bxgOwner.PostalCode
            Session("OwnerSubdivision") = bxgOwner.Subdivision
            Session("OwnerStateAbr") = bxgOwner.StateAbr
            Session("OwnerCountryCode") = bxgOwner.CountryCode
            Session("OwnerHomePhone") = bxgOwner.HomePhone
            Session("OwnerAlternatePhone") = bxgOwner.AlternatePhone
            Session("OwnerLast4SSN") = Trim(Right("    " & bxgOwner.last4SSN, 4))
            Session("OwnerEncoreDividends") = bxgOwner.encoreDividends
            Session("OwnerPoints") = bxgOwner.PointsTotal
            Session("OwnerOriginalPoints") = bxgOwner.PointsTotalAnnual
            Session("OwnerSavedPoints") = bxgOwner.PointsTotalSaved
            Session("AccountNumber") = bxgOwner.accountNumber
            Session("Expires") = bxgOwner.OwnerExpiration
            If bxgOwner.User(0).AllAccountsComesFromSecondaryMarketing.ToString.ToLower.Equals("false") Then
                Session("secIdentity") = bxgOwner.User(0).AllAccountsComesFromSecondaryMarketing
            End If
        End Sub

        Sub populateOwnerLegacyObject()
            With owner
                .OwnerVCNumber = bxgOwner.Arvact.ToString
                .OwnerId = bxgOwner.ownerID
                .OwnerNamePrefix = StrConv(bxgOwner.namePrefix, VbStrConv.ProperCase)
                .OwnerFirstName = StrConv(Trim(bxgOwner.firstName), VbStrConv.ProperCase)
                .OwnerMiddleName = StrConv(bxgOwner.middleName, VbStrConv.ProperCase)
                .OwnerLastName = StrConv(bxgOwner.lastName, VbStrConv.ProperCase)
                .OwnerNameSuffix = StrConv(bxgOwner.nameSuffix, VbStrConv.ProperCase)
                .OwnerEmailAddress = bxgOwner.Email.ToString
                .OwnerAddress1 = bxgOwner.Address1
                .OwnerAddress2 = bxgOwner.Address2
                .OwnerCity = bxgOwner.City
                .OwnerPostalCode = bxgOwner.PostalCode
                .OwnerSubdivision = bxgOwner.Subdivision
                .OwnerStateAbr = bxgOwner.StateAbr
                .OwnerCountryCode = bxgOwner.CountryCode
                .OwnerHomePhone = bxgOwner.HomePhone
                .OwnerAlternatePhone = bxgOwner.AlternatePhone
                .OwnerLast4SSN = bxgOwner.last4SSN
                .OwnerPaymentBalance = bxgOwner.PaymentBalance()
                .OwnerEncoreDividends = bxgOwner.encoreDividends
                .OwnerAnnualPoints = bxgOwner.PointsTotalAnnual
                .OwnerSavedPoints = bxgOwner.PointsTotalSaved
                .OwnerRestrictedPoints = bxgOwner.PointsTotalRestricted
                .OwnerTotalPoints = bxgOwner.PointsTotal
                .OwnerPaymentBalance = bxgOwner.PaymentBalance
                .OwnerClubLevelDescription = bxgOwner.membershipLevelDesc
                .OwnerMembershipType = bxgOwner.membershipLevel
                .OwnerAccountNumber = bxgOwner.accountNumber
                .OwnerTPLevel = bxgOwner.TravelerPlusMembership.TPLevel

            End With

        End Sub

        Sub checkExposeBonusTime()

            Dim sAgent As String = Session("AgentLoginID") & ""
            Dim sBTBlockFixedOwners As String = System.Configuration.ConfigurationManager.AppSettings("BonusTimeBlockFixedOwners")
            Dim sBTEnabled As String = System.Configuration.ConfigurationManager.AppSettings("BonusTimeEnabled")

            With owner
                If sAgent.Length > 0 Or sBTEnabled = "TRUE" Then
                    Session("BonusTimeEnabled") = "TRUE"
                    .BonusTimeEnabled = True
                Else
                    Session("BonusTimeEnabled") = "FALSE"
                    .BonusTimeEnabled = False
                End If

                If sBTBlockFixedOwners = "TRUE" Then
                    Session("BonusTimeBlockFixedOwners") = "TRUE"
                    .BonusTimeBlockFixedOwners = True
                Else
                    Session("BonusTimeBlockFixedOwners") = "FALSE"
                    .BonusTimeBlockFixedOwners = False
                End If
            End With

        End Sub

        Sub checkPasswordChange()

            If bxgOwner.ChPWDflag Then

                ChangePassword = bxgOwner.ChPWDflag
            End If

        End Sub

        Sub checkForTravelerPlusEmployeeLogin()

            Dim CVC As New clsDBConnectivityVC

            If Session("IsTravelerPlusEmployee") = "TRUE" Then

                'Check and Redirect for traveler plus logins
                Dim sIsTPEligibleEmployee As String = "NO"
                Dim drTPEmployee As SqlClient.SqlDataReader

                CVC.dbCmnd.CommandText = "SamplerPlus.dbo.uspVerifyTPEmployeeEligibility"
                CVC.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure
                CVC.dbCmnd.Parameters.Clear()
                CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EMAIL", System.Data.SqlDbType.VarChar, 50)).Value = Session("TPEmployeeEmail")
                CVC.dbCmnd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PASSWORD", System.Data.SqlDbType.VarChar, 50)).Value = Session("TPEmployeePassword")
                drTPEmployee = CVC.dbCmnd.ExecuteReader()

                If drTPEmployee.HasRows Then
                    sIsTPEligibleEmployee = "YES"
                    While drTPEmployee.Read
                        Session("EmployeeFirstName") = drTPEmployee("firstName")
                        Session("EmployeeLastName") = drTPEmployee("lastName")
                        Session("EmployeeEmail") = drTPEmployee("Email")
                        Session("EmployeePhone") = drTPEmployee("Phone")
                        Session("EmployeeID") = drTPEmployee("memberid")
                        Session("EmployeeZip") = drTPEmployee("zip")
                        Session("EmployeeExpireDate") = drTPEmployee("expiredate")
                    End While
                End If

                'A little dab of cleanup
                drTPEmployee.Close()

                ''25-Nov-2015 create the forms authentication 
                'Dim ticket As FormsAuthenticationTicket = New FormsAuthenticationTicket(1, Session("EmployeeEmail"), DateTime.Now, DateTime.Now.AddMinutes(30), False, "Authenticated", FormsAuthentication.FormsCookiePath)

                '' Encrypt the ticket using the machine key
                'Dim encryptedTicket As String = FormsAuthentication.Encrypt(ticket)

                '' Add the cookie to the request to save it
                'Dim cookie As HttpCookie = New HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                'cookie.HttpOnly = True

                'If ticket.IsPersistent Then
                '    cookie.Expires = ticket.Expiration
                'End If

                'Response.Cookies.Add(cookie)
                'Forms authentication ticket code above

                If sIsTPEligibleEmployee = "YES" Then
                    'Code modified by Siva in order to allow the Employees to access the BGO cruise services
                    bxgOwnerTemp = ownerService.fetchOwner(Session("EmployeeID"), "", "", "", "")
                    Session("OwnerNumber") = bxgOwnerTemp.Arvact.ToString
                    Session("BXGOwner") = bxgOwnerTemp
                    Bluegreenowner.CurrentOwner = owner
                    LoginEgress("TravelerPlus/owner/home.aspx?", True)
                Else
                    bSuccess = False
                End If
            End If

            CVC.dbCmnd.Dispose()
            CVC.Close()

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

End Namespace
